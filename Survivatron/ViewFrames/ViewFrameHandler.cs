using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Survivatron
{
    public class ViewFrameHandler
    {
        /*
         * It is always assumed that the frame has an uneven number of tiles.
         * This makes the character always stay in the middle of the frame.
         * Should the frame have an even number of tiles as dimensions, the
         * player character will be offset slightly.
         */

        public static bool IsValid(int playerPos, int frameDimension, int mapDimension)
        {
            int deadZone = (int)((frameDimension / 2));

            // mapDimension is 1-indexed, and should be 0-indexed.
            if (playerPos < deadZone || playerPos > (mapDimension - deadZone - 1))
            { return false; }

            return true;
        }

    }
}
