using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Survivatron.GameActions
{
    public class ActionHandler
    {
        public static GameAction Interact()
        {
            /*
            return new GameAction
            {
                Execute = new Func<string[],int>()
            };
             * 
             */
            return null;
        }

        public static int InteractAction(String[] args)
        {
            /*int id, tx, ty;
            bool validID = Int32.TryParse(args[0], out id);
            bool validTVector = Int32.TryParse(args[1], out ty) && Int32.TryParse(args[2], out tx);
            
            if( && ) {
                Vector2 vT = new Vector2(tx,ty);
                if (MapSpecs.MapController.Current.GetRow(
                MapSpecs.MapController.Current.MoveObject(id, vT);
                return 1;
            }*/
            return -1;
        }
    }
}
