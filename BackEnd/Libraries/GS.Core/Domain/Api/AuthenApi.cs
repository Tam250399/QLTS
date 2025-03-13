using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Core.Domain.Api
{
    public class AuthenApi
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class OAuth2
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }

    }
}
