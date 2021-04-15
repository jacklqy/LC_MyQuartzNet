using LC_QuartzNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_MyConsole
{
    /// <summary>
    /// 1 QuartZ引入&初始化&使用
    /// 2 核心对象Job、Trigger解析
    /// 3 三种Listener扩展订制
    /// 4 可视化界面管理&WindowsService承载
    /// 5 IOC容器结合
    /// 6 自定义的定时调度框架
    /// 
    /// 
    /// 1 定时任务可视化界面管理
    /// 2 配置文件使用和IOC容器结合
    /// 3 WindowsService应用
    /// 
    /// 可视化管理工具：就是为了解决定时任务执行过程中，需要监控--人工介入这种需求---Web系统(只能运行在当前服务器)
    ///   a)建立mvc web项目--4.5.2以上版本
    ///   b)网站NuGet添加引用Quartz(3.0.7.0版本和作业那边的版本要一致)/CrystalQuartz.Remote/System.Diagnostics.DiagnosticSource
    ///     webconfig里面有个SchedulerHost---网站和服务交互的渠道---需要在定时Scheduler启动时做好配置
    ///   c)定时任务的StdSchedulerFactory完成配置
    ///   d)StdSchedulerFactory里面配置的端口必须和web网站里面的Web.config配置文件里面的SchedulerHost节点里面的端口一致。如果监听不到，可能是防火墙问题
    ///   e)启动测试：首先启动定时任务，然后在启动mvc网站，然后在浏览器里面的url地址后面加上CrystalQuartzPanel.axd（如：http://localhost:51448/CrystalQuartzPanel.axd）， 然后回车就可以看到监控页面了。
    /// 
    /// 配置文件使用：把Job和Trigger都通过配置文件指定
    ///   a)初始化Scheduler使用XMLSchedulingDataProcessor
    ///   
    /// WindowsService承载：
    ///   a)添加Windows服务项目
    ///   b)NuGet添加log4net引用
    ///   c)点击Service1来到设计器页面，按F7进入代码，可以做响应的日志配置。
    ///   d)点击Service1来到设计器页面，然后右键'添加安装程序'
    ///       选择serviceProcessInstaller1查看属性，设置Modifiers为public，Account设置为LocalService
    ///       选择serviceInstaller1查看属性StartType设置为Automatic表示自动启动，ServiceName设置服务名称
    ///   e)NuGet添加引用Quartz(3.0.7.0)
    ///   f)根据ReadMe.txt文件安装和卸载Windows服务
    ///   g)如何调试Windows服务？附加到进程，找到刚刚注册的Windows服务，然后在相应的Job里面把断点打好，到了执行时间就会自动进入断点，这样就可以调试了。
    /// WindowsService非常适合做定时服务、托管WCF服务
    /// 
    /// 结合起来，开发者需要写的，就是一个Job业务，再就是做好配置文件Job和Trigger，然后在做一下网站的可视化配置。可以扩展日志、扩展Listener
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
