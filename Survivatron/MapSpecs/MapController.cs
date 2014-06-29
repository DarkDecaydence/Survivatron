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
        private Dictionary<GOID, int[]> actionQueue = new Dictionary<GOID, int[]>();

        /* Singleton constructor and get. */
        public static MapController MCInstance { get; private set; }
        public static MapController GetInstance()
        {
            if (MCInstance == null)
            { throw new Exception("MCInstance is null. Check if constructed."); }
            return MCInstance;
        }

        public static MapController Construct(Map map)
        {
            MCInstance = new MapController(map);
            return MCInstance;
        }

        public void ProcessTurn()
        { Current.ProcessTurn(); }

        public Vector2 GetDimensions()
        { return Current.Dimensions; }

        private MapController(Map map)
        { Current = map; }

        /* Interface Methods */
        public IMap Crop(Rectangle rect)
        { return (Map)Current.Crop(rect.X, rect.Y, rect.Width, rect.Height); }

        // Only gets and sets to the gameobject list, not to the map itself.
        public virtual GameObject GetGameObject(GOID goid)
        { return Current.MapObjects.Find(new Predicate<GameObject>(gObj => gObj.ID.Equals(goid))); }

        public virtual bool SetGameObject(GameObject newObject)
        {
            GameObject target = GetGameObject(newObject.ID);
            if (target == null)
            { Current.SetObject(newObject); }
            else
            { target = newObject; }

            return true;
        }

        public GameObjectMass HasSolid(int x, int y)
        {
            GameObject found = Current.MapObjects.Find(new Predicate<GameObject>(gObj => gObj.Position.Equals(new Vector2(x, y))));

            if (found != null)
            { return found.Mass; }
            else return GameObjectMass.NONE;
        }

        // Frequency is measured in the chance that a tree will appear.
        // A tree cannot appear on a row that already contains a solid.
        public void AddTrees(int frequency)
        {
            Random newRand = new Random();

            for (int i = 0; i < (int)Current.Dimensions.X; i++)
            {
                for (int j = 0; j < (int)Current.Dimensions.Y; j++)
                {
                    if (HasSolid(i, j) == GameObjectMass.NONE && newRand.Next(frequency) == 0)
                    {
                        Tree newTree = new Tree();
                        newTree.Position = new Vector2(i, j);
                        SetGameObject(newTree);
                    }
                }
            }
        }

        public void AddDynamic(Vector2 position, ref Dynamic dynamic)
        {
            dynamic.Position = position;
            SetGameObject(dynamic);
            Current.Dynamics.Add(dynamic);
        }
    }
}
