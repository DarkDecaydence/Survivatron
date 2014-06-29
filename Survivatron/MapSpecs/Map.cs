using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Survivatron.GameActions;
using Survivatron.GameEvents;
using Survivatron.GameObjects;
using Survivatron.GameObjects.Dynamics;

namespace Survivatron.MapSpecs
{
    /*
     * The Map class is responsible for arranging objects in relation to eachother.
     * The mapObjects list is a list of dynamic objects on the map; i.e. everything but the floors.
     * This list is used to quickly find the position of different objects, and move them.
     */
    public class Map : IMap
    {
        /* Object List */
        public List<GameObject> MapObjects { get; private set; }
        public List<Dynamic> Dynamics { get; set; }
        public Vector2 Dimensions { get; private set; }
        public bool Locked { get; private set; }

        /* Interface methods */
        public virtual void ProcessTurn()
        {
            var nextMap = (Map)this.Crop(0, 0, (int)Dimensions.X, (int)Dimensions.Y);
            foreach (Dynamic gObj in Dynamics)
            {
                
            }
            foreach (Dynamic gObj in Dynamics)
            {
                gObj.NextAction.Execute();
                gObj.NextAction = ActionHandler.CreateWait();
            }
        }

        public GameObject GetObject(GameObject target)
        { return MapObjects.Find(new Predicate<GameObject>(gObj => gObj.Equals(target))); }

        public void SetObject(GameObject target)
        { MapObjects.Add(target); }

        public override bool Equals(object obj)
        {
            //Checks if cast is possible.
            Map mapCast = (Map)obj;
            if (mapCast == null)
                return false;

            return mapCast.Dimensions.Equals(this.Dimensions);
                //&& mapCast.MapObjects.Equals(this.MapObjects);
        }

        /* Constructor and help methods */

        public Map() : this(0,0)
        { }

        public Map(int width, int height)
        {
            MapObjects = new List<GameObject>();
            Dynamics = new List<Dynamic>();
            Dimensions = new Vector2(width, height);
            Locked = true;
        }

        // Returns a cropped part of the current map, with the objects related to the cropped map.
        public IMap Crop(int x, int y, int width, int height)
        {
            Map croppedMap = new Map();
            croppedMap.Dimensions = this.Dimensions;
            croppedMap.MapObjects = this.MapObjects.FindAll(new Predicate<GameObject>(gObj => 
                gObj.Position.X >= x && gObj.Position.X < x + width &&
                gObj.Position.Y >= y && gObj.Position.Y < y + height)
                );
            return (IMap)croppedMap;
        }

    }
}
