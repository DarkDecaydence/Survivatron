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
    public class Map
    {
        public Column[] columns { get; set; }
        private TileHandler[] tileHandlers;

        public Map(int dimX, int dimY, TileHandler[] tileHandlers)
        {
            columns = new Column[dimX];
            this.tileHandlers = tileHandlers;
            //mapObjects = new List<GameObject>();

            for (int i = 0; i < dimX; i++)
            { columns[i] = new Column(dimY); }
        }

        // Returns a cropped part of the current map, with the objects related to the cropped map.
        public Map CroppedMap(Rectangle rect)
        {
            Map cropped = new Map(0, 0, tileHandlers);
            cropped.columns = Crop(rect);
            //cropped.mapObjects = MapController.FindObjects(cropped.columns);
            return cropped;
        }

        public Column[] Crop(int x, int y, int width, int height)
        {
            if (width < 1 || height < 1)
                return null;
            if (x < 0 || (x + width) > columns.Length)
                return null;
            if (y < 0 || (y + height) > columns[x + width - 1].rows.Length) // x + width - 1 required, since previous calls are not safely within column length.
                return null;

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

        //---- Draw ----

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            int objectsCount;
            for (int i = 0; i < columns.Length; i++)
            {
                for (int j = 0; j < columns[i].rows.Length; j++)
                {
                    objectsCount = columns[i].rows[j].objects.Count;
                    GameObject curObject = columns[i].rows[j].objects[0];
                    spriteBatch.Draw(tileHandlers[0].tileSet, new Vector2(i * 18, j * 18), tileHandlers[0].getChar(curObject.Representation), Color.Green);
                    if (objectsCount > 1)
                    {
                        curObject = columns[i].rows[j].objects[objectsCount - 1];
                        switch (curObject.FType)
                        {
                            case GameObjectType.BASIC: spriteBatch.Draw(tileHandlers[0].tileSet, new Vector2(i * 18, j * 18), tileHandlers[0].getChar(curObject.Representation), Color.White); break;
                            case GameObjectType.PLAYER: spriteBatch.Draw(tileHandlers[1].tileSet, new Vector2(i * 18, j * 18), tileHandlers[1].getChar(curObject.Representation), Color.White); break;
                            case GameObjectType.CRITTER: spriteBatch.Draw(tileHandlers[2].tileSet, new Vector2(i * 18, j * 18), tileHandlers[2].getChar(curObject.Representation), Color.White); break;
                        }
                    }
                }
            }

            spriteBatch.End();
        }
    }
}
