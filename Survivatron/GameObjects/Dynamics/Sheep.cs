using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survivatron.GameActions;
using Survivatron.GameEvents;
using Survivatron.MapSpecs;
using Survivatron.Mastermind.Tactics;


namespace Survivatron.GameObjects.Dynamics
{
    public class Sheep : DynamicAI
    {
        public override IGameAction NextAction { get; set; }
        public override int ActionPointMax { get; set; }
        public override Tactic AITactic { get; set; }
        private int actionPoints;

        public Sheep(int subID)
        {
            MapController.CallOut += base.OnCall;
            base.Representation = (char)211;
            base.ID = new GOID(2, subID);
            base.FType = GameObjectType.CRITTER;
            base.Solid = true;
            ActionPointMax = 100;
            actionPoints = ActionPointMax;
            AITactic = null;
        }
    }
}
