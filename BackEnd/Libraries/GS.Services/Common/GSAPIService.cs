using GS.Core;
using GS.Core.Configuration;
using GS.Core.Domain.Api;
using GS.Core.Domain.HeThong;
using GS.Services.Authentication;
using GS.Services.HeThong;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GS.Services.Common
{
    public class GSAPIService : IGSAPIService
    {
        private readonly GSConfig _config;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IHoatDongService _hoatDongService;
        private readonly ICauHinhService _cauHinhService;

        public GSAPIService(GSConfig config,
            INguoiDungService nguoiDungService,
            IAuthenticationService authenticationService,
            IHoatDongService hoatDongService,
            ICauHinhService cauHinhService)
        {
            _config = config;
            this._nguoiDungService = nguoiDungService;
            this._authenticationService = authenticationService;
            this._hoatDongService = hoatDongService;
            this._cauHinhService = cauHinhService;
        }

        #region utility

        private string getParaUrl(object paraInput)
        {
            if (paraInput != null)
            {
                var props = paraInput.GetType().GetProperties();
                string _paras = "";
                foreach (var p in props)
                {
                    var _val = p.GetValue(paraInput, null);
                    if (_val == null)
                        continue;
                    if (string.IsNullOrEmpty(_paras))
                        _paras = string.Format("?{0}={1}", p.Name, _val);
                    else
                        _paras = _paras + string.Format("&{0}={1}", p.Name, _val);
                }
                return _paras;
            }
            return "";
        }

        private void addHeader(HttpClient client, string ApiUrlKhoCSDL, string token)
        {
            client.BaseAddress = new System.Uri(ApiUrlKhoCSDL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        #endregion utility

        /// <summary>
        /// lay thong tin theo list object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ApiAction"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<T>> GetListGSApi<T>(string ApiAction, object paramInput = null, string token = null)
        {
            string ApiUrlKhoCSDL = _config.ApiKhoCSDL.ApiUrlKhoCSDL + ApiAction;
            var dataret = new List<T>();
            ApiUrlKhoCSDL = ApiUrlKhoCSDL + getParaUrl(paramInput);
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(1440);
                addHeader(client, ApiUrlKhoCSDL, token);
                HttpResponseMessage response;
                //_hoatDongService.InsertHoatDong("CallWebApi", "Chờ gọi Api nhận tài sản kho", client, null);
                response = await client.GetAsync(client.BaseAddress);
                _hoatDongService.InsertHoatDong(enumHoatDong.ResponseApi, "Gửi dữ liệu sang kho", response);
                //_hoatDongService.InsertHoatDong("CalledWebApi", "Đã gọi nhận tài sản kho", response, null);
                if (response.IsSuccessStatusCode)
                {
                    //string json
                    dataret = await response.Content.ReadAsAsync<List<T>>();
                }
            }
            return dataret;
        }

        public async Task<T> GetObjectGSApi<T>(string ApiAction, object paramInput = null, string token = null, string ApiUrl = null)
        {
            if (string.IsNullOrEmpty(ApiUrl))
            {
                ApiUrl = _config.ApiKhoCSDL.ApiUrlKhoCSDL + ApiAction;
            }
            else
            {
                ApiUrl = ApiUrl + ApiAction;
            }
            T dataret = default(T);
            ApiUrl = ApiUrl + getParaUrl(paramInput);
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(1440);
                addHeader(client, ApiUrl, token);
                HttpResponseMessage response;
                response = await client.GetAsync(client.BaseAddress);
                _hoatDongService.InsertHoatDong(enumHoatDong.ResponseApi, "Gửi dữ liệu sang kho", response);
                if (response.IsSuccessStatusCode)
                {
                    dataret = await response.Content.ReadAsAsync<T>();
                }
            }
            return dataret;
        }

        public async Task<T> GetObjectGSApiKho<T>(string ApiAction, object paramInput = null, string token = null, string ApiUrl = null)
        {
            if (string.IsNullOrEmpty(ApiUrl))
            {
                ApiUrl = _config.ApiKhoCSDL.ApiUrlKhoCSDL + ApiAction;
            }
            else
            {
                ApiUrl = ApiUrl + ApiAction;
            }
            T dataret = default(T);
            ApiUrl = ApiUrl + getParaUrl(paramInput);
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(1440);
                addHeader(client, ApiUrl, token);
                HttpResponseMessage response;
                response = await client.GetAsync(client.BaseAddress);
                _hoatDongService.InsertHoatDong(enumHoatDong.ResponseApi, "Lấy dữ liệu từ kho", response);
                if (response.IsSuccessStatusCode)
                {
                    dataret = await response.Content.ReadAsAsync<T>();
                }
            }
            return dataret;
        }

        public async Task<List<T>> PostListGSApi<T>(string ApiAction, object paramInput = null, string token = null, string ApiUrl = null)
        {
            string _Url;
            if (string.IsNullOrEmpty(ApiUrl))
            {
                _Url = _config.ApiKhoCSDL.ApiUrlKhoCSDL + ApiAction;
            }
            else
            {
                _Url = ApiUrl + ApiAction;
            }
            var dataret = new List<T>();
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(1440);
                addHeader(client, _Url, token);
                HttpResponseMessage response;

                response = await client.PostAsJsonAsync(client.BaseAddress, paramInput);
                _hoatDongService.InsertHoatDong(enumHoatDong.ResponseApi, "Gửi dữ liệu api", response);

                if (response.IsSuccessStatusCode)
                {
                    //string json
                    dataret = await response.Content.ReadAsAsync<List<T>>();
                }
            }
            return dataret;
        }

        public async Task<T> PostObjectGSApi<T>(string ApiAction, object paramInput = null, string token = null, string ApiUrl = null)
        {
            string _Url;
            if (string.IsNullOrEmpty(ApiUrl))
            {
                _Url = _config.ApiKhoCSDL.ApiUrlKhoCSDL + ApiAction;
            }
            else
            {
                _Url = ApiUrl + ApiAction;
            }
            T dataret = default(T);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(1440);
                    addHeader(client, _Url, token);
                    HttpResponseMessage response;
                    response = await client.PostAsJsonAsync(client.BaseAddress, paramInput);
                    _hoatDongService.InsertHoatDong(enumHoatDong.ResponseApi, "Gửi dữ liệu api ", response);
                    if (response.IsSuccessStatusCode)
                    {
                        dataret = await response.Content.ReadAsAsync<T>();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

            return dataret;
        }

        public async Task<T> PutObjectGSApi<T>(string ApiAction, object paramInput = null, string token = null, string ApiUrl = null)
        {
            string _Url;
            if (string.IsNullOrEmpty(ApiUrl))
            {
                _Url = _config.ApiKhoCSDL.ApiUrlKhoCSDL + ApiAction;
            }
            else
            {
                _Url = ApiUrl + ApiAction;
            }
            T dataret = default(T);
            try
            {
                var handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, cert, cetChain, policyErrors) =>
                    {
                        return true;
                    };

                using (HttpClient client = new HttpClient(handler))
                {
                    client.Timeout = TimeSpan.FromMinutes(1440);
                    addHeader(client, _Url, token);
                    HttpResponseMessage response;
                    response = await client.PutAsJsonAsync(client.BaseAddress, paramInput);
                    _hoatDongService.InsertHoatDong("ResetPassword", "Gửi dữ liệu api");
                    if (response.IsSuccessStatusCode)
                    {
                        dataret = await response.Content.ReadAsAsync<T>();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

            return dataret;
        }

        /// <summary>
        /// Ham nay goi api update du lieu, ko can biet thanh cong hay khong
        /// </summary>
        /// <param name="ApiAction"></param>
        /// <param name="paramInput"></param>
        /// <param name="token"></param>
        public async Task PostGSApi(string ApiAction, object paramInput = null, string token = null, string ApiUrl = null)
        {
            if (string.IsNullOrEmpty(ApiUrl))
            {
                ApiUrl = _config.ApiKhoCSDL.ApiUrlKhoCSDL + ApiAction;
            }
            else
            {
                ApiUrl = ApiUrl + ApiAction;
            }
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(1440);
                addHeader(client, ApiUrl, token);
                HttpResponseMessage response;
                response = await client.PostAsJsonAsync(client.BaseAddress, paramInput);
                _hoatDongService.InsertHoatDong(enumHoatDong.ResponseApi, "Gửi dữ liệu sang api", response);

                if (response.IsSuccessStatusCode)
                {
                }
            }
        }

        /// <summary>
        /// get token của webapi
        /// </summary>
        /// <param name="IsWebApi"></param>
        /// <returns></returns>
        public string GetToken(bool IsWebApi = false, decimal NguoiDungId = 0)
        {
            string token = null;
            if (IsWebApi)
            {
                NguoiDung admin = _nguoiDungService.GetNguoiDungById(NguoiDungId);
                if (admin == null)
                {
                    admin = _nguoiDungService.GetNguoiDungByUsername("admin");
                }
                token = _authenticationService.GenerateToken(admin);
            }

            return token;
        }

        public string GetTokenKhoCSDLQG(ApiKhoCSDL apiKhoCSDL = null, decimal? nguonId = null)
        {
            if (apiKhoCSDL == null)
            {
                //authorization server parameters owned from the client
                //this values are issued from the authorization server to the client through a separate process (registration, etc...)
                Uri authorizationServerTokenIssuerUri = new Uri(_config.ApiKhoCSDL.UrlTokenKhoCSDLQG + "aprserver/oauth/token");
                //access token request
                string username = _config.ApiKhoCSDL.Username;
                //if (nguonId == (decimal)enumNguonTaiSan.DKTS40)
                //    username = _config.ApiKhoCSDL.Username;
                //else if (nguonId == (decimal)enumNguonTaiSan.QLTSNN)
                //    username = _config.ApiKhoCSDL.UsernameTsnn;
                //else if (nguonId == (decimal)enumNguonTaiSan.QLTSC)
                //    username = _config.ApiKhoCSDL.UsernameTsc;
                try
                {
                    string rawJwtToken = RequestTokenToAuthorizationServer(
                         authorizationServerTokenIssuerUri,
                         _config.ApiKhoCSDL.ClientId,
                         _config.ApiKhoCSDL.ClientSecret,
                         username,
                         _config.ApiKhoCSDL.Password)
                        .GetAwaiter()
                        .GetResult();
                    var objAuthen = JsonConvert.DeserializeObject<OAuth2>(rawJwtToken);
                    return objAuthen.access_token;
                }
                catch
                {
                    return "1";

                }
            }
            else
            {
                //authorization server parameters owned from the client
                //this values are issued from the authorization server to the client through a separate process (registration, etc...)
                Uri authorizationServerTokenIssuerUri = new Uri(apiKhoCSDL.ApiUrlKhoCSDL + "aprserver/oauth/token");
                //access token request
                string username = apiKhoCSDL.Username;
                //if (nguonId == (decimal)enumNguonTaiSan.DKTS40)
                //    username = apiKhoCSDL.Username;
                //else if (nguonId == (decimal)enumNguonTaiSan.QLTSNN)
                //    username = apiKhoCSDL.UsernameTsnn;
                try
                {
                    string rawJwtToken = RequestTokenToAuthorizationServer(
                         authorizationServerTokenIssuerUri,
                         apiKhoCSDL.ClientId,
                         apiKhoCSDL.ClientSecret,
                         username,
                         apiKhoCSDL.Password)
                        .GetAwaiter()
                        .GetResult();
                    var objAuthen = JsonConvert.DeserializeObject<OAuth2>(rawJwtToken);
                    return objAuthen.access_token;
                }
                catch
                {
                    return "1";
                }
            }
        }

        private async Task<string> RequestTokenToAuthorizationServer(Uri uriAuthorizationServer, string clientId, string clientSecret, string username, string password)
        {
            string credentials = String.Format("{0}:{1}", clientId, clientSecret);

            HttpResponseMessage responseMessage;
            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

                HttpRequestMessage tokenRequest = new HttpRequestMessage(HttpMethod.Post, uriAuthorizationServer);
                HttpHeadAttribute httpHead = new HttpHeadAttribute();
                HttpContent httpContent = new FormUrlEncodedContent(
                    new[]
                    {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("auth_chain", ""),
                    });

                tokenRequest.Content = httpContent;
                responseMessage = await client.SendAsync(tokenRequest);
            }
            return await responseMessage.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// get by Json body
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ApiAction"></param>
        /// <param name="paramInput"></param>
        /// <param name="token"></param>
        /// <param name="ApiUrl"></param>
        /// <returns></returns>
        public async Task<T> GetObjectGSApiByBody<T>(string ApiAction, object paramInput = null, string token = null, string ApiUrl = null)
        {
            if (string.IsNullOrEmpty(ApiUrl))
            {
                ApiUrl = _config.ApiKhoCSDL.ApiUrlKhoCSDL + ApiAction;
            }
            else
            {
                ApiUrl = ApiUrl + ApiAction;
            }
            T dataret = default(T);
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(1440);
                addHeader(client, ApiUrl, token);
                HttpResponseMessage response;
                StringContent content = new StringContent(JsonConvert.SerializeObject(paramInput, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }), Encoding.UTF8, "application/json");
                HttpRequestMessage requestMessage = new HttpRequestMessage()
                {
                    Content = content,
                    RequestUri = client.BaseAddress,
                    Method = HttpMethod.Get
                };
                response = await client.SendAsync(requestMessage);
                _hoatDongService.InsertHoatDong(enumHoatDong.ResponseApi, "Gửi dữ liệu sang kho", response);
                if (response.IsSuccessStatusCode)
                {
                    dataret = await response.Content.ReadAsAsync<T>();
                }
            }
            return dataret;
        }

        /// <summary>
        /// Post string content
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ApiAction"></param>
        /// <param name="stringContent"></param>
        /// <param name="token"></param>
        /// <param name="ApiUrl"></param>
        /// <returns></returns>
        public async Task<T> PostObjectGSApiWithStringContent<T>(string ApiAction, StringContent stringContent = null, string token = null, string ApiUrl = null)
        {
            if (string.IsNullOrEmpty(ApiUrl))
            {
                ApiUrl = _config.ApiKhoCSDL.ApiUrlKhoCSDL + ApiAction;
            }
            else
            {
                ApiUrl = ApiUrl + ApiAction;
            }
            T dataret = default(T);
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(1440);
                addHeader(client, ApiUrl, token);
                HttpResponseMessage response;
                var data = stringContent.ReadAsStringAsync().Result;
                _hoatDongService.InsertHoatDong(enumHoatDong.ResponseApi, "Bắt đầu gửi dữ liệu api " + ApiUrl, data);
                response = await client.PostAsync(client.BaseAddress, stringContent);
                List<MediaTypeFormatter> formatters = new List<MediaTypeFormatter>();
                var a = new JsonMediaTypeFormatter();
                a.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
                formatters.Add(a);
                _hoatDongService.InsertHoatDong(enumHoatDong.ResponseApi, "Nhận ResponseApi " + ApiUrl, response);
                if (response.IsSuccessStatusCode)
                {
                    dataret = await response.Content.ReadAsAsync<T>(formatters);
                    _hoatDongService.InsertHoatDong(enumHoatDong.ResponseApi, "Gửi dữ liệu api thành công" + ApiUrl, dataret);
                }
                else
                {
                    _hoatDongService.InsertHoatDong(enumHoatDong.ResponseApi, "Gửi dữ liệu api lỗi " + ApiUrl, data);
                }
            }
            return dataret;
        }
        public async Task<String> PostObjectGSApiWithStringContentReturnString(string ApiAction, StringContent stringContent = null, string token = null, string ApiUrl = null)
        {
            if (string.IsNullOrEmpty(ApiUrl))
            {
                ApiUrl = _config.ApiKhoCSDL.ApiUrlKhoCSDL + ApiAction;
            }
            else
            {
                ApiUrl = ApiUrl + ApiAction;
            }
            String dataret = "";
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(1440);
                addHeader(client, ApiUrl, token);
                HttpResponseMessage response;
                var data = stringContent.ReadAsStringAsync().Result;
                _hoatDongService.InsertHoatDong(enumHoatDong.ResponseApi, "Bắt đầu gửi dữ liệu api " + ApiUrl, data);
                response = await client.PostAsync(client.BaseAddress, stringContent);
                _hoatDongService.InsertHoatDong(enumHoatDong.ResponseApi, "Nhận ResponseApi " + ApiUrl, response);
                if (response.IsSuccessStatusCode)
                {
                    dataret = await response.Content.ReadAsStringAsync();
                    _hoatDongService.InsertHoatDong(enumHoatDong.ResponseApi, "Gửi dữ liệu api thành công" + ApiUrl, dataret);
                }
                else
                {
                    _hoatDongService.InsertHoatDong(enumHoatDong.ResponseApi, "Gửi dữ liệu api lỗi " + ApiUrl, data);
                }
            }
            return dataret;
        }

        public async Task<List<T>> PostListObjectGSApiWithStringContent<T>(string ApiAction, StringContent stringContent = null, string token = null, string ApiUrl = null)
        {
            if (string.IsNullOrEmpty(ApiUrl))
            {
                ApiUrl = _config.ApiKhoCSDL.ApiUrlKhoCSDL + ApiAction;
            }
            else
            {
                ApiUrl = ApiUrl + ApiAction;
            }
            List<T> dataret = new List<T>();
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(1440);
                addHeader(client, ApiUrl, token);
                HttpResponseMessage response;

                response = await client.PostAsync(client.BaseAddress, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    dataret = await response.Content.ReadAsAsync<List<T>>();
                    _hoatDongService.InsertHoatDong(enumHoatDong.ResponseApi, "Gửi dữ liệu api " + ApiUrl, dataret);
                }
            }
            return dataret;
        }

        public async Task<T> DeleteObjectGSApi<T>(string ApiAction, object paramInput = null, string token = null, string ApiUrl = null)
        {
            if (string.IsNullOrEmpty(ApiUrl))
            {
                ApiUrl = _config.ApiKhoCSDL.ApiUrlKhoCSDL + ApiAction;
            }
            else
            {
                ApiUrl = ApiUrl + ApiAction;
            }
            T dataret = default(T);
            StringContent content = new StringContent(JsonConvert.SerializeObject(paramInput, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }), Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            client.Timeout = TimeSpan.FromMinutes(1440);
            addHeader(client, ApiUrl, token);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, ApiUrl);
            request.Content = content;
            response = await client.SendAsync(request);
            //addHeader(client, ApiUrl, token);
            _hoatDongService.InsertHoatDong(enumHoatDong.ResponseApi, "Gửi dữ liệu xoá sang kho", response);
            if (response.IsSuccessStatusCode)
            {
                dataret = await response.Content.ReadAsAsync<T>();
            }
            return dataret;
        }

        /// <summary>
        /// Post object
        /// </summary>
        /// <param name="ApiAction"></param>
        /// <param name="paramInput"></param>
        /// <param name="token"></param>
        /// <param name="ApiUrl"></param>
        /// <returns> restponseMessage</returns>
        public async Task<HttpResponseMessage> PostAsResponseMessage(string ApiAction, object paramInput = null, string token = null, string ApiUrl = null)
        {
            HttpResponseMessage response;
            if (string.IsNullOrEmpty(ApiUrl))
            {
                ApiUrl = _config.ApiKhoCSDL.ApiUrlKhoCSDL + ApiAction;
            }
            else
            {
                ApiUrl = ApiUrl + ApiAction;
            }
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(1440);
                addHeader(client, ApiUrl, token);
                response = await client.PostAsJsonAsync(client.BaseAddress, paramInput);
                _hoatDongService.InsertHoatDong(enumHoatDong.ResponseApi, "Gửi dữ liệu sang kho", response);
            }
            return response;
        }
    }
}