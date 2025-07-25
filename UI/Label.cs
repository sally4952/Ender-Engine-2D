using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.UI
{
    /// <summary>
    /// 用于显示文本的UI控件。
    /// </summary>
    internal class Label : UIBase
    {
        /// <summary>
        /// UI控件显示的文本。
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 文本的颜色。
        /// </summary>
        public Color FrontColor { get; set; }
        /// <summary>
        /// 文本的字体。
        /// </summary>
        public Font TextFont { get; set; } = new Font("微软雅黑", 20f);
        /// <summary>
        /// 初始化Label控件。
        /// </summary>
        /// <param name="index">（未实现）UI控件的显示层级。</param>
        /// <param name="x">Label初始的X轴位置。</param>
        /// <param name="y">Label初始的Y轴位置。</param>
        /// <param name="text">Label的初始文本。</param>
        /// <param name="frontColor">Label文本使用的字体。</param>
        public Label(int index, float x, float y, string text, Color frontColor) : 
            base(index, x, y)
        {
            Text = text;
            FrontColor = frontColor;
        }
        /// <summary>
        /// 初始化Label控件。
        /// </summary>
        /// <param name="index">（未实现）UI控件的显示层级。</param>
        /// <param name="position">Label的初始位置。</param>
        /// <param name="text">Label的初始文本。</param>
        /// <param name="frontColor">Label文本使用的字体。</param>
        public Label(int index, PointF position, string text, Color frontColor) :
            base(index, position.X, position.Y)
        {
            Text = text;
            FrontColor = frontColor;
        }
        /// <summary>
        /// 初始化Label控件。
        /// </summary>
        /// <param name="index">（未实现）UI控件的显示层级。</param>
        /// <param name="x">Label初始的X轴位置。</param>
        /// <param name="y">Label初始的Y轴位置。</param>
        /// <param name="text">Label的初始文本。</param>
        public Label(int index, float x, float y, string text) :
            base(index, x, y)
        {
            Text = text;
            FrontColor = Color.White;
        }
        /// <summary>
        /// 初始化Label控件。
        /// </summary>
        /// <param name="index">（未实现）UI控件的显示层级。</param>
        /// <param name="position">Label的初始位置。</param>
        /// <param name="text">Label的初始文本。</param>
        public Label(int index, PointF position, string text) :
            base(index, position.X, position.Y)
        {
            Text = text;
            FrontColor = Color.White;
        }
        public override void Draw(Graphics g)
        {
            g.DrawString(Text, TextFont, new SolidBrush(FrontColor), X, Y);
        }
    }
}
