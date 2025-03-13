using GS.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Factories.Common
{
    public interface ICommonFactory
    {
        String GetTokenKhoCSDLQG(ApiKhoCSDL apiKhoCSDL = null, decimal? nguonId = null);
        ApiKhoCSDL GetInfoApiKhoCSDL();
    }
}
