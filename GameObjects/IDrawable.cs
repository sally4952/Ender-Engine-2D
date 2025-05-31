using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.GameObjects
{
    /// <summary>
    /// 使得一个Object可以被绘制的接口。
    /// </summary>
    internal interface IDrawable
    {
        /// <summary>
        /// 定义当要绘制这个Object时需要执行怎样的操作。
        /// </summary>
        /// <param name="g">绘制传入的Graphics，用于绘图。</param>
        void Draw(Graphics g);
    }
}
