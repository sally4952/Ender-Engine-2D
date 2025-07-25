using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.Physics
{
    /// <summary>
    /// 表示一个刚体。
    /// </summary>
    internal class RigidBody : PhysicsBodyBase
    {
        /// <summary>
        /// 刚体的力。
        /// </summary>
        public Vector2 Force { get; set; } = Vector2.Zero;
        /// <summary>
        /// 刚体是否已经落地（对物理模拟没有影响）。
        /// </summary>
        public bool IsGrounded { get; set; }
        /// <summary>
        /// 惯性衰减的值。
        /// </summary>
        public float InertiaAttenuation { get; set; } = 1.2f;
        /// <summary>
        /// 确定它是否会被物理引擎模拟。
        /// </summary>
        public bool IsPhysicsal { get; set; } = true;
        /// <summary>
        /// 初始化刚体。
        /// </summary>
        /// <param name="x">刚体的初始X轴位置。</param>
        /// <param name="y">刚体的初始Y轴位置。</param>
        /// <param name="width">刚体的初始宽度。</param>
        /// <param name="height">刚体的初始高度。</param>
        public RigidBody(float x, float y, float width, float height) : base(x, y, width, height)
        {
        }
        /// <summary>
        /// 初始化刚体。
        /// </summary>
        /// <param name="position">刚体的初始位置。</param>
        /// <param name="size">刚体的初始大小。</param>
        public RigidBody(PointF position, SizeF size) : base(position.X, position.Y, size.Width, size.Height)
        {
        }
        /// <summary>
        /// 初始化刚体。
        /// </summary>
        /// <param name="rect">用于初始化刚体位置和尺寸的矩形。</param>
        public RigidBody(RectangleF rect) : base(rect.X, rect.Y, rect.Width, rect.Height)
        {
        }
    }
}
