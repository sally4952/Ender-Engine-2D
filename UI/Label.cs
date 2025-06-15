using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.UI
{
    internal class Label : UIBase
    {
        public string Text { get; set; }
        public Color FrontColor { get; set; }
        public Font TextFont { get; set; }
        public Label(int index, float x, float y, string text, Color frontColor) : 
            base(index, x, y)
        {
            Text = text;
            FrontColor = frontColor;
        }
        public Label(int index, PointF position, string text, Color frontColor) :
            base(index, position.X, position.Y)
        {
            Text = text;
            FrontColor = frontColor;
        }
        public Label(int index, float x, float y, string text) :
            base(index, x, y)
        {
            Text = text;
            FrontColor = Color.White;
        }
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
