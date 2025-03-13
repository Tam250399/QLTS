using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using GS.Core.Infrastructure;
using GS.Services.HeThong;
using System.Net;
using GS.Core.Configuration;
using GS.Services.Logging;

namespace GS.Services.Tasks
{
    public class DongBoTaiSanDaNhanTask : IJob
    {

        private static bool IsRunning { get; set; }
        Task IJob.Execute(IJobExecutionContext context)
        {
            var scheduleTaskService = EngineContext.Current.Resolve<IScheduleTaskService>();
            var schheduleTask = scheduleTaskService.GetScheduleTaskByName(context.JobDetail.JobType.Name);
            try
            {
                if (DongBoTaiSanDaNhanTask.IsRunning && schheduleTask.ENABLED.GetValueOrDefault(true))
                    return null;

                //Start Job
                schheduleTask.LAST_START = DateTime.Now;
                scheduleTaskService.UpdateScheduleTask(schheduleTask);
                var _config = EngineContext.Current.Resolve<GSConfig>();
                var client = new WebClient();
                string url = _config.UrlQLTSC + "NoBase/DongBoTaiSanDaNhan?TakeNumber=50";
                //_logger.Information("Task open url " + url+ " at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                //Uri uri = new Uri(url);
                //client.OpenReadAsync(uri);
                client.OpenRead(url);

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
