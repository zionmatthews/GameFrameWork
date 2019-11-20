using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    class Vector4
    {
        public float x;
        public float y;
        public float z;
        public float w;

        //Creates a Vector4 with x, y, z, and w at 0
        public Vector4() : this(0f, 0f, 0f, 0f)
        {

        }

        //Creates a Vector4 with x, y, z, and w at the specified values
        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public override string ToString()
        {
            return "{" + x + ", " + y + ", " + z + ", " + w + "}";
        }

        //Returns the magnitude of the Vector4
        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z + w * w);
        }

        //Returns the square of the magnitude of the Vector4
        public float MagnitudeSqr()
        {
            return (x * x + y * y + z * z + w * w);
        }

        //Returns the distance between this Vector4 and another
        public float Distance(Vector4 other)
        {
            float diffX = x - other.x;
            float diffY = y - other.y;
            float diffZ = z - other.z;
            float diffW = w - other.w;
            return (float)Math.Sqrt(diffX * diffX + diffY * diffY + diffZ * diffZ + diffW * diffW);
        }

        //Normalizes this Vector4
        public void Normalize()
        {
            float m = Magnitude();
            x /= m;
            y /= m;
            z /= m;
            w /= m;
        }

        //Returns a normalized Vector4 without modifying this one
        public Vector4 GetNormalized()
        {
            return (this / Magnitude());
        }

        //Returns the dot product of this Vector4 and another
        public float Dot(Vector4 other)
        {
            return x * other.x + y * other.y + z * other.z + w * other.w;
        }

        //Returns the cross product of this Vector2 and another
        public Vector4 Cross(Vector4 other)
        {
            return new Vector4(
                y * other.z - z * other.y,
                z * other.x - x * other.z,
                x * other.y - y * other.x,
                0);
        }

        //Vector4 + Vector4
        public static Vector4 operator +(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(
                lhs.x + rhs.x,
                lhs.y + rhs.y,
                lhs.z + rhs.z,
                lhs.w + rhs.w);
        }

        //Vector4 - Vector4
        public static Vector4 operator -(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(
                lhs.x - rhs.x,
                lhs.y - rhs.y,
                lhs.z - rhs.z,
                lhs.w - rhs.w);
        }

        //Vector4 * float
        public static Vector4 operator *(Vector4 lhs, float rhs)
        {
            return new Vector4(
                lhs.x * rhs,
                lhs.y * rhs,
                lhs.z * rhs,
                lhs.w * rhs);
        }

        //float * Vector4
        public static Vector4 operator *(float lhs, Vector4 rhs)
        {
            return new Vector4(
                lhs * rhs.x,
                lhs * rhs.y,
                lhs * rhs.z,
                lhs * rhs.w);
        }

        //Vector4 / float
        public static Vector4 operator /(Vector4 lhs, float rhs)
        {
            return new Vector4(
                lhs.x / rhs,
                lhs.y / rhs,
                lhs.z / rhs,
                lhs.w / rhs);
        }

        //float / Vector4
        public static Vector4 operator /(float lhs, Vector4 rhs)
        {
            return new Vector4(
                lhs / rhs.x,
                lhs / rhs.y,
                lhs / rhs.z,
                lhs / rhs.w);
        }
    }
}

