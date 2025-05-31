using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnderEngine2D.ExceptionsHandle
{
    /// <summary>
    /// 这是一个错误窗口，当游戏内有异常抛出时，会弹出。（但是现在还并没有正式加入）
    /// </summary>
    internal partial class ErrorWindow : Form
    {
        /// <summary>
        /// 日志记录器。
        /// </summary>
        public Logging Logging;

        /// <summary>
        /// 初始化错误窗口。
        /// </summary>
        public ErrorWindow()
        {
            InitializeComponent();
            var ta = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyTitleAttribute));
            this.Text = ta?.Title;
            Logging = new Logging();

            Task.Run(async () =>
            {
                while (true)
                {
                    textBox1.Text = Logging.WritedStrings;
                    await Task.Delay(500);
                }
            });

            this.FormClosing += ErrorWindow_FormClosing;

            Task.Run(async () =>
            {
                await Task.Delay(1000);
                Logging.Write("正在保存游戏并退出...请稍后...", Logging.LogType.INFO);
                // *保存游戏*
                await Task.Delay(1000);
                Logging.Write("保存成功！正在退出...", Logging.LogType.INFO);
                Environment.Exit(0);
            });
        }

        /// <summary>
        /// 当错误窗口关闭时发生的事件，阻止用户关闭窗口。
        /// </summary>
        private void ErrorWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// 重写ShowDialog。请使用此ShowDialog。
        /// </summary>
        /// <param name="exception"></param>
        public void ShowDialog(Exception exception)
        {
            Logging.Write($"引发了异常：{exception}{Environment.NewLine}可在日志文件{Path.GetFullPath(Logging.FilePath)}中查看详细信息。{Environment.NewLine}", Logging.LogType.ERROR);
            base.ShowDialog();
        }

        [Obsolete("原ShowDialog已废弃。请使用新版ShowDialog(Exception)。", true)]
        public new DialogResult ShowDialog()
        {
            return DialogResult.None;
        }
    }
}
