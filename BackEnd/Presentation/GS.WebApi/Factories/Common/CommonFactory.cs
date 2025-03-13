using GS.Core.Configuration;
using GS.Core.Domain.TaiSans;
using GS.Services.HeThong;
using GS.WebApi.Models.ConsumingApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GS.WebApi.Factories.Common
{
    public class CommonFactory : ICommonFactory
    {
        private readonly GSConfig _gsconfig;
        private readonly IHoatDongService _hoatDongService;

        public CommonFactory(GSConfig gSConfig,
            IHoatDongService hoatDongService)
        {
            this._gsconfig = gSConfig;
            this._hoatDongService = hoatDongService;
        }
        public string GetTokenKhoCSDLQG(ApiKhoCSDL apiKhoCSDL = null,decimal? nguonId = null)
        {
            if(apiKhoCSDL==null)
            {
                //authorization server parameters owned from the client
                //this values are issued from the authorization server to the client through a separate process (registration, etc...)
                Uri authorizationServerTokenIssuerUri = new Uri(_gsconfig.ApiKhoCSDL.UrlTokenKhoCSDLQG + "aprserver/oauth/token");
                //access token request
                string username = _gsconfig.ApiKhoCSDL.Username;
                //if (nguonId == (decimal)enumNguonTaiSan.DKTS40)
                //    username = _gsconfig.ApiKhoCSDL.Username;
                //else if (nguonId == (decimal)enumNguonTaiSan.QLTSNN)
                //    username = _gsconfig.ApiKhoCSDL.UsernameTsnn;
                //else if (nguonId == (decimal)enumNguonTaiSan.QLTSC)
                //    username = _gsconfig.ApiKhoCSDL.UsernameTsc;
                try
                {
                    string rawJwtToken = RequestTokenToAuthorizationServer(
                         authorizationServerTokenIssuerUri,
                         _gsconfig.ApiKhoCSDL.ClientId,
                         _gsconfig.ApiKhoCSDL.ClientSecret,
                         username,
                         _gsconfig.ApiKhoCSDL.Password)
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
                Uri authorizationServerTokenIssuerUri = new Uri(apiKhoCSDL.UrlTokenKhoCSDLQG + "aprserver/oauth/token");
                //access token request
                string username = apiKhoCSDL.Username;
                //if (nguonId == (decimal)enumNguonTaiSan.DKTS40)
                //    username = apiKhoCSDL.Username;
                //else if (nguonId == (decimal)enumNguonTaiSan.QLTSNN)
                //    username = apiKhoCSDL.UsernameTsnn;
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
        }
        public ApiKhoCSDL GetInfoApiKhoCSDL()
        {
            return _gsconfig.ApiKhoCSDL;
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
    }
}
