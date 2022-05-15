using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace TopDownRacer
{
    class Player
    {
        //Declaring a variable of type Texture2D to add an image to
        Texture2D playerTexture;


        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        private int MaxPositionSpeed { get; set; } = 15;
        private float CurrentPositionSpeed { get; set; }
        private float ChangePositionSpeed { get; set; }
        private float RotationSpeed { get; set; } = 2.5f;
        private Vector2 Origin { get; set; }
        public int areaWidth { get; set; }
        public int areaHeight { get; set; }

        public Player()
        {
        }

        public void Initialize()
        {
            Position = new Vector2(Game1.ScreenWidth / 2,
                Game1.ScreenHeight / 2);
        }
        public void LoadContent(Texture2D texture)
        {
            playerTexture = texture;
            Origin = new Vector2(playerTexture.Width / 2, playerTexture.Height / 2);
        }

        public void Move()
        {
            // TODO: Add your update logic here

            //Declaring basic player controls
            var kstate = Keyboard.GetState();
            // TODO backwards driving is not mirrored
            // Rotate the car based on which key is pressed
            if (kstate.IsKeyDown(Keys.A))
                Rotation -= MathHelper.ToRadians(RotationSpeed);
            if (kstate.IsKeyDown(Keys.D))
                Rotation += MathHelper.ToRadians(RotationSpeed);
                

            var direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));

            if (kstate.IsKeyDown(Keys.W))
            {
                // if the current speed is not above the max speed accelerate the car forwards
                if (CurrentPositionSpeed < MaxPositionSpeed)
                {
                    ChangePositionSpeed += 0.15f;
                    CurrentPositionSpeed += ChangePositionSpeed;
                }
            } else if (kstate.IsKeyDown(Keys.S))
            {
                // if the current speed is not above the max speed accelerate the car backwards
                if (CurrentPositionSpeed > (0 - MaxPositionSpeed)) 
                {
                    ChangePositionSpeed += -0.15f;
                    CurrentPositionSpeed += ChangePositionSpeed;
                }
            } else
            {
                // automatic braking if no key is pressed
                if (CurrentPositionSpeed > 0.25f || CurrentPositionSpeed < -0.25f)
                {
                    ChangePositionSpeed -= CurrentPositionSpeed * 0.01f;
                } else
                {
                    CurrentPositionSpeed = 0;
                }
                CurrentPositionSpeed += ChangePositionSpeed;
            }

            // if the car is driving faster than the maxSpeed set the speed to the maxSpeed
            if (ChangePositionSpeed > MaxPositionSpeed / 20)
                ChangePositionSpeed = MaxPositionSpeed / 20;
            if (ChangePositionSpeed < -1*(MaxPositionSpeed / 20))
                ChangePositionSpeed = -1 * MaxPositionSpeed / 20;

            Position += direction * CurrentPositionSpeed;

            // limit the positions in which the car can travel
            Position = Vector2.Clamp(Position, new Vector2(playerTexture.Width / 2, playerTexture.Height / 2), new Vector2(Game1.ScreenWidth - playerTexture.Width / 2, Game1.ScreenHeight - playerTexture.Height / 2));
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(playerTexture, Position, null, Color.White, Rotation, Origin, 1, SpriteEffects.None, 0f);
        }
    }
}
