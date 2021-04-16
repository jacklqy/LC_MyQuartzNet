using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LC_QuartzNet.CustomListener
{
    public class CustomTriggerListener : ITriggerListener
    {
        public string Name => "CustomTriggerListener";

        /// <summary>
        /// 完成了
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="context"></param>
        /// <param name="triggerInstructionCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomTriggerListener)} TriggerComplete {context.JobDetail.Description}");
            });
        }

        public async Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomTriggerListener)} TriggerFired {context.JobDetail.Description}");
            });
        }

        public async Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomTriggerListener)} TriggerMisfired");
            });
        }

        /// <summary>
        /// 要不要放弃Job
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomTriggerListener)} VetoJobExecution {context.JobDetail.Description}");
            });
            return false;//false才能继续执行
        }
    }
}
