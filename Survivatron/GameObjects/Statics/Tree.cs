using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Survivatron.GameObjects.Statics
{
    class Tree : Static
    {
        private static char[] repCatalogue = { (char)5, (char)6, (char)255 };
        private static Random ranGen = new Random();
        private static GOID nextID = new GOID(-1,0);

        public Tree()
        {
            // By convention, static objects have negative IDs
            base.Representation = (char)repCatalogue[ranGen.Next(3)];
            base.ID = Tree.nextID;
            Tree.nextID.subID++;
            base.FType = GameObjectType.BASIC;
        }
    }
}
