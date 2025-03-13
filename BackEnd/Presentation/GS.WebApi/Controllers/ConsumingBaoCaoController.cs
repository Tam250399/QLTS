using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GS.Core.Domain.HeThong;
using GS.Services.HeThong;
using GS.Web.Framework.Kendoui;
using GS.WebApi.Models.BaoCaoSvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GS.Services.Logging;
using GS.Core;
using GS.Services.BaoCaos;
using GS.Core.Domain.BaoCaos;
using GS.Core.Domain.CauHinh;
using GS.WebApi.Factories.BaoCaoSvc;
using System.Collections;
using GS.WebApi.Models.ConsumingApi.TaiSanApi;
using GS.WebApi.Models.ConsumingApi;
using GS.WebApi.Common;
using System.Net.Http;
using System.Text;
using GS.Services.Common;
using GS.WebApi.Factories.Common;
using GS.Services.DB;
using GS.Core.Domain.DB;
using GS.Core.Domain.Api.TaiSanDBApi;

namespace GS.WebApi.Controllers
{
    public class ConsumingBaoCaoController : BaseAdminController
    {
        private readonly IHoatDongService _hoatDongService;
        private readonly ILogger _logger;
        private readonly IQueueProcessService _queueProcessService;
        private readonly IFileCongViecService _fileCongViecService;
        private readonly ICauHinhService _cauHinhService;
        private readonly IBaoCaoSvcModelFactory _baoCaoSvcModelFactory;
        private readonly IGSAPIService _gSAPIService;
        private readonly ICommonFactory _commonFactory;
        private readonly IDB_QueueProcessService _dB_QueueProcessService;
        private readonly IDB_QueueProcessHistoryService _dB_QueueProcessHistoryService;

        public ConsumingBaoCaoController(IHoatDongService hoatDongService,
            ILogger logger,
            IQueueProcessService queueProcessService,
            IFileCongViecService fileCongViecService,
            ICauHinhService cauHinhService,
            IBaoCaoSvcModelFactory baoCaoSvcModelFactory,
            IGSAPIService gSAPIService,
            ICommonFactory commonFactory,
            IDB_QueueProcessService dB_QueueProcessService,
            IDB_QueueProcessHistoryService dB_QueueProcessHistoryService)
        {
            this._dB_QueueProcessService = dB_QueueProcessService;
            this._dB_QueueProcessHistoryService = dB_QueueProcessHistoryService;
            this._hoatDongService = hoatDongService;
            this._logger = logger;
            this._queueProcessService = queueProcessService;
            this._fileCongViecService = fileCongViecService;
            this._cauHinhService = cauHinhService;
            this._baoCaoSvcModelFactory = baoCaoSvcModelFactory;
            this._gSAPIService = gSAPIService;
            this._commonFactory = commonFactory;
        }
        [HttpPost]
        public async Task<IActionResult> DongBoBaoCao([FromBody] Kho_DongBoBaoCao model)
        {

            if (model == null)
                return NullModelResponseApi();
            if (String.IsNullOrEmpty(model.DATA_JSON))
                return OkErrorResponseApi("DATA_JSON is null", model);
            else if (model.DATA_JSON == "[]")
                return OkErrorResponseApi("no data", model);
            Type _type = typeof(CommonActionReport);
            if (String.IsNullOrEmpty(model.MA_BAO_CAO_KHO))
                return OkErrorResponseApi("MA_BAO_CAO_KHO is null", model);
            
            var _field = _type?.GetField(model.MA_BAO_CAO_KHO);
            String action = _field?.GetValue(null).ToString();
            if (String.IsNullOrEmpty(action))
                return OkErrorResponseApi("ACTION is null", model);
            #region Gửi dữ liệu báo cáo sang kho
            HoatDong hoatDong = _hoatDongService.InsertHoatDong(enumHoatDong.DbBaoCao, $"Gửi dữ liệu báo cáo {model.MA_BAO_CAO} sang kho " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), model);
            ResponseApi apiRespon = new ResponseApi();
            StringContent content = new StringContent(model.DATA_JSON, Encoding.UTF8, "application/json");
            string dataJson = content.ReadAsStringAsync().Result;
            apiRespon = await _gSAPIService.PostObjectGSApiWithStringContent<ResponseApi>(CommonActionReport.RequestReport + action, content, _commonFactory.GetTokenKhoCSDLQG());
            #endregion

            if (apiRespon != null && apiRespon.Status)
            {
                hoatDong.MO_TA += " Message: Đồng bộ thành công " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "; ResponseApi: " + apiRespon.toStringJson();
                _hoatDongService.UpdateHoatDong(hoatDong);
                //return OkSuccessMessage("OK", apiRespon);
                return OkSuccessResponseApi("Đồng bộ báo cáo thành công", apiRespon);
            }
            else
            {
                hoatDong.MO_TA += " Message: Đồng bộ báo cáo thất bại " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "; ResponseApi: " + apiRespon.toStringJson();
                _hoatDongService.UpdateHoatDong(hoatDong);
                //return OkErrorMessage("Đồng bộ báo cáo thất bại", apiRespon);
                return OkErrorResponseApi("Đồng bộ báo cáo thất bại", apiRespon);
            }
        }
    }
}