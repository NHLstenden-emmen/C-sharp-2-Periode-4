using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TopDownRacer.Models;

namespace TopDownRacer.Sprites
{
    public class Sprite
    {
        public Texture2D _texture;
        public Vector2 Position;
        public Vector2 Origin;
        public Color Color = Color.White;
        public Input Input;
        public float Rotation = 0;
        public float CurrentPositionSpeed = 0;

        public Rectangle sourceRectangle;

        public delegate Vector2 getCornerCoords(Sprite sprite);

        public getCornerCoords getCornerCoordsDel;
        public int orientation;
        public int height;
        public int width;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, width, height);
            }
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            spriteBatch.Draw(_texture, Position, null, Color, Rotation, Origin, 1, SpriteEffects.None, 0f);
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
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