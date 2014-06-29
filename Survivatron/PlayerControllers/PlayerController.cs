using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Survivatron.GameActions;
using Survivatron.GameObjects;
using Survivatron.GameObjects.Dynamics;
using Survivatron.MapSpecs;
using Survivatron.ViewFrames;


namespace Survivatron.PlayerControllers
{
    public class PlayerController : IPlayerController
    {
        public PlayerCharacter Character { get; set; }
        public ViewFrame GameFrame { get; set; }
        private Keys[] charControls, camControls;
        private int playerNumber;

        public bool UpdateGameObject(GOID goid, GameObject newObject)
        { return false; }

        public bool AttachAction(ref GameObject gameObject, IGameAction gameAction)
        { return false; }

        public PlayerController(int player, ViewFrame gameFrame)
        {
            Character = new PlayerCharacter(player);
            playerNumber = player;
            GameFrame = gameFrame;
            
            // Setup player character controls.
            switch (playerNumber)
            {
                case 1: charControls = new Keys[] { Keys.W, Keys.A, Keys.S, Keys.D, Keys.C }; break;
                case 2: charControls = new Keys[] { Keys.NumPad8, Keys.NumPad4, Keys.NumPad2, Keys.NumPad6, Keys.NumPad5 }; break;
                default: charControls = new Keys[5]; break;
            }

            // Setup player camera controls.
            switch (playerNumber)
            {
                case 1: camControls = new Keys[] { Keys.I, Keys.J, Keys.K, Keys.L }; break;
                default: camControls = new Keys[4]; break;
            }
        }

        public void Update()
        {
            Vector2 camVector = new Vector2();
            Vector2 moveVector = new Vector2();
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

            foreach (Keys k in pressedKeys)
            {
                switch (k)
                {
                    case Keys.C:
                        GameFrame.CenterFrame(Character.Position); break;
                    case Keys.W:
                        camVector = Vector2.Add(camVector, new Vector2(0, -1)); break;
                    case Keys.A:
                        camVector = Vector2.Add(camVector, new Vector2(-1, 0)); break;
                    case Keys.S:
                        camVector = Vector2.Add(camVector, new Vector2(0, 1)); break;
                    case Keys.D:
                        camVector = Vector2.Add(camVector, new Vector2(1, 0)); break;

                    case Keys.Up:
                        moveVector = Vector2.Add(moveVector, new Vector2(0, -1)); break;
                    case Keys.Left:
                        moveVector = Vector2.Add(moveVector, new Vector2(-1, 0)); break;
                    case Keys.Down:
                        moveVector = Vector2.Add(moveVector, new Vector2(0, 1)); break;
                    case Keys.Right:
                        moveVector = Vector2.Add(moveVector, new Vector2(1, 0)); break;
                }
            }

            camVector = Vector2.Clamp(camVector, new Vector2(-1, -1), new Vector2(1, 1));

            Character.NextAction = ActionHandler.CreateMove(Character, moveVector);
            if (!camVector.Equals(new Vector2(0, 0)))
            { GameFrame.MoveFrame(camVector); }
        }
    }
}
