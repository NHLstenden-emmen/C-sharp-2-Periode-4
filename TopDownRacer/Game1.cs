using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TopDownRacer.Controller;
using TopDownRacer.Models;
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

        public Random rnd { get; } = new Random();

        private State _currentState;

        private SpriteFont _font;

        private State _nextState;

        // TODO: naar gamestate
        public static List<Texture2D> playerTexture = new List<Texture2D>();
        public static Texture2D backgroundTexture, checkpointTexture, bumperTexture, finishlineTexture;

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

            // TODO: moet naar gamestate:

            playerTexture.Insert(0, Content.Load<Texture2D>("Player/car_small_1"));
            playerTexture.Insert(1, Content.Load<Texture2D>("Player/car_small_2"));
            playerTexture.Insert(2, Content.Load<Texture2D>("Player/car_small_3"));
            playerTexture.Insert(3, Content.Load<Texture2D>("Player/car_small_4"));
            playerTexture.Insert(4, Content.Load<Texture2D>("Player/car_small_5"));

            bumperTexture = Content.Load<Texture2D>("Levels/tires_white");
            finishlineTexture = Content.Load<Texture2D>("Levels/finishline");
            checkpointTexture = Content.Load<Texture2D>("Levels/checkpoint");
            backgroundTexture = Content.Load<Texture2D>("Levels/background");

            var xmlMap = XmlMapReader.LoadMap("XMLFil1");
            _sprites = xmlMap.getSprites();
            // tot hier

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
            _currentState.Draw(gameTime, _spriteBatch, _sprites, _font);

            base.Draw(gameTime);
        }
    }
}