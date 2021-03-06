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

        Texture2D player1, player2;
        Vector2 playerPos1, playerPos2;
        Rectangle playerRect1, playerRect2;
        float playerSpeed;

        Texture2D ball;
        Vector2 ballPos;
        Rectangle ballRect;
        float ballYMultiplier, ballSpeed;
        bool ballUp, ballRight;

        SpriteFont scorefont;
        Vector2 scr1Pos, scr2Pos;
        int player1Scr, player2Scr;

        double gameTimer;
        double startTime;

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
            //reset game
            gameTimer = 0f;
            startTime = .5f;
            //text pos
            scr1Pos = GameMaster.TextPos(3, 1, graphics);
            scr2Pos = GameMaster.TextPos(5, 1, graphics);
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
            //set rectangle for collision
            playerRect1 = new Rectangle();
            playerRect2 = new Rectangle();
            ballRect = new Rectangle();
            //ball rand multiplier
            ballYMultiplier = (float)GameMaster.RandMult(2);
            //reset score
            player1Scr = 0;
            player2Scr = 0;

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
            //load content
            player1 = Content.Load<Texture2D>("player");
            player2 = Content.Load<Texture2D>("player");
            ball = Content.Load<Texture2D>("ball");
            scorefont = Content.Load<SpriteFont>("testFont");

            //fix center position of object
            playerPos1 = playerPos1 - new Vector2(player1.Width/2, player1.Height/2);
            playerPos2 = playerPos2 - new Vector2(player2.Width / 2, player2.Height / 2);
            ballPos = ballPos - new Vector2(ball.Width / 2, ball.Height / 2);

            //assign rect size for collision
            playerRect1 = new Rectangle((int)playerPos1.X, (int)playerPos1.Y, player1.Width, player1.Height);
            playerRect2 = new Rectangle((int)playerPos2.X, (int)playerPos2.Y, player2.Width, player2.Height);
            ballRect = new Rectangle((int)ballPos.X, (int)ballPos.Y, ball.Width, ball.Height);
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
            
            //game timer
            gameTimer += gameTime.ElapsedGameTime.TotalSeconds;
            //sync rect pos with obj pos
            //p1
            playerRect1.Location = playerPos1.ToPoint();
            //p2
            playerRect2.Location = playerPos2.ToPoint();
            //ball
            ballRect.Location = ballPos.ToPoint();

            //game control
            var kstate = Keyboard.GetState();

            //restart the game
            if (kstate.IsKeyDown(Keys.R))
            {
                player1Scr = 0;
                player2Scr = 0;
                //GameMaster.GameReset(graphics, "Ball", ballPos, ballUp, ballRight, ballYMultiplier, gameTimer);
                GameReset();
            }

            //player 1 control
            if (kstate.IsKeyDown(Keys.W)) playerPos1.Y -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (kstate.IsKeyDown(Keys.S)) playerPos1.Y += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //player 2 control
            if (kstate.IsKeyDown(Keys.Up)) playerPos2.Y -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (kstate.IsKeyDown(Keys.Down)) playerPos2.Y += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            //player area collision
            //player 1 bounds
            if (playerPos1.Y > graphics.PreferredBackBufferHeight - player1.Height)
                playerPos1.Y = graphics.PreferredBackBufferHeight - player1.Height;
            if (playerPos1.Y < 0)
                playerPos1.Y = 0;
            //player 2 bounds
            if (playerPos2.Y > graphics.PreferredBackBufferHeight - player2.Height)
                playerPos2.Y = graphics.PreferredBackBufferHeight - player2.Height;
            if (playerPos2.Y < 0)
                playerPos2.Y = 0;

            //ball logic
            //ball movement
            if(gameTimer > startTime)
            {
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
            }

            //ball y boundaries
            if (ballPos.Y > graphics.PreferredBackBufferHeight - ball.Height / 2) ballUp = true;
            if (ballPos.Y < ball.Height / 2) ballUp = false;

            //ball to player 1 collision
            if (playerRect1.Intersects(ballRect)) ballRight = true;
            else if (playerRect2.Intersects(ballRect)) ballRight = false;

            //reset game flag
            if (ballPos.X > graphics.PreferredBackBufferWidth - ball.Width)
            {
                //GameMaster.GameReset(graphics, "Ball", ballPos, ballUp, ballRight, ballYMultiplier, gameTimer);
                GameReset();
                player1Scr++;
            }
            if (ballPos.X < 0)
            {
                //GameMaster.GameReset(graphics, "Ball", ballPos, ballUp, ballRight, ballYMultiplier, gameTimer);
                GameReset();
                player2Scr++;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            //begin drawing
            spriteBatch.Begin();
            //drawing object
            spriteBatch.Draw(player1, playerPos1, playerRect1, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(player2, playerPos2, playerRect2, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(ball, ballPos, ballRect, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            //drawing text
            spriteBatch.DrawString(scorefont, player1Scr.ToString(), scr1Pos, Color.White);
            spriteBatch.DrawString(scorefont, player2Scr.ToString(), scr2Pos, Color.White);
            //end drawing
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void GameReset()
        {
            ballPos = GameMaster.ObjPos("Ball", graphics);
            ballUp = GameMaster.RandBool();
            ballRight = GameMaster.RandBool();
            ballYMultiplier = (float)GameMaster.RandMult(2);
            gameTimer = 0f;
        }
    }
}
