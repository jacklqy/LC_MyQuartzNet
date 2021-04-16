using Dal;
using Model;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        //log4net
        private LC_Common.Logger logger = new LC_Common.Logger(typeof(TestJob));

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
                    this.logger.Info($"This is TestJob start...");

                    Console.WriteLine($"This is TestJob start {DateTime.Now}");
                    //处理具体业务逻辑todo...
                    //{
                    //    //查询
                    //    tb_log lg = SqlHelper.Find<tb_log>(1);
                    //    //更新
                    //    lg.info = "测试Job更新数据";
                    //    bool result = SqlHelper.Update<tb_log>(lg);
                    //    //新增
                    //    tb_log lgAdd = new tb_log()
                    //    {
                    //        info = "Job测试新增1111",
                    //        methodname = "TestJob1111",
                    //        mqpath = "This is TestJob1111",
                    //        mqpathid = 1,
                    //        createtime = DateTime.Now,
                    //        updatetime = DateTime.Now
                    //    };
                    //    int id = SqlHelper.Insert<tb_log>(lgAdd);
                    //    //删除
                    //    bool iResult = SqlHelper.Delete<tb_log>(13);
                    //}

                    //{
                    //    //批量新增10000条数据测试
                    //    SqlHelper.BatchInsertTest();
                    //}

                    //{
                    //    //sql利用DataReader一行一行的读数据(性能高，每次一行一行获取，所以性能高)
                    //    SqlParameter sqlParameter = new SqlParameter("@id", 5000);
                    //    SqlDataReader sdr = SqlHelper.GetReader("select * from tb_log where id>@id", sqlParameter);
                    //    while (sdr.Read())
                    //    {
                    //        Console.WriteLine(sdr["info"]);
                    //    }
                    //    //关闭释放
                    //    sdr.Close();
                    //}

                    //{
                    //    //sql增删改操作
                    //    string sqlAdd = $"insert into tb_log(mqpathid,mqpath,methodname,info,createtime,updatetime) values(3,'jd.com','GetToken','获取Token异常','{DateTime.Now}','{DateTime.Now}')";
                    //    string sqlUpd = "update tb_log set info='sql增删改' where id=2";
                    //    string sqlDel = "delete tb_log where id=@id";
                    //    SqlParameter sqlParameter = new SqlParameter("@id", 3);
                    //    int iResult1 = SqlHelper.ExecuteNonQuery(sqlAdd, null);
                    //    int iResult2 = SqlHelper.ExecuteNonQuery(sqlUpd, null);
                    //    int iResult3 = SqlHelper.ExecuteNonQuery(sqlDel, sqlParameter);
                    //}

                    //{
                    //    //sql获取Table数据(性能低，因为是将数据一次性获取都内存)
                    //    string sql = "select * from tb_log where id>@id";
                    //    SqlParameter sqlParameter = new SqlParameter("@id", 6000);
                    //    DataTable dt = SqlHelper.GetTable(sql, sqlParameter);
                    //    for (int i = 0; i < dt.Rows.Count; i++)
                    //    {
                    //        Console.WriteLine(dt.Rows[i]["info"].ToString());
                    //    }
                    //}

                    Thread.Sleep(60000);//模拟耗时
                    Console.WriteLine($"This is TestJob end {DateTime.Now}");

                    this.logger.Info($"This is TestJob end...");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.logger.Info($"TestJob异常信息：{ex.Message}");
            }

            #region MyRegion
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
            #endregion

        }
    }
}
