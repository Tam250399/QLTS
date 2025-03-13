using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GS.Core;
using GS.Core.Configuration;
using GS.Core.Domain.Api.TaiSanDBApi;
using GS.Core.Domain.DB;
using GS.Core.Domain.SHTD;
using GS.Services.Common;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.HeThong;
using GS.Services.SHTD;
using GS.Services.TaiSans;
using GS.WebApi.Common;
using GS.WebApi.Factories.Common;
using GS.WebApi.Factories.Common.ConsumingApi;
using GS.WebApi.Factories.ConsumingApi;
using GS.WebApi.Models.ConsumingApi;
using GS.WebApi.Models.ConsumingApi.TaiSanApi;
using GS.WebApi.Models.ConsumingApi.TaiSanToanDan;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GS.WebApi.Controllers
{
    public class ConsumingTaiSanXacLapController : BaseAdminController
    {

        private readonly IKho_DanhMucFactory _khoDanhMuc;
        private readonly IKho_TaiSanFactory _khoTaiSanFactory;
        private readonly IGSAPIService _gSAPIService;
        private readonly ITaiSanService _taiSanService;
        private readonly ITaiSanTdService _taiSanTdService;
        private readonly ICommonFactory _commonFactory;
        private readonly IHoatDongService _hoatDongService;
        private readonly ITaiSanNhatKyService _taiSanNhatKyService;
        private readonly GSConfig _gSConfig;
        private readonly IQuyetDinhTichThuService _quyetDinhTichThuService;
        private readonly IKho_TaiSanToanDanFactory _kho_TaiSanToanDanFactory;
        private readonly IXuLyService _xuLyService;
        private readonly IThuChiService _thuChiService;
        private readonly IKetQuaService _ketQuaService;
        private readonly IPhuongAnXuLyService _phuongAnXuLyService;
        private readonly ITaiSanTdXuLyService _taiSanTdXuLyService;

        public ConsumingTaiSanXacLapController(
            IKho_DanhMucFactory kho_DanhMuc,
            IKho_TaiSanFactory kho_TaiSanFactory,
            IGSAPIService gSAPIService,
            IKhaiThacTaiSanService khaiThacTaiSanService,
            ITaiSanService taiSanService,
            ITaiSanTdService taiSanTdService,
            ICommonFactory commonFactory,
            IHoatDongService hoatDongService,
            ITaiSanNhatKyService taiSanNhatKyService,
            GSConfig gSConfig,
            IQuyetDinhTichThuService quyetDinhTichThuService,
            IKho_TaiSanToanDanFactory kho_TaiSanToanDanFactory,
            IXuLyService xuLyService,
            IThuChiService thuChiService,
            IKetQuaService ketQuaService,
            IPhuongAnXuLyService phuongAnXuLyService,
            ITaiSanTdXuLyService taiSanTdXuLyService

            )
        {
            this._khoDanhMuc = kho_DanhMuc;
            this._khoTaiSanFactory = kho_TaiSanFactory;
            this._gSAPIService = gSAPIService;
            this._taiSanService = taiSanService;
            this._commonFactory = commonFactory;
            this._hoatDongService = hoatDongService;
            this._taiSanNhatKyService = taiSanNhatKyService;
            this._gSConfig = gSConfig;
            this._quyetDinhTichThuService = quyetDinhTichThuService;
            this._kho_TaiSanToanDanFactory = kho_TaiSanToanDanFactory;
            this._xuLyService = xuLyService;
            this._thuChiService = thuChiService;
            this._ketQuaService = ketQuaService;
            this._phuongAnXuLyService = phuongAnXuLyService;
            this._taiSanTdXuLyService = taiSanTdXuLyService;
            this._taiSanService = taiSanService;
            this._taiSanTdService = taiSanTdService;
        }
        #region Tài sản xác lập
        /// <summary>
        /// đồng bộ quyết định tịch thu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<IActionResult> UpdateQuyetDinhTichThu([FromBody] SearchTaiSanModel model)
        {
            var listQDTT = _quyetDinhTichThuService.GetAllQuyetDinhTichThusChuaDongBo();
            var DuLieuDongBo = _kho_TaiSanToanDanFactory.PrepareDuLieuDongBoQuyetDinhTichThu(listQDTT?.ToList());
            
            string action = CommonTaiSan.RequestTaiSan + CommonTSXacLap.QuyenDinh + CommonAction.DongBo;
            ResponseApi apiRespon = new ResponseApi();
            StringContent content = new StringContent(JsonConvert.SerializeObject(DuLieuDongBo, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }), Encoding.UTF8, "application/json");
            apiRespon = await _gSAPIService.PostObjectGSApiWithStringContent<ResponseApi>(action, content, _commonFactory.GetTokenKhoCSDLQG());
            foreach (var item in listQDTT)
            {
                //Insert Hoạt đông
                //DbCho: Đang chờ đồng bộ
                InsertLogModel insertLogModel = new InsertLogModel()
                {
                    Data = content.ReadAsStringAsync().Result,
                    ResponseApi = apiRespon,
                    LoaiDuLieu = "QuyetDinhTichThu"
                };
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Đồng bộ quyết định tịch thu", item.ID, "QuyetDinhTichThu", insertLogModel);
                //lấy db_id từ phần mềm kho
                var result = await _gSAPIService.GetObjectGSApi<Kho_MaDongBo>(CommonTaiSan.RequestTaiSan + CommonTSXacLap.QuyenDinh + CommonAction.LayThongTinDongBo + "?" + CommonParam.SyncId + "=" + item.ID, null, _commonFactory.GetTokenKhoCSDLQG());
                var qddb = result?.decision;
                if (qddb != null)
                {
                    if (qddb.syncId == ((int)item.ID).ToString())
                    {
                        //update quyết định
                        var qd = _quyetDinhTichThuService.GetQuyetDinhTichThuById(item.ID);
                        if (qd != null)
                        {
                            qd.DB_ID = ((int)(qddb.id??0)).ToString();
                            _quyetDinhTichThuService.UpdateQuyetDinhTichThu(qd);
                            //Insert hoạt động
                            _hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ quyết định tịch thu, Update mã quyết định kho", qd, "QuyetDinhTichThu");
                            // cập nhật trạng thái quyết định
                            TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetTaiSanNhatKyByQuyetDinhTichThu(item.ID);
                            if (taiSanNhatKy != null)
                            {
                                taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.DA_DONG_BO;
                                _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                            }
                           
                        }
                    }
                }
                //thành công
                if (apiRespon.Status == true)
                {
                    
                    
                }
                else
                {
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThatBai, "Đồng bộ quyết định tịch thu", item.ID, "QuyetDinhTichThu", insertLogModel);
                    // cập nhật trạng thái quyết định
                    TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetTaiSanNhatKyByQuyetDinhTichThu(item.ID);
                    taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.DONG_BO_THAT_BAI;
                    _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                }

            }

            return Ok(apiRespon);
        }

        [HttpPost]
        public virtual async Task<IActionResult> UpdateDBIdQuyetDinhTichThu([FromBody] SearchTaiSanModel model)
        {
            if (model.ListQuyetDinhTichThu != null)
            {
                foreach (var item in model.ListQuyetDinhTichThu)
                {

                    //lấy db_id từ phần mềm kho
                    var qddb = await _gSAPIService.GetObjectGSApi<Kho_MaDongBo>(CommonTaiSan.RequestTaiSan + CommonTSXacLap.QuyenDinh + CommonAction.LayThongTinDongBo + "?" + CommonAction.DongBo + "=" + item.ID, null, _commonFactory.GetTokenKhoCSDLQG());
                    if (qddb != null)
                    {
                        if (qddb?.syncId == item.ID)
                        {
                            //update quyết định
                            var qd = _quyetDinhTichThuService.GetQuyetDinhTichThuById(item.ID);
                            if (qd != null)
                            {
                                qd.DB_ID = qddb.id;
                                _quyetDinhTichThuService.UpdateQuyetDinhTichThu(qd);
                                //Insert hoạt động
                                _hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ quyết định tịch thu, Update mã quyết định kho", qd, "QuyetDinhTichThu");
                                // cập nhật trạng thái quyết định
                                TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetTaiSanNhatKyByQuyetDinhTichThu(item.ID);
                                taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.DA_DONG_BO;
                                _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                            }

                        }

                    }
                }
            }
        

            return Ok();
        }
        /// <summary>
        /// đồng bộ tài sản toàn dân
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task<IActionResult> UpdateTaiSanTd( )
        {
            var model = _taiSanTdService.GetAllTaiSanTdsChuaDongBo();
            var DuLieuDongBo = _kho_TaiSanToanDanFactory.PrepareDuLieuDongBoTaiSanTd(model?.ToList());
            string action = CommonTaiSan.RequestTaiSan + CommonTSXacLap.TaiSanTd + CommonAction.DongBo;
            ResponseApi apiRespon = new ResponseApi();
            StringContent content = new StringContent(JsonConvert.SerializeObject(DuLieuDongBo, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }), Encoding.UTF8, "application/json");
            apiRespon = await _gSAPIService.PostObjectGSApiWithStringContent<ResponseApi>(action, content, _commonFactory.GetTokenKhoCSDLQG());
            if (model != null)
            {
                foreach (var item in model)
                {
                    //Insert Hoạt đông
                    //DbCho: Đang chờ đồng bộ
                    InsertLogModel insertLogModel = new InsertLogModel()
                    {
                        Data = content.ReadAsStringAsync().Result,
                        ResponseApi = apiRespon,
                        LoaiDuLieu = "TaiSanTd"
                    };
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Đồng bộ tài sản toàn dân", item.ID, "TaiSanTd", insertLogModel);
                    StringContent syncId = new StringContent(JsonConvert.SerializeObject(new object [] { new { syncId = item.ID} }, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }), Encoding.UTF8, "application/json");
                    //lấy db_id từ phần mềm kho
                    var qddb = await _gSAPIService.PostListGSApi<Kho_MaDongBo>(CommonTaiSan.RequestTaiSan + CommonTSXacLap.TaiSanTd + CommonAction.LayThongTinTSTD, new object[] { new { syncId = (int)item.ID } }, _commonFactory.GetTokenKhoCSDLQG());
                    if (qddb != null && qddb[0]?.idList != null && qddb[0].idList.Count() >0)
                    {
                        if (qddb[0].syncId == item.ID)
                        {
                            //update tài sản td
                            var ts = _taiSanTdService.GetTaiSanTdById(item.ID);
                            if (ts != null)
                            {
                                ts.DB_ID = qddb[0].idList[0].ToString();
                                _taiSanTdService.UpdateTaiSanTd(ts);
                                //Insert hoạt động
                                _hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ tài sản toàn dân, Update mã tài sản toàn dân", ts, "TaiSanTd");
                                // cập nhật trạng thái quyết định
                                TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanTdId(item.ID);
                                if (taiSanNhatKy != null)
                                {
                                    taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.DA_DONG_BO;
                                    _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                                }
                              
                            }
                        }
                    }
                    
                    //thành công
                    if (apiRespon != null && apiRespon.Status == true)
                    {
                        
                    }
                    else
                    {
                        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThatBai, "Đồng bộ tài sản toàn dân", item.ID, "TaiSanTd", insertLogModel);
                        // cập nhật trạng thái quyết định
                        TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanTdId(item.ID);
                        taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.DONG_BO_THAT_BAI;
                        _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                    }

                }
            }

            return Ok(apiRespon);
        }
        /// <summary>
        /// đồng bộ phương án xử lý
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task<IActionResult> UpdateXuLy()
        {
                var model = _xuLyService.GetAllXuLyChuaDongBo().ToList();
            var DuLieuDongBo = _kho_TaiSanToanDanFactory.PrepareDuLieuDongBoPhuongAnXuLy(model);
            string action = CommonTaiSan.RequestTaiSan + CommonTSXacLap.PhuongAnXuLy + CommonAction.DongBo;
            
            StringContent content = new StringContent(JsonConvert.SerializeObject(DuLieuDongBo, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }), Encoding.UTF8, "application/json");
            string dataJson = content.ReadAsStringAsync().Result;
            var apiRespon = await _gSAPIService.PostObjectGSApiWithStringContent<ResponseApi>(action, content, _commonFactory.GetTokenKhoCSDLQG());
            if (model != null)
            {
                foreach (var item in model)
                {
                    //lấy db_id từ phần mềm kho
                    var qddb = await _gSAPIService.PostListGSApi<Kho_MaDongBo>(CommonTaiSan.RequestTaiSan + CommonTSXacLap.PhuongAnXuLy + CommonAction.LayThongTinXuLy, new object[] { new { syncId = (int)item.ID } }, _commonFactory.GetTokenKhoCSDLQG());
                    if (qddb != null && qddb[0]?.idList != null && qddb[0].idList.Count() > 0)
                    {
                        if (qddb[0].syncId == item.ID)
                        {
                            //update tài sản td
                            var xl = _xuLyService.GetXuLyById(item.ID);
                            if (xl != null)
                            {
                                xl.DB_ID = qddb[0].idList[0].ToString();
                                _xuLyService.UpdateXuLy(xl);
                                //Insert hoạt động
                                _hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ tài sản toàn dân, Update xử lý id", xl, "TaiSanTd");
                            }
                        }
                    }
                }
            }
            return Ok(apiRespon);
        }
        /// <summary>
        /// đồng bộ tài sản xử lý
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task<IActionResult> UpdateTaiSanXuLy()
        {
            var model = _taiSanTdXuLyService.GetAllTaiSanTdXuLyChuaDongBo().ToList();
            var DuLieuDongBo = _kho_TaiSanToanDanFactory.PrepareDuLieuDongBoTaiSanPhuongAnXuLy(model);
            string action = CommonTaiSan.RequestTaiSan +CommonTSXacLap.TaiSanXuLy + CommonAction.DongBo;
            ResponseApi apiRespon = new ResponseApi();
            StringContent content = new StringContent(JsonConvert.SerializeObject(DuLieuDongBo, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }), Encoding.UTF8, "application/json");
            string dataJson = content.ReadAsStringAsync().Result;
            apiRespon = await _gSAPIService.PostObjectGSApiWithStringContent<ResponseApi>(action, content, _commonFactory.GetTokenKhoCSDLQG());
            if (model != null)
            {
                foreach (var item in model)
                {
                    //lấy db_id từ phần mềm kho
                    var qddb = await _gSAPIService.PostListGSApi<Kho_MaDongBo>(CommonTaiSan.RequestTaiSan + CommonTSXacLap.TaiSanXuLy + CommonAction.LayThongTinTaiSanXuLy, new object[] { new { syncId = (int)item.ID } }, _commonFactory.GetTokenKhoCSDLQG());
                    if (qddb != null && qddb[0]?.idList != null && qddb[0].idList.Count() > 0)
                    {
                        if (qddb[0].syncId == item.ID)
                        {
                            //update tài sản td
                            var xl = _taiSanTdXuLyService.GetTaiSanTdXuLyById(item.ID);
                            if (xl != null)
                            {
                                xl.DB_ID = qddb[0].idList[0].ToString();
                                _taiSanTdXuLyService.UpdateTaiSanTdXuLy(xl);
                                //Insert hoạt động
                                _hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ tài sản toàn dân, Update xử lý id", xl, "TaiSanTd");
                            }
                        }
                    }
                }
            }
            return Ok(apiRespon);
        }
        /// <summary>
        /// đồng bộ kết quả xử lý
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task<IActionResult> UpdateKetQua()
        {
            var model = _ketQuaService.GetAllKetQuaChuaDongBo().ToList();
            var DuLieuDongBo = _kho_TaiSanToanDanFactory.PrepareDuLieuDongBoKetQuaXuLy(model);
            string action = CommonTaiSan.RequestTaiSan + CommonTSXacLap.KetQuaXuLy + CommonAction.DongBo;
            ResponseApi apiRespon = new ResponseApi();
            StringContent content = new StringContent(JsonConvert.SerializeObject(DuLieuDongBo, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }), Encoding.UTF8, "application/json");
            string dataJson = content.ReadAsStringAsync().Result;

            apiRespon = await _gSAPIService.PostObjectGSApiWithStringContent<ResponseApi>(action, content, _commonFactory.GetTokenKhoCSDLQG());
            if (model != null)
            {
                foreach (var item in model)
                {
                    //lấy db_id từ phần mềm kho
                    var qddb = await _gSAPIService.PostListGSApi<Kho_MaDongBo>(CommonTaiSan.RequestTaiSan + CommonTSXacLap.KetQuaXuLy + CommonAction.LayThongTinKetQua, new object[] { new { syncId = (int)item.ID } }, _commonFactory.GetTokenKhoCSDLQG());
                    if (qddb != null && qddb[0]?.idList != null && qddb[0].idList.Count() > 0)
                    {
                        if (qddb[0].syncId == item.ID)
                        {
                            //update tài sản td
                            var xl = _ketQuaService.GetKetQuaById(item.ID);
                            if (xl != null)
                            {
                                xl.DB_ID = qddb[0].idList[0].ToString();
                                _ketQuaService.UpdateKetQua(xl);
                                //Insert hoạt động
                                _hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ tài sản toàn dân, Update xử lý id", xl, "TaiSanTd");
                            }
                        }
                    }
                }
            }
            return Ok(apiRespon);
        }
        /// <summary>
        /// đồng bộ thu chi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task<IActionResult> UpdateThuChi()
        {
            var model = _thuChiService.GetAllThuChiChuaDongBo().ToList();
            var DuLieuDongBo = _kho_TaiSanToanDanFactory.PrepareDuLieuDongBoSoSachThuChi(model);
            string action = CommonTaiSan.RequestTaiSan + CommonTSXacLap.SoSachThuChi + CommonAction.DongBo;
            ResponseApi apiRespon = new ResponseApi();
            StringContent content = new StringContent(JsonConvert.SerializeObject(DuLieuDongBo, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }), Encoding.UTF8, "application/json");
            string dataJson = content.ReadAsStringAsync().Result;
            apiRespon = await _gSAPIService.PostObjectGSApiWithStringContent<ResponseApi>(action, content, _commonFactory.GetTokenKhoCSDLQG());
            if (model != null)
            {
                foreach (var item in model)
                {
                    //lấy db_id từ phần mềm kho
                    var qddb = await _gSAPIService.PostListGSApi<Kho_MaDongBo>(CommonTaiSan.RequestTaiSan + CommonTSXacLap.SoSachThuChi + CommonAction.LayThongTinThuChi, new object[] { new { syncId = (int)item.ID } }, _commonFactory.GetTokenKhoCSDLQG());
                    if (qddb != null && qddb[0]?.idList != null && qddb[0].idList.Count() > 0)
                    {
                        if (qddb[0].syncId == item.ID)
                        {
                            //update tài sản td
                            var xl = _thuChiService.GetThuChiById(item.ID);
                            if (xl != null)
                            {
                                xl.DB_ID = qddb[0].idList[0].ToString();
                                _thuChiService.UpdateThuChi(xl);
                                //Insert hoạt động
                                _hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ tài sản toàn dân, Update xử lý id", xl, "TaiSanTd");
                            }
                        }
                    }
                }
            }
            return Ok(apiRespon);
        }
        #endregion
    }
}