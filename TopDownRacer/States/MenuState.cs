﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TopDownRacer.MenuControls;
using TopDownRacer.Sprites;

namespace TopDownRacer.States
{
    //MenuState inherit van de abstracte State klasse
    public class MenuState : State
    {
        private readonly List<Component> _components;

        //constuctor van de MenuState
        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            //Laden van de font en button png
            Texture2D buttonTexture = _content.Load<Texture2D>("Controls/Button");
            SpriteFont buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            //Toevoegen van nieuwe buttons en functionaliteiten van de buttons
            Button singlePlayerButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((Game1.ScreenWidth / 2) - 100, (Game1.ScreenHeight / 2) - 150),
                Text = "Single Player",
            };

            singlePlayerButton.Click += NewGameButton_Click;

            Button multiplayerButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((Game1.ScreenWidth / 2) - 100, (Game1.ScreenHeight / 2) - 100),
                Text = "multiplayer",
            };

            multiplayerButton.Click += MultiplayerButton_Click;

            Button AiTrainingButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((Game1.ScreenWidth / 2) - 100, (Game1.ScreenHeight / 2) - 50),
                Text = "neral network",
            };

            AiTrainingButton.Click += AiTrainingButton_Click;

            Button quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((Game1.ScreenWidth /2) - 100, (Game1.ScreenHeight / 2) - 0),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()

            {
                singlePlayerButton,
                multiplayerButton,
                AiTrainingButton,
                quitGameButton,
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
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new SinglePlayerState(_game, _graphicsDevice, _content));
        }
        private void MultiplayerButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MultiplayerState(_game, _graphicsDevice, _content));
        }
        private void AiTrainingButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new NeralNetworkState(_game, _graphicsDevice, _content));
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

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}