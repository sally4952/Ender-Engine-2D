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
    internal class Button : UIBase
    {
        private Thread mThread;
        private Label Label;
        public string Text
        {
            get => Label.Text; set => Label.Text = value;
        }
        private Square Square;
        public new float X { get => Square.X; set => Square.X = value; }
        public new float Y { get => Square.Y; set => Square.Y = value; }
        public float Width
        {
            get
            {
                if (AutoWidth)
                {
                    float w;
                    using (var g = Graphics.FromHwnd(Program.MainForm.Output.Handle))
                    {
                        w = g.MeasureString(Text, TextFont).Width;
                    }
                    return w;
                }
                return Square.Width;
            }
        }
        public float Height
        {
            get
            {
                if (AutoHeight)
                {
                    float h;
                    using (var g = Graphics.FromHwnd(Program.MainForm.Output.Handle))
                    {
                        h = g.MeasureString(Text, TextFont).Height;
                    }
                    return h;
                }
                return Square.Height;
            }
        }
        public bool AutoWidth { get; set; } = true;
        public bool AutoHeight { get; set; } = true;
        public Color FrontColor { get => Label.FrontColor; set => Label.FrontColor = value; }
        public Color BackColor { get => Square.BackgroundColor; set => Square.BackgroundColor = value; }
        public Color HoverColor { get; set; }
        public Font TextFont { get => Label.TextFont; set => Label.TextFont = value; }
        public Action OnClick;
        public Button(int index, string text, float x, float y, Color frontColor, Color backColor, Color hoverColor, Font textFont, bool autoWidth = true, bool autoHeight = true, Action onClick = null) :
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
                while (true)
                {
                    if (!UIContainer.IsGaming)
                    {
                        if (Cursor.Position.X > X)
                        {
                            if (Cursor.Position.X < X + Width)
                            {
                                if (Cursor.Position.Y > Y)
                                {
                                    if (Cursor.Position.Y < Y + Height)
                                    {
                                    }
                                }
                            }
                        }
                    }
                    await Task.Delay(50);
                }
            });
        }

        public override void Draw(Graphics g)
        {
            Square.Draw(g);
            Label.Draw(g);
        }
    }
}
