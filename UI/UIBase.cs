using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.UI
{
    internal abstract class UIBase
    {
        public static List<UIBase> VisibleComponents;
        public int Index { get; set; }
        public bool Visible { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public UIBase(int index, float x, float y)
        {
            Index = index;
            Visible = true;
            X = x; 
            Y = y;
            VisibleComponents = new List<UIBase>();
        }
        public virtual void Draw(Graphics g)
        {
        }
    }
}
