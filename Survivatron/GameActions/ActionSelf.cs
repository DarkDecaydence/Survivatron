using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survivatron.GameObjects;

namespace Survivatron.GameActions
{
    public class ActionSelf : IGameAction
    {
        private int[] _args;
        private Func<int[], int> _command;

        public virtual void Execute()
        { _command(_args); }

        public ActionSelf(Func<int[], int> command, int[] args)
        {
            _args = args;
            _command = command; }
    }
}
