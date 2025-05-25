using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.ExceptionsHandle
{
    internal class Logging : IDisposable
    {
        public enum LogType
        {
            INFO,
            WARNING,
            ERROR,
        }

        public string LogName;
        public string FilePath;
        public string WritedStrings;
        private FileStream mLoggingStream;

        public Action<string>[] OnLogging = new Action<string>[0];

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
