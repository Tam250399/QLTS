using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api
{
    public class LogApi
    {
        public String Method { get; set; }
        public String URL { get; set; }
        public String Token { get; set; }
        public String MethodName { get; set; }
        public String BodyInput { get; set; }
        public String UrlInput { get; set; }
    }
}
