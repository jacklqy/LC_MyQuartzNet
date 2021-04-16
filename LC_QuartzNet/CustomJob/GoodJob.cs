using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LC_QuartzNet.CustomJob
{
    //[PersistJobDataAfterExecution]//执行后保留作业数据
    [DisallowConcurrentExecution]//不允许并发执行，只有执行完了才能进入(重点)
    public class GoodJob : IJob
    {
        //静态字段来做共享数据
        private static object TempData = new object();//或者存在在第三方或磁盘
        //log4net
        private LC_Common.Logger logger = new LC_Common.Logger(typeof(TestJob));

        public GoodJob()
        {
            //Console.WriteLine("GoodJob被构造了...");
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await Task.Run(() =>
                {
                    this.logger.Info($"This is GoodJob start...");

                    Console.WriteLine($"This is GoodJob start {DateTime.Now}");
                    //处理具体业务逻辑todo...
                    Thread.Sleep(5000);
                    Console.WriteLine($"This is GoodJob end {DateTime.Now}");

                    this.logger.Info($"This is GoodJob end...");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.logger.Info($"GoodJob异常信息：{ex.Message}");
            }
        }
    }
}
