using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survivatron.GameEvents
{
    public class CallEventArgs : EventArgs
    {
        public GOID ID { get; private set; }

        public CallEventArgs(GOID id)
        { ID = id; }
    }
}
