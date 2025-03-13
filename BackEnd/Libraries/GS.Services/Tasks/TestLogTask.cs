using GS.Core.Configuration;
using GS.Core.Domain.BaoCaos;
using GS.Core.Domain.HeThong;
using GS.Core.Infrastructure;
using GS.Services.BaoCaos;
using GS.Services.HeThong;
using GS.Services.Logging;
using Quartz;
using System;
using System.Net;
using System.Threading.Tasks;

namespace GS.Services.Tasks
{
    public class TestLogTask : IJob
    {
        private static bool IsRunning { get; set; }

        Task IJob.Execute(IJobExecutionContext context)
        {
            var scheduleTaskService = EngineContext.Current.Resolve<IScheduleTaskService>();
            var schheduleTask = scheduleTaskService.GetScheduleTaskByName(context.JobDetail.JobType.Name);


            try
            {
                if (TestLogTask.IsRunning && schheduleTask.ENABLED.GetValueOrDefault(true))
                    return null;

                //Start Job
                schheduleTask.LAST_START = DateTime.Now;
                scheduleTaskService.UpdateScheduleTask(schheduleTask);


                var _config = EngineContext.Current.Resolve<GSConfig>();
                var client = new WebClient();
                string url = _config.UrlQLTSC + "NoBase/TestLog";
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