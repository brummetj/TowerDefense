using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TeamVGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Game Logic.
        Boolean done = false;
        float elapsed;
        int frames = 0;
        float delay = 200f;

        //Font
        SpriteFont Font1;

        //Delay to populate more then one.
        float spawnTimer;
        float spawnDouble;
        float spawnDelay = 3;
        float multipleDelay = 10;
        float spawnCounter = 0f;
        bool populate = false;

        float frameElapsed;
        int DamageCounter;
    
        //Textures for background and mainframe.
        Texture2D Background;
        Rectangle mainFrame;

        //Texture for Grass.
        Texture2D Grass;

        //Tower
        Tower Tower;

        //WeakEnemy List.
        List<weakEnemy> weakEnemys = new List<weakEnemy>();

        //Random.
        Random rnd = new Random();

        //Pausing States
        private bool paused = false;
        private bool pauseKeyDown = false;
        private bool pausedForGuide = false;

        //Function to create weaks.
        void CreateWeak()
        {
            // This is the same code as I used in Initialize().
            // Duplicate code is extremely bad practice. So you should now modify 
            // Initialize() so that it calls this method instead.
            int screenHeight = GraphicsDevice.Viewport.Height;
            float yPosition = Shared.Random.Next(125, screenHeight);      
           
            weakEnemys.Add(new weakEnemy(new Vector2(0, yPosition)));
        }

        //Function to create multiple weaks
        void createMultipleWeak()
        {
            int screenHeight = GraphicsDevice.Viewport.Height;
            float yPosition = Shared.Random.Next(125, screenHeight);  
                      
            for (int i = 0; i < 2; i++)
                {

                    weakEnemys.Add(new weakEnemy(new Vector2(0, yPosition)));
                }
        }
        //Function to create more multiple weaks.
        void createMoreMultipleWeak()
        {
            int screenHeight = GraphicsDevice.Viewport.Height;
            float yPosition = Shared.Random.Next(125, screenHeight);
           
            for (int i = 0; i < 4; i++)
                {
                    weakEnemys.Add(new weakEnemy(new Vector2(0, yPosition)));
                }
                spawnCounter = 0;
        }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //  changing the back buffer size changes the window size (when in windowed mode)
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 400;
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
            this.IsMouseVisible = true;
            for (int i = 0; i < 1; i++)
            {
                int screenHeight = GraphicsDevice.Viewport.Height;
                float yPosition = Shared.Random.Next(115, screenHeight);
                weakEnemys.Add(new weakEnemy(new Vector2(0, yPosition)));
            }
   
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

            //--Backgrounds--
            Background = Content.Load<Texture2D>("bg");

            Grass = Content.Load<Texture2D>("Grass");

            //--Castle
            Tower = new Tower(Content.Load<Texture2D>("Castle"), new Vector2(950, 105), new Vector2(200, 200),
                graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            Font1 = Content.Load<SpriteFont>("Courier New");

            //--weakEnemies--

            weakEnemy.texture = Content.Load<Texture2D>("WalkingWeak");

            //--MainFramie--
            mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
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
        /// <param name="gameTime">Provides a snapshot of
        /// timing values.</param>
        /// 
        private void BeginPause(bool UserInitiated)
        {
            paused = true;
            pausedForGuide = !UserInitiated;
            //TODO: Pause audio playback
            //TODO: Pause controller vibration
        }
        private void EndPause()
        {
            //TODO: Resume audio
            //TODO: Resume controller vibration
            pausedForGuide = false;
            paused = false;
        }
        private void checkPauseKey(KeyboardState keyboardState, GamePadState gamePadState)
        {
            bool pauseKeyDownThisFrame = (keyboardState.IsKeyDown(Keys.P) ||
                (gamePadState.Buttons.Y == ButtonState.Pressed));
            // If key was not down before, but is down now, we toggle the
            // pause setting
            if (!pauseKeyDown && pauseKeyDownThisFrame)
            {
                if (!paused)
                    BeginPause(true);
                else
                    EndPause();
            }
            pauseKeyDown = pauseKeyDownThisFrame;
        }

        //Game Logic.
        private void Simulate(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Time Delays.
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;           
            spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            spawnDouble += (float)gameTime.ElapsedGameTime.TotalSeconds;
            frameElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //time for ball frames to adjust.

            
            //updating position. 
            foreach (weakEnemy weak in weakEnemys)
            {
                 weak.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            
            //Changing Frame Rates for enemy sprites.
            if (elapsed >= delay) //if time of game is greater then delaty do if statement
            {
                if (frames >= 3) //if frames are greater then 3 go back to the first.
                {
                    frames = 0;
                }
                else  //otherwise increment frames
                {
                    frames++;
                }
                foreach (weakEnemy weak in weakEnemys)
                {
                    weak.changeFrame(frames);
                }
                elapsed = 0; //set time back to zero.
            }
            

            //Delay Timers for Weak Spawns.
            if (spawnTimer >= spawnDelay)
            {            
                spawnTimer -= spawnDelay; // subtract "used" time
                CreateWeak();
            }
            if(spawnDouble >= multipleDelay)
            {
                spawnDouble -= multipleDelay;
                createMultipleWeak();
            }

            //Moving Weak enemy.
            var mouseState = Mouse.GetState();
            for (int i = weakEnemys.Count - 1; i >= 0; i--)
            {
                weakEnemys[i].move(Tower);

                if (mouseState.LeftButton == ButtonState.Pressed)
                {           
                    weakEnemys[i].E_Attacked(); //Trying to figure out damage. Need help.
                   
                }
            }

            //WeakEnemy dealing damage to tower.
            for (int i = 0; i < weakEnemys.Count; i++)
            {
                if(weakEnemys[i].attackState == true && gameTime.ElapsedGameTime.Milliseconds == 3)
                { 
                        Tower.health -= 0.01f;
                    if (Tower.health <= 0)
                    {
                        done = true;
                        Tower.health = 0;
                    }
                }
                
            }

            //Damage Logic
          
            //if (mouseState.LeftButton == ButtonState.Pressed)
            //{
            //    foreach(weakEnemy weak in weakEnemys)
            //    {
            //        weak.health -= 50;
            //        if(weak.health == 0)
            //        {
            //            weak.position = new Vector2(1000, 0);
            //        }
            //    }
            //}


        }

        // Pausing Logic
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Check to see if the user has paused or unpaused
            KeyboardState keyState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            checkPauseKey(keyState,gamePadState);

            // If the user hasn't paused, Update normally
            if (!paused)
            {
                Simulate(gameTime);
            }
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

            //Background.
            spriteBatch.Draw(Background, mainFrame, Color.White);
            spriteBatch.Draw(Grass, new Vector2(0, 175), Color.White);
        
            //Castle
            Tower.Draw(spriteBatch);


            //Drawing health.
            spriteBatch.DrawString(Font1, "Health: " + Tower.health, new Vector2(5, 10), Color.Azure);

            //P to pause
            spriteBatch.DrawString(Font1, "P to pause", new Vector2(800, 10), Color.Azure);



            //When game has finished.
            if (done == true)
            {
                spriteBatch.DrawString(Font1, "Score: ", new Vector2(5, 10), Color.Azure);
            }

            //Drawing Weak Sprite
            foreach (weakEnemy weak in weakEnemys)
            {
                    weak.Draw(spriteBatch);
               
            }

            spriteBatch.End();

            if (done == false)
            {
                base.Draw(gameTime);
            }
        }
    }
}
