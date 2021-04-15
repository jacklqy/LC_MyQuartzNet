using LC_QuartzNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_MyConsole
{
    /// <summary>
    /// 1 quartZ引入&初始化&使用
    /// 2 核心对象Job、Trigger解析
    /// 3 三种Listener扩展订制
    /// 4 可视化界面管理&WindowsService承载
    /// 5 IOC容器结合
    /// 6 自定义的定时调度框架
    /// 
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DispatchManager.Init().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
