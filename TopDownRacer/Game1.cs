using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TopDownRacer.Models;
using TopDownRacer.Sprites;
using TopDownRacer.States;

namespace TopDownRacer
{
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        public static int ScreenWidth;
        public static int ScreenHeight;
        private static int TrackWidth = 400;
        //Declaring a variable of type Texture2D to add an image to
        Texture2D playerTexture;
        Texture2D bumperTexture;
        private List<Sprite> _sprites;

        private State _currentState;

        private State _nextState;

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            // set graphics resolution to the resolution of the display
            ScreenWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            ScreenHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;

            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();

            // set the window size to fullscreen
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playerTexture = Content.Load<Texture2D>("Player/rectangle");
            bumperTexture = Content.Load<Texture2D>("Levels/tires_white");

            _sprites = new List<Sprite>()
              {
                new Player(playerTexture)
                {
                  Input = new Input()
                  {
                    Left = Keys.A,
                    Right = Keys.D,
                    Up = Keys.W,
                    Down = Keys.S,
                  },
                  Position = new Vector2(1200, 1200),
                  Color = Color.Blue,
                },
                /*new Player(playerTexture)
                {
                  Input = new Input()
                  {
                    Left = Keys.Left,
                    Right = Keys.Right,
                    Up = Keys.Up,
                    Down = Keys.Down,
                  },
                  Position = new Vector2(ScreenWidth - 100 - playerTexture.Width, 100),
                  Rotation = MathHelper.Pi,
                  Color = Color.Green,
                },*/
                // outer ring
                new Bumper(bumperTexture, 0, bumperTexture.Height,ScreenWidth)
                {
                    Position = new Vector2(0,0)
                },
                new Bumper(bumperTexture, 1, ScreenHeight, bumperTexture.Width)
                {
                    Position = new Vector2(ScreenWidth - bumperTexture.Width, 0)
                },
                new Bumper(bumperTexture, 2, bumperTexture.Height, ScreenWidth)
                {
                    Position = new Vector2(0, ScreenHeight - bumperTexture.Height)
                },
                new Bumper(bumperTexture, 3,ScreenHeight, bumperTexture.Width)
                {
                    Position = new Vector2(0, 0)
                },
                // inner ring
                
                new Bumper(bumperTexture, 0, bumperTexture.Height,ScreenWidth-TrackWidth*2)
                {
                    Position = new Vector2(TrackWidth,TrackWidth)
                },
                new Bumper(bumperTexture, 1, ScreenHeight - TrackWidth*2, bumperTexture.Width)
                {
                    Position = new Vector2(ScreenWidth - bumperTexture.Width - TrackWidth, TrackWidth)
                },
                new Bumper(bumperTexture, 2, bumperTexture.Height, ScreenWidth - TrackWidth * 2)
                {
                    Position = new Vector2(TrackWidth, ScreenHeight - bumperTexture.Height - TrackWidth)
                },
                new Bumper(bumperTexture, 3,ScreenHeight - TrackWidth * 2, bumperTexture.Width)
                {
                    Position = new Vector2(TrackWidth, TrackWidth)
                }
            };

            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non loaded content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;

                _nextState = null;
            }

            foreach (var sprite in _sprites)
                sprite.Update(gameTime, _sprites);

            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);

            //Press esc to close the game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);

            foreach (var sprite in _sprites)
                sprite.Draw(_spriteBatch);
                //_currentState.Draw(gameTime, _spriteBatch, playerTexture, player1.Position, player1.Rotation);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
