using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TopDownRacer.Controller;
using TopDownRacer.Models;
using TopDownRacer.NeuralNetwork;
using TopDownRacer.NeuralNetwork.ActivationFunctions;
using TopDownRacer.NeuralNetwork.InputFunctions;
using TopDownRacer.NeuralNetwork.Layers;
using TopDownRacer.Sprites;

namespace TopDownRacer.States
{
    internal class NeuralNetworkState : State
    {
        private bool initizalized = false;
        private RaceNeuralNetwork network;

        //constuctor van de game state
        public NeuralNetworkState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            playerTexture.Insert(0, content.Load<Texture2D>("Player/car_small_1"));
            playerTexture.Insert(1, content.Load<Texture2D>("Player/car_small_2"));
            playerTexture.Insert(2, content.Load<Texture2D>("Player/car_small_3"));
            playerTexture.Insert(3, content.Load<Texture2D>("Player/car_small_4"));
            playerTexture.Insert(4, content.Load<Texture2D>("Player/car_small_5"));

            bumperTexture = content.Load<Texture2D>("Levels/tires_white_small");
            finishlineTexture = content.Load<Texture2D>("Levels/finishline");
            checkpointTexture = content.Load<Texture2D>("Levels/checkpoint");
            backgroundTexture = content.Load<Texture2D>("Levels/background");

            var xmlMap = XmlMapReader.LoadMap("ZigZag");
            game._sprites = xmlMap.getSprites();
            Vector2 spawnpoint = xmlMap.getSpawnpoint();
            float orientation = xmlMap.getOrientation();

            var player = new Player(State.playerTexture[Game1.rnd.Next(State.playerTexture.Count)], (Game1.ScreenWidth / 4 * 3) - 20, (Game1.ScreenHeight / 2) - 130)
            {
                Name = "test",
                Input = new Input() { },
                Color = new Color(Game1.rnd.Next(0, 255), Game1.rnd.Next(0, 255), Game1.rnd.Next(0, 255)),
                MaxPositionSpeed = 3
            };
            player.Position = spawnpoint;
            player.Rotation = orientation;
            game._sprites.Add(player);
        }

