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
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await Task.Run(() =>
                {
                    Console.WriteLine();
                    Console.WriteLine($"This is GoodJob start {DateTime.Now.ToLongDateString()}");
                    Thread.Sleep(1000);
                    Console.WriteLine($"This is GoodJob end {DateTime.Now.ToLongDateString()}");
                    Console.WriteLine();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
