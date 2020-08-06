﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mini_pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        Texture2D player1;
        Vector2 playerPos1;

        Texture2D player2;
        Vector2 playerPos2;
        
        Texture2D ball;
        Vector2 ballPos;
        float ballYMultiplier;
        bool ballUp;
        bool ballRight;

        float playerSpeed;
        float ballSpeed;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            //player pos
            playerPos1 = GameMaster.ObjPos("Player1", graphics);
            playerPos2 = GameMaster.ObjPos("Player2", graphics);
            //player speed
            playerSpeed = 500f;
            //ball pos
            ballPos = GameMaster.ObjPos("Ball", graphics);
            ballUp = GameMaster.RandBool();
            ballRight = GameMaster.RandBool();
            //ball speed
            ballSpeed = 500f;

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

            // TODO: use this.Content to load your game content here
            player1 = Content.Load<Texture2D>("player");
            player2 = Content.Load<Texture2D>("player");
            ball = Content.Load<Texture2D>("ball");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            //player control
            var kstate = Keyboard.GetState();
            //player 1 control
            if (kstate.IsKeyDown(Keys.W)) playerPos1.Y -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (kstate.IsKeyDown(Keys.S)) playerPos1.Y += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //player 2 control
            if (kstate.IsKeyDown(Keys.Up)) playerPos2.Y -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (kstate.IsKeyDown(Keys.Down)) playerPos2.Y += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //player area collision
            //player 1 bounds
            if (playerPos1.Y > graphics.PreferredBackBufferHeight - player1.Height / 2)
                playerPos1.Y = graphics.PreferredBackBufferHeight - player1.Height / 2;
            if (playerPos1.Y < player1.Height / 2)
                playerPos1.Y = player1.Height / 2;
            //player 2 bounds
            if (playerPos2.Y > graphics.PreferredBackBufferHeight - player2.Height / 2)
                playerPos2.Y = graphics.PreferredBackBufferHeight - player2.Height / 2;
            if (playerPos2.Y < player2.Height / 2)
                playerPos2.Y = player2.Height / 2;

            //ball logic
            ballYMultiplier = 2f;
            
            //ball y movement
            if (ballUp == true)
                ballPos.Y -= ballYMultiplier * ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                ballPos.Y += ballYMultiplier * ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //ball x movement
            if (ballRight == true)
                ballPos.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                ballPos.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //ball y boundaries
            if (ballPos.Y > graphics.PreferredBackBufferHeight - ball.Height / 2) ballUp = true;
            if (ballPos.Y < ball.Height / 2) ballUp = false;

            //ball reset logic
            if (ballPos.X > graphics.PreferredBackBufferWidth - ball.Width / 2)
            {
                ballPos = GameMaster.ObjPos("Ball", graphics);
                ballUp = GameMaster.RandBool();
                ballRight = GameMaster.RandBool();
            }
            if (ballPos.X < ball.Width / 2)
            {
                ballPos = GameMaster.ObjPos("Ball", graphics);
                ballUp = GameMaster.RandBool();
                ballRight = GameMaster.RandBool();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(player1, playerPos1, null, Color.White, 0f, new Vector2(player1.Width / 2, player1.Height / 2), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(player2, playerPos2, null, Color.White, 0f, new Vector2(player2.Width / 2, player2.Height / 2), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(ball, ballPos, null, Color.White, 0f, new Vector2(ball.Width / 2, ball.Height / 2), 1f, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
