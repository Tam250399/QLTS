using GS.Core.Configuration;
using GS.Core.Infrastructure;
using GS.Services.HeThong;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GS.Services.Tasks
{
    public class DongBoBienDongDaNhanTask : IJob
    {
        private static bool IsRunning { get; set; }
        Task IJob.Execute(IJobExecutionContext context)
        {
            var scheduleTaskService = EngineContext.Current.Resolve<IScheduleTaskService>();
            var schheduleTask = scheduleTaskService.GetScheduleTaskByName(context.JobDetail.JobType.Name);
            try
            {
                if (DongBoBienDongDaNhanTask.IsRunning && schheduleTask.ENABLED.GetValueOrDefault(true))
                    return null;

                //Start Job
                schheduleTask.LAST_START = DateTime.Now;
                scheduleTaskService.UpdateScheduleTask(schheduleTask);
                var _config = EngineContext.Current.Resolve<GSConfig>();
                var client = new WebClient();
                string url = _config.UrlQLTSC + "NoBase/DongBoBienDongDaTiepNhan?TakeNumber=50";
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
