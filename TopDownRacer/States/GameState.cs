using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TopDownRacer.Sprites;

namespace TopDownRacer.States
{
    public class GameState : State
    {
        //constuctor van de game state
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
        }

        //Het starten van het spel
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, List<Sprite> _sprites, SpriteFont _font)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);

            int fontY = 50;

            foreach (Sprite sprite in _sprites)
            {
                if (sprite is Player)
                {
                    if (!((Player)sprite).Dead)
                        sprite.Draw(spriteBatch);
                    spriteBatch.DrawString(_font, string.Format("Player {0}: {1}", ((Player)sprite).Name, ((Player)sprite).Score), new Vector2(70, fontY += 20), ((Player)sprite).Color, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
                }
                else
                {
                    sprite.Draw(spriteBatch);
                }
            }
            spriteBatch.DrawString(_font, string.Format("Time {0}: ", gameTime.TotalGameTime), new Vector2((Game1.ScreenWidth / 2) - 150, 10), Color.Black);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //kan later worden gebruikt
        }

        public override void Update(GameTime gameTime)
        {
            //kan later worden gebruikt
        }
    }
}