using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using NLog;
using GS.Core;
using GS.Services.Logging;
using GS.Core.Domain.Api;

namespace GS.WebApi.Infrastructure
{

    public class NLogger : IApiLog
    {
        private static NLog.ILogger _logger = LogManager.GetCurrentClassLogger();
        public readonly GS.Services.Logging.ILogger _gslogger;

        public NLogger(GS.Services.Logging.ILogger gsLoger)
        {
            _gslogger = gsLoger;
        }
        public void Trace(HttpRequest request, string methodName, object paraInput, string contentBody)
        {
            var message = new StringBuilder();

            LogApi logInfo = new LogApi();
            if (request != null)
            {
                message.Append("Method: " + request.Method + Environment.NewLine);
                message.Append("URL: " + request.Path.Value + Environment.NewLine);
                logInfo.Method = request.Method;
                logInfo.URL = request.Path.Value;
                StringValues authorizationToken;
                request.Headers.TryGetValue("Authorization", out authorizationToken);
                if (!string.IsNullOrEmpty(authorizationToken.ToString()))
                {
                    message.Append("Token: " + authorizationToken.ToString() + Environment.NewLine);
                    logInfo.Token = authorizationToken.ToString();
                }

            }
            message.Append("Method Name: " + methodName + Environment.NewLine);
            logInfo.MethodName = methodName;
            if (!string.IsNullOrEmpty(contentBody))
            {
                message.Append("Body Input: " + contentBody + Environment.NewLine);
                logInfo.BodyInput = contentBody;
            }
            if (paraInput != null)
            {
                message.Append("Url Input: " + paraInput.toStringJson(true));
                logInfo.UrlInput = paraInput.toStringJson(true);
            }
            try
            {
                _gslogger.Information(message: "Api Log " + DateTime.Now.toDateVNString(true) + " " + logInfo.URL, dataJson: logInfo.toStringJson());
            }
            catch (Exception ex) { }
            Information(message.ToString());
        }
        public void Information(string message)
        {
            _logger.Info(message);
        }

        public void Warning(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }
    }

}
