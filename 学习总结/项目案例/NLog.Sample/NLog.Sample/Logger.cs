using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NLog.Sample
{
    public class Logger
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static ReaderWriterLockSlim logWriteLock = new ReaderWriterLockSlim();
        /// <summary>
        /// 非常详细的日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Trace(string message, Exception exception = null)
        {
            logger.Trace(message);
        }
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Debug(string message, Exception exception = null)
        {
            logger.Debug(message);
        }
        /// <summary>
        /// 信息消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Info(string message, Exception exception = null)
        {
            logger.Info(message);
        }
        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Warn(string message, Exception exception = null)
        {
            logger.Warn(message);
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Error(string message, Exception exception = null)
        {
            logger.Error(message);
        }
        /// <summary>
        /// 非常严重的错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Fatal(string message, Exception exception = null)
        {
            logger.Fatal(message);
        }
        /// <summary>
        /// 自定义日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="folder"></param>
        public static void Write(string message, string folder = "folder")
        {
            var thread = new Thread(() => WriteLog(message, folder));
            thread.IsBackground = false;
            thread.Start();
        }
        private static void WriteLog(string message, string folder)
        {
            var dt = DateTime.Now;
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                logWriteLock.EnterWriteLock();

                var dir = string.Format(@"{0}\LogFiles\{1}\{2}", baseDir, dt.ToString("yyyy-MM-dd"), folder);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                var path = Path.Combine(dir + @"\" + dt.ToString("yyyy-MM-dd HH") + ".log");
                using (var sw = new StreamWriter(path, true))
                {
                    sw.WriteLine("时间：{0}\n内容：{1}\n\n", dt.ToString("yyyy-MM-dd HH:mm:ss"), message);
                    sw.Close();
                }
            }
            finally
            {
                logWriteLock.ExitWriteLock();
            }
        }
    }
}
