using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Survivatron.GameObjects
{
    public abstract class GameObject
    {
        public abstract char Representation { get; set; }
        public abstract GOID ID { get; set; }
        public abstract GameObjectType FType { get; set; }
        public abstract Vector2 Position { get; set; }
        public abstract bool Solid { get; set; }

        public override bool Equals(object obj)
        {
            if ((GameObject)obj == null) return false;
            return ID.Equals(((GameObject)obj).ID);
        }
    }
}
