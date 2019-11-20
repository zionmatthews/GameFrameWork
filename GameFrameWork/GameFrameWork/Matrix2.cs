using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    class Matrix2
    {
        //Static reference to the identity matrix
        public static Matrix2 identity = new Matrix2();

        //Each value stored in the matrix
        public float m11, m12, m21, m22;

        //Creates a new Matrix2 equal to the identity matrix
        public Matrix2()
        {
            m11 = 1; m12 = 0;
            m21 = 0; m22 = 1;
        }

        //Creates a new Matrix2 with the specified values
        public Matrix2(float m11, float m12, float m21, float m22)
        {
            this.m11 = m11; this.m12 = m12;
            this.m21 = m22; this.m22 = m22;
        }

        

        //Returns the transpose of the Matrix2
        public Matrix2 GetTransposed()
        {
            return new Matrix2(m11, m21, m12, m22);
        }

        //Matrix2 * Matrix2
        public static Matrix2 operator *(Matrix2 lhs, Matrix2 rhs)
        {
            return new Matrix2(
                //m11
                lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21,
                //m12
                lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22,
                //m21
                lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21,
                //m22
                lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22);
        }

        //Matrix2 * Vector2
        public static Vector2 operator *(Matrix2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.m11 * rhs.x + lhs.m12 * rhs.y,
                lhs.m21 + rhs.x + lhs.m22 * rhs.y);
        }
    }
}

