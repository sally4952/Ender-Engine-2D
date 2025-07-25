using EnderEngine2D.Assets;
using EnderEngine2D.ExceptionsHandle;
using EnderEngine2D.GameObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnderEngine2D
{
    /// <summary>
    /// 主程序类，所有数据的集中。
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// 获取主窗口。
        /// </summary>
        public static MainForm MainForm;
        /// <summary>
        /// 获取正在使用的物理引擎。
        /// </summary>
        public static Physics.Engine PhysicalEngine;
        /// <summary>
        /// 程序入口点。
        /// </summary>
        static void Main(string[] args)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            PhysicalEngine = new Physics.Engine(1.1f);
            Task.Run(async () =>
            {
                while (true)
                {
                    PhysicalEngine.Update();
                    await Task.Delay(1);
                }
            });
            MainForm = new MainForm();
            GameObjectBase.Init(new Level { BackgroundColor = Color.Black, Objects = new Dictionary<string, GameObjectBase>
            {
                { "PLAYER", new Player(new RectangleF(400, 100, 100, 100), Color.White) },
                { "GROUND", new Ground(new RectangleF(0, 864, 1920, 10), Color.White) },
            }});
            UI.UIContainer.IsGaming = true;
            Application.Run(MainForm);
        }
        /// <summary>
        /// 定义当线程引发异常时该如何操作。去掉注释可以让它在引发异常时弹出错误窗口。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            new ErrorWindow().ShowDialog(e.Exception);
        }
    }
}
