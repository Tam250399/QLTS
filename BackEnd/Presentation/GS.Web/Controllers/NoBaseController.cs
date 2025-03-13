using GS.Core;
using GS.Core.Configuration;
using GS.Core.Domain.BaoCaos;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.Common;
using GS.Core.Domain.DB;
using GS.Services.BaoCaos;
using GS.Services.Common;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.HeThong;
using GS.Web.Factories.BaoCaos;
using GS.Web.Factories.DongBo;
using GS.Web.Models.BaoCaos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GS.Services.Logging;
using GS.Web.Models.KeToan;
using GS.Web.Models.DongBoTaiSan;
using GS.Core.Domain.TaiSans;
using GS.Services.TaiSans;
using GS.Web.Factories.KeToan;
using System.Reflection.Metadata.Ecma335;
using GS.Services.Tasks;
using GS.Core.Domain.HeThong;
using DevExpress.DataProcessing;
using Microsoft.EntityFrameworkCore.Internal;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.BaoCaos.TS_BCTH;
using GS.Core.Domain.BaoCaos.TS_BCQH;
using GS.Core.Domain.BaoCaos.TSTD;
using GS.Core.Domain.BaoCaos.TS_BCCK;
using GS.Core.Domain.BienDongs;
using Newtonsoft.Json;
using GS.Services.KT;
using GS.Core.Domain.Api.TaiSanDBApi;
using GS.Core.Domain.Api;
using GS.Services.BienDongs;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;
using GS.Services.Authentication;
using GS.Services.Security;
using GS.Web.Factories.BienDongs;
using GS.Web.Models.Common;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using GS.Core.Domain.Api.DanhMuc;
using DevExpress.CodeParser.VB;
using System.Xml.Serialization;
using GS.Web.Models.DB;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using DevExpress.XtraReports.Templates;
using DevExpress.Xpo.Helpers;
using GS.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace GS.Web.Controllers
{
    public class NoBaseController : Controller
    {
        private readonly IQueueProcessService _queueProcessService;
        private readonly IQueueProcessModelFactory _queueProcessModelFactory;
        private readonly ICauHinhService _cauHinhService;
        private readonly IWorkContext _workContext;
        private readonly IDonViService _donViService;
        private readonly IDongBoFactory _dongBoFactory;
        private readonly IDB_QueueProcessService _dB_QueueProcessService;
        private readonly IGSAPIService _gSAPIService;
        private readonly GSConfig _gsConfig;
        private readonly IDB_QueueProcessHistoryService _dB_QueueProcessHistoryService;
        private readonly ILogger _logger;
        private readonly ITaiSanService _taiSanService;
        private readonly ITaiSanNhatKyService _taiSanNhatKyService;
        private readonly IHaoMonTaiSanLogModelFactory _haoMonTaiSanLogModelFactory;
        private readonly IHoatDongService _hoatDongService;
        private readonly IHaoMonTaiSanService _haoMonTaiSanService;
        private readonly IBienDongService _bienDongService;
        private readonly IDongBoServices _dongBoServices;
        private readonly IReportModelFactory _reportModelFactory;
        private readonly IEncryptionService _encryptionService;
        private readonly IThaoTacBienDongModelFactory _thaoTacBienDongModelFactory;
        private readonly IDBTaiSanService _dBTaiSanService;
        private readonly IDbContext _dbContext;

        public NoBaseController(IQueueProcessService queueProcessService,
            IQueueProcessModelFactory queueProcessModelFactory,
            ICauHinhService cauHinhService,
            IWorkContext workContext,
            IDonViService donViService,
            IDongBoFactory dongBoFactory,
            IDB_QueueProcessService dB_QueueProcessService,
            IGSAPIService gSAPIService,
            GSConfig gSConfig,
            IDB_QueueProcessHistoryService dB_QueueProcessHistoryService, ILogger logger,
            ITaiSanService taiSanService,
            ITaiSanNhatKyService taiSanNhatKyService,
            IHaoMonTaiSanLogModelFactory haoMonTaiSanLogModelFactory,
            IHoatDongService hoatDongService,
            IHaoMonTaiSanService haoMonTaiSanService,
            IBienDongService bienDongService,
            IDongBoServices dongBoServices,
            IReportModelFactory reportModelFactory,
            IEncryptionService encryptionService,
            IThaoTacBienDongModelFactory thaoTacBienDongModelFactory,
            IDBTaiSanService dBTaiSanService,
            IDbContext dbContext
            )
        {
            this._queueProcessService = queueProcessService;
            this._queueProcessModelFactory = queueProcessModelFactory;
            this._cauHinhService = cauHinhService;
            this._workContext = workContext;
            this._donViService = donViService;
            this._dongBoFactory = dongBoFactory;
            this._dB_QueueProcessService = dB_QueueProcessService;
            this._gSAPIService = gSAPIService;
            this._gsConfig = gSConfig;
            this._dB_QueueProcessHistoryService = dB_QueueProcessHistoryService;
            _logger = logger;
            this._taiSanService = taiSanService;
            this._taiSanNhatKyService = taiSanNhatKyService;
            this._haoMonTaiSanLogModelFactory = haoMonTaiSanLogModelFactory;
            this._hoatDongService = hoatDongService;
            this._haoMonTaiSanService = haoMonTaiSanService;
            this._bienDongService = bienDongService;
            this._dongBoServices = dongBoServices;
            this._reportModelFactory = reportModelFactory;
            this._encryptionService = encryptionService;
            this._thaoTacBienDongModelFactory = thaoTacBienDongModelFactory;
            this._dBTaiSanService = dBTaiSanService;
            this._dbContext = dbContext;
        }

        public virtual IActionResult RenderReport()
        {
            _logger.Information("Chạy vào render report");
            var first = _queueProcessService.GetQueueProcessCanXuatFileBaoCao();
            if (first != null)
            {
                _logger.Information("Bắt đầu render report");
                try
                {
                    first.TRANG_THAI_ID = (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_XUAT_FILE;
                    _queueProcessService.UpdateQueueProcessTrangThaiAndGUIDFILE(first);

                    #region var searchModel = first.SEARCH_MODEL_JSON.toEntity<BaoCaoTaiSanChiTietSearchModel>();

                    // thay cho câu trên
                    //cách truyền type vào 1 generic method
                    Type searchModelType = Type.GetType(first.SEARCH_MODEL_CLASS);
                    MethodInfo method_searchModel = typeof(CommonExtention).GetMethod(nameof(CommonExtention.toEntity), new[] { typeof(string) });
                    MethodInfo generic_searchModel = method_searchModel.MakeGenericMethod(searchModelType);
                    var searchModel = generic_searchModel.Invoke(null, new object[] { first.SEARCH_MODEL_JSON ?? "" });

                    #endregion var searchModel = first.SEARCH_MODEL_JSON.toEntity<BaoCaoTaiSanChiTietSearchModel>();

                    var bcDonVi = _donViService.GetDonViById(first.DON_VI_ID);
                    var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: bcDonVi.ID);
                    var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
                    var cauHinhModel = new CauHinhBaoCaoModel();
                    var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
                    if (cauHinh.BaoCao != null)
                        cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == first.MA_BAO_CAO) ?? new CauHinhBaoCaoModel();
                    //xuất file báo cáo
                    dynamic model = Activator.CreateInstance(Type.GetType(first.REPORT_CLASS), searchModel, cauHinhModel, cauHinhChunghModel);

                    #region model.DataSource = first.DATA_JSON.toEntities<classType>();

                    // thay cho câu trên
                    //cách truyền type vào 1 generic method
                    string modelTypeName = $"{first.MODEL_DATA_TYPE},GS.CORE";
                    Type classType = Type.GetType(modelTypeName);
                    MethodInfo method = typeof(CommonExtention).GetMethod(nameof(CommonExtention.toEntities));
                    MethodInfo generic = method.MakeGenericMethod(classType);
                    model.DataSource = generic.Invoke(null, new object[] { first.DATA_JSON ?? "" });
                    //khi mà data là null thì tạo data mặc định cho nó
                    if (model.DataSource == null)
                        model.DataSource = new List<string>();

                    #endregion model.DataSource = first.DATA_JSON.toEntities<classType>();

                    _queueProcessModelFactory.SaveFileBaoCao(model, first.MA_BAO_CAO, first.ID);
                    //hoàn thành
                }
                catch (Exception ex)
                {
                    first.TRANG_THAI_ID = (int)enumTRANG_THAI_QUEUE_BAO_CAO.LOI;
                    _queueProcessService.UpdateQueueProcessTrangThaiAndGUIDFILE(first);
                }
            }
            return Json("Không có dữ liệu");
        }
        public virtual IActionResult RenderReportDongBo(decimal queueID)
        {
            HoatDong hoatDong = _hoatDongService.InsertHoatDong(enumHoatDong.RenderReport, "RenderReportDongBo started at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            QueueProcess first;
            if (queueID > 0)
                first = _queueProcessService.GetQueueProcessById(queueID);
            else
                first = _queueProcessService.GetQueueProcessBaoCaoCanRender(true);
            if (first != null)
            {
                var result = _dongBoFactory.PrepareReport(first.ID);
                if (result.Code == MessageReturn.SUCCESS_CODE)
                {
                    try
                    {
                        first.TRANG_THAI_ID = (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_XUAT_FILE;
                        _queueProcessService.UpdateQueueProcessTrangThaiAndGUIDFILE(first);

                        #region var searchModel = first.SEARCH_MODEL_JSON.toEntity<BaoCaoTaiSanChiTietSearchModel>();

                        // var bcDonVi = _donViService.GetDonViById(first.DON_VI_ID);
                        var cauHinh = _cauHinhService.LoadCauHinh<CauHinhMapBC>();
                        var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
                        var cauHinhModel = new CauHinhBaoCaoModel();
                        var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
                        if (cauHinh.BaoCao != null)
                            cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == first.MA_BAO_CAO) ?? new CauHinhBaoCaoModel();
                        // thay cho câu trên
                        //cách truyền type vào 1 generic method
                        Type searchModelType = Type.GetType(cauHinhModel.FullName_SearchModelClass);
                        MethodInfo method_searchModel = typeof(CommonExtention).GetMethod(nameof(CommonExtention.toEntity), new[] { typeof(string) });
                        MethodInfo generic_searchModel = method_searchModel.MakeGenericMethod(searchModelType);
                        var searchModel = generic_searchModel.Invoke(null, new object[] { first.SEARCH_MODEL_JSON ?? "" });

                        #endregion var searchModel = first.SEARCH_MODEL_JSON.toEntity<BaoCaoTaiSanChiTietSearchModel>();


                        //xuất file báo cáo
                        dynamic model = Activator.CreateInstance(Type.GetType(first.REPORT_CLASS), searchModel, cauHinhModel, cauHinhChunghModel);

                        #region model.DataSource = first.DATA_JSON.toEntities<classType>();

                        // thay cho câu trên
                        //cách truyền type vào 1 generic method
                        string modelTypeName = $"{first.MODEL_DATA_TYPE},GS.CORE";
                        Type classType = Type.GetType(modelTypeName);
                        MethodInfo method = typeof(CommonExtention).GetMethod(nameof(CommonExtention.toEntities));
                        MethodInfo generic = method.MakeGenericMethod(classType);
                        model.DataSource = generic.Invoke(null, new object[] { first.DATA_JSON ?? "" });
                        //khi mà data là null thì tạo data mặc định cho nó
                        if (model.DataSource == null)
                            model.DataSource = new List<string>();

                        #endregion model.DataSource = first.DATA_JSON.toEntities<classType>();

                        _queueProcessModelFactory.SaveFileBaoCao(model, first.MA_BAO_CAO, first.ID);
                        //hoàn thành
                    }
                    catch (Exception ex)
                    {
                        first.TRANG_THAI_ID = (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_LOI;
                        _queueProcessService.UpdateQueueProcessTrangThaiAndGUIDFILE(first);
                        hoatDong.MO_TA += $" end {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} with error message {ex.Message}";
                        _hoatDongService.UpdateHoatDong(hoatDong);
                    }
                }
                else
                {
                    first.TRANG_THAI_ID = (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_LOI;
                    _queueProcessService.UpdateQueueProcessTrangThaiAndGUIDFILE(first);
                    hoatDong.MO_TA += $" end {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} with error message {result.Message}";
                    hoatDong.DU_LIEU = first.toStringJson();
                    _hoatDongService.UpdateHoatDong(hoatDong);
                }
                hoatDong.MO_TA += $" end {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} with message {result.Message}";
                hoatDong.DU_LIEU = first.toStringJson();
                _hoatDongService.UpdateHoatDong(hoatDong);
                return Json(result);
            }

            hoatDong.MO_TA += $" end {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} with message Không có dữ liệu";
            _hoatDongService.UpdateHoatDong(hoatDong);
            return Json("Không có dữ liệu");
        }

        public virtual IActionResult GetDataReport(decimal queueID, bool isDongBo = false)
        {
            QueueProcess queueProcess;
            if (queueID > 0)
                queueProcess = _queueProcessService.GetQueueProcessById(queueID);
            else
            {
                queueProcess = _queueProcessService.GetQueueProcessBaoCao(isDongBo);
            }
            String data = "";
            if (queueProcess != null)
            {
                #region get data
                if (queueProcess.MA_BAO_CAO == "TS_BCTH_08A_DK_TSC_P3")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCTH_08A_DK_TSC>(queueProcess.STATEMENT);
                    //var x = _queueProcessService.GetListDataByStatement<TS_BCTH_08A_DK_TSC>(queueProcess.STATEMENT);
                    //if (isDongBo)
                    //    x = _reportModelFactory.Group_TS_BCTH_08A_DK_TSC(x);
                    //data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCTH_08A_DK_TSC_P2")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCTH_08A_DK_TSC>(queueProcess.STATEMENT);
                    //var x = _queueProcessService.GetListDataByStatement<TS_BCTH_08A_DK_TSC>(queueProcess.STATEMENT);
                    //if (isDongBo)
                    //    x = _reportModelFactory.Group_TS_BCTH_08A_DK_TSC(x);
                    //data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCTH_08A_DK_TSC_P1")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCTH_08A_DK_TSC>(queueProcess.STATEMENT);
                    //var x = _queueProcessService.GetListDataByStatement<TS_BCTH_08A_DK_TSC>(queueProcess.STATEMENT);
                    //if (isDongBo)
                    //    x = _reportModelFactory.Group_TS_BCTH_08A_DK_TSC(x);
                    //data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCCK_9b_CK_TSC_QL_SD_TRU_SO")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    //data = _queueProcessService.GetDataByStatement<TS_BCCK_09B_CK_TSC>(queueProcess.STATEMENT);
                    var x = _queueProcessService.GetListDataByStatement<TS_BCCK_09B_CK_TSC>(queueProcess.STATEMENT);
                    //x = _reportModelFactory.(x);
                    data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCCK_9c_CK_TSC_QL_SD_OTO_KHAC")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    //data = _queueProcessService.GetDataByStatement<TS_BCCK_09C_CK_TSC>(queueProcess.STATEMENT);
                    var x = _queueProcessService.GetListDataByStatement<TS_BCCK_09C_CK_TSC>(queueProcess.STATEMENT);
                    if (isDongBo)

                        x = _reportModelFactory.Group_TS_BCCK_09C_CK_TSC(x);
                    data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCCK_9d_CK_TSC_XULY_TS")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    //data = _queueProcessService.GetDataByStatement<TS_BCCK_09D_CK_TSC>(queueProcess.STATEMENT);
                    var x = _queueProcessService.GetListDataByStatement<TS_BCCK_09D_CK_TSC>(queueProcess.STATEMENT);
                    if (isDongBo)
                        x = _reportModelFactory.Group_TS_BCCK_09D_CK_TSC(x);
                    data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCCK_9dd_CK_TSC_KHAITHAC_TC")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    var x = _queueProcessService.GetListDataByStatement<TS_BCCK_09DD_CK_TSC>(queueProcess.STATEMENT);
                    if (isDongBo)
                        x = _reportModelFactory.Group_TS_BCCK_09DD_CK_TSC(x);
                    data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCCK_10a_CK_TSC_DAUTU_XD_THUE")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    //data = _queueProcessService.GetDataByStatement<TS_BCCK_10A_CK_TSC>(queueProcess.STATEMENT);
                    var x = _queueProcessService.GetListDataByStatement<TS_BCCK_10A_CK_TSC>(queueProcess.STATEMENT);
                    if (isDongBo)
                        x = _reportModelFactory.Group_TS_BCCK_10A_CK_TSC(x);
                    data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCCK_10b_CK_TSC_QLSD_TS")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    //data = _queueProcessService.GetDataByStatement<TS_BCCK_10B_CK_TSC>(queueProcess.STATEMENT);
                    var x = _queueProcessService.GetListDataByStatement<TS_BCCK_10B_CK_TSC>(queueProcess.STATEMENT);
                    if (isDongBo)
                        x = _reportModelFactory.Group_TS_BCCK_10B_CK_TSC(x);
                    data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCCK_10c_CK_TSC_XL_TS")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    //data = _queueProcessService.GetDataByStatement<TS_BCCK_10C_CK_TSC>(queueProcess.STATEMENT);
                    var x = _queueProcessService.GetListDataByStatement<TS_BCCK_10C_CK_TSC>(queueProcess.STATEMENT);
                    if (isDongBo)
                        x = _reportModelFactory.Group_TS_BCCK_10C_CK_TSC(x);
                    data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCCK_10d_CK_TSC_KTNL_TS")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    //data = _queueProcessService.GetDataByStatement<TS_BCCK_10D_CK_TSC>(queueProcess.STATEMENT);
                    var x = _queueProcessService.GetListDataByStatement<TS_BCCK_10D_CK_TSC>(queueProcess.STATEMENT);
                    if (isDongBo)
                        x = _reportModelFactory.Group_TS_BCCK_10D_CK_TSC(x);
                    data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCQH_MAU01_THTSNN")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCQH_MAU01_THTSNN>(queueProcess.STATEMENT);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCQH_MAU02_CCTSNN")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCQH_MAU02_CCTSNN>(queueProcess.STATEMENT);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCQH_MAU04_TS_LTS")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCQH_MAU04_TS_LTS>(queueProcess.STATEMENT);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCQH_MAU05_TS_CQ_TC_DV")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCQH_MAU05_TS_CQ_TC_DV>(queueProcess.STATEMENT);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCQH_MAU06_TS_CAPQL")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCQH_MAU06_TS_CAPQL>(queueProcess.STATEMENT);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCQH_MAU07_OTO_SD")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCQH_MAU07_OTO_SD>(queueProcess.STATEMENT);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCQH_MAU08_OTO_VSD")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCQH_MAU08_OTO_VSD>(queueProcess.STATEMENT);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCQH_MAU10_TS_TREN500_MDSD")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCQH_MAU10_TS_TREN500_MDSD>(queueProcess.STATEMENT);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCQH_TANG_GIAM_PL10_TSNN")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCQH_PL10_TANG_GIAM_TSNN>(queueProcess.STATEMENT);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCQH_MAU11A_THTSNN")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCQH_MAU11A_THTSNN>(queueProcess.STATEMENT);
                }
                else if (queueProcess.MA_BAO_CAO == "BaoCaoKetQuaXuLyTSTD")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<BaoCaoKetQuaXuLyTSTD>(queueProcess.STATEMENT);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCTH_08B_DK_TSC_P1")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCTH_08B_DK_TSC>(queueProcess.STATEMENT);
                    //var x = _queueProcessService.GetListDataByStatement<TS_BCTH_08B_DK_TSC>(queueProcess.STATEMENT);
                    //if (isDongBo)
                    //    x = _reportModelFactory.Group_TS_BCTH_08B_DK_TSC(x);
                    //data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCTH_08B_DK_TSC_P2")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCTH_08B_DK_TSC>(queueProcess.STATEMENT);
                    //var x = _queueProcessService.GetListDataByStatement<TS_BCTH_08B_DK_TSC>(queueProcess.STATEMENT);
                    //if (isDongBo)
                    //    x = _reportModelFactory.Group_TS_BCTH_08B_DK_TSC(x);
                    //data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCTH_08B_DK_TSC_P3")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCTH_08B_DK_TSC>(queueProcess.STATEMENT);
                    //var x = _queueProcessService.GetListDataByStatement<TS_BCTH_08B_DK_TSC>(queueProcess.STATEMENT);
                    //if (isDongBo)
                    //    x = _reportModelFactory.Group_TS_BCTH_08B_DK_TSC(x);
                    //data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCQH_MAU11B_THTSNN")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCQH_MAU11B_THTSNN>(queueProcess.STATEMENT);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCCK_9a_CK_TSC_TINHHINH_DAUTU")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    //data = _queueProcessService.GetDataByStatement<TS_BCCK_09A_CK_TSC>(queueProcess.STATEMENT);
                    var x = _queueProcessService.GetListDataByStatement<TS_BCCK_09A_CK_TSC>(queueProcess.STATEMENT);
                    if (isDongBo)
                        x = _reportModelFactory.Group_TS_BCCK_09A_CK_TSC(x);
                    data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCCK_11a_CK_TSC_NGUONLUC_TC_TS")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    //data = _queueProcessService.GetDataByStatement<TS_BCCK_11A_CK_TSC>(queueProcess.STATEMENT);
                    var x = _queueProcessService.GetListDataByStatement<TS_BCCK_11A_CK_TSC>(queueProcess.STATEMENT);
                    if (isDongBo)
                        x = _reportModelFactory.Group_TS_BCCK_11A_CK_TSC(x);
                    data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCCK_11b_CK_TSC_QL_SD_TS")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    //data = _queueProcessService.GetDataByStatement<TS_BCCK_11B_CK_TSC>(queueProcess.STATEMENT);
                    var x = _queueProcessService.GetListDataByStatement<TS_BCCK_11B_CK_TSC>(queueProcess.STATEMENT);
                    if (isDongBo)
                        x = _reportModelFactory.Group_TS_BCCK_11B_CK_TSC(x);
                    data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCCK_11c_CK_TSC_XL_TS")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    //data = _queueProcessService.GetDataByStatement<TS_BCCK_11C_CK_TSC>(queueProcess.STATEMENT);
                    var x = _queueProcessService.GetListDataByStatement<TS_BCCK_11C_CK_TSC>(queueProcess.STATEMENT);
                    if (isDongBo)
                        x = _reportModelFactory.Group_TS_BCCK_11C_CK_TSC(x);
                    data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCCK_11d_CK_TSC_KTNL_TS")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    //data = _queueProcessService.GetDataByStatement<TS_BCCK_11D_CK_TSC>(queueProcess.STATEMENT);
                    var x = _queueProcessService.GetListDataByStatement<TS_BCCK_11D_CK_TSC>(queueProcess.STATEMENT);
                    if (isDongBo)
                        x = _reportModelFactory.Group_TS_BCCK_11D_CK_TSC(x);
                    data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "BaoCaoPhuongAnXuLyTSTD")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<BaoCaoPhuongAnXuLyTSTD>(queueProcess.STATEMENT);
                }
                else if (queueProcess.MA_BAO_CAO == "TSTD_MAU_01_BC_XLSHTD")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TSTD_MAU_01_BC_XLSHTD>(queueProcess.STATEMENT);
                }
                else if (queueProcess.MA_BAO_CAO == "TSTD_MAU_02_BC_XLSHTD")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TSTD_MAU_02_BC_XLSHTD>(queueProcess.STATEMENT);
                }
                else if (queueProcess.MA_BAO_CAO == "B03_CCTT_TSCDHH")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH>(queueProcess.STATEMENT);
                }
                else if (queueProcess.MA_BAO_CAO == "B03_CCTT_TSCDVH")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCTH_TT_CUNGCAP_TAICHINH>(queueProcess.STATEMENT);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCTH_02B_DK_TSNN")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;
                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    //data = _queueProcessService.GetDataByStatement<TS_BCTH_02B_DK_TSNN>(queueProcess.STATEMENT);
                    var x = _queueProcessService.GetListDataByStatement<TS_BCTH_02B_DK_TSNN>(queueProcess.STATEMENT);
                    if (isDongBo)
                        x = _reportModelFactory.Group_TS_BCTH_02B_DK_TSNN(x);
                    data = x.toStringJson(isIgnoreNull: true);
                }
                else if (queueProcess.MA_BAO_CAO == "TS_BCQH_MAU03_QUYDAT_MDSD")
                {
                    queueProcess.TIME_START_GET_DATA = DateTime.Now;

                    queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_LAY_DU_LIEU : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
                    _queueProcessService.UpdateQueueProcess(queueProcess);
                    data = _queueProcessService.GetDataByStatement<TS_BCQH_MAU03_QUYDAT_MDSD>(queueProcess.STATEMENT);
                }
                else
                {
                    return Json(new
                    {
                        Message = "Không tìm thấy báo cáo"
                    });
                };
                #endregion
                queueProcess.TIME_END_GET_DATA = DateTime.Now;
                queueProcess.DATA_JSON = data;
                queueProcess.TRANG_THAI_ID = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_CHO_XUAT_FILE : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_CHO_XUAT_FILE;
                _queueProcessService.UpdateQueueProcess(queueProcess);

            }
            return
                Json(new
                {
                    Code = "00",
                    Message = String.IsNullOrEmpty(data) ? "Lấy dữ liệu thất bại" : (data == "[]" ? "Không có dữ liệu" : "Lấy dữ liệu thành công"),
                    ID = queueProcess.ID,
                    FileGuid = queueProcess.GUID
                });
        }
        public virtual IActionResult DongBoTaiSanDaNhan(String maDonVi = null, int? TakeNumber = 200)
        {
            //_logger.Information("DongBoTaiSanDaNhan started at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            var result = _dongBoFactory.DongBoTaiSan(maDonVi: maDonVi, TakeNumber: TakeNumber);
            return Json(result);
        }
        //hoanglv
        public virtual IActionResult DBTaiSanDaNhan(String maDonVi = null, int? TakeNumber = 200)
        {
            //_logger.Information("DongBoTaiSanDaNhan started at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            var result = _dongBoFactory.DBTaiSan(maDonVi: maDonVi, TakeNumber: TakeNumber);
            return Json(result);
        }
        public virtual IActionResult DongBoBienDongDaTiepNhan(String DBId = null, int? TakeNumber = 200)
        {
            //_logger.Information("DongBoTaiSanDaNhan started at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            var result = _dongBoFactory.DongBoBienDongDaTiepNhan(DBId: DBId, TakeNumber: TakeNumber);
            return Json(result);
        }
        public virtual IActionResult DBBienDongDaTiepNhan(String DBId = null, int? TakeNumber = 200)
        {
            //_logger.Information("DongBoTaiSanDaNhan started at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            var result = _dongBoFactory.DBBienDongDaTiepNhan(DBId: DBId, TakeNumber: TakeNumber);
            return Json(result);
        }
        public virtual async Task<IActionResult> SendRequestApi(string id)
        {
            DB_QueueProcess first = new DB_QueueProcess();

            if (!string.IsNullOrEmpty(id))
                first = _dB_QueueProcessService.GetDB_QueueProcessById(decimal.Parse(id));
            else
                first = _dB_QueueProcessService.GetDB_QueueProcessByIdNeedToSendRequest();

            if (first != null)
            {
                //_logger.Information("Bắt đầu send request api đến " + first.URL_CALL + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                try
                {
                    first.LAST_SEND_REQUEST = DateTime.Now;
                    first.TRANG_THAI_ID = (decimal)enumTrangThaiQueueProcessDB.DA_GUI_REQUEST;
                    _dB_QueueProcessService.UpdateDB_QueueProcess(first);
                    StringContent content = new StringContent(first.DATA_INPUT, Encoding.UTF8, "application/json");
                    var result = await _gSAPIService.PostObjectGSApiWithStringContent<ResponseApi>(first.URL_CALL, content, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
                    first.LAST_SEND_REQUEST = DateTime.Now;
                    String stringGuid = "";
                    if (result != null)
                    {
                        stringGuid = Convert.ToString(result.Data);
                        first.GUID_RESPONSE = (stringGuid ?? "").Length >= 40 ? null : stringGuid;
                        first.API_RESPONSE = result.toStringJson();
                        if (result.Status)
                        {
                            first.TRANG_THAI_ID = (decimal)enumTrangThaiQueueProcessDB.GUI_REQUEST_THANH_CONG;
                        }
                        else
                        {
                            first.TRANG_THAI_ID = (decimal)enumTrangThaiQueueProcessDB.GUI_REQUEST_THAT_BAI;
                        }
                    }

                    _dB_QueueProcessService.UpdateDB_QueueProcess(first);
                    // insert lịch sử
                    DB_QueueProcessHistory processHistory = new DB_QueueProcessHistory()
                    {

                        GUID_RESPONSE = (stringGuid ?? "").Length >= 40 ? null : stringGuid,
                        QUEUE_PROCESS_ID = first.ID,
                        RESPONSE = result != null ? result.toStringJson() : null,
                        TIME_REQUEST = DateTime.Now,
                    };
                    _dB_QueueProcessHistoryService.InsertDB_QueueProcessHistory(processHistory);

                    if (first.URL_CALL == enumSendRequest.DongBoBaoCao && first.DOI_TUONG_ID.HasValue)
                    {
                        QueueProcess queueProcess = _queueProcessService.GetQueueProcessById(first.DOI_TUONG_ID.Value);
                        if (queueProcess != null)
                        {
                            queueProcess.RESPONSE = result != null ? result.toStringJson() : null;
                            _queueProcessService.UpdateQueueProcess(queueProcess);
                        }
                    }
                    return Json(result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                }
                catch (Exception ex)
                {
                    first.API_RESPONSE = null;
                    first.TRANG_THAI_ID = (decimal)enumTrangThaiQueueProcessDB.GUI_REQUEST_THAT_BAI;
                    _dB_QueueProcessService.UpdateDB_QueueProcess(first);
                    return Json(ex);
                }
            }
            return Json("done");
        }
        public virtual async Task<IActionResult> DongBoBaoCaoApi(string id)
        {
            DB_QueueProcess first = new DB_QueueProcess();

            if (!string.IsNullOrEmpty(id))
                first = _dB_QueueProcessService.GetDB_QueueProcessById(decimal.Parse(id));
            else
                first = _dB_QueueProcessService.GetDB_QueueProcessByIdNeedToSendRequest();

            if (first != null)
            {
                //_logger.Information("Bắt đầu send request api đến " + first.URL_CALL + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                try
                {
                    first.LAST_SEND_REQUEST = DateTime.Now;
                    first.TRANG_THAI_ID = (decimal)enumTrangThaiQueueProcessDB.DA_GUI_REQUEST;
                    _dB_QueueProcessService.UpdateDB_QueueProcess(first);
                    StringContent content = new StringContent(first.DATA_INPUT, Encoding.UTF8, "application/json");
                    var result = await _gSAPIService.PostObjectGSApiWithStringContent<ResponseApi>(first.URL_CALL, content, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
                    first.LAST_SEND_REQUEST = DateTime.Now;
                    String stringGuid = "";
                    if (result != null)
                    {
                        stringGuid = Convert.ToString(result.Data);
                        first.GUID_RESPONSE = (stringGuid ?? "").Length >= 40 ? null : stringGuid;
                        first.API_RESPONSE = result.toStringJson();
                        if (result.Status)
                        {
                            first.TRANG_THAI_ID = (decimal)enumTrangThaiQueueProcessDB.GUI_REQUEST_THANH_CONG;
                        }
                        else
                        {
                            first.TRANG_THAI_ID = (decimal)enumTrangThaiQueueProcessDB.GUI_REQUEST_THAT_BAI;
                        }
                    }

                    _dB_QueueProcessService.UpdateDB_QueueProcess(first);
                    // insert lịch sử
                    DB_QueueProcessHistory processHistory = new DB_QueueProcessHistory()
                    {

                        GUID_RESPONSE = (stringGuid ?? "").Length >= 40 ? null : stringGuid,
                        QUEUE_PROCESS_ID = first.ID,
                        RESPONSE = result != null ? result.toStringJson() : null,
                        TIME_REQUEST = DateTime.Now,
                    };
                    _dB_QueueProcessHistoryService.InsertDB_QueueProcessHistory(processHistory);

                    if (first.URL_CALL == enumSendRequest.DongBoBaoCao && first.DOI_TUONG_ID.HasValue)
                    {
                        QueueProcess queueProcess = _queueProcessService.GetQueueProcessById(first.DOI_TUONG_ID.Value);
                        if (queueProcess != null)
                        {
                            queueProcess.RESPONSE = result != null ? result.toStringJson() : null;
                            _queueProcessService.UpdateQueueProcess(queueProcess);
                        }
                    }
                    return Json(result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                }
                catch (Exception ex)
                {
                    first.API_RESPONSE = null;
                    first.TRANG_THAI_ID = (decimal)enumTrangThaiQueueProcessDB.GUI_REQUEST_THAT_BAI;
                    _dB_QueueProcessService.UpdateDB_QueueProcess(first);
                    return Json(ex);
                }
            }
            return Json("done");
        }
        public virtual IActionResult PrepareReport(decimal? QueueProcessID)
        {
            if (QueueProcessID.GetValueOrDefault(0) == 0)
                return null;
            var result = _dongBoFactory.PrepareReport(QueueProcessID);

            return Json(result);
        }
        /// <summary>
        /// Description: Task chốt giá trị tài sản
        /// </summary>
        /// <returns></returns>
        public virtual IActionResult ChotHaoMonTaiSan()
        {
            _haoMonTaiSanLogModelFactory.ChotToanBoTaiSan();
            return Json("done");
        }

        public virtual async Task<IActionResult> DongBoTaiSanApi(decimal DonViId, decimal LoaiBienDongId, decimal NguonTaiSanId)
        {

            var data = new
            {
                DonViId = DonViId,
                LoaiBienDongId = LoaiBienDongId,
                NguonTaiSanId = NguonTaiSanId
                //ListTaiSanId = ListTaiSanId
            };
            var rs = await _gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingTaiSan/DongBoThuCong", data, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return Json(rs);

        }
        public virtual async Task<IActionResult> DongBoTaiSanTangMoi(decimal DonViId, decimal nguonTaiSan, int TakeNumber = 1000)
        {

            var data = new
            {
                DonViId = DonViId,
                TakeNumber = TakeNumber,
                LoaiBienDongId = 1,
                NguonTaiSanId = nguonTaiSan
            };
            var rs = await _gSAPIService.PostObjectGSApi<ResponseApi>(enumSendRequest.DongBoBienDongTaiSanTangMoi, data, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return Json(rs);

        }
        public IActionResult GetFileDongBoTaiSanTangMoi(string maDonVi, decimal nguonTaiSan, int TakeNumber = int.MaxValue)
        {

            //var data = new
            //{
            //    DonViId = DonViId,
            //    TakeNumber = TakeNumber,
            //    LoaiBienDongId = 1,
            //    NguonTaiSanId = nguonTaiSan
            //};
            //var rs = await _gSAPIService.PostObjectGSApi<ResponseApi>(enumSendRequest.GetJsonBienDongTangMoi, data, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);

            //if (rs == null)
            //    return Json(rs);
            //else
            //{
            //    return new FileContentResult(Encoding.UTF8.GetBytes(rs.Data), MimeTypes.ApplicationJson)
            //    {
            //        FileDownloadName = string.Format("EXP_DB_TS_{0}_{1}({2}).json", DonViId, ((enumNguonTaiSan)nguonTaiSan).ToString(), DateTime.Now.ToString("dd-MM-yyyy HH24-mm-ss"))
            //    };
            //}
            String timeStart = DateTime.Now.ToString("dd-mm-yyyy HH:mm:ss");
            DonVi donVi = _donViService.GetDonViByMa(maDonVi);
            List<ResponseApi> ListResult = new List<ResponseApi>();
            // lấy tất cả tài sản của đơn vị
            List<BienDong> bienDongs = new List<BienDong>();
            if (donVi != null)
            {
                bienDongs = _bienDongService.GetAllBienDongTangMoi(donViChaId: donVi.ID, nguonTaiSan: nguonTaiSan, TakeNumber: TakeNumber);
            }
            else
            {
                bienDongs = _bienDongService.GetAllBienDongTangMoi(nguonTaiSan: nguonTaiSan, TakeNumber: TakeNumber);
            }
            if (bienDongs == null || (bienDongs != null && bienDongs.Count == 0))
            {
                return Json("Không có biến động tăng mới cần đồng bộ");
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
                kho_TaiSan_BienDong = _dongBoServices.PrepareBienDongDongBo(dongBoApi_BienDongTaiSans, nguonTaiSan);
                if (kho_TaiSan_BienDong == null)
                    continue;
                kho_TaiSan_BienDongs.Add(kho_TaiSan_BienDong);
            }
            duLieuDongBo.data = kho_TaiSan_BienDongs;

            if (duLieuDongBo.data.Count > 0)
            {
                duLieuDongBo = _dongBoServices.ConfigDataByLoaiHinhTaiSan(duLieuDongBo, (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO);
                return new FileContentResult(Encoding.UTF8.GetBytes(duLieuDongBo.toStringJson(isIgnoreNull: true)), MimeTypes.ApplicationJson)
                {
                    FileDownloadName = string.Format("TS_{0}({1})_{2}({3}).json", donVi?.MA, duLieuDongBo.data.Count, ((enumNguonTaiSan)nguonTaiSan).ToString(), timeStart + "->" + DateTime.Now.ToString("HH-mm-ss"))
                };
            }
            else
            {
                return Json("Không có biến động tăng mới cần đồng bộ");
            }

        }

        public IActionResult GetFileDongBoTaiSanKhacTangMoi(string maDonVi, decimal nguonTaiSan, decimal LoaiBienDongId, int TakeNumber = int.MaxValue)
        {

            //var data = new
            //{
            //    DonViId = DonViId,
            //    TakeNumber = TakeNumber,
            //    LoaiBienDongId = LoaiBienDongId,
            //    NguonTaiSanId = nguonTaiSan
            //};
            //var rs = await _gSAPIService.PostObjectGSApi<ResponseApi>(enumSendRequest.GetJsonBienDongKhacTangMoi, data, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);

            //if (rs == null)
            //    return Json(rs);
            //else
            //{
            //    return new FileContentResult(Encoding.UTF8.GetBytes(rs.Data), MimeTypes.ApplicationJson)
            //    {
            //        FileDownloadName = string.Format("EXP_DB_TS_{0}_{1}_{2}({3}).json", ((enumLOAI_LY_DO_TANG_GIAM)LoaiBienDongId).ToString(), DonViId, ((enumNguonTaiSan)nguonTaiSan).ToString(), DateTime.Now.ToString("dd-MM-yyyy HH24-mm-ss"))
            //    };
            //}
            String timeStart = DateTime.Now.ToString("dd-mm-yyyy HH:mm:ss");
            DonVi donVi = _donViService.GetDonViByMa(maDonVi);
            List<BienDong> bienDongs = new List<BienDong>();
            if (donVi != null)
            {
                bienDongs = _bienDongService.GetAllBienDongKhacTangMoiCanDongBo(donViChaId: donVi.ID, TakeNumber: TakeNumber, nguonTaiSan: nguonTaiSan, loaiBienDongId: LoaiBienDongId);
            }
            else
            {
                bienDongs = _bienDongService.GetAllBienDongKhacTangMoiCanDongBo(TakeNumber: TakeNumber, nguonTaiSan: nguonTaiSan, loaiBienDongId: LoaiBienDongId);
            }
            if (bienDongs == null || (bienDongs != null && bienDongs.Count == 0))
            {
                return Json("Không có biến động khác tăng mới cần đồng bộ");
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
                kho_TaiSan_BienDong = _dongBoServices.PrepareBienDongDongBo(dongBoApi_BienDongTaiSans, nguonTaiSan);
                if (kho_TaiSan_BienDong == null)
                    continue;
                kho_TaiSan_BienDongs.Add(kho_TaiSan_BienDong);
            }
            duLieuDongBo.data = kho_TaiSan_BienDongs;
            if (duLieuDongBo.data.Count > 0)
            {
                duLieuDongBo = _dongBoServices.ConfigDataByLoaiHinhTaiSan(duLieuDongBo, LoaiBienDongId);
                return new FileContentResult(Encoding.UTF8.GetBytes(duLieuDongBo.toStringJson(isIgnoreNull: true)), MimeTypes.ApplicationJson)
                {
                    FileDownloadName = string.Format("TS_{0}({1})_{2}_{3}({4}).json", ((enumLOAI_LY_DO_TANG_GIAM)LoaiBienDongId).ToString(), donVi?.MA, duLieuDongBo.data.Count, ((enumNguonTaiSan)nguonTaiSan).ToString(), timeStart + "->" + DateTime.Now.ToString("HH-mm-ss"))
                };
            }
            else
            {
                return Json("Không có biến động khác tăng mới cần đồng bộ");
            }
        }
        public virtual async Task<IActionResult> DongBoBienDongKhacTangMoi(decimal DonViId, decimal nguonTaiSan, decimal LoaiBienDongId = 0, int TakeNumber = 1000)
        {
            var data = new
            {
                DonViId = DonViId,
                TakeNumber = TakeNumber,
                NguonTaiSanId = nguonTaiSan,
                LoaiBienDongId = LoaiBienDongId
            };
            var rs = await _gSAPIService.PostObjectGSApi<ResponseApi>(enumSendRequest.DongBoBienDongKhacTangMoi, data, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return Json(rs);
        }


        public virtual async Task<IActionResult> CleanDanhMuc(string nameDanhMuc, bool DB_ID_NULL = true)
        {
            if (String.IsNullOrEmpty(nameDanhMuc))
                return null;
            var rs = await _gSAPIService.PostObjectGSApi<MessageReturn>($"ConsumingDanhMuc/CleanDanhMuc?danhMuc={nameDanhMuc}&DB_ID_NULL={DB_ID_NULL}", null, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return Json(rs);
        }
        public virtual async Task<IActionResult> GetJsonBienDongKho(decimal BienDongId, decimal nguonID)
        {
            if (BienDongId == 0)
                return null;
            var rs = await _gSAPIService.PostObjectGSApi<MessageReturn>($"ConsumingTaiSan/GetJsonBienDong?BienDongId={BienDongId}&nguonID={nguonID}", null, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return Json(rs, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }


        public virtual async Task<IActionResult> TestLog()
        {
            var log = String.Format("AnPX Test log - {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            _logger.Information(log);
            return Json(log);
        }

        public virtual async Task<IActionResult> updateDanhMuc(string nameDanhMuc, bool isThemMoi = false)
        {
            if (String.IsNullOrEmpty(nameDanhMuc))
                Json("nameDanhMuc is null");
            var rs = await _dongBoFactory.UpdateDanhMuc(nameDanhMuc, isThemMoi);
            return Json(rs);
        }

        public virtual async Task<IActionResult> updateDonVi(bool IsThemMoi = false, int TakeNumber = 100)
        {
            var data =
            new ParamDongBoApiModel()
            {
                IsThemMoi = IsThemMoi,
                TakeNumber = TakeNumber
            };
            var rs = await _gSAPIService.PostObjectGSApi<ResponseApi>(enumSendRequest.DongBoDonViThuCong, data, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);

            return Json(rs);
        }
        public virtual async Task<IActionResult> GenKhoDonVi(decimal? ID = 0, bool IsThemMoi = false, int TakeNumber = 100)
        {
            if (ID > 0)
            {
                var rs = _dongBoServices.PrepareKhoDonVi(id: ID.Value);
                return Json(rs, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            }
            else
            {
                var ListDonViModel = _donViService.GetAllDonVis2(!IsThemMoi, TakeNumber);
                List<Kho_DonVi_Api> rs = new List<Kho_DonVi_Api>();
                foreach (var item in ListDonViModel)
                {
                    var khoDonVi = _dongBoServices.PrepareKhoDonVi(id: item.ID);
                    rs.Add(khoDonVi);
                }
                return Json(rs, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            }
        }

        public virtual async Task<IActionResult> updateTaiSan(decimal bienDongId)
        {
            if (bienDongId == 0)
                Json("bienDongId is null");
            var rs = _thaoTacBienDongModelFactory.DuyetYeuCauFunc1(bienDongId);
            return Json(rs);
        }

        public virtual async Task<IActionResult> DongBoHaoMonTaiSan(string maDonVi, decimal nguonTaiSan)
        {

            string url = enumSendRequest.DongBoHaoMonTaiSan + $"?maDonVi={maDonVi}&nguonTaiSan={nguonTaiSan}";
            var rs = await _gSAPIService.PostObjectGSApi<ResponseApi>(url, null, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return Json(rs);
        }
        public virtual async Task<IActionResult> CheckLogHaoMonKho(string maDonVi, decimal nguonTaiSan, decimal trangThaiDongBoId = 1)
        {
            List<AssetCodeHaoMon> haoMonKhos = new List<AssetCodeHaoMon>();
            haoMonKhos = _haoMonTaiSanService.GetHaoMonCanCheckLog(maDonVi, nguonTaiSan, trangThaiDongBoId).ToList();
            if (haoMonKhos == null || (haoMonKhos != null && haoMonKhos.Count == 0))
            {
                return Json("Không có dữ liệu");
            }
            //string url = enumSendRequest.CheckLogHaoMonKho + $"?maDonVi={maDonVi}&nguonTaiSan={nguonTaiSan}&trangThaiDongBoId={trangThaiDongBoId}";
            //var rs = await _gSAPIService.PostObjectGSApi<ResponseApi>(url, null, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            //var rs = await _gSAPIService.PostObjectGSApi<ResponseApi>(enumSendRequest.CheckLogHaoMonKho, haoMonKhos, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);

            StringContent content = new StringContent(haoMonKhos.toStringJson(isIgnoreNull: true), Encoding.UTF8, "application/json");
            string dataJson = content.ReadAsStringAsync().Result;
            var rs = await _gSAPIService.PostListObjectGSApiWithStringContent<HaoMonKhoModel>(enumSendRequest.CheckLogHaoMonKho, content, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return Json(rs);
        }
        public virtual async Task<IActionResult> CheckLogBienDongKho(string maDonVi, decimal nguonTaiSan, decimal trangThaiDongBoId = 1, decimal? loaiBienDongId = null)
        {
            List<LogObject> logObjects = new List<LogObject>();
            logObjects = _bienDongService.GetAllBienDongsByTrangThaiDongBo(maDonVi, nguonTaiSan, trangThaiDongBoId, loaiBienDongId).Select(x => new LogObject() { syncSourceId = x.GUID.ToString() }).ToList();

            var rs = await _gSAPIService.PostObjectGSApi<ResponseApi>(enumSendRequest.CheckLogBienDongKho, logObjects, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return Json(rs);
        }
        public virtual async Task<IActionResult> GetMaTaiSanKho(string maDonVi, decimal nguonTaiSan)
        {
            var data = new
            {
                MaDonVi = maDonVi,
                NguonTaiSanId = nguonTaiSan,
            };
            var rs = await _gSAPIService.PostObjectGSApi<ResponseApi>(enumSendRequest.CapNhatMaTaiSanKho2, data, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return Json(rs);
        }
        public virtual async Task<IActionResult> GetMaTaiSanKho2(string maDonVi, decimal nguonTaiSan)
        {
            List<LogObject> logObjects = new List<LogObject>();
            logObjects = _taiSanService.GetTaiSanCanLayMa(maDonVi, nguonTaiSan)?.Select(x => new LogObject() { syncSourceId = x.MA.ToString() }).ToList();
            string action = CommonTaiSan.RequestTaiSan + CommonTaiSan.GetMaTaiSan;
            StringContent content = new StringContent(logObjects.toStringJson(isIgnoreNull: true), Encoding.UTF8, "application/json");
            var apiRespon = await _gSAPIService.PostListObjectGSApiWithStringContent<LogObject>(action, content, _gSAPIService.GetTokenKhoCSDLQG());
            List<string> lstStr = apiRespon.Where(x => x.assetCode != null).Select(c => String.Format("UPDATE TS_TAI_SAN SET MA_DB = '{0}' WHERE MA_DB IS NULL AND MA = '{1}';COMMIT;", c.assetCode, c.syncSourceId)).ToList();
            return Json(String.Join("\n", lstStr));
        }
        public virtual IActionResult GetJsonMaTaiSanKho(string maDonVi, decimal nguonTaiSan)
        {
            List<LogObject> logObjects = new List<LogObject>();
            logObjects = _taiSanService.GetTaiSanCanLayMa(maDonVi, nguonTaiSan)?.Select(x => new LogObject() { syncSourceId = x.MA.ToString() }).ToList();
            return Json(logObjects, new Newtonsoft.Json.JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
        public virtual IActionResult GetJsonLogBienDongKho(string maDonVi, decimal nguonTaiSan, decimal trangThaiDongBoId = 1, decimal? loaiBienDongId = null)
        {
            List<LogObject> logObjects = new List<LogObject>();
            logObjects = _bienDongService.GetAllBienDongsByTrangThaiDongBo(maDonVi, nguonTaiSan, trangThaiDongBoId, loaiBienDongId).Select(x => new LogObject() { syncSourceId = x.GUID.ToString() }).ToList();
            return Json(logObjects, new Newtonsoft.Json.JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
        public virtual IActionResult GetJsonDongBoHaoMonTaiSan(string maDonVi, decimal nguonTaiSan)
        {

            String timeStart = DateTime.Now.ToString("dd-mm-yyyy HH:mm:ss");
            var data = _haoMonTaiSanService.GetHaoMonDongBo(maDonVi: maDonVi, nguonTaiSan: nguonTaiSan);
            if (data == null)
                return Json("Không có dữ liệu hao mòn đồng bộ");
            DuLieuHaoMonKho duLieuHaoMon = new DuLieuHaoMonKho() { data = data.Select(x => new HaoMonKhoModel(x)).ToList() };
            string dataJson = duLieuHaoMon.toStringJson(isIgnoreNull: true);
            return new FileContentResult(Encoding.UTF8.GetBytes(dataJson), MimeTypes.ApplicationJson)
            {
                FileDownloadName = string.Format("KT_HAO_MON_{0}({1})_{2}_({3}).json", maDonVi, data.Count, ((enumNguonTaiSan)nguonTaiSan).ToString(), timeStart + "..." + DateTime.Now.ToString("HH-mm-ss"))
            };
        }
        public virtual IActionResult GenPassword(string pass)
        {
            string rs = "";
            var saltKey = _encryptionService.CreateSaltKey(GSCustomerServiceDefaults.PasswordSaltKeySize);
            rs = _encryptionService.CreatePasswordHash(pass, saltKey, "SHA512");
            string PASSWORD_HASH = _encryptionService.CreatePasswordHashKhoCSDLTSC(pass);
            return Json(new { password = rs, key = saltKey, PASSWORD_HASH = PASSWORD_HASH });
        }

        #region Chốt hao mòn tài sản của đơn vị

        public virtual IActionResult ChotHaoMonTaiSanDonVi()
        {
            return View();
        }
        public virtual IActionResult ChotHaoMonTaiSanDonVi(string maDonVi)
        {
            var donVi = _donViService.GetDonViByMa(maDonVi);
            if (donVi != null)
            {
                var result = _haoMonTaiSanService.ChotToanBoHaoMonDonVi(donVi.ID);
                if (result)
                {
                    return Json(MessageReturn.CreateSuccessMessage("Chốt hao mòn thành công", true));
                }
            }
            return Json(MessageReturn.CreateErrorMessage("Chốt hao mòn không thành công", false));
        }

        #endregion


        [HttpPost]
        public async Task<IActionResult> SynTaiSan([FromBody] ModelServiceApi model)
        {
            if (!model.Key.Equals("sp8cHiLPlFKEdukzi7BhwrC60FwgxkAk"))
                return Json(new { Code = "01", Status = "Key not valid" });
            StringContent content = new StringContent(model.Value, Encoding.UTF8, "application/json");
            var result = await _gSAPIService.PostObjectGSApiWithStringContent<MessageReturn>(enumSendRequest.DongBoListTaiSan, content, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return Json(result);
        }
        public virtual IActionResult GenXmlDMDC()
        {
            GsHeaderXml hearderXMl = new GsHeaderXml()
            {
                Sender_Code = "DMDC",
                Sender_Name = "Hệ thống CSDL danh mục dùng chung ngành Tài chính",
                Receiver_Code = "TTA0000000",
                Receiver_Name = "Hệ thống kết nối, chia sẻ dữ liệu số ngành Tài chính",
                Tran_Code = "0049",
                Tran_Name = "Danh mục Địa bàn hành chính",
                Msg_ID = "DMDC004923" + "1".PadLeft(7, '0'),
                Original_Code = "DMDC",
                Original_Name = "Hệ thống CSDL danh mục dùng chung ngành Tài chính",
                Finish_Code = "DMDC",
                Finish_Name = "Hệ thống CSDL danh mục dùng chung ngành Tài chính",
                Version = "1.0",
                Msg_RefID = "DMDC004923" + "1".PadLeft(7, '0'),
                Send_Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                Export_Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                Tran_Num = "5"

            };
            string xmlString = "";
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(hearderXMl.GetType());
                serializer.Serialize(stringwriter, this);
                xmlString = stringwriter.ToString();
            }
            return this.Content(xmlString, "text/xml");
        }
        [HttpPost]
        public async Task<IActionResult> SynDBTaiSan([FromBody] ModelServiceApi model)
        {
            if (!model.Key.Equals("sp8cHiLPlFKEdukzi7BhwrC60FwgxkAk"))
                return Json(new { Code = "01", Status = "Key not valid" });
            List<DBTaiSan> dbTaiSan = model.Value.toEntities<DBTaiSan>();
            _dBTaiSanService.InsertTaiSan(dbTaiSan);
            return Json("done");
        }
        public async Task<IActionResult> PostDBTaiSan(int takeNumber = 1000)
        {
            var ts = _dBTaiSanService.GetAllTaiSans(TrangThaiId: 1, TakeNumber: takeNumber);
            var model = ts.Select(x => { DBTaiSanModel m = x.ToModel<DBTaiSanModel>(); m.ID = 0; m.QLDKTS_MA = null; m.TRANG_THAI_ID = 2; return m; }).ToList();
            string stringData = model.toStringJson(isIgnoreNull: true);
            ModelServiceApi data = new ModelServiceApi()
            {
                Key = "sp8cHiLPlFKEdukzi7BhwrC60FwgxkAk",
                Value = stringData
            };
            foreach (var x in ts)
            {
                x.TRANG_THAI_ID = 10;
                _dBTaiSanService.UpdateTaiSan(x);
            }
            stringData = data.toStringJson(isIgnoreNull: true);
            StringContent content = new StringContent(stringData, Encoding.UTF8, "application/json");
            try
            {
                var result = await _gSAPIService.PostObjectGSApiWithStringContentReturnString("/NoBase/SynDBTaiSan", content, null, "https://qltsc.mof.gov.vn");
                if (result == "done")
                {
                    return Json(new { mess = model.Count() + " tài sản lên bộ 😊", result = result });
                }
                else
                {
                    foreach (var x in ts)
                    {
                        x.TRANG_THAI_ID = 1;
                        _dBTaiSanService.UpdateTaiSan(x);
                    }
                    return Json(result);
                }

            }
            catch (Exception ex)
            {
                foreach (var x in ts)
                {
                    x.TRANG_THAI_ID = 1;
                    _dBTaiSanService.UpdateTaiSan(x);
                }
                return Json(ex);
            }

            //return Json(data);

        }
        public async Task<IActionResult> PostDBTaiSan2(int takeNumber = 1000)
        {
            var ts = _dBTaiSanService.GetAllTaiSans(TrangThaiId: 1, TakeNumber: takeNumber);
            var model = ts.Select(x => { DBTaiSanModel m = x.ToModel<DBTaiSanModel>(); m.ID = 0; m.QLDKTS_MA = null; m.TRANG_THAI_ID = 2; return m; }).ToList();
            string stringData = model.toStringJson(isIgnoreNull: true);
            ModelServiceApi data = new ModelServiceApi()
            {
                Key = "sp8cHiLPlFKEdukzi7BhwrC60FwgxkAk",
                Value = stringData
            };
            foreach (var x in ts)
            {
                x.TRANG_THAI_ID = 10;
                _dBTaiSanService.UpdateTaiSan(x);
            }
            stringData = data.toStringJson(isIgnoreNull: true);
            //return Json(data);
            //Encoding.ASCII.GetBytes(stringData);
            return new FileContentResult(Encoding.UTF8.GetBytes(stringData), MimeTypes.ApplicationJson)
            {
                FileDownloadName = "ExpDBTaiSan" + DateTime.Now.ToString("ddMMyyyy") + model.Count() + ".json"
            };
        }
        public async Task<IActionResult> QuerySql(string sql)
        {
            try
            {
                if (String.IsNullOrEmpty(sql))
                    return Json("");
                sql = sql.ToUpper();
                if (!sql.StartsWith("SELECT"))
                    return Json("Không phải lệnh select");
                OracleParameter P_STATEMENT = new OracleParameter("P_STATEMENT", OracleDbType.Clob, sql, ParameterDirection.Input);
                OracleParameter MSS_OUT = new OracleParameter("MSS_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
                var result = _dbContext.EntityFromStore<MssReturn>("SP_EXEC", P_STATEMENT, MSS_OUT).ToList();
                String mss = "";
                if (result != null && result.Count() > 0)
                    mss = result.FirstOrDefault().STRJSON;
                //return Json(mss);
                // Chuyển chuỗi JSON thành mảng JSON
                var jsonArray = JsonConvert.DeserializeObject<List<object>>(mss);

                // Trả về mảng JSON
                return Json(jsonArray);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
    }
}