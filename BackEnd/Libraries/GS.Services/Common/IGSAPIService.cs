using GS.Core.Configuration;
using GS.Core.Domain.Common;
using GS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GS.Services.Common
{
    public interface IGSAPIService
    {
        Task PostGSApi(string ApiAction, object paramInput = null, string token = null, string ApiUrl = null);
        Task<List<T>> GetListGSApi<T>(string ApiAction, object paramInput = null, string token = null);
        Task<T> GetObjectGSApi<T>(string ApiAction, object paramInput = null, string token = null, string ApiUrl = null);
        Task<List<T>> PostListGSApi<T>(string ApiAction, object paramInput = null, string token = null, string ApiUrl = null);
        Task<T> PostObjectGSApi<T>(string ApiAction, object paramInput = null, string token = null, string ApiUrl = null);
        Task<T> PutObjectGSApi<T>(string ApiAction, object paramInput = null, string token = null, string ApiUrl = null);
        string GetToken(bool IsWebApi = false, decimal NguoiDungId = 0);
        Task<T> GetObjectGSApiByBody<T>(string ApiAction, object paramInput = null, string token = null, string ApiUrl = null);
        Task<T> PostObjectGSApiWithStringContent<T>(string ApiAction, StringContent stringContent = null, string token = null, string ApiUrl = null);
        Task<T> DeleteObjectGSApi<T>(string ApiAction, object paramInput = null, string token = null, string ApiUrl = null);
        Task<HttpResponseMessage> PostAsResponseMessage(string ApiAction, object paramInput = null, string token = null, string ApiUrl = null);
        Task<List<T>> PostListObjectGSApiWithStringContent<T>(string ApiAction, StringContent stringContent = null, string token = null, string ApiUrl = null);
        string GetTokenKhoCSDLQG(ApiKhoCSDL apiKhoCSDL = null, decimal? nguonId = null);

        Task<T> GetObjectGSApiKho<T>(string ApiAction, object paramInput = null, string token = null, string ApiUrl = null);
        Task<String> PostObjectGSApiWithStringContentReturnString(string ApiAction, StringContent stringContent = null, string token = null, string ApiUrl = null);
    }

}
