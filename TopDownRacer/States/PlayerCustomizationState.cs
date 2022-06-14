using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TopDownRacer.MenuControls;
using TopDownRacer.Models;
using TopDownRacer.Sprites;

namespace TopDownRacer.States
{
    internal class PlayerCustomizationState : State
    {
        private readonly List<Component> _components;
        private String gameMode;
        private String MapFileName;
        private List<Player> players;
        private SoundEffectInstance backgroundMusic;

        //constuctor van de MenuState
        public PlayerCustomizationState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, String GameMode)
          : base(game, graphicsDevice, content)
        {
            //Laden van de player textures
            playerTexture.Insert(0, content.Load<Texture2D>("Player/car_small_1"));
            playerTexture.Insert(1, content.Load<Texture2D>("Player/car_small_2"));
            playerTexture.Insert(2, content.Load<Texture2D>("Player/car_small_3"));
            playerTexture.Insert(3, content.Load<Texture2D>("Player/car_small_4"));
            playerTexture.Insert(4, content.Load<Texture2D>("Player/car_small_5"));

            //Laad de muziek in
            backgroundMusic = Game1._soundEffects[0].CreateInstance();
            backgroundMusic.Volume = 0.4f;
            backgroundMusic.IsLooped = true;
            backgroundMusic.Play();

            //Laden van de font en button png
            Texture2D buttonTexture = _content.Load<Texture2D>("Controls/Button");
            SpriteFont buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            backgroundTexture = content.Load<Texture2D>("Levels/background");

            gameMode = GameMode;
            //Toevoegen van nieuwe buttons en functionaliteiten van de buttons
            Button StartGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((Game1.ScreenWidth / 2) - 100, (Game1.ScreenHeight / 4)),
                Text = "Start Game",
                Disabled = true
            };

            _components = new List<Component>()
            {
                StartGameButton,
            };

            StartGameButton.Click += StartGameButton_Click;
            if(gameMode == "Multiplayer")
            {
                Button AddPlayerButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2((Game1.ScreenWidth / 4 * 3) - 100, (Game1.ScreenHeight / 4)),
                    Text = "add player",
                };

                AddPlayerButton.Click += AddPlayerButton_Click;

                _components.Add(AddPlayerButton);
            }
            
            players = new List<Player>(){new Player(State.playerTexture[Game1.rnd.Next(State.playerTexture.Count)], (Game1.ScreenWidth / 4 * 3) - 20, (Game1.ScreenHeight / 2) - 130)
                {
                    Name = "test",
                    Input = new Input(){ },
                    Color = new Color(Game1.rnd.Next(0, 255), Game1.rnd.Next(0, 255), Game1.rnd.Next(0, 255)),
                }
            };

            List<String> maps = new List<string>
            {
                "Nascar","L-shape","ZigZag"
            };
            for (int i = 0; i < maps.Count; i++)
            {
                Button MapButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2((Game1.ScreenWidth / 2) - 100, (Game1.ScreenHeight / 2) - (150 - (50 * i))),
                    Text = maps[i],
                };
                MapButton.Click += MapButton_Click;
                _components.Add(MapButton);
            }
        }

        //Het maken van de buttons op basis van de buttons die aan de component list is toegevoegd
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, List<Sprite> _sprites, SpriteFont _font)
        {
            spriteBatch.Begin();

            // draw background
            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), null, Color.White, 0, new Vector2(0, 0), 4, SpriteEffects.None, 0.1f);

            foreach (Component component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            foreach (Player player in players)
            {
                player.Draw(spriteBatch, 0.6f);
            }

            spriteBatch.End();
        }

        //De click om een nieuw spel te starten door state te veranderen
        private void StartGameButton_Click(object sender, EventArgs e)
        {
            // Check of er een map geselecteerd is
            if (MapFileName == null)
                return;
            if (gameMode == "Multiplayer") 
                _game.ChangeState(new MultiplayerState(_game, _graphicsDevice, _content, MapFileName, players));
            if (gameMode == "Single Player")
                _game.ChangeState(new SinglePlayerState(_game, _graphicsDevice, _content, MapFileName, players[0]));
        }

        //De click om een Speler toe te voegen
        private void AddPlayerButton_Click(object sender, EventArgs e)
        {
            Game1._soundEffects[1].Play();
            if (players.Count >= 4)
                return;
                
            players.Add(new Player(State.playerTexture[Game1.rnd.Next(State.playerTexture.Count)], (Game1.ScreenWidth / 4 * 3) - 20, (Game1.ScreenHeight / 2) - (130 - (50 * players.Count)), players.Count)
            {
                Name = "test",
                Input = new Input() { },
                Color = new Color(Game1.rnd.Next(0, 255), Game1.rnd.Next(0, 255), Game1.rnd.Next(0, 255)),

            });
            if (players.Count >= 4)
                ((Button)sender).Disabled = true;

        }

        //De click om een map te selecteren
        private void MapButton_Click(object sender, EventArgs e)
        {
            MapFileName = ((Button)sender).Text;
            foreach (Component component in _components)
            {
                if (component is Button)
                {
                    ((Button)component).Active = false;
                    ((Button)component).Disabled = false;
                }
            }
            ((Button)sender).Active = true;
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