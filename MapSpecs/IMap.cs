using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survivatron.MapSpecs
{
    interface IMap
    {
        IMap GetZone(Rectangle rect);
        IMap SetZone(Vector2 origin, IMap newMap);
        IMap ProcessTurn();
    }
}
