﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TopDownRacer.Sprites
{
    public class Player : Sprite
    {
        public String Name = "kevin";
        public int Score;
        public int checkpointId = 0;
        public Boolean Dead = false;
        private int MaxPositionSpeed { get; set; } = 15;
        private float ChangePositionSpeed { get; set; }

        private float RotationSpeed { get; set; } = 0f;
        private float MaxRotationSpeed { get; set; } = 2.5f;

        public Player(Texture2D texture, int x, int y)
        : base(texture)
        {
            Position = new Vector2(x, y);
        }

        public void Initialize()
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();

            if (!Dead)
                if (CurrentPositionSpeed > 10.0)
                    Score++;
        }

        public void Move()
        {
            //Declaring basic player controls
            KeyboardState kstate = Keyboard.GetState();
            // Rotate the car based on which key is pressed
            if (kstate.IsKeyDown(Input.Left))
            {
                if (RotationSpeed < MaxRotationSpeed)
                    RotationSpeed = CurrentPositionSpeed / (MaxPositionSpeed / 2);
                if (CurrentPositionSpeed > 0.0f)
                    Rotation -= MathHelper.ToRadians(RotationSpeed);
                if (CurrentPositionSpeed < 0.0f)
                    Rotation += MathHelper.ToRadians(RotationSpeed);
            }

            if (kstate.IsKeyDown(Input.Right))
            {
                if (RotationSpeed < MaxRotationSpeed)
                    RotationSpeed = CurrentPositionSpeed / (MaxPositionSpeed / 2);
                if (CurrentPositionSpeed > 0.0f)
                    Rotation += MathHelper.ToRadians(RotationSpeed);
                if (CurrentPositionSpeed < 0.0f)
                    Rotation -= MathHelper.ToRadians(RotationSpeed);
            }

            Vector2 direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));

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
            // if rotation speed is to high set it to the max rotation speed
            if (RotationSpeed > MaxRotationSpeed)
                RotationSpeed = MaxRotationSpeed;

            Position += direction * CurrentPositionSpeed;

            // limit the positions in which the car can travel
            Position = Vector2.Clamp(Position, new Vector2(_texture.Width / 2, _texture.Height / 2), new Vector2(Game1.ScreenWidth - _texture.Width / 2, Game1.ScreenHeight - _texture.Height / 2));
        }
    }
}