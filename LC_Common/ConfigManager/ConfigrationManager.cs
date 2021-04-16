using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace LC_Common
{
    /// <summary>
    /// 配置文件读取
    /// </summary>
    public class ConfigrationManager
    {
        //有了IOC在去注入--容器单列
        static ConfigrationManager()
        {
            _SqlConnectionStringWrite = ConfigurationManager.AppSettings["ConnectionStringWrite"];
            _SqlConnectionStringRead = ConfigurationManager.AppSettings["ConnectionStringRead"].Split(',');
        }

        private static string _SqlConnectionStringWrite = null;
        public static string SqlConnectionStringWrite
        {
            get
            {
                return _SqlConnectionStringWrite;
            }
        }

        private static string[] _SqlConnectionStringRead = null;
        public static string[] SqlConnectionStringRead
        {
            get
            {
                return _SqlConnectionStringRead;
            }
        }
    }
}
