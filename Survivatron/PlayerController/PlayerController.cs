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

namespace Survivatron
{
    public class PlayerController
    {
        public PlayerCharacter Character { get; set; }
        public Frame GameFrame { get; set; }
        private Keys[] charControls, camControls;
        private int playerNumber;

        public PlayerController(int player, Frame gameFrame)
        {
            Character = new PlayerCharacter(player);
            playerNumber = player;
            GameFrame = gameFrame;
            
            // Setup player character controls.
            switch (playerNumber)
            {
                case 1: charControls = new Keys[] { Keys.W, Keys.A, Keys.S, Keys.D, Keys.X }; break;
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
            /*
             * The width and height of the gameframe is defined in tiles, not pixels, hench map.columns.length is used to determain deadzones.
             */
            if (Keyboard.GetState().IsKeyDown(charControls[0]))
            {
                var charX = (int)Character.Position.X;
                var charY = Character.Position.Y; var gameH = GameFrame.dimensions.Height; var mapH = GameFrame.worldMap.columns[charX].rows.Length;
                bool shouldCamMove = FrameHandler.IsValid((int)charY, gameH, mapH);
                var prevPos = Character.Position;

                Interact(new Vector2(0, -1));

                bool charMoved = Character.Position != prevPos;
                if (shouldCamMove && charMoved) { GameFrame.MoveFrame(0, -1); }
            }
            if (Keyboard.GetState().IsKeyDown(charControls[1]))
            {
                var charX = Character.Position.X; var gameW = GameFrame.dimensions.Width; var mapW = GameFrame.worldMap.columns.Length;
                bool shouldCamMove = FrameHandler.IsValid((int)charX, gameW, mapW);
                var prevPos = Character.Position;

                Interact(new Vector2(-1, 0));

                bool charMoved = Character.Position != prevPos;
                if (shouldCamMove && charMoved) { GameFrame.MoveFrame(-1, 0); }
            }
            if (Keyboard.GetState().IsKeyDown(charControls[2]))
            {
                var charX = (int)Character.Position.X;
                var charY = Character.Position.Y; var gameH = GameFrame.dimensions.Height; var mapH = GameFrame.worldMap.columns[charX].rows.Length;
                bool shouldCamMove = FrameHandler.IsValid((int)charY, gameH, mapH);
                var prevPos = Character.Position;

                Interact(new Vector2(0, 1));
                bool charMoved = Character.Position != prevPos;
                if (shouldCamMove && charMoved) { GameFrame.MoveFrame(0, 1); }
            }
            if (Keyboard.GetState().IsKeyDown(charControls[3]))
            {
                var charX = Character.Position.X; var gameW = GameFrame.dimensions.Width; var mapW = GameFrame.worldMap.columns.Length;
                bool shouldCamMove = FrameHandler.IsValid((int)charX, gameW, mapW);
                var prevPos = Character.Position;

                Interact(new Vector2(1, 0));
                bool charMoved = Character.Position != prevPos;
                if (shouldCamMove && charMoved) { GameFrame.MoveFrame(1, 0); }
            }
            if (Keyboard.GetState().IsKeyDown(charControls[4]))
            { //Character.Move('x'); 
            }

            /*
            if (Keyboard.GetState().IsKeyDown(camControls[0]))
            { GameFrame.MoveFrame(0, -1); }
            if (Keyboard.GetState().IsKeyDown(camControls[1]))
            { GameFrame.MoveFrame(-1, 0); }
            if (Keyboard.GetState().IsKeyDown(camControls[2]))
            { GameFrame.MoveFrame(0, 1); }
            if (Keyboard.GetState().IsKeyDown(camControls[3]))
            { GameFrame.MoveFrame(1, 0); }
             */
        }

        private void Interact(Vector2 direction)
        {
            Func<int[], int> command = iArr => {
                MapController.MoveObject(Character.ID, direction);
                return 1; };
            Character.NextAction = new GameAction(command);
            Character.ReadyUp(new int[] {(int)direction.X, (int)direction.Y});
        }

        private void Wait()
        {
            GameAction action = new GameAction();
            Character.NextAction = action;
        }
    }
}
