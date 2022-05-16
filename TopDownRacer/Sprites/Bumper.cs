using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TopDownRacer.Sprites
{
    class Bumper : Sprite
    {
        private Rectangle sourceRectangle;
        private delegate Vector2 getCornerCoords(Sprite sprite);
        private getCornerCoords getCornerCoordsDel;
        private int orientation;
        public int height;
        public int width;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, width, height);
            }
        }

        public Bumper(Texture2D texture, int orientation, int height, int width)
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
            spriteBatch.Draw(_texture, Position, sourceRectangle, Color, Rotation, Vector2.Zero, 1, SpriteEffects.None, 0f);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite is Player)
                {
                    // the car is moving right
                    var coords = getCornerCoordsDel(sprite);
                    if (coords.Y < this.Position.Y + height && coords.Y > this.Position.Y && coords.X < this.Position.X + width && coords.X > this.Position.X)
                    {
                        this.Color = Color.Red;
                    }
                }
            }
        }

        public Vector2 getCornerCoordsTop(Sprite sprite)
        {
            // calculate the orientation and choose which method to call
            var direction = new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation));
            if (direction.Y < 0)
            {
                if (direction.X < 0)
                {
                    return cornerCoordFR(sprite);
                }
                else
                {
                    return cornerCoordFL(sprite);
                }

            }
            else
            {
                if (direction.X < 0)
                {
                    return cornerCoordBR(sprite);
                }
                else
                {
                    return cornerCoordBL(sprite);
                }
            }
        }
        public Vector2 getCornerCoordsRight(Sprite sprite)
        {
            // calculate the orientation and choose which method to call
            var direction = new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation));
            if (direction.X > 0)
            {
                if (direction.Y < 0)
                {
                    return cornerCoordFR(sprite);
                }
                else
                {
                    return cornerCoordFL(sprite);
                }

            }
            else
            {
                if (direction.Y < 0)
                {
                    return cornerCoordBR(sprite);
                }
                else
                {
                    return cornerCoordBL(sprite);
                }
            }
        }
        public Vector2 getCornerCoordsBottom(Sprite sprite)
        {
            // calculate the orientation and choose which method to call
            var direction = new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation));
            if (direction.Y > 0)
            {
                if (direction.X < 0)
                {
                    return cornerCoordFR(sprite);
                }
                else
                {
                    return cornerCoordFL(sprite);
                }

            }
            else
            {
                if (direction.X < 0)
                {
                    return cornerCoordBR(sprite);
                }
                else
                {
                    return cornerCoordBL(sprite);
                }
            }
        }

        public Vector2 getCornerCoordsLeft(Sprite sprite)
        {
            // calculate the orientation and choose which method to call
            var direction = new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation));
            if (direction.X < 0)
            {
                if (direction.Y > 0)
                {
                    return cornerCoordFR(sprite);
                }
                else
                {
                    return cornerCoordFL(sprite);
                }

            }
            else
            {
                if (direction.Y > 0)
                {
                    return cornerCoordBR(sprite);
                }
                else
                {
                    return cornerCoordBL(sprite);
                }
            }
        }

        private Vector2 cornerCoordFR(Sprite sprite)
        {
            // Corner front Right
            float Ox = sprite._texture.Width / 2;
            float Oy = sprite._texture.Height / 2;
            float θ = MathHelper.ToDegrees(sprite.Rotation);
            float Rx = (float)(sprite.Position.X + (Ox * Math.Cos(θ)) - (Oy * Math.Sin(θ)));
            float Ry = (float)(sprite.Position.Y + (Ox * Math.Sin(θ)) + (Oy * Math.Cos(θ)));
            return new Vector2(Rx, Ry);
        }

        private Vector2 cornerCoordFL(Sprite sprite)
        {
            // Corner Bottom Right
            float Ox = sprite._texture.Width / 2;
            float Oy = -sprite._texture.Height / 2;
            float θ = MathHelper.ToDegrees(sprite.Rotation);
            float Rx = (float)(sprite.Position.X + (Ox * Math.Cos(θ)) - (Oy * Math.Sin(θ)));
            float Ry = (float)(sprite.Position.Y + (Ox * Math.Sin(θ)) + (Oy * Math.Cos(θ)));
            return new Vector2(Rx, Ry);
        }

        private Vector2 cornerCoordBR(Sprite sprite)
        {
            // Corner Bottom Right
            float Ox = -sprite._texture.Width / 2;
            float Oy = sprite._texture.Height / 2;
            float θ = MathHelper.ToDegrees(sprite.Rotation);
            float Rx = (float)(sprite.Position.X + (Ox * Math.Cos(θ)) - (Oy * Math.Sin(θ)));
            float Ry = (float)(sprite.Position.Y + (Ox * Math.Sin(θ)) + (Oy * Math.Cos(θ)));
            return new Vector2(Rx, Ry);
        }

        private Vector2 cornerCoordBL(Sprite sprite)
        {
            // Corner Bottom Right
            float Ox = -sprite._texture.Width / 2;
            float Oy = -sprite._texture.Height / 2;
            float θ = MathHelper.ToDegrees(sprite.Rotation);
            float Rx = (float)(sprite.Position.X + (Ox * Math.Cos(θ)) - (Oy * Math.Sin(θ)));
            float Ry = (float)(sprite.Position.Y + (Ox * Math.Sin(θ)) + (Oy * Math.Cos(θ)));
            return new Vector2(Rx, Ry);
        }
    }
}
