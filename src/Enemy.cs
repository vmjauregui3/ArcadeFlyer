using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    // A little evil thing
    class Enemy : Sprite
    {
        // A reference to the game that will contain this enemy
        private ArcadeFlyerGame root;

        // The the velocity for this enemy
        private Vector2 velocity;
        private int speed;
        public int health;
        private Vector2 playerPosition;

        private Timer projectileCooldown;

        public EnemyType EnemyType;

        // Initialize an enemy
        public Enemy(ArcadeFlyerGame root, Vector2 position, EnemyType enemyType) : base(position)
        {
            // Initialize values
            this.root = root;
            this.position = position;
            this.SpriteWidth = 64.0f;
            this.EnemyType = enemyType;

            switch (EnemyType)
            {
                case EnemyType.Wizard :
                    speed = 3;
                    SpriteWidth = 64.0f;
                    health = 1;
                    projectileCooldown = new Timer(1.0f);
                    break;
                case EnemyType.Boss :
                    speed = 3;
                    health = 15;
                    SpriteWidth = 96.0f;
                    projectileCooldown = new Timer(0.33f);
                    break;
                default :
                    speed = 2;
                    break;
            }

            /*
            var rand = new System.Random();
            int randVelocityX = rand.Next(-4,-1);
            int randVelocityY = rand.Next(-3,5);
            while(randVelocityY == 0)
            {
                randVelocityY = rand.Next(-3,5);
            }
            this.velocity = new Vector2(randVelocityX, randVelocityY);
            //this.velocity = new Vector2(-1.0f, 2.0f); //Original Default Speed
            */

            // Load the content for this enemy
            LoadContent();
        }

        // Loads all the assets for this enemy
        public void LoadContent()
        {
            // Get the Enemy image
            switch (EnemyType)
            {
                case EnemyType.Wizard :
                    this.SpriteImage = root.Content.Load<Texture2D>("Enemy");
                    break;
                case EnemyType.Boss :
                    this.SpriteImage = root.Content.Load<Texture2D>("Boss");
                    break;
                default :
                    this.SpriteImage = root.Content.Load<Texture2D>("Enemy");
                    break;
            }
        }

        // Called each frame
        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            // Handle movement
            this.playerPosition = playerPosition;

            velocity.X = (playerPosition.X-position.X);
            velocity.Y = (playerPosition.Y-position.Y);
            velocity.Normalize();

            // Bounce on top and bottom
            if (position.Y <= 0 || position.Y > (root.ScreenHeight*10 - SpriteHeight))
            {
                velocity.Y *= -1;
            }
            if (position.X <= 0 || position.X > (root.ScreenWidth*10 - SpriteWidth))
            {
                velocity.X *= -1;
            }

            position += (velocity*speed);

            projectileCooldown.Update(gameTime);
            if(!projectileCooldown.Active)
            {
                projectileCooldown.StartTimer();
                Vector2 projectilePosition = new Vector2();
                projectilePosition.X = position.X;
                projectilePosition.Y = position.Y + (SpriteHeight / 2);
                
                Vector2 projectileVelocity = new Vector2();
                projectileVelocity.X = (playerPosition.X-position.X);
                projectileVelocity.Y = (playerPosition.Y-position.Y);
                projectileVelocity.Normalize();
                projectileVelocity *= 5;
                
                if(EnemyType == EnemyType.Wizard)
                {
                    root.FireProjectile(projectilePosition, projectileVelocity, ProjectileType.EnemyFire);
                }
                else if(EnemyType == EnemyType.Boss)
                {
                    root.FireProjectile(projectilePosition, projectileVelocity, ProjectileType.BossFire);
                }
            }
        }
    }
}
