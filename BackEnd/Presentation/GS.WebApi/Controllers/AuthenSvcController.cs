using GS.Core;
using GS.Core.Domain.Api;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.SHTD;
using GS.Core.Domain.TaiSans;
using GS.Services.Authentication;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.HeThong;
using GS.Services.NghiepVu;
using GS.Services.SHTD;
using GS.Services.TaiSans;
using GS.WebApi.Infrastructure.Mapper.Extensions;
using GS.WebApi.Models;
using GS.WebApi.Models.TaiSanXacLap;
using GS.WebApi.Models.TaiSan;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.ExpressionGraph.FunctionCompilers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace GS.WebApi.Controllers
{
    public class AuthenSvcController : BaseApiController
    {
        #region fields
        private readonly CauHinhNguoiDung _nguoiDungCauHinh;
        private readonly CauHinhChung _cauHinhChung;
        private readonly XacThucChungThucSoCauHinh _cauHinhXacThuc;

        private readonly IWebHelper _webHelper;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IAuthenticationService _authenticationService;
        #endregion
        #region ctor
        public AuthenSvcController(
            XacThucChungThucSoCauHinh cauHinhXacThuc,
            CauHinhChung cauHinhChung,
             CauHinhNguoiDung customerSettings,
             IWebHelper webHelper,
            INguoiDungService nguoiDungService,
            IAuthenticationService authenticationService

            )
        {
            this._cauHinhXacThuc = cauHinhXacThuc;
            this._cauHinhChung = cauHinhChung;
            this._authenticationService = authenticationService;
            this._webHelper = webHelper;
            this._nguoiDungCauHinh = customerSettings;
            this._nguoiDungService = nguoiDungService;
        }
        #endregion
        #region Method
        [HttpPost]
        public IActionResult Login([FromBody]LoginModel model)
        {
            var loginResult = _nguoiDungService.ValidateNguoiDung(model.Username, model.Password);
            if(loginResult==Core.Domain.HeThong.NguoiDungKetQuaDangNhap.Successful)
            {
                var customer = _nguoiDungService.GetNguoiDungByUsername(model.Username);
                return OkSuccessMessage("Ok", _authenticationService.GenerateToken(customer));
            }    
            return OkNotFoundMessage("Username or password is incorrect");
        }
        #endregion
    }
}
