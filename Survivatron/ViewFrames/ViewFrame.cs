﻿using System;
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
        private static Random rand = new Random();
        private int frameSeed = rand.Next();
        public Map curMap { get; set; }
        public Rectangle dimensions { get; set; } // This is the dimensions in rows, not pixels.
        private MapController mc;

        /* Interface methods: */
        public virtual IViewFrame Pan(Vector2 direction)
        {
            MoveFrame(direction);
            return this;
        }

        //Frame requires the world map to create a crop.
        public ViewFrame(Rectangle dimensions)
        {
            mc = MapController.GetInstance();
            curMap = (Map)mc.GetZone(dimensions);
            dimensions = CorrectDimensions(dimensions);
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

            Vector2 worldMapDims = mc.GetDimensions();

            /*
            if (tempDim.X < 0 || (tempDim.X + tempDim.Width) >= worldMapDims.X)
                return;
            if (tempDim.Y < 0 || (tempDim.Y + tempDim.Height) >= worldMapDims.Y)
                return;
            */

            dimensions = tempDim;
        }

        public void MoveFrame(Vector2 moveVector)
        { MoveFrame((int)moveVector.X, (int)moveVector.Y); }

        public void ChangeFrame(Rectangle dimensions)
        { this.dimensions = CorrectDimensions(dimensions); }

        private Rectangle CorrectDimensions(Rectangle dimensions)
        {
            Vector2 mapDimensions = mc.GetDimensions();

            if (dimensions.Width < 1)
                dimensions.Width = 1;
            if (dimensions.Height < 1)
                dimensions.Height = 1;
            /*
            if (dimensions.X < 0)
                dimensions.X = 0;
            if (dimensions.Y < 0)
                dimensions.Y = 0;
            if ((dimensions.X + dimensions.Width) > mapDimensions.X)
                dimensions.X = (int)mapDimensions.X - dimensions.Width;
            if ((dimensions.Y + dimensions.Height) > mapDimensions.Y)
                dimensions.Y = (int)mapDimensions.Y - dimensions.Height;
             */

            return new Rectangle(dimensions.X, dimensions.Y, dimensions.Width, dimensions.Height);
        }

        // Draws the map part that is currently in the frame.

        //---- Draw ----

        public void Draw(SpriteBatch spriteBatch)
        {
            TileHandler[] ths = TileHandler.Instances;
            curMap = (Map)mc.NewCrop(dimensions);
            Vector2 drawPos = new Vector2(0,0);

            spriteBatch.Begin();

            // Math commencing
            for (int i = 0; i < dimensions.Width; i++) {
                for (int j = 0; j < dimensions.Height; j++) {
                    drawPos = new Vector2(i*18, j*18);
                    Random ranGen = new Random(frameSeed-(dimensions.X + i) * (dimensions.Y + j));
                    spriteBatch.Draw(ths[0].TileSet, drawPos, ths[0].getChar((char)(ranGen.Next(ranGen.Next(255 + dimensions.X + i) * ranGen.Next(255 + dimensions.Y + j)) % 4 + 151)), Color.Green);
                }
            }

            foreach (GameObject gObj in curMap.MapObjects)
            {
                /* Current Position */
                drawPos = new Vector2((gObj.Position.X - dimensions.X) * ths[0].TileEdge, (gObj.Position.Y - dimensions.Y) * ths[0].TileEdge);

                TileHandler curTH;
                switch (gObj.FType)
                {
                    case GameObjectType.BASIC: curTH = ths[0]; break;
                    case GameObjectType.PLAYER: curTH = ths[1]; break;
                    case GameObjectType.CRITTER: curTH = ths[2]; break;
                    default: curTH = ths[0]; break;
                }

                spriteBatch.Draw(curTH.TileSet, drawPos, curTH.getChar(gObj.Representation), Color.White);
            }

            spriteBatch.End();

        }
    }
}
