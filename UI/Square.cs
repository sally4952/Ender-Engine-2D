using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.UI
{
    /// <summary>
    /// 显示纯色或图片的UI控件。
    /// </summary>
    internal class Square : UIBase
    {
        /// <summary>
        /// Square的宽度。
        /// </summary>
        public float Width { get; set; }
        /// <summary>
        /// Square的高度。
        /// </summary>
        public float Height { get; set; }
        /// <summary>
        /// Square的颜色。
        /// </summary>
        public Color BackgroundColor { get; set; }
        /// <summary>
        /// Square显示的图片（默认为null）。
        /// </summary>
        public Image BackgroundImage { get; set; } = null;
        /// <summary>
        /// 确定图像是否以图像物理大小绘制。
        /// </summary>
        public bool ImageUnscale { get; set; }
        /// <summary>
        /// 初始化Square。
        /// </summary>
        /// <param name="index">（未实现）UI控件的显示层级。</param>
        /// <param name="x">Square的初始X轴位置。</param>
        /// <param name="y">Square的初始Y轴位置。</param>
        /// <param name="width">Square的初始宽度。</param>
        /// <param name="height">Square的初始高度。</param>
        public Square(int index, float x, float y, float width, float height) :
            base(index, x, y)
        {
            Width = width;
            Height = height;
            BackgroundColor = Color.Black;
            BackgroundImage = null;
        }
        /// <summary>
        /// 初始化Square。
        /// </summary>
        /// <param name="index">（未实现）UI控件的显示层级。</param>
        /// <param name="x">Square的初始X轴位置。</param>
        /// <param name="y">Square的初始Y轴位置。</param>
        /// <param name="width">Square的初始宽度。</param>
        /// <param name="height">Square的初始高度。</param>
        /// <param name="bgColor">Square显示的颜色。</param>
        public Square(int index, float x, float y, float width, float height, Color bgColor) : 
            base(index, x, y)
        {
            Width = width;
            Height = height;
            BackgroundColor = bgColor;
            BackgroundImage = null;
        }
        /// <summary>
        /// 初始化Square。
        /// </summary>
        /// <param name="index">（未实现）UI控件的显示层级。</param>
        /// <param name="x">Square的初始X轴位置。</param>
        /// <param name="y">Square的初始Y轴位置。</param>
        /// <param name="width">Square的初始宽度。</param>
        /// <param name="height">Square的初始高度。</param>
        /// <param name="bgImage">Square的初始背景图像。</param>
        public Square(int index, float x, float y, float width, float height, Image bgImage) :
            base(index, x, y)
        {
            Width = width;
            Height = height;
            BackgroundColor = Color.Black;
            BackgroundImage = bgImage;
        }
        /// <summary>
        /// 初始化Square。
        /// </summary>
        /// <param name="index">（未实现）UI控件的显示层级。</param>
        /// <param name="rect">通过一个矩形初始化Square的位置和大小。</param>
        /// <param name="bgColor">Square显示的颜色。</param>
        public Square(int index, RectangleF rect, Color bgColor) :
            base(index, rect.X, rect.Y)
        {
            Width = rect.Width;
            Height = rect.Height;
            BackgroundColor = bgColor;
            BackgroundImage = null;
        }
        /// <summary>
        /// 初始化Square。
        /// </summary>
        /// <param name="index">（未实现）UI控件的显示层级。</param>
        /// <param name="rect">通过一个矩形初始化Square的位置和大小。</param>
        /// <param name="bgImage">Square的初始背景图像。</param>
        public Square(int index, RectangleF rect, Image bgImage) :
            base(index, rect.X, rect.Y)
        {
            Width = rect.Width;
            Height = rect.Height;
            BackgroundColor = Color.Black;
            BackgroundImage = bgImage;
        }
        public override void Draw(Graphics g)
        {
            if (BackgroundImage != null)
            {
                if (ImageUnscale)
                {
                    g.DrawImage(BackgroundImage, X, Y, BackgroundImage.Width, BackgroundImage.Height);
                    return;
                }
                g.DrawImage(BackgroundImage, X, Y, Width, Height);
                return;
            }
            g.FillRectangle(new SolidBrush(BackgroundColor), X, Y, Width, Height);
        }
    }
}
