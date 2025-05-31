using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.Physics
{
    /// <summary>
    /// 此物理引擎中定义的静态体（不受重力约束的）。
    /// </summary>
    internal class StaticBody
    {
        /// <summary>
        /// 静态体的X轴。
        /// </summary>
        public float X;
        /// <summary>
        /// 静态体的Y轴。
        /// </summary>
        public float Y;
        /// <summary>
        /// 静态体的位置。
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
        /// 静态体的宽。
        /// </summary>
        public float Width;
        /// <summary>
        /// 静态体的高。
        /// </summary>
        public float Height;
        /// <summary>
        /// 静态体的大小。
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
        /// 用于表示此静态体的长方形结构体。
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
        /// 从x、y、长、宽初始化静态体
        /// </summary>
        /// <param name="x">静态体所在的X</param>
        /// <param name="y">静态体所在的Y</param>
        /// <param name="width">静态体的宽</param>
        /// <param name="height">静态体的长</param>
        public StaticBody(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        /// <summary>
        /// 从PointF（位置）和SizeF（大小）初始化静态体。
        /// </summary>
        /// <param name="position">静态体所在位置。</param>
        /// <param name="size">静态体的大小。</param>
        public StaticBody(PointF position, SizeF size)
        {
            Position = position;
            Size = size;
        }
        /// <summary>
        /// 从长方形结构体初始化一个静态体。
        /// </summary>
        /// <param name="rectangle">表示这个静态体的长方形结构体。</param>
        public StaticBody(RectangleF rectangle)
        {
            Rectangle = rectangle;
        }
    }
}
