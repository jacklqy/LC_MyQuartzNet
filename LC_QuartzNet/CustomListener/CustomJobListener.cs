using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LC_QuartzNet.CustomListener
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomJobListener : IJobListener
    {
        //指定名称
        public string Name => "CustomJobListener";

        /// <summary>
        /// Job作业执行被拒绝(本来该我执行了，但是Trigger把我放弃了)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"This is {nameof(CustomJobListener)} JobExecutionVetoed {context.JobDetail.Description}");
            });
        }

        /// <summary>
        /// Job准备执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"This is {nameof(CustomJobListener)} JobToBeExecuted {context.JobDetail.Description}");
            });
        }

        /// <summary>
        /// Job已经执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jobException"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"This is {nameof(CustomJobListener)} JobWasExecuted {context.JobDetail.Description}");
            });
        }
    }
}
