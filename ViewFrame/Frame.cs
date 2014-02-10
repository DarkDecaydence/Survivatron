using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Survivatron.MapSpecs;
using Survivatron.GameObjects;

namespace Survivatron
{
    public class Frame
    {
        public Map curMap { get; set; }
        public Map worldMap { get; set; }
        public Rectangle dimensions { get; set; } // This is the dimensions in rows, not pixels.
        private TileHandler[] tileHandlers;

        //Frame requires the world map to create a crop.
        public Frame(Map map, Rectangle dimensions)
        {
            this.dimensions = dimensions;
            worldMap = map;
            curMap = worldMap.CroppedMap(dimensions);
        }

        public Frame(Map map, int x, int y, int width, int height)
            : this(map, new Rectangle(x, y, width, height))
        { }

        public Frame(Map map)
            : this(map, new Rectangle())
        { }

        public void MoveFrame(int x, int y)
        {
            var tempDim = dimensions;
            tempDim.Offset(x, y);

            if (tempDim.X < 0 || (tempDim.X + tempDim.Width) > (worldMap.columns.Length))
                return;
            if (tempDim.Y < 0 || (tempDim.Y + tempDim.Height) > worldMap.columns[tempDim.X + tempDim.Width - 1].rows.Length) // X + width - 1 required to avoid "Early-end-of-map" bug.
                return;

            dimensions = tempDim;
            curMap = worldMap.CroppedMap(dimensions);
        }

        public void MoveFrame(Vector2 moveVector)
        { MoveFrame((int)moveVector.X, (int)moveVector.Y); }

        public void ChangeFrame(Rectangle dimensions)
        { this.dimensions = dimensions; }

        // Draws the map part that is currently in the frame.

        //---- Draw ----

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            int objectsCount;
            for (int i = 0; i < curMap.columns.Length; i++)
            {
                for (int j = 0; j < curMap.columns[i].rows.Length; j++)
                {
                    objectsCount = curMap.columns[i].rows[j].objects.Count;
                    GameObject curObject = curMap.columns[i].rows[j].objects[0];
                    spriteBatch.Draw(tileHandlers[0].tileSet, new Vector2(i * 18, j * 18), tileHandlers[0].getChar(curObject.Representation), Color.Green);
                    foreach (GameObject gameObj in curMap.columns[i].rows[j].objects)
                    {
                        switch (gameObj.FType)
                        {
                            case GameObjectType.BASIC: spriteBatch.Draw(tileHandlers[0].tileSet,
                                new Vector2(i * 18, j * 18), tileHandlers[0].getChar(gameObj.Representation), Color.Transparent); break;
                            case GameObjectType.PLAYER: spriteBatch.Draw(tileHandlers[1].tileSet,
                                new Vector2(i * 18, j * 18), tileHandlers[1].getChar(gameObj.Representation), Color.Transparent); break;
                            case GameObjectType.CRITTER: spriteBatch.Draw(tileHandlers[2].tileSet,
                                new Vector2(i * 18, j * 18), tileHandlers[2].getChar(gameObj.Representation), Color.Transparent); break;
                        }
                    }
                }
            }

            spriteBatch.End();
        }
    }
}
