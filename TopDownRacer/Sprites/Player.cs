using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownRacer.Managers;
using TopDownRacer.Models;

namespace TopDownRacer.Sprites
{
    public class Player : Sprite
    {
        public String Name = "kevin";
        public int Score;
        public int checkpointId = 0;
        public Boolean Dead = false;

        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;

        public Vector2 _position
        {
            get { return Position; }
            set
            {
                Position = value;

                if (_animationManager != null)
                    _animationManager.Position = Position;
            }
        }
        private int MaxPositionSpeed { get; set; } = 15;
        private float ChangePositionSpeed { get; set; }

        private float RotationSpeed { get; set; } = 0f;
        private float MaxRotationSpeed { get; set; } = 2.5f;
        private int playerNumber;

        //public Player(Texture2D texture, int x, int y, int playerNumber = 0, Dictionary<string, Animation> animations)
        public Player(Texture2D texture, int x, int y, int playerNumber = 0)
        : base(texture)
        {
            //_animations = animations;
            //_animationManager = new AnimationManager(_animations.First().Value);

            Position = new Vector2(x, y);
            if (playerNumber < 0 || playerNumber > 3)
            {
                this.playerNumber = 0;
            } else
            {
                this.playerNumber = playerNumber;
            }
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
            if (kstate.IsKeyDown(Input.Left[playerNumber]))
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

                //_animationManager.Play(_animations["BlinkerRight"]);
            }

            if (kstate.IsKeyDown(Input.Right[playerNumber]))
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

            Vector2 direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));

            if (kstate.IsKeyDown(Input.Up[playerNumber]))
            {
                // if the current speed is not above the max speed accelerate the car forwards
                if (CurrentPositionSpeed < MaxPositionSpeed)
                {
                    ChangePositionSpeed += 0.15f;
                    CurrentPositionSpeed += ChangePositionSpeed;
                }
            }
            else if (kstate.IsKeyDown(Input.Down[playerNumber]))
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