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

        private Timer projectileCooldown;

        // Initialize an enemy
        public Enemy(ArcadeFlyerGame root, Vector2 position) : base(position)
        {
            // Initialize values
            this.root = root;
            this.Position = position;
            this.SpriteWidth = 64.0f;
            this.velocity = new Vector2(-1.0f, 2.0f);

            projectileCooldown = new Timer(1.0f);

            // Load the content for this enemy
            LoadContent();
        }

        // Loads all the assets for this enemy
        public void LoadContent()
        {
            // Get the Enemy image
            this.SpriteImage = root.Content.Load<Texture2D>("Enemy");
        }

        // Called each frame
        public void Update(GameTime gameTime)
        {
            // Handle movement
            position += velocity;

            // Bounce on top and bottom
            if (position.Y < 0 || position.Y > (root.ScreenHeight - SpriteHeight))
            {
                velocity.Y *= -1;
            }
            if (position.X < 0 || position.X > (root.ScreenWidth - SpriteWidth))
            {
                velocity.X *= -1;
            }

            projectileCooldown.Update(gameTime);
            if(!projectileCooldown.Active)
            {
                projectileCooldown.StartTimer();
                Vector2 projectilePosition = new Vector2();
                projectilePosition.X = position.X;
                projectilePosition.Y = position.Y + (SpriteHeight / 2);
                Vector2 projectileVelocity = new Vector2();
                projectileVelocity.X = -5.0f;
                projectileVelocity.Y = 0f;
                root.FireProjectile(projectilePosition, projectileVelocity, ProjectileType.Enemy);
            }
        }
    }
}
