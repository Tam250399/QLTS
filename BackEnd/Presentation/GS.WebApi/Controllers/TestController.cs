using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GS.Api.Infrastructure.Api;
using GS.WebApi.Factories;
using GS.WebApi.Infrastructure;
using GS.WebApi.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using GS.Core.Configuration;
using GS.Core.Domain.Common;
using GS.WebApi.Models.DanhMuc;
using GS.WebApi.Common;
using GS.Services.Common;
using GS.WebApi.Models.ConsumingApi;
using GS.WebApi.Models.ConsumingApi.DanhMucApi;
using GS.WebApi.Factories.Common;
using GS.Core;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GS.WebApi.Controllers
{
    public class TestController : BaseApiController
    {
        private readonly IDanhMucSvcModelFactory _danhMucModelFactory;
        private readonly GSConfig _gSConfig;
        private readonly IGSAPIService _gSAPIService;
        private readonly ICommonFactory _commonFactory;
        public TestController(IDanhMucSvcModelFactory danhMucModelFactory,
            GSConfig gSConfig,
            IGSAPIService gSAPIService,
            ICommonFactory commonFactory)
        {
            this._danhMucModelFactory = danhMucModelFactory;
            this._gSConfig = gSConfig;
            this._gSAPIService = gSAPIService;
            this._commonFactory = commonFactory;
        }
        [HttpGet]
        public IActionResult HelloWorld()
        {
            return Ok("Hello world");
        }
        [HttpPost]
        public IActionResult UpdateModel([FromBody] TestModel model)
        {
            model.BirthDay = DateTime.Now;
            return Ok(model);
        }
        [HttpGet]
        public IActionResult GetNhanXe()
        {
            var items = _danhMucModelFactory.GetAllNhanXe();
            return Ok(items);
        }
        [HttpGet]
        public IActionResult GetTokenCSDLQG(ApiKhoCSDL apiKhoCSDL = null)
        {
            //authorization server parameters owned from the client
            //this values are issued from the authorization server to the client through a separate process (registration, etc...)
            try
            {
                return OkSuccessResponseApi("Done", _commonFactory.GetTokenKhoCSDLQG(apiKhoCSDL));
                //;
                //if (apiKhoCSDL == null || String.IsNullOrEmpty(apiKhoCSDL.UrlTokenKhoCSDLQG))
                //{
                //    Uri authorizationServerTokenIssuerUri = new Uri(_gSConfig.ApiKhoCSDL.UrlTokenKhoCSDLQG + "aprserver/oauth/token");
                //    //access token request
                //    string rawJwtToken = RequestTokenToAuthorizationServer(
                //         authorizationServerTokenIssuerUri,
                //         _gSConfig.ApiKhoCSDL.ClientId,
                //         _gSConfig.ApiKhoCSDL.ClientSecret,
                //         _gSConfig.ApiKhoCSDL.Username,
                //         _gSConfig.ApiKhoCSDL.Password)
                //        .GetAwaiter()
                //        .GetResult();
                //    var objAuthen = JsonConvert.DeserializeObject<OAuth2>(rawJwtToken);
                //    string token = objAuthen.access_token;
                //    return OkSuccessResponseApi("Done", token);
                //    //return Ok(MessageReturn.CreateSuccessMessage("Done", new
                //    //{
                //    //    Token = token,
                //    //    ClientId = _gSConfig.ApiKhoCSDL.ClientId,
                //    //    ClientSecret = _gSConfig.ApiKhoCSDL.ClientSecret,
                //    //    Username = _gSConfig.ApiKhoCSDL.Username,
                //    //    Password = _gSConfig.ApiKhoCSDL.Password,
                //    //    Url = _gSConfig.ApiKhoCSDL.ApiUrlKhoCSDL,
                //    //    UrlToken = _gSConfig.ApiKhoCSDL.UrlTokenKhoCSDLQG
                //    //}));
                //}
                //else
                //{
                //    Uri authorizationServerTokenIssuerUri = new Uri(apiKhoCSDL.UrlTokenKhoCSDLQG + "aprserver/oauth/token");
                //    //access token request
                //    string rawJwtToken = RequestTokenToAuthorizationServer(
                //         authorizationServerTokenIssuerUri,
                //         apiKhoCSDL.ClientId,
                //         apiKhoCSDL.ClientSecret,
                //         apiKhoCSDL.Username,
                //         apiKhoCSDL.Password)
                //        .GetAwaiter()
                //        .GetResult();
                //    var objAuthen = JsonConvert.DeserializeObject<OAuth2>(rawJwtToken);
                //    string token = objAuthen.access_token;
                //    return OkSuccessResponseApi("Done", token);
                //    //return Ok(MessageReturn.CreateSuccessMessage("Done", new
                //    //{
                //    //    Token = rawJwtToken,
                //    //    ClientId = apiKhoCSDL.ClientId,
                //    //    ClientSecret = apiKhoCSDL.ClientSecret,
                //    //    Username = apiKhoCSDL.Username,
                //    //    Password = apiKhoCSDL.Password,
                //    //    Url = apiKhoCSDL.ApiUrlKhoCSDL,
                //    //    UrlToken = _gSConfig.ApiKhoCSDL.UrlTokenKhoCSDLQG
                //    //}));
                //}
            }
            catch (Exception ex)
            {
                return OkErrorResponseApi("Error", ex.Message);
            }

        }
        [HttpGet]
        public IActionResult GetInfoApiKhoCSDL()
        {
            return Ok(MessageReturn.CreateSuccessMessage("Done", _commonFactory.GetInfoApiKhoCSDL()));
        }

        private static async Task<string> RequestTokenToAuthorizationServer(Uri uriAuthorizationServer, string clientId, string clientSecret, string username, string password)
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
        [HttpPost]
        public async Task<IActionResult> TestDongBoQuocGia([FromBody] QuocGiaModel quocGiaModel)
        {
            string _action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.QuocGia + CommonAction.DongBo;
            List<Kho_QuocGia> kho_QuocGias = new List<Kho_QuocGia>();
            kho_QuocGias.Add(new Kho_QuocGia()
            {
                actionType = quocGiaModel.DB_ID > 0 ? 2 : 1,
                name = quocGiaModel.TEN,
                code = quocGiaModel.MA != null ? quocGiaModel.MA : "QG" + quocGiaModel.ID.ToString(),
                syncId = quocGiaModel.ID.ToString()
            });
            var response = await _gSAPIService.PostAsResponseMessage(_action, kho_QuocGias, _commonFactory.GetTokenKhoCSDLQG());
            return Ok(MessageReturn.CreateSuccessMessage("Done", new
            {
                RequestUri = response.RequestMessage.RequestUri,
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                Content = kho_QuocGias,
                StatusCode = response.StatusCode,
            }));
        }
        [HttpPost]
        public async Task<IActionResult> TestDongBoQuocGia2([FromBody] TestDongBoQuocGia TestDongBoQuocGia)
        {
            string _action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.QuocGia + CommonAction.DongBo;
            List<Kho_QuocGia> kho_QuocGias = new List<Kho_QuocGia>();
            kho_QuocGias.Add(new Kho_QuocGia()
            {
                actionType = TestDongBoQuocGia.QuocGiaModel.DB_ID > 0 ? 2 : 1,
                name = TestDongBoQuocGia.QuocGiaModel.TEN,
                code = TestDongBoQuocGia.QuocGiaModel.MA != null ? TestDongBoQuocGia.QuocGiaModel.MA : "QG" + TestDongBoQuocGia.QuocGiaModel.ID.ToString(),
                syncId = TestDongBoQuocGia.QuocGiaModel.ID.ToString()
            });
            var response = await _gSAPIService.PostAsResponseMessage(_action, kho_QuocGias, _commonFactory.GetTokenKhoCSDLQG(TestDongBoQuocGia.ApiKhoCSDL));
            return Ok(MessageReturn.CreateSuccessMessage("Done", new
            {
                RequestUri = response.RequestMessage.RequestUri,
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                Content = kho_QuocGias,
                StatusCode = response.StatusCode,
            }));
        }
    }
}
