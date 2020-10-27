using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArcadeFlyer2D
{
    class Enemy
    {
        private ArcadeFlyerGame root;

        private Vector2 position;

        private Texture2D spriteImage;

        private float spriteWidth = 100.0f;

        private Vector2 velocity;

        public float SpriteHeight
        {
            get 
            {
                float scale = spriteWidth / spriteImage.Width;
                return spriteImage.Height * scale;
            }
        }
        
        public Rectangle PositionRectangle
        {
            get 
            { 
                return new Rectangle((int)position.X, (int)position.Y, (int)spriteWidth, (int)SpriteHeight);
            }
        }

        public Enemy(ArcadeFlyerGame root, Vector2 position)
        {
            this.root = root;
            this.position = position;
            this.spriteWidth = 128.0f;
            this.velocity = new Vector2(-5.0f, 25.0f);

            LoadContent();
        }
        public void LoadContent()
        {
            this.spriteImage = root.Content.Load<Texture2D>("Enemy");
        }

        public void Update(GameTime gametime)
        {
            position += velocity;        
            if(position.Y < 0 || position.Y > (root.ScreenHeight - SpriteHeight))
            {
                velocity.Y *= -1;
            }

            if(position.X < 0 || position.X > (root.ScreenWidth - spriteWidth))
            {
                velocity.X *= -1;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteImage, PositionRectangle, Color.White);
        }
    }
}