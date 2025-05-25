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
    internal static class GDIMain
    {
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
