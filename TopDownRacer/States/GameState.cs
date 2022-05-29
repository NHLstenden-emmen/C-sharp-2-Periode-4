using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using TopDownRacer.Models;
using TopDownRacer.Sprites;

namespace TopDownRacer.States
{
    public class GameState : State
    {
        //Added extra list so we can extract sprite location seperate from the constructor
        List<Sprite> gameSprites = new List<Sprite>(); 
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
                /*new Player(playerTexture[game.rnd.Next(playerTexture.Count)])
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
                },*/
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

            //Added timer so we only get location every X seconds
            System.Timers.Timer timer = new System.Timers.Timer(1000); // 1 seconds
            timer.Elapsed += new ElapsedEventHandler(OnTimerElapsed);

            timer.Interval = 1000;
            timer.Enabled = true;
        

        gameSprites = game._sprites;
        }

        //Het starten van het spel
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, List<Sprite> _sprites, SpriteFont _font)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);

            int fontY = 50;

            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);

            foreach (Sprite sprite in _sprites)
            {
                if (sprite is Player)
                {
                    if (!((Player)sprite).Dead)
                        sprite.Draw(spriteBatch);
                    spriteBatch.DrawString(_font, string.Format("YeetYeetYeetYeet"), findClosestBarrierFront(sprite), Color.Red);
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
            Console.WriteLine("test");
        }

        public override void Update(GameTime gameTime)
        {

            foreach (Sprite sprite in _game._sprites)
            {
                sprite.Update(gameTime, _game._sprites);
            }
        }

        //timer that fires once per second
        private void OnTimerElapsed(object source, ElapsedEventArgs e)
        {
            Vector2 saveData = new Vector2(0, 0);
            foreach (Sprite sprite in _game._sprites)
            {
                if (sprite is Player)
                {
                    //Debug.WriteLine(((Player)sprite).Name + " - " + sprite.Position.X + " - " + sprite.Position.Y + " - " + MathHelper.ToDegrees(sprite.Rotation) % 360);
                    //Debug.WriteLine(bumperTexture.Height + " - " + (Game1.ScreenWidth - Game1.TrackWidth * 2) + " - " + bumperTexture.Width);

                    //saveData = findClosestBarrierFront(sprite);
                    //Debug.WriteLine(((Player)sprite).Name + " - " + saveData.X + " - " + saveData.Y);
                }
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

            return new Vector2(0,0);
        }


        /*  public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite is Player)
                {
                    // the car is moving right
                    var coords = getCornerCoordsDel(sprite);
                    if (coords.Y < this.Position.Y + height && coords.Y > this.Position.Y && coords.X < this.Position.X + width && coords.X > this.Position.X)
                    {
                        ((Player)sprite).Dead = true;
                    }
                }
            }
        } */

        /*
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
         
         */
    }
}