using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Survivatron.GameActions;
using Survivatron.GameEvents;
using Survivatron.MapSpecs;

namespace Survivatron.GameObjects.Dynamics
{
    public abstract class Dynamic : GameObject
    {
        public abstract int ActionPointMax { get; set; }
    }
}
