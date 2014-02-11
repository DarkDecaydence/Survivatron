using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Survivatron.MapSpecs;
using Survivatron.GameObjects;

namespace Survivatron.ViewFrames
{
    public class ViewFrame : IViewFrame
    {
        public Map curMap { get; set; }
        public Rectangle dimensions { get; set; } // This is the dimensions in rows, not pixels.
        private MapController mc;

        //Frame requires the world map to create a crop.
        public ViewFrame(Rectangle dimensions)
        {
            this.dimensions = dimensions;
            mc = MapController.GetInstance();
            curMap = (Map)mc.GetZone(dimensions);
        }

        public ViewFrame(int x, int y, int width, int height)
            : this(new Rectangle(x, y, width, height))
        { }

        public ViewFrame()
            : this(new Rectangle())
        { }

        public void MoveFrame(int x, int y)
        {
            var tempDim = dimensions;
            tempDim.Offset(x, y);

            mc = MapController.GetInstance();
            Vector2 worldMapDims = mc.GetDimensions();

            if (tempDim.X < 0 || (tempDim.X + tempDim.Width) >= worldMapDims.X)
                return;
            if (tempDim.Y < 0 || (tempDim.Y + tempDim.Height) >= worldMapDims.Y)
                return;

            dimensions = tempDim;
            curMap = (Map)mc.GetZone(dimensions);
        }

        public void MoveFrame(Vector2 moveVector)
        { MoveFrame((int)moveVector.X, (int)moveVector.Y); }

        public void ChangeFrame(Rectangle dimensions)
        { this.dimensions = dimensions; }

        // Draws the map part that is currently in the frame.

        //---- Draw ----

        public void Draw(SpriteBatch spriteBatch)
        {
            MapController mc = MapController.GetInstance();
            TileHandler[] ths = TileHandler.Instances;
            curMap = (Map)mc.GetZone(dimensions);

            spriteBatch.Begin();

            for (int i = 0; i < dimensions.Width; i++)
            {
                for (int j = 0; j < dimensions.Height; j++)
                {
                    /* Current position. */
                    Row curField = curMap.columns[i].rows[j];
                    Vector2 drawPos = new Vector2(i * ths[0].TileEdge, j * ths[0].TileEdge);

                    /* Draws green floor. */
                    GameObject curObject = curField.objects[0];
                    spriteBatch.Draw(ths[0].TileSet, drawPos, ths[0].getChar(curObject.Representation), Color.Green);

                    // Draws top object in the current field
                    if (curField.objects.Count > 1)
                    {
                        curObject = curField.objects[curField.objects.Count - 1];
                        TileHandler curTH;
                        switch (curObject.FType)
                        {
                            case GameObjectType.BASIC:      curTH = ths[0]; break;
                            case GameObjectType.PLAYER:     curTH = ths[1]; break;
                            case GameObjectType.CRITTER:    curTH = ths[2]; break;
                            default:                        curTH = ths[0]; break;
                        }

                        spriteBatch.Draw(curTH.TileSet, drawPos, curTH.getChar(curObject.Representation), Color.White);
                    }
                }
            }

            spriteBatch.End();

        }
    }
}
