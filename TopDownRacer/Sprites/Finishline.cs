using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TopDownRacer.Sprites
{
    internal class Finishline : Sprite
    {
        public int amountCheckpoint = 0;

        public Finishline(Texture2D texture, int orientation, int width, int height)
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

        public override void Draw(SpriteBatch spriteBatch, float layerdepth)
        {
            spriteBatch.Draw(_texture, Position, sourceRectangle, Color, Rotation, Vector2.Zero, 1, SpriteEffects.None, layerdepth);
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
                            if (amountCheckpoint == ((Player)sprite).checkpointId)
                            {
                                ((Player)sprite).Score += 1000;
                                ((Player)sprite).Dead = true;
                            }
                        }
                    }
            }
        }
    }
}