using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace TopDownRacer.Sprites
{
    internal class Checkpoint : Sprite
    {
        private int checkpointList = 0;
        public Checkpoint(Texture2D texture, int orientation, int height, int width, int checkpointList)
        : base(texture)
        {
            this.orientation = orientation;
            this.height = height;
            this.width = width;
            sourceRectangle = Rectangle;
            switch (orientation)
            {
                case 0:
                    getCornerCoordsDel = getCornerCoordsTop;
                    break;

                case 1:
                    getCornerCoordsDel = getCornerCoordsRight;
                    break;

                case 2:
                    getCornerCoordsDel = getCornerCoordsBottom;
                    break;

                case 3:
                    getCornerCoordsDel = getCornerCoordsLeft;
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, sourceRectangle, Color, Rotation, Vector2.Zero, 1, SpriteEffects.None, 0.9f);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite is Player)
                    if (!((Player)sprite).Dead)
                    {
                        // the car is moving right
                        var coords = getCornerCoordsDel(sprite);
                        if (coords.Y < this.Position.Y + height && coords.Y > this.Position.Y && coords.X < this.Position.X + width && coords.X > this.Position.X)
                        {
                            checkpointList++;
                            Debug.WriteLine(checkpointList);
                            Debug.WriteLine(sprite);
                            ((Player)sprite).Score += 500;
                        }
                    }
            }
        }
    }
}