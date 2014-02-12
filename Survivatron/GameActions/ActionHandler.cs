﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Survivatron.GameActions
{
    public class ActionHandler
    {
        public IGameAction CreateWait()
        {
            Func<int> waitCommand = new Func<int>(() => { return 1; });
            ActionSelf waitAction = new ActionSelf(waitCommand);
            return (IGameAction)waitAction;
        }

        public IGameAction CreateMove(Vector2 direction)
        {
            Func<int[], int> moveCommand = new Func<int[], int>(args => { return 1; });
            ActionTarget moveAction = new ActionTarget(moveCommand, new int[] { (int)direction.X, (int)direction.Y });
            return (IGameAction)moveAction;
        }
    }
}
