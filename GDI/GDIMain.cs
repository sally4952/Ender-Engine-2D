using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using EnderEngine2D.GameObjects;
using System.Runtime.InteropServices;

namespace EnderEngine2D.GDI
{
    /// <summary>
    /// 对引擎内部提供一套与绘图有关的方法。
    /// </summary>
    internal static class GDIMain
    {
        /// <summary>
        /// 在引擎内部使用。这是每当输出窗口刷新时所需要调用的方法，用于绘制物体。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static unsafe void GDIDrawEvent(object sender, RenderEventArgs e)
        {
            var gl = Program.MainForm.Output.OpenGL;
            gl?.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            if (VisibleObjects.Objects == null)
            {
                return;
            }
            foreach (var obj in VisibleObjects.Objects)
            {
                obj->Draw(e.Graphics);
            }
        }
    }
}
