using GS.Core.Configuration;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.KT;
using GS.Services.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GS.Services.Tasks
{
    public class ChotTaiSanTask : IJob
    {
        private static bool IsRunning { get; set; }
        System.Threading.Tasks.Task IJob.Execute(IJobExecutionContext context)
        {
            //try
            //{
            //	if (ChotTaiSanTask.IsRunning)
            //		return null;
            //	var _config = EngineContext.Current.Resolve<GSConfig>();
            //	var url = _config.UrlSendRequest;
            //	var client = new WebClient();
            //	var content = client.DownloadString(url);
            //}
            //catch
            //{
            //}
            //return null;
            
            try
            {
                if (ChotTaiSanTask.IsRunning)
                    return null;
                ChotTaiSanTask.IsRunning = true;
                var _logger = EngineContext.Current.Resolve<ILogger>();
                var _config = EngineContext.Current.Resolve<GSConfig>();
                Guid g = Guid.NewGuid();
                _logger.Information(g + " ChotTaiSanTask started at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                var client = new WebClient();
                var content = client.OpenRead(_config.UrlQLTSC + "NoBase/ChotHaoMonTaiSan");
                ChotTaiSanTask.IsRunning = false;
                //_logger.Information(g + "ChotTaiSanTask finished at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (Exception ex)
            {
                ChotTaiSanTask.IsRunning = false;
            }
            return null;
        }
    }
}
