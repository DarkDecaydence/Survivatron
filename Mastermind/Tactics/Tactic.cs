using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survivatron.GameActions;

namespace Survivatron.Mastermind.Tactics
{
    public enum TacticType {
        PEACEFUL, RESERVED, HOSTILE
    }

    public interface Tactic
    {
        GameAction CalculateNextAction();
    }
}
