using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survivatron.GameObjects;

namespace Survivatron.GameActions
{
    public class ActionSelf : IGameAction
    {
        private Func<int> _command;

        public virtual void Execute()
        { _command(); }

        public ActionSelf(Func<int> command)
        { _command = command; }
    }
}
