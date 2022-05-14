using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TopDownRacer.States;

namespace TopDownRacer
{
    public class Game1 : Game
    {
        //Declaring a variable of type Texture2D to add an image to
        Texture2D playerTexture;

        //Declaring a vector and variable to track player position and player speed
        public Vector2 playerPosition;
        public float playerSpeed;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

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
            IsMouseVisible = true;
            _graphics.ApplyChanges();
            _graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //Initialize the player speed and position
            playerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                _graphics.PreferredBackBufferHeight / 2);
            playerSpeed = 200f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Declare a texture to the variable, Ball is a temporary texture
            playerTexture = Content.Load<Texture2D>("ball");

            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);
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
                Exit();

            // TODO: Add your update logic here

            //Declaring basic player controls
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.W))
                playerPosition.Y -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.S))
                playerPosition.Y += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.A))
                playerPosition.X -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.D))
                playerPosition.X += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;



            //Declaring basic physics so the player does not go out of bounds
            if (playerPosition.X > _graphics.PreferredBackBufferWidth - playerTexture.Width / 2)
                playerPosition.X = _graphics.PreferredBackBufferWidth - playerTexture.Width / 2;
            else if (playerPosition.X < playerTexture.Width / 2)
                playerPosition.X = playerTexture.Width / 2;

            if (playerPosition.Y > _graphics.PreferredBackBufferHeight - playerTexture.Height / 2)
                playerPosition.Y = _graphics.PreferredBackBufferHeight - playerTexture.Height / 2;
            else if (playerPosition.Y < playerTexture.Height / 2)
                playerPosition.Y = playerTexture.Height / 2;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentState.Draw(gameTime, _spriteBatch, playerTexture, playerPosition);

            base.Draw(gameTime);
        }
    }
}
