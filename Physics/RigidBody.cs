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
    /// 对于本物理引擎中刚体的定义。
    /// </summary>
    internal class RigidBody
    {
        /// <summary>
        /// 刚体的X轴。
        /// </summary>
        public float X;
        /// <summary>
        /// 刚体的Y轴。
        /// </summary>
        public float Y;
        /// <summary>
        /// 刚体的力的方向和大小。
        /// </summary>
        public Vector2 Force;
        /// <summary>
        /// 刚体的位置。
        /// </summary>
        public PointF Position
        {
            get => new PointF(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        /// <summary>
        /// 刚体的宽。
        /// </summary>
        public float Width;
        /// <summary>
        /// 刚体的高。
        /// </summary>
        public float Height;
        /// <summary>
        /// 刚体的大小。
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
        /// 获取或设置用于表示此刚体的长方形结构体。
        /// </summary>
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
        /// <summary>
        /// 从x、y、长、宽初始化刚体
        /// </summary>
        /// <param name="x">刚体所在的X</param>
        /// <param name="y">刚体所在的Y</param>
        /// <param name="width">刚体的宽</param>
        /// <param name="height">刚体的长</param>
        public RigidBody(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        /// <summary>
        /// 从PointF（位置）和SizeF（大小）初始化刚体。
        /// </summary>
        /// <param name="position">刚体所在位置。</param>
        /// <param name="size">刚体的大小。</param>
        public RigidBody(PointF position, SizeF size)
        {
            Position = position;
            Size = size;
        }
        /// <summary>
        /// 从长方形结构体初始化一个刚体。
        /// </summary>
        /// <param name="rectangle">表示这个刚体的长方形结构体。</param>
        public RigidBody(RectangleF rectangle)
        {
            Rectangle = rectangle;
        }
    }
}
