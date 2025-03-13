#region Using namespaces...
using Microsoft.AspNetCore.Mvc.Filters;
using GS.Core;
using GS.Core.Configuration;
using GS.Core.Infrastructure;
using GS.WebApi.Infrastructure;
using System;
using System.IO;
using System.Text;
using GS.Services.Logging;
#endregion

namespace GS.Api.Infrastructure.Api
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(EngineContext.Current.Resolve<GSConfig>().IsWriteLogFile)
            {
                var logger = EngineContext.Current.Resolve<IApiLog>();
               
                logger.Trace(context.HttpContext.Request, context.ActionDescriptor.DisplayName, context.ActionArguments, "");

            }            
            base.OnActionExecuting(context);
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }
       
    }
}