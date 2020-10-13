﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    // The Game itself
    class ArcadeFlyerGame : Game
    {
        // Graphics Manager
        private GraphicsDeviceManager graphics;

        // Sprite Drawer
        private SpriteBatch spriteBatch;

        // Player Character Graphic
        //private Texture2D playerImage;
        private Player player; //test
        
        // Initalized the game
        public ArcadeFlyerGame()
        {
            // Get the graphics
            graphics = new GraphicsDeviceManager(this);

            // Set the height and width
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();

            // Set up the directory containing the assets
            Content.RootDirectory = "Content";

            // Make mouse visible
            IsMouseVisible = true;

            player = new Player(this, new Vector2(10.0f, 10.0f)); //test
        }

        // Initialize
        protected override void Initialize()
        {
            base.Initialize();
        }

        // Load the content for the game, called automatically on start
        protected override void LoadContent()
        {
            //playerImage = Content.Load<Texture2D>("MainChar"); //test(//)
            // Create the sprite batch
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        // Called every frame
        protected override void Update(GameTime gameTime)
        {   
            // Update base game
            base.Update(gameTime);
            player.Update(gameTime);
        }

        // Draw everything in the game
        protected override void Draw(GameTime gameTime)
        {
            // First clear the screen
            GraphicsDevice.Clear(Color.LightSteelBlue);

            spriteBatch.Begin();
            /*
            //Drawing will happen here
            Rectangle playerDestinationRect = new Rectangle(100,100,playerImage.Width,playerImage.Height);
            spriteBatch.Draw(playerImage, playerDestinationRect, Color.LightGreen); 
            //Color.White adds a clear filter to show the image. Color.(other) tints it that color
            */ //test (/*)
            player.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
