using GS.Core.Configuration;
using GS.Core.Infrastructure;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GS.Services.HeThong;

namespace GS.Services.Tasks
{
    public class RenderReportDongBoTask : IJob
    {
        private static bool IsRunning { get; set; }
        Task IJob.Execute(IJobExecutionContext context)
        {
            var scheduleTaskService = EngineContext.Current.Resolve<IScheduleTaskService>();
            var schheduleTask = scheduleTaskService.GetScheduleTaskByName(context.JobDetail.JobType.Name);
            try
            {
                if (RenderReportDongBoTask.IsRunning && schheduleTask.ENABLED.GetValueOrDefault(true))
                    return null;


                schheduleTask.LAST_START = DateTime.Now;
                scheduleTaskService.UpdateScheduleTask(schheduleTask);


                var _config = EngineContext.Current.Resolve<GSConfig>();
                var client = new WebClient();
                string url = _config.UrlQLTSC + "NoBase/RenderReportDongBo";
                client.OpenRead(url);
                //var content = client.DownloadString(_config.UrlRenderReportDongBo);

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
