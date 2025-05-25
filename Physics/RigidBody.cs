using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.Physics
{
    internal class RigidBody
    {
        public float X;
        public float Y;
        public Vector2 Force;
        public PointF Position
        {
            get => new PointF(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        public float Width;
        public float Height;
        public SizeF Size
        {
            get => new SizeF(Width, Height);
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }
        public RectangleF Rectangle
        {
            get => new RectangleF(X, Y, Width, Height);
            set
            {
                X = value.X;
                Y = value.Y;
                Width = value.Width;
                Height = value.Height;
            }
        }
        public RigidBody(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public RigidBody(PointF position, SizeF size)
        {
            Position = position;
            Size = size;
        }

        public RigidBody(RectangleF rectangle)
        {
            Rectangle = rectangle;
        }
    }
}
