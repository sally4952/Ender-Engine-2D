using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.ExceptionsHandle
{
    /// <summary>
    /// 日志记录器。
    /// </summary>
    internal class Logging : IDisposable
    {
        /// <summary>
        /// 记录消息时确定这条消息的严重性。
        /// </summary>
        public enum LogType
        {
            /// <summary>
            /// 只是一个信息出现。
            /// </summary>
            INFO,
            /// <summary>
            /// 作为警告出现。
            /// </summary>
            WARNING,
            /// <summary>
            /// 错误的消息出现。
            /// </summary>
            ERROR,
        }

        /// <summary>
        /// 这个日志的名称。
        /// </summary>
        public string LogName;
        /// <summary>
        /// 日志文件的路径。
        /// </summary>
        public string FilePath;
        /// <summary>
        /// 已写入日志的文字。
        /// </summary>
        public string WritedStrings;
        /// <summary>
        /// 用于写入日志文件的Stream。
        /// </summary>
        private FileStream mLoggingStream;

        /// <summary>
        /// 当记录日志时会发生的Action。
        /// </summary>
        public Action<string>[] OnLogging = new Action<string>[0];

        /// <summary>
        /// 日志记录器的初始化。
        /// </summary>
        public Logging()
        {
            LogName = $"Log_{ExceptionInformations.Default.LoggingCount}";
            FilePath = $".\\Logs\\{LogName}.log";
            if (!Directory.Exists(".\\Logs"))
                Directory.CreateDirectory(".\\Logs");
            mLoggingStream = File.Create(FilePath);

            ExceptionInformations.Default.LoggingCount++;
            ExceptionInformations.Default.Save();
        }

        /// <summary>
        /// 将一条消息写入日志。
        /// </summary>
        /// <param name="message">这条消息的内容。</param>
        /// <param name="type">这条消息的严重性。</param>
        public void Write(string message, LogType type)
        {
            message = $"[{DateTime.Now.ToString("g")}][{type}]:{message}{Environment.NewLine}";
            byte[] data = Encoding.UTF8.GetBytes(message);
            try
            {
                mLoggingStream.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                message += $"{Environment.NewLine}[{DateTime.Now.ToString("g")}][{LogType.WARNING}]:无法写入日志文件。详情：{e.Message}。";
            }
            WritedStrings += message;
            if (OnLogging == null)
                goto Label_01;
            foreach (var item in OnLogging)
            {
                item(message);
            }
            Label_01:
            Console.Write(message);
        }

        void IDisposable.Dispose()
        {
            mLoggingStream.Close();
            mLoggingStream.Dispose();
        }
    }
}
