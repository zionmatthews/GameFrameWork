using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    class Vector3
    {
        public float x;
        public float y;
        public float z;

        //Creates a Vector3 with x, y, and z at 0
        public Vector3() : this(0f, 0f, 0f)
        {

        }

        //Creates a Vector3 with x, y, and z at the specified values
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override string ToString()
        {
            return "{" + x + ", " + y + ", " + z + "}";
        }

        //Returns the magnitude of the Vector3
        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }

        //Returns the square of the magnitude of the Vector3
        public float MagnitudeSqr()
        {
            return (x * x + y * y + z * z);
        }

        //Returns the distance between this Vector3 and another
        public float Distance(Vector3 other)
        {
            float diffX = x - other.x;
            float diffY = y - other.y;
            float diffZ = z - other.z;
            return (float)Math.Sqrt(diffX * diffX + diffY * diffY + diffZ * diffZ);
        }

        //Normalizes this Vector3
        public void Normalize()
        {
            float m = Magnitude();
            x /= m;
            y /= m;
            z /= m;
        }

        //Returns a normalized Vector3 without modifying this one
        public Vector3 GetNormalized()
        {
            return (this / Magnitude());
        }

        //Returns the dot product of this Vector3 and another
        public float Dot(Vector3 other)
        {
            return x * other.x + y * other.y + z * other.z;
        }

        //Returns the cross product of this Vector2 and another
        public Vector3 Cross(Vector3 other)
        {
            return new Vector3(
                y * other.z - z * other.y,
                z * other.x - x * other.z,
                x * other.y - y * other.x);
        }

        public float GetAngle(Vector3 other)
        {
            Vector3 a = GetNormalized();
            Vector3 b = other.GetNormalized();

            return (float)Math.Acos(a.Dot(b));
        }

        //Vector3 + Vector3
        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }

        //Vector3 - Vector3
        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }

        //Vector3 * float
        public static Vector3 operator *(Vector3 lhs, float rhs)
        {
            return new Vector3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
        }

        //float * Vector3
        public static Vector3 operator *(float lhs, Vector3 rhs)
        {
            return new Vector3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);
        }

        //Vector3 / float
        public static Vector3 operator /(Vector3 lhs, float rhs)
        {
            return new Vector3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
        }

        //float / Vector3
        public static Vector3 operator /(float lhs, Vector3 rhs)
        {
            return new Vector3(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z);
        }

        public static Vector3 Min (Vector3 a, Vector3 b)
        {
            return new Vector3(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z));
        }

        public static Vector3 Max(Vector3 a, Vector3 b)
        {
            return new Vector3(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Max(a.z, b.z));
        }

        public static Vector3 Clamp(Vector3 t, Vector3 a, Vector3 b)
        {
            return Max(a, Min(b, t));
        }

    }
}
