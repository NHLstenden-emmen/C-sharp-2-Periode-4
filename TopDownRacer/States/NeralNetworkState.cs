using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TopDownRacer.Controller;
using TopDownRacer.Sprites;

namespace TopDownRacer.States
{
    internal class NeralNetworkState : State
    {
        //constuctor van de game state
        public NeralNetworkState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            playerTexture.Insert(0, content.Load<Texture2D>("Player/car_small_1"));
            playerTexture.Insert(1, content.Load<Texture2D>("Player/car_small_2"));
            playerTexture.Insert(2, content.Load<Texture2D>("Player/car_small_3"));
            playerTexture.Insert(3, content.Load<Texture2D>("Player/car_small_4"));
            playerTexture.Insert(4, content.Load<Texture2D>("Player/car_small_5"));

            bumperTexture = content.Load<Texture2D>("Levels/tires_white_small");
            finishlineTexture = content.Load<Texture2D>("Levels/finishline");
            checkpointTexture = content.Load<Texture2D>("Levels/checkpoint");
            backgroundTexture = content.Load<Texture2D>("Levels/background");

            var xmlMap = XmlMapReader.LoadMap("L-shape");
            game._sprites = xmlMap.getSprites();
        }

        //Het starten van het spel
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, List<Sprite> _sprites, SpriteFont _font)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.LinearWrap, null, null);

            int fontY = 50;
            // draw background
            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), null, Color.White, 0, new Vector2(0, 0), 4, SpriteEffects.None, 0.1f);

            foreach (Sprite sprite in _sprites)
            {
                if (sprite is Player)
                {
                    var ScoreBoardPosition = new Vector2(70, fontY += 20);
                    // draw scoarboard background
                    spriteBatch.Draw(bumperTexture, ScoreBoardPosition, null, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0.2f);
                    // draw text on scoarboard
                    spriteBatch.DrawString(_font, string.Format("Player {0}: {1}", ((Player)sprite).Name, ((Player)sprite).Score), ScoreBoardPosition, ((Player)sprite).Color, 0, Vector2.Zero, 1, SpriteEffects.None, 0.4f);
                    if (!((Player)sprite).Dead)
                    {
                        // draw player
                        sprite.Draw(spriteBatch, 0.6f);
                        //draw closet barrier front indication
                        spriteBatch.DrawString(_font, "00000", findClosestBarrierFront(sprite), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0.4f);
                    }
                }
                else
                {
                    // draw walls checkpoints and finish line
                    sprite.Draw(spriteBatch, 0.3f);
                }
            }
            // draw time
            spriteBatch.DrawString(_font, string.Format("Time {0}: ", gameTime.TotalGameTime), new Vector2((Game1.ScreenWidth / 2) - 150, 10), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.9f);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //kan later worden gebruikt
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Sprite sprite in _game._sprites)
            {
                sprite.Update(gameTime, _game._sprites);
            }
        }

        private Vector2 findClosestBarrierFront(Sprite sprite)
        {
            int count = 0;

            while (count < 1920 / 28)
            {
                count++;

                //double yDif = Math.Tan(sprite.Rotation) * (sprite.Position.X  + count * (bumperTexture.Width / 2) - sprite.Position.X);
                //double y = sprite.Position.Y - yDif;
                double y = sprite.Position.Y + ((count * bumperTexture.Width / 2) * Math.Sin((sprite.Rotation)));

                double x = sprite.Position.X + ((count * bumperTexture.Width / 2) * Math.Cos((sprite.Rotation)));

                //Debug.WriteLine(x + ", " + y + " - " + MathHelper.ToDegrees(sprite.Rotation));

                foreach (var sprite2 in _game._sprites)
                {
                    if (sprite2 is Bumper)
                    {
                        //Debug.WriteLine("car postion: " + x + ", " + y + " - Bumber: " + sprite.);

                        if (y < sprite2.Position.Y + sprite2.height && y > sprite2.Position.Y && x < sprite2.Position.X + sprite2.width && x > sprite2.Position.X)
                        {
                            return new Vector2((float)x, (float)y);
                        }
                    }
                }
            }

            return new Vector2(0, 0);
        }
    }
}