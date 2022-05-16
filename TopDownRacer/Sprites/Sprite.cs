using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TopDownRacer.Models;

namespace TopDownRacer.Sprites
{
    public class Sprite
    {
        protected Texture2D _texture;
        public Vector2 Position;
        public Vector2 Origin;
        public Color Color = Color.White;
        public Input Input;
        public float Rotation;

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            spriteBatch.Draw(_texture, Position, null, Color, Rotation, Origin, 1, SpriteEffects.None, 0f);
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
        }
    }
}