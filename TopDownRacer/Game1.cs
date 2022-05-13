using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownRacer
{
    public class Game1 : Game
    {
        //Declaring a variable of type Texture2D to add an image to
        Texture2D playerTexture;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player1 = new Player();

        public static int ScreenWidth;
        public static int ScreenHeight;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.ApplyChanges();

            // set graphics resolution to the resolution of the display
            _graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            ScreenWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            ScreenHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();

            // set the window size to fullscreen
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //Initialize the player speed and position
            player1.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Declare a texture to the variable, Ball is a temporary texture
            playerTexture = Content.Load<Texture2D>("rectangle");
            player1.LoadContent(playerTexture);
            // TODO: use this.Content to load your game content here
            player1.areaHeight = _graphics.PreferredBackBufferHeight;
            player1.areaWidth= _graphics.PreferredBackBufferWidth;
        }

        protected override void Update(GameTime gameTime)
        {
            //Press esc to close the game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            player1.Move();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //Draw the car in the game
            _spriteBatch.Begin();

            player1.Draw(_spriteBatch);

            /*_spriteBatch.Draw(playerTexture, playerPosition, null, Color.White, 0f, new Vector2(playerTexture.Width / 2, playerTexture.Height / 2),
            Vector2.One, SpriteEffects.None, 0f);*/
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
