using EnderEngine2D.Physics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.GameObjects
{
    /// <summary>
    /// 默认游戏中的一个正方形。
    /// </summary>
    internal class Square : GameObjectBase, IDrawable
    {
        /// <summary>
        /// 获取正方形的刚体（表示此Obejct可以掉落）。
        /// </summary>
        public override RigidBody RigidBody { get; set; }
        /// <summary>
        /// 获取此正方形的静态体，表示此Object在引力下是固定不动的。
        /// </summary>
        public override StaticBody StaticBody { get; set; }
        /// <summary>
        /// 正方形的X轴。
        /// </summary>
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
        /// <summary>
        /// 正方形的Y轴。
        /// </summary>
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
        /// <summary>
        /// 正方形的宽。
        /// </summary>
        public float Width;
        /// <summary>
        /// 正方形的长。
        /// </summary>
        public float Height;
        /// <summary>
        /// 正方形要显示的颜色。
        /// </summary>
        public Color Color;
        /// <summary>
        /// 正方形的大小。
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
        /// 获取或设置用于表示这个正方形的RectangleF结构体。
        /// </summary>
        public RectangleF Rectangle
        {
            get => new RectangleF(X, Y, Width, Height);
            set
            {
                X = value.X; Y = value.Y;
                Width = value.Width; Height = value.Height;
            }
        }
        /// <summary>
        /// 初始化此正方形。
        /// </summary>
        /// <param name="rect">用于表示这个正方形位置、大小的长方形结构体。</param>
        /// <param name="color">设置这个正方形显示的颜色。</param>
        public Square(RectangleF rect, Color color)
        {
            Rectangle = rect;
            Color = color;
            RigidBody = new RigidBody(rect);
            StaticBody = new StaticBody(rect);
        }

        /// <summary>
        /// 定义此正方形如何被绘制。
        /// </summary>
        /// <param name="g">绘制使用的Graphics。</param>
        void IDrawable.Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color), Rectangle);
        }
    }
}
