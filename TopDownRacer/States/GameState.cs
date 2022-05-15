using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TopDownRacer.States
{
    public class GameState : State
    {
        //constuctor van de game state
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {

        }

        //Het starten van het spel
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D playerTexture, Vector2 playerPosition, float playerRotation = 0f)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(playerTexture, playerPosition, null, Color.White, playerRotation, new Vector2(playerTexture.Width / 2, playerTexture.Height / 2),
            Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //kan later worden gebruikt
        }

        public override void Update(GameTime gameTime)
        {
            //kan later worden gebruikt
        }
    }
}
