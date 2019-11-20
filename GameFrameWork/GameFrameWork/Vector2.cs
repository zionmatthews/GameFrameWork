using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    class Vector2
    {
        public float x;
        public float y;

        //Creates a Vector2 with x and y at 0
        public Vector2() : this(0f, 0f)
        {

        }

        //Creates a Vector2 with x and y at the specified values
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return "{" + x + ", " + y + "}";
        }

        //Returns the magnitude of the Vector2
        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }

        //Returns the square of the magnitude of the Vector2
        public float MagnitudeSqr()
        {
            return (x * x + y * y);
        }

        //Returns the distance between this Vector2 and another
        public float Distance(Vector2 other)
        {
            float diffX = x - other.x;
            float diffY = y - other.y;
            return (float)Math.Sqrt(diffX * diffX + diffY * diffY);
        }

        //Normalizes this Vector2
        public void Normalize()
        {
            float m = Magnitude();
            x /= m;
            y /= m;
        }

        //Returns a normalized Vector2 without modifying this one
        public Vector2 GetNormalized()
        {
            return (this / Magnitude());
        }

        //Returns the dot product of this Vector2 and another
        public float Dot(Vector2 other)
        {
            return x * other.x + y * other.y;
        }

        public Vector2 GetPerpendicularRH()
        {
            return new Vector2(-y, x);
        }

        public Vector2 GetPerpendicularLH()
        {
            return new Vector2(y, -x);
        }

        public float GetAngle(Vector2 other)
        {
            Vector2 a = GetNormalized();
            Vector2 b = other.GetNormalized();

            return (float)Math.Acos(a.Dot(b));
        }

        //Vector2 + Vector2
        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x + rhs.x, lhs.y + rhs.y);
        }

        //Vector2 - Vector2
        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x - rhs.x, lhs.y - rhs.y);
        }

        //Vector2 * float
        public static Vector2 operator *(Vector2 lhs, float rhs)
        {
            return new Vector2(lhs.x * rhs, lhs.y * rhs);
        }

        //float * Vector2
        public static Vector2 operator *(float lhs, Vector2 rhs)
        {
            return new Vector2(lhs * rhs.x, lhs * rhs.y);
        }

        //Vector2 / float
        public static Vector2 operator /(Vector2 lhs, float rhs)
        {
            return new Vector2(lhs.x / rhs, lhs.y / rhs);
        }

        //float / Vector2
        public static Vector2 operator /(float lhs, Vector2 rhs)
        {
            return new Vector2(lhs / rhs.x, lhs / rhs.y);
        }


    }
}
