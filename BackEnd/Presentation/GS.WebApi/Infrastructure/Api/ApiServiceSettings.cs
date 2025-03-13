using GS.Core.Configuration;
using GS.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GS.Api.Infrastructure.Api
{
     
    public class ClientApiSetting
    {
        public ClientApiSetting()
        {

        }
        public ClientApiSetting(string _errMsg)
        {
            this.ErrorMsg = _errMsg;
        }
        public string ClientApp { get; set; }
        public string CodeName { get; set; }
        public string KeyPass { get; set; }
        public string ClientIP { get; set; }
        public int NguoiDungId { get; set; }
        public int DonViId { get; set; }        
        public string ErrorMsg { get; set; }
        public int ClientType { get; set; }
    }
    public class ApiSettings : ICauHinh
    {
        public string DataConfig { get; set; }
    }
    public class ApiCommon
    {
        //public static ClientApiSetting isAuthentication(ClientApp model, string _dataConfig, string _ip)
        //{
        //    if (string.IsNullOrEmpty(model.ClientId))
        //        return new ClientApiSetting("ClientId không được rỗng");
        //    if (string.IsNullOrEmpty(model.CodeName))
        //        return new ClientApiSetting("CodeName không được rỗng");
        //    if (string.IsNullOrEmpty(model.KeyPass))
        //        return new ClientApiSetting("KeyPass không được rỗng");

        //    if (string.IsNullOrEmpty(_dataConfig))
        //        return new ClientApiSetting("API server is not config. Please contact to Adminstrator");

        //    var apiConfigs = _dataConfig.ToEntities<ClientApiSetting>();
        //    var clientInfo = apiConfigs.Where(c => c.ClientApp.Equals(model.ClientId)).FirstOrDefault();
        //    if (clientInfo == null)
        //    {
        //        return new ClientApiSetting(model.ClientId + " không có quyền truy xuất");
        //    }
        //    if (!model.CodeName.Equals(clientInfo.CodeName, StringComparison.Ordinal))
        //        return new ClientApiSetting("CodeName không đúng");
        //    if (!model.KeyPass.Equals(clientInfo.KeyPass, StringComparison.Ordinal))
        //        return new ClientApiSetting("KeyPass không đúng");

        //    if (!string.IsNullOrEmpty(clientInfo.ClientIP) && !_ip.Equals(clientInfo.ClientIP, StringComparison.Ordinal))
        //        return new ClientApiSetting("IP(" + _ip + ") không được được phép truy cập");

        //    return clientInfo;
        //}
    }
}