using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TopDownRacer.Models;
using TopDownRacer.Sprites;

namespace TopDownRacer.States
{
    public class GameState : State
    {
        //constuctor van de game state
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var playerTexture = new List<Texture2D> { };

            playerTexture.Insert(0, content.Load<Texture2D>("Player/car_small_1"));
            playerTexture.Insert(1, content.Load<Texture2D>("Player/car_small_2"));
            playerTexture.Insert(2, content.Load<Texture2D>("Player/car_small_3"));
            playerTexture.Insert(3, content.Load<Texture2D>("Player/car_small_4"));
            playerTexture.Insert(4, content.Load<Texture2D>("Player/car_small_5"));

            bumperTexture = content.Load<Texture2D>("Levels/tires_white");
            finishlineTexture = content.Load<Texture2D>("Levels/finishline");
            checkpointTexture = content.Load<Texture2D>("Levels/checkpoint");
            backgroundTexture = content.Load<Texture2D>("Levels/background");

            game._sprites = new List<Sprite>()
              {
                new Player(playerTexture[game.rnd.Next(playerTexture.Count)])
                {
                  Name = "Simchaja",
                  Input = new Input(),
                  Color = Color.Blue,
                },
                new Player(playerTexture[game.rnd.Next(playerTexture.Count)])
                {
                  Name = "Roan",
                  Input = new Input()
                  {
                    Left = Keys.Left,
                    Right = Keys.Right,
                    Up = Keys.Up,
                    Down = Keys.Down,
                  },
                  Color = Color.Green,
                },
                // outer ring
                new Bumper(bumperTexture, 0, Game1.ScreenWidth,bumperTexture.Height)
                {
                    Position = new Vector2(0,0)
                },
                new Bumper(bumperTexture, 1, bumperTexture.Width, Game1.ScreenHeight - Game1.TrackWidth)
                {
                    Position = new Vector2(Game1.ScreenWidth - bumperTexture.Width, 0)
                },
                new Bumper(bumperTexture, 2, Game1.ScreenWidth, bumperTexture.Height)
                {
                    Position = new Vector2(0, Game1.ScreenHeight - bumperTexture.Height)
                },
                new Bumper(bumperTexture, 3,bumperTexture.Width, Game1.ScreenHeight)
                {
                    Position = new Vector2(0, 0)
                },
                // inner ring

                new Bumper(bumperTexture, 0, Game1.ScreenWidth-Game1.TrackWidth*2, bumperTexture.Height)
                {
                    Position = new Vector2(Game1.TrackWidth,Game1.TrackWidth)
                },
                new Bumper(bumperTexture, 1, bumperTexture.Width, Game1.ScreenHeight - Game1.TrackWidth*2)
                {
                    Position = new Vector2(Game1.ScreenWidth - bumperTexture.Width - Game1.TrackWidth, Game1.TrackWidth)
                },
                new Bumper(bumperTexture, 2, Game1.ScreenWidth - Game1.TrackWidth * 2, bumperTexture.Height)
                {
                    Position = new Vector2(Game1.TrackWidth, Game1.ScreenHeight - bumperTexture.Height - Game1.TrackWidth)
                },
                new Bumper(bumperTexture, 3,bumperTexture.Width, Game1.ScreenHeight - Game1.TrackWidth * 2)
                {
                    Position = new Vector2(Game1.TrackWidth, Game1.TrackWidth)
                },
                // FinishLine
                new Finishline(finishlineTexture, 1, finishlineTexture.Width, Game1.ScreenHeight)
                {
                    Position = new Vector2(Game1.ScreenWidth - finishlineTexture.Width, Game1.ScreenHeight - Game1.TrackWidth),
                    amountCheckpoint = 2
                },
                // checkpoint's
                new Checkpoint(checkpointTexture, 1, checkpointTexture.Width, Game1.TrackWidth)
                {
                    Position = new Vector2((Game1.ScreenWidth / 2) - checkpointTexture.Width, 0),
                    checkpointId = 0
                },
                new Checkpoint(checkpointTexture, 1, checkpointTexture.Width, Game1.ScreenHeight)
                {
                    Position = new Vector2((Game1.ScreenWidth / 2) - checkpointTexture.Width, Game1.ScreenHeight - Game1.TrackWidth),
                    checkpointId = 1
                },
            };
        }

        //Het starten van het spel
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, List<Sprite> _sprites, SpriteFont _font)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.LinearWrap, null, null);

            int fontY = 50;
            // draw background
            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0),null, Color.White, 0, new Vector2(0, 0), 4, SpriteEffects.None, 0.1f);

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
                        // draw player
                        sprite.Draw(spriteBatch, 0.6f);
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
    }
}