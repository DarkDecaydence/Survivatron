using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survivatron.MapSpecs;

namespace Survivatron.GameActions
{
    public class GameAction
    {
        public Func<int[], int> Execute { get; private set; }

        public GameAction()
        { Execute = sArr => { return (int)ActionEnum.WAIT; }; }

        public GameAction(Func<int[], int> command)
        { Execute = command; }
    }
}
