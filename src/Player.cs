using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ArcadeFlyer2D
{
    class Player : Sprite
    {
        private ArcadeFlyerGame root;
        
        private float movementSpeed = 20.0f;
        //private Vector2 cursorPosition;
        //private Timer cooldownTimer;
        private bool firingProjectile = false;

        private Timer burstTimer;

        public Player(ArcadeFlyerGame root, Vector2 position) : base(position)
        {
            this.root = root;
            this.Position = position;
            this.SpriteWidth = 100.0f;

            //cooldownTimer = new Timer(0.5f);

            LoadContent();
        }
        public void LoadContent()
        {
            this.SpriteImage = root.Content.Load<Texture2D>("MainChar");
        }
        public void HandleInput(KeyboardState currentKeyboardState, MouseState currentMouseState)
        {
            Point currentMousePosition = currentMouseState.Position;
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

            bool spaceKeyPressed = currentKeyboardState.IsKeyDown(Keys.Space);
            if (spaceKeyPressed && !firingProjectile)
            {
                Vector2 projectilePosition;
                Vector2 projectileVelocity;

                projectilePosition = new Vector2(position.X + (SpriteWidth / 2), position.Y + (SpriteHeight / 2));
                //projectileVelocity = new Vector2(10.0f, 0.0f);   //original

                //These two lines do the same thing as the following, except with a lot of math instead of code.
                //projectileVelocity.X = ( 10*(currentMousePosition.X-position.X)/(Convert.ToInt32( Math.Sqrt( Math.Pow((currentMousePosition.X-position.X),2) + Math.Pow(((currentMousePosition.Y-position.Y)),2) ) ) ) ); //works
                //projectileVelocity.Y = ( 10*(currentMousePosition.Y-position.Y)/(Convert.ToInt32( Math.Sqrt( Math.Pow((currentMousePosition.X-position.X),2) + Math.Pow(((currentMousePosition.Y-position.Y)),2) ) ) ) ); //works
                projectileVelocity.X = (currentMousePosition.X-position.X);
                projectileVelocity.Y = (currentMousePosition.Y-position.Y);
                projectileVelocity.Normalize();
                projectileVelocity *= 10; 


                root.FireProjectile(projectilePosition, projectileVelocity, ProjectileType.Player);
                firingProjectile = true;
            }

            /*  How to fire consistently
            bool eKeyPressed = currentKeyboardState.IsKeyDown(Keys.E);
            if (eKeyPressed && !cooldownTimer.Active)
            {
                Vector2 projectilePosition;
                Vector2 projectileVelocity;

                projectilePosition = new Vector2(position.X + (SpriteWidth / 2), position.Y + (SpriteHeight / 2));
                projectileVelocity = new Vector2(10.0f, 0.0f);
                root.FireProjectile(projectilePosition, projectileVelocity, ProjectileType.Player);
                cooldownTimer.StartTimer();
            }
            */

            // Shop Management:

            bool tKeyPressed = currentKeyboardState.IsKeyDown(Keys.T);
            if(tKeyPressed && (root.points >= 5) )
            {
                position.X = currentMousePosition.X;
                position.Y = currentMousePosition.Y;
                root.points -= 5;
            }

            bool qKeyPressed = currentKeyboardState.IsKeyDown(Keys.Q);
            if(qKeyPressed && (root.points >= 10) )
            {
                root.life++;
                root.points -= 10;
            }

            bool eKeyPressed = currentKeyboardState.IsKeyDown(Keys.E);
            if(eKeyPressed && (root.points >= 30) )
            {
                //Nothing yet
            }

        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            MouseState currentMouseState = Mouse.GetState();
            HandleInput(currentKeyboardState, currentMouseState);

            bool spaceKeyPressed = currentKeyboardState.IsKeyDown(Keys.Space);
            if (firingProjectile && !spaceKeyPressed)
            {
                firingProjectile = false;
            }

            //cooldownTimer.Update(gameTime);
        }
    }
}