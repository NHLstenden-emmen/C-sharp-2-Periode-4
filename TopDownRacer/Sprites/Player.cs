using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TopDownRacer.Sprites
{
    public class Player : Sprite
    {
        private int MaxPositionSpeed { get; set; } = 15;
        private float CurrentPositionSpeed { get; set; }
        private float ChangePositionSpeed { get; set; }
        private float RotationSpeed { get; set; } = 2.5f;
        public int areaWidth { get; set; }
        public int areaHeight { get; set; }

        public Player(Texture2D texture)
        : base(texture)
        {
        }

        public void Initialize()
        {
            Position = new Vector2(Game1.ScreenWidth / 2,
                Game1.ScreenHeight / 2);
        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();
        }

        public void Move()
        {
            //Declaring basic player controls
            var kstate = Keyboard.GetState();
            // TODO backwards driving is not mirrored
            // Rotate the car based on which key is pressed
            if (kstate.IsKeyDown(Input.Left))
                Rotation -= MathHelper.ToRadians(RotationSpeed);
            if (kstate.IsKeyDown(Input.Right))
                Rotation += MathHelper.ToRadians(RotationSpeed);


            var direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));

            if (kstate.IsKeyDown(Input.Up))
            {
                // if the current speed is not above the max speed accelerate the car forwards
                if (CurrentPositionSpeed < MaxPositionSpeed)
                {
                    ChangePositionSpeed += 0.15f;
                    CurrentPositionSpeed += ChangePositionSpeed;
                }
            }
            else if (kstate.IsKeyDown(Input.Down))
            {
                // if the current speed is not above the max speed accelerate the car backwards
                if (CurrentPositionSpeed > (0 - MaxPositionSpeed))
                {
                    ChangePositionSpeed += -0.15f;
                    CurrentPositionSpeed += ChangePositionSpeed;
                }
            }
            else
            {
                // automatic braking if no key is pressed
                if (CurrentPositionSpeed > 0.25f || CurrentPositionSpeed < -0.25f)
                {
                    ChangePositionSpeed -= CurrentPositionSpeed * 0.01f;
                }
                else
                {
                    CurrentPositionSpeed = 0;
                }
                CurrentPositionSpeed += ChangePositionSpeed;
            }

            // if the car is driving faster than the maxSpeed set the speed to the maxSpeed
            if (ChangePositionSpeed > MaxPositionSpeed / 20)
                ChangePositionSpeed = MaxPositionSpeed / 20;
            if (ChangePositionSpeed < -1 * (MaxPositionSpeed / 20))
                ChangePositionSpeed = -1 * MaxPositionSpeed / 20;

            Position += direction * CurrentPositionSpeed;

            // limit the positions in which the car can travel
            Position = Vector2.Clamp(Position, new Vector2(_texture.Width / 2, _texture.Height / 2), new Vector2(Game1.ScreenWidth - _texture.Width / 2, Game1.ScreenHeight - _texture.Height / 2));
        }
    }
}
