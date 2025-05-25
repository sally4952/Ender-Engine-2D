using EnderEngine2D.ExceptionsHandle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnderEngine2D
{
    internal class Program
    {
        public static MainForm MainForm;
        public static Physics.Engine PhysicalEngine;

        static void Main(string[] args)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            PhysicalEngine = new Physics.Engine(0.1f);
            Task.Run(async () =>
            {
                while (true)
                {
                    PhysicalEngine.Update();
                    await Task.Delay(10);
                }
            });
            MainForm = new MainForm();
            Application.Run(MainForm);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            //new ErrorWindow().ShowDialog(e.Exception);
        }
    }
}
