using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Survivatron.ViewFrames
{
    public class TileHandler
    {
        /* Singleton constructor and get. */
        /* 
         * Instances[0]: The basic "ironhand" tileset.
         * Instances[1]: The "dwarves" tileset.
         * Instances[2]: the "critters" tileset.
         */
        public static TileHandler[] Instances { get; private set; }
        public static TileHandler[] Construct(Texture2D[] tileSets, int tileEdge)
        {
            Instances = new TileHandler[tileSets.Length];
            for (int i = 0; i < tileSets.Length; i++)
                Instances[i] = new TileHandler(tileSets[i], tileEdge);
            return Instances;
        }

        public Texture2D TileSet { get; set; }
        public int TileEdge { get; private set; }
        private int rows;
        private int columns;

        private TileHandler(Texture2D tileSet, int rows, int columns)
        {
            this.TileSet = tileSet;
            this.rows = rows;
            this.columns = columns;
            this.TileEdge = tileSet.Bounds.Height / rows;
        }

        // TileSet is assumed square, and tileEdge is assumed to be a dividend of tileSet bounds.
        public TileHandler(Texture2D tileSet, int tileEdge) 
        {
            this.TileSet = tileSet;
            this.TileEdge = tileEdge;
            this.rows = (this.TileSet.Bounds.Height / tileEdge);
            this.columns = (this.TileSet.Bounds.Width / tileEdge);
        }


        public Rectangle getChar(char character)
        {
            int offset = (int)character;
            return getRect((offset % columns), (int)(offset / rows), TileEdge);
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
