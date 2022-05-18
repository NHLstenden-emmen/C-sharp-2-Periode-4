﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public static int ScreenWidth;
        public static int ScreenHeight;

        //Declaring a variable of type Texture2D to add an image to
        public static Texture2D playerTexture;
        public static Texture2D checkpointTexture;
        public static Texture2D bumperTexture;
        public static Texture2D finishlineTexture;
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

            playerTexture = Content.Load<Texture2D>("Player/car_blue_small");
            bumperTexture = Content.Load<Texture2D>("Levels/tires_white_small");
            finishlineTexture = Content.Load<Texture2D>("Levels/finishline");
            checkpointTexture = Content.Load<Texture2D>("Levels/checkpoint");

            
            var xmlMap = XmlMapReader.LoadMap("XMLFil1");
            _sprites = xmlMap.getSprites();
            /*_sprites = new List<Sprite>()
            {
                new Player(playerTexture)
                {
                  Name = "Simchaja",
                  Input = new Input()
                  {},
                  Color = Color.Blue,
                },
                new Player(playerTexture)
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
                new Bumper(bumperTexture, 0, ScreenWidth,bumperTexture.Height)
                {
                    Position = new Vector2(0,0)
                },
                new Bumper(bumperTexture, 1, bumperTexture.Width, ScreenHeight - TrackWidth)
                {
                    Position = new Vector2(ScreenWidth - bumperTexture.Width, 0)
                },
                new Bumper(bumperTexture, 2, ScreenWidth, bumperTexture.Height)
                {
                    Position = new Vector2(0, ScreenHeight - bumperTexture.Height)
                },
                new Bumper(bumperTexture, 3,bumperTexture.Width, ScreenHeight)
                {
                    Position = new Vector2(0, 0)
                },
                // inner ring

                new Bumper(bumperTexture, 0, ScreenWidth-TrackWidth*2, bumperTexture.Height)
                {
                    Position = new Vector2(TrackWidth,TrackWidth)
                },
                new Bumper(bumperTexture, 1, bumperTexture.Width, ScreenHeight - TrackWidth*2)
                {
                    Position = new Vector2(ScreenWidth - bumperTexture.Width - TrackWidth, TrackWidth)
                },
                new Bumper(bumperTexture, 2, ScreenWidth - TrackWidth * 2, bumperTexture.Height)
                {
                    Position = new Vector2(TrackWidth, ScreenHeight - bumperTexture.Height - TrackWidth)
                },
                new Bumper(bumperTexture, 3,bumperTexture.Width, ScreenHeight - TrackWidth * 2)
                {
                    Position = new Vector2(TrackWidth, TrackWidth)
                },
                // FinishLine
                new Finishline(finishlineTexture, 1, finishlineTexture.Width, ScreenHeight)
                {
                    Position = new Vector2(ScreenWidth - finishlineTexture.Width, ScreenHeight - TrackWidth),
                    amountCheckpoint = 2
                },
                // checkpoint's
                new Checkpoint(checkpointTexture, 1, checkpointTexture.Width, TrackWidth)
                {
                    Position = new Vector2((ScreenWidth / 2) - checkpointTexture.Width, 0),
                    checkpointId = 0
                },
                new Checkpoint(checkpointTexture, 1, checkpointTexture.Width, ScreenHeight)
                {
                    Position = new Vector2((ScreenWidth / 2) - checkpointTexture.Width, ScreenHeight - TrackWidth),
                    checkpointId = 1
                },
            };*/

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

            foreach (Sprite sprite in _sprites)
            {
                sprite.Update(gameTime, _sprites);
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _currentState.Draw(gameTime, _spriteBatch, _sprites, _font);

            base.Draw(gameTime);
        }
    }
}