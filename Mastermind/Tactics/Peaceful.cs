using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survivatron.GameActions;

namespace Survivatron.Mastermind.Tactics
{
    public class Peaceful : Tactic
    {
        private TacticType type = TacticType.PEACEFUL;
        private Random rand = new Random();

        public GameAction CalculateNextAction()
        {
            return new GameAction();
        }
    }
}
