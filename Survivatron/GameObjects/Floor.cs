using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Survivatron.GameObjects.Statics
{
    class Floor : GameObject
    {
        private static char[] repCatalogue = { (char)151, (char)152, (char)153, (char)154 };
        private static Random ranGen = new Random();
        public override char Representation { get; set; }
        public override GOID ID { get; set; }
        public override GameObjectType FType { get; set; }
        public override Vector2 Position { get; set; }
        public override bool Solid { get; set; }

        public Floor(int x, int y)
        {
            int i = Floor.ranGen.Next(4);
            Representation = Floor.repCatalogue[i];
            ID = new GOID(-1,-1);
            FType = GameObjectType.BASIC;
            Solid = false;
            Position = new Vector2(x, y);
        }
    }
}
