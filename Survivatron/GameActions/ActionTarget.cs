using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survivatron.GameActions
{
    public struct ActionTarget : IGameAction
    {
        private int[] _args = new int[1];
        private Func<int[], int> _command;

        public virtual void Execute()
        { _command(_args); }

        public ActionTarget(Func<int[], int> command, int[] args)
        {
            _args = args;
            _command = command;
        }
    }
}
