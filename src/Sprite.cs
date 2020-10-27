using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    class Sprite
    {
        // The current position of the sprite
        protected Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
        // An image texture for the sprite
        private Texture2D spriteImage;
        public Texture2D SpriteImage
        {
            get { return spriteImage; }
            set { spriteImage = value; }
        }
        
        // The width of the sprite
        private float spriteWidth;
        public float SpriteWidth
        {
            get { return spriteWidth; }
            set { spriteWidth = value; }
        }
        
        // The height of the sprite
        public float SpriteHeight
        {
            get
            {
                float scale = spriteWidth / spriteImage.Width;
                return spriteImage.Height * scale;
            }
        }
        // The properly scaled position rectangle for the sprite
        public Rectangle PositionRectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)spriteWidth, (int)SpriteHeight);
            }
        }

        public Sprite(Vector2 position)
        {
            this.position = position;
        }
        
        //Draw the sprite
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteImage, PositionRectangle, Color.White);
        }

    }
}