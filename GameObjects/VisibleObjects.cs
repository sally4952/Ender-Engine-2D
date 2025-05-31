#pragma warning disable CS8500

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.GameObjects
{
    /// <summary>
    /// 确定有哪些可绘制物体会被绘制到屏幕上。
    /// </summary>
    internal static class VisibleObjects
    {
        /// <summary>
        /// 要绘制到屏幕上的物体。
        /// </summary>
        public static unsafe IDrawable[] Objects = new IDrawable[0];
    }
}
