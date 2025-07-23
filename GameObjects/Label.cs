using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.GameObjects
{
    internal class Label : GameObjectBase, IDrawable
    {
        public virtual string Text { get; set; }
        public Font Font { get; set; }
        public Color ForeColor { get; set; }
        void IDrawable.Draw(Graphics g)
        {
            g.DrawString(Text, Font, new SolidBrush(ForeColor), X, Y);
        }
    }
}
