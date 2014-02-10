using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survivatron.GameActions;
using Survivatron.GameEvents;
using Survivatron.MapSpecs;

namespace Survivatron.GameObjects.Dynamics
{
    public class PlayerCharacter : Dynamic
    {
        public override GameAction NextAction { get; set; }
        public override int ActionPointMax { get; set; }
        private int actionPoints;

        public PlayerCharacter(int owner)
        {
            MapController.CallOut += base.OnCall;
            base.Representation = (char)16;
            base.ID = new GOID(1, owner);
            base.FType = GameObjectType.PLAYER;
            base.Solid = true;
            ActionPointMax = 100;
            actionPoints = ActionPointMax;
        }
    }
}
