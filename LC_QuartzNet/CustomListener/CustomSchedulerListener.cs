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
    public class CustomSchedulerListener : ISchedulerListener
    {
        public async Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} JobAdded {jobDetail.Description}");
            });
        }

        public async Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} JobDeleted");
            });
        }

        public async Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} JobInterrupted");
            });
        }

        public async Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} JobPaused");
            });
        }

        public async Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} JobResumed");
            });
        }

        public async Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} JobScheduled");
            });
        }

        public async Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} JobsPaused");
            });
        }

        public async Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} JobsResumed");
            });
        }

        public async Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} JobUnscheduled");
            });
        }

        public async Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} SchedulerError");
            });
        }

        public async Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} SchedulerInStandbyMode");
            });
        }

        public async Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} SchedulerShutdown");
            });
        }

        public async Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} SchedulerShuttingdown");
            });
        }

        public async Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} SchedulerStarted");
            });
        }

        public async Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} SchedulerStarting");
            });
        }

        public async Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} SchedulingDataCleared");
            });
        }

        public async Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} TriggerFinalized");
            });
        }

        public async Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"This is {nameof(CustomSchedulerListener)} TriggerPaused");
            });
        }

        public async Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} TriggerResumed");
            });
        }

        public async Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} TriggersPaused");
            });
        }

        public async Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                //Console.WriteLine($"This is {nameof(CustomSchedulerListener)} TriggersResumed");
            });
        }
    }
}
