using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.Physics
{
    /// <summary>
    /// StaticBody和RigidBody的基类。
    /// </summary>
    internal abstract class PhysicsBodyBase
    {
        /// <summary>
        /// PhysicsBody所在的X轴位置。
        /// </summary>
        public virtual float X { get; set; }
        /// <summary>
        /// PhysicsBody所在的Y轴位置。
        /// </summary>
        public virtual float Y { get; set; }
        /// <summary>
        /// PhysicsBody所在的位置（使用PointF表示）。
        /// </summary>
        public virtual PointF Position
        {
            get => new PointF(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        /// <summary>
        /// 此PhysicsBody的宽。
        /// </summary>
        public float Width { get; set; }
        /// <summary>
        /// 此PhysicsBody的高。
        /// </summary>
        public float Height { get; set; }
        /// <summary>
        /// 此PhysicsBody的大小（使用SizeF表示）。
        /// </summary>
        public SizeF Size
        {
            get => new SizeF(Width, Height);
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }
        /// <summary>
        /// 此PhysicsBody的矩形（使用RectangleF表示）。
        /// </summary>
        public RectangleF Bounds
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
        /// <summary>
        /// 初始化PhysicsBodyBase。
        /// </summary>
        /// <param name="x">初始化时的X轴位置。</param>
        /// <param name="y">初始化时的Y轴位置。</param>
        /// <param name="width">初始化时的宽。</param>
        /// <param name="height">初始化时的高。</param>
        public PhysicsBodyBase(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
