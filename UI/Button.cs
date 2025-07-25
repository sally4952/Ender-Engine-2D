using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnderEngine2D.UI
{
    /// <summary>
    /// 按钮UI控件。
    /// </summary>
    internal class Button : UIBase
    {
        private Thread mThread;
        private Label Label;
        /// <summary>
        /// 按钮显示的文本。
        /// </summary>
        public string Text
        {
            get => Label.Text; set => Label.Text = value;
        }
        private Square Square;
        /// <summary>
        /// 按钮所在的X轴位置。
        /// </summary>
        public new float X { get => Square.X; set => Square.X = value; }
        /// <summary>
        /// 按钮所在的Y轴位置。
        /// </summary>
        public new float Y { get => Square.Y; set => Square.Y = value; }
        /// <summary>
        /// 按钮的宽度。
        /// </summary>
        public float Width
        {
            set
            {
                AutoWidth = false;
                Square.Width = value;
            }
            get
            {
                if (AutoWidth)
                {
                    float w;
                    using (var g = Graphics.FromHwnd(Program.MainForm.Handle))
                    {
                        w = g.MeasureString(Text, TextFont).Width;
                    }
                    return w;
                }
                return Square.Width;
            }
        }
        /// <summary>
        /// 按钮的高度。
        /// </summary>
        public float Height
        {
            set
            {
                AutoHeight = false;
                Square.Height = value;
            }
            get
            {
                if (AutoHeight)
                {
                    float h;
                    using (var g = Graphics.FromHwnd(Program.MainForm.Handle))
                    {
                        h = g.MeasureString(Text, TextFont).Height;
                    }
                    return h;
                }
                return Square.Height;
            }
        }
        /// <summary>
        /// （未实现）根据文本高度计算按钮高度。
        /// </summary>
        public bool AutoWidth { get; set; } = true;
        /// <summary>
        /// （未实现）根据文本宽度计算按钮宽度。
        /// </summary>
        public bool AutoHeight { get; set; } = true;
        /// <summary>
        /// 按钮文本的颜色。
        /// </summary>
        public Color FrontColor { get => Label.FrontColor; set => Label.FrontColor = value; }
        /// <summary>
        /// 按钮的背景色。
        /// </summary>
        public Color BackColor { get => Square.BackgroundColor; set => Square.BackgroundColor = value; }
        /// <summary>
        /// 当鼠标悬浮在按钮上时按钮的背景色。
        /// </summary>
        public Color HoverColor { get; set; }
        /// <summary>
        /// 按钮显示的文本。
        /// </summary>
        public Font TextFont { get => Label.TextFont; set => Label.TextFont = value; }
        /// <summary>
        /// 当按钮被按下时触发的事件。
        /// </summary>
        public Action OnClick;
        /// <summary>
        /// 初始化按钮UI控件。
        /// </summary>
        /// <param name="index">（未实现）UI控件的显示层级。</param>
        /// <param name="text">按钮的初始文本。</param>
        /// <param name="x">按钮的初始X轴位置。</param>
        /// <param name="y">按钮的初始Y轴位置。</param>
        /// <param name="frontColor">按钮的文本颜色。</param>
        /// <param name="backColor">按钮的背景色。</param>
        /// <param name="hoverColor">当鼠标悬浮在按钮上时显示的背景色。</param>
        /// <param name="textFont">用于按钮文本的字体。</param>
        /// <param name="autoWidth">（未实现）根据文本宽度自动计算按钮宽度。</param>
        /// <param name="autoHeight">（未实现）根据文本高度自动计算按钮高度。</param>
        /// <param name="onClick">当鼠标点击按钮时触发的事件（默认为null）。</param>
        public Button(int index, string text, float x, float y, Color frontColor, Color backColor, Color hoverColor, Font textFont, bool autoWidth = false, bool autoHeight = false, Action onClick = null) :
            base(index, x, y)
        {
            Label = new Label(index - 1, x, y, text);
            Text = text;
            Square = new Square(index, x, y, 10, 10);
            X = x;
            Y = y;
            AutoWidth = autoWidth;
            AutoHeight = autoHeight;
            FrontColor = frontColor;
            BackColor = backColor;
            HoverColor = hoverColor;
            TextFont = textFont;
            OnClick = onClick;

            mThread = new Thread(async () =>
            {
                var bColor = BackColor;
                while (true)
                {
                    if (!UIContainer.IsGaming)
                    {
                        if (new RectangleF(X, Y, Width, Height).IntersectsWith(new RectangleF(Cursor.Position.X, Cursor.Position.Y, 1, 1)))
                        {
                            this.BackColor = HoverColor;
                            if (Program.MainForm.MouseInput.IsLeftButtonDown)
                            {
                                OnClick?.Invoke();
                            }
                        }
                        else
                        {
                            this.BackColor = bColor;
                        }
                    }
                    await Task.Delay(50);
                }
            });
            mThread.Start();
        }

        public override void Draw(Graphics g)
        {
            Square.Draw(g);
            Label.Draw(g);
        }
    }
}
