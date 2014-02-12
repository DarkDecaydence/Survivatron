using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Survivatron.GameObjects;
using Survivatron.MapSpecs;

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
            Func<int[], GameObject, int> moveCommand =
                new Func<int[], GameObject, int>((args, gObj) => {
                    gObj.Position = Vector2.Add(gObj.Position, new Vector2(args[0], args[1]));
                    MapController mc = MapController.GetInstance();
                    mc.SetGameObject(gObj);
                    return 1;
                });
            ActionTarget moveAction = new ActionTarget(moveCommand, new int[] { (int)direction.X, (int)direction.Y });
            return (IGameAction)moveAction;
        }
    }
}
