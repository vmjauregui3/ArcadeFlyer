using Microsoft.Xna.Framework;
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

        private Player player; 

        private Enemy enemy;
        
        //propfull
        //ctor
        private int screenWidth = 1600;
        public int ScreenWidth
        {
            get { return screenWidth; }
            set { screenWidth = value; }
        }

        private int screenHeight = 900;
        public int ScreenHeight
        {
            get { return screenHeight; }
            set { screenHeight = value; }
        }
        

        // Initalized the game
        public ArcadeFlyerGame()
        {
            // Get the graphics
            graphics = new GraphicsDeviceManager(this);

            // Set the height and width
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            // Set up the directory containing the assets
            Content.RootDirectory = "Content";

            // Make mouse visible
            IsMouseVisible = true;

            Vector2 position = new Vector2(100.0f, 100.0f);
            player = new Player(this, position);

            enemy = new Enemy(this, new Vector2(screenWidth-200, 0));
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
            enemy.Update(gameTime);
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
            enemy.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
