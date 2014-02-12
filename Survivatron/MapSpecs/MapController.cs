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
            Current = map;
            MapObjects = FindObjects(Current.columns);
            foreach (GameObject gObj in MapObjects)
                SetGameObject(gObj);
        }

        /* Interface Methods */
        public virtual IMap GetZone(Rectangle rect)
        { return Current.GetZone(rect); }

        public virtual Vector2 GetDimensions()
        { return Current.GetDimensions(); }

        // Only gets and sets to the gameobject list, not to the map itself.
        public virtual GameObject GetGameObject(GOID goid)
        { return MapObjects.Find(new Predicate<GameObject>(gObj => gObj.ID.Equals(goid))); }

        public virtual bool SetGameObject(GameObject newObject)
        {
            GameObject target = GetGameObject(newObject.ID);
            if (target != null)
            {
                MapObjects.Remove(target);
                Current.SetZone(target.Position, ToggleObject(target));
            }

            MapObjects.Add(newObject);
            Current.SetZone(newObject.Position, ToggleObject(newObject));

            return true;
        }

        private IMap ToggleObject(GameObject gameObject)
        {
            Map gObjZone = (Map)Current.GetZone(new Rectangle((int)gameObject.Position.X, (int)gameObject.Position.Y, 1, 1));
            gObjZone.columns[0].rows[0].ToggleObject(gameObject);
            return (IMap)gObjZone;
        }

        public static bool HasSolid(Row row)
        {
            foreach (GameObject g in row.Objects)
              if (g.Solid) { return true; }

            return false;
        }

        public static List<GameObject> FindObjects(Column[] columns)
        {
            List<GameObject> found = new List<GameObject>();

            foreach (Column c in columns)
                foreach (Row r in c.rows)
                    if (r.Objects.Count > 1)
                        found.AddRange(r.Objects.GetRange(1, r.Objects.Count - 1));

            return found;
        }

        // Frequency is measured in the chance that a tree will appear.
        // A tree cannot appear on a row that already contains a solid.
        public void AddTrees(Map map, int frequency)
        {
            if (frequency < 1) { return; }
            Random rand = new Random();

            int width = map.columns.Length;
            int height = map.columns[0].rows.Length;

            int fields = width * height;

            for (int treecount = fields / frequency; treecount != 0; treecount--)
            {
                var nextTree = new Tree();
                nextTree.Position = new Vector2(rand.Next(width), rand.Next(height));
                while (!map.GetRow((int)nextTree.Position.X, (int)nextTree.Position.Y).isFree())
                { nextTree.Position = new Vector2(rand.Next(width), rand.Next(height)); }
                ToggleObject(nextTree);
            }
        }

        public void AddDynamic(Vector2 position, ref Dynamic dynamic)
        {
            dynamic.Position = position;
            SetGameObject(dynamic);
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
                        Current.columns[newX].rows[newY].Objects.Add(f);
                        Current.columns[x].rows[y].Objects.Remove(f);
                        f.Position = new Vector2(newX, newY);
                    }
                    return;
                }
            }
        }
    }
}
