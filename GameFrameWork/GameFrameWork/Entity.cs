using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Raylib;
using RL = Raylib.Raylib;

namespace GameFrameWork
{
    delegate void Event();
    delegate void UpdateEvent(float deltaTime);

    class Entity
    {
        //Events that are called when the Entity is Started, Updated, and Drawn
        public Event OnStart;
        public UpdateEvent OnUpdate;
        public Event OnDraw;

        protected Entity _parent = null;
        protected List<Entity> _children = new List<Entity>();

        //The location of the Entity
        //private Vector2 _location = new Vector2();
        //private Vector3 _location = new Vector3(0, 0, 1);
        //The velocity of the Entity
        private Vector2 _velocity = new Vector2();
        //private Matrix3 _transform = new Matrix3();
        //private Matrix3 _translation = new Matrix3();
        //private Matrix3 _rotation = new Matrix3();
        //private Matrix3 _scale = new Matrix3();
        //private float _scale = 1.0f;
        private Matrix3 _localTransform = new Matrix3();
        private Matrix3 _globalTransform = new Matrix3();

        public AABB Hitbox { get; set; }

        //Whether this Entity's Start method has been called
        private bool _started = false;
        public bool Started
        {
            get { return _started; }
        }

        //The character representing the Entity on the screen
        public char Icon { get; set; } = ' ';
        //The image representing the Entity on the screen
        public SpriteEntity Sprite { get; set; }
        //Whether or not this Entity returns a collision
        public bool Solid { get; set; } = false;

        public float OriginX { get; set; } = 0;

        public float OriginY { get; set; } = 0;
        //The Entity's location on the X axis
        public float X
        {
            get
            {
                return _localTransform.m13;
            }
            set
            {
                _localTransform.SetTranslation(value, Y, 1);
            }
        }

        public float XAbsolute
        {
            get
            {
                { return _globalTransform.m13; }
            }
        }

        //The Entity's location on the Y axis
        public float Y
        {
            get
            {
                return _localTransform.m23;
            }
            set
            {
                _localTransform.SetTranslation(X, value, 1);
            }
        }

        public float YAbsolute
        {
            get { return _globalTransform.m23; }
        }
        //The Entity's velocity on the X axis
        public float XVelocity
        {
            get
            {
                return _velocity.x;
                //return _translation.m13;
            }
            set
            {
                _velocity.x = value;
                //_translation.SetTranslation(value, YVelocity, 1);
            }
        }
        //The Entity's velocity on the Y axis
        public float YVelocity
        {
            get
            {
                return _velocity.y;
                //return _translation.m23;
            }
            set
            {
                _velocity.y = value;
                //_translation.SetTranslation(XVelocity, value, 1);
            }
        }
        //The Entity's scale
        public float Size
        {
            get
            {
                //return _scale.m11;
                //return _scale;
                return 1;
            }
            //set
            //{
            //    _localTransform.SetScaled(value, value, 1);
            //    //_scale = value;
            //}
        }
        //The Entity's rotation in radians
        public float Rotation
        {
            get
            {
                return (float)Math.Atan2(_globalTransform.m21, _globalTransform.m11);
            }
            //set
            //{
            //    _localTransform.SetRotateZ(value);
            //}
        }

        public float Width
        {
            get
            {
                return _localTransform.m13;
            }
        }

        //The Scene the Entity is currently in
        public Scene CurrentScene { get; set; }
        //The parent of this Entity
        public Entity Parent
        {
            get
            {
                return _parent;
            }
        }

       
        //Creates an Entity with default values
        public Entity()
        {
            Hitbox = new AABB(
                new Vector3(XAbsolute - 0.5f, YAbsolute - 0.5f, 1),
                new Vector3(XAbsolute + 0.5f, YAbsolute + 0.5f, 1));
        }

        //Creates an Entity with the specified icon and default values
        public Entity(char icon)
        {
            Icon = icon;
        }

        //Creates an Entity with the specified icon and image
        public Entity(char icon, string imageName) : this(icon)
        {
            Sprite = new SpriteEntity();
            Sprite.Load(imageName);
            AddChild(Sprite);
        }

        ~Entity()
        {
            if(_parent != null)
            {
                _parent.RemoveChild(this);
            }

            foreach (Entity e in _children)
            {
                e._parent = null;
            }
        }

        public int GetChildCount()
        {
            return _children.Count;

        }

        public Entity GetChild(int index)
        {
            return _children[index];
        }

        public void AddChild(Entity child)
        {
            //Make sure the child doesn't already have a parent
            if(child._parent != null)
            {
                return;
            }
            //Assogn this Entity as the child's parent
            child._parent = this;
            //Add child to collection
            _children.Add(child);
        }

        public void RemoveChild(Entity child)
        {
            bool isMyChild = _children.Remove(child);
            _localTransform = _globalTransform;
        }

        //Scale the Entity by the specified amount
        public void Scale(float width, float height)
        {
            _localTransform.Scale(width, height, 1);
        }

        //Rotate the Entity by the specified amount
        public void Rotate(float radians)
        {
            _localTransform.RotateZ(radians);
        }

        protected void UpdateTransform()
        {
            if (_parent != null)
            {
                _globalTransform = _parent._globalTransform * _localTransform;
            }
            else
            {
                _globalTransform = _localTransform;
            }

            foreach (Entity child in _children)
            {
                child.UpdateTransform();
            }
        }

        //Find the distance between this Entity and another
        public float GetDistance(Entity other)
        {
            Vector3 position = new Vector3(XAbsolute, YAbsolute, 1);
            Vector3 otherPosition = new Vector3(other.XAbsolute,  other.YAbsolute, 1);
            return position.Distance(otherPosition);
        }

        //Call the Entity's OnStart event
        public void Start()
        {
            OnStart?.Invoke();
            _started = true;
            Hitbox = new AABB(
                new Vector3(XAbsolute - 0.5f, YAbsolute - 0.5f, 1),
                new Vector3(XAbsolute + 0.5f, YAbsolute + 0.5f, 1));
        }

        //Call the Entity's OnUpdate event
        public void Update(float deltaTime)
        {
            //_location += _velocity;
            //_location = _transform * _location;
            //Matrix3 transform = _translation * _rotation;
            //_location = transform * _location;
            X += _velocity.x * deltaTime;
            Y += _velocity.y * deltaTime;
            Hitbox.Move(new Vector3(XAbsolute, YAbsolute, 1));
        }

        //Call the Entity's OnDraw event
        public void Draw()
        {
            OnDraw?.Invoke();
            Hitbox.Draw(Raylib.Color.LIME);
        }
    }
}
