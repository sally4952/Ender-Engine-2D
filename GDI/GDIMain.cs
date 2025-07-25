using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using EnderEngine2D.GameObjects;
using System.Runtime.InteropServices;
using EnderEngine2D.UI;

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
        public static unsafe void GDIDrawEvent(object _, RenderEventArgs e)
        {
            var gl = Program.MainForm.Output.OpenGL;
            gl?.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl?.ClearColor(Level.Now.BackgroundColor.R, Level.Now.BackgroundColor.G, Level.Now.BackgroundColor.B, Level.Now.BackgroundColor.A);
            if (VisibleObjects.Objects == null)
            {
                return;
            }
            foreach (var obj in VisibleObjects.Objects)
            {
                if (obj == null)
                {
                    continue;
                }
                ((GameObjectBase)obj).X -= Camera.X;
                ((GameObjectBase)obj).Y -= Camera.Y;
                obj.Draw(e.Graphics);
                ((GameObjectBase)obj).X += Camera.X;
                ((GameObjectBase)obj).Y += Camera.Y;
            }

            if (UIContainer.IsGaming || (UIContainer.Now == null) || (UIContainer.Now.Length == 0))
            {
                goto Label_01;
            }
            foreach (var con in UIContainer.Now)
            {
                e.Graphics.FillRectangle(new SolidBrush(con.BackgroundColor), 0, 0, GameScreenConvert.PercentageToScreen(1, DirectionType.X), GameScreenConvert.PercentageToScreen(1, DirectionType.Y));
                foreach (var comp in con.Components)
                {
                    comp.Value.Draw(e.Graphics);
                }
            }
        Label_01:;
        }
    }
}
