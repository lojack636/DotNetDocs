using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4Net.Utils
{
    /// <summary>
    /// 日志内容
    /// </summary>
    public class LoggerMessage
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 日志等级
        /// </summary>
        public LoggerLevel Level { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception { get; set; }

    }
}
