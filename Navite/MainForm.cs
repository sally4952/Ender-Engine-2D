#pragma warning disable CS8500
#pragma warning disable CS4014
#define BASE_CODE

using EnderEngine2D.GameObjects;
using EnderEngine2D.GameSave;
using EnderEngine2D.Navite;
using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnderEngine2D
{
    /// <summary>
    /// 图像输出的主窗口，同时是游戏的主窗口。
    /// </summary>
    internal partial class MainForm : Form
    {
        /// <summary>
        /// 此窗口默认的键盘监听器。
        /// </summary>
        public Inputs.Keyboard.Keyboard KeyboardInput;
#if BASE_CODE
        public Square Square;
        public Square Ground0;
        public Square Ground1;
#endif
        /// <summary>
        /// 窗口的初始化方法。
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            this.Size = Screen.PrimaryScreen.Bounds.Size;
            Output.Size = this.Size;
            this.FormClosing += MainForm_FormClosing;
            this.Text = PublicAssemblyInfo.Title;
            this.Output.GDIDraw += GDI.GDIMain.GDIDrawEvent;
            Output.Focus();

            #if DEBUG
            Output.DrawFPS = true;
#endif

#if BASE_CODE
            unsafe
            {
                fixed (OpenGLControl* control = &Output)
                    KeyboardInput = new Inputs.Keyboard.Keyboard((Control*)control);
            }

            Task.Run(async () =>
            {
                var jumping = false;
                while (true)
                {
                    if (KeyboardInput.GetKeyDown(Keys.Escape))
                    {
                        Application.Exit();
                    }
                    if (KeyboardInput.GetKeyDown(Keys.A))
                    {
                        Square.X -= 4;
                    }
                    if (KeyboardInput.GetKeyDown(Keys.D))
                    {
                        Square.X += 4;
                    }
                    if (KeyboardInput.GetKeyDown(Keys.Space))
                    {
                        if (Square.RigidBody.Force.Y != 0f)
                        {
                            goto Label_02;
                        }
                        if (!jumping)
                        {
                            jumping = true;
                            Task.Run(async () =>
                            {
                                for (float y = 30f; y >= 0; y -= 1f)
                                {
                                    Square.Y -= y;
                                    await Task.Delay(50);
                                }
                                jumping = false;
                            });
                        }
                    }
                    Label_02:
                    await Task.Delay(10);
                }
            });

            Square = new Square(new RectangleF(100, GameScreenConvert.PercentageToScreen(0.8f, DirectionType.Y), 100, 100), Color.White);
            Ground0 = new Square(new RectangleF(0, GameScreenConvert.PercentageToScreen(0.88f, DirectionType.Y), GameScreenConvert.PercentageToScreen(0.5f, DirectionType.X), 10), Color.White);
            Ground1 = new Square(new RectangleF(1300, GameScreenConvert.PercentageToScreen(0.88f, DirectionType.Y), GameScreenConvert.PercentageToScreen(0.3f, DirectionType.X), 10), Color.White);
            Task.Run(() =>
            {
                unsafe
                {
                    fixed (Square* ground1 = &Ground1)
                    fixed (Square* ground0 = &Ground0)
                    fixed (Square* square = &Square)
                    {
                        VisibleObjects.Objects = new[] { (IDrawable*)square, (IDrawable*)ground0, (IDrawable*)ground1, };
                        while (true) ;
                    }
                }
            });
            Program.PhysicalEngine.RigidBodies.Add(0, Square.RigidBody);
            Program.PhysicalEngine.StaticBodies.Add(1, Ground0.StaticBody);
            Program.PhysicalEngine.StaticBodies.Add(2, Ground1.StaticBody);
#endif
        }
        /// <summary>
        /// 定义当窗口关闭时需要执行怎样的操作。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((IDisposable)KeyboardInput)?.Dispose();
        }
    }
}
