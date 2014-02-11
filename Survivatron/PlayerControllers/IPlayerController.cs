using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survivatron.GameObjects;
using Survivatron.GameActions;

namespace Survivatron.PlayerControllers
{
    interface IPlayerController
    {
        bool UpdateGameObject(GOID goid, GameObject newObject);
        bool AttachAction(ref GameObject gameObject, GameAction gameAction);
    }
}
