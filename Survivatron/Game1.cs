#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Survivatron.MapSpecs;
using Survivatron.GameObjects;
using Survivatron.GameObjects.Dynamics;
using Survivatron.Mastermind;
using Survivatron.PlayerControllers;
using Survivatron.ViewFrames;
#endregion

namespace Survivatron
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D[] tilesets = new Texture2D[3];
        private PlayerController[] players;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1152;
            graphics.PreferredBackBufferHeight = 864;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            /* Map creation and initialization: */
            Map gameMap = new Map((int)(14400 / 18), (int)(10800 / 18));
            MapController mc = MapController.Construct(gameMap);

            ViewFrame player1ViewFrame = new ViewFrame();
            PlayerController player1 = new PlayerController(1, player1ViewFrame);
            players = new PlayerController[] { player1 };

            // Creating dynamics:
            PlayerCharacter pc = new PlayerCharacter(1);
            players[0].Character = pc;

            // Adding statics:
            //mc.AddTrees(gameMap, 70);

            // Adding dynamics:
            Dynamic dynamic = (Dynamic)players[0].Character;
            mc.AddDynamic(new Vector2(5, 4), ref dynamic);
            players[0].Character = (PlayerCharacter)dynamic;
            /* End of map creation and initialization. */

            /* GameFrame calculations: */
            int frameX, frameY;
            double frameW, frameH;
            frameW = Math.Round((double)(graphics.PreferredBackBufferWidth - 128) / 18.0);
            frameH = Math.Round((double)(graphics.PreferredBackBufferHeight - 96) / 18.0);
            frameX = (int)players[0].Character.Position.X - (int)Math.Round(frameW / 2);
            frameY = (int)players[0].Character.Position.Y - (int)Math.Round(frameH / 2);
            player1ViewFrame.ChangeFrame(new Rectangle(frameX, frameY, (int)frameW, (int)frameH));
            /* End of calculations. */

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tilesets[0] = Content.Load<Texture2D>("ironhand");
            tilesets[1] = Content.Load<Texture2D>("dwarves");
            tilesets[2] = Content.Load<Texture2D>("critters");

            TileHandler.Construct(tilesets, 18);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            players[0].Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            players[0].GameFrame.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
