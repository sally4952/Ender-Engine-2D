using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.GameObjects
{
    /// <summary>
    /// 表示一个在游戏中只显示文字的GameObject。
    /// </summary>
    internal class Label : GameObjectBase, IDrawable
    {
        /// <summary>
        /// Label要显示的文本。
        /// </summary>
        public virtual string Text { get; set; }
        /// <summary>
        /// 确定此Label使用什么字体显示文本。
        /// </summary>
        public Font Font { get; set; }
        /// <summary>
        /// Label的文本颜色。
        /// </summary>
        public Color ForeColor { get; set; }

        void IDrawable.Draw(Graphics g)
        {
            g.DrawString(Text, Font, new SolidBrush(ForeColor), X, Y);
        }
    }
}
