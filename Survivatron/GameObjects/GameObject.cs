using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Survivatron.GameActions;

namespace Survivatron.GameObjects
{
    public abstract class GameObject
    {
        public virtual GOID ID { get; set; }
        public virtual Vector2 Position { get; set; }
        public virtual char Representation { get; set; }
        public virtual IGameAction NextAction { get; set; }

        public virtual GameObjectMass Mass { get; set; }
        public virtual GameObjectType FType { get; set; }

        public override bool Equals(object obj)
        {
            if ((GameObject)obj == null) return false;
            return ID.Equals(((GameObject)obj).ID);
        }
    }
}
