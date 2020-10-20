using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArcadeFlyer2D
{
    class Player
    {
        private  float spriteWidth;
        private ArcadeFlyerGame root;
        private Vector2 position;
        private Texture2D spriteImage;
        
        private float movementSpeed = 20.0f;

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
                Rectangle rec = new Rectangle((int)position.X, (int)position.Y, (int)spriteWidth, (int)SpriteHeight);
                return rec;
            }
        }
        public Player(ArcadeFlyerGame root, Vector2 position)
        {
            this.root = root;
            this.position = position;
            this.spriteWidth = 100.0f;

            LoadContent();
        }

        public void LoadContent()
        {
            this.spriteImage = root.Content.Load<Texture2D>("MainChar");
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            HandleInput(currentKeyboardState);
        }

        public void HandleInput(KeyboardState currentKeyboardState)
        {
            bool wKeyPressed = currentKeyboardState.IsKeyDown(Keys.W);
            if(wKeyPressed && position.Y >= 0 + movementSpeed)
            {
                position.Y -= movementSpeed; //position.Y = position.Y - movementSpeed
            }

            bool sKeyPressed = currentKeyboardState.IsKeyDown(Keys.S);
            if(sKeyPressed)
            {
                position.Y += movementSpeed;
            }

            bool aKeyPressed = currentKeyboardState.IsKeyDown(Keys.A);
            if(aKeyPressed && position.X >= 0 + movementSpeed)
            {
                position.X -= movementSpeed;
            }

            bool dKeyPressed = currentKeyboardState.IsKeyDown(Keys.D);
            if(dKeyPressed)
            {
                position.X += movementSpeed;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteImage, PositionRectangle, Color.White);
        }
    }
}