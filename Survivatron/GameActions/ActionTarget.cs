using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survivatron.GameObjects;

namespace Survivatron.GameActions
{
    public struct ActionTarget : IGameAction
    {
        private int[] _args;
        private Func<int[], GameObject, int> _command;

        public void Execute()
        { _command(_args, null); }

        public ActionTarget(Func<int[], GameObject, int> command, int[] args)
        {
            _args = args;
            _command = command;
        }
    }
}
