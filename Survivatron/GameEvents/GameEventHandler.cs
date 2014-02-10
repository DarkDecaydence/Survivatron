using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survivatron.GameObjects;
using Survivatron.GameObjects.Dynamics;

namespace Survivatron.GameEvents
{
    public delegate void MoveEventHandler(GameObject sender, MoveEventArgs e);
    public delegate void ReadyEventHandler(Dynamic sender, ActionEventArgs e);
    public delegate void CallEventHandler(CallEventArgs e);
}
