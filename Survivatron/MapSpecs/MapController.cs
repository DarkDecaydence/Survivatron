using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Survivatron.GameActions;
using Survivatron.GameEvents;
using Survivatron.GameObjects;
using Survivatron.GameObjects.Dynamics;
using Survivatron.GameObjects.Statics;

namespace Survivatron.MapSpecs
{
    public class MapController : IMapController
    {
        /* Fields */
        public Map Current { get; set; }
        public List<GameObject> MapObjects { get; private set; }
        private Dictionary<GOID, int[]> actionQueue = new Dictionary<GOID, int[]>();

        /* Singleton constructor and get. */
        public static MapController MCInstance { get; private set; }
        public static MapController GetInstance()
        {
            if (MCInstance == null)
            { throw new Exception("MCInstance is null. Check if constructed."); }
            return MCInstance; }

        public static MapController Construct(Map map)
        {
            MCInstance = new MapController(map);
            return MCInstance;
        }

        private MapController(Map map)
        {
            Dynamic.Ready += ReadyUp;
            Current = map;
            MapObjects = FindObjects(Current.columns);
        }

        /* Interface Methods */
        public virtual IMap GetZone(Rectangle rect)
        { return Current.GetZone(rect); }

        public virtual Vector2 GetDimensions()
        { return new Vector2(Current.columns.Length, Current.columns[0].rows.Length); }

        public virtual GameObject GetGameObject(GOID goid)
        { return MapObjects.Find(new Predicate<GameObject>(gObj => gObj.ID.Equals(goid))); }

        public virtual bool SetGameObject(GameObject gameObject)
        {
            GameObject target = MapObjects.Find(new Predicate<GameObject>(gObj => gObj.ID.Equals(gameObject.ID)));
            if (target != null) { return true; }
            else { return false; }
        }

        /* Static class. */
        public static event CallEventHandler CallOut;

        public static bool HasSolid(Row row)
        {
            foreach (GameObject g in row.objects)
              if (g.Solid) { return true; }

            return false;
        }

        public static List<GameObject> FindObjects(Column[] columns)
        {
            List<GameObject> found = new List<GameObject>();

            foreach (Column c in columns)
                foreach (Row r in c.rows)
                    if (r.objects.Count > 1)
                        found.AddRange(r.objects.GetRange(1, r.objects.Count - 1));

            return found;
        }

        // Frequency is measured in the chance that a tree will appear.
        // A tree cannot appear on a row that already contains a solid.
        public void AddTrees(Map map, int frequency)
        {
            if (frequency < 1) { return; }
            Random rand = new Random();

            int width = map.columns.Length;
            int height = map.columns[width - 1].rows.Length;

            int fields = width * height;

            for (int treecount = fields / frequency; treecount != 0; treecount--)
            {
                var x = rand.Next(width); var y = rand.Next(height);
                while (!map.GetRow(x, y).isFree())
                { x = rand.Next(width); y = rand.Next(height); }
                AddObject(new Vector2(x, y), new Tree());
            }
        }

        public void AddDynamics(Vector2 start, ref Dynamic[] dynamics)
        {
            Vector2 position = start;
            foreach (Dynamic d in dynamics)
            {
                AddObject(position, d);
                MapObjects.Add(d);
                d.Position = new Vector2(position.X, position.Y);
                position = Vector2.Add(position, new Vector2(3,3));
            }
        }

        public void AddObject(Vector2 position, GameObject gameObject)
        { Current.columns[(int)position.X].rows[(int)position.Y].objects.Add(gameObject); }

        public GameObject GetObject(GOID ID)
        {
            foreach (GameObject o in MapObjects)
                if (ID.Equals(o)) { return o; }

            return null;
        }

        public void MoveObject(GOID ID, Vector2 moveVector)
        {
            foreach (GameObject f in MapObjects)
            {
                if (ID.Equals(f.ID))
                {
                    int x, y, newX, newY;
                    x = (int)f.Position.X; y = (int)f.Position.Y;
                    newX = x + (int)moveVector.X;
                    newY = y + (int)moveVector.Y;

                    // Figures out if the move is valid.
                    // A valid move requires that newX is part of [0, columns[ and that newY is part of [0, columns[newX].rows[.
                    // It also requires that the target row is free from solid objects.
                    // TODO: Implement SOLID, SEMI-SOLID, NON-SOLID
                    bool withinBounds = (newX >= 0 && newX < Current.columns.Length) && (newY >= 0 && newY < Current.columns[newX].rows.Length);
                    bool free = false;
                    if (withinBounds) { free = !MapController.HasSolid(Current.columns[newX].rows[newY]); }
                    if (free)
                    {
                        Current.columns[newX].rows[newY].objects.Add(f);
                        Current.columns[x].rows[y].objects.Remove(f);
                        f.Position = new Vector2(newX, newY);
                    }
                    return;
                }
            }
        }

        private void ReadyUp(Dynamic sender, ActionEventArgs e)
        {
            if (actionQueue.ContainsKey(sender.ID))
            {
                actionQueue.Remove(sender.ID);
                actionQueue.Add(sender.ID, e.args);
            }
            else
            { actionQueue.Add(sender.ID, e.args); }

            int objectCount = MapObjects.Count;
            if (actionQueue.Count > objectCount) { throw new Exception("ReadyCounter out of range\n"); }
            if (actionQueue.Count == objectCount) { ExecuteActions(); }
        }

        private void ExecuteActions()
        {
            foreach (KeyValuePair<GOID, int[]> action in actionQueue)
                CallOut(new CallEventArgs(action.Key));
        }

        public void CallReturn(Dynamic dynam)
        {
            int[] args;
            if (actionQueue.TryGetValue(dynam.ID, out args))
            { dynam.NextAction.Execute(args); dynam.NextAction = null; }
        }

    }
}
