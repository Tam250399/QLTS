using GS.Core;
using GS.Core.Configuration;
using GS.Core.Data;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.DM;
using GS.Core.Domain.HeThong;
using GS.Core.Infrastructure;
using GS.Services;
using GS.Services.Common;
using GS.Services.DanhMuc;
using GS.Services.DM;
using GS.Services.HeThong;
using GS.Services.Logging;
using GS.WebApi.Common;
using GS.WebApi.Factories.Common;
using GS.WebApi.Factories.Common.ConsumingApi;
using GS.WebApi.Infrastructure.Mapper.Extensions;
using GS.WebApi.Models;
using GS.WebApi.Models.ConsumingApi;
using GS.WebApi.Models.ConsumingApi.DanhMucApi;
using GS.WebApi.Models.DanhMuc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace GS.WebApi.Controllers
{
    public class ConsumingDanhMucController : BaseAdminController
    {
        private readonly IKho_DanhMucFactory _khoDanhMuc;
        private readonly IQuocGiaService _quocGiaService;
        private readonly IHoatDongService _hoatDongService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly ILoaiDonViService _loaiDonViService;
        private readonly INguonGocTaiSanService _nguonGocTaiSanService;
        private readonly IDonViService _donViService;
        private readonly IMucDichSuDungService _mucDichSuDungService;
        private readonly IHienTrangService _hienTrangService;
        private readonly ILyDoBienDongService _lyDoBienDongService;
        private readonly INguoiDungDonViService _nguoiDungDonViService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly ICheDoHaoMonService _cheDoHaoMonService;
        private readonly IGSAPIService _gsAPIService;
        private readonly IDiaBanService _diaBanService;
        private readonly IGSFileProvider _fileProvider;
        private readonly ICommonFactory _commonFactory;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly IDuAnService _duAnService;
        private readonly ILoaiLyDoBienDongService _loaiLyDoBienDongService;
        private readonly INhanXeService _nhanXeService;
        private readonly IDongXeService _dongXeService;
        private readonly IChucDanhService _chucDanhService;
        private readonly IHinhThucMuaSamService _hinhThucMuaSamService;
        private readonly ILogger _logger;
        private readonly GSConfig _config;

        public ConsumingDanhMucController(
            IGSAPIService gsAPIService,
            IKho_DanhMucFactory khoDanhMuc,
            IQuocGiaService quocGiaService,
            IHoatDongService hoatDongService,
            ILoaiTaiSanService loaiTaiSanService,
            ILoaiDonViService loaiDonViService,
            INguonGocTaiSanService nguonGocTaiSanService,
            IDonViService donViService,
            IMucDichSuDungService mucDichSuDungService,
            IHienTrangService hienTrangService,
            ILyDoBienDongService lyDoBienDongService,
            INguoiDungService nguoiDungService,
            INguoiDungDonViService nguoiDungDonViService,
            ICheDoHaoMonService cheDoHaoMonService,
            IDiaBanService diaBanService,
            IGSFileProvider gSFileProvider,
            ICommonFactory commonFactory,
            IDonViBoPhanService donViBoPhanService,
            IDuAnService duAnService,
            ILoaiLyDoBienDongService loaiLyDoBienDongService,
            INhanXeService nhanXeService,
            IDongXeService dongXeService,
            IChucDanhService chucDanhService,
            IHinhThucMuaSamService hinhThucMuaSamService,
            ILogger logger,
            GSConfig config

            )
        {
            this._gsAPIService = gsAPIService;
            this._khoDanhMuc = khoDanhMuc;
            this._quocGiaService = quocGiaService;
            this._hoatDongService = hoatDongService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._loaiDonViService = loaiDonViService;
            this._nguonGocTaiSanService = nguonGocTaiSanService;
            this._donViService = donViService;
            this._mucDichSuDungService = mucDichSuDungService;
            this._hienTrangService = hienTrangService;
            this._lyDoBienDongService = lyDoBienDongService;
            this._nguoiDungDonViService = nguoiDungDonViService;
            this._nguoiDungService = nguoiDungService;
            this._cheDoHaoMonService = cheDoHaoMonService;
            this._diaBanService = diaBanService;
            this._fileProvider = gSFileProvider;
            this._commonFactory = commonFactory;
            this._donViBoPhanService = donViBoPhanService;
            this._duAnService = duAnService;
            this._loaiLyDoBienDongService = loaiLyDoBienDongService;
            this._nhanXeService = nhanXeService;
            this._dongXeService = dongXeService;
            this._chucDanhService = chucDanhService;
            this._hinhThucMuaSamService = hinhThucMuaSamService;
            this._logger = logger;
            _config = config;
        }
        #region Quốc gia

        public async Task<IActionResult> TestGetToken()
        {
            string token = _commonFactory.GetTokenKhoCSDLQG();
            return Ok(token);
        }
        [HttpGet]
        public async Task<IActionResult> TestGetAll()
        {
            //if(ListQuocGiaModel == null)
            //     return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            // List<Kho_QuocGia> kho_QuocGias = ListQuocGiaModel.Select(m => new Kho_QuocGia()
            // {
            //     actionType = m.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi,
            //     name = m.TEN,
            //     code = m.ID.ToString(),
            //     description = m.MO_TA,
            //     syncId = m.ID.ToString(),
            // }).ToList();
            DetaiModel detaiModel = new DetaiModel()
            {
                syncId = "1"
            };
            string action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.QuocGia + CommonAction.ChiTiet;
            var response = await _gsAPIService.GetObjectGSApi<Kho_QuocGia>(action, detaiModel, _commonFactory.GetTokenKhoCSDLQG());
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Get quốc gia đã đồng bộ", 0, "QuocGia", response);

            return Ok(response.toStringJson());
        }
        [HttpGet]
        public async Task<IActionResult> GetAllQuocGias()
        {
            //List<Kho_QuocGia> ListQuocGia_Kho = new List<Kho_QuocGia>();
            var ListQuocGia_Kho = await _gsAPIService.GetListGSApi<Kho_QuocGia>(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.QuocGia + "all");
            //using (var httpClient = new HttpClient())
            //{
            //    using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.QuocGia + "all"))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        ListQuocGia_Kho = JsonConvert.DeserializeObject<List<Kho_QuocGia>>(apiResponse);
            //    }
            //}
            var ListQuocGiaModel = _khoDanhMuc.PrepareQuocGiaModel(ListQuocGia_Kho);
            return Ok(ListQuocGiaModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateQuocGia([FromBody] List<QuocGiaModel> ListQuocGiaModel)
        {
            if (ListQuocGiaModel == null)
                return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            List<Kho_QuocGia> kho_QuocGias = new List<Kho_QuocGia>();

            foreach (var item in ListQuocGiaModel)
            {
                Kho_QuocGia kho_QuocGia = new Kho_QuocGia();
                QuocGia quocGia = _quocGiaService.GetQuocGiaById(item.ID);
                if (quocGia != null)
                {
                    item.DB_ID = quocGia.DB_ID;
                }
                kho_QuocGia.actionType = item.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_QuocGia.name = item.TEN;
                kho_QuocGia.code = item.ID.ToString();
                kho_QuocGia.description = item.MO_TA;
                kho_QuocGia.syncId = item.ID.ToString();
                kho_QuocGia.id = item.DB_ID;
                kho_QuocGias.Add(kho_QuocGia);
            }
            string apiResponse = null;
            string action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.QuocGia + CommonAction.DongBo;
            var response = await PostDanhMuc(CommonDanhMuc.QuocGia, kho_QuocGias, "QuocGia");
            if (response != null && response.Status)
            {
                foreach (var item in ListQuocGiaModel)
                {
                    Kho_QuocGia kho_QuocGia = await GetIdDongBo<Kho_QuocGia>(CommonDanhMuc.QuocGia, item.ID.ToString());
                    if (kho_QuocGia != null)
                    {
                        QuocGia quocGia = _quocGiaService.GetQuocGiaById(item.ID);
                        quocGia.DB_ID = (int)kho_QuocGia.id;
                        _quocGiaService.UpdateQuocGia(quocGia);
                        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThanhCong, "Đồng bộ thành công", quocGia.ID, "QuocGia", item);
                    }
                }
            }
            else
            {
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThatBai, "Chưa gửi được dữ liệu đồng bộ", 0, "QuocGia", response);
                return Ok(MessageReturn.CreateErrorMessage("Tiếp nhận thất bại"));
            }
            // kiểm tra dữ liệu đã được insert hay chưa

            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteQuocGia([FromBody] QuocGiaModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await DeleteDanhMuc(CommonDanhMuc.QuocGia, (long)model.DB_ID, model);
                return Ok(result);
            }
            return Ok("error");

        }
        [HttpPost]
        public async Task<IActionResult> GetDetailQuocGia([FromBody] List<QuocGiaModel> ListQuocGiaModel)
        {
            if (ListQuocGiaModel == null)
                return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            List<string> ListResponse = new List<string>();
            foreach (var item in ListQuocGiaModel)
            {
                string apiResponse;
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.QuocGia + "detail?code=" + item.ID))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            apiResponse = await response.Content.ReadAsStringAsync();
                            ListResponse.Add(apiResponse);
                        }

                    }
                }
                // _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Đang chờ đồng bộ", item.ID, "QuocGia", item);
            }
            foreach (var item in ListResponse)
            {
                var Kho_QuocGiaResponse = JsonConvert.DeserializeObject<Kho_QuocGia>(item);

                QuocGia quocGia = _quocGiaService.GetQuocGiaById(int.Parse(Kho_QuocGiaResponse.code));
                if (quocGia != null)
                {
                    quocGia.DB_ID = (int)Kho_QuocGiaResponse.id;
                    _quocGiaService.UpdateQuocGia(quocGia);
                }
                //  _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThanhCong, "Đồng bộ thành công", quocGia.ID, "QuocGia", quocGia.ToModel<QuocGiaModel>());

            }
            // return Ok(_khoDanhMuc.PrepareMessageReturrn(apiResponse));
            return Ok(MessageReturn.CreateSuccessMessage("Success done"));
        }
        [HttpGet]
        public async Task<IActionResult> TestUpdateQuocGia()
        {
            //if (ListLoaiDonViModel == null)
            // return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            // getall Loại đơn vị
            string apiResponse = null;
            var ListQuocGia = _quocGiaService.GetAllQuocGias();
            // var _ListDonViModel = ListLoaiDonVi.Select(m => m.ToModel<LoaiDonViModel>());
            var ListQuocGiaModel = ListQuocGia.Select(m => new QuocGiaModel()
            {
                MA = m.MA,
                MO_TA = m.MO_TA,
                DB_ID = m.DB_ID,
                TEN = m.TEN,
                ID = (long)m.ID,
            }).ToList();
            foreach (var item in ListQuocGiaModel)
            {
                using (var httpClient = new HttpClient())
                {
                    string token = _commonFactory.GetTokenKhoCSDLQG();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.QuocGia + "detail?syncId=" + item.ID.ToString()))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            apiResponse = await response.Content.ReadAsStringAsync();
                            var kho_QuocGia = JsonConvert.DeserializeObject<Kho_QuocGia>(apiResponse);
                            QuocGia quocGia = _quocGiaService.GetQuocGiaById(item.ID);
                            quocGia.DB_ID = (int)kho_QuocGia.id;
                            item.DB_ID = (int)kho_QuocGia.id;
                            _quocGiaService.UpdateQuocGia(quocGia);
                            //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThanhCong, "Đồng bộ thành công", item.ID, "LoaiDonVi", item);
                        }

                    }
                }

            }
            List<Kho_QuocGia> kho_QuocGias = new List<Kho_QuocGia>();
            foreach (var m in ListQuocGiaModel)
            {
                Kho_QuocGia kho_QuocGia = new Kho_QuocGia();
                kho_QuocGia.actionType = m.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_QuocGia.name = m.TEN;
                kho_QuocGia.code = m.MA != null ? m.MA : "QG" + m.ID.ToString();
                kho_QuocGia.syncId = m.ID.ToString();
                kho_QuocGias.Add(kho_QuocGia);
            }
            var dataJson = kho_QuocGias.toStringJson();
            // kho_LoaiDonVis = kho_LoaiDonVis.Where(m => m.id > 0).ToList();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(kho_QuocGias), Encoding.UTF8, "application/json");
                string token = _commonFactory.GetTokenKhoCSDLQG();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.QuocGia + "sync", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    var responseObj = JsonConvert.DeserializeObject<ResponseApi>(apiResponse);
                    if (responseObj.Status)
                    {
                        foreach (var item in ListQuocGiaModel)
                        {
                            item.TrangThaiDongBo = responseObj.Data;
                        }
                        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Đang chờ đồng bộ", 0, "QuocGia", ListQuocGiaModel);
                    }
                    else
                    {
                        return Ok(MessageReturn.CreateErrorMessage("Tiếp nhận thất bại"));
                    }
                }
            }
            foreach (var item in ListQuocGiaModel)
            {
                bool IsUpdate = false;
                for (int i = 0; i < 3; i++)
                {
                    await Task.Delay(2000);
                    IsUpdate = false;
                    using (var httpClient = new HttpClient())
                    {
                        string token = _commonFactory.GetTokenKhoCSDLQG();
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.QuocGia + "detail?syncId=" + item.ID.ToString()))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                apiResponse = await response.Content.ReadAsStringAsync();
                                var kho_QuocGia = JsonConvert.DeserializeObject<Kho_QuocGia>(apiResponse);
                                QuocGia quocGia = _quocGiaService.GetQuocGiaById(item.ID);
                                quocGia.DB_ID = (int)kho_QuocGia.id;
                                _quocGiaService.UpdateQuocGia(quocGia);
                                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThanhCong, "Đồng bộ thành công", item.ID, "QuocGia", item);
                                IsUpdate = true;
                                break;
                            }
                            else
                            {
                                IsUpdate = false;
                            }
                        }
                    }
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Đang chờ đồng bộ", item.ID, "QuocGia", item);
                }
                if (!IsUpdate)
                {
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThatBai, "Đồng bộ thất bại", item.ID, "QuocGia", item);
                }
                else
                {
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThanhCong, "Đồng bộ thành công", item.ID, "QuocGia", item);
                }
            }
            return Ok(_khoDanhMuc.PrepareMessageReturrn(apiResponse));
        }
        #endregion
        #region Lý do biến đông
        [HttpGet]
        public async Task<IActionResult> GetAllLyDoBienDongs()
        {
            List<Kho_LyDoBienDong> ListLyDoBienDong_Kho = new List<Kho_LyDoBienDong>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + "increasementCauses/all"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ListLyDoBienDong_Kho = JsonConvert.DeserializeObject<List<Kho_LyDoBienDong>>(apiResponse);
                }
            }
            var ListLyDoBienDongModel = _khoDanhMuc.PrepareLyDoBienDongModel(ListLyDoBienDong_Kho);
            return Ok(ListLyDoBienDongModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateLyDoBienDong([FromBody] List<LyDoBienDongModel> ListLyDoBienDongModel)
        {
            if (ListLyDoBienDongModel == null)
                return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            List<Kho_LyDoBienDong> kho_LyDoBienDongs = new List<Kho_LyDoBienDong>();
            foreach (var item in ListLyDoBienDongModel)
            {
                LyDoBienDong lyDoBienDong = _lyDoBienDongService.GetLyDoBienDongById(item.ID);
                item.DB_ID = item.DB_ID;
                Kho_LyDoBienDong kho_LyDoBienDong = new Kho_LyDoBienDong();
                kho_LyDoBienDong.actionType = item.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_LyDoBienDong.name = item.TEN;
                kho_LyDoBienDong.code = item.MA;
                kho_LyDoBienDong.syncId = item.ID.ToString();
                kho_LyDoBienDong.id = item.DB_ID;
                kho_LyDoBienDong.causeTypeId = (long?)item.LOAI_LY_DO_ID;
                //if (item.LOAI_LY_DO_BIEN_DONG_ID.HasValue)
                //{
                //    LoaiLyDoBienDong loaiLyDoBienDong = _loaiLyDoBienDongService.GetLoaiLyDoBienDongById(item.LOAI_LY_DO_BIEN_DONG_ID.Value);
                //    if (loaiLyDoBienDong == null || (loaiLyDoBienDong != null && loaiLyDoBienDong.DB_ID == null))
                //        continue;
                //    kho_LyDoBienDong.causeTypeId = (long?)loaiLyDoBienDong.DB_ID;
                //}
                //else continue;

                kho_LyDoBienDong.displayOrder = (long?)item.TRUONG_SAP_XEP;
                if (!string.IsNullOrEmpty(item.LOAI_HINH_TAI_SAN_AP_DUNG_ID))
                {
                    kho_LyDoBienDong.assetTypeIds = JsonConvert.DeserializeObject<List<decimal>>(item.LOAI_HINH_TAI_SAN_AP_DUNG_ID)
                                                        .Select(m => (long)CommonHelper.toLoaiHinhTaiSanKho(m))
                                                        .ToList();
                }
                else
                {
                    kho_LyDoBienDong.assetTypeIds = new enumLOAI_HINH_TAI_SAN_KHO().ToSelectList().Select(c => (long)Convert.ToInt32(c.Value)).Distinct().ToList();
                    //kho_LyDoBienDong.assetTypeIds.Add((long)enumIdNhomTaiSanKho.CoQuanToChuc);
                }
                kho_LyDoBienDongs.Add(kho_LyDoBienDong);
            }
            var dataJson = kho_LyDoBienDongs.toStringJson();
            var result = await PostDanhMuc(CommonDanhMuc.LyDoBienDong, kho_LyDoBienDongs, "LyDoBienDong");
            if (result != null && result.Status)
            {
                foreach (var item in ListLyDoBienDongModel)
                {
                    Kho_LyDoBienDong kho_LyDoBienDong = await GetIdDongBo<Kho_LyDoBienDong>(CommonDanhMuc.LyDoBienDong, item.ID.ToString());
                    if (kho_LyDoBienDong != null)
                    {
                        LyDoBienDong lyDoBienDong = _lyDoBienDongService.GetLyDoBienDongById(item.ID);
                        lyDoBienDong.DB_ID = kho_LyDoBienDong.id;
                        _lyDoBienDongService.UpdateLyDoBienDong(lyDoBienDong);
                        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThanhCong, "Đồng bộ thành công", item.ID, "LyDoBienDong", item);
                    }
                    else
                    {
                        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThatBai, "Đồng bộ thất bại", item.ID, "LyDoBienDong", item);
                    }

                }
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> TestUpdateLyDoBienDong()
        {

            var ListLyDoBienDong = _lyDoBienDongService.GetAllLyDoBienDongs().Where(m => m.DB_ID == null);
            List<LyDoBienDongModel> ListLyDoBienDongModel = new List<LyDoBienDongModel>();
            foreach (var item in ListLyDoBienDong)
            {
                LyDoBienDongModel bienDongModel = new LyDoBienDongModel();
                bienDongModel.ID = (long)item.ID;
                bienDongModel.LOAI_HINH_TAI_SAN_AP_DUNG_ID = item.LOAI_HINH_TAI_SAN_AP_DUNG_ID;
                bienDongModel.MA = item.MA;
                bienDongModel.TEN = item.TEN;
                bienDongModel.DB_ID = (int?)item.DB_ID;
                bienDongModel.TRUONG_SAP_XEP = item.TRUONG_SAP_XEP;
                bienDongModel.LOAI_LY_DO_BIEN_DONG_ID = item.LOAI_LY_DO_BIEN_DONG_ID;
                if (item.LoaiLyDoBienDong != null)
                {
                    bienDongModel.DB_LoaiLyDoBienDongId = item.LoaiLyDoBienDong.DB_ID;
                }

                ListLyDoBienDongModel.Add(bienDongModel);
            }

            string apiResponse = null;
            string Action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.LyDoBienDong + CommonAction.ChiTiet;
            List<Kho_LyDoBienDong> kho_LyDoBienDongs = new List<Kho_LyDoBienDong>();

            foreach (var item in ListLyDoBienDongModel)
            {
                Kho_LyDoBienDong kho_LyDoBienDong = new Kho_LyDoBienDong();
                kho_LyDoBienDong.id = (long?)item.DB_ID;
                kho_LyDoBienDong.actionType = kho_LyDoBienDong.id > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_LyDoBienDong.name = item.TEN;
                kho_LyDoBienDong.code = item.MA;
                kho_LyDoBienDong.syncId = item.ID.ToString();
                if (!string.IsNullOrEmpty(item.LOAI_HINH_TAI_SAN_AP_DUNG_ID))
                {
                    kho_LyDoBienDong.assetTypeIds = JsonConvert.DeserializeObject<List<decimal>>(item.LOAI_HINH_TAI_SAN_AP_DUNG_ID)
                                                        .Select(m => (long)CommonHelper.toLoaiHinhTaiSanKho(m))
                                                        .ToList();
                }
                else
                {
                    kho_LyDoBienDong.assetTypeIds = new enumLOAI_HINH_TAI_SAN_KHO().ToSelectList().Select(c => (long)Convert.ToInt32(c.Value)).ToList();
                    //kho_LyDoBienDong.assetTypeIds.Add((long)enumIdNhomTaiSanKho.CoQuanToChuc);
                }
                kho_LyDoBienDong.causeTypeId = (long?)item.LOAI_LY_DO_ID;
                kho_LyDoBienDongs.Add(kho_LyDoBienDong);
            }
            var dataJson = kho_LyDoBienDongs.toStringJson();
            AllModel allModel = new AllModel()
            {
                startDate = "01-01-2020 12:00:00",
                endDate = "12-12-2020 12:00:00"
            };

            var _result = await _gsAPIService.GetListGSApi<Kho_LyDoBienDong>(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.LyDoBienDong + CommonAction.TatCa, allModel, _commonFactory.GetTokenKhoCSDLQG());
            if (_result != null)
            {
                foreach (var item in _result)
                {
                    LyDoBienDong lyDo = _lyDoBienDongService.GetLyDoBienDongById(decimal.Parse(item.syncId));
                    lyDo.DB_ID = item.id;
                    _lyDoBienDongService.UpdateLyDoBienDong(lyDo);
                }
            }
            Action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.LyDoBienDong + CommonAction.DongBo;
            var responseObj = await _gsAPIService.PostObjectGSApi<ResponseApi>(Action, kho_LyDoBienDongs, _commonFactory.GetTokenKhoCSDLQG());
            if (responseObj != null && responseObj.Status)
            {
                InsertLogModel insertLogModel = new InsertLogModel()
                {
                    ResponseApi = responseObj,
                    Data = ListLyDoBienDong,
                    LoaiDuLieu = "Danh mục"
                };
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Đang chờ đồng bộ", 0, "LyDoBienDong", insertLogModel);
            }
            else
            {
                return Ok(MessageReturn.CreateErrorMessage("Tiếp nhận thất bại"));
            }
            #region cmt
            foreach (var item in ListLyDoBienDong)
            {
                await Task.Delay(1000);
                Action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.LyDoBienDong + CommonAction.ChiTiet;
                DetaiModel detaiModel = new DetaiModel()
                {
                    syncId = item.ID.ToString()
                };
                Kho_LyDoBienDong kho_LyDoBienDong = await _gsAPIService.GetObjectGSApi<Kho_LyDoBienDong>(Action, detaiModel, _commonFactory.GetTokenKhoCSDLQG());
                if (kho_LyDoBienDong != null)
                {
                    LyDoBienDong lyDoBienDong = _lyDoBienDongService.GetLyDoBienDongById(item.ID);
                    lyDoBienDong.DB_ID = kho_LyDoBienDong.id;
                    _lyDoBienDongService.UpdateLyDoBienDong(lyDoBienDong);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThanhCong, "Đồng bộ thành công", item.ID, "LyDoBienDong", item);
                }
                else
                {
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThatBai, "Đồng bộ thành công", item.ID, "LyDoBienDong", item);
                }

            }
            #endregion
            return Ok(_khoDanhMuc.PrepareMessageReturrn(apiResponse));
        }
        [HttpGet]
        public async Task<IActionResult> DeleteLyDoBienDong()
        {
            var ListLyDoBienDongModel = _lyDoBienDongService.GetAllLyDoBienDongs();
            var ListDelete = ListLyDoBienDongModel.Where(m => m.DB_ID != null).Select(m => new
            {
                id = m.DB_ID
            }).ToList();
            string action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.LyDoBienDong + CommonAction.Xoa;
            ResponseApi responseApi = await _gsAPIService.DeleteObjectGSApi<ResponseApi>(action, ListDelete, _commonFactory.GetTokenKhoCSDLQG());
            return Ok(responseApi);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteLyDoBienDong([FromBody] LyDoBienDongModel model)
        {
            var result = await DeleteDanhMuc(CommonDanhMuc.LyDoBienDong, (long)model.DB_ID, model);
            return Ok(result);
        }

        #endregion
        #region Loại lý do biến động
        public async Task<IActionResult> TestUpdateLoaiLyDoBienDong()
        {
            var ListLoaiLyDo = _loaiLyDoBienDongService.GetAllLoaiLyDoBienDongs();
            var ListLoaiLyDoModel = ListLoaiLyDo.Select(m => new LoaiLyDoBienDongModel()
            {
                MA = m.MA,
                TEN = m.TEN,
                DB_ID = m.DB_ID,
                PARENT_ID = m.PARENT_ID,
                ID = (long)m.ID,
            }).ToList();
            AllModel allModel = new AllModel()
            {
                startDate = "01-01-2020 12:00:00",
                endDate = "12-12-2020 12:00:00"
            };

            var _result = await _gsAPIService.GetListGSApi<Kho_LoaiLyDoBienDong>(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.LoaiLyDoBienDong + CommonAction.TatCa, allModel, _commonFactory.GetTokenKhoCSDLQG());
            if (_result != null)
            {
                foreach (var item in _result)
                {
                    LoaiLyDoBienDong loaiLyDoBienDong = _loaiLyDoBienDongService.GetLoaiLyDoBienDongById(decimal.Parse(item.syncId));
                    loaiLyDoBienDong.DB_ID = item.id;
                    _loaiLyDoBienDongService.UpdateLoaiLyDoBienDong(loaiLyDoBienDong);
                }
            }
            foreach (var item in ListLoaiLyDoModel)
            {
                Kho_LoaiLyDoBienDong kho_LoaiLyDo = await GetIdDongBo<Kho_LoaiLyDoBienDong>(CommonDanhMuc.LoaiLyDoBienDong, item.ID.ToString());
                if (kho_LoaiLyDo != null)
                {
                    LoaiLyDoBienDong loaiLyDo = _loaiLyDoBienDongService.GetLoaiLyDoBienDongById(item.ID);
                    loaiLyDo.DB_ID = kho_LoaiLyDo.id;
                    _loaiLyDoBienDongService.UpdateLoaiLyDoBienDong(loaiLyDo);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThanhCong, "Đồng bộ thành công", item.ID, "LoaiLyDoBienDong", item);
                }
                else
                {
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThatBai, "Đồng bộ thất bại", item.ID, "LoaiLyDoBienDong", item);
                }

            }
            List<Kho_LoaiLyDoBienDong> kho_LoaiLyDoBienDongs = new List<Kho_LoaiLyDoBienDong>();
            foreach (var m in ListLoaiLyDoModel)
            {
                Kho_LoaiLyDoBienDong kho_LoaiLyDo = new Kho_LoaiLyDoBienDong();
                kho_LoaiLyDo.actionType = m.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_LoaiLyDo.name = m.TEN;
                kho_LoaiLyDo.code = m.MA;
                kho_LoaiLyDo.syncId = m.ID.ToString();
                kho_LoaiLyDoBienDongs.Add(kho_LoaiLyDo);
            }
            var dataJson = kho_LoaiLyDoBienDongs.toStringJson();
            // kho_LoaiDonVis = kho_LoaiDonVis.Where(m => m.id > 0).ToList();
            string action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.LoaiLyDoBienDong + CommonAction.DongBo;
            var result = await PostDanhMuc(CommonDanhMuc.LoaiLyDoBienDong, kho_LoaiLyDoBienDongs, "LoaiLyDoBienDong");
            if (result != null && result.Status)
            {
                foreach (var item in ListLoaiLyDoModel)
                {
                    Kho_LoaiLyDoBienDong kho_LoaiLyDo = await GetIdDongBo<Kho_LoaiLyDoBienDong>(CommonDanhMuc.LoaiLyDoBienDong, item.ID.ToString());
                    if (kho_LoaiLyDo != null)
                    {
                        LoaiLyDoBienDong loaiLyDo = _loaiLyDoBienDongService.GetLoaiLyDoBienDongById(item.ID);
                        loaiLyDo.DB_ID = kho_LoaiLyDo.id;
                        _loaiLyDoBienDongService.UpdateLoaiLyDoBienDong(loaiLyDo);
                        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThanhCong, "Đồng bộ thành công", item.ID, "LoaiLyDoBienDong", item);
                    }
                    else
                    {
                        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThatBai, "Đồng bộ thất bại", item.ID, "LoaiLyDoBienDong", item);
                    }

                }
            }
            return Ok(result);
        }
        #endregion
        #region Địa bàn
        [HttpGet]
        public async Task<IActionResult> GetAllDiaBans()
        {
            List<Kho_DiaBan> ListDiaBan_Kho = new List<Kho_DiaBan>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + "region/all"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ListDiaBan_Kho = JsonConvert.DeserializeObject<List<Kho_DiaBan>>(apiResponse);
                }
            }
            var ListDiaBanModel = _khoDanhMuc.PrepareDiaBanModel(ListDiaBan_Kho);
            return Ok(ListDiaBanModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDiaBan([FromBody] List<DiaBanModel> ListDiaBanModel)
        {
            if (ListDiaBanModel == null)
                return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            List<Kho_DiaBan> kho_DiaBans = new List<Kho_DiaBan>();
            foreach (var m in ListDiaBanModel)
            {
                DiaBan diaBan = _diaBanService.GetDiaBanById(m.ID);
                m.DB_ID = diaBan.DB_ID;
                Kho_DiaBan kho_DiaBan = new Kho_DiaBan();
                kho_DiaBan.id = (long?)m.DB_ID;
                kho_DiaBan.actionType = m.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_DiaBan.name = m.TEN;
                kho_DiaBan.code = m.MA;
                kho_DiaBan.isActive = m.TRANG_THAI_ID == 1 ? true : false;
                kho_DiaBan.syncParentId = m.PARENT_ID > 0 ? m.PARENT_ID.ToString() : null;
                if (m.TREE_LEVEL == 3)
                {
                    if (m.PARENT_ID > 0)
                    {
                        DiaBan CapHuyen = _diaBanService.GetDiaBanById(m.PARENT_ID.Value);
                        if (CapHuyen == null)
                            continue;
                        kho_DiaBan.districtId = CapHuyen.DB_ID.ToString();
                        if (CapHuyen.PARENT_ID > 0)
                        {
                            DiaBan CapTinh = _diaBanService.GetDiaBanById(CapHuyen.PARENT_ID.Value);
                            if (CapTinh == null)
                                continue;
                            kho_DiaBan.provinceId = CapTinh.DB_ID.ToString();
                        }
                    }
                }
                if (m.TREE_LEVEL == 2)
                {
                    DiaBan CapTinh = _diaBanService.GetDiaBanById(m.PARENT_ID.Value);
                    if (CapTinh == null)
                        continue;
                    kho_DiaBan.provinceId = CapTinh.DB_ID.ToString();
                }
                kho_DiaBan.syncId = m.ID.ToString();
                kho_DiaBans.Add(kho_DiaBan);
            }
            ResponseApi responseApi = await PostDanhMuc(CommonDanhMuc.TinhThanh_DiaBan, kho_DiaBans, "DiaBan");
            if (responseApi.Status)
            {
                foreach (var item in ListDiaBanModel)
                {
                    Kho_DiaBan kho_DiaBan = await GetIdDongBo<Kho_DiaBan>(CommonDanhMuc.TinhThanh_DiaBan, item.ID.ToString());
                    if (kho_DiaBan != null)
                    {
                        DiaBan diaBan = _diaBanService.GetDiaBanById(item.ID);
                        diaBan.DB_ID = kho_DiaBan.id;
                        _diaBanService.UpdateDiaBan(diaBan);
                        _hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ danh mục địa bàn", diaBan.ToModel<DiaBanModel>(), "DiaBan");
                    }
                }
            }

            return Ok(responseApi);
        }
        [HttpGet]
        public async Task<IActionResult> TestUpdateDiaBan()
        {
            //if (ListLoaiDonViModel == null)
            // return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            // getall Loại đơn vị
            string apiResponse = null;
            List<DiaBan> ListDiaBan = _diaBanService.GetDiaBans(QuocGiaId: 33).Where(m => m.TREE_LEVEL == 3 && m.TREE_NODE != null).ToList();
            //  List<DiaBan> ListDiaBan = _diaBanService.GetDiaBans(QuocGiaId: 33).Where(m => m.PARENT_ID == 2457).ToList();
            // var _ListDonViModel = ListLoaiDonVi.Select(m => m.ToModel<LoaiDonViModel>());
            List<DiaBanModel> ListDiaBanModel = ListDiaBan.Select(m => m.ToModel<DiaBanModel>()).ToList();
            List<Kho_DiaBan> kho_DiaBans = new List<Kho_DiaBan>();
            //ListDiaBanModel = ListDiaBanModel.OrderBy(m => m.TREE_NODE).ToList(); ;
            foreach (var m in ListDiaBanModel)
            {
                Kho_DiaBan kho_DiaBan = new Kho_DiaBan();
                kho_DiaBan.id = (long?)m.DB_ID;
                kho_DiaBan.actionType = m.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_DiaBan.name = m.TEN;
                kho_DiaBan.code = m.MA;
                kho_DiaBan.isActive = m.TRANG_THAI_ID == 1 ? true : false;
                kho_DiaBan.syncParentId = m.PARENT_ID > 0 ? m.PARENT_ID.ToString() : null;
                if (m.TREE_LEVEL == 3)
                {
                    if (m.PARENT_ID > 0)
                    {
                        DiaBan CapHuyen = _diaBanService.GetDiaBanById(m.PARENT_ID.Value);
                        if (CapHuyen == null)
                            continue;
                        kho_DiaBan.districtId = CapHuyen.DB_ID.ToString();
                        if (CapHuyen.PARENT_ID > 0)
                        {
                            DiaBan CapTinh = _diaBanService.GetDiaBanById(CapHuyen.PARENT_ID.Value);
                            if (CapTinh == null)
                                continue;
                            kho_DiaBan.provinceId = CapTinh.DB_ID.ToString();
                        }
                    }
                }
                if (m.TREE_LEVEL == 2)
                {
                    DiaBan CapTinh = _diaBanService.GetDiaBanById(m.PARENT_ID.Value);
                    if (CapTinh == null)
                        continue;
                    kho_DiaBan.provinceId = CapTinh.DB_ID.ToString();
                }
                kho_DiaBan.syncId = m.ID.ToString();
                kho_DiaBans.Add(kho_DiaBan);
            }
            var dataJson = kho_DiaBans.toStringJson();
            var responseObj = await _gsAPIService.PostObjectGSApi<ResponseApi>(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.TinhThanh_DiaBan + CommonAction.DongBo, kho_DiaBans, _commonFactory.GetTokenKhoCSDLQG());
            if (responseObj.Status)
            {
                //await Task.Delay(5000);
                //string action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.TinhThanh_DiaBan + CommonAction.TatCa;
                //AllModel allModel = new AllModel()
                //{
                //    startDate = "01-01-2020 12:00:00",
                //    endDate = "12-12-2020 12:00:00"
                //};
                //List<Kho_DiaBan> ListKhoDiaBan = await _gsAPIService.GetObjectGSApi<List<Kho_DiaBan>>(action, allModel, _commonFactory.GetTokenKhoCSDLQG());
                //foreach (var item in kho_DiaBans)
                //{
                //    DiaBan diaBan = _diaBanService.GetDiaBanById(decimal.Parse(item.syncId));
                //    diaBan.DB_ID = item.id;
                //    ListDiaBan.Add(diaBan);
                //}
                //_diaBanService.UpdateDiaBan(ListDiaBan);
            }
            else
            {
                return Ok(MessageReturn.CreateErrorMessage("Tiếp nhận thất bại"));
            }
            return Ok(responseObj);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateIdDongBoDiaBan()
        {
            var httpClient = new HttpClient();
            string apiResponse = "";
            //HttpResponseMessage responseMessage = new HttpResponseMessage();
            //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.TinhThanh_DiaBan + CommonAction.TatCa);
            //HttpHeadAttribute httpHead = new HttpHeadAttribute();
            //HttpContent httpContent = new FormUrlEncodedContent(
            //        new[]
            //        {
            //        new KeyValuePair<string, string>("startDate", "01-01-2020 12:00:00"),
            //        new KeyValuePair<string, string>("endDate", "12-12-2020 12:00:00"),                  
            //        });
            //request.Content = httpContent;

            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _commonFactory.GetTokenKhoCSDLQG());

            //responseMessage = await httpClient.SendAsync(request);
            //apiResponse = await responseMessage.Content.ReadAsStringAsync();
            string action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.TinhThanh_DiaBan + CommonAction.TatCa;
            AllModel allModel = new AllModel()
            {
                startDate = "01-01-2020 12:00:00",
                endDate = "12-12-2020 12:00:00"
            };
            List<Kho_DiaBan> ListKhoDiaBan = await _gsAPIService.GetObjectGSApi<List<Kho_DiaBan>>(action, allModel, _commonFactory.GetTokenKhoCSDLQG());
            List<DiaBan> ListDiaBan = new List<DiaBan>();
            foreach (var item in ListKhoDiaBan)
            {
                DiaBan diaBan = _diaBanService.GetDiaBanById(decimal.Parse(item.syncId));
                diaBan.DB_ID = item.id;
                ListDiaBan.Add(diaBan);
            }
            _diaBanService.UpdateDiaBan(ListDiaBan);
            return OkSuccessMessage("Done");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDiaBan([FromBody] DiaBanModel model)
        {
            var result = await DeleteDanhMuc(CommonDanhMuc.TinhThanh_DiaBan, (long)model.DB_ID, model);
            return Ok(result);
        }
        #endregion
        #region Loai don vi
        [HttpGet]
        public async Task<IActionResult> GetAllLoaiDonVis()
        {
            List<Kho_LoaiDonVi> ListLoaiDonVi_Kho = new List<Kho_LoaiDonVi>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + "unitTypes/all"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ListLoaiDonVi_Kho = JsonConvert.DeserializeObject<List<Kho_LoaiDonVi>>(apiResponse);
                }
            }
            var ListLoaiDonViModel = _khoDanhMuc.PrepareLoaiDonViModel(ListLoaiDonVi_Kho);
            return Ok(ListLoaiDonViModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateLoaiDonVi([FromBody] List<LoaiDonViModel> ListLoaiDonViModel)
        {
            if (ListLoaiDonViModel == null)
                return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            // get
            List<Kho_LoaiDonVi> kho_LoaiDonVis = new List<Kho_LoaiDonVi>();
            foreach (var m in ListLoaiDonViModel)
            {
                LoaiDonVi loaiDonVi = _loaiDonViService.GetLoaiDonViById(m.ID);
                m.DB_ID = (int?)loaiDonVi.DB_ID;
                Kho_LoaiDonVi kho_LoaiDonVi = new Kho_LoaiDonVi();
                kho_LoaiDonVi.actionType = m.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_LoaiDonVi.name = m.TEN;
                kho_LoaiDonVi.code = m.MA != null ? m.MA : "LDV" + m.ID.ToString();
                kho_LoaiDonVi.syncId = m.ID.ToString();
                kho_LoaiDonVi.syncParentId = m.PARENT_ID > 0 ? m.PARENT_ID.ToString() : null;
                if (m.PARENT_ID > 0)
                {
                    kho_LoaiDonVi.parentId = (long?)_loaiDonViService.GetLoaiDonViById(m.PARENT_ID.Value).DB_ID;
                }
                kho_LoaiDonVi.accountingStandard = m.CHE_DO_HACH_TOAN_ID == null ? (long)enumCHE_DO_HACH_TOAN.HAO_MON : (long)m.CHE_DO_HACH_TOAN_ID;
                kho_LoaiDonVi.displayOrder = (long?)m.SAP_XEP;
                kho_LoaiDonVi.id = m.DB_ID;
                kho_LoaiDonVis.Add(kho_LoaiDonVi);
            }
            ResponseApi responseApi = await PostDanhMuc(CommonDanhMuc.LoaiDonVi, kho_LoaiDonVis, "LoaiDonVi");
            if (responseApi.Status)
            {
                foreach (var item in ListLoaiDonViModel)
                {
                    Kho_LoaiDonVi kho_LoaiDonVi = await GetIdDongBo<Kho_LoaiDonVi>(CommonDanhMuc.LoaiDonVi, item.ID.ToString());
                    if (kho_LoaiDonVi != null)
                    {
                        LoaiDonVi loaiDonVi = _loaiDonViService.GetLoaiDonViById(item.ID);
                        loaiDonVi.DB_ID = (int?)kho_LoaiDonVi.id;
                        _loaiDonViService.UpdateLoaiDonVi(loaiDonVi);
                        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThanhCong, "Đồng bộ thành công loại đơn vị", item.ID, "LoaiDonVi", loaiDonVi.ToModel<LoaiDonViModel>());
                    }
                }
            }
            return Ok(responseApi);
        }
        [HttpGet]
        public async Task<IActionResult> TestUpdateLoaiDonVi()
        {
            //if (ListLoaiDonViModel == null)
            // return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            // getall Loại đơn vị
            string apiResponse = null;
            var ListLoaiDonVi = _loaiDonViService.GetAllLoaiDonVis().Where(m => m.TREE_LEVEL == 4);
            // var _ListDonViModel = ListLoaiDonVi.Select(m => m.ToModel<LoaiDonViModel>());
            var ListLoaiDonViModel = ListLoaiDonVi.Select(m => new LoaiDonViModel()
            {
                MA = m.MA,
                CHE_DO_HACH_TOAN_ID = m.CHE_DO_HOACH_TOAN_ID,
                DB_ID = (int?)m.DB_ID,
                PARENT_ID = m.PARENT_ID,
                TEN = m.TEN,
                ID = (long)m.ID,
                SAP_XEP = m.SAP_XEP
            }).ToList();
            List<Kho_LoaiDonVi> kho_LoaiDonVis = new List<Kho_LoaiDonVi>();
            foreach (var m in ListLoaiDonViModel)
            {
                Kho_LoaiDonVi kho_LoaiDonVi = new Kho_LoaiDonVi();
                kho_LoaiDonVi.actionType = m.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_LoaiDonVi.name = m.TEN;
                kho_LoaiDonVi.code = m.MA != null ? m.MA : "LDV" + m.ID.ToString();
                kho_LoaiDonVi.syncId = m.ID.ToString();
                kho_LoaiDonVi.syncParentId = m.PARENT_ID > 0 ? m.PARENT_ID.ToString() : null;
                if (m.PARENT_ID > 0)
                {
                    kho_LoaiDonVi.parentId = (long?)_loaiDonViService.GetLoaiDonViById(m.PARENT_ID.Value).DB_ID;
                }
                kho_LoaiDonVi.displayOrder = (long?)m.SAP_XEP;
                kho_LoaiDonVi.accountingStandard = m.CHE_DO_HACH_TOAN_ID == null ? (long)enumCHE_DO_HACH_TOAN.HAO_MON : (long)m.CHE_DO_HACH_TOAN_ID;
                kho_LoaiDonVi.id = m.DB_ID;
                kho_LoaiDonVis.Add(kho_LoaiDonVi);
            }
            string action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.LoaiDonVi + CommonAction.DongBo;
            var result = PostDanhMuc(action, kho_LoaiDonVis, "LoaiDonViModel");

            foreach (var item in ListLoaiDonViModel)
            {
                await Task.Delay(2000);
                using (var httpClient = new HttpClient())
                {
                    string token = _commonFactory.GetTokenKhoCSDLQG();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.LoaiDonVi + "detail?syncId=" + item.ID.ToString()))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            apiResponse = await response.Content.ReadAsStringAsync();
                            var kho_LoaiDonVi = JsonConvert.DeserializeObject<Kho_LoaiDonVi>(apiResponse);
                            LoaiDonVi loaiDonVi = _loaiDonViService.GetLoaiDonViById(item.ID);
                            loaiDonVi.DB_ID = (int)kho_LoaiDonVi.id;
                            _loaiDonViService.UpdateLoaiDonVi(loaiDonVi);
                            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThanhCong, "Đồng bộ thành công", item.ID, "LoaiDonVi", item);
                            break;
                        }
                    }
                }
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Đang chờ đồng bộ", item.ID, "LoaiDonVi", item);
            }
            return Ok(_khoDanhMuc.PrepareMessageReturrn(apiResponse));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteLoaiDonVi([FromBody] LoaiDonViModel model)
        {
            var result = await DeleteDanhMuc(CommonDanhMuc.LoaiDonVi, (long)model.DB_ID, model);
            return Ok(result);
        }
        #endregion
        #region don vi
        [HttpGet]
        public async Task<IActionResult> GetAllDonVis()
        {
            List<Kho_DonVi> ListDonVi_Kho = new List<Kho_DonVi>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + "unit/all"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ListDonVi_Kho = JsonConvert.DeserializeObject<List<Kho_DonVi>>(apiResponse);
                }
            }
            var ListDonViModel = _khoDanhMuc.PrepareDonViModel(ListDonVi_Kho);
            return Ok(ListDonViModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDonVi([FromBody] List<DonViModel> ListDonViModel)
        {
            if (ListDonViModel == null)
                return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            List<Kho_DonVi> kho_DonVis = new List<Kho_DonVi>();
            foreach (var m in ListDonViModel)
            {
                DonVi donVi = _donViService.GetDonViById(m.ID);
                Kho_DonVi kho_DonVi = new Kho_DonVi();
                kho_DonVi.actionType = donVi.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_DonVi.id = donVi.DB_ID;
                if (donVi.PARENT_ID > 0)
                {
                    kho_DonVi.parentId = _donViService.GetDonViById(donVi.PARENT_ID.Value)?.DB_ID;
                }
                kho_DonVi.name = donVi.TEN;
                kho_DonVi.nationalBudgetCode = donVi.MA_DVQHNS;
                kho_DonVi.code = donVi.MA;
                kho_DonVi.unitLevelId = MapCapDonVi(donVi.CAP_DON_VI_ID, donVi.LOAI_CAP_DON_VI_ID.Value);
                //kho_DonVi.unitTypeId = (int?)m.DB_LOAI_DON_VI_ID;
                kho_DonVi.unitTypeId = (int?)donVi.LoaiDonVi.DB_ID;
                kho_DonVi.address = donVi.DIA_CHI;
                kho_DonVi.fax = donVi.FAX;
                kho_DonVi.accountingStandard = (donVi.CHE_DO_HACH_TOAN_ID > 0) ? (int)donVi.CHE_DO_HACH_TOAN_ID.Value : (int)enumCHE_DO_HACH_TOAN.HAO_MON;
                kho_DonVi.isActive = donVi.TRANG_THAI_ID;
                kho_DonVi.syncId = donVi.ID.ToString();
                kho_DonVi.syncParentId = donVi.PARENT_ID > 0 ? ((int?)donVi.PARENT_ID).ToString() : null;
                kho_DonVi.phoneNumber = donVi.DIEN_THOAI;
                kho_DonVi.administrativeLevel = (int)donVi.LOAI_CAP_DON_VI_ID;
                if (donVi.LA_DON_VI_NHAP_LIEU == null)
                {
                    continue;
                }
                kho_DonVi.functionalUnitType = donVi.LA_DON_VI_NHAP_LIEU == false ? 1 : 2;
                kho_DonVis.Add(kho_DonVi);
            }
            ResponseApi responseApi = await PostDanhMuc(CommonDanhMuc.DonVi, kho_DonVis, "DonVi");
            if (responseApi.Status)
            {
                foreach (var item in ListDonViModel)
                {
                    Kho_DonVi kho_DonVi = await GetIdDongBo<Kho_DonVi>(CommonDanhMuc.DonVi, item.ID.ToString());
                    if (kho_DonVi != null)
                    {
                        DonVi donVi = _donViService.GetDonViById(item.ID);
                        donVi.DB_ID = (int?)kho_DonVi.id;
                        _donViService.UpdateDonVi(donVi);
                        _hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ thành công danh mục đơn vị");
                    }
                }
            }
            return Ok(responseApi);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDonViThuCong([FromBody]ParamDongBoModel paramDongBo)
        {
            var ListDonViModel = _donViService.GetAllDonVis2(!paramDongBo.IsThemMoi, paramDongBo.TakeNumber);
            List<Kho_DonVi> kho_DonVis = new List<Kho_DonVi>();
            foreach (var m in ListDonViModel)
            {
                DonVi donVi = _donViService.GetDonViById(m.ID);
                Kho_DonVi kho_DonVi = new Kho_DonVi();
                kho_DonVi.actionType = donVi.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_DonVi.id = donVi.DB_ID;
                if (donVi.PARENT_ID > 0)
                {
                    kho_DonVi.parentId = _donViService.GetDonViById(donVi.PARENT_ID.Value)?.DB_ID;
                }
                kho_DonVi.name = donVi.TEN;
                kho_DonVi.nationalBudgetCode = donVi.MA_DVQHNS;
                kho_DonVi.code = donVi.MA;
                kho_DonVi.unitLevelId = MapCapDonVi(donVi.CAP_DON_VI_ID, donVi.LOAI_CAP_DON_VI_ID.Value);
                //kho_DonVi.unitTypeId = (int?)m.DB_LOAI_DON_VI_ID;
                kho_DonVi.unitTypeId = (int?)donVi.LoaiDonVi.DB_ID;
                kho_DonVi.address = donVi.DIA_CHI;
                kho_DonVi.fax = donVi.FAX;
                kho_DonVi.accountingStandard = (donVi.CHE_DO_HACH_TOAN_ID > 0) ? (int)donVi.CHE_DO_HACH_TOAN_ID.Value : (int)enumCHE_DO_HACH_TOAN.HAO_MON;
                kho_DonVi.isActive = donVi.TRANG_THAI_ID;
                kho_DonVi.syncId = donVi.ID.ToString();
                kho_DonVi.syncParentId = donVi.PARENT_ID > 0 ? ((int?)donVi.PARENT_ID).ToString() : null;
                kho_DonVi.phoneNumber = donVi.DIEN_THOAI;
                kho_DonVi.administrativeLevel = (int)donVi.LOAI_CAP_DON_VI_ID;
                if (donVi.LA_DON_VI_NHAP_LIEU == null)
                {
                    continue;
                }
                kho_DonVi.functionalUnitType = donVi.LA_DON_VI_NHAP_LIEU == false ? 1 : 2;
                kho_DonVis.Add(kho_DonVi);
            }
            ResponseApi responseApi = await PostDanhMuc(CommonDanhMuc.DonVi, kho_DonVis, "DonVi");
            if (responseApi.Status)
            {
                foreach (var item in ListDonViModel)
                {
                    Kho_DonVi kho_DonVi = await GetIdDongBo<Kho_DonVi>(CommonDanhMuc.DonVi, item.ID.ToString());
                    if (kho_DonVi != null)
                    {
                        DonVi donVi = _donViService.GetDonViById(item.ID);
                        donVi.DB_ID = (int?)kho_DonVi.id;
                        _donViService.UpdateDonVi(donVi);
                        _hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ thành công danh mục đơn vị");
                    }
                }
            }
            return Ok(responseApi);
        }
        [HttpGet]
        public async Task<IActionResult> GetInfoDonViKho(decimal? id)
        {
            if (!id.HasValue)
                return NullModel();
            Kho_DonVi kho_DonVi = await GetIdDongBo<Kho_DonVi>(CommonDanhMuc.DonVi, id.ToString());
            return Ok(kho_DonVi);
        }
        [HttpGet]
        public async Task<IActionResult> TestUpdateDonVi(decimal TreeLevel, decimal Id)
        {
            List<DonVi> ListDonVi = _donViService.GetAllDonVis(TreeLevel).Where(m => !(m.DB_ID > 0)).Where(m => m.CAP_DON_VI_ID != null && m.MA.Contains("T07")).ToList();
            List<DonViModel> ListDonViModel = new List<DonViModel>();
            //for (int i = 1; i < ListDonVi.Count; i++)
            //{
            //    List<DonVi> _ListDonVi = ListDonVi.Skip(i * 5000).Take(5000).ToList();
            //    foreach (var item in _ListDonVi)
            //    {
            //        DonViModel donViModel = new DonViModel();
            //        donViModel.ID = (long)item.ID;
            //        donViModel.MA = item.MA;
            //        donViModel.MA_DVQHNS = item.MA_DVQHNS;
            //        donViModel.TEN = item.TEN;
            //        donViModel.CHE_DO_HACH_TOAN_ID = item.CHE_DO_HACH_TOAN_ID;
            //        donViModel.CAP_DON_VI_ID = item.CAP_DON_VI_ID;
            //        donViModel.PARENT_ID = item.PARENT_ID;
            //        donViModel.DB_LOAI_DON_VI_ID = item.LoaiDonVi != null ? item.LoaiDonVi.DB_ID : null;
            //        donViModel.DIA_CHI = item.DIA_CHI;
            //        donViModel.FAX = item.FAX;
            //        donViModel.DIEN_THOAI = item.DIEN_THOAI;
            //        donViModel.TRANG_THAI_ID = item.TRANG_THAI_ID != null ? item.TRANG_THAI_ID.Value : false;
            //        donViModel.DB_ID = item.DB_ID;
            //        donViModel.LOAI_CAP_DON_VI_ID = item.LOAI_CAP_DON_VI_ID;
            //        ListDonViModel.Add(donViModel);
            //    }

            //    List<Kho_DonVi> kho_DonVis = new List<Kho_DonVi>();
            //    foreach (var m in ListDonViModel)
            //    {
            //        Kho_DonVi kho_DonVi = new Kho_DonVi();
            //        kho_DonVi.actionType = m.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
            //        kho_DonVi.id = m.DB_ID;
            //        if (m.PARENT_ID > 0)
            //        {
            //            kho_DonVi.parentId = _donViService.GetDonViById(m.PARENT_ID.Value).DB_ID;
            //        }
            //        kho_DonVi.name = m.TEN;
            //        kho_DonVi.nationalBudgetCode = m.MA_DVQHNS;
            //        kho_DonVi.code = m.MA;
            //        kho_DonVi.unitLevelId = MapCapDonVi(m.CAP_DON_VI_ID, m.LOAI_CAP_DON_VI_ID.Value);
            //        kho_DonVi.unitTypeId = (int?)m.DB_LOAI_DON_VI_ID;
            //        kho_DonVi.address = m.DIA_CHI;
            //        kho_DonVi.fax = m.FAX;
            //        kho_DonVi.accountingStandard = (m.CHE_DO_HACH_TOAN_ID > 0) ? (int)m.CHE_DO_HACH_TOAN_ID.Value : (int)enumCHE_DO_HACH_TOAN.HAO_MON;
            //        kho_DonVi.isActive = m.TRANG_THAI_ID;
            //        kho_DonVi.syncId = m.ID.ToString();
            //        kho_DonVi.syncParentId = m.PARENT_ID > 0 ? m.PARENT_ID.ToString() : null;
            //        kho_DonVi.phoneNumber = m.DIEN_THOAI;
            //        kho_DonVi.administrativeLevel = (int)m.LOAI_CAP_DON_VI_ID;
            //        kho_DonVis.Add(kho_DonVi);
            //    }
            //    string action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.DonVi + CommonAction.DongBo;
            //    GhiFile(kho_DonVis, "DM_DonVi_file");
            //    var responseObj = await _gsAPIService.PostObjectGSApi<ResponseApi>(action, kho_DonVis, _commonFactory.GetTokenKhoCSDLQG());
            //    if (responseObj != null && responseObj.Status)
            //    {
            //        foreach (var item in ListDonViModel)
            //        {
            //            item.TrangThaiDongBo = responseObj.Data;
            //        }
            //        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Đang chờ đồng bộ", 0, "DonVi", ListDonViModel);
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}
            foreach (var item in ListDonVi)
            {
                DonViModel donViModel = new DonViModel();
                donViModel.ID = (long)item.ID;
                donViModel.MA = item.MA;
                donViModel.MA_DVQHNS = item.MA_DVQHNS;
                donViModel.TEN = item.TEN;
                donViModel.CHE_DO_HACH_TOAN_ID = item.CHE_DO_HACH_TOAN_ID;
                donViModel.CAP_DON_VI_ID = item.CAP_DON_VI_ID;
                donViModel.PARENT_ID = item.PARENT_ID;
                donViModel.DB_LOAI_DON_VI_ID = item.LoaiDonVi != null ? item.LoaiDonVi.DB_ID : null;
                donViModel.DIA_CHI = item.DIA_CHI;
                donViModel.FAX = item.FAX;
                donViModel.DIEN_THOAI = item.DIEN_THOAI;
                donViModel.TRANG_THAI_ID = item.TRANG_THAI_ID != null ? item.TRANG_THAI_ID.Value : false;
                donViModel.DB_ID = item.DB_ID;
                donViModel.LOAI_CAP_DON_VI_ID = item.LOAI_CAP_DON_VI_ID;
                donViModel.LA_DON_VI_NHAP_LIEU = item.LA_DON_VI_NHAP_LIEU;
                ListDonViModel.Add(donViModel);
            }

            List<Kho_DonVi> kho_DonVis = new List<Kho_DonVi>();
            foreach (var m in ListDonViModel)
            {
                Kho_DonVi kho_DonVi = new Kho_DonVi();
                kho_DonVi.actionType = m.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_DonVi.id = m.DB_ID;
                if (m.PARENT_ID > 0)
                {
                    kho_DonVi.parentId = _donViService.GetDonViById(m.PARENT_ID.Value).DB_ID;
                }
                kho_DonVi.name = m.TEN;
                kho_DonVi.nationalBudgetCode = m.MA_DVQHNS;
                kho_DonVi.code = m.MA;
                kho_DonVi.unitLevelId = MapCapDonVi(m.CAP_DON_VI_ID, m.LOAI_CAP_DON_VI_ID.Value);
                kho_DonVi.unitTypeId = (int?)m.DB_LOAI_DON_VI_ID;
                kho_DonVi.address = m.DIA_CHI;
                kho_DonVi.fax = m.FAX;
                kho_DonVi.accountingStandard = (m.CHE_DO_HACH_TOAN_ID > 0) ? (int)m.CHE_DO_HACH_TOAN_ID.Value : (int)enumCHE_DO_HACH_TOAN.HAO_MON;
                kho_DonVi.isActive = m.TRANG_THAI_ID;
                kho_DonVi.syncId = m.ID.ToString();
                kho_DonVi.syncParentId = m.PARENT_ID > 0 ? m.PARENT_ID.ToString() : null;
                kho_DonVi.phoneNumber = m.DIEN_THOAI;
                kho_DonVi.administrativeLevel = (int)m.LOAI_CAP_DON_VI_ID;
                if (m.LA_DON_VI_NHAP_LIEU == null)
                {
                    continue;
                }
                kho_DonVi.functionalUnitType = m.LA_DON_VI_NHAP_LIEU == false ? 1 : 2;//1 : là đơn vị tổng hợp; 2: là đơn vị đăng  ký
                kho_DonVis.Add(kho_DonVi);
            }
            string action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.DonVi + CommonAction.DongBo;
            var responseObj = await _gsAPIService.PostObjectGSApi<ResponseApi>(action, kho_DonVis, _commonFactory.GetTokenKhoCSDLQG());
            if (responseObj != null && responseObj.Status)
            {
                //foreach (var item in ListDonViModel)
                //{
                //    item.TrangThaiDongBo = responseObj.Data;
                //}
                //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Đang chờ đồng bộ", 0, "DonVi", ListDonViModel);
            }
            if (Id > 0)
            {
                ListDonVi = ListDonVi.Where(m => m.ID == Id).ToList();
            }
            // var _ListDonViModel = ListLoaiDonVi.Select(m => m.ToModel<LoaiDonViModel>());


            return Ok(responseObj);

        }
        public int MapCapDonVi(decimal? CapDonViQLTS, decimal LoaiCapDonVi)
        {
            if (LoaiCapDonVi == (decimal)EnumLoaiCapDonVi.CapTrungUong)
            {
                switch (CapDonViQLTS)
                {
                    case (int)CapEnum.BoCoQuanTrungUong:
                        return 1;
                    //case (int)CapEnum.TongCuc:
                    //    return 2;
                    //case (int)CapEnum.Cuc:
                    //    return 3;
                    //case (int)CapEnum.ChiCuc:
                    //    return 4;
                    default:
                        return 1;
                }
            }
            if (LoaiCapDonVi == (decimal)EnumLoaiCapDonVi.CapDiaPhuong)
            {
                switch (CapDonViQLTS)
                {
                    case (int)CapEnum.Tinh:
                        return 1;
                    case (int)CapEnum.Huyen:
                        return 2;
                    case (int)CapEnum.Xa:
                        return 3;
                    default:
                        return 3;
                }
            }
            return 0;
        }
        [HttpGet]
        public async Task<IActionResult> UpdateDonViDaDongBo(int TreeLevel)
        {
            List<DonVi> ListDonVi = _donViService.GetAllDonVis(TreeLevel).Where(m => m.CAP_DON_VI_ID != null && m.MA.Contains("T07")).ToList();
            List<DonVi> ListErr = new List<DonVi>();
            List<DonVi> ListSuccess = new List<DonVi>();
            List<Kho_DonVi> ListChuaUpdate = new List<Kho_DonVi>();

            // get đơn vị đã đồng bộ sang kho
            string action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.DonVi + CommonAction.TatCa;
            AllModel allModel = new AllModel()
            {
                startDate = "11-01-2021 12:00:00",
                endDate = "13-03-2021 12:00:00"
            };
            var responseApi = await _gsAPIService.GetListGSApi<Kho_DonVi>(action, allModel, _commonFactory.GetTokenKhoCSDLQG());
            if (responseApi != null)
            {
                foreach (var item in ListDonVi)
                {
                    Kho_DonVi kho_DonVi = responseApi.Where(m => m.syncId == item.ID.ToString()).FirstOrDefault();
                    if (kho_DonVi != null)
                    {
                        item.DB_ID = (int?)kho_DonVi.id;
                        ListSuccess.Add(item);
                    }
                    else
                    {
                        ListErr.Add(item);
                    }
                }
            }
            //// get all đơn vị đã đồng bộ
            //string Action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.DonVi + CommonAction.TatCa;
            //AllModel allModel = new AllModel()
            //{
            //    startDate = "01-01-2020 12:00:00",
            //    endDate = "12-12-2020 12:00:00"
            //};

            //List<Kho_DonVi> kho_DonVis = await _gsAPIService.GetObjectGSApi<List<Kho_DonVi>>(Action, allModel, _commonFactory.GetTokenKhoCSDLQG());
            //List<DonVi> ListDonVi = new List<DonVi>();
            //List<string> ListDelete = new List<string>();
            //foreach (var item in kho_DonVis)
            //{
            //    try
            //    {
            //        DonVi donVi = _donViService.GetDonViById(decimal.Parse(item.syncId));
            //        if (donVi == null)
            //        {
            //            ListDelete.Add(item.syncId);
            //            continue;
            //        }
            //        if (donVi.DB_ID > 0)
            //            continue;
            //        donVi.DB_ID = (int?)item.id;
            //        ListDonVi.Add(donVi);
            //    }
            //    catch (Exception ex)
            //    {

            //        continue;
            //    }

            //}
            _donViService.UpdateListDonVi(ListSuccess);
            return Ok(ListErr);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteDonViDaDongBo()
        {
            List<int> ListId = new List<int>();
            for (int i = 21; i <= 92; i++)
            {
                ListId.Add(i);
            }
            return Ok(ListId.Select(m => new
            {
                id = m
            }));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDonVi([FromBody] DonViModel model)
        {
            var result = await DeleteDanhMuc(CommonDanhMuc.DonVi, (long)model.DB_ID, model);
            return Ok(result);
        }
        #endregion
        #region Mục đích sử dụng
        [HttpGet]
        public async Task<IActionResult> GetAllMucDichSuDungs()
        {
            List<Kho_MucDichSuDung> ListMucDichSuDung_Kho = new List<Kho_MucDichSuDung>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + "assetUsagePurposes/all"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ListMucDichSuDung_Kho = JsonConvert.DeserializeObject<List<Kho_MucDichSuDung>>(apiResponse);
                }
            }
            var ListMucDichSuDungModel = _khoDanhMuc.PrepareMucDichSuDungModel(ListMucDichSuDung_Kho);
            return Ok(ListMucDichSuDungModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateMucDichSuDung([FromBody] List<MucDichSuDungModel> ListMucDichSuDungModel)
        {
            #region cơ quan tổ chức
            List<Kho_MucDichSuDung> kho_MucDichSuDungs = new List<Kho_MucDichSuDung>();
            foreach (var m in ListMucDichSuDungModel)
            {
                MucDichSuDung mucDichSuDung = _mucDichSuDungService.GetMucDichSuDungById(m.ID);
                m.DB_ID = mucDichSuDung.DB_ID;
                Kho_MucDichSuDung kho_MucDichSuDung = new Kho_MucDichSuDung();
                kho_MucDichSuDung.actionType = m.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_MucDichSuDung.name = m.TEN;
                kho_MucDichSuDung.code = m.MA;
                kho_MucDichSuDung.syncId = m.ID.ToString();
                kho_MucDichSuDung.id = m.DB_ID;
                //kho_MucDichSuDung.assetTypeId = (int)enumIdNhomTaiSanKho.CoQuanToChuc;
                kho_MucDichSuDung.assetTypeIds = m.LOAI_HINH_TAI_SAN_ID.HasValue ? new List<long?>() { (long)CommonHelper.toLoaiHinhTaiSanKho(m.LOAI_HINH_TAI_SAN_ID.Value) } : null;
                kho_MucDichSuDungs.Add(kho_MucDichSuDung);
            }
            var result = await PostDanhMuc(CommonDanhMuc.MucDichSuDung, kho_MucDichSuDungs, "MucDichSuDung");
            if (result.Status)
            {
                foreach (var item in ListMucDichSuDungModel)
                {
                    Kho_MucDichSuDung kho_MucDich = await GetIdDongBo<Kho_MucDichSuDung>(CommonDanhMuc.MucDichSuDung, item.ID.ToString());
                    if (kho_MucDich != null)
                    {
                        MucDichSuDung mucDichSuDung = _mucDichSuDungService.GetMucDichSuDungById(item.ID);
                        if (mucDichSuDung != null)
                        {
                            mucDichSuDung.DB_ID = (int?)kho_MucDich.id;
                            _mucDichSuDungService.UpdateMucDichSuDung(mucDichSuDung);
                        }
                        _hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ thành công mục đích sử dụng tài sản cơ quan tổ chức", mucDichSuDung.ToModel<MucDichSuDungModel>(), "MucDichSuDung");
                    }
                    else
                    {
                        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThatBai, "Đồng bộ thất bại", item.ID, "MucDichSuDung", item);
                    }

                }
            }
            #endregion
            #region Tài sản toàn dân
            //kho_MucDichSuDungs = new List<Kho_MucDichSuDung>();
            //foreach (var m in ListMucDichSuDungModel)
            //{
            //    MucDichSuDung mucDichSuDung = _mucDichSuDungService.GetMucDichSuDungById(m.ID);
            //    m.DB_ID = CommonHelper.Kho_GetIdDanhMuc(mucDichSuDung.DB_ID_JSON, enumCSDLQG_MA_NHOM_TAI_SAN.XacLapToanDan);
            //    Kho_MucDichSuDung kho_MucDichSuDung = new Kho_MucDichSuDung();
            //    kho_MucDichSuDung.actionType = m.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
            //    kho_MucDichSuDung.name = m.TEN;
            //    kho_MucDichSuDung.code = m.MA;
            //    kho_MucDichSuDung.syncId = CommonHelperApi.IdLoaiTaiSanKho(m.ID, enumCSDLQG_MA_NHOM_TAI_SAN.XacLapToanDan);
            //    kho_MucDichSuDung.id = m.DB_ID;
            //    kho_MucDichSuDung.assetTypeId = (int)enumIdNhomTaiSanKho.XacLapToanDan;
            //    kho_MucDichSuDungs.Add(kho_MucDichSuDung);
            //}
            //result = await PostDanhMuc(CommonDanhMuc.MucDichSuDung, kho_MucDichSuDungs, "MucDichSuDung");
            //if (result.Status)
            //{
            //    foreach (var item in ListMucDichSuDungModel)
            //    {
            //        Kho_MucDichSuDung kho_MucDich = await GetIdDongBo<Kho_MucDichSuDung>(CommonDanhMuc.MucDichSuDung, CommonHelperApi.IdLoaiTaiSanKho(item.ID, enumCSDLQG_MA_NHOM_TAI_SAN.XacLapToanDan));
            //        if (kho_MucDich != null)
            //        {
            //            MucDichSuDung mucDichSuDung = _mucDichSuDungService.GetMucDichSuDungById(item.ID);
            //            if (mucDichSuDung.DB_ID_JSON == null)
            //            {
            //                NhomTaiSanKho nhomTaiSanKho = new NhomTaiSanKho();
            //                nhomTaiSanKho.ID_NPA = (int)kho_MucDich.id;
            //                mucDichSuDung.DB_ID_JSON = nhomTaiSanKho.toStringJson();
            //                _mucDichSuDungService.UpdateMucDichSuDung(mucDichSuDung);
            //            }
            //            else
            //            {
            //                NhomTaiSanKho nhomTaiSanKho = JsonConvert.DeserializeObject<NhomTaiSanKho>(mucDichSuDung.DB_ID_JSON);
            //                nhomTaiSanKho.ID_NPA = (int)kho_MucDich.id;
            //                mucDichSuDung.DB_ID_JSON = nhomTaiSanKho.toStringJson();
            //                _mucDichSuDungService.UpdateMucDichSuDung(mucDichSuDung);
            //            }
            //            _hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ thành công mục đích sử dụng tài sản xác lập", mucDichSuDung.ToModel<MucDichSuDungModel>(), "MucDichSuDung");
            //        }
            //    }
            //}
            #endregion           
            return Ok(result);
        }
        //[HttpGet]
        //public async Task<IActionResult> TestUpdateMucDichSuDung()
        //{
        //    //if (ListLoaiDonViModel == null)
        //    // return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
        //    // getall Loại đơn vị
        //    string apiResponse = null;
        //    var ListMucDichSuDung = _mucDichSuDungService.GetMucDichSuDungs();
        //    // var _ListDonViModel = ListLoaiDonVi.Select(m => m.ToModel<LoaiDonViModel>());
        //    var ListMucDichSuDungModel = ListMucDichSuDung.Select(m => new MucDichSuDungModel()
        //    {
        //        MA = m.MA,
        //        LOAI_HINH_TAI_SAN_ID = m.LOAI_HINH_TAI_SAN_ID,
        //        DB_ID = m.DB_ID,
        //        TEN = m.TEN,
        //        ID = (long)m.ID,
        //    }).ToList();
        //    foreach (var item in ListMucDichSuDungModel)
        //    {
        //        using (var httpClient = new HttpClient())
        //        {
        //            string token = _commonFactory.GetTokenKhoCSDLQG();
        //            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //            using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.MucDichSuDung + "detail?syncId=" + CommonHelperApi.IdLoaiTaiSanKho(item.ID, enumCSDLQG_MA_NHOM_TAI_SAN.CoQuanToChuc)))
        //            {
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    apiResponse = await response.Content.ReadAsStringAsync();
        //                    var kho_MucDichSuDung = JsonConvert.DeserializeObject<Kho_MucDichSuDung>(apiResponse);
        //                    MucDichSuDung mucDichSuDung = _mucDichSuDungService.GetMucDichSuDungById(item.ID);
        //                    if (mucDichSuDung.DB_ID_JSON == null)
        //                    {
        //                        NhomTaiSanKho nhomTaiSanKho = new NhomTaiSanKho();
        //                        nhomTaiSanKho.ID_OA = (int)kho_MucDichSuDung.id;
        //                        mucDichSuDung.DB_ID_JSON = nhomTaiSanKho.toStringJson();
        //                        _mucDichSuDungService.UpdateMucDichSuDung(mucDichSuDung);
        //                        item.DB_ID = (int)kho_MucDichSuDung.id;
        //                    }
        //                    else
        //                    {
        //                        NhomTaiSanKho nhomTaiSanKho = JsonConvert.DeserializeObject<NhomTaiSanKho>(mucDichSuDung.DB_ID_JSON);
        //                        nhomTaiSanKho.ID_OA = (int)kho_MucDichSuDung.id;
        //                        mucDichSuDung.DB_ID_JSON = nhomTaiSanKho.toStringJson();
        //                        _mucDichSuDungService.UpdateMucDichSuDung(mucDichSuDung);
        //                        item.DB_ID = (int)kho_MucDichSuDung.id;
        //                    }

        //                }

        //            }
        //        }
        //    }
        //    var LoaiTaiSanLv1 = _loaiTaiSanService.GetLoaiTaiSanByTreeLevel(1).Where(m => m.CHE_DO_HAO_MON_ID == 23 && m.LOAI_HINH_TAI_SAN_ID != (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC);
        //    List<Kho_MucDichSuDung> kho_MucDichSuDungs = new List<Kho_MucDichSuDung>();
        //    foreach (var m in ListMucDichSuDungModel)
        //    {
        //        Kho_MucDichSuDung kho_MucDichSuDung = new Kho_MucDichSuDung();
        //        kho_MucDichSuDung.actionType = m.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
        //        kho_MucDichSuDung.name = m.TEN;
        //        kho_MucDichSuDung.code = m.MA;
        //        kho_MucDichSuDung.syncId = CommonHelperApi.IdLoaiTaiSanKho(m.ID, enumCSDLQG_MA_NHOM_TAI_SAN.CoQuanToChuc);
        //        kho_MucDichSuDung.id = m.DB_ID;
        //        kho_MucDichSuDung.assetTypeId = (int)enumIdNhomTaiSanKho.CoQuanToChuc;
        //        kho_MucDichSuDungs.Add(kho_MucDichSuDung);
        //    }
        //    var dataJson = kho_MucDichSuDungs.toStringJson();
        //    // kho_LoaiDonVis = kho_LoaiDonVis.Where(m => m.id > 0).ToList();
        //    using (var httpClient = new HttpClient())
        //    {
        //        StringContent content = new StringContent(JsonConvert.SerializeObject(kho_MucDichSuDungs), Encoding.UTF8, "application/json");
        //        string token = _commonFactory.GetTokenKhoCSDLQG();
        //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //        using (var response = await httpClient.PostAsync(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.MucDichSuDung + "sync", content))
        //        {
        //            apiResponse = await response.Content.ReadAsStringAsync();
        //            var responseObj = JsonConvert.DeserializeObject<ResponseApi>(apiResponse);
        //            if (responseObj.Status)
        //            {
        //                foreach (var item in ListMucDichSuDungModel)
        //                {
        //                    item.TrangThaiDongBo = responseObj.Data;
        //                }
        //                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Đang chờ đồng bộ", 0, "MucDichSuDung", ListMucDichSuDungModel);
        //            }
        //            else
        //            {
        //                return Ok(MessageReturn.CreateErrorMessage("Tiếp nhận thất bại"));
        //            }
        //        }
        //    }
        //    foreach (var item in ListMucDichSuDungModel)
        //    {
        //        bool IsUpdate = false;
        //        for (int i = 0; i < 3; i++)
        //        {
        //            await Task.Delay(1000);
        //            IsUpdate = false;
        //            using (var httpClient = new HttpClient())
        //            {
        //                string token = _commonFactory.GetTokenKhoCSDLQG();
        //                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //                using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.MucDichSuDung + "detail?syncId=" + CommonHelperApi.IdLoaiTaiSanKho(item.ID, enumCSDLQG_MA_NHOM_TAI_SAN.CoQuanToChuc)))
        //                {
        //                    if (response.IsSuccessStatusCode)
        //                    {
        //                        apiResponse = await response.Content.ReadAsStringAsync();
        //                        var kho_MucDichSuDung = JsonConvert.DeserializeObject<Kho_MucDichSuDung>(apiResponse);
        //                        MucDichSuDung mucDichSuDung = _mucDichSuDungService.GetMucDichSuDungById(item.ID);
        //                        if (mucDichSuDung.DB_ID_JSON == null)
        //                        {
        //                            NhomTaiSanKho nhomTaiSanKho = new NhomTaiSanKho();
        //                            nhomTaiSanKho.ID_OA = (int)kho_MucDichSuDung.id;
        //                            mucDichSuDung.DB_ID_JSON = nhomTaiSanKho.toStringJson();
        //                            _mucDichSuDungService.UpdateMucDichSuDung(mucDichSuDung);
        //                        }
        //                        else
        //                        {
        //                            NhomTaiSanKho nhomTaiSanKho = JsonConvert.DeserializeObject<NhomTaiSanKho>(mucDichSuDung.DB_ID_JSON);
        //                            nhomTaiSanKho.ID_OA = (int)kho_MucDichSuDung.id;
        //                            mucDichSuDung.DB_ID_JSON = nhomTaiSanKho.toStringJson();
        //                            _mucDichSuDungService.UpdateMucDichSuDung(mucDichSuDung);
        //                        }
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        IsUpdate = false;
        //                    }
        //                }
        //            }
        //            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Đang chờ đồng bộ", item.ID, "MucDichSuDung", item);
        //        }
        //        if (!IsUpdate)
        //        {
        //            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThatBai, "Đồng bộ thất bại", item.ID, "MucDichSuDung", item);
        //        }
        //        else
        //        {
        //            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThanhCong, "Đồng bộ thành công", item.ID, "MucDichSuDung", item);
        //        }
        //    }
        //    return Ok(_khoDanhMuc.PrepareMessageReturrn(apiResponse));
        //}
        [HttpPost]
        public async Task<IActionResult> DeleteMucDichSuDung([FromBody] MucDichSuDungModel model)
        {

            MucDichSuDung mucDichSuDung = _mucDichSuDungService.GetMucDichSuDungById(model.ID);
            var result = await DeleteDanhMuc(CommonDanhMuc.MucDichSuDung, (long)mucDichSuDung.DB_ID, model);
            return Ok(result);
        }
        #endregion
        #region Nguồn gốc tài sản
        [HttpGet]
        public async Task<IActionResult> GetAllNguonGocTaiSans()
        {
            List<Kho_NguonGocTaiSan> ListNguonGocTaiSan_Kho = new List<Kho_NguonGocTaiSan>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + "assetSources/all"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ListNguonGocTaiSan_Kho = JsonConvert.DeserializeObject<List<Kho_NguonGocTaiSan>>(apiResponse);
                }
            }
            var ListNguonGocTaiSanModel = _khoDanhMuc.PrepareNguonGocTaiSanModel(ListNguonGocTaiSan_Kho);
            return Ok(ListNguonGocTaiSanModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateNguonGocTaiSan([FromBody] List<NguonGocTaiSanModel> ListNguonGocTaiSanModel)
        {
            List<Kho_NguonGocTaiSan> kho_NguonGocs = new List<Kho_NguonGocTaiSan>();
            foreach (var m in ListNguonGocTaiSanModel)
            {
                NguonGocTaiSan nguonGocTaiSan = _nguonGocTaiSanService.GetNguonGocTaiSanById(m.ID);
                m.DB_ID = nguonGocTaiSan.DB_ID;
                Kho_NguonGocTaiSan kho_NguonGoc = new Kho_NguonGocTaiSan();
                kho_NguonGoc.actionType = m.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_NguonGoc.name = m.TEN;
                kho_NguonGoc.code = m.MA;
                kho_NguonGoc.syncId = m.ID.ToString();
                kho_NguonGoc.syncParentId = m.PARENT_ID > 0 ? m.PARENT_ID.ToString() : null;
                if (m.PARENT_ID > 0)
                {
                    kho_NguonGoc.parentId = _nguonGocTaiSanService.GetNguonGocTaiSanById(m.PARENT_ID.Value).DB_ID;
                }
                kho_NguonGoc.id = m.DB_ID;
                kho_NguonGocs.Add(kho_NguonGoc);
            }
            var response = await PostDanhMuc(CommonDanhMuc.NguonGocTaiSan, kho_NguonGocs, "NguonGocTaiSan");

            if (response.Status)
            {
                foreach (var item in ListNguonGocTaiSanModel)
                {
                    Kho_NguonGocTaiSan kho_NguonGoc = await GetIdDongBo<Kho_NguonGocTaiSan>(CommonDanhMuc.NguonGocTaiSan, item.ID.ToString());
                    if (kho_NguonGoc != null)
                    {
                        var nguonGoc = _nguonGocTaiSanService.GetNguonGocTaiSanById(item.ID);
                        nguonGoc.DB_ID = (int)kho_NguonGoc.id;
                        _nguonGocTaiSanService.UpdateNguonGocTaiSan(nguonGoc);
                        _hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ thành công", nguonGoc.ToModel<NguonGocTaiSanModel>(), "NguonGocTaiSan");
                    }
                }

            }
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> TestUpdateNguonGocTaiSan()
        {
            //if (ListLoaiDonViModel == null)
            // return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            // getall Loại đơn vị
            string apiResponse = null;
            List<NguonGocTaiSan> ListNguonGocTaiSan = _nguonGocTaiSanService.GetAllNguonGocTaiSans().Where(m => m.TREE_LEVEL == 2 && m.ID != 1).ToList();
            // var _ListDonViModel = ListLoaiDonVi.Select(m => m.ToModel<LoaiDonViModel>());
            var ListNguonGocTaiSanModel = ListNguonGocTaiSan.Select(m => new NguonGocTaiSanModel()
            {
                MA = m.MA,
                DB_ID = m.DB_ID,
                PARENT_ID = m.PARENT_ID,
                TEN = m.TEN,
                ID = (long)m.ID,
            }).ToList();
            foreach (var item in ListNguonGocTaiSanModel)
            {
                apiResponse = await GetApiRespond(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.NguonGocTaiSan + "detail?syncId=" + item.ID.ToString(), null, 2);
                if (!string.IsNullOrEmpty(apiResponse))
                {
                    Kho_NguonGocTaiSan kho_NguonGoc = JsonConvert.DeserializeObject<Kho_NguonGocTaiSan>(apiResponse);
                    NguonGocTaiSan nguonGoc = _nguonGocTaiSanService.GetNguonGocTaiSanById(item.ID);
                    nguonGoc.DB_ID = (int)kho_NguonGoc.id;
                    item.DB_ID = (int)kho_NguonGoc.id;
                    _nguonGocTaiSanService.UpdateNguonGocTaiSan(nguonGoc);
                }
                //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThanhCong, "Đồng bộ thành công", item.ID, "LoaiDonVi", item);
            }
            List<Kho_NguonGocTaiSan> kho_NguonGocs = new List<Kho_NguonGocTaiSan>();
            foreach (var m in ListNguonGocTaiSanModel)
            {
                Kho_NguonGocTaiSan kho_NguonGoc = new Kho_NguonGocTaiSan();
                kho_NguonGoc.actionType = m.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_NguonGoc.name = m.TEN;
                kho_NguonGoc.code = m.MA;
                kho_NguonGoc.syncId = m.ID.ToString();
                kho_NguonGoc.syncParentId = m.PARENT_ID > 0 ? m.PARENT_ID.ToString() : null;
                if (m.PARENT_ID > 0)
                {
                    kho_NguonGoc.parentId = _nguonGocTaiSanService.GetNguonGocTaiSanById(m.PARENT_ID.Value).DB_ID;
                }
                kho_NguonGoc.id = m.DB_ID;
                kho_NguonGocs.Add(kho_NguonGoc);
            }
            var dataJson = kho_NguonGocs.toStringJson();
            // kho_LoaiDonVis = kho_LoaiDonVis.Where(m => m.id > 0).ToList();
            apiResponse = await GetApiRespond(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.NguonGocTaiSan + "sync", kho_NguonGocs, 1);
            if (!string.IsNullOrEmpty(apiResponse))
            {
                var responseObj = JsonConvert.DeserializeObject<ResponseApi>(apiResponse);
                if (responseObj.Status)
                {
                    foreach (var item in ListNguonGocTaiSanModel)
                    {
                        item.TrangThaiDongBo = responseObj.Data;
                    }
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Đang chờ đồng bộ", 0, "NguonGocTaiSan", ListNguonGocTaiSanModel);
                }
                else
                {
                    return Ok(MessageReturn.CreateErrorMessage("Tiếp nhận thất bại"));
                }
            }
            foreach (var item in ListNguonGocTaiSanModel)
            {
                bool IsUpdate = false;
                for (int i = 0; i < 3; i++)
                {
                    await Task.Delay(2000);
                    IsUpdate = false;
                    apiResponse = await GetApiRespond(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.NguonGocTaiSan + "detail?syncId=" + item.ID.ToString(), null, 2);
                    if (!string.IsNullOrEmpty(apiResponse))
                    {
                        Kho_NguonGocTaiSan kho_NguonGoc = JsonConvert.DeserializeObject<Kho_NguonGocTaiSan>(apiResponse);
                        NguonGocTaiSan nguonGoc = _nguonGocTaiSanService.GetNguonGocTaiSanById(item.ID);
                        nguonGoc.DB_ID = (int)kho_NguonGoc.id;
                        _nguonGocTaiSanService.UpdateNguonGocTaiSan(nguonGoc);
                        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThanhCong, "Đồng bộ thành công", item.ID, "NguonGocTaiSan", item);
                        break;
                    }
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Đang chờ đồng bộ", item.ID, "NguonGocTaiSan", item);
                }
                if (!IsUpdate)
                {
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThatBai, "Đồng bộ thất bại", item.ID, "NguonGocTaiSan", item);
                }
                else
                {
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThanhCong, "Đồng bộ thành công", item.ID, "NguonGocTaiSan", item);
                }
            }
            return Ok(_khoDanhMuc.PrepareMessageReturrn(apiResponse));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteNguonGocTaiSan([FromBody] NguonGocTaiSanModel model)
        {
            var result = await DeleteDanhMuc(CommonDanhMuc.NguonGocTaiSan, (long)model.DB_ID, model);
            return Ok(result);
        }
        #endregion
        #region Hiện trạng sử dụng
        [HttpGet]
        public async Task<IActionResult> GetAllHienTrangs()
        {
            List<Kho_HienTrang> ListHienTrang_Kho = new List<Kho_HienTrang>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + "assetUsageStates/all"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ListHienTrang_Kho = JsonConvert.DeserializeObject<List<Kho_HienTrang>>(apiResponse);
                }
            }
            var ListHienTrangModel = _khoDanhMuc.PrepareHienTrangModel(ListHienTrang_Kho);
            return Ok(ListHienTrangModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateHienTrang([FromBody] List<HienTrangModel> ListHienTrangModel)
        {
            #region Tài sản cơ quan tổ chức
            List<Kho_HienTrang> kho_HienTrangs = new List<Kho_HienTrang>();
            foreach (var m in ListHienTrangModel)
            {
                Kho_HienTrang kho_HienTrang = new Kho_HienTrang();
                kho_HienTrang.actionType = m.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_HienTrang.name = m.TEN_HIEN_TRANG;
                kho_HienTrang.code = m.ID.ToString();
                kho_HienTrang.syncId = m.ID.ToString();
                kho_HienTrang.id = m.DB_ID;
                kho_HienTrang.assetTypeIds = new List<long?>() { (long)CommonHelper.toLoaiHinhTaiSanKho(m.LOAI_HINH_TAI_SAN_ID) };
                //kho_HienTrang.assetTypeId = CommonHelper.toLoaiHinhTaiSanKho(m.LOAI_HINH_TAI_SAN_ID);
                switch (m.LOAI_HINH_TAI_SAN_ID)
                {
                    case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                    case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                        {
                            kho_HienTrang.isAreaUsage = true;
                            break;
                        }
                    default:
                        {
                            kho_HienTrang.isAreaUsage = false;
                            break;
                        }
                }
                kho_HienTrangs.Add(kho_HienTrang);
            }
            var result = await PostDanhMuc(CommonDanhMuc.HienTrangSuDung, kho_HienTrangs, "HienTrangSuDung");
            if (result.Status)
            {
                foreach (var item in ListHienTrangModel)
                {
                    Kho_HienTrang kho_HienTrang = await GetIdDongBo<Kho_HienTrang>(CommonDanhMuc.HienTrangSuDung, item.ID.ToString());
                    if (kho_HienTrang != null)
                    {
                        HienTrang hienTrang = _hienTrangService.GetHienTrangById(item.ID);

                        hienTrang.DB_ID = (decimal)kho_HienTrang.id.Value;
                        _hienTrangService.UpdateHienTrang(hienTrang);

                    }
                }
            }
            #endregion          
            return Ok(result);
        }
        //[HttpPost]
        //public async Task<IActionResult> UpdateHienTrang2(Decimal? id = null, Decimal? LoaiHinhTaiSan = null, Decimal? NhomHienTrang = null, bool? isDbIDNull = null)
        //{
        //    #region Tài sản cơ quan tổ chức
        //    List<HienTrang> ListHienTrang = new List<HienTrang>();
        //    if (id > 0)
        //    {
        //        HienTrang ht = _hienTrangService.GetHienTrangById(id.Value);
        //        if (ht != null)
        //            ListHienTrang.Add(ht);
        //    }
        //    _hienTrangService.GetHienTrangs
        //    List<Kho_HienTrang> kho_HienTrangs = new List<Kho_HienTrang>();
        //    foreach (var m in ListHienTrangModel)
        //    {
        //        Kho_HienTrang kho_HienTrang = new Kho_HienTrang();
        //        kho_HienTrang.actionType = m.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
        //        kho_HienTrang.name = m.TEN_HIEN_TRANG;
        //        kho_HienTrang.code = m.ID.ToString();
        //        kho_HienTrang.syncId = m.ID.ToString();
        //        kho_HienTrang.id = m.DB_ID;
        //        //kho_HienTrang.assetTypeId = CommonHelper.toLoaiHinhTaiSanKho(m.LOAI_HINH_TAI_SAN_ID);
        //        switch (m.LOAI_HINH_TAI_SAN_ID)
        //        {
        //            case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
        //            case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
        //                {
        //                    kho_HienTrang.isAreaUsage = true;
        //                    break;
        //                }
        //            default:
        //                {
        //                    kho_HienTrang.isAreaUsage = false;
        //                    break;
        //                }
        //        }
        //        kho_HienTrangs.Add(kho_HienTrang);
        //    }
        //    var result = await PostDanhMuc(CommonDanhMuc.HienTrangSuDung, kho_HienTrangs, "HienTrangSuDung");
        //    if (result.Status)
        //    {
        //        foreach (var item in ListHienTrangModel)
        //        {
        //            Kho_HienTrang kho_HienTrang = await GetIdDongBo<Kho_HienTrang>(CommonDanhMuc.HienTrangSuDung, item.ID.ToString());
        //            if (kho_HienTrang != null)
        //            {
        //                HienTrang hienTrang = _hienTrangService.GetHienTrangById(item.ID);

        //                hienTrang.DB_ID = (decimal)kho_HienTrang.id.Value;
        //                _hienTrangService.UpdateHienTrang(hienTrang);

        //            }
        //        }
        //    }
        //    #endregion          
        //    return Ok(result);
        //}
        [HttpGet]
        public async Task<IActionResult> TestUpdateHienTrang()
        {
            string apiResponse = null;
            var ListHienTrang = _hienTrangService.GetHienTrangs();
            // var _ListDonViModel = ListLoaiDonVi.Select(m => m.ToModel<LoaiDonViModel>());
            var ListHienTrangModel = ListHienTrang.Where(m => !(m.DB_ID > 0)).Select(m => new HienTrangModel()
            {
                MA = m.ID.ToString(),
                LOAI_HINH_TAI_SAN_ID = m.LOAI_HINH_TAI_SAN_ID,
                DB_ID = (int?)m.DB_ID,
                TEN_HIEN_TRANG = m.TEN_HIEN_TRANG,
                KIEU_DU_LIEU_ID = m.KIEU_DU_LIEU_ID,
                ID = (long)m.ID,
            }).ToList();
            List<Kho_HienTrang> kho_HienTrangs = new List<Kho_HienTrang>();
            foreach (var m in ListHienTrangModel)
            {
                Kho_HienTrang kho_HienTrang = new Kho_HienTrang();
                kho_HienTrang.actionType = m.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_HienTrang.name = m.TEN_HIEN_TRANG;
                kho_HienTrang.code = m.ID.ToString();
                kho_HienTrang.syncId = m.ID.ToString();
                kho_HienTrang.id = m.DB_ID;

                switch (m.LOAI_HINH_TAI_SAN_ID)
                {
                    case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                        {
                            kho_HienTrang.isAreaUsage = true;
                            kho_HienTrang.assetTypeIds.Add(7);
                            break;
                        }
                    case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                        {
                            kho_HienTrang.isAreaUsage = true;
                            kho_HienTrang.assetTypeIds.Add(8);
                            break;
                        }
                    default:
                        {
                            kho_HienTrang.isAreaUsage = false;
                            break;
                        }
                }
                kho_HienTrangs.Add(kho_HienTrang);
            }
            var dataJson = kho_HienTrangs.toStringJson();
            StringContent content = new StringContent(JsonConvert.SerializeObject(kho_HienTrangs), Encoding.UTF8, "application/json");
            string token = _commonFactory.GetTokenKhoCSDLQG();
            var response = await _gsAPIService.PostObjectGSApi<ResponseApi>(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.HienTrangSuDung + CommonAction.DongBo, kho_HienTrangs, _commonFactory.GetTokenKhoCSDLQG());
            if (response.Status)
            {
                foreach (var item in ListHienTrangModel)
                {
                    Kho_HienTrang kho_HienTrang = await GetIdDongBo<Kho_HienTrang>(CommonDanhMuc.HienTrangSuDung, item.ID.ToString());
                    if (kho_HienTrang != null)
                    {
                        HienTrang hienTrang = _hienTrangService.GetHienTrangById(item.ID);

                        hienTrang.DB_ID = (decimal)kho_HienTrang.id.Value;
                        _hienTrangService.UpdateHienTrang(hienTrang);
                    }
                }
                InsertLogModel insertLogModel = new InsertLogModel()
                {
                    ResponseApi = response,
                    Data = ListHienTrangModel,
                    LoaiDuLieu = "Danh mục"
                };
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Đang chờ đồng bộ", 0, "HienTrang", insertLogModel);
            }
            else
            {
                return Ok(MessageReturn.CreateErrorMessage("Tiếp nhận thất bại"));
            }
            return Ok(_khoDanhMuc.PrepareMessageReturrn(apiResponse));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteHienTrang([FromBody] HienTrangModel model)
        {
            HienTrang hienTrang = _hienTrangService.GetHienTrangById(model.ID);
            var result = await DeleteDanhMuc(CommonDanhMuc.HienTrangSuDung, (long)model.DB_ID, model);
            return Ok(result);
        }
        #endregion
        #region Người dùng
        [HttpGet]
        public async Task<IActionResult> GetAllNguoiDungs()
        {
            List<Kho_NguoiDung> ListNguoiDung_Kho = new List<Kho_NguoiDung>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + "syncUsers/all"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ListNguoiDung_Kho = JsonConvert.DeserializeObject<List<Kho_NguoiDung>>(apiResponse);
                }
            }
            var ListNguoiDungModel = _khoDanhMuc.PrepareNguoiDungModel(ListNguoiDung_Kho);
            return Ok(ListNguoiDungModel);
        }
        [HttpGet]
        public async Task<IActionResult> GetNguoiDungByName(string name)
        {
            ResponseApi responseApi = new ResponseApi();
            var detail = new
            {
                username = name
            };
            var token = _commonFactory.GetTokenKhoCSDLQG();
            if (token == "1")
            {
                responseApi = new ResponseApi()
                {
                    Status = false,
                    Message = "Lỗi truy xuất dữ liệu bên kho",
                    Date = DateTime.Now,
                    Data = 1
                };
                return Ok(responseApi);
            }
            Kho_NguoiDung kho_NguoiDung = await _gsAPIService.GetObjectGSApiKho<Kho_NguoiDung>(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.NguoiDung + CommonAction.ChiTiet, detail, token);
            if (kho_NguoiDung != null)
            {
                responseApi = new ResponseApi()
                {
                    Status = true,
                    Message = "Tên đã tồn tại",
                    Date = DateTime.Now,
                    Data = kho_NguoiDung
                };
            }
            else
            {
                responseApi = new ResponseApi()
                {
                    Status = false,
                    Message = "Ok",
                    Date = DateTime.Now,
                    Data = null
                };
            }
            return Ok(responseApi);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateNguoiDung([FromBody] List<NguoiDungModel> ListNguoiDungModel)
        {
            if (ListNguoiDungModel == null)
                return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            List<Kho_NguoiDung> kho_NguoiDungs = new List<Kho_NguoiDung>();
            ResponseApi responseApi = new ResponseApi();
            foreach (var m in ListNguoiDungModel)
            {
                try
                {
                    NguoiDung nguoiDung = _nguoiDungService.GetNguoiDungByUsername(m.TEN_DANG_NHAP);
                    Kho_NguoiDung kho_NguoiDung = new Kho_NguoiDung();
                    var detail = new
                    {
                        username = nguoiDung.TEN_DANG_NHAP
                    };
                    var token = _commonFactory.GetTokenKhoCSDLQG();
                    if (token == "1")
                    {
                        responseApi = new ResponseApi()
                        {
                            Status = false,
                            Message = "Lỗi truy xuất dữ liệu bên kho",
                            Date = DateTime.Now,
                            Data = 1
                        };
                        return Ok(responseApi);
                    }
                    Kho_NguoiDung Check_kho_NguoiDung = await _gsAPIService.GetObjectGSApiKho<Kho_NguoiDung>(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.NguoiDung + CommonAction.ChiTiet, detail, token);
                    if (Check_kho_NguoiDung != null)
                    {
                        kho_NguoiDung.id = Check_kho_NguoiDung.id;
                    }
                    kho_NguoiDung.actionType = nguoiDung.DB_ID == "0" ? 1 : 2;
                    kho_NguoiDung.username = m.TEN_DANG_NHAP;
                    kho_NguoiDung.email = m.EMAIL;
                    kho_NguoiDung.phoneNumber = m.MOBILE;
                    kho_NguoiDung.passwordHash = nguoiDung.PASSWORD_HASH;
                    if (m.TMP_MAT_KHAU != null)
                    {
                        kho_NguoiDung.password = m.TMP_MAT_KHAU;
                    }
                    kho_NguoiDung.fullName = m.TEN_DAY_DU;
                    kho_NguoiDung.lockoutEnabled = m.TRANG_THAI_ID == 1 ? false : true;
                    kho_NguoiDung.isAdministrator = m.IS_QUAN_TRI;
                    List<NguoiDungDonViMapping> nguoiDungDonVis = _nguoiDungDonViService.GetMapByNguoiDungId(nguoiDung.ID).OrderBy(c => c.donvi.TREE_LEVEL).ToList();
                    kho_NguoiDung.unitId = nguoiDungDonVis.Count > 0 ? nguoiDungDonVis.FirstOrDefault().donvi.DB_ID : 1;
                    kho_NguoiDungs.Add(kho_NguoiDung);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            responseApi = await PostDanhMuc(CommonDanhMuc.NguoiDung, kho_NguoiDungs, "NguoiDung");
            if (responseApi.Status)
            {
                foreach (var item in ListNguoiDungModel)
                {

                    var detail = new
                    {
                        username = item.TEN_DANG_NHAP
                    };
                    var token = _commonFactory.GetTokenKhoCSDLQG();
                    if (token == "1")
                    {
                        responseApi = new ResponseApi()
                        {
                            Status = false,
                            Message = "Lỗi truy xuất dữ liệu bên kho",
                            Date = DateTime.Now,
                            Data = 1
                        };
                        return Ok(responseApi);
                    }
                    Kho_NguoiDung kho_NguoiDung = await _gsAPIService.GetObjectGSApiKho<Kho_NguoiDung>(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.NguoiDung + CommonAction.ChiTiet, detail, token);
                    if (kho_NguoiDung != null)
                    {
                        NguoiDung nguoiDung = _nguoiDungService.GetNguoiDungByUsername(item.TEN_DANG_NHAP);
                        nguoiDung.TRANG_THAI_ID = (int)GS.Core.Domain.HeThong.NguoiDungStatusID.ok;
                        nguoiDung.DB_ID = kho_NguoiDung.id.ToString();
                        nguoiDung.TMP_MAT_KHAU = null;
                        _nguoiDungService.UpdateNguoiDung(nguoiDung);
                        _hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ thành công người dùng", nguoiDung.ToModel<NguoiDungModel>(), "NguoiDung");
                    }
                    else
                    {
                        responseApi = new ResponseApi()
                        {
                            Status = false,
                            Message = "Đồng bộ sang kho thất bại",
                            Date = DateTime.Now,
                            Data = null
                        };
                        return Ok(responseApi);
                    }
                }
            }
            return Ok(responseApi);
        }

        public async Task<IActionResult> ChangePasswordByKhoCSDL([FromBody] PasswordRequestModel request)
        {
            if (request == null)
                return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            var changePasswordRequest = new ChangePasswordRequest() { newPassword = request.newPassword, password = request.password, username = request.username };
            var responseApi = await _gsAPIService.PostObjectGSApi<ResponseApi>(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.NguoiDung + CommonAction.ChangePassword, changePasswordRequest, _commonFactory.GetTokenKhoCSDLQG());
            return Ok(responseApi);
        }

        public async Task<IActionResult> ResetPasswordByKhoCSDL([FromBody] PasswordRequestModel request)
        {
            if (request == null)
                return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            var resetPasswordRequest = new ResetPasswordRequest() { newPassword = request.newPassword, username = request.username };
            var responseApi = await _gsAPIService.PutObjectGSApi<bool>(CommonDanhMuc.ResetPassword + CommonAction.ResetPassword, resetPasswordRequest, request.TokenSSO, _config.ApiKhoCSDL.ApiUrlKhoCSDLResetPassword);
            return Ok(responseApi);
        }
        public async Task<IActionResult> TestUpDateNguoiDung()
        {
            //if (ListLoaiDonViModel == null)
            // return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            // getall Loại đơn vị
            string apiResponse = null;
            List<NguoiDung> ListNguoiDung = _nguoiDungService.GetAllNguoiDungs().Where(m => m.DB_ID == null).ToList();
            // var _ListDonViModel = ListLoaiDonVi.Select(m => m.ToModel<LoaiDonViModel>());
            var ListNguoiDungModel = ListNguoiDung.Select(m => m.ToModel<NguoiDungModel>()
            ).ToList();
            //foreach (var item in ListNguoiDungModel)
            //{
            //    using (var httpClient = new HttpClient())
            //    {
            //        string token = _commonFactory.GetTokenKhoCSDLQG();
            //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //        using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.NguoiDung + "detail?username=" + item.TEN_DANG_NHAP))
            //        {
            //            if (response.IsSuccessStatusCode)
            //            {
            //                apiResponse = await response.Content.ReadAsStringAsync();
            //                var Kho_NguoiDung = JsonConvert.DeserializeObject<Kho_NguoiDung>(apiResponse);
            //                NguoiDung nguoiDung = _nguoiDungService.GetNguoiDungByUsername(item.TEN_DANG_NHAP);
            //                nguoiDung.DB_ID = Kho_NguoiDung.id.ToString();
            //                _nguoiDungService.UpdateNguoiDung(nguoiDung);
            //            }
            //        }
            //    }
            //}
            List<Kho_NguoiDung> kho_NguoiDungs = new List<Kho_NguoiDung>();
            foreach (var m in ListNguoiDungModel)
            {
                try
                {
                    Kho_NguoiDung kho_NguoiDung = new Kho_NguoiDung();
                    kho_NguoiDung.actionType = string.IsNullOrEmpty(m.DB_ID) ? 1 : 2;
                    kho_NguoiDung.username = m.TEN_DANG_NHAP;
                    kho_NguoiDung.email = m.EMAIL;
                    // kho_NguoiDung.phoneNumber = m.MOBILE;
                    kho_NguoiDung.passwordHash = m.MAT_KHAU;
                    kho_NguoiDung.fullName = m.TEN_DAY_DU;
                    kho_NguoiDung.lockoutEnabled = m.TRANG_THAI_ID == 1 ? false : true;
                    kho_NguoiDung.isAdministrator = m.IS_QUAN_TRI;
                    List<NguoiDungDonViMapping> nguoiDungDonVis = _nguoiDungDonViService.GetMapByNguoiDungId(m.ID).OrderBy(c => c.donvi.TREE_LEVEL).ToList();
                    kho_NguoiDung.unitId = nguoiDungDonVis.Count > 0 ? nguoiDungDonVis.FirstOrDefault().donvi.DB_ID : 1;
                    kho_NguoiDungs.Add(kho_NguoiDung);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            var dataJson = kho_NguoiDungs.toStringJson();
            // kho_LoaiDonVis = kho_LoaiDonVis.Where(m => m.id > 0).ToList();
            string action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.NguoiDung + CommonAction.DongBo;
            var responseObj = await _gsAPIService.PostObjectGSApi<ResponseApi>(action, kho_NguoiDungs, _commonFactory.GetTokenKhoCSDLQG());
            if (responseObj.Status)
            {
                action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.NguoiDung + CommonAction.TatCa;
                AllModel allModel = new AllModel()
                {
                    startDate = "01-01-2020 12:00:00",
                    endDate = "12-12-2020 12:00:00"
                };
                List<Kho_NguoiDung> ListKhoNguoiDung = await _gsAPIService.GetObjectGSApi<List<Kho_NguoiDung>>(action, allModel, _commonFactory.GetTokenKhoCSDLQG());
                ListNguoiDung = new List<NguoiDung>();
                foreach (var item in ListKhoNguoiDung)
                {
                    NguoiDung nguoiDung = _nguoiDungService.GetNguoiDungByUsername(item.username);
                    nguoiDung.DB_ID = item.id.ToString();
                    ListNguoiDung.Add(nguoiDung);
                }
                _nguoiDungService.UpdateListNguoiDung(ListNguoiDung.ToList());
            }


            return Ok(_khoDanhMuc.PrepareMessageReturrn(apiResponse));
        }
        #endregion
        #region Loại tài sản
        [HttpGet]
        public async Task<IActionResult> GetAllLoaiTaiSans()
        {
            List<Kho_LoaiTaiSan> ListLoaiTaiSan_Kho = new List<Kho_LoaiTaiSan>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + "assetTypes/all"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ListLoaiTaiSan_Kho = JsonConvert.DeserializeObject<List<Kho_LoaiTaiSan>>(apiResponse);
                }
            }
            var ListLoaiTaiSanModel = _khoDanhMuc.PrepareLoaiTaiSanModel(ListLoaiTaiSan_Kho);
            return Ok(ListLoaiTaiSanModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateLoaiTaiSan([FromBody] List<LoaiTaiSanModel> ListLoaiTaiSanModel)
        {
            #region tài sản tại cơ quan tổ chức
            List<Kho_LoaiTaiSan> kho_LoaiTaiSans = new List<Kho_LoaiTaiSan>();
            foreach (var item in ListLoaiTaiSanModel)
            {
                Kho_LoaiTaiSan kho_LoaiTaiSan = new Kho_LoaiTaiSan();
                kho_LoaiTaiSan.actionType = item.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_LoaiTaiSan.name = item.TEN;
                if (item.PARENT_ID.HasValue)
                {
                    kho_LoaiTaiSan.syncParentId = item.PARENT_ID.ToString();
                    LoaiTaiSan parent = _loaiTaiSanService.GetLoaiTaiSanById(item.PARENT_ID.Value);
                    if (parent != null)
                    {
                        kho_LoaiTaiSan.parentId = (int?)parent.DB_ID;
                    }
                }
                kho_LoaiTaiSan.description = item.MO_TA;
                kho_LoaiTaiSan.syncId = item.ID.ToString();
                kho_LoaiTaiSan.code = item.MA;
                kho_LoaiTaiSan.treeId = _cheDoHaoMonService.GetCheDoHaoMonById(item.CHE_DO_HAO_MON_ID.Value).DB_ID;
                kho_LoaiTaiSan.syncTreeId = item.CHE_DO_HAO_MON_ID.ToString();

                kho_LoaiTaiSan.isActive = true;
                kho_LoaiTaiSan.id = item.DB_ID;
                kho_LoaiTaiSan.amortizationPeriod = (long?)item.HM_THOI_HAN_SU_DUNG;
                kho_LoaiTaiSan.amortizationRate = (float?)item.HM_TY_LE;
                kho_LoaiTaiSan.calculationUnit = item.DON_VI_TINH;
                kho_LoaiTaiSan.assetTypeGroupId = (int)CommonHelper.toLoaiHinhTaiSanKho(item.LOAI_HINH_TAI_SAN_ID.Value);
                kho_LoaiTaiSans.Add(kho_LoaiTaiSan);
            }
            var dataJson = kho_LoaiTaiSans.toStringJson();
            var result = await PostDanhMuc(CommonDanhMuc.LoaiTaiSan, kho_LoaiTaiSans, "LoaiTaiSan");
            if (result.Status)
            {
                foreach (var item in ListLoaiTaiSanModel)
                {
                    Kho_LoaiTaiSan kho_LoaiTaiSan = await GetIdDongBo<Kho_LoaiTaiSan>(CommonDanhMuc.LoaiTaiSan, item.ID.ToString());
                    if (kho_LoaiTaiSan != null)
                    {
                        LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(item.ID);
                        loaiTaiSan.DB_ID = (decimal)kho_LoaiTaiSan.id.Value;
                        _loaiTaiSanService.UpdateLoaiTaiSan(loaiTaiSan);
                        _hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ thành công", loaiTaiSan.ToModel<NguonGocTaiSanModel>(), "LoaiTaiSan");
                    }
                }
            }
            #endregion
            #region Tài sản toàn dân 
            //kho_LoaiTaiSans = new List<Kho_LoaiTaiSan>();
            //foreach (var item in ListLoaiTaiSanModel)
            //{
            //    Kho_LoaiTaiSan kho_LoaiTaiSan = new Kho_LoaiTaiSan();
            //    LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(item.ID);
            //    item.DB_ID = CommonHelper.Kho_GetIdDanhMuc(loaiTaiSan.DB_ID_JSON, enumCSDLQG_MA_NHOM_TAI_SAN.XacLapToanDan);
            //    kho_LoaiTaiSan.actionType = item.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
            //    kho_LoaiTaiSan.name = item.TEN;
            //    kho_LoaiTaiSan.parentId = item.DB_PARENT_ID != null ? item.DB_PARENT_ID.Value : 1;
            //    kho_LoaiTaiSan.description = item.MO_TA;
            //    kho_LoaiTaiSan.syncId = CommonHelperApi.IdLoaiTaiSanKho(item.ID, enumCSDLQG_MA_NHOM_TAI_SAN.XacLapToanDan);
            //    kho_LoaiTaiSan.code = CommonHelperApi.MaLoaiTaiSanKho(item.MA, enumCSDLQG_MA_NHOM_TAI_SAN.XacLapToanDan);
            //    kho_LoaiTaiSan.treeId = (long)item.DB_CHE_DO_HAO_MON_ID;
            //    kho_LoaiTaiSan.syncTreeId = item.CHE_DO_HAO_MON_ID.ToString();
            //    kho_LoaiTaiSan.syncParentId = item.PARENT_ID != null ? CommonHelperApi.IdLoaiTaiSanKho((long)item.PARENT_ID.Value, enumCSDLQG_MA_NHOM_TAI_SAN.XacLapToanDan) : null;
            //    kho_LoaiTaiSan.isActive = true;
            //    kho_LoaiTaiSan.id = item.DB_ID;
            //    kho_LoaiTaiSans.Add(kho_LoaiTaiSan);
            //}
            //result = await PostDanhMuc(CommonDanhMuc.LoaiTaiSan, kho_LoaiTaiSans);
            //if (result.Status)
            //{
            //    foreach (var item in ListLoaiTaiSanModel)
            //    {
            //        Kho_LoaiTaiSan kho_LoaiTaiSan = await GetIdDongBo<Kho_LoaiTaiSan>(CommonDanhMuc.LoaiTaiSan, CommonHelperApi.IdLoaiTaiSanKho(item.ID, enumCSDLQG_MA_NHOM_TAI_SAN.XacLapToanDan));
            //        if (kho_LoaiTaiSan != null)
            //        {
            //            LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(item.ID);
            //            if (string.IsNullOrEmpty(loaiTaiSan.DB_ID_JSON))
            //            {
            //                NhomTaiSanKho nhomTaiSanKho = new NhomTaiSanKho();
            //                nhomTaiSanKho.ID_NPA = (int)kho_LoaiTaiSan.id;
            //                loaiTaiSan.DB_ID_JSON = nhomTaiSanKho.toStringJson();
            //            }
            //            else
            //            {
            //                var nhomTaiSanKho = JsonConvert.DeserializeObject<NhomTaiSanKho>(loaiTaiSan.DB_ID_JSON);
            //                nhomTaiSanKho.ID_NPA = (int)kho_LoaiTaiSan.id;
            //                loaiTaiSan.DB_ID_JSON = nhomTaiSanKho.toStringJson();
            //            }
            //            _loaiTaiSanService.UpdateLoaiTaiSan(loaiTaiSan);
            //            _hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ thành công", loaiTaiSan.ToModel<NguonGocTaiSanModel>(), "LoaiTaiSan");
            //        }
            //    }

            //}
            #endregion          
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateLoaiTaiSan(int TreeLevel)
        {
            // tài sản tại cơ quan tổ chức nhà nước
            // truyền loại tài sản cha
            // tree lv = 1
            List<LoaiTaiSanModel> ListLoaiTaiSanModel = new List<LoaiTaiSanModel>();
            var _loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanByTreeLevel(TreeLevel).Where(m => m.CHE_DO_HAO_MON_ID == 23 && m.LOAI_HINH_TAI_SAN_ID != 11 && !(m.DB_ID > 0));
            ListLoaiTaiSanModel = _loaiTaiSan.Select(m =>
            new LoaiTaiSanModel()
            {
                ID = (long)m.ID,
                DB_CHE_DO_HAO_MON_ID = m.CheDoHaoMon.DB_ID,
                DB_ID = (int?)m.DB_ID,
                TEN = m.TEN,
                DB_PARENT_ID = m.PARENT_ID > 0 ? (int?)m.LoaiTaiSanCha.DB_ID : null,
                PARENT_ID = m.PARENT_ID,
                MO_TA = m.MO_TA,
                MA = m.MA,
                CHE_DO_HAO_MON_ID = m.CHE_DO_HAO_MON_ID ?? m.CHE_DO_HAO_MON_ID.Value,
                HM_TY_LE = m.HM_TY_LE,
                LOAI_HINH_TAI_SAN_ID = m.LOAI_HINH_TAI_SAN_ID
            }).ToList();
            List<Kho_LoaiTaiSan> kho_LoaiTaiSans = ListLoaiTaiSanModel.Select(m => new Kho_LoaiTaiSan()
            {
                actionType = m.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi,
                name = m.TEN,
                parentId = m.DB_PARENT_ID != null ? m.DB_PARENT_ID.Value : 2,
                description = m.MO_TA,
                syncId = m.ID.ToString(),
                code = m.MA,
                treeId = (long)m.DB_CHE_DO_HAO_MON_ID,
                syncTreeId = m.CHE_DO_HAO_MON_ID.ToString(),
                syncParentId = m.PARENT_ID == null ? null : m.PARENT_ID.ToString(),
                isActive = true,
                id = m.DB_ID,
                assetTypeGroupId = (int)CommonHelper.toLoaiHinhTaiSanKho(m.LOAI_HINH_TAI_SAN_ID.Value)
                //_khoDanhMuc.GetMapLoaiHinhTaiSanKho((int)m.LOAI_HINH_TAI_SAN_ID.Value),
            }).ToList();
            var dataJson = kho_LoaiTaiSans.toStringJson();

            var response = await _gsAPIService.PostObjectGSApi<ResponseApi>(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.LoaiTaiSan + CommonAction.DongBo, kho_LoaiTaiSans, _commonFactory.GetTokenKhoCSDLQG());
            if (response.Status)
            {
                InsertLogModel insertLogModel = new InsertLogModel()
                {
                    ResponseApi = response,
                    Data = ListLoaiTaiSanModel,
                    LoaiDuLieu = "Danh mục"
                };
                foreach (var item in ListLoaiTaiSanModel)
                {
                    Kho_LoaiTaiSan kho_LoaiTaiSan = await GetIdDongBo<Kho_LoaiTaiSan>(CommonDanhMuc.LoaiTaiSan, item.ID.ToString());
                    if (kho_LoaiTaiSan != null)
                    {
                        LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(item.ID);
                        loaiTaiSan.DB_ID = kho_LoaiTaiSan.id;
                        _loaiTaiSanService.UpdateLoaiTaiSan(loaiTaiSan);
                        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbThanhCong, "Đồng bộ thành công", item.ID, "LoaiTaiSan", item);
                    }
                }

                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Đang chờ đồng bộ", 0, "InsertLogModel", insertLogModel);
            }
            else
            {
                return Ok(MessageReturn.CreateErrorMessage("Tiếp nhận thất bại"));
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLoaiTaiSan([FromBody] LoaiTaiSanModel model)
        {
            LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(model.ID);
            var result = await DeleteDanhMuc(CommonDanhMuc.LoaiTaiSan, (long)model.DB_ID, model);
            return Ok(result);
        }
        #endregion
        #region Chế độ hao mòn
        public async Task<IActionResult> GetAllCheDoHaoMons()
        {
            List<Kho_QuocGia> ListQuocGia_Kho = new List<Kho_QuocGia>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + "countries/all"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ListQuocGia_Kho = JsonConvert.DeserializeObject<List<Kho_QuocGia>>(apiResponse);
                }
            }
            var ListQuocGiaModel = _khoDanhMuc.PrepareQuocGiaModel(ListQuocGia_Kho);
            return Ok(ListQuocGiaModel);
        }
        [NonAction]
        [HttpPost]
        public async Task<IActionResult> UpdateCheDoHaoMon([FromBody] List<CheDoHaoMonModel> ListCheDoHaoMonModel)
        {
            if (ListCheDoHaoMonModel == null)
            {
                _hoatDongService.InsertHoatDong(enumHoatDong.GuiDuLieuLoi, "Gửi đồng bộ chế độ hao mòn", new { data = ListCheDoHaoMonModel, response = new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng") }, "CheDoHaoMon");
                return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            }

            List<Kho_CheDoHaoMon> kho_CheDoHaoMons = new List<Kho_CheDoHaoMon>();
            foreach (var item in ListCheDoHaoMonModel)
            {
                CheDoHaoMon cheDoHaoMon = _cheDoHaoMonService.GetCheDoHaoMonById(item.ID);
                Kho_CheDoHaoMon kho_cheDoHaoMon = new Kho_CheDoHaoMon();
                item.DB_ID = cheDoHaoMon.DB_ID;
                kho_cheDoHaoMon.actionType = item.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_cheDoHaoMon.name = item.TEN_CHE_DO;
                kho_cheDoHaoMon.code = item.MA_CHE_DO;
                kho_cheDoHaoMon.syncId = item.ID.ToString();
                kho_cheDoHaoMon.startDate = DateTime.Parse(item.TU_NGAY);
                kho_cheDoHaoMon.endDate = DateTime.Parse(item.DEN_NGAY);
                kho_cheDoHaoMon.id = item.DB_ID;
                kho_CheDoHaoMons.Add(kho_cheDoHaoMon);
            }
            var result = await PostDanhMuc(CommonDanhMuc.CayPhanLoaiTaiSan, kho_CheDoHaoMons, "CheDoHaoMon");
            if (result.Status)
            {
                foreach (var item in ListCheDoHaoMonModel)
                {
                    var kho_CheDoHaoMon = await GetIdDongBo<Kho_CheDoHaoMon>(CommonDanhMuc.CayPhanLoaiTaiSan, item.ID.toStringJson());
                    if (kho_CheDoHaoMon != null)
                    {
                        CheDoHaoMon cheDoHaoMon = _cheDoHaoMonService.GetCheDoHaoMonById(item.ID);
                        cheDoHaoMon.DB_ID = kho_CheDoHaoMon.id;
                        _cheDoHaoMonService.UpdateCheDoHaoMon(cheDoHaoMon);
                    }

                }
            }
            string apiResponse = null;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(kho_CheDoHaoMons), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(CommonDanhMuc.RequestDanhMuc + "assetTypeTrees/sync", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
                var responseModel = JsonConvert.DeserializeObject<ResponseApi>(apiResponse);
                if (responseModel.Status == true)
                {
                    await Task.Delay(5000);
                    content = new StringContent(JsonConvert.SerializeObject(kho_CheDoHaoMons), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(CommonDanhMuc.RequestDanhMuc + "assetTypeTrees/sync", content))
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            _hoatDongService.InsertHoatDong(enumHoatDong.DaGuiDuLieu, "Gửi đồng bộ chế độ hao mòn", new { data = kho_CheDoHaoMons, response = _khoDanhMuc.PrepareMessageReturrn(apiResponse) }, "CheDoHaoMon");
            return Ok(_khoDanhMuc.PrepareMessageReturrn(apiResponse));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCheDoHaoMon([FromBody] CheDoHaoMonModel model)
        {
            CheDoHaoMon cheDoHaoMon = _cheDoHaoMonService.GetCheDoHaoMonById(model.ID);
            var result = await DeleteDanhMuc(CommonDanhMuc.CayPhanLoaiTaiSan, (long)model.ID, model);
            return Ok(result);
        }
        #endregion
        //public async Task<IActionResult> GetToken()
        //{
        //    string apiResponse;
        //    Authen authen = new Authen()
        //    {
        //        Username = "quyetpt",
        //        Password = "Ab123456@"
        //    };

        //    using (var httpClient = new HttpClient(""))
        //    {
        //        //  var accessToken = await HttpContext.GetTokenAsync("access_token");
        //        var login = new Dictionary<string, string>
        //        {
        //            {"grant_type", "password"},
        //            {"Username", "quyetpt"},
        //            {"Password", "Ab123456@"},
        //        };

        //        StringContent content = new StringContent(JsonConvert.SerializeObject(authen), Encoding.UTF8, "application/json");
        //        using (var response = await httpClient.GetAsync(CommonDanhMuc.RequestDanhMuc + "unitType/sync",))
        //        {
        //            apiResponse = await response.Content.ReadAsStringAsync();
        //        }
        //    }
        //    return Ok(apiResponse);
        //}      
        /// <summary>
        /// Get string api respond
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="method">1: post; 2: get</param>
        /// <returns></returns>
        public async Task<string> GetApiRespond(string url, object model, int method)
        {
            string apiResponse = null;
            string token = _commonFactory.GetTokenKhoCSDLQG();
            var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            if (method == 1)
            {
                using (var response = await httpClient.PostAsync(url, content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            if (method == 2)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }

            return apiResponse;
        }
        #region Đơn vị bộ phận
        [HttpGet]
        public async Task<IActionResult> TestUpdateDonViBoPhan(int TreeLevel)
        {
            var ListDonViBoPhan = _donViBoPhanService.GetDonViBoPhans(TreeLevel: TreeLevel).Where(m => !(m.DB_ID > 0));

            List<Kho_DonViSuDung> kho_DonViSuDungs = new List<Kho_DonViSuDung>();
            foreach (var item in ListDonViBoPhan)
            {
                Kho_DonViSuDung kho_DonViSuDung = new Kho_DonViSuDung();
                kho_DonViSuDung.actionType = item.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_DonViSuDung.name = item.TEN;
                kho_DonViSuDung.code = item.MA;
                kho_DonViSuDung.syncId = item.ID.ToString();
                kho_DonViSuDung.id = (long?)item.DB_ID;

                if (item.PARENT_ID > 0)
                {
                    kho_DonViSuDung.parentId = _donViService.GetDonViById(item.PARENT_ID.Value).DB_ID;
                    kho_DonViSuDung.syncParentId = item.DonViBoPhanCha.ID.ToString();
                }
                kho_DonViSuDung.phoneNumber = item.DIEN_THOAI;
                kho_DonViSuDung.fax = item.FAX;
                if (!(item.DON_VI_ID > 0))
                {
                    continue;
                }
                DonVi donVi = _donViService.GetDonViById(item.DON_VI_ID.Value);
                if (donVi != null && donVi.DB_ID > 0)
                {
                    kho_DonViSuDung.unitId = donVi.DB_ID;
                }
                else
                {
                    continue;
                }
                kho_DonViSuDung.id = (int?)item.DB_ID;
                kho_DonViSuDung.isActive = true;
                kho_DonViSuDung.address = item.DIA_CHI;
                kho_DonViSuDungs.Add(kho_DonViSuDung);
            }
            var result = await PostDanhMuc(CommonDanhMuc.DonViBoPhan, kho_DonViSuDungs, "ChucDanh");
            //if (result.Status)
            //{
            //    foreach (var item in ListDonViBoPhan)
            //    {
            //        var kho_DonViSuDung = await GetIdDongBo<Kho_DonViSuDung>(CommonDanhMuc.DonViBoPhan, item.ID.toStringJson());
            //        if (kho_DonViSuDung != null)
            //        {

            //            item.DB_ID = (int?)kho_DonViSuDung.id;
            //            _donViBoPhanService.UpdateDonViBoPhan(item);
            //        }
            //    }
            //}
            return Ok(result);
        }
        public async Task<IActionResult> UpdateDonViBoPhanDaDongBo(int TreeLevel)
        {
            List<DonViBoPhan> donViBoPhans = _donViBoPhanService.GetDonViBoPhans(TreeLevel: TreeLevel).Where(m => !(m.DB_ID > 0)).ToList();
            List<DonViBoPhan> ListErr = new List<DonViBoPhan>();
            List<DonViBoPhan> ListSuccess = new List<DonViBoPhan>();
            List<Kho_DonViSuDung> kho_DonViSuDungs = new List<Kho_DonViSuDung>();

            // get đơn vị đã đồng bộ sang kho
            string action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.DonViBoPhan + CommonAction.TatCa;
            AllModel allModel = new AllModel()
            {
                startDate = "11-01-2021 12:00:00",
                endDate = "14-03-2021 12:00:00"
            };
            var responseApi = await _gsAPIService.GetListGSApi<Kho_DonViSuDung>(action, allModel, _commonFactory.GetTokenKhoCSDLQG());
            if (responseApi != null)
            {
                foreach (var item in donViBoPhans)
                {
                    Kho_DonViSuDung kho_DonViSuDung = responseApi.Where(m => m.syncId == item.ID.ToString()).FirstOrDefault();
                    if (kho_DonViSuDung != null)
                    {
                        item.DB_ID = (int?)kho_DonViSuDung.id;
                        _donViBoPhanService.UpdateDonViBoPhan(item);
                        ListSuccess.Add(item);
                    }
                    else
                    {
                        ListErr.Add(item);
                    }
                }
            }

            return Ok(ListErr);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDonViBoPhan([FromBody] List<DonViBoPhanModel> ListModel)
        {
            List<Kho_DonViSuDung> kho_DonViSuDungs = new List<Kho_DonViSuDung>();
            foreach (var m in ListModel)
            {
                //thêm tạm GUID vào để đồng bộ của bên tsnn
                var kieudongbo = (int)enumLoaiDongBo.ThemMoi;
                if (m.DB_ID > 0 || m.DB_GUID != null)
                {
                    kieudongbo = (int)enumLoaiDongBo.CapNhat;
                }
                Kho_DonViSuDung kho_DonViSuDung = new Kho_DonViSuDung();
                kho_DonViSuDung.actionType = kieudongbo;
                kho_DonViSuDung.name = m.TEN;
                kho_DonViSuDung.code = m.MA;
                kho_DonViSuDung.syncId = m.ID.ToString();
                kho_DonViSuDung.syncParentId = m.PARENT_ID > 0 ? m.PARENT_ID.ToString() : null;
                if (m.PARENT_ID > 0)
                {
                    kho_DonViSuDung.parentId = _donViService.GetDonViById(m.PARENT_ID.Value).DB_ID;
                }
                kho_DonViSuDung.phoneNumber = m.DIEN_THOAI;
                kho_DonViSuDung.fax = m.FAX;
                if (!(m.DON_VI_ID > 0))
                {
                    continue;
                }
                DonVi donVi = _donViService.GetDonViById(m.DON_VI_ID.Value);
                if (donVi.DB_ID > 0)
                {
                    kho_DonViSuDung.unitId = donVi.DB_ID;
                }
                else
                {
                    continue;
                }
                kho_DonViSuDung.id = (int?)m.DB_ID;
                kho_DonViSuDung.guid = m.DB_GUID;
                kho_DonViSuDung.isActive = true;
                kho_DonViSuDung.address = m.DIA_CHI;
                kho_DonViSuDungs.Add(kho_DonViSuDung);
            }
            var dataJson = kho_DonViSuDungs.toStringJson();
            // kho_LoaiDonVis = kho_LoaiDonVis.Where(m => m.id > 0).ToList();
            string Action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.DonViBoPhan;
            var responseApi = await PostDanhMuc(Action, kho_DonViSuDungs, "DonViBoPhan");
            if (responseApi != null && responseApi.Status)
            {
                foreach (var item in ListModel)
                {
                    DetaiModel detaiModel = new DetaiModel()
                    {
                        syncId = item.ID.ToString()
                    };
                    Action = CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.DonViBoPhan + CommonAction.ChiTiet;
                    Kho_DonViSuDung kho_DonViSuDung = await _gsAPIService.GetObjectGSApi<Kho_DonViSuDung>(Action, detaiModel, _commonFactory.GetTokenKhoCSDLQG());
                    if (kho_DonViSuDung != null)
                    {
                        DonViBoPhan donViBoPhan = _donViBoPhanService.GetDonViBoPhanById(item.ID);
                        if (donViBoPhan != null)
                        {
                            donViBoPhan.DB_ID = kho_DonViSuDung.id;
                            _donViBoPhanService.UpdateDonViBoPhan(donViBoPhan);
                        }
                    }
                }
            }
            return Ok(responseApi);
        }
        #endregion
        #region Danh mục dự án
        [HttpPost]
        public async Task<IActionResult> UpdateDuAn([FromBody] List<DuAnModel> ListModel)
        {
            if (ListModel == null)
                return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            List<Kho_DuAn> kho_DuAns = new List<Kho_DuAn>();
            foreach (var item in ListModel)
            {
                DuAn duAn = _duAnService.GetDuAnById(item.ID);
                Kho_DuAn kho_DuAn = new Kho_DuAn();
                kho_DuAn.id = (int?)item.DB_ID;
                kho_DuAn.actionType = item.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_DuAn.name = item.TEN;
                kho_DuAn.code = item.MA;
                kho_DuAn.syncId = item.ID.ToString();
                kho_DuAn.unitId = _donViService.GetDonViById(item.DON_VI_ID.Value).DB_ID;
                kho_DuAn.decisionNumber = item.SO_QUYET_DINH_PHE_DUYET;
                kho_DuAn.expense = (long?)item.TONG_KINH_PHI;
                kho_DuAn.expenseStateBudget = (long?)item.NGUON_NS;
                kho_DuAn.expenseOda = (long?)item.NGUON_ODA;
                kho_DuAn.expenseAid = (long?)item.NGUON_VIEN_TRO;
                kho_DuAn.expenseOther = (long?)item.NGUON_KHAC;
                kho_DuAn.startDate = CommonHelper.toDateKhoString(item.NGAY_BAT_DAU);
                kho_DuAn.endDate = CommonHelper.toDateKhoString(item.NGAY_KET_THUC);
                kho_DuAn.investor = item.CHU_DAU_TU;
                kho_DuAn.location = item.DIA_DIEM;
                kho_DuAn.syncDate = CommonHelper.toDateKhoString(DateTime.Now);
                kho_DuAn.description = item.GHI_CHU;
                kho_DuAns.Add(kho_DuAn);
            }
            var result = await PostDanhMuc(CommonDanhMuc.DuAn, kho_DuAns, "DuAn");
            if (result != null && result.Status)
            {
                foreach (var item in ListModel)
                {
                    var kho_DuAn = await GetIdDongBo<Kho_DuAn>(CommonDanhMuc.DuAn, item.ID.toStringJson());
                    if (kho_DuAn != null)
                    {
                        DuAn duAn = _duAnService.GetDuAnById(item.ID);
                        duAn.DB_ID = kho_DuAn.id;
                        _duAnService.UpdateDuAn(duAn);
                    }
                }
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDuAn([FromBody] CheDoHaoMonModel model)
        {
            DuAn duAn = _duAnService.GetDuAnById(model.ID);
            var result = await DeleteDanhMuc(CommonDanhMuc.DuAn, (long)model.ID, model);
            return Ok(result);
        }
        #endregion
        #region Nhãn hiệu
        [HttpGet]
        public async Task<IActionResult> TestUpdateNhanHieu()
        {
            var NhanHieu = _nhanXeService.GetAllNhanXes().Where(m => m.DB_ID == null).Select(m => new NhanXeModel()
            {
                ID = (long)m.ID,
                TEN = m.TEN,
                MA = m.MA,
                DB_ID = m.DB_ID
            });

            List<Kho_NhanXe> kho_NhanXes = new List<Kho_NhanXe>();
            foreach (var item in NhanHieu)
            {
                NhanXe nhanXe = _nhanXeService.GetNhanXeById(item.ID);
                Kho_NhanXe kho_NhanXe = new Kho_NhanXe();
                item.DB_ID = nhanXe.DB_ID;
                kho_NhanXe.actionType = item.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_NhanXe.name = item.TEN;
                kho_NhanXe.code = item.MA;
                kho_NhanXe.syncId = item.ID.ToString();
                kho_NhanXe.id = item.DB_ID;
                kho_NhanXes.Add(kho_NhanXe);
            }
            var result = await PostDanhMuc(CommonDanhMuc.NhanHieu, kho_NhanXes, "NhanXe");
            if (result != null && result.Status)
            {
                foreach (var item in NhanHieu)
                {
                    var kho_NhanXe = await GetIdDongBo<Kho_NhanXe>(CommonDanhMuc.NhanHieu, item.ID.toStringJson());
                    if (kho_NhanXe != null)
                    {
                        NhanXe nhanXe = _nhanXeService.GetNhanXeById(item.ID);
                        nhanXe.DB_ID = (int?)kho_NhanXe.id;
                        item.DB_ID = nhanXe.DB_ID;
                        _nhanXeService.UpdateNhanXe(nhanXe);
                    }
                }
            }
            return Ok(NhanHieu);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateNhanXe([FromBody] List<NhanXeModel> ListNhanXeModel)
        {
            if (ListNhanXeModel == null)
            {
                _hoatDongService.InsertHoatDong(enumHoatDong.GuiDuLieuLoi, "Gửi đồng bộ Nhãn xe", new { data = ListNhanXeModel, response = new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng") }, "CheDoHaoMon");
                return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            }
            List<Kho_NhanXe> kho_NhanXes = new List<Kho_NhanXe>();
            foreach (var item in ListNhanXeModel)
            {
                NhanXe nhanXe = _nhanXeService.GetNhanXeById(item.ID);
                Kho_NhanXe kho_NhanXe = new Kho_NhanXe();
                item.DB_ID = nhanXe.DB_ID;
                kho_NhanXe.actionType = item.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_NhanXe.name = item.TEN;
                kho_NhanXe.code = item.MA;
                kho_NhanXe.syncId = item.ID.ToString();
                kho_NhanXe.id = item.DB_ID;
                kho_NhanXes.Add(kho_NhanXe);
            }
            var result = await PostDanhMuc(CommonDanhMuc.NhanHieu, kho_NhanXes, "NhanXe");
            if (result.Status)
            {
                foreach (var item in ListNhanXeModel)
                {
                    var kho_NhanXe = await GetIdDongBo<Kho_NhanXe>(CommonDanhMuc.NhanHieu, item.ID.toStringJson());
                    if (kho_NhanXe != null)
                    {
                        NhanXe nhanXe = _nhanXeService.GetNhanXeById(item.ID);
                        nhanXe.DB_ID = (int?)kho_NhanXe.id;
                        _nhanXeService.UpdateNhanXe(nhanXe);
                    }
                }
            }
            return Ok(result);
        }
        //[HttpPost]
        //public async Task<IActionResult> DeleteCheDoHaoMon([FromBody] CheDoHaoMonModel model)
        //{
        //    CheDoHaoMon cheDoHaoMon = _cheDoHaoMonService.GetCheDoHaoMonById(model.ID);
        //    var result = await DeleteDanhMuc(CommonDanhMuc.CayPhanLoaiTaiSan, (long)model.ID, model);
        //    return Ok(result);
        //}
        #endregion
        #region Dòng sản phẩm
        [HttpGet]
        public async Task<IActionResult> TestUpdateDongSanPham()
        {
            var DongSanPham = _dongXeService.GetAllDongXes().Select(m => new DongXeModel()
            {
                ID = (long)m.ID,
                TEN = m.TEN,
                MA = m.MA,
                DB_ID = m.DB_ID,
                NHAN_XE_ID = (decimal?)m.NhanXe.DB_ID
            });
            List<Kho_DongXe> kho_DongXes = new List<Kho_DongXe>();
            foreach (var item in DongSanPham)
            {
                DongXe dongXe = _dongXeService.GetDongXeById(item.ID);
                Kho_DongXe kho_DongXe = new Kho_DongXe();
                item.DB_ID = dongXe.DB_ID;
                kho_DongXe.actionType = item.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_DongXe.name = item.TEN;
                kho_DongXe.code = item.MA;
                kho_DongXe.syncId = item.ID.ToString();
                kho_DongXe.brandId = (long?)item.NHAN_XE_ID;
                kho_DongXe.id = item.DB_ID;
                kho_DongXes.Add(kho_DongXe);
            }
            var result = await PostDanhMuc(CommonDanhMuc.DongSanPham, kho_DongXes, "DongXe");
            if (result.Status)
            {
                foreach (var item in DongSanPham)
                {
                    var kho_DongXe = await GetIdDongBo<Kho_DongXe>(CommonDanhMuc.DongSanPham, item.ID.toStringJson());
                    if (kho_DongXe != null)
                    {
                        DongXe dongXe = _dongXeService.GetDongXeById(item.ID);
                        dongXe.DB_ID = (int?)kho_DongXe.id;
                        item.DB_ID = dongXe.DB_ID;
                        _dongXeService.UpdateDongXe(dongXe);
                    }
                }
            }
            return Ok(DongSanPham);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDongXe([FromBody] List<DongXeModel> ListDongXeModel)
        {
            if (ListDongXeModel == null)
            {
                _hoatDongService.InsertHoatDong(enumHoatDong.GuiDuLieuLoi, "Gửi đồng bộ Nhãn xe", new { data = ListDongXeModel, response = new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng") }, "CheDoHaoMon");
                return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            }

            List<Kho_DongXe> kho_DongXes = new List<Kho_DongXe>();
            foreach (var item in ListDongXeModel)
            {
                DongXe dongXe = _dongXeService.GetDongXeById(item.ID);
                Kho_DongXe kho_DongXe = new Kho_DongXe();
                item.DB_ID = dongXe.DB_ID;
                kho_DongXe.actionType = item.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_DongXe.name = item.TEN;
                kho_DongXe.code = item.MA;
                kho_DongXe.syncId = item.ID.ToString();
                kho_DongXe.id = item.DB_ID;
                kho_DongXes.Add(kho_DongXe);
            }
            var result = await PostDanhMuc(CommonDanhMuc.DongSanPham, kho_DongXes, "DongXe");
            if (result.Status)
            {
                foreach (var item in ListDongXeModel)
                {
                    var kho_NhanXe = await GetIdDongBo<Kho_DongXe>(CommonDanhMuc.DongSanPham, item.ID.toStringJson());
                    if (kho_NhanXe != null)
                    {
                        NhanXe nhanXe = _nhanXeService.GetNhanXeById(item.ID);
                        nhanXe.DB_ID = (int?)kho_NhanXe.id;
                        _nhanXeService.UpdateNhanXe(nhanXe);
                    }
                }
            }
            return Ok(result);
        }
        #endregion
        #region Chức danh
        [HttpGet]
        public async Task<IActionResult> TestUpdateChucDanh()
        {
            var ListChucDanhModel = _chucDanhService.GetAllChucDanhs().Where(x => x.DB_ID == null).Select(m => m.ToModel<ChucDanhModel>());
            List<Kho_ChucDanh> chucDanhs = new List<Kho_ChucDanh>();
            foreach (var item in ListChucDanhModel)
            {
                ChucDanh chucDanh = _chucDanhService.GetChucDanhById(item.ID);
                Kho_ChucDanh kho_ChucDanh = new Kho_ChucDanh();
                item.DB_ID = chucDanh.DB_ID;
                kho_ChucDanh.actionType = item.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_ChucDanh.name = item.TEN_CHUC_DANH;
                kho_ChucDanh.code = item.MA_CHUC_DANH;
                kho_ChucDanh.syncId = item.ID.ToString();
                kho_ChucDanh.id = (long?)item.DB_ID;
                kho_ChucDanh.description = item.MO_TA;
                kho_ChucDanh.administrativeLevel = item.KHOI_DON_VI_ID > 0 ? (int?)item.KHOI_DON_VI_ID : null;
                chucDanhs.Add(kho_ChucDanh);
            }
            var result = await PostDanhMuc(CommonDanhMuc.ChucDanh, chucDanhs, "ChucDanh");
            if (result.Status)
            {
                foreach (var item in ListChucDanhModel)
                {
                    var kho_NhanXe = await GetIdDongBo<Kho_ChucDanh>(CommonDanhMuc.ChucDanh, item.ID.toStringJson());
                    if (kho_NhanXe != null)
                    {
                        ChucDanh chucDanh = _chucDanhService.GetChucDanhById(item.ID);
                        chucDanh.DB_ID = (int?)kho_NhanXe.id;
                        _chucDanhService.UpdateChucDanh(chucDanh);
                    }
                }
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateChucDanh([FromBody] List<ChucDanhModel> ListChucDanhModel)
        {
            if (ListChucDanhModel == null)
            {
                _hoatDongService.InsertHoatDong(enumHoatDong.GuiDuLieuLoi, "Gửi đồng bộ Chức danh", new { data = ListChucDanhModel, response = new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng") }, "ChucDanh");
                return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            }

            List<Kho_ChucDanh> chucDanhs = new List<Kho_ChucDanh>();
            foreach (var item in ListChucDanhModel)
            {
                ChucDanh chucDanh = _chucDanhService.GetChucDanhById(item.ID);
                Kho_ChucDanh kho_ChucDanh = new Kho_ChucDanh();
                item.DB_ID = chucDanh.DB_ID;
                kho_ChucDanh.actionType = item.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_ChucDanh.name = item.TEN_CHUC_DANH;
                kho_ChucDanh.code = item.MA_CHUC_DANH;
                kho_ChucDanh.syncId = item.ID.ToString();
                kho_ChucDanh.id = (long?)item.DB_ID;
                kho_ChucDanh.description = item.MO_TA;
                kho_ChucDanh.administrativeLevel = item.KHOI_DON_VI_ID > 0 ? (int?)item.KHOI_DON_VI_ID : null;
                chucDanhs.Add(kho_ChucDanh);
            }
            var result = await PostDanhMuc(CommonDanhMuc.ChucDanh, chucDanhs, "ChucDanh");
            if (result.Status)
            {
                foreach (var item in ListChucDanhModel)
                {
                    var kho_NhanXe = await GetIdDongBo<Kho_DongXe>(CommonDanhMuc.ChucDanh, item.ID.toStringJson());
                    if (kho_NhanXe != null)
                    {
                        ChucDanh chucDanh = _chucDanhService.GetChucDanhById(item.ID);
                        chucDanh.DB_ID = (int?)kho_NhanXe.id;
                        _chucDanhService.UpdateChucDanh(chucDanh);
                    }
                }
            }
            return Ok(result);
        }
        #endregion
        #region Hình thức mua sắm
        [HttpGet]
        public async Task<IActionResult> TestUpdateHinhThucMuaSam()
        {
            var LsthinhThucMuaSam = _hinhThucMuaSamService.GetHinhThucMuaSams().Select(m => new HinhThucMuaSamModel()
            {
                ID = (long)m.ID,
                TEN = m.TEN,
                MA = m.MA,
                DB_ID = m.DB_ID,
            });
            List<Kho_HinhThucMuaSam> kho_HinhThucMuaSams = new List<Kho_HinhThucMuaSam>();
            foreach (var item in LsthinhThucMuaSam)
            {
                HinhThucMuaSam hinhThucMuaSam = _hinhThucMuaSamService.GetHinhThucMuaSamById(item.ID);
                Kho_HinhThucMuaSam kho_HinhThucMuaSam = new Kho_HinhThucMuaSam();
                item.DB_ID = hinhThucMuaSam.DB_ID;
                kho_HinhThucMuaSam.actionType = item.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_HinhThucMuaSam.name = item.TEN;
                kho_HinhThucMuaSam.code = item.MA;
                kho_HinhThucMuaSam.syncId = item.ID.ToString();
                kho_HinhThucMuaSams.Add(kho_HinhThucMuaSam);
            }
            var result = await PostDanhMuc(CommonDanhMuc.HinhThucMuaSam, kho_HinhThucMuaSams, "HinhThucMuaSam");
            if (result.Status)
            {
                foreach (var item in LsthinhThucMuaSam)
                {
                    var kho = await GetIdDongBo<Kho_HinhThucMuaSam>(CommonDanhMuc.DongSanPham, item.ID.toStringJson());
                    if (kho != null)
                    {
                        HinhThucMuaSam hinh = _hinhThucMuaSamService.GetHinhThucMuaSamById(item.ID);
                        hinh.DB_ID = (int?)kho.id;
                        item.DB_ID = hinh.DB_ID;
                        _hinhThucMuaSamService.UpdateHinhThucMuaSam(hinh);
                    }
                }
            }
            return Ok(LsthinhThucMuaSam);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateHinhThucMuaSam([FromBody] List<HinhThucMuaSamModel> ListHinhThucMuaSamModel)
        {
            if (ListHinhThucMuaSamModel == null)
            {
                _hoatDongService.InsertHoatDong(enumHoatDong.GuiDuLieuLoi, "Gửi đồng bộ Hình thức mua sắm", new { data = ListHinhThucMuaSamModel, response = new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng") }, "HinhThucMuaSam");
                return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
            }

            List<Kho_HinhThucMuaSam> kho_HinhThucMuaSams = new List<Kho_HinhThucMuaSam>();
            foreach (var item in ListHinhThucMuaSamModel)
            {
                HinhThucMuaSam hinhThucMuaSam = _hinhThucMuaSamService.GetHinhThucMuaSamById(item.ID);
                Kho_HinhThucMuaSam kho_HinhThucMuaSam = new Kho_HinhThucMuaSam();
                item.DB_ID = hinhThucMuaSam.DB_ID;
                kho_HinhThucMuaSam.actionType = item.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
                kho_HinhThucMuaSam.name = item.TEN;
                kho_HinhThucMuaSam.code = item.MA;
                kho_HinhThucMuaSam.syncId = item.ID.ToString();
                kho_HinhThucMuaSam.id = (long?)item.DB_ID;
                kho_HinhThucMuaSams.Add(kho_HinhThucMuaSam);
            }
            var result = await PostDanhMuc(CommonDanhMuc.HinhThucMuaSam, kho_HinhThucMuaSams, "HinhThucMuaSam");
            if (result.Status)
            {
                foreach (var item in ListHinhThucMuaSamModel)
                {
                    var kho_HinhThucMuaSam = await GetIdDongBo<Kho_HinhThucMuaSam>(CommonDanhMuc.HinhThucMuaSam, item.ID.toStringJson());
                    if (kho_HinhThucMuaSam != null)
                    {
                        HinhThucMuaSam hinhThucMuaSam = _hinhThucMuaSamService.GetHinhThucMuaSamById(item.ID);
                        hinhThucMuaSam.DB_ID = (int?)kho_HinhThucMuaSam.id;
                        _hinhThucMuaSamService.UpdateHinhThucMuaSam(hinhThucMuaSam);
                    }
                }
            }
            return Ok(result);
        }
        #endregion
        async Task<ResponseApi> PostDanhMuc(string action, Object obj, string TenDanhMuc)
        {
            string _action = CommonDanhMuc.RequestDanhMuc + action + CommonAction.DongBo;
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, $"Gửi dữ liệu Đồng bộ Danh mục {TenDanhMuc}", 0, TenDanhMuc, obj);
            ResponseApi response = new ResponseApi();
            var token = _commonFactory.GetTokenKhoCSDLQG();
            if (token == "1")
            {
                response = new ResponseApi()
                {
                    Status = false,
                    Message = "Lỗi truy xuất dữ liệu bên kho",
                    Date = DateTime.Now,
                    Data = 1
                };
                return response;
            }
            response = await _gsAPIService.PostObjectGSApi<ResponseApi>(_action, obj, token);
            InsertLogModel model = new InsertLogModel()
            {
                ResponseApi = response,
                Data = obj,
                LoaiDuLieu = "Danh mục"
            };
            if (response != null && response.Status)
            {
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DaGuiDuLieu, "Đã Gửi dữ liệu Đồng bộ danh mục", 0, TenDanhMuc, model);
                return response;
            }
            else
            {
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.GuiDuLieuLoi, "Đã Gửi dữ liệu Đồng bộ danh mục bị lỗi", 0, TenDanhMuc, model);
                return response;
            }
        }
        async Task<T> GetIdDongBo<T>(string urlDanhMuc, string ID) where T : class
        {
            DetaiModel detaiModel = new DetaiModel()
            {
                syncId = ID
            };
            string Action = CommonDanhMuc.RequestDanhMuc + urlDanhMuc + CommonAction.ChiTiet;
            T result = await _gsAPIService.GetObjectGSApi<T>(Action, detaiModel, _commonFactory.GetTokenKhoCSDLQG());
            return result;
        }
        async Task<ResponseApi> DeleteDanhMuc(string UrlDanhMuc, long ID, object obj)
        {
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, "Gửi dữ liệu Đồng bộ xoá danh mục", ID, obj.GetType().Name, obj);
            List<DeleteDanhMucKho> ListDelete = new List<DeleteDanhMucKho>() { new DeleteDanhMucKho()
            {
                id= ID
            }
            };
            ResponseApi response = await _gsAPIService.DeleteObjectGSApi<ResponseApi>(CommonDanhMuc.RequestDanhMuc + UrlDanhMuc + CommonAction.Xoa, ListDelete, _commonFactory.GetTokenKhoCSDLQG());
            InsertLogModel model = new InsertLogModel()
            {
                ResponseApi = response,
                Data = obj,
                LoaiDuLieu = "Danh mục"
            };
            if (response != null && response.Status)
            {
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DaGuiDuLieu, "Đã Gửi dữ liệu Đồng bộ xoá danh mục", ID, obj.GetType().Name, model);
                return response;
            }
            else
            {
                response = new ResponseApi()
                {
                    Status = false
                };
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.GuiDuLieuLoi, "Gửi dữ liệu đồng bộ danh mục bị lỗi", 0, obj.GetType().Name, model);
                return response;
            }
        }
        //ghi file
        void GhiFile(object obj, string name)
        {
            string fileName = "data_" + obj.GetType().Name + ".json";
            string _pathStore = DateTime.Now.ToPathFolderStore();
            _pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
            _fileProvider.CreateDirectory(_pathStore);
            var fullpath = _fileProvider.Combine(_pathStore, fileName);
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            serializerSettings.DateFormatString = "dd/MM/yyyy";
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            string dataJson = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            _fileProvider.WriteAllText(fullpath, dataJson, Encoding.UTF8);
        }
        [HttpPost]
        public async Task<IActionResult> CleanDanhMuc(string danhMuc, bool DB_ID_NULL = true)
        {
            _logger.Information($"CleanDanhMuc {danhMuc} started at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            if (danhMuc == "DonVi")
            {
                List<DonVi> listDanhMuc = new List<DonVi>();
                if (DB_ID_NULL)
                    listDanhMuc = _donViService.GetAllDonVis().Where(x => x.DB_ID == null).ToList();
                else
                    listDanhMuc = _donViService.GetAllDonVis().Where(x => x.DB_ID != null).ToList();
                Kho_DonVi kho_DanhMuc = new Kho_DonVi();
                if (listDanhMuc != null && listDanhMuc.Count > 0)
                {
                    foreach (DonVi item in listDanhMuc)
                    {
                        try
                        {
                            kho_DanhMuc = null;
                            kho_DanhMuc = await GetIdDongBo<Kho_DonVi>(CommonDanhMuc.DonVi, item.ID.ToString());
                            if (kho_DanhMuc != null)
                            {
                                if (item.DB_ID != kho_DanhMuc.id)
                                {
                                    item.DB_ID = (int?)kho_DanhMuc.id;
                                    _donViService.UpdateDonVi(item);
                                }
                            }
                            else if (item.DB_ID != null)
                            {
                                item.DB_ID = null;
                                _donViService.UpdateDonVi(item);
                            }

                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
                return OkSuccessMessage("done");
            }
            else if (danhMuc == "DiaBan")
            {
                List<DiaBan> listDanhMuc = new List<DiaBan>();
                if (DB_ID_NULL)
                    listDanhMuc = _diaBanService.GetDiaBans().Where(x => x.DB_ID == null).ToList();
                else
                    listDanhMuc = _diaBanService.GetDiaBans().Where(x => x.DB_ID != null).ToList();
                Kho_DiaBan kho_DanhMuc = new Kho_DiaBan();
                if (listDanhMuc != null && listDanhMuc.Count > 0)
                {
                    foreach (DiaBan item in listDanhMuc)
                    {
                        try
                        {
                            kho_DanhMuc = null;
                            kho_DanhMuc = await GetIdDongBo<Kho_DiaBan>(CommonDanhMuc.TinhThanh_DiaBan, item.ID.ToString());
                            if (kho_DanhMuc != null)
                            {
                                if (item.DB_ID != kho_DanhMuc.id)
                                {
                                    item.DB_ID = (int?)kho_DanhMuc.id;
                                    _diaBanService.UpdateDiaBan(item);
                                }
                            }
                            else if (item.DB_ID != null)
                            {
                                item.DB_ID = null;
                                _diaBanService.UpdateDiaBan(item);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                return OkSuccessMessage("done");
            }
            else if (danhMuc == "DuAn")
            {
                List<DuAn> listDanhMuc = new List<DuAn>();
                if (DB_ID_NULL)
                    listDanhMuc = _duAnService.GetAllDuAns().Where(x => x.DB_ID == null).ToList();
                else
                    listDanhMuc = _duAnService.GetAllDuAns().Where(x => x.DB_ID != null).ToList();
                Kho_DuAn kho_DanhMuc = new Kho_DuAn();
                if (listDanhMuc != null && listDanhMuc.Count > 0)
                {
                    foreach (DuAn item in listDanhMuc)
                    {
                        try
                        {
                            kho_DanhMuc = null;
                            kho_DanhMuc = await GetIdDongBo<Kho_DuAn>(CommonDanhMuc.DuAn, item.ID.ToString());
                            if (kho_DanhMuc != null)
                            {
                                if (item.DB_ID != kho_DanhMuc.id)
                                {
                                    item.DB_ID = kho_DanhMuc.id;
                                    _duAnService.UpdateDuAn(item);
                                }
                            }
                            else if (item.DB_ID != null)
                            {
                                item.DB_ID = null;
                                _duAnService.UpdateDuAn(item);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                return OkSuccessMessage("done");
            }
            else if (danhMuc == "DonViBoPhan")
            {
                List<DonViBoPhan> listDanhMuc = new List<DonViBoPhan>();
                if (DB_ID_NULL)
                    listDanhMuc = _donViBoPhanService.GetDonViBoPhans().Where(x => x.DB_ID == null).ToList();
                else
                    listDanhMuc = _donViBoPhanService.GetDonViBoPhans().Where(x => x.DB_ID != null).ToList();
                Kho_DonViSuDung kho_DanhMuc = new Kho_DonViSuDung();
                if (listDanhMuc != null && listDanhMuc.Count > 0)
                {
                    DonViBoPhan item = new DonViBoPhan();
                    for (int i = 0; i < listDanhMuc.Count; i++)
                    {
                        try
                        {
                            item = listDanhMuc[i];
                            kho_DanhMuc = null;
                            kho_DanhMuc = await GetIdDongBo<Kho_DonViSuDung>(CommonDanhMuc.DonViBoPhan, item.ID.ToString());
                            if (kho_DanhMuc != null)
                            {
                                if (item.DB_ID != kho_DanhMuc.id)
                                {
                                    item.DB_ID = kho_DanhMuc.id;
                                    _donViBoPhanService.UpdateDonViBoPhan(item);
                                }
                            }
                            else if (item.DB_ID != null)
                            {
                                item.DB_ID = null;
                                _donViBoPhanService.UpdateDonViBoPhan(item);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                return OkSuccessMessage("done");
            }
            else if (danhMuc == "LoaiTaiSan")
            {
                List<LoaiTaiSan> listDanhMuc = new List<LoaiTaiSan>();
                if (DB_ID_NULL)
                    listDanhMuc = _loaiTaiSanService.GetLoaiTaiSans().Where(x => x.DB_ID == null).ToList();
                else
                    listDanhMuc = _loaiTaiSanService.GetLoaiTaiSans().Where(x => x.DB_ID != null).ToList();
                Kho_LoaiTaiSan kho_DanhMuc = new Kho_LoaiTaiSan();
                if (listDanhMuc != null && listDanhMuc.Count > 0)
                {
                    LoaiTaiSan item = new LoaiTaiSan();
                    for (int i = 0; i < listDanhMuc.Count; i++)
                    {
                        try
                        {
                            item = listDanhMuc[i];
                            kho_DanhMuc = null;
                            kho_DanhMuc = await GetIdDongBo<Kho_LoaiTaiSan>(CommonDanhMuc.LoaiTaiSan, item.ID.ToString());
                            if (kho_DanhMuc != null)
                            {
                                if (item.DB_ID != kho_DanhMuc.id)
                                {
                                    item.DB_ID = kho_DanhMuc.id;
                                    _loaiTaiSanService.UpdateLoaiTaiSan(item);
                                }
                            }
                            else if (item.DB_ID != null)
                            {
                                item.DB_ID = null;
                                _loaiTaiSanService.UpdateLoaiTaiSan(item);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                return OkSuccessMessage("done");
            }
            else if (danhMuc == "LoaiDonVi")
            {
                List<LoaiDonVi> listDanhMuc = new List<LoaiDonVi>();
                if (DB_ID_NULL)
                    listDanhMuc = _loaiDonViService.GetAllLoaiDonVis().Where(x => x.DB_ID == null).ToList();
                else
                    listDanhMuc = _loaiDonViService.GetAllLoaiDonVis().Where(x => x.DB_ID != null).ToList();
                Kho_LoaiDonVi kho_DanhMuc = new Kho_LoaiDonVi();
                if (listDanhMuc != null && listDanhMuc.Count > 0)
                {
                    LoaiDonVi item = new LoaiDonVi();
                    for (int i = 0; i < listDanhMuc.Count; i++)
                    {
                        try
                        {
                            item = listDanhMuc[i];
                            kho_DanhMuc = null;
                            kho_DanhMuc = await GetIdDongBo<Kho_LoaiDonVi>(CommonDanhMuc.LoaiDonVi, item.ID.ToString());
                            if (kho_DanhMuc != null)
                            {
                                if (item.DB_ID != kho_DanhMuc.id)
                                {
                                    item.DB_ID = kho_DanhMuc.id;
                                    _loaiDonViService.UpdateLoaiDonVi(item);
                                }
                            }
                            else if (item.DB_ID != null)
                            {
                                item.DB_ID = null;
                                _loaiDonViService.UpdateLoaiDonVi(item);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                return OkSuccessMessage("done");
            }
            else if (danhMuc == "HienTrang")
            {
                List<HienTrang> listDanhMuc = new List<HienTrang>();
                if (DB_ID_NULL)
                    listDanhMuc = _hienTrangService.GetHienTrangs().Where(x => x.DB_ID == null).ToList();
                else
                    listDanhMuc = _hienTrangService.GetHienTrangs().Where(x => x.DB_ID != null).ToList();
                Kho_HienTrang kho_DanhMuc = new Kho_HienTrang();
                if (listDanhMuc != null && listDanhMuc.Count > 0)
                {
                    HienTrang item = new HienTrang();
                    for (int i = 0; i < listDanhMuc.Count; i++)
                    {
                        try
                        {
                            item = listDanhMuc[i];
                            kho_DanhMuc = null;
                            kho_DanhMuc = await GetIdDongBo<Kho_HienTrang>(CommonDanhMuc.HienTrangSuDung, item.ID.ToString());
                            if (kho_DanhMuc != null)
                            {
                                if (item.DB_ID != kho_DanhMuc.id)
                                {
                                    item.DB_ID = kho_DanhMuc.id;
                                    _hienTrangService.UpdateHienTrang(item);
                                }
                            }
                            else if (item.DB_ID != null)
                            {
                                item.DB_ID = null;
                                _hienTrangService.UpdateHienTrang(item);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                return OkSuccessMessage("done");
            }
            else if (danhMuc == "LyDoBienDong")
            {
                List<LyDoBienDong> listDanhMuc = new List<LyDoBienDong>();
                if (DB_ID_NULL)
                    listDanhMuc = _lyDoBienDongService.GetLyDoBienDongs().Where(x => x.DB_ID == null).ToList();
                else
                    listDanhMuc = _lyDoBienDongService.GetLyDoBienDongs().Where(x => x.DB_ID != null).ToList();
                Kho_LyDoBienDong kho_DanhMuc = new Kho_LyDoBienDong();
                if (listDanhMuc != null && listDanhMuc.Count > 0)
                {
                    LyDoBienDong item = new LyDoBienDong();
                    for (int i = 0; i < listDanhMuc.Count; i++)
                    {
                        try
                        {
                            item = listDanhMuc[i];
                            kho_DanhMuc = null;
                            kho_DanhMuc = await GetIdDongBo<Kho_LyDoBienDong>(CommonDanhMuc.LyDoBienDong, item.ID.ToString());
                            if (kho_DanhMuc != null)
                            {
                                if (item.DB_ID != kho_DanhMuc.id)
                                {
                                    item.DB_ID = kho_DanhMuc.id;
                                    _lyDoBienDongService.UpdateLyDoBienDong(item);
                                }
                            }
                            else if (item.DB_ID != null)
                            {
                                item.DB_ID = null;
                                _lyDoBienDongService.UpdateLyDoBienDong(item);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                return OkSuccessMessage("done");
            }
            else if (danhMuc == "ChucDanh")
            {
                List<ChucDanh> listDanhMuc = new List<ChucDanh>();
                if (DB_ID_NULL)
                    listDanhMuc = _chucDanhService.GetAllChucDanhs().Where(x => x.DB_ID == null).ToList();
                else
                    listDanhMuc = _chucDanhService.GetAllChucDanhs().Where(x => x.DB_ID != null).ToList();
                Kho_ChucDanh kho_DanhMuc = new Kho_ChucDanh();
                if (listDanhMuc != null && listDanhMuc.Count > 0)
                {
                    ChucDanh item = new ChucDanh();
                    for (int i = 0; i < listDanhMuc.Count; i++)
                    {
                        try
                        {
                            item = listDanhMuc[i];
                            kho_DanhMuc = null;
                            kho_DanhMuc = await GetIdDongBo<Kho_ChucDanh>(CommonDanhMuc.ChucDanh, item.ID.ToString());
                            if (kho_DanhMuc != null)
                            {
                                if (item.DB_ID != kho_DanhMuc.id)
                                {
                                    item.DB_ID = kho_DanhMuc.id;
                                    _chucDanhService.UpdateChucDanh(item);
                                }
                            }
                            else if (item.DB_ID != null)
                            {
                                item.DB_ID = null;
                                _chucDanhService.UpdateChucDanh(item);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                return OkSuccessMessage("done");
            }
            else if (danhMuc == "NhanXe")
            {
                List<NhanXe> listDanhMuc = new List<NhanXe>();
                if (DB_ID_NULL)
                    listDanhMuc = _nhanXeService.GetAllNhanXes().Where(x => x.DB_ID == null).ToList();
                else
                    listDanhMuc = _nhanXeService.GetAllNhanXes().Where(x => x.DB_ID != null).ToList();
                Kho_NhanXe kho_DanhMuc = new Kho_NhanXe();
                if (listDanhMuc != null && listDanhMuc.Count > 0)
                {
                    NhanXe item = new NhanXe();
                    for (int i = 0; i < listDanhMuc.Count; i++)
                    {
                        try
                        {
                            item = listDanhMuc[i];
                            kho_DanhMuc = null;
                            kho_DanhMuc = await GetIdDongBo<Kho_NhanXe>(CommonDanhMuc.NhanHieu, item.ID.ToString());
                            if (kho_DanhMuc != null)
                            {
                                if (item.DB_ID != kho_DanhMuc.id)
                                {
                                    item.DB_ID = (int?)kho_DanhMuc.id;
                                    _nhanXeService.UpdateNhanXe(item);
                                }
                            }
                            else if (item.DB_ID != null)
                            {
                                item.DB_ID = null;
                                _nhanXeService.UpdateNhanXe(item);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                return OkSuccessMessage("done");
            }
            else if (danhMuc == "MucDichSuDung")
            {
                List<MucDichSuDung> listDanhMuc = new List<MucDichSuDung>();
                if (DB_ID_NULL)
                    listDanhMuc = _mucDichSuDungService.GetMucDichSuDungs().Where(x => x.DB_ID == null).ToList();
                else
                    listDanhMuc = _mucDichSuDungService.GetMucDichSuDungs().Where(x => x.DB_ID != null).ToList();
                Kho_MucDichSuDung kho_DanhMuc = new Kho_MucDichSuDung();
                if (listDanhMuc != null && listDanhMuc.Count > 0)
                {
                    MucDichSuDung item = new MucDichSuDung();
                    for (int i = 0; i < listDanhMuc.Count; i++)
                    {
                        try
                        {
                            item = listDanhMuc[i];
                            kho_DanhMuc = null;
                            kho_DanhMuc = await GetIdDongBo<Kho_MucDichSuDung>(CommonDanhMuc.MucDichSuDung, item.ID.ToString());
                            if (kho_DanhMuc != null)
                            {
                                if (item.DB_ID != kho_DanhMuc.id)
                                {
                                    item.DB_ID = (int?)kho_DanhMuc.id;
                                    _mucDichSuDungService.UpdateMucDichSuDung(item);
                                }
                            }
                            else if (item.DB_ID != null)
                            {
                                item.DB_ID = null;
                                _mucDichSuDungService.UpdateMucDichSuDung(item);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                return OkSuccessMessage("done");
            }
            return OkErrorMessage("Lỗi");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDanhMuc(string danhMuc, bool DB_ID_NULL = true)
        {
            _logger.Information($"UpdateDanhMuc {danhMuc} started at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");

            return OkErrorMessage("Lỗi");
        }
    }


}

