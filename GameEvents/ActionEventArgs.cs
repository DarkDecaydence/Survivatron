using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survivatron.GameEvents
{
    public class ActionEventArgs : EventArgs
    {
        public int[] args { get; private set; }

        public ActionEventArgs(int[] args)
        { this.args = args; }
    }
}
