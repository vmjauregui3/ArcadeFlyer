using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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

        private List<Projectile> projectiles;
        private Texture2D playerProjectileSprite;
        private Texture2D enemyProjectileSprite;
        
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

            enemy = new Enemy(this, new Vector2(screenWidth-128, 0));

            projectiles = new List<Projectile>();
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
            playerProjectileSprite = Content.Load<Texture2D>("PlayerFire");
            enemyProjectileSprite = Content.Load<Texture2D>("EnemyFire");
        }

        // Called every frame
        protected override void Update(GameTime gameTime)
        {   
            // Update base game
            base.Update(gameTime);
            player.Update(gameTime);
            enemy.Update(gameTime);

            foreach(Projectile p in projectiles)
            {
                if(p.Position.X>this.ScreenWidth || p.Position.X<0 || p.Position.Y>this.ScreenHeight || p.Position.Y<0)
                {
                    projectiles.Remove(p);
                    return;
                }
                p.Update();
            }
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
            foreach(Projectile p in projectiles)
            {
                p.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }
    
        public void FireProjectile(Vector2 position, Vector2 velocity, ProjectileType projectileType)
        {
            Texture2D projectileTexture;

            switch (projectileType)
            {
                case ProjectileType.Enemy :
                    projectileTexture = enemyProjectileSprite;
                    break;
                case ProjectileType.Player :
                    projectileTexture = playerProjectileSprite;
                    break;
                default :
                    projectileTexture = playerProjectileSprite;
                    break;
            }

            Projectile firedProjectile = new Projectile(position, velocity, projectileTexture);
            projectiles.Add(firedProjectile);
        }
    }
}
