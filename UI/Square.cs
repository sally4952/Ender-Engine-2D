using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.UI
{
    internal class Square : UIBase
    {
        public float Width { get; set; }
        public float Height { get; set; }
        public Color BackgroundColor { get; set; }
        public Image BackgroundImage { get; set; }
        public bool ImageUnscale { get; set; }
        public Square(int index, float x, float y, float width, float height) :
            base(index, x, y)
        {
            Width = width;
            Height = height;
            BackgroundColor = Color.Black;
            BackgroundImage = null;
        }
        public Square(int index, float x, float y, float width, float height, Color bgColor) : 
            base(index, x, y)
        {
            Width = width;
            Height = height;
            BackgroundColor = bgColor;
            BackgroundImage = null;
        }
        public Square(int index, float x, float y, float width, float height, Image bgImage) :
            base(index, x, y)
        {
            Width = width;
            Height = height;
            BackgroundColor = Color.Black;
            BackgroundImage = bgImage;
        }
        public Square(int index, RectangleF rect, Color bgColor) :
            base(index, rect.X, rect.Y)
        {
            Width = rect.Width;
            Height = rect.Height;
            BackgroundColor = bgColor;
            BackgroundImage = null;
        }
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