        //Het starten van het spel
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, List<Sprite> _sprites, SpriteFont _font)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.LinearWrap, null, null);

            int fontY = 50;
            // draw background
            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), null, Color.White, 0, new Vector2(0, 0), 4, SpriteEffects.None, 0.1f);

            foreach (Sprite sprite in _sprites)
            {
                if (sprite is Player)
                {
                    var ScoreBoardPosition = new Vector2(70, fontY += 20);
                    // draw scoarboard background
                    spriteBatch.Draw(bumperTexture, ScoreBoardPosition, null, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0.2f);
                    // draw text on scoarboard
                    spriteBatch.DrawString(_font, string.Format("Player {0}: {1}", ((Player)sprite).Name, ((Player)sprite).Score), ScoreBoardPosition, ((Player)sprite).Color, 0, Vector2.Zero, 1, SpriteEffects.None, 0.4f);
                    if (!((Player)sprite).Dead)
                    {
                        // draw player
                        sprite.Draw(spriteBatch, 0.6f);
                        //draw closet barrier front indication
                        //spriteBatch.DrawString(_font, "front\nfront\nfront", findClosestBarrierDirectional(sprite), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0.4f);
                        //spriteBatch.DrawString(_font, "right\nright\nright", findClosestBarrierDirectional(sprite, 90), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0.4f);
                        //spriteBatch.DrawString(_font, "back\nback\nback", findClosestBarrierDirectional(sprite, 180), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0.4f);
                        //spriteBatch.DrawString(_font, "left\nleft\nleft", findClosestBarrierDirectional(sprite, 270), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0.4f);
                    }
                }
                else
                {
                    // draw walls checkpoints and finish line
                    sprite.Draw(spriteBatch, 0.3f);
                }
            }
            // draw time
            spriteBatch.DrawString(_font, string.Format("Time {0}: ", gameTime.TotalGameTime), new Vector2((Game1.ScreenWidth / 2) - 150, 10), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.9f);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //kan later worden gebruikt
        }

        public override void Update(GameTime gameTime)
        {
            if (!initizalized)
            {
                //implementatie van Neural network moet hier komen
                //inputs in the order of: speed, left, front-left, front, front-right, right, back
                var network = new RaceNeuralNetwork(6);

                //Factory voor het maken van de layers
                var layerFactory = new NeuralLayerFactory();

                //Toevoegen van twee nieuwe layers aan het neural network
                network.AddLayer(layerFactory.CreateNeuralLayer(6, new RectifiedActivationFuncion(), new WeightedSumFunction()));
                network.AddLayer(layerFactory.CreateNeuralLayer(6, new StepActivationFunction(0.7), new WeightedSumFunction()));
                network.AddLayer(layerFactory.CreateNeuralLayer(4, new SigmoidActivationFunction(0.7), new WeightedSumFunction()));

                //hier moeten de expected values komen te staan
                network.PushExpectedValues(
                    new double[][] {
                    //in the form of front, back, left, right
                    new double[] { 1, 0, 1, 0} , //front left
                    new double[] { 0, 1, 0, 1 }, //back right
                    new double[] { 1, 0, 0, 1 }, //front right
                    new double[] { 1, 0, 0, 1 }, //front right
                    new double[] { 0, 1, 1, 0 }, //back left
                    new double[] { 0, 1, 0, 0 }, //back
                    new double[] { 1, 0, 0, 0 }, //front
                    new double[] { 1, 0, 1, 0} , //front left
                    new double[] { 0, 1, 0, 1 }, //back right
                    new double[] { 1, 0, 0, 1 }, //front right
                    new double[] { 0, 1, 1, 0 }, //back left
                    new double[] { 0, 1, 0, 0 }, //back
                    new double[] { 1, 0, 0, 0 }, //front
                    new double[] { 1, 0, 0, 0 }, //front
                    new double[] { 0, 1, 0, 0 }, //back
                    new double[] { 0, 1, 0, 0 }, //back
                    new double[] { 1, 0, 0, 0 }, //front
                    new double[] { 1, 0, 0, 1 }, //front right
                    });

                //hier moet de train methode komen
                network.Train(
                    new double[][] {
                    //inputs in the order of: left, front-left, front, front-right, right, back
                    new double[] {0, 0, 0, 1, 1, 1 },
                    new double[] {1, 1, 1, 0, 0, 0 },
                    new double[] {1, 1, 0, 0, 0, 1 },
                    new double[] {1, 1, 0, 0, 0, 0 },
                    new double[] {0, 0, 1, 1, 1, 0 },
                    new double[] {1, 1, 1, 1, 1, 0 },
                    new double[] {1, 1, 0, 1, 1, 1 },
                    new double[] {0, 0, 0, 1, 1, 1 },
                    new double[] {1, 1, 1, 0, 0, 0 },
                    new double[] {1, 1, 0, 0, 0, 1 },
                    new double[] {0, 0, 1, 1, 1, 0 },
                    new double[] {1, 1, 1, 1, 1, 0 },
                    new double[] {1, 1, 0, 1, 1, 1 },
                    new double[] {0, 0, 0, 0, 0, 0 },
                    new double[] {0, 1, 1, 1, 0, 0 },
                    new double[] {0, 0, 1, 0, 0, 0 },
                    new double[] {0, 0, 0, 0, 0, 0 },
                    new double[] {0, 0, 0, 0, 1, 0 },
                    }, 100);

                this.network = network;
                initizalized = true;
            }

            foreach (Sprite sprite in _game._sprites)
            {
                if (sprite is Player)
                {
                    if (!((Player)sprite).Dead)
                    {
                        double[] input = new double[] { findClosestBarrierDirectionalBoolean(sprite, 270), findClosestBarrierDirectionalBoolean(sprite, 315), findClosestBarrierDirectionalBoolean(sprite, 0), findClosestBarrierDirectionalBoolean(sprite, 45), findClosestBarrierDirectionalBoolean(sprite, 90), findClosestBarrierDirectionalBoolean(sprite, 180) };
                        this.network.PushInputValues(input);
                        var outputs = this.network.GetOutput();
                        if (outputs[0] >= 0.99)
                        {
                            ((Player)sprite).DriveForward();
                        }
                        else if (outputs[1] >= 0.99)
                        {
                            ((Player)sprite).DriveBackwards();
                        }
                        if (outputs[2] >= outputs[3])
                        {
                            ((Player)sprite).TurnLeft();
                        }
                        else
                        {
                            ((Player)sprite).TurnRight();
                        }
                        //check outputs ---- checkpoint 14-6-2022
                        string testValues = "";
                        foreach (double index in input)
                        {
                            testValues += "/ " + index;
                        }
                        testValues += ", ";
                        foreach (double index in outputs)
                        {
                            testValues += "/ " + index;
                        }
                        Debug.WriteLine(testValues);
                    }
                }
                sprite.Update(gameTime, _game._sprites);
            }
        }

        private int findClosestBarrierDirectionalBoolean(Sprite sprite, int rotation = 0, int distance = 5)
        {
            int count = 0;
            while (count <= distance)
            {
                count++;

                double y = sprite.Position.Y + ((count * bumperTexture.Width / 2) * Math.Sin((sprite.Rotation + MathHelper.ToRadians(rotation))));

                double x = sprite.Position.X + ((count * bumperTexture.Width / 2) * Math.Cos((sprite.Rotation + MathHelper.ToRadians(rotation))));

                foreach (var sprite2 in _game._sprites)
                {
                    if (sprite2 is Bumper)
                    {
                        if (y < sprite2.Position.Y + sprite2.height && y > sprite2.Position.Y && x < sprite2.Position.X + sprite2.width && x > sprite2.Position.X)
                        {
                            return 1;
                        }
                    }
                }
            }

            return 0;
        }

        private Vector2 findClosestBarrierDirectional(Sprite sprite, int rotation = 0)
        {
            int count = 0;

            while (count < 1920 / 28)
            {
                count++;

                double y = sprite.Position.Y + ((count * bumperTexture.Width / 2) * Math.Sin((sprite.Rotation + MathHelper.ToRadians(rotation))));

                double x = sprite.Position.X + ((count * bumperTexture.Width / 2) * Math.Cos((sprite.Rotation + MathHelper.ToRadians(rotation))));

                foreach (var sprite2 in _game._sprites)
                {
                    if (sprite2 is Bumper)
                    {
                        if (y < sprite2.Position.Y + sprite2.height && y > sprite2.Position.Y && x < sprite2.Position.X + sprite2.width && x > sprite2.Position.X)
                        {
                            return new Vector2((float)x, (float)y);
                        }
                    }
                }
            }

            return new Vector2(0, 0);
        }

        private Double getDistanceToCoordinates(Vector2 pos1, Vector2 pos2)
        {
            return Math.Sqrt(Math.Pow(pos1.X - pos2.X, 2) + Math.Pow(pos1.Y - pos2.Y, 2));
        }
    }
}