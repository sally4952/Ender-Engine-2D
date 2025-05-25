using EnderEngine2D.Physics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.GameObjects
{
    internal class Square : GameObjectBase, IDrawable
    {
        public RigidBody RigidBody;
        public StaticBody StaticBody;
        public override float X
        {
            get
            {
                return RigidBody.X;
            }
            set
            {
                if (RigidBody != null)
                {
                    RigidBody.X = value;
                }
            }
        }
        public override float Y
        {
            get
            {
                return RigidBody.Y;
            }
            set
            {
                if (RigidBody != null)
                {
                    RigidBody.Y = value;
                }
            }
        }
        public float Width;
        public float Height;
        public Color Color;
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
                X = value.X; Y = value.Y;
                Width = value.Width; Height = value.Height;
            }
        }

        public Square(RectangleF rect, Color color)
        {
            Rectangle = rect;
            Color = color;
            RigidBody = new RigidBody(rect);
            StaticBody = new StaticBody(rect);
        }

        void IDrawable.Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color), Rectangle);
        }
    }
}
