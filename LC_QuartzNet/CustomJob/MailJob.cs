using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_QuartzNet.CustomJob
{
    /// <summary>
    /// 邮件任务
    /// </summary>
    //[PersistJobDataAfterExecution]//执行后保留作业数据
    [DisallowConcurrentExecution]//不允许并发执行，只有执行完了才能进入(重点)
    public class MailJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
