using Microsoft.Xna.Framework;

namespace ArcadeFlyer2D
{
    public class Camera
    {
        private ArcadeFlyerGame root;
        public Matrix Transform { get; protected set; }
        public Vector2 absolutePosition;

        public Camera (ArcadeFlyerGame root)
        {
            this.root = root;
        }

        public void Follow(Sprite target)
        {
            Vector2 absolutePosition = new Vector2(target.Position.X, target.Position.Y);

            Matrix offset = Matrix.CreateTranslation(
                    root.ScreenWidth/2, 
                    root.ScreenHeight/2,
                    0);
            Matrix position = Matrix.CreateTranslation(
                -target.Position.X - (target.PositionRectangle.Width/2), 
                -target.Position.Y - (target.PositionRectangle.Height/2),
                0);
            Transform = position * offset;
        }
    }
}