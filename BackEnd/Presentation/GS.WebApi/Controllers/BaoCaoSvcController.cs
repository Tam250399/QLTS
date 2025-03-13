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

namespace GS.WebApi.Controllers
{
    public class BaoCaoSvcController : BaseAdminController
    {
        private readonly IHoatDongService _hoatDongService;
        private readonly ILogger _logger;
        private readonly IQueueProcessService _queueProcessService;
        private readonly IFileCongViecService _fileCongViecService;
        private readonly ICauHinhService _cauHinhService;
        private readonly IBaoCaoSvcModelFactory _baoCaoSvcModelFactory;

        public BaoCaoSvcController(IHoatDongService hoatDongService,
            ILogger logger,
            IQueueProcessService queueProcessService,
            IFileCongViecService fileCongViecService,
            ICauHinhService cauHinhService,
            IBaoCaoSvcModelFactory baoCaoSvcModelFactory)
        {
            this._hoatDongService = hoatDongService;
            this._logger = logger;
            this._queueProcessService = queueProcessService;
            this._fileCongViecService = fileCongViecService;
            this._cauHinhService = cauHinhService;
            this._baoCaoSvcModelFactory = baoCaoSvcModelFactory;
        }
        [HttpPost]
        public IActionResult RequestBaoCao([FromBody] ThamSoNhanBaoCaoSvcSearchModel model)
        {
            try
            {
                #region check token
                if (!CheckCurrentUser())
                    return OkErrorMessage("Token hết hạn");
                #endregion
                //if (model == null)
                //	return this.NullModel();
                if (!ModelState.IsValid)
                {
                    var ListError = ModelState.SerializeErrors();
                    return OkErrorMessage("Error", ListError);
                }
                if (model == null)
                    return this.NullModel();
                if (model.MA_BAO_CAO == "TS_BCQH_MAU01_THTSNN" ||
                    model.MA_BAO_CAO == "TS_BCQH_MAU04_TS_LTS" ||
                    model.MA_BAO_CAO == "TS_BCQH_TANG_GIAM_PL10_TSNN")
                    model.NHOM_TAI_SAN_LON = 1;

                _hoatDongService.InsertHoatDong(enumHoatDong.TaoMoi, string.Format("Nhận thành công yêu cầu báo cáo: {0} của đơn vị: {1}!", model.MA_BAO_CAO, string.Join(",", model.LIST_DON_VI_ID.ToArray())), model);
                var cauHinhDonVi = _cauHinhService.LoadCauHinh<CauHinhMapBC>();
                if (cauHinhDonVi != null && !string.IsNullOrEmpty(cauHinhDonVi.BaoCao))
                {
                    var cauHinhBaoCao = cauHinhDonVi.BaoCao.toEntities<CauHinhMapBaoCao>().Where(p => p.MaBaoCao == model.MA_BAO_CAO).FirstOrDefault();
                    if (cauHinhBaoCao != null && !string.IsNullOrEmpty(cauHinhBaoCao.ExcecuteStatment))
                    {
                        var statement = _baoCaoSvcModelFactory.PrepareStatementQueueProcess(baseStatement: cauHinhBaoCao.ExcecuteStatment, thamSoNhanBaoCao: model);
                        var searchModel = _baoCaoSvcModelFactory.PrepareReportSearchModel(model);
                       // _hoatDongService.InsertHoatDong(enumHoatDong.TaoMoi, "PrepareStatementQueueProcess", searchModel);
                        var queue = new QueueProcess()
                        {
                            MA_BAO_CAO = model.MA_BAO_CAO,
                            DON_VI_ID = (int)model.LIST_DON_VI_ID.FirstOrDefault(),
                            FILE_TYPE = (int)enumEXPORT_FILE_TYPE.PDF,//model.LOAI_DATA_TRA_VE,
                            SEARCH_MODEL_JSON = searchModel.toStringJson(isIgnoreNull: true),
                            //SEARCH_MODEL_CLASS = "GS.Web.Models.BaoCaoDongBos.BaoCaoTaiSanDongBoSearch",
                            SEARCH_MODEL_CLASS = cauHinhBaoCao.FullName_SearchModelClass,
                            REPORT_CLASS = cauHinhBaoCao.FullName_ReportClass,
                            MODEL_DATA_TYPE = cauHinhBaoCao.Model,
                            TYPE_QUEUE_PROCESS_ID = (int)enumTypeQueue_Process.CSDLQGTSC,
                            STATEMENT = statement
                        };
                        _queueProcessService.InsertQueueProcessDongBo(queue);
                        //_hoatDongService.InsertHoatDong(logLevel: LogLevel.All, shortMessage: string.Format("Nhận thành công yêu cầu báo cao: {0} của đơn vị: {1}.", model.MA_BAO_CAO, string.Join(",", model.LIST_DON_VI_ID.ToArray())), fullMessage: model.toStringJson());
                        return OkSuccessMessage(msg: string.Format("Nhận thành công yêu cầu báo cáo: {0} của đơn vị: {1}!", model.MA_BAO_CAO, string.Join(",", model.LIST_DON_VI_ID.ToArray())), objdata: new { fileGuid = queue.GUID });
                    }
                }
                return OkErrorMessage(msg: "Báo cáo không được đồng bộ.");
            }
            catch (Exception ex)
            {
                return OkErrorMessage(msg: ex.Message);
            }


        }
        [HttpPost]
        public IActionResult GetBaoCao([FromBody] ThamSoNhanBaoCaoSvcSearchModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var queue = _queueProcessService.GetQueueProcessByGuid(model.fileGuid);
            if (queue == null)
                return this.NullModel();
            if (queue.TRANG_THAI_ID == (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DA_LUU_FILE || queue.TRANG_THAI_ID == (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DA_EXPORT_FILE)
            {
                var returnBaoCaoSvc = _baoCaoSvcModelFactory.PrepareReturnBaoCaoSvcFromQueueProcess(queue);
                if (returnBaoCaoSvc == null)
                    return OkErrorMessage("Không tìm thấy file báo cáo. Vui lòng xuất lại.", model.fileGuid);
                return OkSuccessMessage(objdata: returnBaoCaoSvc);
            }
            else if (queue.TRANG_THAI_ID == (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_LOI)
                return OkErrorMessage("Xuất báo cáo bị lỗi. Vui lòng xuất lại.", model.fileGuid);
            else
                return OkErrorMessage("Đang xuất file. Vui lòng đợi", model.fileGuid);

        }
        // lấy báo cáo tương ứng và compare dataJson chuyển sang bên kho
        [HttpPost]
        public IActionResult GetBaoCaoDongBo([FromBody] ThamSoNhanBaoCaoSvcSearchModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            //if (!ModelState.IsValid)
            //{
            //    var ListError = ModelState.SerializeErrors();
            //    return OkErrorMessage("Error", ListError);
            //}
            if (model == null)
                return this.NullModel();
            var queue = _queueProcessService.GetQueueProcessByGuid(model.fileGuid);
            if (queue == null)
            {
                var cauHinhDonVi = _cauHinhService.LoadCauHinh<CauHinhMapBC>();
                if (cauHinhDonVi != null && !string.IsNullOrEmpty(cauHinhDonVi.BaoCao))
                {
                    var cauHinhBaoCao = cauHinhDonVi.BaoCao.toEntities<CauHinhMapBaoCao>().Where(p => p.MaBaoCao == model.MA_BAO_CAO).FirstOrDefault();
                    if (cauHinhBaoCao != null && !string.IsNullOrEmpty(cauHinhBaoCao.ExcecuteStatment))
                    {
                        var statement = _baoCaoSvcModelFactory.PrepareStatementQueueProcess(baseStatement: cauHinhBaoCao.ExcecuteStatment, thamSoNhanBaoCao: model);
                        var queuenew = new QueueProcess()
                        {
                            MA_BAO_CAO = model.MA_BAO_CAO,
                            NGUOI_TAO_ID = (int)currentUser.ID,
                            DON_VI_ID = (int)model.LIST_DON_VI_ID.FirstOrDefault(),
                            FILE_TYPE = (int)enumEXPORT_FILE_TYPE.PDF,//model.LOAI_DATA_TRA_VE,
                            SEARCH_MODEL_JSON = _baoCaoSvcModelFactory.PrepareReportSearchModel(model).toStringJson(isIgnoreNull: true),
                            SEARCH_MODEL_CLASS = cauHinhBaoCao.FullName_SearchModelClass,
                            //SEARCH_MODEL_CLASS = "GS.Web.Models.BaoCaoDongBos.BaoCaoTaiSanDongBoSearch",
                            REPORT_CLASS = cauHinhBaoCao.FullName_ReportClass,
                            MODEL_DATA_TYPE = cauHinhBaoCao.Model,
                            TYPE_QUEUE_PROCESS_ID = (int)enumTypeQueue_Process.CSDLQGTSC,
                            STATEMENT = statement
                        };
                        _queueProcessService.InsertQueueProcess(queuenew);
                        _logger.InsertLog(logLevel: LogLevel.All, shortMessage: "Request Báo cáo đồng bộ", fullMessage: model.toStringJson());
                        var returnBaoCaoSvc = _baoCaoSvcModelFactory.PrepareReturnBaoCaoSvcFromQueueProcess(queuenew);
                        return OkSuccessMessage(objdata: returnBaoCaoSvc);
                    }
                }
                return OkErrorMessage(msg: "Báo cáo không được đồng bộ. Vui lòng xuất lại");
            }
            else
            {
                if (queue.TRANG_THAI_ID == (int)enumTRANG_THAI_QUEUE_BAO_CAO.DA_LUU_FILE || queue.TRANG_THAI_ID == (int)enumTRANG_THAI_QUEUE_BAO_CAO.DA_EXPORT_FILE)
                {
                    var returnBaoCaoSvc = _baoCaoSvcModelFactory.PrepareReturnBaoCaoSvcFromQueueProcessAndQlCauHinh(queue);
                    if (returnBaoCaoSvc == null)
                        return OkErrorMessage("Không tìm thấy file báo cáo. Vui lòng xuất lại.", model.fileGuid);
                    return OkSuccessMessage(objdata: returnBaoCaoSvc);
                }
                else if (queue.TRANG_THAI_ID == (int)enumTRANG_THAI_QUEUE_BAO_CAO.LOI)
                    return OkErrorMessage("Xuất báo cáo bị lỗi. Vui lòng xuất lại.", model.fileGuid);
                else
                    return OkErrorMessage("Đang xuất file. Vui lòng đợi", model.fileGuid);
            }

        }
    }
}