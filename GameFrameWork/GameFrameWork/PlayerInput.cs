using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using RL = Raylib.Raylib;

namespace GameFrameWork
{
     class PlayerInput
    {
        //Delegate that takes a ConsoleKey
        private delegate void KeyEvent(int key);
        //KeyEvent called when a key is pressed
        private  KeyEvent OnKeyPress;

        //Binds the specified Event to the specified ConsoleKey
        public  void AddKeyEvent(Event action, int key)
        {
            //Local method that takes a ConsoleKey and calls action if the specified ConsoleKey matches key
            void keyPressed(int keyPress)
            {
                if (key == keyPress)
                {
                    action();
                }
            }
            //Add the local method to the OnKeyPress KeyEvent
            OnKeyPress += keyPressed;
        }

        //Gets input from the Console
        public  void ReadKey(float deltaTime)
        {
            //ConsoleKey inputKey = Console.ReadKey().Key;
            int inputKey = RL.GetKeyPressed();
            OnKeyPress(inputKey);
        }
    }
}
