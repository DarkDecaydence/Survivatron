using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Survivatron
{
    public class TileHandler
    {
        public Texture2D tileSet { get; set; }
        private int tileEdge;
        private int rows;
        private int columns;

        public TileHandler(Texture2D tileSet, int rows, int columns)
        {
            this.tileSet = tileSet;
            this.rows = rows;
            this.columns = columns;
            this.tileEdge = tileSet.Bounds.Height / rows;
        }

        // TileSet is assumed square, and tileEdge is assumed to be a dividend of tileSet bounds.
        public TileHandler(Texture2D tileSet, int tileEdge) 
        {
            this.tileSet = tileSet;
            this.tileEdge = tileEdge;
            this.rows = (this.tileSet.Bounds.Height / tileEdge);
            this.columns = (this.tileSet.Bounds.Width / tileEdge);
        }

        public Rectangle getChar(char character)
        {
            int offset = (int)character;
            return getRect((offset % columns), (int)(offset / rows), tileEdge);
        }

        // Converts from tile coordinates to pixel coordinates.
        private Rectangle getRect(int col, int row, int edge)
        {
            return new Rectangle(
                (col*edge), (row*edge),
                edge, edge
                ); 
        }
    }
}
