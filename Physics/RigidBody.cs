using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.Physics
{
    internal class RigidBody : PhysicsBodyBase
    {
        public Vector2 Force { get; set; } = Vector2.Zero;
        public bool IsGrounded { get; set; }
        public float InertiaAttenuation { get; set; } = 1.2f;
        public bool IsPhysicsal { get; set; } = true;
        public RigidBody(float x, float y, float width, float height) : base(x, y, width, height)
        {
        }
        public RigidBody(PointF position, SizeF size) : base(position.X, position.Y, size.Width, size.Height)
        {
        }
        public RigidBody(RectangleF rect) : base(rect.X, rect.Y, rect.Width, rect.Height)
        {
        }
    }
}
