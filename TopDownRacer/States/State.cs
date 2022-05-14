using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TopDownRacer.States
{
    public abstract class State
    {
        //Fields

        protected ContentManager _content;

        protected GraphicsDevice _graphicsDevice;

        protected Game1 _game;

        //Methods

        //Basis methode voor de draw van de game/sprite
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D playerTexture, Vector2 playerPosition);

        //kan worden gebruikt om components te verwijderen
        public abstract void PostUpdate(GameTime gameTime);

        //De basis constructor voor de game state
        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _game = game;

            _graphicsDevice = graphicsDevice;

            _content = content;
        }

        public abstract void Update(GameTime gameTime);
    }
}
