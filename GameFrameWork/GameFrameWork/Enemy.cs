using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    class Enemy : Entity
    {
        private Direction _facing;

        public float speed { get; set; } = 5f;

        public Enemy() : this('e')
        {

        }

        public Enemy(string imageName) : base('e', imageName)
        {
            _facing = Direction.North;
            OnUpdate += Move;
            OnUpdate += TouchPlayer;
        }

        public Enemy(char icon) : base(icon)
        {
            _facing = Direction.North;
            OnUpdate += Move;
            OnUpdate += TouchPlayer;
        }

        private void TouchPlayer(float deltaTime)
        {
            //Get the List of Entites in our space
            List<Entity> touched = CurrentScene.GetEntities(X, Y);

            //Check if any of them are Players
            bool hit = false;
            foreach (Entity e in touched)
            {
                if (e is Player)
                {
                    hit = true;
                    break;
                }
            }

            if (hit)
            {
                CurrentScene.RemoveEntity(this);
            }
        }

        private void Move(float deltaTime)
        {
            switch (_facing)
            {
                case Direction.North:
                    MoveUp(deltaTime);
                    break;
                case Direction.South:
                    MoveDown(deltaTime);
                    break;
                case Direction.East:
                    MoveRight(deltaTime);
                    break;
                case Direction.West:
                    MoveLeft(deltaTime);
                    break;
            }
        }

        private void MoveUp(float deltaTime)
        {
            if (!CurrentScene.GetCollision(XAbsolute, Sprite.Top - speed * deltaTime))
            {
                YVelocity = -speed * deltaTime;
            }
            else
            {
                YVelocity = 0f;
                _facing++;
            }
        }

        private void MoveDown(float deltaTime)
        {
            if (!CurrentScene.GetCollision(XAbsolute, Sprite.Bottom + speed * deltaTime))
            {
                YVelocity = speed * deltaTime;
            }
            else
            {
                YVelocity = 0f;
                _facing++;
            }
        }

        private void MoveLeft(float deltaTime)
        {
            if (!CurrentScene.GetCollision(Sprite.Left - speed * deltaTime, YAbsolute))
            {
                XVelocity = -speed * deltaTime;
            }
            else
            {
                XVelocity = 0f;
                _facing = Direction.North;
            }
        }

        private void MoveRight(float deltaTime)
        {
            if (!CurrentScene.GetCollision(Sprite.Right + speed * deltaTime, YAbsolute))
            {
                XVelocity = speed * deltaTime;
            }
            else
            {
                XVelocity = 0f;
                _facing++;
            }
        }
    }
}
