using GS.Core;
using GS.Core.Configuration;
using GS.Core.Domain.Api;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.SHTD;
using GS.Core.Domain.TaiSans;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.NghiepVu;
using GS.Services.SHTD;
using GS.Services.TaiSans;
using GS.Web.Framework.Kendoui;
using GS.WebApi.Factories;
using GS.WebApi.Factories.Common;
using GS.WebApi.Factories.Common.ConsumingApi;
using GS.WebApi.Infrastructure.Mapper.Extensions;
using GS.WebApi.Models;
using GS.WebApi.Models.TaiSanXacLap;
using GS.WebApi.Models.TaiSan;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.ExpressionGraph.FunctionCompilers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using GS.Services.Logging;

namespace GS.WebApi.Controllers
{
    public class TaiSanSvcController : BaseAdminController
    {
        #region Ctor
        private readonly ITaiSanSvcFactory _taiSanSvcFactory;
        private readonly IKho_TaiSanFactory _kho_TaiSanFactory;
        private readonly IBienDongService _bienDongService;
        private readonly IDBTaiSanService _dBTaiSanService;
        private readonly GSConfig _gSConfig;
        private readonly ILogger _logger;
        private readonly ILogsDongBoCsdlqgService _logsDongBoCsdlqgService;
        private readonly ITaiSanService _taiSanService;
        public TaiSanSvcController(
            ITaiSanSvcFactory taiSanSvcFactory,
            IKho_TaiSanFactory kho_TaiSanFactory,
            IBienDongService bienDongService,
            IDBTaiSanService dBTaiSanService,
            GSConfig gSConfig,
            ILogger logger,
            ILogsDongBoCsdlqgService logsDongBoCsdlqgService,
            ITaiSanService taiSanService
            )
        {
            this._taiSanSvcFactory = taiSanSvcFactory;
            this._kho_TaiSanFactory = kho_TaiSanFactory;
            this._bienDongService = bienDongService;
            this._dBTaiSanService = dBTaiSanService;
            this._gSConfig = gSConfig;
            this._logger = logger;
            this._logsDongBoCsdlqgService = logsDongBoCsdlqgService;
            this._taiSanService = taiSanService;
        }
        #endregion
        #region method
        public IActionResult Ping(string s)
        {
            return Ok(string.Format("Hello {0}", s));
        }
        #region TaiSanTD
        [HttpPost]
        public virtual IActionResult UpdateListQuyetDinhTichThu([FromBody] List<QuyetDinhTichThuModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            var result = _taiSanSvcFactory.UpdateQuyetDinhTichThuModel(model);
            if (result.Code != MessageReturn.SUCCESS_CODE)
                result.ObjectInfo = model;

            var client = new WebClient();
            var content = client.OpenRead(_gSConfig.UrlQLTSC + "NoBase/DongBoTaiSanDaNhan");
            return Ok(result);
        }
        [HttpPost]
        public virtual IActionResult Test([FromBody] TestAPIModel model)
        {

            return Ok(model);
        }
        [HttpPost]
        public virtual IActionResult UpdateListTaiSanToanDan([FromBody] List<TaiSanToanDanModel> listModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            var result = _taiSanSvcFactory.UpdateTaiSanToanDanModel(listModel);
            if (result.Code != MessageReturn.SUCCESS_CODE)
                result.ObjectInfo = listModel;

            var client = new WebClient();
            var content = client.OpenRead(_gSConfig.UrlQLTSC + "NoBase/DongBoTaiSanDaNhan");
            return Ok(result);
        }
        [HttpPost]
        public virtual IActionResult UpdateListPhuongAnXuLy([FromBody] List<XuLyModel> listModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            var result = _taiSanSvcFactory.UpdatePhuongAnXuLyModel(listModel);
            if (result.Code != MessageReturn.SUCCESS_CODE)
                result.ObjectInfo = listModel;

            var client = new WebClient();
            var content = client.OpenRead(_gSConfig.UrlQLTSC + "NoBase/DongBoTaiSanDaNhan");
            return Ok(result);
        }
        [HttpPost]
        public virtual IActionResult UpdateListTaiSanTdXuLy([FromBody] List<TSToanDanXuLyModel> listModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            var result = _taiSanSvcFactory.UpdateTaiSanToanDanXuLyModel(listModel);
            if (result.Code != MessageReturn.SUCCESS_CODE)
                result.ObjectInfo = listModel;

            var client = new WebClient();
            var content = client.OpenRead(_gSConfig.UrlQLTSC + "NoBase/DongBoTaiSanDaNhan");
            return Ok(result);
        }
        [HttpPost]
        public virtual IActionResult UpdateListKetQuaXuLy([FromBody] List<KetQuaXuLyTaiSanModel> listModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            var result = _taiSanSvcFactory.UpdateKetQuaXuLyModel(listModel);
            if (result.Code != MessageReturn.SUCCESS_CODE)
                result.ObjectInfo = listModel;

            var client = new WebClient();
            var content = client.OpenRead(_gSConfig.UrlQLTSC + "NoBase/DongBoTaiSanDaNhan");
            return Ok(result);
        }
        [HttpPost]
        public virtual IActionResult UpdateListThuChi([FromBody] List<ThuChiModel> listModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            var result = _taiSanSvcFactory.UpdateThuChiModel(listModel);
            if (result.Code != MessageReturn.SUCCESS_CODE)
                result.ObjectInfo = listModel;

            var client = new WebClient();
            var content = client.OpenRead(_gSConfig.UrlQLTSC + "NoBase/DongBoTaiSanDaNhan");
            return Ok(result);
        }
        #endregion
        #region TaiSan
        [HttpGet]
        public IActionResult GetAllTaiSans(int LOAI_HINH_TAI_SAN_ID, int? pageSize, int? pageIndex, int DonViId)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (pageSize == null)
            {
                pageSize = 100;
            }
            if (pageIndex == null)
            {
                pageIndex = 0;
            }
            var ListTaiSan = _taiSanSvcFactory.GetAllTaiSans(LOAI_HINH_TAI_SAN_ID, pageSize: pageSize.Value, pageIndex: pageIndex.Value, DonViId: DonViId);
            return Ok(ListTaiSan);


