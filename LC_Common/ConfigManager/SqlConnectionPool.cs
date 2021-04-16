using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_Common.ConfigManager
{
    public class SqlConnectionPool
    {
        public static string GetConnectionString(SqlConnectionType sqlConnectionType)
        {
            string conn = null;
            switch (sqlConnectionType)
            {
                case SqlConnectionType.Write:
                    conn = ConfigrationManager.SqlConnectionStringWrite;
                    break;
                case SqlConnectionType.Read:
                    conn = Dispatcher(ConfigrationManager.SqlConnectionStringRead);
                    break;
                default:
                    throw new Exception("wrong sqlConnectionType");
            }
            return conn;
        }

        public readonly static object _objectLock = new object();
        //种子
        private static int _Seed = 0;
        /// <summary>
        /// 调度分配--随机
        /// </summary>
        /// <param name="connectionStrings"></param>
        /// <returns></returns>
        private static string Dispatcher(string[] connectionStrings)
        {
            string conn = connectionStrings[new Random(_Seed++).Next(0, connectionStrings.Length)];//平均策略
            //string conn = connectionStrings[_Seed++ % connectionStrings.Length];//轮询 ---seed需要线程安全，加锁
            //权重(有些服务器配置好些，好些配置差些，根据服务器配置情况分配)  2 3 4  9次请求 根据数组[1,1,2,2,2,3,3,3,3]去随机，然后在数据库连接字符串处进去比较选择。
            return conn;
        }

        public enum SqlConnectionType
        {
            /// <summary>
            /// 数据库写操作
            /// </summary>
            Write,
            /// <summary>
            /// 数据库读操作
            /// </summary>
            Read
        }
    }
}
