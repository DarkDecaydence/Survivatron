using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Survivatron.GameObjects.Statics
{
    public class Static : GameObject
    {
        public override char Representation { get; set; }
        public override GOID ID { get; set; }
        public override GameObjectType FType { get; set; }
        public override Vector2 Position { get; set; }
        public override bool Solid { get; set; }
    }
}
