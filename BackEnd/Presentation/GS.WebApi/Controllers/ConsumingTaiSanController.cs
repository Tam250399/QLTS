using DevExpress.DataAccess.Excel;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Fields;
using GS.Core;
using GS.Core.Configuration;
using GS.Core.Data;
using GS.Core.Domain.Api;
using GS.Core.Domain.Api.TaiSanDBApi;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.CCDC;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.KT;
using GS.Core.Domain.TaiSans;
using GS.Core.Infrastructure;
using GS.Services.BienDongs;
using GS.Services.Common;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.HeThong;
using GS.Services.KT;
using GS.Services.Logging;
using GS.Services.SHTD;
using GS.Services.TaiSans;
using GS.Web.Framework.Kendoui;
using GS.WebApi.Common;
using GS.WebApi.Factories.Common;
using GS.WebApi.Factories.Common.ConsumingApi;
using GS.WebApi.Models.ConsumingApi;
using GS.WebApi.Models.ConsumingApi.TaiSanApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GS.WebApi.Controllers
{

    public class ConsumingTaiSanController : BaseAdminController
    {
        private readonly IKho_DanhMucFactory _khoDanhMuc;
        private readonly IKho_TaiSanFactory _khoTaiSanFactory;
        private readonly IGSAPIService _gSAPIService;
        private readonly IKhaiThacTaiSanService _khaiThacTaiSanService;
        private readonly ITaiSanService _taiSanService;
        private readonly ICommonFactory _commonFactory;
        private readonly IHoatDongService _hoatDongService;
        private readonly ITaiSanNhatKyService _taiSanNhatKyService;
        private readonly IBienDongService _bienDongService;
        private readonly GSConfig _gSConfig;
        private readonly IGSFileProvider _fileProvider;
        private readonly IQuyetDinhTichThuService _quyetDinhTichThuService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IDonViService _donViService;
        private readonly ITempDongBoTaiSanCuService _tempDongBoTaiSanCuService;
        private readonly ILogger _logger;
        private readonly IDB_QueueProcessService _dB_QueueProcessService;
        private readonly IHaoMonTaiSanService _haoMonTaiSanService;
        private readonly IDongBoServices _dongBoServices;
        public ConsumingTaiSanController(
            IKho_DanhMucFactory kho_DanhMuc,
            IKho_TaiSanFactory kho_TaiSanFactory,
            IGSAPIService gSAPIService,
            IKhaiThacTaiSanService khaiThacTaiSanService,
            ITaiSanService taiSanService,
            ICommonFactory commonFactory,
            IHoatDongService hoatDongService,
            ITaiSanNhatKyService taiSanNhatKyService,
            GSConfig gSConfig,
            IGSFileProvider fileProvider,
            IQuyetDinhTichThuService quyetDinhTichThuService,
            INhanHienThiService nhanHienThiService,
            IDonViService donViService,
            ITempDongBoTaiSanCuService tempDongBoTaiSanCuService,
            ILogger logger,
            IBienDongService bienDongService,
            IDB_QueueProcessService dB_QueueProcessService,
            IHaoMonTaiSanService haoMonTaiSanService,
            IDongBoServices dongBoServices
            )
        {
            this._khoDanhMuc = kho_DanhMuc;
            this._khoTaiSanFactory = kho_TaiSanFactory;
            this._gSAPIService = gSAPIService;
            this._khaiThacTaiSanService = khaiThacTaiSanService;
            this._taiSanService = taiSanService;
            this._commonFactory = commonFactory;
            this._hoatDongService = hoatDongService;
            this._taiSanNhatKyService = taiSanNhatKyService;
            this._gSConfig = gSConfig;
            this._fileProvider = fileProvider;
            this._quyetDinhTichThuService = quyetDinhTichThuService;
            this._nhanHienThiService = nhanHienThiService;
            this._donViService = donViService;
            this._tempDongBoTaiSanCuService = tempDongBoTaiSanCuService;
            this._logger = logger;
            this._bienDongService = bienDongService;
            this._dB_QueueProcessService = dB_QueueProcessService;
            this._haoMonTaiSanService = haoMonTaiSanService;
            this._dongBoServices = dongBoServices;
        }
        #region Tài sản cơ quan tổ chức
        /// <summary>
        /// Hàm đồng bộ tài sản - biến động tức thời.
        /// khi duyệt biến động trên web app sẽ gọi đến api này để đẩy dữ liệu biến động sang kho
        /// </summary>
        /// <param name="DonViId"></param>
        /// <param name="LoaiBienDongId"></param>
        /// <param name="LoaiHinhTaiSanId"></param>
        /// <param name="ngayCapNhatTu"></param>
        /// <param name="ngayCapNhatDen"></param>
        /// <param name="DonViChaId"></param>
        /// <param name="TaiSanId"></param>
        /// <param name="IsGiamDieuChuyenToanBo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateBienDong([FromBody] SearchTaiSanModel searchModel)
        {
            HoatDong hoatDong = _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DongBoTaiSan, "UpdateBienDong started " + DateTime.Now, 0, "DongBoTaiSan", searchModel);
            string action;
            decimal LoaiBienDongId = searchModel.LoaiBienDongId.Value;
            //Kho_DongBoTaiSan DuLieuDongBo = await _khoTaiSanFactory.GetListTaiSanDongBo(LoaiBienDong: searchModel.LoaiBienDongId.Value, LoaiHinhTaiSanId: searchModel.LoaiHinhTaiSanId.Value, TaiSanId: searchModel.TaiSanId.Value, BienDongId: searchModel.BienDongId.Value);            //
            decimal nguonId = 0;
            Kho_DongBoTaiSan DuLieuDongBo = new Kho_DongBoTaiSan();
            BienDong bienDong = _bienDongService.GetBienDongById(searchModel.BienDongId.Value);
            if (bienDong == null)
            {
                hoatDong.MO_TA += " Error : Biến động không tồn tại";
                _hoatDongService.UpdateHoatDong(hoatDong);
                return OkErrorResponseApi("UpdateBienDong - Biến động không tồn tại", bienDong);
            }
            TaiSan taiSan = _taiSanService.GetTaiSanById(bienDong.TAI_SAN_ID);
            if (taiSan == null)
            {
                hoatDong.MO_TA += " Error : Tài sản không tồn tại";
                hoatDong.DU_LIEU += bienDong.toStringJson();
                _hoatDongService.UpdateHoatDong(hoatDong);
                return OkErrorResponseApi("UpdateBienDong - Tài sản không tồn tại");
            }
            else if (LoaiBienDongId != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO &&
               LoaiBienDongId != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY && taiSan.MA_DB == null)
            {
                hoatDong.MO_TA += " Error : Tài sản chưa đồng bộ tăng mới";
                hoatDong.DU_LIEU += bienDong.toStringJson();
                _hoatDongService.UpdateHoatDong(hoatDong);
                return OkErrorResponseApi("UpdateBienDong - Tài sản chưa đồng bộ tăng mới");
            }
            nguonId = taiSan.NGUON_TAI_SAN_ID;
            DuLieuDongBo = _khoTaiSanFactory.PrepareBienDongCanDongBo(new List<BienDong>() { bienDong });
            //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Bắt đầu đồng bộ biến động", searchModel.TaiSanId.Value, "BienDong", DuLieuDongBo);
            if (DuLieuDongBo != null && DuLieuDongBo.data.Count > 0)
            {
                ResponseApi apiRespon = await GuiDuLieuSangKho(DuLieuDongBo, LoaiBienDongId, nguonId);
                //Insert Hoạt đông
                StringContent content = new StringContent(JsonConvert.SerializeObject(DuLieuDongBo, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }), Encoding.UTF8, "application/json");
                InsertLogModel insertLogModel = new InsertLogModel()
                {
                    Data = content.ReadAsStringAsync().Result,
                    ResponseApi = apiRespon,
                    LoaiDuLieu = "TaiSan"
                };

                hoatDong.MO_TA += " Success : Đồng bộ biến động thành công";
                _hoatDongService.UpdateHoatDong(hoatDong);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThanhCong, "Success : Đồng bộ biến động thành công BienDongId = "+ bienDong.ID, 0, "DongBoTaiSan", insertLogModel);
                //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Đồng bộ biến động", searchModel.TaiSanId.Value, "BienDong", insertLogModel);//DbCho: Đang chờ đồng bộ
                // Update nhật ký
                if (apiRespon != null && apiRespon.Status)
                {
                    foreach (var item in DuLieuDongBo.CapNhatIDBienDongs)
                    {
                        TaiSanNhatKy tsnk = _taiSanNhatKyService.GetByTaiSanId(item.TaiSanId);
                        if (tsnk != null)
                        {
                            List<decimal> lstBienDong = new List<decimal>() { item.BienDongId };
                            tsnk.NGAY_DONG_BO = apiRespon.Date;
                            tsnk.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: tsnk.BD_IDS, idDel: lstBienDong);
                            tsnk.BD_IDS_DANG_DB = string.Join(',', lstBienDong);
                            tsnk.RESPONSE = apiRespon.toStringJson();
                            tsnk.TRANG_THAI_ID = LoaiBienDongId == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO ? (decimal)enumTrangThaiTaiSanNhatKy.CHO_GET_MA : (decimal)enumTrangThaiTaiSanNhatKy.CHO_DONG_BO;
                            _taiSanNhatKyService.UpdateTaiSanNhatKy(tsnk);
                        }
                    }
                    if (LoaiBienDongId == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO ||
                    LoaiBienDongId == (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
                    {
                        List<LogObject> logObjects = DuLieuDongBo.data.Select(x => new LogObject()
                        {
                            syncSourceId = x.syncSourceAssetId
                        }).ToList();
                        _dB_QueueProcessService.InsertActionToQueue(action: enumSendRequest.CapNhatMaTaiSanKho, obj: logObjects, level: (int)enumLevelQueueProcessDB.HIGHEST);
                    }
                    else
                    {
                        List<LogObject> logObjects = DuLieuDongBo.data.Select(x => new LogObject()
                        {
                            syncSourceId = x.syncSourceId
                        }).ToList();
                        _dB_QueueProcessService.InsertActionToQueue(action: enumSendRequest.CheckLogBienDongKho, obj: logObjects, level: (int)enumLevelQueueProcessDB.HIGHEST);
                    }
                }
                else
                {
                    hoatDong.MO_TA += " Error : Đồng bộ biến động thất bại";
                    hoatDong.DU_LIEU += bienDong.toStringJson();
                    _hoatDongService.UpdateHoatDong(hoatDong);
                }
                return Ok(apiRespon);
            }
            return Ok(new ResponseApi() { Message = "không có dữ liệu đồng bộ" });
        }
        /// <summary>
        /// Khi bỏ duyệt biến động sẽ tự động xóa biến động trên kho
        /// </summary>
        /// <param name="BienDongId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DongBoBienDongFromFile([FromBody] SearchTaiSanModel searchModel)
        {
            if (!string.IsNullOrEmpty(searchModel.FileContent))
            {
                ResponseApi apiRespon = await GuiDuLieuTuFileSangKho(searchModel.FileContent, searchModel.LoaiBienDongId ?? 0);
                return Ok(apiRespon);
            }
            return Ok(new ResponseApi() { Message = "không có dữ liệu đồng bộ" });
        }
        [HttpPost]
        public async Task<IActionResult> DongBoHaoMonFromFile([FromBody] SearchTaiSanModel searchModel)
        {
            var token = _commonFactory.GetTokenKhoCSDLQG();
            if (!string.IsNullOrEmpty(searchModel.FileContent))
            {
                string action = CommonTaiSan.RequestTaiSan + CommonTaiSan.HaoMonTaiSan;
                #region Gửi dữ liệu sang kho
                string dataJson = searchModel.FileContent;
                StringContent content = new StringContent(dataJson, Encoding.UTF8, "application/json");

                #endregion
                var apiRespon = await _gSAPIService.PostObjectGSApiWithStringContent<ResponseApi>(action, content, _commonFactory.GetTokenKhoCSDLQG(nguonId: searchModel.nguonId));
                return Ok(apiRespon);
            }
            return Ok(new ResponseApi() { Message = "không có dữ liệu đồng bộ" });
        }

        [HttpPost]
        public async Task<IActionResult> XoaBienDong([FromBody] ParameterXoaBienDong model)
        {
            Data_XoaBienDong data_XoaBienDong = _khoTaiSanFactory.PrepareDuLieuXoaBienDong(model);
            List<Data_XoaBienDong> kho_XoaBienDong = new List<Data_XoaBienDong>();
            kho_XoaBienDong.Add(data_XoaBienDong);
            string action = CommonTaiSan.RequestTaiSan + CommonTaiSan.XoaBienDong;
            ResponseApi responseApi = await _gSAPIService.PostObjectGSApi<ResponseApi>(action, kho_XoaBienDong, _commonFactory.GetTokenKhoCSDLQG(nguonId: model.nguonId));
            if (responseApi != null && responseApi.Status)
            {
                InsertLogModel insertLogModel = new InsertLogModel()
                {
                    Data = kho_XoaBienDong,
                    ResponseApi = responseApi,
                    LoaiDuLieu = "BienDong"
                };
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Đồng bộ Xóa biến động", 0, "BienDong", insertLogModel);
                //Check log 
                //var checkLog = await CheckLogBienDong()
            }
            return Ok(responseApi);
        }
        /// <summary>
        /// hàm đẩy dữ liệu sang kho
        /// </summary>
        /// <param name="kho_DongBoTaiSan"></param>
        /// <param name="LoaiBienDongId"></param>
        /// <returns></returns>
        async Task<ResponseApi> GuiDuLieuSangKho(Kho_DongBoTaiSan kho_DongBoTaiSan, decimal LoaiBienDongId, decimal? nguonId = null)
        {
            #region Gửi dữ liệu sang kho
            string action = GetActionKho(LoaiBienDongId);
            // một tùy theo loại biến động và loại tài sản mà set các giá trị null cho các trường để loại bỏ khỏi dữ liệu truyền sang
            kho_DongBoTaiSan = _dongBoServices.ConfigDataByLoaiHinhTaiSan(kho_DongBoTaiSan, LoaiBienDongId);
            _logger.Information(message: "Gửi dữ liệu biến động " + _nhanHienThiService.GetGiaTriEnum<enumLOAI_LY_DO_TANG_GIAM>((enumLOAI_LY_DO_TANG_GIAM)LoaiBienDongId) + " sang kho " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            ResponseApi apiRespon = new ResponseApi();
            StringContent content = new StringContent(JsonConvert.SerializeObject(kho_DongBoTaiSan, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }), Encoding.UTF8, "application/json");
            #endregion
            string dataJson = content.ReadAsStringAsync().Result;
            apiRespon = await _gSAPIService.PostObjectGSApiWithStringContent<ResponseApi>(action, content, _commonFactory.GetTokenKhoCSDLQG(nguonId: nguonId));
            return apiRespon;
        }
        async Task<ResponseApi> GuiDuLieuTuFileSangKho(string fileContent, decimal LoaiBienDongId, decimal? nguonId = null)
        {
            #region Gửi dữ liệu sang kho
            string action = GetActionKho(LoaiBienDongId);
            // một tùy theo loại biến động và loại tài sản mà set các giá trị null cho các trường để loại bỏ khỏi dữ liệu truyền sang
            _logger.Information(message: "Gửi dữ liệu sang kho từ file " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            ResponseApi apiRespon = new ResponseApi();
            StringContent content = new StringContent(fileContent, Encoding.UTF8, "application/json");
            #endregion
            apiRespon = await _gSAPIService.PostObjectGSApiWithStringContent<ResponseApi>(action, content, _commonFactory.GetTokenKhoCSDLQG(nguonId: nguonId));
            return apiRespon;
        }
        /// <summary>
        /// get url api theo loại biến động
        /// </summary>
        /// <param name="LoaiBienDongId"></param>
        /// <returns></returns>
        string GetActionKho(decimal LoaiBienDongId)
        {
            string Action = "";
            switch (LoaiBienDongId)
            {
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO:
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY:
                    Action = CommonTaiSan.RequestTaiSan + CommonTaiSan.TangMoi;
                    break;
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI:
                    Action = CommonTaiSan.RequestTaiSan + CommonTaiSan.TangNguyenGia;
                    break;
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI:
                    Action = CommonTaiSan.RequestTaiSan + CommonTaiSan.GiamNguyenGia;
                    break;
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO:
                    Action = CommonTaiSan.RequestTaiSan + CommonTaiSan.GiamTaiSan;
                    break;
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN:
                    Action = CommonTaiSan.RequestTaiSan + CommonTaiSan.ThayDoiThongTin;
                    break;
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN:
                    Action = CommonTaiSan.RequestTaiSan + CommonTaiSan.DieuChuyenCungHeThong;
                    break;
            }
            return Action;
        }

        async Task<ResponseApi> GetMaTaiSanKho(List<LogObject> logObjects, decimal? nguonId = null)
        {

            string action = CommonTaiSan.RequestTaiSan + CommonTaiSan.GetMaTaiSan;
            var apiRespon = await _gSAPIService.PostListGSApi<LogObject>(action, logObjects, _commonFactory.GetTokenKhoCSDLQG(nguonId: nguonId));

            foreach (var item in apiRespon)
            {
                TaiSan taiSan = _taiSanService.GetTaiSanByMa(item.syncSourceId);
                if (taiSan == null && taiSan.MA_DB != null)
                    continue;
                taiSan.MA_DB = item.assetCode;
                _taiSanService.UpdateTaiSan(taiSan);
                BienDong bienDong = _bienDongService.GetAllBienDongs(taiSanId: taiSan.ID, loaiBienDong: (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO).FirstOrDefault();
                if (bienDong != null && bienDong.TRANG_THAI_DONG_BO != (decimal)enumDongBoBienDong.DA_DONG_BO)
                {
                    bienDong.TRANG_THAI_DONG_BO = (decimal)enumDongBoBienDong.DA_DONG_BO;
                    _bienDongService.UpdateBienDong(bienDong);
                }
                TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanId(taiSan.ID);
                if (taiSanNhatKy == null)
                    continue;
                taiSanNhatKy.TRANG_THAI_ID = taiSanNhatKy.BD_IDS != null ? (decimal)enumTrangThaiTaiSanNhatKy.CO_THAY_DOI : (decimal)enumTrangThaiTaiSanNhatKy.DA_DONG_BO;
                _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
            }
            return ResponseApi.CreateSuccessMessage("Done", apiRespon);
        }
        public async Task<MessageReturn> CheckLogBienDong(ResponseApi apiRespon, decimal TaiSanId = 0, Kho_DongBoTaiSan duLieuDongBo = null, decimal? nguonId = null)
        {
            CheckLog checkLog = new CheckLog()
            {
                uuid = apiRespon.Data,
                indexName = CommonLog.LogTaiSan,
                currentPage = 1,
                pageSize = int.MaxValue
            };
            string action = CommonLog.RequestLog + CommonLog.SearchLog;
            LogDetail log = await _gSAPIService.GetObjectGSApiByBody<LogDetail>(action, checkLog, _commonFactory.GetTokenKhoCSDLQG(nguonId: nguonId));
            if (log == null)
                return null;

            TaiSanNhatKy taiSanNhatKy = new TaiSanNhatKy();
            TaiSan taiSan = new TaiSan();
            LogObject logObject = new LogObject();

            List<LogResult> logSucess = log.results.Where(m => m.status == "1").ToList();
            List<LogResult> logError = log.results.Where(m => m.status != "1").ToList();
            if (logError.Count > 0)
            {
                logError = logError.Where(x => x.layer == CommonLog.LogCleaner).ToList();
                foreach (LogResult item in logError)
                {
                    logObject = item.message.toEntity<LogObject>();
                    if (logObject == null)
                        continue;
                    taiSan = _taiSanService.GetTaiSanByMa(logObject.syncSourceAssetId);
                    if (taiSan == null)
                        continue;
                    taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanId(taiSan.ID);
                    if (taiSanNhatKy == null)
                        continue;
                    Guid guidBienDong = Guid.Parse(logObject.syncSourceId);
                    BienDong bienDong = _bienDongService.GetBienDongByGuid(guidBienDong);
                    if (item.errorMessage.Contains("Đã tồn tại biền động"))
                    {
                        if (bienDong != null)
                        {
                            bienDong.TRANG_THAI_DONG_BO = (decimal)enumDongBoBienDong.DA_DONG_BO;
                            _bienDongService.UpdateBienDong(bienDong);
                        }
                        if (!String.IsNullOrEmpty(taiSanNhatKy.BD_IDS_DANG_DB))
                        {
                            List<decimal> BD_IDS_DANG_DB = taiSanNhatKy.BD_IDS_DANG_DB.Split(',').Select(c => Convert.ToDecimal(c)).ToList();
                            taiSanNhatKy.BD_IDS_DANG_DB = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: taiSanNhatKy.BD_IDS_DANG_DB, idDel: new List<decimal>() { bienDong.ID });
                        }
                        taiSanNhatKy.TRANG_THAI_ID = !String.IsNullOrEmpty(taiSanNhatKy.BD_IDS_DANG_DB) ? (decimal)enumTrangThaiTaiSanNhatKy.DANG_DONG_BO : (decimal)enumTrangThaiTaiSanNhatKy.DA_DONG_BO;
                        _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                    }
                    else
                    {
                        if (bienDong != null)
                        {
                            bienDong.TRANG_THAI_DONG_BO = (decimal)enumDongBoBienDong.THAT_BAI;
                            _bienDongService.UpdateBienDong(bienDong);
                        }
                        if (!String.IsNullOrEmpty(taiSanNhatKy.BD_IDS_DANG_DB))
                        {
                            List<decimal> BD_IDS_DANG_DB = taiSanNhatKy.BD_IDS_DANG_DB.Split(',').Select(c => Convert.ToDecimal(c)).ToList();
                            taiSanNhatKy.BD_IDS_DANG_DB = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: taiSanNhatKy.BD_IDS_DANG_DB, idDel: new List<decimal>() { bienDong.ID });
                            taiSanNhatKy.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: taiSanNhatKy.BD_IDS, idAdd: new List<decimal>() { bienDong.ID });
                        }
                        //taiSanNhatKy.BD_IDS_DANG_DB = null;
                        taiSanNhatKy.TRANG_THAI_ID = !String.IsNullOrEmpty(taiSanNhatKy.BD_IDS_DANG_DB) ? (decimal)enumTrangThaiTaiSanNhatKy.DANG_DONG_BO : (decimal)enumTrangThaiTaiSanNhatKy.DONG_BO_THAT_BAI;
                        var errorMessages = logError.Select(m => m.errorMessage).ToList();
                        taiSanNhatKy.LOG_DETAIL = string.Join("; ", errorMessages);
                        _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                    }
                }
            }
            if (logSucess.Count > 0)
            {
                logSucess = logSucess.Where(x => x.layer == CommonLog.LogSaving).ToList();
                foreach (LogResult item in logSucess)
                {
                    logObject = item.message.toEntity<LogObject>();
                    if (logObject == null)
                        continue;
                    taiSan = _taiSanService.GetTaiSanByMa(logObject.syncSourceAssetId);
                    if (taiSan == null)
                        continue;
                    taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanId(taiSan.ID);
                    if (taiSanNhatKy == null)
                        continue;
                    Guid guidBienDong = Guid.Parse(logObject.syncSourceId);
                    BienDong bienDong = _bienDongService.GetBienDongByGuid(guidBienDong);
                    if (bienDong != null)
                    {
                        bienDong.TRANG_THAI_DONG_BO = (decimal)enumDongBoBienDong.DA_DONG_BO;
                        _bienDongService.UpdateBienDong(bienDong);
                    }
                    if (!String.IsNullOrEmpty(taiSanNhatKy.BD_IDS_DANG_DB))
                    {
                        List<decimal> BD_IDS_DANG_DB = taiSanNhatKy.BD_IDS_DANG_DB.Split(',').Select(c => Convert.ToDecimal(c)).ToList();
                        taiSanNhatKy.BD_IDS_DANG_DB = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: taiSanNhatKy.BD_IDS_DANG_DB, idDel: new List<decimal>() { bienDong.ID });
                    }
                    taiSanNhatKy.TRANG_THAI_ID = !String.IsNullOrEmpty(taiSanNhatKy.BD_IDS_DANG_DB) ? (decimal)enumTrangThaiTaiSanNhatKy.DANG_DONG_BO : (decimal)enumTrangThaiTaiSanNhatKy.DA_DONG_BO;
                    _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                }

            }
            return MessageReturn.CreateSuccessMessage("done");
        }

        void GhiFile(Kho_DongBoTaiSan kho_DongBoTaiSan, decimal LoaiHinhTaiSanId, decimal LoaiBienDongId)
        {
            string fileName = "data.json";
            string fullpath;
            switch (LoaiHinhTaiSanId)
            {
                case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                    {
                        switch (LoaiBienDongId)
                        {
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO:
                                {
                                    fileName = "Dat-TangMoi.json";
                                    break;
                                }
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI:
                                {
                                    fileName = "Dat-TangNguyenGia.json";
                                    break;
                                }
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI:
                                {
                                    fileName = "Dat-GiamNguyenGia.json";
                                    break;
                                }
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN:
                                {
                                    fileName = "Dat-ThayDoiThongTin.json";
                                    break;
                                }
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN:
                                {
                                    fileName = "Dat-DieuChuyenMotPhan.json";
                                    break;
                                }
                            default:
                                break;
                        }

                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                    {
                        switch (LoaiBienDongId)
                        {
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO:
                                {
                                    fileName = "Nha-TangMoi.json";
                                    break;
                                }
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI:
                                {
                                    fileName = "Nha-TangNguyenGia.json";
                                    break;
                                }
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI:
                                {
                                    fileName = "Nha-GiamNguyenGia.json";
                                    break;
                                }
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN:
                                {
                                    fileName = "Nha-ThayDoiThongTin.json";
                                    break;
                                }
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN:
                                {
                                    fileName = "Nha-DieuChuyenMotPhan.json";
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                case (decimal)enumLOAI_HINH_TAI_SAN.OTO:
                    {
                        switch (LoaiBienDongId)
                        {
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO:
                                {
                                    fileName = "PTVT-TangMoi.json";
                                    break;
                                }
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI:
                                {
                                    fileName = "PTVT-TangNguyenGia.json";
                                    break;
                                }
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI:
                                {
                                    fileName = "PTVT-GiamNguyenGia.json";
                                    break;
                                }
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN:
                                {
                                    fileName = "PTVT-ThayDoiThongTin.json";
                                    break;
                                }
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN:
                                {
                                    fileName = "PTVT-DieuChuyenMotPhan.json";
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                default:
                    {
                        switch (LoaiBienDongId)
                        {
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO:
                                {
                                    fileName = "Khac-TangMoi.json";
                                    break;
                                }
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI:
                                {
                                    fileName = "Khac-TangNguyenGia.json";
                                    break;
                                }
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI:
                                {
                                    fileName = "Khac-GiamNguyenGia.json";
                                    break;
                                }
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN:
                                {
                                    fileName = "Khac-ThayDoiThongTin.json";
                                    break;
                                }
                            case (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN:
                                {
                                    fileName = "Khac-DieuChuyenMotPhan.json";
                                    break;
                                }
                            default:

                                break;
                        }
                    }
                    break;
            }
            string _pathStore = DateTime.Now.ToPathFolderStore();
            _pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
            _fileProvider.CreateDirectory(_pathStore);
            fullpath = _fileProvider.Combine(_pathStore, fileName);
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            serializerSettings.DateFormatString = "dd/MM/yyyy";
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            string dataJson = JsonConvert.SerializeObject(kho_DongBoTaiSan, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            _fileProvider.WriteAllText(fullpath, dataJson, Encoding.UTF8);
        }
        #region Khai thác tài sản
        [HttpGet]
        public async Task<IActionResult> DongBoKhaiThacTaiSan(int HinhThucKhaiThacId, decimal KhaiThacId, decimal? nguonId = null)
        {
            string action = "";
            switch (HinhThucKhaiThacId)
            {
                case (int)enumHinhThucKhaiThac.CHO_THUE_TS:
                    {
                        action = CommonTaiSan.RequestTaiSan + CommonTaiSan.KhaiThac_QuyetDinh + CommonTaiSan.KhaiThac_ChoThue;
                        break;
                    }
                case (int)enumHinhThucKhaiThac.SXKD:
                    {
                        action = CommonTaiSan.RequestTaiSan + CommonTaiSan.KhaiThac_QuyetDinh + CommonTaiSan.KhaiThac_KinhDoanh;
                        break;
                    }
                case (int)enumHinhThucKhaiThac.LDLK:
                    {
                        action = CommonTaiSan.RequestTaiSan + CommonTaiSan.KhaiThac_QuyetDinh + CommonTaiSan.KhaiThac_LienDoanh;
                        break;
                    }
                default:
                    {
                        action = CommonTaiSan.RequestTaiSan + CommonTaiSan.KhaiThac_QuyetDinh + CommonTaiSan.KhaiThac_SoTien;
                    }
                    break;
            }
            // get tài thông tin khai thác tài sản.
            List<KhaiThacTaiSan> ListKhaiThac = new List<KhaiThacTaiSan>();
            if (KhaiThacId > 0)
            {
                ListKhaiThac.AddRange(_khaiThacTaiSanService.GetMapByKhaiThacId(KhaiThacId));
            }
            else
            {
                ListKhaiThac = _khaiThacTaiSanService.GetAllKhaiThacTaiSans(HinhThucKhaiThacId).ToList();
            }
            var data = _khoTaiSanFactory.PrepareDuLieuDongBoKhaiThacTaiSan(ListKhaiThac, HinhThucKhaiThacId);
            if (data.Count == 0)
            {
                return Ok("Không có dữ liệu đồng bộ");
            }
            //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Chờ gửi dữ liệu đồng bộ khai thác tài sản", 0, "KhaiThacTaiSan", data);
            var apiRespon = await _gSAPIService.PostObjectGSApi<ResponseApi>(action, data, _commonFactory.GetTokenKhoCSDLQG(nguonId: nguonId));
            var datajson = data.toStringJson();
            if (apiRespon != null && apiRespon.Status)
            {
                InsertLogModel model = new InsertLogModel()
                {
                    Data = data,
                    LoaiDuLieu = "Khai thác tài sản",
                    ResponseApi = apiRespon
                };
                //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DaGuiDuLieu, "Đã gửi dữ liệu khai thác tài sản", 0, "KhaiThacTaiSan", model);
                // check log 
                CheckLog CheckLog = new CheckLog()
                {
                    uuid = apiRespon.Data,
                    indexName = CommonLog.LogTaiSan,
                    currentPage = 1,
                    pageSize = int.MaxValue
                };
                action = CommonLog.RequestLog + CommonLog.SearchLog;
                LogDetail log = await _gSAPIService.GetObjectGSApiByBody<LogDetail>(action, CheckLog, _commonFactory.GetTokenKhoCSDLQG(nguonId: nguonId));
                if (log != null)
                {
                    var ListStatus = log.results.Where(m => m.status != null).Select(m => int.Parse(m.status)).ToList();

                    if (ListStatus.Contains(0))// đồng bộ thất bại
                    {
                        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThatBai, "Đã gửi dữ liệu khai thác tài sản", 0, "KhaiThacTaiSan", model);
                    }
                    else
                    {
                        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThanhCong, "Đã gửi dữ liệu khai thác tài sản", 0, "KhaiThacTaiSan", model);
                    }
                }
            }
            else
            {
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.GuiDuLieuLoi, " Gửi dữ liệu khai thác tài sản bị lỗi", 0, "KhaiThacTaiSan", data);
            }
            return Ok(apiRespon);
        }
        #endregion
        #region Khấu hao- hao mòn tài sản
        [HttpPost]
        public async Task<IActionResult> DongBoKhauHaoTaiSan(decimal? nguonId = null)
        {

            string action = CommonTaiSan.RequestTaiSan + CommonTaiSan.HaoMonTaiSan;
            ResponseApi apiRespon = await _gSAPIService.PostObjectGSApi<ResponseApi>(action, _commonFactory.GetTokenKhoCSDLQG(nguonId: nguonId));
            return Ok(apiRespon);
        }
        //[HttpPost]
        //public async Task<IActionResult> DongBoHaoMonTaiSan([FromBody]DuLieuHaoMonKho duLieuHaoMon)
        //{
        //    if (duLieuHaoMon == null)
        //        return OkErrorResponseApi("null data");
        //    _haoMonTaiSanService.UPDATE_TRANG_THAI_DONG_BO_HAO_MON(duLieuHaoMon.data, (decimal)enumDongBoBienDong.DANG_CHAY_JOB);
        //    var hoatDong = _hoatDongService.InsertHoatDong(enumHoatDong.DbHaoMon, "Đồng bộ hao mòn tài sản " + DateTime.Now.toDateVNString(true), duLieuHaoMon);
        //    string action = CommonTaiSan.RequestTaiSan + CommonTaiSan.HaoMonTaiSan;
        //    #region Gửi dữ liệu sang kho
        //    _logger.Information(message: "Gửi dữ liệu sang kho " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //    ResponseApi apiRespon = new ResponseApi();
        //    string data = duLieuHaoMon.toStringJson();
        //    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        //    #endregion
        //    apiRespon = await _gSAPIService.PostObjectGSApiWithStringContent<ResponseApi>(action, content, _commonFactory.GetTokenKhoCSDLQG());
        //    hoatDong.MO_TA += "apiRespon: " + apiRespon;
        //    _hoatDongService.UpdateHoatDong(hoatDong);
        //    return Ok(apiRespon);
        //}
        [HttpPost]
        public async Task<IActionResult> DongBoHaoMonTaiSan(string maDonVi, decimal nguonTaiSan)
        {
            var data = _haoMonTaiSanService.GetHaoMonDongBo(maDonVi: maDonVi, nguonTaiSan: nguonTaiSan);
            if (data == null)
                return OkErrorResponseApi("Không có dữ liệu hao mòn đồng bộ");
            //_haoMonTaiSanService.UPDATE_TRANG_THAI_DONG_BO_HAO_MON(data, (decimal)enumDongBoBienDong.DANG_CHAY_JOB);
            //data = data.Select(x => { x.TAI_SAN_ID = null; return x; }).ToList();
            DuLieuHaoMonKho duLieuHaoMon = new DuLieuHaoMonKho() { data = data.Select(x => new HaoMonKhoModel(x)).ToList() };
            var hoatDong = _hoatDongService.InsertHoatDong(enumHoatDong.DbHaoMon, "Đồng bộ hao mòn tài sản " + DateTime.Now.toDateVNString(true), duLieuHaoMon);
            string action = CommonTaiSan.RequestTaiSan + CommonTaiSan.HaoMonTaiSan;
            #region Gửi dữ liệu sang kho
            ResponseApi apiRespon = new ResponseApi();
            string dataJson = duLieuHaoMon.toStringJson(isIgnoreNull: true);
            StringContent content = new StringContent(dataJson, Encoding.UTF8, "application/json");
            #endregion
            apiRespon = await _gSAPIService.PostObjectGSApiWithStringContent<ResponseApi>(action, content, _commonFactory.GetTokenKhoCSDLQG(nguonId: nguonTaiSan));
            hoatDong.MO_TA += "apiRespon: " + apiRespon;
            _hoatDongService.UpdateHoatDong(hoatDong);
            return Ok(apiRespon);
        }
        #endregion
        #endregion
        #region Đồng bộ tài sản 4.0
        /// <summary>
        /// chỉ phục vụ đổ dữ liệu lần đầu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DongBoThuCong([FromBody] ParamDongBoModel model)
        {
            //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DongBoTaiSan, "DongBoThuCong started " + DateTime.Now, 0, "DongBoTaiSan", model);
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("lỗi", ListError);
            }
            // biến động tăng mới
            List<TaiSan> taiSans = new List<TaiSan>();
            List<ResponseApi> ListResult = new List<ResponseApi>();
            if (model.DonViId > 0 && model.ListTaiSanId == null)
            {
                // lấy tất cả tài sản của đơn vị
                var ListDonViCon = _donViService.GetAllDonViChildUsingProc(model.DonViId);
                //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DongBoTaiSan, "Bắt đầu đồng bộ tài sản 4.0 sang kho của đơn vị " + model.DonViId + " " + DateTime.Now, 0, "DongBoTaiSan", model);
                foreach (var item in ListDonViCon)
                {
                    taiSans = _taiSanService.GetTaiSanCanDongBo((int)item.ID);
                    //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DongBoTaiSan, $"Bắt đầu đồng bộ tài sản 4.0 sang kho của đơn vị con {item.MA} số lượng {(taiSans != null ? taiSans.Count() : 0)} {DateTime.Now}", 0, "DongBoTaiSan", model);
                    model.ListTaiSanId = taiSans.Select(m => m.ID).ToList();
                    if (model.ListTaiSanId.Count == 0)
                    {
                        continue;
                    }
                    taiSans = taiSans.Select(c => { c.TRANG_THAI_ID = (int)enumTrangThaiTaiSanNhatKy.DANG_DONG_BO; return c; }).ToList();
                    _taiSanService.UpdateTaiSan(taiSans);
                    ResponseApi apiRespon = new ResponseApi();
                    //if (model.ListTaiSanId == null || (model.ListTaiSanId != null && model.ListTaiSanId.Count == 0))
                    //    return Ok(apiRespon);
                    //else
                    //    taiSans = _taiSanService.GetTaiSanByIds(model.ListTaiSanId.ToArray()).ToList();
                    //đẩy tài sản theo biến động
                    switch (model.LoaiBienDongId)
                    {
                        case (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO:
                            {
                                foreach (var loaihinhtaisan in taiSans.Select(c => c.LOAI_HINH_TAI_SAN_ID).ToList())
                                {
                                    var duLieuDongBo = await _khoTaiSanFactory.GetListTaiSanDongBo(_ListTaiSan: taiSans, LoaiBienDong: (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO, LoaiHinhTaiSanId: (decimal)loaihinhtaisan);
                                    if (duLieuDongBo.data.Count > 0)
                                    {
                                        apiRespon = await GuiDuLieuSangKho(duLieuDongBo, (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO);
                                        //Insert Hoạt đông
                                        //InsertLogModel insertLogModel = new InsertLogModel()
                                        //{
                                        //    Data = duLieuDongBo,
                                        //    ResponseApi = apiRespon,
                                        //    LoaiDuLieu = "Tài sản"
                                        //};
                                        //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "đồng bộ biến động tăng mới", 0, "DuLieuDongBoTaiSan", insertLogModel);//DbCho: Đang chờ đồng bộ
                                        if (apiRespon != null && apiRespon.Status)
                                        {
                                            List<string> ListMaTaiSan = new List<string>();
                                            foreach (var dl in duLieuDongBo.CapNhatIDBienDongs)
                                            {
                                                //insertLogModel = new InsertLogModel()
                                                //{
                                                //    Data = dl,
                                                //    ResponseApi = apiRespon,
                                                //    LoaiDuLieu = "Tài sản-biến động"
                                                //};
                                                // _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DaGuiDuLieu, "Chờ cập nhật mã tài sản kho", dl.TaiSanId, "DuLieuDongBoTaiSan", insertLogModel);
                                                TaiSanNhatKy tsnk = _taiSanNhatKyService.GetByTaiSanId(dl.TaiSanId);
                                                if (tsnk != null)
                                                {
                                                    tsnk.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: tsnk.BD_IDS, idDel: dl.ListBienDongId);
                                                    tsnk.BD_IDS_DANG_DB = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: tsnk.BD_IDS_DANG_DB, idAdd: dl.ListBienDongId);
                                                    tsnk.NGAY_DONG_BO = apiRespon.Date;
                                                    tsnk.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CHO_GET_MA;
                                                    tsnk.RESPONSE = apiRespon.toStringJson();
                                                    _taiSanNhatKyService.UpdateTaiSanNhatKy(tsnk);
                                                    ListMaTaiSan.Add(tsnk.Taisan.MA);
                                                }
                                            }
                                        }
                                    }
                                }
                                //hoạt động
                                //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DongBoTaiSan, "Đã đồng bộ tài sản 4.0 sang kho của đơn vị " + model.DonViId + " " + DateTime.Now + " đã đẩy sang nhưng chưa lấy mã", 0, "DongBoTaiSan", model);
                                break;
                            }
                        case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI:
                            {
                                apiRespon = await DongBoBienDongThuCong((decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI, model.ListTaiSanId.ToList());
                                break;
                            }
                        case (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN:
                            {
                                apiRespon = await DongBoBienDongThuCong((decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN, model.ListTaiSanId.ToList());
                                break;
                            }
                        case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI:
                            {
                                apiRespon = await DongBoBienDongThuCong((decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI, model.ListTaiSanId.ToList());
                                break;
                            }
                        case (decimal)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN:
                            {
                                apiRespon = await DongBoBienDongThuCong((decimal)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN, model.ListTaiSanId.ToList());
                                break;
                            }
                        case (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO:
                            {
                                apiRespon = await DongBoBienDongThuCong((decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO, model.ListTaiSanId.ToList());
                                break;
                            }
                        default:
                            {
                                apiRespon = await DongBoBienDongThuCong((decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI, model.ListTaiSanId.ToList());

                                apiRespon = await DongBoBienDongThuCong((decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI, model.ListTaiSanId.ToList());

                                apiRespon = await DongBoBienDongThuCong((decimal)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN, model.ListTaiSanId.ToList());

                                apiRespon = await DongBoBienDongThuCong((decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN, model.ListTaiSanId.ToList());

                                apiRespon = await DongBoBienDongThuCong((decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO, model.ListTaiSanId.ToList());
                            }
                            break;
                    }
                    string fileName = "DongBoDonVi_" + model.DonViId.ToString() + "_" + item.MA + ".json";
                    string _pathStore = DateTime.Now.ToPathFolderStore();
                    _pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
                    _fileProvider.CreateDirectory(_pathStore);
                    var fullpath = _fileProvider.Combine(_pathStore, fileName);
                    var serializerSettings = new JsonSerializerSettings();
                    serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                    serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    serializerSettings.DateFormatString = "dd/MM/yyyy";
                    serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    string dataJson = JsonConvert.SerializeObject(apiRespon, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    _fileProvider.WriteAllText(fullpath, dataJson, Encoding.UTF8);
                    ListResult.Add(apiRespon);
                }

            }
            else if (model.ListTaiSanId != null && model.ListTaiSanId.Count > 0 && model.LoaiBienDongId.HasValue)
            {
                ResponseApi apiRespon = new ResponseApi();
                apiRespon = await DongBoBienDongThuCong(model.LoaiBienDongId.Value, model.ListTaiSanId.ToList());
                string fileName = "DongBoDonVi_" + model.DonViId.ToString() + "_.json";
                string _pathStore = DateTime.Now.ToPathFolderStore();
                _pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
                _fileProvider.CreateDirectory(_pathStore);
                var fullpath = _fileProvider.Combine(_pathStore, fileName);
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                serializerSettings.DateFormatString = "dd/MM/yyyy";
                serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                string dataJson = JsonConvert.SerializeObject(apiRespon, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
                _fileProvider.WriteAllText(fullpath, dataJson, Encoding.UTF8);
                ListResult.Add(apiRespon);
            }
            //hoạt động
            //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DongBoTaiSan, "Đã xong đồng bộ tài sản 4.0 sang kho của đơn vị " + model.DonViId + " " + DateTime.Now, 0, "DongBoTaiSan", model);
            return OkSuccessMessage("Ok", ListResult);
        }
        /// <summary>
        /// get mã toàn bộ tài sản có trạng thái chờ get mã trong ts nhật ký
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> CapNhatMaTaiSanKho(string NgayDongBo, int donViId, Decimal? NguonTaiSanId = null)
        //{
        //    //List<TaiSanNhatKy> taiSanNhatKy = _taiSanNhatKyService.GetAllTaiSanNhatKys(trangThai: (decimal)enumTrangThaiTaiSanNhatKy.CHO_GET_MA).ToList();
        //    //List<DateTime> ListDate = new List<DateTime>() { DateTime.Now };/*taiSanNhatKy.Select(m => m.NGAY_DONG_BO.Value.Date).Distinct().ToList();*/
        //    //    List<string> ListMaTaiSan = taiSanNhatKy.Where(m => m.NGAY_DONG_BO.Value.Date == item.Date).Select(m => m.Taisan.MA).ToList();
        //    var taiSans = _taiSanService.GetListTaiSanTheoDonVi(donViId).Where(c => c.MA_DB == null);
        //    if (NguonTaiSanId.HasValue)
        //    {
        //        taiSans = taiSans.Where(c => c.NGUON_TAI_SAN_ID == NguonTaiSanId);
        //    }
        //    List<string> ListMaTaiSan = taiSans.Select(c => c.MA).ToList();
        //    await GetMaTaiSanKho(ListMaTaiSan);
        //    return Ok(MessageReturn.CreateSuccessMessage("done"));
        //}


        [HttpPost]
        /// <summary>
        /// prepare dữ liệu đồng bộ để lưu vào bảng temp chỉ nhận listId tài sản, đơn vị cấp cha, loại biến động, loại hình tài sản
        /// dữ liệu tài sản của tất cả đơn vị cấp dưới sẽ được lưu
        /// trước hết sẽ lấy từ 4.0 còn nguồn tài sản từ QLTSNN sẽ cập nhật sau 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> PrepareDuLieuDongBo([FromBody] ParamDongBoModel model)
        {
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("lỗi", ListError);
            }
            List<decimal> ListTaiSanId = new List<decimal>();
            if (model.DonViId > 0 && model.LoaiBienDongId > 0)
            {
                // lấy tất cả tài sản của đơn vị
                //var ListDonViCon = _donViService.GetAllDonViChildUsingProc(model.DonViId).Where(m => m.TREE_LEVEL == 2).Select(m => m.ID);
                var ListDonViCon = _donViService.GetAllDonViChildUsingProc(model.DonViId).Select(m => m.ID).ToList();
                foreach (var item in ListDonViCon)
                {

                    List<TaiSan> taiSans = new List<TaiSan>();
                    List<ResponseApi> ListResult = new List<ResponseApi>();
                    taiSans = _taiSanService.GetTaiSanCanDongBo((int)item);
                    model.ListTaiSanId = taiSans.Select(m => m.ID).ToList();
                    var listloaihinhtaisan = taiSans.Select(m => m.LOAI_HINH_TAI_SAN_ID).ToList();
                    if (model.ListTaiSanId.Count == 0)
                    {
                        continue;
                    }
                    foreach (var loaihinhtaisan in listloaihinhtaisan)
                    {
                        // biến động tăng mới
                        try
                        {
                            var duLieuDongBo = await _khoTaiSanFactory.GetListTaiSanDongBo(_ListTaiSan: taiSans, LoaiHinhTaiSanId: (decimal)loaihinhtaisan, LoaiBienDong: model.LoaiBienDongId.Value);
                            if (duLieuDongBo.data.Count > 0)
                            {
                                TempDongBoTaiSanCu temp = new TempDongBoTaiSanCu();
                                temp.DATA = duLieuDongBo.toStringJson();
                                temp.DON_VI_ID = item;
                                temp.LOAI_BIEN_DONG_ID = model.LoaiBienDongId;
                                temp.TAI_SAN_ID = string.Join(",", model.ListTaiSanId);
                                temp.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO;
                                temp.NGAY_TAO = DateTime.Now;
                                temp.LOAI_HINH_TAI_SAN_ID = loaihinhtaisan;
                                _tempDongBoTaiSanCuService.InsertTempDongBoTaiSanCu(temp);
                                ListTaiSanId.AddRange(model.ListTaiSanId);
                            }
                        }
                        catch (Exception ex)
                        {
                            TempDongBoTaiSanCu temp = new TempDongBoTaiSanCu();
                            temp.DATA = ex.toStringJson();
                            temp.DON_VI_ID = item;
                            temp.LOAI_BIEN_DONG_ID = model.LoaiBienDongId;
                            temp.TAI_SAN_ID = string.Join(",", model.ListTaiSanId);
                            temp.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.LOI_DU_LIEU;
                            temp.NGAY_TAO = DateTime.Now;
                            temp.LOAI_HINH_TAI_SAN_ID = loaihinhtaisan;
                            _tempDongBoTaiSanCuService.InsertTempDongBoTaiSanCu(temp);
                            //return Ok(MessageReturn.CreateErrorMessage("Errorr", ex));
                        }
                    }
                }
            }

            return await DongBoTaiSanCu(new TempDongBoTaiSanCu() { TRANG_THAI_ID = (int)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO, NGAY_TAO = DateTime.Now, LOAI_BIEN_DONG_ID = model.LoaiBienDongId });

        }
        [HttpPost]
        public async Task<IActionResult> DongBoTaiSanCu(TempDongBoTaiSanCu model)
        {
            //list tài sản cần đồng bộ
            var listTempDB = _tempDongBoTaiSanCuService.GetTempDongBoTaiSanCus(TrangThaiId: model.TRANG_THAI_ID, NgayTao: model.NGAY_TAO, LoaiHinhTaiSanId: (decimal)model.LOAI_HINH_TAI_SAN_ID, LoaiBienDong: (decimal)model.LOAI_BIEN_DONG_ID, DonViId: (decimal)model.DON_VI_ID);
            // đẩy từng bản ghi sang khi
            foreach (var TemDB in listTempDB)
            {
                Kho_DongBoTaiSan duLieuDongBo = TemDB.DATA.toEntity<Kho_DongBoTaiSan>();
                var apiRespon = await GuiDuLieuSangKho(duLieuDongBo, (decimal)TemDB.LOAI_BIEN_DONG_ID);
                //cập nhập trạng thái
                if (apiRespon.Status)
                {
                    TemDB.TRANG_THAI_ID = (int)enumTrangThaiTaiSanNhatKy.DA_DONG_BO;
                    TemDB.NGAY_DONG_BO = DateTime.Now;
                    _tempDongBoTaiSanCuService.UpdateTempDongBoTaiSanCu(TemDB);
                }
                else
                {
                    TemDB.TRANG_THAI_ID = (int)enumTrangThaiTaiSanNhatKy.DONG_BO_THAT_BAI;
                    TemDB.NGAY_DONG_BO = DateTime.Now;
                    _tempDongBoTaiSanCuService.UpdateTempDongBoTaiSanCu(TemDB);
                }
            }
            return Ok(MessageReturn.CreateSuccessMessage("Done"));
        }
        #endregion
        async Task<ResponseApi> DongBoBienDongThuCong(decimal LoaiBienDongId, List<decimal> ListTaiSanId)
        {
            var duLieuDongBo = await _khoTaiSanFactory.GetListTaiSanDongBo(ListTaiSanId: ListTaiSanId, LoaiBienDong: LoaiBienDongId);
            ResponseApi responseApi = new ResponseApi();
            if (duLieuDongBo.data.Count > 0)
            {
                responseApi = await GuiDuLieuSangKho(duLieuDongBo, LoaiBienDongId);
                //Insert Hoạt đông
                InsertLogModel insertLogModel = new InsertLogModel()
                {
                    Data = duLieuDongBo,
                    ResponseApi = responseApi,
                    LoaiDuLieu = "Tài sản"
                };
                //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "đồng bộ biến động" + _nhanHienThiService.GetGiaTriEnum<enumLOAI_LY_DO_TANG_GIAM>((enumLOAI_LY_DO_TANG_GIAM)LoaiBienDongId), 0, "DuLieuDongBoTaiSan", insertLogModel);//DbCho: Đang chờ đồng bộ
                if (responseApi.Status)
                {
                    foreach (var dl in duLieuDongBo.CapNhatIDBienDongs)
                    {
                        insertLogModel = new InsertLogModel()
                        {
                            Data = dl,
                            ResponseApi = responseApi,
                            LoaiDuLieu = "Tài sản-biến động"
                        };
                        //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DaGuiDuLieu, "Đã gửi dữ liệu đồng bộ biến động" + _nhanHienThiService.GetGiaTriEnum<enumLOAI_LY_DO_TANG_GIAM>((enumLOAI_LY_DO_TANG_GIAM)LoaiBienDongId), dl.TaiSanId, "DuLieuDongBoTaiSan", insertLogModel);
                        TaiSanNhatKy tsnk = _taiSanNhatKyService.GetByTaiSanId(dl.TaiSanId);
                        if (tsnk != null)
                        {
                            tsnk.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: tsnk.BD_IDS, idDel: dl.ListBienDongId);
                            tsnk.BD_IDS_DANG_DB = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: tsnk.BD_IDS_DANG_DB, idAdd: dl.ListBienDongId);
                            tsnk.NGAY_DONG_BO = responseApi.Date;
                            tsnk.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CHO_DONG_BO;
                            tsnk.RESPONSE = responseApi.toStringJson();
                            _taiSanNhatKyService.UpdateTaiSanNhatKy(tsnk);
                        }
                    }
                    //await CheckLogBienDong(responseApi);
                    _dB_QueueProcessService.InsertActionToQueue(action: enumSendRequest.CheckLogBienDongKho, obj: responseApi, level: (int)enumLevelQueueProcessDB.HIGHEST);
                }
            }
            return responseApi;
        }
        #region anhnt
        /// <summary>
        /// chỉ phục vụ đổ dữ liệu lần đầu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DongBoBienDongKhacTangMoi([FromBody] ParamDongBoModel model)
        {
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DongBoTaiSan, "DongBoBienDongKhacTangMoi started " + DateTime.Now, 0, "DongBoTaiSan", model);
            if (model == null)
                return NullModelResponseApi();
            List<BienDong> bienDongs = new List<BienDong>();
            if (model.DonViId > 0)
            {
                bienDongs = _bienDongService.GetAllBienDongKhacTangMoiCanDongBo(donViChaId: model.DonViId, TakeNumber: model.TakeNumber, nguonTaiSan: model.NguonTaiSanId.GetValueOrDefault(0));
            }
            else
            {
                bienDongs = _bienDongService.GetAllBienDongKhacTangMoiCanDongBo(TakeNumber: model.TakeNumber, nguonTaiSan: model.NguonTaiSanId.GetValueOrDefault(0));
            }
            if (bienDongs == null || (bienDongs != null && bienDongs.Count == 0))
            {
                return OkErrorMessage("Không có biến động khác tăng mới cần đồng bộ");
            }
            _bienDongService.UPDATE_TRANG_THAI_DONG_BO_BIEN_DONG(bienDongs, (decimal)enumDongBoBienDong.DANG_CHAY_JOB);
            List<ResponseApi> ListResult = new List<ResponseApi>();
            // lấy tất cả tài sản của đơn vị
            //List<BienDong> bienDongs = _bienDongService.GetBienDongByIds(model.BienDongIds.ToArray()).ToList();
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DongBoTaiSan, "Bắt đầu đồng bộ biến động tài sản khác tăng mới " + DateTime.Now, 0, "DongBoTaiSan", model);
            List<decimal> lstLoaiBienDong = new List<decimal>()
            {
                (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI,
                (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI,
                (decimal)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN,
                (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN,
                (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO
            };
            if (model.LoaiBienDongId > 0 && lstLoaiBienDong.Contains(model.LoaiBienDongId.Value))
                lstLoaiBienDong = new List<decimal>()
            {
                model.LoaiBienDongId.Value
            };
            List<BienDong> lstBienDong = new List<BienDong>();
            List<decimal> taiSanIds = new List<decimal>();
            ResponseApi responseApi = new ResponseApi();
            Kho_DongBoTaiSan duLieuDongBo = new Kho_DongBoTaiSan();
            foreach (decimal loaiBienDong in lstLoaiBienDong)
            {
                lstBienDong = bienDongs.Where(x => x.LOAI_BIEN_DONG_ID == loaiBienDong).ToList();
                duLieuDongBo = _khoTaiSanFactory.PrepareBienDongCanDongBo(lstBienDong);

                if (duLieuDongBo.data.Count > 0)
                {

                    taiSanIds = lstBienDong.Select(x => x.TAI_SAN_ID).Distinct().ToList();
                    foreach (decimal taisanId in taiSanIds)
                    {
                        TaiSanNhatKy tsnk = _taiSanNhatKyService.GetByTaiSanId(taisanId);
                        if (tsnk != null)
                        {
                            List<decimal> bdids = bienDongs.Where(x => x.TAI_SAN_ID == taisanId).Select(x => x.ID).ToList();
                            tsnk.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: tsnk.BD_IDS, idDel: bdids);
                            tsnk.BD_IDS_DANG_DB = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: tsnk.BD_IDS_DANG_DB, idAdd: bdids);
                            tsnk.NGAY_DONG_BO = responseApi.Date;
                            tsnk.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.DANG_DONG_BO;
                            tsnk.RESPONSE = responseApi.toStringJson();
                            _taiSanNhatKyService.UpdateTaiSanNhatKy(tsnk);
                        }
                    }
                    responseApi = await GuiDuLieuSangKho(duLieuDongBo, loaiBienDong);

                    ListResult.Add(responseApi);
                    //Insert Hoạt đông
                    InsertLogModel insertLogModel = new InsertLogModel()
                    {
                        Data = duLieuDongBo,
                        ResponseApi = responseApi,
                        LoaiDuLieu = "Tài sản"
                    };
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "đồng bộ biến động" + _nhanHienThiService.GetGiaTriEnum((enumLOAI_LY_DO_TANG_GIAM)loaiBienDong), 0, "DuLieuDongBoTaiSan", insertLogModel);//DbCho: Đang chờ đồng bộ

                    if (responseApi.Status)
                    {
                        List<LogObject> logObjects = duLieuDongBo.data.Select(x => new LogObject() { syncSourceId = x.syncSourceId }).ToList();
                        foreach (var dl in duLieuDongBo.CapNhatIDBienDongs)
                        {
                            insertLogModel = new InsertLogModel()
                            {
                                Data = dl,
                                ResponseApi = responseApi,
                                LoaiDuLieu = "Tài sản-biến động"
                            };
                            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DaGuiDuLieu, "Đã gửi dữ liệu đồng bộ biến động" + _nhanHienThiService.GetGiaTriEnum<enumLOAI_LY_DO_TANG_GIAM>((enumLOAI_LY_DO_TANG_GIAM)loaiBienDong), dl.TaiSanId, "DuLieuDongBoTaiSan", insertLogModel);
                        }
                        _dB_QueueProcessService.InsertActionToQueue(action: enumSendRequest.CheckLogBienDongKho, obj: logObjects, level: (int)enumLevelQueueProcessDB.HIGHEST);
                        //await CheckLogBienDong(apiRespon: responseApi, duLieuDongBo: duLieuDongBo);
                    }
                }
            }
            //hoạt động
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DongBoTaiSan, "Đã xong đồng bộ tài sản biến động tài sản khác tăng mới " + DateTime.Now, 0, "DongBoTaiSan", model);
            return OkSuccessResponseApi("Done", ListResult);
        }

        /// <summary>
        /// Đồng bộ biến động tăng mới
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DongBoBienDongTaiSanTangMoi([FromBody] ParamDongBoModel model)
        {
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DongBoTaiSan, "DongBoBienDongTaiSan started " + DateTime.Now, 0, "DongBoTaiSan", model);
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorResponseApi("lỗi", ListError);
            }
            // biến động tăng mới

            List<ResponseApi> ListResult = new List<ResponseApi>();
            // lấy tất cả tài sản của đơn vị
            model.TakeNumber = model.TakeNumber == 0 ? 100 : model.TakeNumber;
            List<BienDong> bienDongs = new List<BienDong>();
            if (model.DonViId > 0)
            {
                bienDongs = _bienDongService.GetAllBienDongTangMoiCanDongBo(donViChaId: model.DonViId, TakeNumber: model.TakeNumber, nguonTaiSan: model.NguonTaiSanId.GetValueOrDefault(0));
            }
            else
            {
                bienDongs = _bienDongService.GetAllBienDongTangMoiCanDongBo(TakeNumber: model.TakeNumber, nguonTaiSan: model.NguonTaiSanId.GetValueOrDefault(0));
            }
            if (bienDongs == null || (bienDongs != null && bienDongs.Count == 0))
            {
                return OkErrorMessage("Không có biến động tăng mới cần đồng bộ");
            }
            _bienDongService.UPDATE_TRANG_THAI_DONG_BO_BIEN_DONG(bienDongs, (decimal)enumDongBoBienDong.DANG_CHAY_JOB);
            int total = bienDongs.Count();
            int pageSize = 10000;
            int TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;
            for (int i = 0; i < TotalPages; i++)
            {
                var lstSplit = bienDongs.Skip(i * pageSize).Take(pageSize).ToList();
                Kho_DongBoTaiSan duLieuDongBo = _khoTaiSanFactory.PrepareBienDongTangMoiCanDongBo(model.DonViId, lstSplit);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DongBoTaiSan, $"Bắt đầu đồng bộ biến động tăng mới sang kho của đơn vị {model.DonViId} số lượng {(duLieuDongBo.data != null ? duLieuDongBo.data.Count() : 0)} {DateTime.Now}", 0, "DongBoTaiSan", duLieuDongBo);
                ResponseApi apiRespon = new ResponseApi();
                if (duLieuDongBo.data.Count > 0)
                {
                    apiRespon = await GuiDuLieuSangKho(duLieuDongBo, (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO);
                    if (apiRespon != null && apiRespon.Status)
                    {
                        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DongBoTaiSan, $"Gửi đồng bộ biến động tăng mới sang kho của đơn vị {model.DonViId} số lượng {(duLieuDongBo.data != null ? duLieuDongBo.data.Count() : 0)} {DateTime.Now} thành công", 0, "DongBoTaiSan", duLieuDongBo);
                        foreach (var dl in duLieuDongBo.CapNhatIDBienDongs)
                        {
                            TaiSanNhatKy tsnk = _taiSanNhatKyService.GetByTaiSanId(dl.TaiSanId);
                            if (tsnk != null)
                            {
                                tsnk.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: tsnk.BD_IDS, idDel: new List<decimal>() { dl.BienDongId });
                                tsnk.BD_IDS_DANG_DB = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: tsnk.BD_IDS_DANG_DB, idAdd: new List<decimal>() { dl.BienDongId });
                                tsnk.NGAY_DONG_BO = DateTime.Now;
                                tsnk.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CHO_GET_MA;
                                tsnk.RESPONSE = apiRespon.toStringJson();
                                _taiSanNhatKyService.UpdateTaiSanNhatKy(tsnk);
                            }
                        }
                    }
                }
                else
                {
                    return OkErrorMessage("Không có biến động tăng mới cần đồng bộ");
                }
                string fileName = "DongBoDonVi_" + model.DonViId.ToString() + "_" + model.DonViId + ".json";
                string _pathStore = DateTime.Now.ToPathFolderStore();
                _pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
                _fileProvider.CreateDirectory(_pathStore);
                var fullpath = _fileProvider.Combine(_pathStore, fileName);
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                serializerSettings.DateFormatString = "dd/MM/yyyy";
                serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                string dataJson = JsonConvert.SerializeObject(apiRespon, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
                _fileProvider.WriteAllText(fullpath, dataJson, Encoding.UTF8);
                ListResult.Add(apiRespon);
            }
            //hoạt động
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DongBoTaiSan, "Đã xong đồng bộ tài sản 4.0 sang kho của đơn vị " + model.DonViId + " " + DateTime.Now, 0, "DongBoTaiSan", model);
            if (ListResult != null && ListResult.Count > 0)
                return OkSuccessResponseApi("Done", ListResult);
            else return OkErrorResponseApi("Không có dữ liệu đồng bộ");
        }

        [HttpPost]
        public IActionResult GetJsonBienDong(decimal BienDongId,decimal nguonID)
        {
            DongBoApi_BienDongTaiSan dongBoApi_BienDongTaiSans = _bienDongService.GET_INFO_BIEN_DONG_BY_ID(BienDongId);
            dongBoApi_BienDongTaiSans.ID = BienDongId;
            var rs = _dongBoServices.PrepareBienDongDongBo(dongBoApi_BienDongTaiSans, nguonID);
            return OkSuccessResponseApi("Done", rs);
        }
        [HttpPost]
        public async Task<IActionResult> CapNhatMaTaiSanKho([FromBody]List<LogObject> logObjects)
        {
            string action = CommonTaiSan.RequestTaiSan + CommonTaiSan.GetMaTaiSan;
            StringContent content = new StringContent(logObjects.toStringJson(isIgnoreNull: true), Encoding.UTF8, "application/json");
            String ma_tai_san = logObjects.FirstOrDefault()?.syncSourceId;
            TaiSan ts = _taiSanService.GetTaiSanByMa(ma_tai_san);
            if (ts == null)
                return OkErrorResponseApi("tai san null");
            var apiRespon = await _gSAPIService.PostListObjectGSApiWithStringContent<LogObject>(action, content, _commonFactory.GetTokenKhoCSDLQG(nguonId: ts.NGUON_TAI_SAN_ID));

            foreach (var item in apiRespon)
            {
                if (String.IsNullOrEmpty(item.assetCode))
                    continue;
                TaiSan taiSan = _taiSanService.GetTaiSanByMa(item.syncSourceId);
                if (taiSan == null && taiSan.MA_DB != null)
                    continue;
                taiSan.MA_DB = item.assetCode;
                _taiSanService.UpdateTaiSan(taiSan);
                BienDong bienDong = _bienDongService.GetAllBienDongs(taiSanId: taiSan.ID, loaiBienDong: (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO).FirstOrDefault();
                if (bienDong != null && bienDong.TRANG_THAI_DONG_BO != (decimal)enumDongBoBienDong.DA_DONG_BO)
                {
                    bienDong.TRANG_THAI_DONG_BO = (decimal)enumDongBoBienDong.DA_DONG_BO;
                    _bienDongService.UpdateBienDong(bienDong);
                }
                TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanId(taiSan.ID);
                if (taiSanNhatKy == null)
                    continue;
                taiSanNhatKy.TRANG_THAI_ID = taiSanNhatKy.BD_IDS != null ? (decimal)enumTrangThaiTaiSanNhatKy.CO_THAY_DOI : (decimal)enumTrangThaiTaiSanNhatKy.DA_DONG_BO;
                _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
            }
            return OkSuccessResponseApi("Ok", apiRespon);
        }

        [HttpPost]
        public async Task<IActionResult> CapNhatMaTaiSanKho2([FromBody]ParamDongBoModel model)
        {
            List<LogObject> logObjects = new List<LogObject>();
            logObjects = _taiSanService.GetTaiSanCanLayMa(model.MaDonVi, model.NguonTaiSanId.GetValueOrDefault(0))?.Select(x => new LogObject() { syncSourceId = x.MA.ToString() }).ToList();
            string action = CommonTaiSan.RequestTaiSan + CommonTaiSan.GetMaTaiSan;
            StringContent content = new StringContent(logObjects.toStringJson(isIgnoreNull: true), Encoding.UTF8, "application/json");
            var apiRespon = await _gSAPIService.PostListObjectGSApiWithStringContent<LogObject>(action, content, _commonFactory.GetTokenKhoCSDLQG(nguonId: model.NguonTaiSanId));

            foreach (var item in apiRespon)
            {
                if (String.IsNullOrEmpty(item.assetCode))
                    continue;
                TaiSan taiSan = _taiSanService.GetTaiSanByMa(item.syncSourceId);
                if (taiSan == null && taiSan.MA_DB != null)
                    continue;
                taiSan.MA_DB = item.assetCode;
                _taiSanService.UpdateTaiSan(taiSan);
                //BienDong bienDong = _bienDongService.GetAllBienDongs(taiSanId: taiSan.ID, loaiBienDong: (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO).FirstOrDefault();
                //if (bienDong != null && bienDong.TRANG_THAI_DONG_BO != (decimal)enumDongBoBienDong.DA_DONG_BO)
                //{
                //    bienDong.TRANG_THAI_DONG_BO = (decimal)enumDongBoBienDong.DA_DONG_BO;
                //    _bienDongService.UpdateBienDong(bienDong);
                //}
                //TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanId(taiSan.ID);
                //if (taiSanNhatKy == null)
                //    continue;
                //taiSanNhatKy.TRANG_THAI_ID = taiSanNhatKy.BD_IDS != null ? (decimal)enumTrangThaiTaiSanNhatKy.CO_THAY_DOI : (decimal)enumTrangThaiTaiSanNhatKy.DA_DONG_BO;
                //_taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
            }
            return OkSuccessResponseApi("Ok", apiRespon);
        }
        [HttpPost]
        public async Task<IActionResult> CheckLogBienDongKho([FromBody]List<LogObject> logObjects)
        {
            string action = CommonTaiSan.RequestTaiSan + CommonTaiSan.CheckBienDong;
            StringContent content = new StringContent(logObjects.toStringJson(isIgnoreNull: true), Encoding.UTF8, "application/json");
            string dataJson = content.ReadAsStringAsync().Result;
            Guid guidBienDong = Guid.Parse(logObjects.FirstOrDefault()?.syncSourceId);
            BienDong bienDong = _bienDongService.GetBienDongByGuid(guidBienDong);
            if (bienDong == null)
                return OkErrorResponseApi("Err");
            var apiRespon = await _gSAPIService.PostListObjectGSApiWithStringContent<LogObject>(action, content, _commonFactory.GetTokenKhoCSDLQG(nguonId: bienDong.taisan.NGUON_TAI_SAN_ID));
            if (apiRespon == null)
                return null;

            TaiSanNhatKy taiSanNhatKy = new TaiSanNhatKy();
            TaiSan taiSan = new TaiSan();
            LogObject logObject = new LogObject();

            List<LogObject> logSucess = apiRespon.Where(m => m.assetMutationId != null).ToList();
            List<LogObject> logError = apiRespon.Where(m => m.assetMutationId == null).ToList();
            if (logSucess.Count > 0)
            {
                foreach (LogObject item in logSucess)
                {
                    guidBienDong = Guid.Parse(item.syncSourceId);
                    bienDong = _bienDongService.GetBienDongByGuid(guidBienDong);
                    if (bienDong != null && bienDong.TRANG_THAI_DONG_BO != (decimal)enumDongBoBienDong.DA_DONG_BO)
                    {
                        bienDong.TRANG_THAI_DONG_BO = (decimal)enumDongBoBienDong.DA_DONG_BO;
                        _bienDongService.UpdateBienDong(bienDong);
                        taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanId(bienDong.TAI_SAN_ID);
                        if (taiSanNhatKy == null)
                            continue;
                        if (!String.IsNullOrEmpty(taiSanNhatKy.BD_IDS_DANG_DB))
                        {
                            List<decimal> BD_IDS_DANG_DB = taiSanNhatKy.BD_IDS_DANG_DB.Split(',').Select(c => Convert.ToDecimal(c)).ToList();
                            taiSanNhatKy.BD_IDS_DANG_DB = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: taiSanNhatKy.BD_IDS_DANG_DB, idDel: new List<decimal>() { bienDong.ID });
                        }
                        taiSanNhatKy.TRANG_THAI_ID = !String.IsNullOrEmpty(taiSanNhatKy.BD_IDS_DANG_DB) ? (decimal)enumTrangThaiTaiSanNhatKy.DANG_DONG_BO : (decimal)enumTrangThaiTaiSanNhatKy.DA_DONG_BO;
                        _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                    }

                }

            }
            if (logError.Count > 0)
            {
                foreach (LogObject item in logError)
                {

                    guidBienDong = Guid.Parse(item.syncSourceId);
                    bienDong = _bienDongService.GetBienDongByGuid(guidBienDong);

                    if (bienDong != null && bienDong.TRANG_THAI_DONG_BO != (decimal)enumDongBoBienDong.THAT_BAI)
                    {
                        bienDong.TRANG_THAI_DONG_BO = (decimal)enumDongBoBienDong.THAT_BAI;
                        _bienDongService.UpdateBienDong(bienDong);
                        taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanId(bienDong.TAI_SAN_ID);
                        if (taiSanNhatKy == null)
                            continue;
                        if (!String.IsNullOrEmpty(taiSanNhatKy.BD_IDS_DANG_DB))
                        {
                            List<decimal> BD_IDS_DANG_DB = taiSanNhatKy.BD_IDS_DANG_DB.Split(',').Select(c => Convert.ToDecimal(c)).ToList();
                            taiSanNhatKy.BD_IDS_DANG_DB = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: taiSanNhatKy.BD_IDS_DANG_DB, idDel: new List<decimal>() { bienDong.ID });
                            taiSanNhatKy.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: taiSanNhatKy.BD_IDS, idAdd: new List<decimal>() { bienDong.ID });
                        }
                        //taiSanNhatKy.BD_IDS_DANG_DB = null;
                        taiSanNhatKy.TRANG_THAI_ID = !String.IsNullOrEmpty(taiSanNhatKy.BD_IDS_DANG_DB) ? (decimal)enumTrangThaiTaiSanNhatKy.DANG_DONG_BO : (decimal)enumTrangThaiTaiSanNhatKy.DONG_BO_THAT_BAI;
                        _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                    }
                }
            }

            return OkSuccessResponseApi("Ok", apiRespon);
        }
        [HttpPost]
        public async Task<IActionResult> CheckLogHaoMonKho([FromBody]List<AssetCodeHaoMon> haoMonKhos)
        //public async Task<IActionResult> CheckLogHaoMonKho(string maDonVi, decimal nguonTaiSan, decimal trangThaiDongBoId = 1)
        {
            //List<AssetCodeHaoMon> haoMonKhos = new List<AssetCodeHaoMon>();
            //haoMonKhos = _haoMonTaiSanService.GetHaoMonCanCheckLog(maDonVi, nguonTaiSan, trangThaiDongBoId).Take(1000).ToList();
            //if (haoMonKhos == null || (haoMonKhos != null && haoMonKhos.Count == 0))
            //{
            //    return OkErrorResponseApi("Không có dữ liệu");
            //}
            string action = CommonTaiSan.RequestTaiSan + CommonTaiSan.GetHaoMonKho;
            var models = haoMonKhos.Select(c => new { assetCode = c.assetCode }).ToList();
            StringContent content = new StringContent(models.toStringJson(isIgnoreNull: true), Encoding.UTF8, "application/json");
            string dataJson = content.ReadAsStringAsync().Result;
            String ma_tai_san = models.FirstOrDefault()?.assetCode;
            TaiSan ts = _taiSanService.GetTaiSanByMa(ma_tai_san);
            if (ts == null)
                return OkErrorResponseApi("tai san null");
            var apiRespon = await _gSAPIService.PostListObjectGSApiWithStringContent<HaoMonKhoModel>(action, content, _commonFactory.GetTokenKhoCSDLQG(nguonId: ts.NGUON_TAI_SAN_ID));
            if (apiRespon == null)
                return null;
            List<string> maTaiSans = apiRespon.Select(x => x.assetCode).Distinct().ToList();
            TaiSan taiSan;
            List<HaoMonTaiSan> haoMonTaiSans = new List<HaoMonTaiSan>();
            HaoMonKhoModel haoMonKho = new HaoMonKhoModel();
            foreach (string maTaiSan in maTaiSans)
            {
                taiSan = _taiSanService.GetTaiSanByMaDB(Ma: maTaiSan, NguonTaiSanId: (decimal)enumNguonTaiSan.CSDLQGTSC);
                if (taiSan != null && taiSan.ID > 0)
                {
                    haoMonTaiSans = _haoMonTaiSanService.GetListHaoMonTaiSans(taiSan.ID).ToList();
                    foreach (HaoMonTaiSan haoMonTaiSan in haoMonTaiSans)
                    {
                        haoMonKho = apiRespon.Where(c => c.assetCode == maTaiSan && c.year == haoMonTaiSan.NAM_HAO_MON).FirstOrDefault();
                        if (haoMonKho != null)
                        {
                            if (haoMonTaiSan.TRANG_THAI_DONG_BO != (decimal)enumDongBoBienDong.DA_DONG_BO || haoMonTaiSan.DB_ID != haoMonKho.id)
                            {
                                haoMonTaiSan.TRANG_THAI_DONG_BO = (decimal)enumDongBoBienDong.DA_DONG_BO;
                                haoMonTaiSan.DB_ID = haoMonKho.id;
                                _haoMonTaiSanService.UpdateHaoMonTaiSan(haoMonTaiSan);
                            }

                        }
                        else
                        {
                            if (haoMonTaiSan.TRANG_THAI_DONG_BO != (decimal)enumDongBoBienDong.THAT_BAI || haoMonTaiSan.DB_ID != null)
                            {
                                haoMonTaiSan.TRANG_THAI_DONG_BO = (decimal)enumDongBoBienDong.THAT_BAI;
                                haoMonTaiSan.DB_ID = null;
                                _haoMonTaiSanService.UpdateHaoMonTaiSan(haoMonTaiSan);
                            }
                        }
                    }
                }
            }
            List<string> maTaiSanErrs = haoMonKhos.Where(x => !maTaiSans.Contains(x.assetCode)).Select(c => c.assetCode).ToList();

            foreach (string maTaiSan in maTaiSanErrs)
            {
                taiSan = _taiSanService.GetTaiSanByMaDB(Ma: maTaiSan, NguonTaiSanId: (decimal)enumNguonTaiSan.CSDLQGTSC);
                if (taiSan != null && taiSan.ID > 0)
                {
                    haoMonTaiSans = _haoMonTaiSanService.GetListHaoMonTaiSans(taiSan.ID).ToList();
                    haoMonTaiSans = haoMonTaiSans.Select(x => { x.TRANG_THAI_DONG_BO = (decimal)enumDongBoBienDong.THAT_BAI; x.DB_ID = null; return x; }).ToList();
                    _haoMonTaiSanService.UpdateHaoMonTaiSan(haoMonTaiSans);
                }
            }
            return OkSuccessResponseApi("Ok", apiRespon);
        }

        [HttpPost]
        public virtual IActionResult GetJsonBienDongTangMoi([FromBody]ParamDongBoModel model)
        {
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorResponseApi("lỗi", ListError);
            }
            // biến động tăng mới

            List<ResponseApi> ListResult = new List<ResponseApi>();
            // lấy tất cả tài sản của đơn vị
            model.TakeNumber = model.TakeNumber == 0 ? int.MaxValue : model.TakeNumber;
            List<BienDong> bienDongs = new List<BienDong>();
            if (model.DonViId > 0)
            {
                bienDongs = _bienDongService.GetAllBienDongTangMoi(donViChaId: model.DonViId, nguonTaiSan: model.NguonTaiSanId.GetValueOrDefault(0), TakeNumber: model.TakeNumber);
            }
            else
            {
                bienDongs = _bienDongService.GetAllBienDongTangMoi(nguonTaiSan: model.NguonTaiSanId.GetValueOrDefault(0), TakeNumber: model.TakeNumber);
            }
            if (bienDongs == null || (bienDongs != null && bienDongs.Count == 0))
            {
                return OkErrorResponseApi("Không có biến động tăng mới cần đồng bộ");
            }

            Kho_TaiSan_BienDong kho_TaiSan_BienDong = new Kho_TaiSan_BienDong();
            DongBoApi_BienDongTaiSan dongBoApi_BienDongTaiSans = new DongBoApi_BienDongTaiSan();
            Kho_DongBoTaiSan duLieuDongBo = new Kho_DongBoTaiSan();
            List<Kho_TaiSan_BienDong> kho_TaiSan_BienDongs = new List<Kho_TaiSan_BienDong>();
            foreach (BienDong bienDong in bienDongs)
            {
                dongBoApi_BienDongTaiSans = _bienDongService.GET_INFO_BIEN_DONG_BY_ID(bienDong.ID);
                if (dongBoApi_BienDongTaiSans == null)
                    continue;
                dongBoApi_BienDongTaiSans.ID = bienDong.ID;
                kho_TaiSan_BienDong = _dongBoServices.PrepareBienDongDongBo(dongBoApi_BienDongTaiSans, model.NguonTaiSanId.GetValueOrDefault(0));
                if (kho_TaiSan_BienDong == null)
                    continue;
                kho_TaiSan_BienDongs.Add(kho_TaiSan_BienDong);
            }
            duLieuDongBo.data = kho_TaiSan_BienDongs;

            if (duLieuDongBo.data.Count > 0)
            {
                duLieuDongBo = _dongBoServices.ConfigDataByLoaiHinhTaiSan(duLieuDongBo, (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO);
                return OkSuccessResponseApi("Ok", duLieuDongBo.toStringJson(isIgnoreNull: true));
            }
            else
            {
                return OkErrorResponseApi("Không có biến động tăng mới cần đồng bộ");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetJsonBienDongKhacTangMoi([FromBody] ParamDongBoModel model)
        {
            if (model == null)
                return NullModelResponseApi();
            List<BienDong> bienDongs = new List<BienDong>();
            if (model.DonViId > 0)
            {
                bienDongs = _bienDongService.GetAllBienDongKhacTangMoiCanDongBo(donViChaId: model.DonViId, TakeNumber: model.TakeNumber, nguonTaiSan: model.NguonTaiSanId.GetValueOrDefault(0), loaiBienDongId: model.LoaiBienDongId);
            }
            else
            {
                bienDongs = _bienDongService.GetAllBienDongKhacTangMoiCanDongBo(TakeNumber: model.TakeNumber, nguonTaiSan: model.NguonTaiSanId.GetValueOrDefault(0), loaiBienDongId: model.LoaiBienDongId);
            }
            if (bienDongs == null || (bienDongs != null && bienDongs.Count == 0))
            {
                return OkErrorMessage("Không có biến động khác tăng mới cần đồng bộ");
            }

            Kho_DongBoTaiSan duLieuDongBo = new Kho_DongBoTaiSan();
            Kho_TaiSan_BienDong kho_TaiSan_BienDong = new Kho_TaiSan_BienDong();
            DongBoApi_BienDongTaiSan dongBoApi_BienDongTaiSans = new DongBoApi_BienDongTaiSan();
            List<Kho_TaiSan_BienDong> kho_TaiSan_BienDongs = new List<Kho_TaiSan_BienDong>();
            foreach (BienDong bienDong in bienDongs)
            {
                dongBoApi_BienDongTaiSans = _bienDongService.GET_INFO_BIEN_DONG_BY_ID(bienDong.ID);
                if (dongBoApi_BienDongTaiSans == null)
                    continue;
                dongBoApi_BienDongTaiSans.ID = bienDong.ID;
                kho_TaiSan_BienDong = _dongBoServices.PrepareBienDongDongBo(dongBoApi_BienDongTaiSans, model.NguonTaiSanId.GetValueOrDefault(0));
                if (kho_TaiSan_BienDong == null)
                    continue;
                kho_TaiSan_BienDongs.Add(kho_TaiSan_BienDong);
            }
            duLieuDongBo.data = kho_TaiSan_BienDongs;
            if (duLieuDongBo.data.Count > 0)
            {
                duLieuDongBo = _dongBoServices.ConfigDataByLoaiHinhTaiSan(duLieuDongBo, model.LoaiBienDongId.Value);
                return OkSuccessResponseApi("Ok", duLieuDongBo.toStringJson(isIgnoreNull: true));
            }
            else
            {
                return OkErrorResponseApi("Không có biến động khác tăng mới cần đồng bộ");
            }
        }

        #endregion

    }


}
