using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_QuartzNet.CustomLog
{
    /// <summary>
    /// 日志提供者
    /// </summary>
    public class CustomLogProvider : ILogProvider
    {
        public Logger GetLogger(string name)
        {
            return new Logger((level, func, exception, parameters) =>
            {
                if (level >= LogLevel.Info && func != null)
                {
                    Console.WriteLine($"[{ DateTime.Now.ToLongTimeString()}] [{ level}] { func()} {string.Join(";", parameters.Select(p => p == null ? " " : p.ToString()))}  自定义日志{name}");
                }
                return true;
            });
        }

        public IDisposable OpenMappedContext(string key, string value)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }
    }
}
