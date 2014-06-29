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
        public static IGameAction CreateWait()
        {
            Func<int[], int> waitCommand = new Func<int[], int>(args => { return 1; });

            ActionSelf waitAction = new ActionSelf(waitCommand, null);
            return (IGameAction)waitAction;
        }

        public static IGameAction CreateMove(GameObject target, Vector2 direction)
        {
            Func<int[], int> moveCommand =
                new Func<int[], int>(args =>
                {
                    MapController mc = MapController.GetInstance();
                    GameObject gObj = mc.GetGameObject(new GOID(args[0], args[1]));
                    gObj.Position = Vector2.Add(gObj.Position, new Vector2(args[2], args[3]));
                    return 1;
                });

            ActionSelf moveAction = new ActionSelf(moveCommand, new int[] { target.ID.ID, target.ID.subID, (int)direction.X, (int)direction.Y });
            return (IGameAction)moveAction;
        }
    }
}
