#pragma warning disable CS8500
#pragma warning disable CS4014

using EnderEngine2D.GameObjects;
using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnderEngine2D
{
    internal partial class MainForm : Form
    {
        public Inputs.Keyboard.Keyboard KeyboardInput;

        public MainForm()
        {
            InitializeComponent();
            this.Size = Screen.PrimaryScreen.Bounds.Size;
            Output.Size = this.Size;
            this.FormClosing += MainForm_FormClosing;
            this.Text = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyTitleAttribute)))?.Title;
            this.Output.GDIDraw += GDI.GDIMain.GDIDrawEvent;
            Output.Focus();

            #if DEBUG
            Output.DrawFPS = true;
            #endif

            unsafe
            {
                fixed (OpenGLControl* control = &Output)
                    KeyboardInput = new Inputs.Keyboard.Keyboard((Control*)control);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((IDisposable)KeyboardInput).Dispose();
        }
    }
}
