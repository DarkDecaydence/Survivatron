using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Survivatron.GameActions;

namespace Survivatron.GameObjects.Statics
{
    public abstract class Static : GameObject
    {
        public override IGameAction NextAction { get; set; }
    }
}
