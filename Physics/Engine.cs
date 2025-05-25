using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnderEngine2D.Physics
{
    internal class Engine
    {
        public Dictionary<int, RigidBody> RigidBodies = new Dictionary<int, RigidBody>();
        public Dictionary<int, StaticBody> StaticBodies = new Dictionary<int, StaticBody>();
        public float Gravitation;
        public Engine(float g = 9.81f)
        {
            Gravitation = g;
        }
        public void Update()
        {
            if (RigidBodies.Count == 0)
                return;
            if (StaticBodies.Count == 0)
            {
                foreach (var item in RigidBodies)
                {
                    item.Value.Force.Y += Gravitation;
                    item.Value.Y += item.Value.Force.Y;
                }
                return;
            }
            foreach (var item in RigidBodies)
            {
                item.Value.Force.Y += Gravitation;
                foreach (var item2 in StaticBodies)
                {
                    if (IsOverlapping1(
                        item.Value.X, 
                        item.Value.Y, 
                        item.Value.Width, 
                        item.Value.Height, 
                        item2.Value.X, 
                        item2.Value.Y, 
                        item2.Value.Width, 
                        item2.Value.Height, 
                        item.Value.Force.Y))
                    {
                        item.Value.Force = Vector2.Zero;
                        item.Value.Y = item2.Value.Y - item.Value.Height;
                        continue;
                    }
                }
                item.Value.Y += item.Value.Force.Y;
            }
        }

        static bool IsOverlapping1(float x1, float y1, float w1, float h1, float x2, float y2, float w2, float h2, float n)
        {
            // 计算矩形1的新边界
            float x1Right = x1 + w1;
            float y1Top = y1 + n + h1;
            float y1Bottom = y1 + n;

            // 计算矩形2的边界
            float x2Right = x2 + w2;
            float y2Top = y2 + h2;

            // 判断是否重叠
            if (x1Right <= x2 || x1 >= x2Right || y1Top <= y2 || y1Bottom >= y2Top)
            {
                return false; // 不重叠
            }

            return true; // 重叠
        }
    }
}
