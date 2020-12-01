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
        private int life = 3;
        private int score = 0;

        private List<Enemy> enemies;
        private Timer enemyCreationTimer;

        private List<Projectile> projectiles;
        private Texture2D playerProjectileSprite;
        private Texture2D enemyProjectileSprite;

        //Font for drawing text
        private SpriteFont textFont;
        
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

            enemies = new List<Enemy>();
            enemies.Add(new Enemy(this, new Vector2(screenWidth-128, 0)));

            enemyCreationTimer = new Timer(1.0f);
            enemyCreationTimer.StartTimer();

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
            //Load the font
            textFont = Content.Load<SpriteFont>("Text");
        }

        // Called every frame
        protected override void Update(GameTime gameTime)
        {   
            // Update base game
            base.Update(gameTime);
            player.Update(gameTime);
            
            foreach(Enemy enemy in enemies)
            {
                enemy.Update(gameTime);
            }

            foreach(Projectile p in projectiles)
            {
                if(p.Position.X>this.ScreenWidth || p.Position.X<0 || p.Position.Y>this.ScreenHeight || p.Position.Y<0)
                {
                    projectiles.Remove(p);
                    return;
                }
                p.Update();
            }

            for(int i = projectiles.Count - 1; i >= 0; i--)
            {
                Projectile p = projectiles[i];
                p.Update();

                bool isPlayerProjectile = p.ProjectileType == ProjectileType.Player;

                if(!isPlayerProjectile && player.Overlaps(p))
                {
                    projectiles.Remove(p);
                    life--; //Same as: life = life -1;
                }
                else if(isPlayerProjectile)
                {
                    for(int j = enemies.Count - 1; j >= 0; j--)
                    {
                        Enemy e = enemies[j];

                        if(e.Overlaps(p))
                        {
                            projectiles.Remove(p);
                            enemies.Remove(e);
                            score = score + 1;
                        }
                    }
                    for(int k = projectiles.Count - 1; k >= 0; k--)
                    {
                        Projectile q = projectiles[k];

                        if(q.Overlaps(p) && q.ProjectileType == ProjectileType.Enemy)
                        {
                            projectiles.Remove(q); k--;
                            projectiles.Remove(p); i--;
                        }
                    }
                }
            }

            for(int i = enemies.Count - 1; i >= 0; i--)
            {
                Enemy e = enemies[i];

                if(e.Overlaps(player))
                {
                    enemies.Remove(e);
                    life = life - 2;
                }
            }

            if(!enemyCreationTimer.Active)
            {
                var randSpawnHeight = new System.Random();
                enemies.Add(new Enemy(this, new Vector2(screenWidth-128, randSpawnHeight.Next(0,screenHeight))));

                enemyCreationTimer.StartTimer();
            }
            enemyCreationTimer.Update(gameTime);

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

            foreach(Enemy enemy in enemies)
            {
                enemy.Draw(gameTime, spriteBatch);
            }

            foreach(Projectile p in projectiles)
            {
                p.Draw(gameTime, spriteBatch);
            }

            //Draw the score and lives
            string scoreString = "Score: " + score.ToString();
            string livesString = "Lives: " + life.ToString();
            spriteBatch.DrawString(textFont, scoreString, Vector2.Zero, Color.Maroon);
            spriteBatch.DrawString(textFont, livesString, new Vector2(0.0f, 20.0f), Color.Maroon);

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

            Projectile firedProjectile = new Projectile(position, velocity, projectileTexture, projectileType);
            projectiles.Add(firedProjectile);
        }
    }
}
