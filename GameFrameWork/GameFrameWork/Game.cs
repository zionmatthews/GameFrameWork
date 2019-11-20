using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Raylib;
using RL = Raylib.Raylib;

namespace GameFrameWork
{
    class Game
    {
        //The tilesize of the game
        public static readonly Vector2 UnitSize = new Vector2(16, 16);
       // public static readonly int UnitSizeX = 16;
       // public static readonly int UnitSizey = 16;
        //Whether or not the Game should finish Running and exit
        public static bool Gameover = false;
        //The Scene we are currently running
        private static Scene _currentScene;
        //The Scene we are about to go to
        private static Scene _nextScene;
        //The Timer for the entire game
        private Timer _gameTimer;
        //The camre for the 3D view
        private Camera3D _camera;

        //Creates a Game and new Scene instance as its active Scene
        public Game()
        {
            RL.InitWindow(640, 480, "THIS IS A MANS WORLD!!!");
            RL.SetTargetFPS(60);

            _gameTimer = new Timer();

            Raylib.Vector3 cameraPosition = new Raylib.Vector3(-10, -10, -10);
            Raylib.Vector3 cameraTarget = new Raylib.Vector3(320, 240, 0);
            Raylib.Vector3 cameraUp = new Raylib.Vector3(0, 0, 1);
        }

        //The Scene we are currently running
        public static Scene CurrentScene
        {
            set
            {
                _nextScene = value;
            }
            get
            {
                return _currentScene;
            }
        }

        private void Init()
        {
            Room startingRoom = new Room(8, 6);
            Room otherRoom = new Room(12, 6);

            Enemy enemy = new Enemy("flat,800x800,075,f.u6.jpg");
            void OtherRoomStart()
            {
                enemy.X = 4;
                enemy.Y = 4;
            }

            otherRoom.OnStart += OtherRoomStart;

            startingRoom.North = otherRoom;
            //Add Walls to the startingRoom
            startingRoom.AddEntity(new Wall(2, 2));
            //north walls
            for (int i = 0; i < startingRoom.SizeX; i++)
            {
                if (i != 2)
                {
                    startingRoom.AddEntity(new Wall(i, 0));
                }
            }
            //south walls
            for (int i = 0; i < startingRoom.SizeX; i++)
            {
                startingRoom.AddEntity(new Wall(i, startingRoom.SizeY - 1));
            }
            //east walls
            for (int i = 1; i < startingRoom.SizeY - 1; i++)
            {
                startingRoom.AddEntity(new Wall(startingRoom.SizeX - 1, i));
            }
            //west walls
            for (int i = 1; i < startingRoom.SizeY - 1; i++)
            {
                startingRoom.AddEntity(new Wall(0, i));
            }
            //Add Walls to the otherRoom
            //north walls
            for (int i = 0; i < otherRoom.SizeX; i++)
            {
                otherRoom.AddEntity(new Wall(i, 0));
            }
            //south walls
            for (int i = 0; i < otherRoom.SizeX; i++)
            {
                if (i != 2)
                {
                    otherRoom.AddEntity(new Wall(i, otherRoom.SizeY - 1));
                }
            }
            //east walls
            for (int i = 1; i < otherRoom.SizeY - 1; i++)
            {
                otherRoom.AddEntity(new Wall(otherRoom.SizeX - 1, i));
            }
            //west walls
            for (int i = 1; i < otherRoom.SizeY - 1; i++)
            {
                otherRoom.AddEntity(new Wall(0, i));
            }

            //Create a Player, position it, and add it to startingRoom
            Player player = new Player("a945c929345e94a4dd330026e639a03da3dc1678_00.jpg");
            player.X = 4;
            player.Y = 3;
            player.Sprite.X -= 0.5f;
            player.Sprite.Y -= 0.5f;
            startingRoom.AddEntity(player);
            Entity sword = new Entity('/');
            player.AddChild(sword);
            sword.X += 1.5f;
            sword.Y += 0.5f;
            startingRoom.AddEntity(sword);
            //Add enemy to otherRoom
            otherRoom.AddEntity(enemy);

            CurrentScene = startingRoom;
        }

        public void Run()
        {
            //Bind Esc to exit the game(no longer needed
            //PlayerInput.AddKeyEvent(Quit, ConsoleKey.Escape);

            Init();

            Camera2D camera = new Camera2D();
            camera.zoom = 3;

            //Update, draw, and get input until the game is over
            while (!Gameover && !RL.WindowShouldClose())
            {
                //Start the Scene if needed
                if (_currentScene != _nextScene)
                {
                    _currentScene = _nextScene;
                   
                }

                _currentScene.Update(_gameTimer.GetDeltaTime());

                if (!_currentScene.Started)
                {
                    _currentScene.Start();
                }


                //Update the active Scene
                _currentScene.Update(_gameTimer.GetDeltaTime());

                //Draw the active Scene
                RL.BeginDrawing();
                RL.BeginMode3D(_camera);
                _currentScene.Draw();
                RL.EndMode3D();
                RL.EndDrawing();


               
            }

            RL.CloseWindow();
        }

        public void Quit()
        {
            Gameover = true;
        }

        
    }
}
