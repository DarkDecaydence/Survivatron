using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Survivatron.GameEvents
{
    public class MoveEventArgs : EventArgs
    {
        public Vector2 MoveVector { get; set; }
        public int PlayerNumber { get; set; }
        public MoveEventArgs(Vector2 moveVector, Int32 playerNumber) { this.MoveVector = moveVector; this.PlayerNumber = playerNumber; }
    }
}
