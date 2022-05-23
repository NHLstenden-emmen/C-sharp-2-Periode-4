using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TopDownRacer.Sprites;
using TopDownRacer.States;

namespace TopDownRacer
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int ScreenWidth = 1920;
        public static int ScreenHeight = 1080;
        public static int TrackWidth = 400;

        //Declaring a variable of type Texture2D to add an image to
        public List<Sprite> _sprites;

        public Random rnd = new Random();

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

            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

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

            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);

            //Press esc to close the game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);
            _currentState.Draw(gameTime, _spriteBatch, _sprites, _font);

            base.Draw(gameTime);
        }
    }
}