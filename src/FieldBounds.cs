using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    public class FieldBounds
    {
        private ArcadeFlyerGame root;
        private Vector2 playerPosition;
        public Rectangle Boundaries;
        public Rectangle ScreenBoundaries;
        public Vector2 OriginalScreenMiddle;
        public Vector2 ScreenMiddle;

        public FieldBounds(ArcadeFlyerGame root)
        {
            this.root = root;

            Rectangle Boundaries = new Rectangle(0,0, root.ScreenWidth, root.ScreenHeight);
            Rectangle ScreenBoundaries = new Rectangle(0,0, root.ScreenWidth, root.ScreenHeight);
            Vector2 OriginalScreenMiddle = new Vector2(root.ScreenWidth/2, root.ScreenHeight/2);
        }

        public void Update(Vector2 playerPosition)
        {
            this.playerPosition = playerPosition;

            Boundaries.X = (int)playerPosition.X+(root.player.PositionRectangle.Width/2)-(root.ScreenWidth);
            Boundaries.Y = (int)playerPosition.Y+(root.player.PositionRectangle.Height/2)-(root.ScreenHeight);
            Boundaries.Width = 2*root.ScreenWidth;
            Boundaries.Height = 2*root.ScreenHeight;

            ScreenBoundaries.X = (int)playerPosition.X+(root.player.PositionRectangle.Width/2)-(root.ScreenWidth/2);
            ScreenBoundaries.Y = (int)playerPosition.Y+(root.player.PositionRectangle.Height/2)-(root.ScreenHeight/2);
            ScreenBoundaries.Width = root.ScreenWidth;
            ScreenBoundaries.Height = root.ScreenHeight;

            Vector2 ScreenMiddle = new Vector2((int)playerPosition.X+(root.player.PositionRectangle.Width/2), (int)playerPosition.Y+(root.player.PositionRectangle.Height/2));

            //cooldownTimer.Update(gameTime);
        }
    }
}