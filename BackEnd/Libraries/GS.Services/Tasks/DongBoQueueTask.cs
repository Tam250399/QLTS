using GS.Core.Infrastructure;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GS.Services.Logging;
using GS.Core.Configuration;
using System.Net;
using GS.Services.HeThong;

namespace GS.Services.Tasks
{
    public class DongBoQueueTask : IJob
    {
        private static bool IsRunning { get; set; }

        Task IJob.Execute(IJobExecutionContext context)
        {
            var scheduleTaskService = EngineContext.Current.Resolve<IScheduleTaskService>();
            var schheduleTask = scheduleTaskService.GetScheduleTaskByName(context.JobDetail.JobType.Name);
            try
            {
                if (DongBoQueueTask.IsRunning && schheduleTask.ENABLED.GetValueOrDefault(true))
                    return null;

                //Start Job
                schheduleTask.LAST_START = DateTime.Now;
                scheduleTaskService.UpdateScheduleTask(schheduleTask);
                var _config = EngineContext.Current.Resolve<GSConfig>();
                //_logger.Information("DongBoQueueTask started at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                var client = new WebClient();
                var content = client.OpenRead(_config.UrlQLTSC + "NoBase/SendRequestApi");

                //End Job
                schheduleTask.LAST_END = DateTime.Now;
                schheduleTask.LAST_SUCCESS = DateTime.Now;
                scheduleTaskService.UpdateScheduleTask(schheduleTask);

            }
            catch
            {
                schheduleTask.LAST_END = DateTime.Now;
                scheduleTaskService.UpdateScheduleTask(schheduleTask);
            }
            return null;
        }
    }
}
