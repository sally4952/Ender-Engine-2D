using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.Physics
{
    /// <summary>
    /// 表示一个静态体（可阻止刚体的运动）。
    /// </summary>
    internal class StaticBody : PhysicsBodyBase
    {
        /// <summary>
        /// 确定它是否具有碰撞箱（是否能阻止刚体运动）。
        /// </summary>
        public bool HasCollision { get; set; } = true;
        /// <summary>
        /// 初始化静态体。
        /// </summary>
        /// <param name="x">静态体的初始X轴位置。</param>
        /// <param name="y">静态体的初始Y轴位置。</param>
        /// <param name="width">静态体的初始宽度。</param>
        /// <param name="height">静态体的初始高度。</param>
        public StaticBody(float x, float y, float width, float height) : base(x, y, width, height)
        {
        }
        /// <summary>
        /// 初始化静态体。
        /// </summary>
        /// <param name="position">静态体的初始位置</param>
        /// <param name="size">静态体的初始大小。</param>
        public StaticBody(PointF position, SizeF size) : base(position.X, position.Y, size.Width, size.Height)
        {
        }
        /// <summary>
        /// 初始化静态体。
        /// </summary>
        /// <param name="rect">用于初始化静态体位置和尺寸的矩形。</param>
        public StaticBody(RectangleF rect) : base(rect.X, rect.Y, rect.Width, rect.Height)
        {
        }
    }
}
