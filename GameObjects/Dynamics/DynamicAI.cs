using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survivatron.Mastermind.Tactics;

namespace Survivatron.GameObjects.Dynamics
{
    public abstract class DynamicAI : Dynamic
    {
        public abstract Tactic AITactic { get; set; }
    }
}
