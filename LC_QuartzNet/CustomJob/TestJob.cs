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
    public class TestJob : IJob
    {
        //静态字段来做共享数据
        private static object TempData = new object();//或者存在在第三方或磁盘
        public TestJob()
        {
            //Console.WriteLine("TestJob被构造了...");
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await Task.Run(() =>
                {
                    Console.WriteLine();
                    Console.WriteLine($"This is TestJob start {DateTime.Now.ToLongDateString()}");
                    Thread.Sleep(1000);
                    Console.WriteLine($"This is TestJob end {DateTime.Now.ToLongDateString()}");
                    Console.WriteLine();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //try
            //{
            //    await Task.Run(() =>
            //    {
            //        {
            //            //接收参数方式一
            //            Console.WriteLine();
            //            Console.WriteLine("**********************************");
            //            JobDataMap dataMap = context.JobDetail.JobDataMap;
            //            Console.WriteLine(dataMap.Get("parameter1"));
            //            Console.WriteLine(dataMap.Get("parameter2"));
            //            Console.WriteLine(dataMap.GetInt("parameter3"));
            //            Console.WriteLine(dataMap.GetInt("Year"));//
            //        }
            //        {
            //            //接收参数方式二
            //            Console.WriteLine();
            //            Console.WriteLine("**********************************");
            //            JobDataMap dataMap = context.Trigger.JobDataMap;
            //            Console.WriteLine(dataMap.Get("parameter5"));
            //            Console.WriteLine(dataMap.Get("parameter6"));
            //            Console.WriteLine(dataMap.GetInt("parameter7"));
            //            Console.WriteLine(dataMap.GetInt("Year"));//
            //        }
            //        {
            //            //接收参数方式三
            //            Console.WriteLine();
            //            Console.WriteLine("**********************************");
            //            JobDataMap dataMap = context.MergedJobDataMap;
            //            Console.WriteLine(dataMap.Get("parameter1"));
            //            Console.WriteLine(dataMap.Get("parameter2"));
            //            Console.WriteLine(dataMap.GetInt("parameter3"));
            //            Console.WriteLine(dataMap.Get("parameter5"));
            //            Console.WriteLine(dataMap.Get("parameter6"));
            //            Console.WriteLine(dataMap.GetInt("parameter7"));
            //            Console.WriteLine(dataMap.GetInt("Year"));//冲突的时候回覆盖，以后者为准
            //        }

            //        Console.WriteLine();
            //        Console.WriteLine("**********************************");
            //        Console.WriteLine($"This is {Thread.CurrentThread.ManagedThreadId} {DateTime.Now}");
            //        //具体业务操作...
            //        Console.WriteLine("**********************************");
            //    });
            //}
            //catch (Exception ex)//异常内部消化，然后做日志，尽量不让任务停掉
            //{
            //    Console.WriteLine(ex.Message);
            //}
            
        }
    }
}
