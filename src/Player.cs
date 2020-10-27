using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArcadeFlyer2D
{
    class Player : Sprite
    {
        private ArcadeFlyerGame root;
        
        private float movementSpeed = 20.0f;

        public Player(ArcadeFlyerGame root, Vector2 position) : base(position)
        {
            this.root = root;
            this.Position = position;
            this.SpriteWidth = 100.0f;

            LoadContent();
        }

        public void LoadContent()
        {
            this.SpriteImage = root.Content.Load<Texture2D>("MainChar");
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

    }
}