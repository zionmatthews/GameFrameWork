using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using RL = Raylib.Raylib;

namespace GameFrameWork
{
    class Scene
    {
        //The list of all the Entities in the Scene
        private List<Entity> _entities = new List<Entity>();
        //The list of Entities to add to the Scene
        private List<Entity> _additions = new List<Entity>();
        //The list of Entities to remove from the scene
        private List<Entity> _removals = new List<Entity>();
        //The size of the Scene
        private int _sizeX, _sizeY;
        //The grid for collision detection
        private bool[,] _collision;
        //The grid for Entity tracking
        private List<Entity>[,] _tracking;
        private bool _started = false;

        //Events that are called when the Scene is Started, Updated, and Drawn
        public Event OnStart;
        public Event OnUpdate;
        public Event OnDraw;

        //Creates a new Scene with a size of 24x6
        public Scene() : this(6, 6)
        {

        }

        //Creates a new Scene with the specified size
        //sizeX: the horizontal size of the Scene
        //sizeY: the vertical size of the Scene
        public Scene(int sizeX, int sizeY)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            //Create the collision grid
            _collision = new bool[_sizeX, _sizeY];
            //Create the tracking grid
            _tracking = new List<Entity>[_sizeX, _sizeY];
        }

        //The horizontal size of the Scene
        public int SizeX
        {
            get
            {
                return _sizeX;
            }
        }

        //The vertical size of the Scene
        public int SizeY
        {
            get
            {
                return _sizeY;
            }
        }

        public bool Started
        {
            get { return _started; }
        }

        //Called in Game when the Scene should begin
        public void Start()
        {
            OnStart?.Invoke();

            foreach (Entity e in _entities)
            {
                //Call the Entity's Start events
                if (!e.Started)
                {
                    e.Start();
                }
               
            }

            _started = true;
        }

        //Called in Game every step to update each Entity in the Scene
        public void Update(float deltaTime)
        {
            OnUpdate?.Invoke();

            //Clear the collision grid
            _collision = new bool[_sizeX, _sizeY];
            //Clear the tracking grid
            for (int y = 0; y < _sizeY; y++)
            {
                for (int x = 0; x < _sizeX; x++)
                {
                    _tracking[x, y] = new List<Entity>();
                }
            }

            //Add all the Entities readied for addition
            foreach (Entity e in _removals)
            {
                //Add e to _entities
                _entities.Add(e);
            }
            _additions.Clear();

            foreach (Entity e in _removals)
            {
                //Remove e from  _entities
                _entities.Remove(e);
            }
            //Reset the removal list
            _removals.Clear();

            foreach (Entity e in _entities)
            {
                //Call the Entity's Update events
                e.Update(deltaTime);
                //Set the Entity's collision in the collision grid
                int x = (int)Math.Round(e.XAbsolute);
                int y = (int)Math.Round(e.YAbsolute);
                //Only update if the Entity is within bounds
                if (x >= 0 && x < _sizeX
                    && y >= 0 && y < _sizeY)
                {
                    //Add the Entity to the tracking grid
                    _tracking[x, y].Add(e);
                    //Only update this point in the grid if the Entity is solid
                    if (!_collision[x, y])
                    {
                        _collision[x, y] = e.Solid;
                    }
                }
            }

            foreach (Entity e in _entities)
            {
                //Call the Entity's Update events
                e.Update(deltaTime);
            }
        }

        //Called in Game every step to render each Entity in the Scene
        public void Draw()
        {
            OnDraw?.Invoke();

            //Clear the screen
            Console.Clear();
            RL.ClearBackground(Color.GOLD);

            //Create the display grid
            char[,] display = new char[_sizeX, _sizeY];

            foreach (Entity e in _entities)
            {
                //Call the Entity's Draw events
                e.Draw();
                //Position the Entity's icon in the display
                int x = (int)Math.Round(e.XAbsolute);
                int y = (int)Math.Round(e.YAbsolute);
                if (x >= 0 && x < _sizeX
                    && y >= 0 && y < _sizeY)
                {
                    display[x, y] = e.Icon;
                }
            }

            //Render the display grid to the screen
            for (int y = 0; y < _sizeY; y++)
            {
                for (int x = 0; x < _sizeX; x++)
                {
                    Console.Write(display[x, y]);
                    foreach (Entity e in _tracking[x, y])
                    {
                        if (e.Sprite == null)
                        {
                            continue;
                        }
                        //RL.DrawTexture(e.Sprite, (int)e.X * Game.SizeX, (int)e.Y * Game.Sizey, Color.WHITE);
                        Texture2D texture = e.Sprite.Texture;
                        float positionX = e.Sprite.XAbsolute * Game.UnitSize.x / 2;
                        float positionY = e.Sprite.YAbsolute * Game.UnitSize.y / 2;
                        Raylib.Vector2 position = new Raylib.Vector2(positionX, positionY);
                        float rotation = e.Rotation * (float)(180.0f / Math.PI);
                        float scale = e.Sprite.Size;
                        RL.DrawTextureEx(texture, position, rotation, scale, Color.WHITE);
                    }
                }
                Console.WriteLine();
            }

            foreach (Entity e in _entities)
            {
                //Call the Entity's Draw events
                e.Draw();
            }
        }

        //Add an Entity to the Scene and set the Scene as the Entity's Scene
        public void AddEntity(Entity entity)
        {
            if (_additions.Contains(entity))
            {
                return;
            }
            //Ready the Entity for addition
            _additions.Add(entity);
            //Add the Entity to the Scene
            _entities.Add(entity);
            //Set this Scene as the Entity's Scene
            entity.CurrentScene = this;
        }

        //Remove an Entity from the Scene and nullify the Entity's Scene
        public void RemoveEntity(Entity entity)
        {
            if (_removals.Contains(entity))
            {
                return;
            }
            //Ready the Entity for removal
            _removals.Add(entity);
            //Nullify the Entity's Scene
            entity.CurrentScene = null;
        }

        //Clear the Scene of Entities and nullify their Scenes
        public void ClearEntities()
        {
            //Nullify each Entity's Scene
            foreach (Entity e in _entities)
            {
                RemoveEntity(e);
            }
            //Clear the Entity List
            _entities.Clear();
        }

        //Returns whether there is a solid Entity at the point
        public bool GetCollision(float x, float y)
        {
            //Ensure the point is within the Scene
            if (x >= 0 && y >= 0 && x < _sizeX && y < _sizeY)
            {
                return _collision[(int)x, (int)y];
            }
            //A point outside the Scene is not a collision
            else
            {
                return true;
            }
        }

        //Returns the List of Entities at a specified point
        public List<Entity> GetEntities(float x, float y)
        {
            int checkX = (int)Math.Round(x);
            int checkY = (int)Math.Round(y);
            //Ensure the point is within the Scene
            if (checkX >= 0 && checkY >= 0 && checkX < _sizeX && checkY < _sizeY)
            {
                return _tracking[checkX, checkY];
            }
            //A point outside the Scene is not a collision
            else
            {
                return new List<Entity>();
            }
        }
    }
}
