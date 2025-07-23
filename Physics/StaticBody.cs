using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.Physics
{
    internal class StaticBody : PhysicsBodyBase
    {
        public bool HasCollision { get; set; } = true;
        public StaticBody(float x, float y, float width, float height) : base(x, y, width, height)
        {
        }
        public StaticBody(PointF position, SizeF size) : base(position.X, position.Y, size.Width, size.Height)
        {
        }
        public StaticBody(RectangleF rect) : base(rect.X, rect.Y, rect.Width, rect.Height)
        {
        }
    }
}
