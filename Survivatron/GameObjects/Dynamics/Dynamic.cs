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
        public override char Representation { get; set; }
        public override GOID ID { get; set; }
        public override GameObjectType FType { get; set; }
        public override Vector2 Position { get; set; }
        public override bool Solid { get; set; }

        public static event ReadyEventHandler Ready;
        public abstract IGameAction NextAction { get; set; }
        public abstract int ActionPointMax { get; set; }
        
        public void ReadyUp(int[] args)
        {
            if (Ready != null)
            { Ready(this, new ActionEventArgs(args)); }
        }

        public void OnCall(CallEventArgs e)
        {
            //if (this.id.equals(e.id))
            //    mapcontroller.callreturn(this);
        }
    }
}
