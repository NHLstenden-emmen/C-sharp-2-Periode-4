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
        //Declaring a variable of type Texture2D to add an image to
        Texture2D playerTexture;
        private List<Sprite> _sprites;

        private State _currentState;

        private SpriteFont _font;

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

            _sprites = new List<Sprite>()
              {
                new Player(playerTexture)
                {
                  Input = new Input()
                  {},
                  Position = new Vector2(100, 100),
                  Color = Color.Blue,
                },
                new Player(playerTexture)
                {
                  Input = new Input()
                  {
                    Left = Keys.Left,
                    Right = Keys.Right,
                    Up = Keys.Up,
                    Down = Keys.Down,
                  },
                  Position = new Vector2(ScreenWidth - 100 - playerTexture.Width, 100),
                  Color = Color.Green,
                },
            };

            _font = Content.Load<SpriteFont>("Fonts/Font");
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

            _spriteBatch.Begin();

            foreach (var sprite in _sprites)
                sprite.Draw(_spriteBatch);

            var fontY = 10;
            var i = 0;

            foreach (var sprite in _sprites)
            {
                if (sprite is Player)
                    _spriteBatch.DrawString(_font, string.Format("Player {0}: {1}", ++i, ((Player)sprite).Score), new Vector2(10, fontY += 20), Color.Black);
            }
            _spriteBatch.DrawString(_font, string.Format("Time {0}: ", gameTime.TotalGameTime), new Vector2((ScreenWidth/2) - 150, 10), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
