using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    class Player : Entity
    {
        private PlayerInput _input = new PlayerInput();
        private Entity _sword = new Entity('/');
        //Creates a new Player represented by the '@' symbol and adds movement key events

           
        public Player() : this('@')
        {

        }

        public Player(string imageName) : base('@', imageName)
        {
            //Bind movement methods to the arrow keys
            _input.AddKeyEvent(MoveLeft, 97); //A
            _input.AddKeyEvent(MoveRight, 100); //D 
            _input.AddKeyEvent(MoveUp, 119); //W
            _input.AddKeyEvent(MoveDown, 115); //S
            _input.AddKeyEvent(DetachSword, 99);//e
            //Add ReadKey to this Entity's OnUpdate
            OnUpdate += _input.ReadKey;
            OnUpdate += Orbit;
            OnUpdate += CreateSword; 
            OnUpdate += AttachSword;
        }

        //Creates a new Player with the specified symbol and adds movement key events
        public Player(char icon) : base(icon)
        {
            //Bind movement methods to the arrow keys
            _input.AddKeyEvent(MoveLeft, 97); //A
            _input.AddKeyEvent(MoveRight, 100); //D 
            _input.AddKeyEvent(MoveUp, 119); //W
            _input.AddKeyEvent(MoveDown, 115); //S
        }

        //Create and add a sword to the scene
        private void CreateSword(float deltaTime)
        {
                     
            CurrentScene.AddEntity(_sword);
            _sword.X = X;
            _sword.Y = Y;
        }

        //Add sword as a child
        private void AttachSword(float deltaTime)
        {
            if(!Hitbox.Overlaps(_sword.Hitbox))
            {
                return;
            }
            AddChild(_sword);
            _sword.X = 0.5f;
            _sword.Y = 0.5f;
        }

        private void DetachSword()
        {
            if(_sword.CurrentScene != CurrentScene)
            {
                return;
            }
            RemoveChild(_sword);
        }

        private void Orbit(float deltaTime)
        {
            foreach (Entity child in _children)
            {
                //child.Rotate(0.5f * deltaTime);
            }
            Rotate(0.5f * deltaTime);
        }

        //Move one space to the up
        private void MoveUp()
        {
            if (Y - 1 < 0)
            {
                if (CurrentScene is Room)
                {
                    Room dest = (Room)CurrentScene;
                    Travel(dest.North);
                }
                Y = CurrentScene.SizeY - 1;
            }
            else if (!CurrentScene.GetCollision(X, Y - 1))
            {
                Y--;
            }
        }

        //Move one space to the down
        private void MoveDown()
        {
            if (Y + 1 >= CurrentScene.SizeY)
            {
                if (CurrentScene is Room)
                {
                    Room dest = (Room)CurrentScene;
                    Travel(dest.South);
                }
                Y = 0;
            }
            else if (!CurrentScene.GetCollision(X, Y + 1))
            {
                Y++;
            }
        }

        //Move one space to the left
        private void MoveLeft()
        {
            if (X - 1 < 0)
            {
                if (CurrentScene is Room)
                {
                    Room dest = (Room)CurrentScene;
                    Travel(dest.West);
                }
                X = CurrentScene.SizeX - 1;
            }
            else if (!CurrentScene.GetCollision(X - 1, Y))
            {
                X--;
            }
        }

        //Move one space to the right
        private void MoveRight()
        {
            if (X + 1 >= CurrentScene.SizeX)
            {
                if (CurrentScene is Room)
                {
                    Room dest = (Room)CurrentScene;
                    Travel(dest.East);
                }
                X = 0;
            }
            else if (!CurrentScene.GetCollision(X + 1, Y))
            {
                X++;
            }
        }

        //Move the Player to the destination Room and change the Scene
        private void Travel(Room destination)
        {
            //Ensure destination is not null
            if (destination == null)
            {
                return;
            }

            //If we are holding the sword
            if(_sword.Parent == this)
            {
                //Remove the sword from the current Room
                CurrentScene.RemoveEntity(_sword);
                //Add the sowrd to the destination Room
                destination.AddEntity(_sword);
            }

            //Remove the Player from its current Room
            CurrentScene.RemoveEntity(this);
            //Add the Player to the destination Room
            destination.AddEntity(this);
            //Change the Game's active Scene to the destination
            Game.CurrentScene = destination;
        }
    }
}
