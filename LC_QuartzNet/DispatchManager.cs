using LC_QuartzNet.CustomJob;
using LC_QuartzNet.CustomListener;
using LC_QuartzNet.CustomLog;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Quartz.Simpl;
using Quartz.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_QuartzNet
{
    /// <summary>
    /// ******************调度管理者******************
    /// 
    /// 1 NuGet引入QuartZ，当前使用的3.0.7.0版本。
    /// 2 三点核心对象：
    ///      IScheduler：单元/实例，在这里去完成定时任务的配置。只有单元启动，里面的作业才能正常运行。
    ///      IJob：任务，定时执行动作就是Job
    ///      ITrigger：定时策略
    ///      就可以基本完成定时任务
    /// 3 传参数问题  
    ///    a)传参方式一：jobDetail.JobDataMap.Add("parameter1", "111");
    ///    b)传参方式二：trigger.JobDataMap.Add("parameter5", "111000");
    ///    c)接收参数方式：context.JobDetail.JobDataMap/context.Trigger.JobDataMap/context.MergedJobDataMap(冲突的时候回覆盖，以后者为准)
    /// 4 为啥是Job+Trigger？更灵活
    ///   拆分真的挺好，方便复用，一个Job绑定多个Trigger
    ///   刚才的传参数，就也应该分开
    ///   比如：归档数据业务的任务(业务表+归档表---把30天之前的数据都移到归档表)
    ///   订单表(30天)-》死循环Task--检查日期--做一次归档
    ///   物流表(60天)-》死循环Task--检查日期--做一次归档
    ///   ......100个类似需求呢？
    ///   希望作业能通用，表不一样，操作差不多，job完成通用逻辑
    ///   需要业务表行+归档表，这个由IJobDetail传递参数来决定
    ///   不同的detail可能频率不同，所以就需要拆分ITrigger
    ///   执行频率不同的，就需要不同的ITrigger，这个时候可能条件是不一样的
    ///   按天执行，就只检查Day；
    ///   按小时执行，就的细致到Hour
    ///   按分钟执行，会细致到minute；
    ///   所以trigger也的传参数
    /// 5 常用Trigger：
    ///    a)Cron：表达式方式，可以灵活订制时间策略，详情请看文档
    ///    b)Simple：从什么时候开始，间隔多久执行重复操作，可以限制最大次数
    /// 6 Listener：框架的各个环节---事件能做的监听
    ///     ISchedulerListener
    ///     IJobListener
    ///     ITriggerListener
    /// 7 ILogProvider可以展示框架运行的一些信息
    /// </summary>
    public class DispatchManager
    {
        public async static Task Init()
        {
            #region 自定义框架日志
            LogProvider.SetCurrentLogProvider(new CustomLogProvider());
            #endregion

            #region scheduler 创建单元/实例
            Console.WriteLine("初始化scheduler......");
            //StdSchedulerFactory factory = new StdSchedulerFactory();
            //IScheduler scheduler = await factory.GetScheduler();
            IScheduler scheduler = await ScheduleManager.BuildScheduler();

            {
                //使用配置文件
                XMLSchedulingDataProcessor processor = new XMLSchedulingDataProcessor(new SimpleTypeLoadHelper());
                //quartz_jobs.xml右键属性设置为‘始终复杂’
                await processor.ProcessFileAndScheduleJobs("~/CfgFiles/quartz_jobs.xml", scheduler);
            }

            {
                //添加ISchedulerListener监听
                scheduler.ListenerManager.AddSchedulerListener(new CustomSchedulerListener());
                //添加IJobListener监听
                scheduler.ListenerManager.AddJobListener(new CustomJobListener());
                //添加ITriggerListener监听
                scheduler.ListenerManager.AddTriggerListener(new CustomTriggerListener());
            }
            await scheduler.Start();
            #endregion

            #region 通过配置文件后，这里就可以不需要了
            ////1
            //{
            //    #region Job 创建作业
            //    //创建作业
            //    IJobDetail jobDetail = JobBuilder.Create<TestJob>()
            //         .WithIdentity("TestJob", "Group1")//分组必须一致
            //         .WithDescription("This is TestJob")
            //         .Build();

            //    //jobDetail传参
            //    jobDetail.JobDataMap.Add("parameter1", "table1");
            //    jobDetail.JobDataMap.Add("parameter2", "table2");
            //    jobDetail.JobDataMap.Add("parameter3", 333);
            //    jobDetail.JobDataMap.Add("Year", DateTime.Now.Year);
            //    #endregion

            //    #region Trigger 创建时间策略
            //    ////创建时间策略 Cron
            //    //ITrigger trigger = TriggerBuilder.Create()
            //    //    .WithIdentity("testtrigger1", "group1")
            //    //    //.StartAt(new DateTimeOffset(DateTime.Now.AddSeconds(5))) //5秒后启动
            //    //    .StartNow()//立即启动
            //    //               //.WithCronSchedule("0 0/1 * * * ?")//每隔一分钟
            //    //     .WithCronSchedule("0/10 * * * * ?")//每隔10秒运行一次
            //    //    .WithDescription("This is TestJob's Trigger")
            //    //    .Build();

            //    //创建时间策略 Simple
            //    ITrigger trigger = TriggerBuilder.Create()
            //            .WithIdentity("TestJobTrigger1", "Group1")//分组必须一致
            //                                                      //.StartNow()//立即启动
            //            .StartAt(new DateTimeOffset(DateTime.Now.AddSeconds(5)))//5秒后启动
            //            .WithSimpleSchedule(x => x
            //                .WithIntervalInSeconds(10) //10秒循环一次
            //                .RepeatForever()//一直执行
            //                                //.WithRepeatCount(2) //只循环10次
            //                )
            //            .WithDescription("This is TestJob's Trigger")
            //            .Build();

            //    //trigger传参
            //    trigger.JobDataMap.Add("parameter5", "存储过程");
            //    trigger.JobDataMap.Add("parameter6", "222000");
            //    trigger.JobDataMap.Add("parameter7", 333000);
            //    trigger.JobDataMap.Add("Year", DateTime.Now.Year + 10);//加一下试试 
            //    #endregion

            //    await scheduler.ScheduleJob(jobDetail, trigger);
            //    Console.WriteLine("scheduler作业添加完成......");
            //}

            ////2
            //{
            //    #region Job 创建作业
            //    //创建作业
            //    IJobDetail jobDetail = JobBuilder.Create<GoodJob>()
            //        .WithIdentity("GoodJob", "Group2")//分组必须一致
            //        .WithDescription("This is GoodJob")
            //        .Build();
            //    #endregion

            //    #region Trigger 创建时间策略
            //    //创建时间策略 Cron
            //    ITrigger trigger = TriggerBuilder.Create()
            //        .WithIdentity("GoodJobTrigger1", "Group2")//分组必须一致
            //        .StartAt(new DateTimeOffset(DateTime.Now.AddSeconds(10))) //10秒后启动
            //                                                                  //.StartNow()//立即启动
            //                                                                  //.WithCronSchedule("0 0/1 * * * ?")//每隔一分钟
            //         .WithCronSchedule("0/10 * * * * ?")//每隔10秒运行一次
            //        .WithDescription("This is GoodJob's Trigger")
            //        .Build();
            //    #endregion

            //    await scheduler.ScheduleJob(jobDetail, trigger);
            //    Console.WriteLine("scheduler作业添加完成......");
            //} 
            #endregion
        }
    }
}
