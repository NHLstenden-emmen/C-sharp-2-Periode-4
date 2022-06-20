using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TopDownRacer.Sprites
{
    public class Player : Sprite
    {
        public String Name = "kevin";
        public int Score;
        public int checkpointId = 0;
        public Boolean Dead = false;
        public int MaxPositionSpeed { get; set; } = 15;
        private float ChangePositionSpeed { get; set; }
        private float RotationSpeed { get; set; } = 0f;
        private float MaxRotationSpeed { get; set; } = 2.5f;
        private int playerNumber;
        private SoundEffectInstance engineSound;

        public Player(Texture2D texture, int x, int y, int playerNumber = 0)
        : base(texture)
        {
            Debug.WriteLine(playerNumber);
            Position = new Vector2(x, y);
            if (playerNumber < 0 || playerNumber > 3)
            {
                this.playerNumber = 0;
            }
            else
            {
                this.playerNumber = playerNumber;
            }

            //Laad de soundEffects in
            engineSound = Game1._soundEffects[1].CreateInstance();
            engineSound.Volume = 0.0f;
            engineSound.IsLooped = true;
            engineSound.Play();
        }

        public void Initialize()
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();

            if (!Dead)
            {
                if (CurrentPositionSpeed > (MaxPositionSpeed / 2))
                    Score++;
            }
            else
            {
                engineSound.Volume = 0f;
            }
        }

        public void Move()
        {
            //Declaring basic player controls
            KeyboardState kstate = Keyboard.GetState();
            // Rotate the car based on which key is pressed
            if (kstate.IsKeyDown(Input.Left[playerNumber]))
            {
                TurnLeft();
            }

            if (kstate.IsKeyDown(Input.Right[playerNumber]))
            {
                TurnRight();
            }

            Vector2 direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));

            if (kstate.IsKeyDown(Input.Up[playerNumber]))
            {
                DriveForward();
            }
            else if (kstate.IsKeyDown(Input.Down[playerNumber]))
            {
                DriveBackwards();
            }
            else
            {
                // verlaag het volume van de motor als er geen gas wordt gegeven
                if (engineSound.Volume > 0.01f)
                {
                    engineSound.Volume -= 0.01f;
                }
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

        public void DriveBackwards()
        {
            // if the current speed is not above the max speed accelerate the car backwards
            if (CurrentPositionSpeed > (0.15f - MaxPositionSpeed))
            {
                ChangePositionSpeed += -0.15f;
                CurrentPositionSpeed += ChangePositionSpeed;
            }
        }

        public void DriveForward()
        {
            // if the current speed is not above the max speed accelerate the car forwards
            if (CurrentPositionSpeed < MaxPositionSpeed - 0.15f)
            {
                engineSound.Volume = Math.Abs(CurrentPositionSpeed) / 15f;
                ChangePositionSpeed += 0.15f;
                CurrentPositionSpeed += ChangePositionSpeed;
            }
        }

        public void TurnRight()
        {
            if (CurrentPositionSpeed > -MaxRotationSpeed && CurrentPositionSpeed < MaxRotationSpeed)
                RotationSpeed = 0;
            else if ((CurrentPositionSpeed <= -MaxRotationSpeed && CurrentPositionSpeed > (-MaxRotationSpeed * 3)) || (CurrentPositionSpeed >= MaxRotationSpeed && CurrentPositionSpeed < (MaxRotationSpeed * 3)))
                RotationSpeed = MaxRotationSpeed;
            else
                RotationSpeed = CurrentPositionSpeed / (MaxPositionSpeed / 2);

            if (CurrentPositionSpeed > 0.0f)
                Rotation += MathHelper.ToRadians(RotationSpeed);
            if (CurrentPositionSpeed < 0.0f)
                Rotation -= MathHelper.ToRadians(RotationSpeed);
        }

        public void TurnLeft()
        {
            if (CurrentPositionSpeed > -MaxRotationSpeed && CurrentPositionSpeed < MaxRotationSpeed)
                RotationSpeed = 0;
            else if ((CurrentPositionSpeed <= -MaxRotationSpeed && CurrentPositionSpeed > (-MaxRotationSpeed * 3)) || (CurrentPositionSpeed >= MaxRotationSpeed && CurrentPositionSpeed < (MaxRotationSpeed * 3)))
                RotationSpeed = MaxRotationSpeed;
            else
                RotationSpeed = CurrentPositionSpeed / (MaxPositionSpeed / 2);

            if (CurrentPositionSpeed > 0.0f)
                Rotation -= MathHelper.ToRadians(RotationSpeed);
            if (CurrentPositionSpeed < 0.0f)
                Rotation += MathHelper.ToRadians(RotationSpeed);
        }
    }
}