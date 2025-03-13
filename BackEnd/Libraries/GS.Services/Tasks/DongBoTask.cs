using GS.Services.Logging;
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
    public class DongBoTask : IJob
    {
        private static bool IsRunning { get; set; }

        Task IJob.Execute(IJobExecutionContext context)
        {
            var scheduleTaskService = EngineContext.Current.Resolve<IScheduleTaskService>();
            var schheduleTask = scheduleTaskService.GetScheduleTaskByName(context.JobDetail.JobType.Name);
            try
            {
                if (DongBoTask.IsRunning && schheduleTask.ENABLED.GetValueOrDefault(true))
                    return null;

                //Start Job
                schheduleTask.LAST_START = DateTime.Now;
                scheduleTaskService.UpdateScheduleTask(schheduleTask);


                var _config = EngineContext.Current.Resolve<GSConfig>();
                var client = new WebClient();
                var content = client.OpenRead(_config.UrlQLTSC + "NoBase/DongBoTaiSanApi");
                //var content = client.OpenRead(_config.UrlQLTSC + "NoBase/DongBoTaiSanApi?DonViId=18871&LoaiBienDongId=1&NguonTaiSanId=1");

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
