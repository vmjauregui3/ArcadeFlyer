using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace ArcadeFlyer2D
{
    // The Game itself
    public class ArcadeFlyerGame : Game
    {
        // Graphics Manager
        private GraphicsDeviceManager graphics;

        // Sprite Drawer
        private SpriteBatch spriteBatch;

        private Camera camera;

        private FieldBounds fieldBounds;

        public Player player; 
        public int life = 300;
        private int score = 0;
        public int points = 0;
        //Is the game over?
        private bool gameOver = false;
        private List<Enemy> enemies;
        private Timer enemyCreationTimer;
        private Timer bossCreationTimer;

        private List<Projectile> projectiles;
        private Texture2D playerProjectileSprite;
        private Texture2D enemyProjectileSprite;
        private Texture2D bossProjectileSprite;
        public MouseState testState;

        //Font for drawing text
        private SpriteFont textFont;
        
        //propfull
        //ctor
        private int screenWidth = 1450;
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

            //viewport = new Viewport(0,0,screenWidth,screenHeight);

            // Set up the directory containing the assets
            Content.RootDirectory = "Content";

            // Make mouse visible
            IsMouseVisible = true;

            Vector2 position = new Vector2(100.0f, 100.0f);
            player = new Player(this, position);
            fieldBounds = new FieldBounds(this);

            enemies = new List<Enemy>();
            enemies.Add(new Enemy(this, new Vector2(19*(screenWidth/20), screenHeight/2), EnemyType.Wizard));

            enemyCreationTimer = new Timer(1.0f);
            enemyCreationTimer.StartTimer();

            bossCreationTimer = new Timer(15.0f);
            bossCreationTimer.StartTimer();

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
            bossProjectileSprite = Content.Load<Texture2D>("BossFire");
            //Load the font
            textFont = Content.Load<SpriteFont>("Text");

            camera = new Camera(this);
        }

        // Called every frame
        protected override void Update(GameTime gameTime)
        {   
            // Update base game
            base.Update(gameTime);
            camera.Follow(player);
            fieldBounds.Update(player.Position);

            MouseState testState = Mouse.GetState();

            //Game is over
            if(gameOver)
            {
                return;
            }

            player.Update(gameTime, fieldBounds, camera.absolutePosition);
            
            foreach(Enemy enemy in enemies)
            {
                enemy.Update(gameTime, player.Position);
            }

            foreach(Projectile p in projectiles)
            {
                if(p.Position.X>(fieldBounds.Boundaries.X+fieldBounds.Boundaries.Width) 
                || p.Position.X<fieldBounds.Boundaries.X
                || p.Position.X<0
                || p.Position.X>ScreenWidth*10
                || p.Position.Y>(fieldBounds.Boundaries.Y+fieldBounds.Boundaries.Height) 
                || p.Position.Y<fieldBounds.Boundaries.Y
                || p.Position.Y<0
                || p.Position.Y>ScreenHeight*10)
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
                            e.health -= 1;
                            projectiles.Remove(p);
                        }

                        if(e.health <= 0)
                        {
                            if(e.EnemyType == EnemyType.Wizard)
                            {
                                
                                enemies.Remove(e);
                                score += 1;
                                points += 1;
                            }
                            else if(e.EnemyType == EnemyType.Boss)
                            {
                                enemies.Remove(e);
                                score += 5;
                                points += 5;
                            }
                        }
                    }
                    for(int k = projectiles.Count - 1; k >= 0; k--)
                    {
                        Projectile q = projectiles[k];

                        if(q.Overlaps(p) && (q.ProjectileType == ProjectileType.EnemyFire || q.ProjectileType == ProjectileType.BossFire))
                        {
                            projectiles.Remove(q);
                            projectiles.Remove(p);
                            k--;  i--;
                        }
                    }
                }
            }

            for(int i = enemies.Count - 1; i >= 0; i--)
            {
                Enemy e = enemies[i];

                if(e.Overlaps(player) && e.EnemyType == EnemyType.Wizard)
                {
                    enemies.Remove(e);
                    life -= 2;
                }
            }

            if(!enemyCreationTimer.Active)
            {
                var randSpawnWidth = new System.Random();
                var randSpawnHeight = new System.Random();
                enemies.Add(new Enemy(this, new Vector2(randSpawnWidth.Next(screenWidth,10*screenWidth), randSpawnHeight.Next(0,10*ScreenHeight)), EnemyType.Wizard));

                Enemy lastAddedEnemy = enemies[enemies.Count - 1];
                if(lastAddedEnemy.Position.X >= (10*screenWidth - lastAddedEnemy.SpriteWidth) )
                {
                    Vector2 enemyPositionCorrectorX = new Vector2(10*screenWidth-lastAddedEnemy.SpriteWidth,lastAddedEnemy.Position.Y);
                    lastAddedEnemy.Position = enemyPositionCorrectorX;
                }
                if(lastAddedEnemy.Position.Y >= (10*screenHeight - lastAddedEnemy.SpriteHeight) )
                {
                    Vector2 enemyPositionCorrectorY = new Vector2(lastAddedEnemy.Position.X,(screenHeight - lastAddedEnemy.SpriteHeight));
                    lastAddedEnemy.Position = enemyPositionCorrectorY;
                }

                enemyCreationTimer.StartTimer();
            }
            enemyCreationTimer.Update(gameTime);

            if(!bossCreationTimer.Active && score > 10)
            {
                var randSpawnWidth = new System.Random();
                var randSpawnHeight = new System.Random();
                enemies.Add(new Enemy(this, new Vector2(player.Position.X+800, player.Position.Y+800), EnemyType.Boss));

                Enemy lastAddedEnemy = enemies[enemies.Count - 1];
                if(lastAddedEnemy.Position.X >= (10*screenWidth - lastAddedEnemy.SpriteWidth) )
                {
                    Vector2 enemyPositionCorrectorX = new Vector2(10*screenWidth-lastAddedEnemy.SpriteWidth,lastAddedEnemy.Position.Y);
                    lastAddedEnemy.Position = enemyPositionCorrectorX;
                }
                if(lastAddedEnemy.Position.Y >= (10*screenHeight - lastAddedEnemy.SpriteHeight) )
                {
                    Vector2 enemyPositionCorrectorY = new Vector2(lastAddedEnemy.Position.X,(screenHeight - lastAddedEnemy.SpriteHeight));
                    lastAddedEnemy.Position = enemyPositionCorrectorY;
                }

                bossCreationTimer.StartTimer();
            }
            bossCreationTimer.Update(gameTime);

            if(life <= 0)
            {
                gameOver = true;
            }
        }
        // Draw everything in the game
        protected override void Draw(GameTime gameTime)
        {
            // First clear the screen
            GraphicsDevice.Clear(Color.LightSteelBlue);

            spriteBatch.Begin(transformMatrix: camera.Transform);
            /*
                //Drawing will happen here
                Rectangle playerDestinationRect = new Rectangle(100,100,playerImage.Width,playerImage.Height);
                spriteBatch.Draw(playerImage, playerDestinationRect, Color.LightGreen); 
                //Color.White adds a clear filter to show the image. Color.(other) tints it that color
            */ //test (/*)
            if(!gameOver)
            {
                player.Draw(gameTime, spriteBatch);
            }
            
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
            //string scoreString = testState.X.ToString();
            //string livesString = testState.Y.ToString();
            string livesPoints = "Points: " + points.ToString();
            
            spriteBatch.DrawString(textFont, scoreString, new Vector2(fieldBounds.ScreenBoundaries.X, fieldBounds.ScreenBoundaries.Y), Color.Black);
            spriteBatch.DrawString(textFont, livesString, new Vector2(fieldBounds.ScreenBoundaries.X, fieldBounds.ScreenBoundaries.Y+20.0f), Color.Black);
            spriteBatch.DrawString(textFont, livesPoints, new Vector2(fieldBounds.ScreenBoundaries.X, fieldBounds.ScreenBoundaries.Y+45.0f), Color.Black);

            if(gameOver)
            {
                spriteBatch.DrawString(textFont, "Game Over!", new Vector2((screenWidth/2),(screenHeight/2)), Color.Black);
                string finalScore = "Final Score " + score.ToString();
                spriteBatch.DrawString(textFont, finalScore, new Vector2((screenWidth/2),(screenHeight/2)+20), Color.Black);
            }

            spriteBatch.End();
        }
    
        public void FireProjectile(Vector2 position, Vector2 velocity, ProjectileType projectileType)
        {
            Texture2D projectileTexture;

            switch (projectileType)
            {
                case ProjectileType.EnemyFire :
                    projectileTexture = enemyProjectileSprite;
                    break;
                case ProjectileType.BossFire :
                    projectileTexture = bossProjectileSprite;
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