            //var ListTaiSan = _kho_TaiSanFactory.GetAllTaiSanDongBo(ListMaTaiSan);
        }
        [HttpGet]
        public IActionResult GetInfoTaiSan(String MaTaiSan)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (String.IsNullOrEmpty(MaTaiSan))
            {
                return OkErrorResponseApi("MaTaiSan null");
            }
            var model = _taiSanService.GetTaiSanByMa(MaTaiSan).ToModel<TaiSanModel>();
            return Ok(model);
        }
        [HttpPost]
        public virtual IActionResult UpdateListTaiSan([FromBody] List<TaiSanDBModel> model)
        {
            _logger.Information(message: "UpdateListTaiSan start at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), dataJson: model.toStringJson());
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            if (currentSource == (decimal)enumNguonTaiSan.CSDLQGTSC || currentSource == (decimal)enumNguonTaiSan.QLTSNN)
                return OkErrorMessage("Không có quyền");
            #endregion
            if (model == null)
            {
                return NullModel();
            }
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                //{
                //   ErrorMessage=m.,
                //   Key =m.
                //});
                //foreach (DBTaiSan dBTaiSan in dBTaiSans)
                //{
                //    dBTaiSan.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.LoiDL;
                //    dBTaiSan.RESPONSE = MessageReturn.CreateErrorMessage("Error", ListError).toStringJson();
                //    _dBTaiSanService.UpdateTaiSan(dBTaiSan);
                //}
                var error = OkErrorMessage("Error", ListError);
                _logger.Information(message: "UpdateListTaiSan lỗi " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), dataJson: error.toStringJson());
                return OkErrorMessage("Error", ListError);
            }

            List<DBTaiSan> dBTaiSans = new List<DBTaiSan>();
            List<MessageReturn> lstMess = new List<MessageReturn>();
            decimal nguonTaiSan = currentSource;
            foreach (TaiSanDBModel taiSanDBModel in model)
            {
                if (!ModelState.IsValid)
                {
                    DBTaiSan dBTaiSan = new DBTaiSan()
                    {
                        DATA_JSON = model.toStringJson(isIgnoreNull: true),
                        DB_MA = taiSanDBModel.MA,
                        IS_BIEN_DONG = false,
                        IS_TAI_SAN_TOAN_DAN = false,
                        LOAI_HINH_TAI_SAN_ID = taiSanDBModel.LOAI_HINH_TAI_SAN_ID,
                        LOAI_TAI_SAN_ID = taiSanDBModel.LOAI_TAI_SAN_ID,
                        NGAY_DONG_BO = DateTime.Now,
                        QUYET_DINH_TICH_THU_NGAY = taiSanDBModel.QUYET_DINH_NGAY.toDateSys(CommonHelper.DATE_FORMAT_DB),
                        QUYET_DINH_TICH_THU_SO = taiSanDBModel.QUYET_DINH_SO,
                        TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.LoiDL,
                        PHAN_MEM_DONG_BO_ID = nguonTaiSan
                    };
                    _dBTaiSanService.InsertTaiSan(dBTaiSan);
                    dBTaiSans.Add(dBTaiSan);
                }

            }
            //List<TaiSanDBModel> listTaiSanDB = new List<TaiSanDBModel>
            //foreach (var item in model)
            //{
            //    var result = _taiSanSvcFactory.UpdateTaiSan(item);
            //}
            var result = _taiSanSvcFactory.UpdateListTaiSan(model, currentUser, nguonTaiSan);
            if (result.Code != MessageReturn.SUCCESS_CODE)
                result.ObjectInfo = model;

            //var client = new WebClient();
            //var content = client.OpenRead(_gSConfig.UrlQLTSC + "NoBase/DongBoTaiSanDaNhan");
            return Ok(result);
        }

        [HttpPost]
        public virtual IActionResult DeleteTaiSan([FromBody] List<LogObject> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (currentSource == (decimal)enumNguonTaiSan.CSDLQGTSC || currentSource == (decimal)enumNguonTaiSan.QLTSNN)
                return OkErrorMessage("Không có quyền");
            if (model == null)
            {
                return NullModel();
            }
            string strMaTaiSan = String.Join(",", model.Select(x => x.MA_TAI_SAN).ToList());
            try
            {
                var rs = _taiSanService.DeleteTaiSansLogic(strMaTaiSan: strMaTaiSan);
                if (rs)
                    return OkSuccessMessage("Đã Xoá xong");
                else
                    return OkErrorMessage("Xoá thất bại");
            }
            catch (Exception ex)
            {
                return OkErrorMessage("Xoá thất bại error: " + ex.Message);
            }

        }
        [HttpPost]
        public virtual IActionResult UpdateListBienDong([FromBody] List<BienDongDBModel> model)
        {

            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (currentSource == (decimal)enumNguonTaiSan.CSDLQGTSC || currentSource == (decimal)enumNguonTaiSan.QLTSNN)
                return OkErrorMessage("Không có quyền");
            if (model == null || model.Count == 0)
                return this.NullModel();
            foreach (BienDongDBModel item in model)
            {
                if (String.IsNullOrEmpty(item.MA_TAI_SAN_DB))
                {
                    ModelState.AddModelError("MA_TAI_SAN_DB", "MA_TAI_SAN_DB không được trống");
                }
                else
                {
                    TaiSan ts = _taiSanService.GetTaiSanByMaDB(Ma: item.MA_TAI_SAN_DB, NguonTaiSanId: currentSource);
                    if (ts == null)
                    {
                        ModelState.AddModelError("MA_TAI_SAN_DB", "MA_TAI_SAN_DB không tồn tại");
                    }


                }
            }
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }

            List<BienDongDBModel> lstErr = new List<BienDongDBModel>();
            var result = _taiSanSvcFactory.UpdateBienDong(model, currentUser, currentSource);
            if (result.Code != MessageReturn.SUCCESS_CODE)
            {
                return Ok(result);
            }
            else
            {
                // insert biến động vào bảng tạm. nếu nếu biến động đã có ở bảng tạm mà chưa đồng bộ thì cập nhật lại data Json
                // kiểm tra biến động đã được đồng Bộ
                foreach (var item in model)
                {
                    DBTaiSan dBTaiSan = _dBTaiSanService.GetTaiSanByMa(DB_MA: item.ID_DB, nguonTaiSanId: currentSource);
                    if (dBTaiSan == null)
                    {
                        item.NGUON_TAI_SAN_ID = currentSource;
                        dBTaiSan = new DBTaiSan();
                        dBTaiSan.IS_BIEN_DONG = true;
                        dBTaiSan.GUID = Guid.NewGuid();
                        dBTaiSan.IS_TAI_SAN_TOAN_DAN = false;
                        dBTaiSan.DB_MA = item.ID_DB;
                        dBTaiSan.NGAY_DONG_BO = DateTime.Now;
                        item.GUID = dBTaiSan.GUID.ToString();
                        dBTaiSan.DATA_JSON = item.toStringJson();
                        dBTaiSan.PHAN_MEM_DONG_BO_ID = currentSource;
                        dBTaiSan.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.ChuaInsert;
                        _dBTaiSanService.InsertTaiSan(dBTaiSan);
                    }
                    else
                    {
                        item.NGUON_TAI_SAN_ID = currentSource;
                        dBTaiSan.PHAN_MEM_DONG_BO_ID = currentSource;
                        dBTaiSan.DATA_JSON = item.toStringJson();
                        dBTaiSan.NGAY_DONG_BO = DateTime.Now;
                        dBTaiSan.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.ChuaInsert;
                        _dBTaiSanService.UpdateTaiSan(dBTaiSan);
                    }
                }
                return Ok(MessageReturn.CreateSuccessMessage("Tiếp nhận thành công", null));
            }
        }
        //[HttpPost]
        //public virtual IActionResult DeleteTaiSan(List<string> ListMaTaiSan)
        //{
        //    #region check token
        //    if (!CheckCurrentUser())
        //        return OkErrorMessage("Token hết hạn");
        //    #endregion
        //    int TotalSucc = 0;
        //    int TotalError = 0;
        //    if (!ModelState.IsValid)
        //    {
        //        var ListError = ModelState.SerializeErrors();
        //        return OkErrorMessage("Error", ListError);
        //    }
        //    List<string> ListResult = new List<string>();
        //    foreach (var item in ListMaTaiSan)
        //    {
        //        var result = _taiSanSvcFactory.DeleteTaiSan(item);
        //        if (result.Code != MessageReturn.SUCCESS_CODE)
        //        {
        //            TotalError++;
        //        }
        //        else
        //        {
        //            TotalSucc++;
        //        }
        //        ListResult.Add(item + " - " + result.Message);
        //    }

        //    if (TotalError > 0)
        //    {
        //        return Ok(new MessageReturn()
        //        {
        //            Code = MessageReturn.SUCCESS_PARTIAL_CODE,
        //            Message = "Total " + TotalSucc.ToString() + " success - Total " + TotalError.ToString() + " error",
        //            ObjectInfo = ListResult
        //        });
        //    }
        //    else
        //    {
        //        return Ok(new MessageReturn()
        //        {
        //            Code = MessageReturn.SUCCESS_CODE,
        //            Message = "Success done",
        //            ObjectInfo = ListResult
        //        });
        //    }
        //}
        [HttpPost]
        public virtual IActionResult DeleteBienDong([FromBody] List<DeleteModel> ListID_DB)
        {
            if (currentSource == (decimal)enumNguonTaiSan.CSDLQGTSC || currentSource == (decimal)enumNguonTaiSan.QLTSNN)
                return OkErrorMessage("Không có quyền");
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            List<string> ListResult = new List<string>();
            //foreach (var item in ListID_DB)
            //{
            //    var result = _taiSanSvcFactory.DeleteBienDong(item.ID_DB);
            //    if (result.Code == MessageReturn.SUCCESS_CODE)
            //    {
            //        ListResult.Add($"ID_DB={item} Done");
            //    }
            //    else
            //    {
            //        ListResult.Add(result.Message);
            //    }
            //}
            return Ok(ListResult);
        }

        [HttpPost]
        public virtual IActionResult UpdateLogsTaiSanDB([FromBody] List<LogsDongBoCsdlqgModel> models)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (models == null)
            {
                return NullModel();
            }
            _logger.Information(message: "Nhận mã tài sản kho started at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            MessageReturn result = new MessageReturn();
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<MessageReturn> ListErr = new List<MessageReturn>();
            foreach (LogsDongBoCsdlqgModel model in models)
            {
                result = _taiSanSvcFactory.UpdateLogsTaiSanDongBo(model);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    ListErr.Add(result);
                }
            }
            if (ListErr.Count > 0)
            {
                return OkErrorMessage("Error", ListErr);
            }
            return Ok(result);
        }

        //public virtual IActionResult UpdateLogsTaiSanDBDaNhan()
        //{
        //    List<LogsDongBoCsdlqg> lst = _logsDongBoCsdlqgService.GetAllLogsDongBoCsdlqgs
        //    List<MessageReturn> ListErr = new List<MessageReturn>();
        //    foreach (LogsDongBoCsdlqgModel model in models)
        //    {
        //        result = _taiSanSvcFactory.UpdateLogsTaiSanDongBo(model);
        //        if (result.Code != MessageReturn.SUCCESS_CODE)
        //        {
        //            ListErr.Add(result);
        //        }
        //        return Ok(result);
        //    }
        //    if (ListErr.Count > 0)
        //    {
        //        return OkErrorMessage("Error", ListErr);
        //    }
        //    return Ok(result);
        //}
        #endregion
        #region TaiSanNhatKy
        [HttpPost]
        public virtual IActionResult UpdateTaiSanDaDongBo([FromBody] NhatKyDongBo model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _taiSanSvcFactory.UpdateTaiSanDaDongBo(model.ListMaTaiSan.ToList(), model.ListQuyetDinhTichThu.ToList(), model.MaDonVi);
            return Ok(result);
        }
        #endregion
        #region KhaiThac
        //[HttpPost]
        //public virtual IActionResult UpdateKhaiThacTaiSan([FromBody] DBKhaiThacTaiSanModel model)
        //{
        //    if (model == null)
        //        return this.NullModel();
        //    var result = _taiSanSvcFactory.UpdateKhaiThacTaiSan(model);
        //    if (result.Code != MessageReturn.SUCCESS_CODE)
        //        model.Error = result.Message;
        //    result.ObjectInfo = model;
        //    return Ok(result);
        //}
        [HttpPost]
        public virtual IActionResult UpdateListKhaiThacTaiSan([FromBody] List<DBKhaiThacTaiSanModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<DBKhaiThacTaiSanModel> lstErr = new List<DBKhaiThacTaiSanModel>();
            List<DBKhaiThacTaiSanModel> lstSucc = new List<DBKhaiThacTaiSanModel>();
            foreach (var item in model)
            {
                var result = _taiSanSvcFactory.UpdateKhaiThacTaiSan(item);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    lstErr.Add(item);
                }
                else
                {
                    lstSucc.Add(item);
                }
            }
            if (lstErr.Count > 0)
                return Ok(lstErr);
            return Ok(MessageReturn.CreateSuccessMessage("Succes done", lstSucc));
        }
        #endregion
        #region Hao mòn- khấu hao tài sản
        [HttpPost]
        public virtual IActionResult UpdateListHaoMonTaiSan([FromBody] List<HaoMonDBModel> ListModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (ListModel == null)
            {
                return NullModel();
            }
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<HaoMonDBModel> LstError = new List<HaoMonDBModel>();

            foreach (HaoMonDBModel item in ListModel)
            {
                int i = ListModel.Count(c => c.MA_TAI_SAN_DB == item.MA_TAI_SAN_DB && c.NAM_HAO_MON == item.NAM_HAO_MON);
                if (i > 1)
                {
                    item.MESSAGE = "Bị lặp";
                    LstError.Add(item);
                }
            }
            if (LstError.Count > 0)
            {
                LstError = LstError.OrderBy(c => c.MA_TAI_SAN_DB + c.NAM_HAO_MON).ToList();
                return Ok(MessageReturn.CreateErrorMessage("Có hao mòn bị lặp MA_TAI_SAN_DB và NAM_HAO_MON", LstError));
            }

            MessageReturn messageReturn = _taiSanSvcFactory.UpdateListHaoMonTaiSan(ListModel, currentUser);
            return Ok(messageReturn);
        }
        [HttpPost]
        public virtual IActionResult DeleteHaoMonTaiSan([FromBody] List<DeleteModel> ListModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _taiSanSvcFactory.DeleteHaoMonTaiSan(ListModel.Select(m => m.ID).ToList());
            return Ok(result);
        }
        [HttpPost]
        public virtual IActionResult UpdateListKhauHaoTaiSan([FromBody] List<KhauHaoDBModel> ListModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (ListModel == null)
            {
                return NullModel();
            }
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<KhauHaoDBModel> LstError = new List<KhauHaoDBModel>();
            foreach (KhauHaoDBModel item in ListModel)
            {
                int i = ListModel.Count(c => c.MA_TAI_SAN_DB == item.MA_TAI_SAN_DB && c.NAM_KHAU_HAO == item.NAM_KHAU_HAO);
                if (i > 1)
                {
                    item.MESSAGE = "Bị lặp";
                    LstError.Add(item);
                }
            }
            if (LstError.Count > 0)
            {
                LstError = LstError.OrderBy(c => c.MA_TAI_SAN_DB + c.NAM_KHAU_HAO).ToList();
                return Ok(MessageReturn.CreateErrorMessage("Có khấu hao bị lặp MA_TAI_SAN_DB và NAM_KHAU_HAO", LstError));
            }
            MessageReturn messageReturn = _taiSanSvcFactory.UpdateListKhauHaoTaiSan(ListModel, currentUser);
            return Ok(messageReturn);
        }
        [HttpPost]
        public virtual IActionResult DeleteKhauHaoTaiSan([FromBody] List<DeleteModel> ListModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _taiSanSvcFactory.DeleteHaoMonTaiSan(ListModel.Select(m => m.ID).ToList());
            return Ok(result);
        }
        #endregion
        #endregion
    }
}
