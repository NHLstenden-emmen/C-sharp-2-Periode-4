using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TopDownRacer.MenuControls;
using TopDownRacer.Sprites;

namespace TopDownRacer.States
{
    internal class PlayerCustomizationState : State
    {
        private readonly List<Component> _components;
        private String gameMode;

        //constuctor van de MenuState
        public PlayerCustomizationState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, String GameMode)
          : base(game, graphicsDevice, content)
        {
            //Laden van de font en button png
            Texture2D buttonTexture = _content.Load<Texture2D>("Controls/Button");
            SpriteFont buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            gameMode = GameMode;
            //Toevoegen van nieuwe buttons en functionaliteiten van de buttons
            Button StartGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((Game1.ScreenWidth / 2) - 100, (Game1.ScreenHeight / 4)),
                Text = "Start Game",
            };

            StartGameButton.Click += StartGameButton_Click;
            _components = new List<Component>()
            {
                StartGameButton,
            };
        }

        //Het maken van de buttons op basis van de buttons die aan de component list is toegevoegd
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, List<Sprite> _sprites, SpriteFont _font)
        {
            spriteBatch.Begin();

            foreach (Component component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }

        //De click om een nieuw spel te starten door state te veranderen
        private void StartGameButton_Click(object sender, EventArgs e)
        {
            if (gameMode == "Multiplayer")
                _game.ChangeState(new SinglePlayerState(_game, _graphicsDevice, _content));
            if (gameMode == "Single Player")
                _game.ChangeState(new MultiplayerState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // verwijder sprites indien ze niet nodig zijn
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Component component in _components)
            {
                component.Update(gameTime);
            }
        }
    }
}