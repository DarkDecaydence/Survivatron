﻿using System;
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
        /* Concrete board */
        public Column[] columns { get; set; }

        /* Interface methods */
        public virtual IMap GetZone(Rectangle rect)
        { return (IMap)CroppedMap(rect); }

        public virtual IMap SetZone(Vector2 origin, IMap newMap)
        {
            int j = 0;
            for (int i = ((int)origin.X);
                i < (((Map)newMap).columns.Length + ((int)origin.X));
                i++)
            { columns[i].SetZone((int)origin.Y, ((Map)newMap).columns[j++]); }

            return this;
        }

        public virtual IMap ProcessTurn()
        {
            return null;
        }

        public Vector2 GetDimensions()
        { return new Vector2(columns.Length, columns[0].rows.Length); }

        public override bool Equals(object obj)
        {
            //Checks if cast is possible.
            Map mapCast = (Map)obj;
            if (mapCast == null)
                return false;

            //Checks if the length is equal.
            if (columns.Length != mapCast.columns.Length)
            { return false; }
            else // Checks recursively if the columns are equal
            {
                int i = 0;
                for (Column c = columns[i]; i < columns.Length; i++)
                    if (!c.Equals(mapCast.columns[i]))
                        return false;
            }
            return true;
        }

        /* Constructor and help methods */

        public Map(int dimX, int dimY)
        {
            columns = new Column[dimX];
            //this.tileHandlers = tileHandlers;
            //mapObjects = new List<GameObject>();

            for (int i = 0; i < dimX; i++)
            { columns[i] = new Column(dimY); }
        }

        // Returns a cropped part of the current map, with the objects related to the cropped map.
        public Map CroppedMap(Rectangle rect)
        {
            Map cropped = new Map(0, 0);
            cropped.columns = Crop(rect);
            //cropped.mapObjects = MapController.FindObjects(cropped.columns);
            return cropped;
        }

        public Column[] Crop(int x, int y, int width, int height)
        {
            if (width < 1 || height < 1)
                return null;
            if (x < 0) return Crop(0, y, width, height);
            if ((x + width) > columns.Length) return Crop((columns.Length - (x + width)), y, width, height);
            if (y < 0) return Crop(x, 0, width, height);
            if ((y + height) > columns[0].rows.Length) return Crop(x, (columns[0].rows.Length - (y + height)), width, height);

            Column[] cropped = new Column[width];
            int j = 0;
            for (int i = x; i < (x + width); i++)
            { cropped[j++] = columns[i].Crop(y, y+height); }
            return cropped;
        }

        public Column[] Crop(Rectangle rect)
        { return Crop(rect.X, rect.Y, rect.Width, rect.Height); }

        public Row GetRow(int x, int y)
        { return columns[x].rows[y]; }
    }
}
