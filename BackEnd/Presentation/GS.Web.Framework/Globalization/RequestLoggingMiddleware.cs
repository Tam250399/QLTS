using Microsoft.AspNetCore.Http;
using GS.Core.Configuration;
using GS.Services.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Web.Framework.Globalization
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;


        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IApiLog logger, GSConfig gsConfig)
        {
            var injectedRequestStream = new MemoryStream();

            try
            {
                if (gsConfig.IsWriteLogFile)
                {
                    var requestBody = "";
                    using (var bodyReader = new StreamReader(context.Request.Body, encoding: Encoding.UTF8))
                    {
                        requestBody = await bodyReader.ReadToEndAsync();
                        var bytesToWrite = Encoding.UTF8.GetBytes(requestBody);
                        injectedRequestStream.Write(bytesToWrite, 0, bytesToWrite.Length);
                        injectedRequestStream.Seek(0, SeekOrigin.Begin);
                        context.Request.Body = injectedRequestStream;
                    }
                    var requestHeader = context.Request.Headers;
                    var requestPara = new Dictionary<string, object>();
                    if (context.Request.QueryString.HasValue)
                    {
                        var valueCollection = System.Web.HttpUtility.ParseQueryString(context.Request.QueryString.Value);
                        foreach (var key in valueCollection.AllKeys)
                        {
                            requestPara.Add(key, valueCollection[key]);
                        }
                    }
                    else
                        requestPara = null;
                    logger.Trace(context.Request, context.Request.Method, requestPara, requestBody);
                }
                await _next.Invoke(context);
            }
            finally
            {
                injectedRequestStream.Dispose();
            }

        }
    }
}
