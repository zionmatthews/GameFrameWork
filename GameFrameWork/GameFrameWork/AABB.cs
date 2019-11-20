﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using RL = Raylib.Raylib;

namespace GameFrameWork
{
    class AABB
    {
        Vector3 min = new Vector3(float.NegativeInfinity,
                                     float.NegativeInfinity,
                                     float.NegativeInfinity);
        Vector3 max = new Vector3(float.PositiveInfinity,
                                   float.PositiveInfinity,
                                   float.PositiveInfinity);

        public AABB()
        {

        }

        public AABB(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }

        public void Resize(Vector3 min, Vector3 max)
        {
            min = min;
            max = max;
        }

        public void Move(Vector3 point)
        {
            Vector3 extents = Extents();
            min = point - extents;
            max = point + extents;
        }

        public Vector3 Center()
        {
            return (min + max) * 0.5f;
        }

        public Vector3 Extents()
        {
            return new Vector3(Math.Abs(max.x - min.x) * 0.5f,
                Math.Abs(max.y - min.y) * 0.5f,
                Math.Abs(max.z - min.z) * 0.5f);
        }

        public List<Vector3> Corners()
        {
            // ignoring z axis for 2D
            List<Vector3> corners = new List<Vector3>(4);
            corners[0] = min;
            corners[1] = new Vector3(min.x, max.y, min.z);
            corners[2] = max;
            corners[3] = new Vector3(max.x, min.y, min.z);
            return corners;
        }

        public void Fit(List<Vector3> points)
        {
            // invalidate the extents
            min = new Vector3(float.PositiveInfinity,
           float.PositiveInfinity,
           float.PositiveInfinity);
            max = new Vector3(float.NegativeInfinity,
           float.NegativeInfinity,
           float.NegativeInfinity);
           
            //find min and max of the points
            foreach(Vector3 p in points)
            {
                min = Vector3.Min(min, p);
                max = Vector3.Max(max, p);
            }
        }

        public bool Overlaps(Vector3 p)
        {
            // test for not overlapped as it exits faster
            return !(p.x < min.x || p.y < min.y ||
            p.x > max.x || p.y > max.y);
        }

        public bool Overlaps(AABB other)
        {
            // test for not overlapped as it exits faster
            return !(max.x < other.min.x || max.y < other.min.y ||
            min.x > other.max.x || min.y > other.max.y);
        }

        public Vector3 ClosestPoint(Vector3 p)
        {
            return Vector3.Clamp(p, min, max);
        }

        public void Draw(Color color)
        {
            float posX = min.x * Game.UnitSize.x + Game.UnitSize.x / 2;
            float posy = min.y * Game.UnitSize.y + Game.UnitSize.y / 2;
            float width = (max.x - min.x) * Game.UnitSize.x;
            float height = (max.y - min.y) * Game.UnitSize.y; 
            RL.DrawRectangleLines((int)min.x, (int)min.y, (int)width, (int)height, color);
        }
    }

}
