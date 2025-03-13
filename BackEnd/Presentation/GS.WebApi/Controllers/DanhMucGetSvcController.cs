using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Web.Framework.Kendoui;
using GS.WebApi.Factories;
using GS.WebApi.Factories.Common;
using GS.WebApi.Infrastructure.Mapper.Extensions;
using GS.WebApi.Models;
using GS.WebApi.Models.DanhMuc;
using GS.WebApi.Models.DMDC;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GS.WebApi.Controllers
{
    public class DanhMucGetSvcController : BaseAdminController
    {
        #region Ctor

        private readonly IDanhMucSvcModelFactory _danhMucSvcModelFactory;
        private readonly IHoatDongService _hoatDongService;
        private readonly INhanHienThiService _nhanHienThiService;
        public DanhMucGetSvcController(IDanhMucSvcModelFactory danhMucSvcModelFactory,
            IHoatDongService hoatDongService,
            INhanHienThiService nhanHienThiService)
        {
            this._danhMucSvcModelFactory = danhMucSvcModelFactory;
            this._hoatDongService = hoatDongService;
            this._nhanHienThiService = nhanHienThiService;
        }
        #endregion
        #region method
        #region quốc gia
        [HttpGet]
        public IActionResult GetAllQuocGias()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllQuocGias().Select(x => new { ID = x.ID, MA = x.MA, TEN = x.TEN }).ToList();
            return Ok(result);
        }
        [HttpPost]
        #endregion
        #region Hiện trạng
        [HttpGet]
        public IActionResult GetAllHienTrangs()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllHienTrangs().Select(x => new { ID = x.ID, TEN_HIEN_TRANG = x.TEN_HIEN_TRANG, TAI_SAN_AP_DUNG = (x.NHOM_HIEN_TRANG_ID.GetValueOrDefault(0) == 0 ? "Tài sản công" : "Tài sản dự án"), LOAI_TAI_SAN_AP_DUNG = _nhanHienThiService.GetGiaTriEnum<enumLOAI_HINH_TAI_SAN>((enumLOAI_HINH_TAI_SAN)x.LOAI_HINH_TAI_SAN_ID) }).ToList();
            return Ok(result);
        }
        [HttpPost]

        #endregion
        #region LoaiDonVi
        [HttpGet]
        public IActionResult GetAllLoaiDonVis()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllLoaiDonVis().Select(x => new { ID = x.ID, MA = x.MA, TEN = x.TEN }).ToList();
            return Ok(result);
        }
        #endregion        
        #region DonVi
        [HttpGet]
        public IActionResult GetAllDonVis(int? pageSize, int? pageIndex)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (pageSize != null && pageIndex != null)
            {
                var result = _danhMucSvcModelFactory.GetAllDonVis(pageSize.Value, pageIndex.Value);
                return Ok(result);
            }
            else
            {
                var result = _danhMucSvcModelFactory.GetAllDonVis();
                return Ok(result);
            }
        }
        #endregion     
        #region Nguồn gốc tài sản
        [HttpGet]
        public IActionResult GetAllNguonGocTaiSans()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllNguonGocTaiSans();
            return Ok(result);
        }
        #endregion     
        #region MucDichSuDung
        //[HttpGet]
        //public IActionResult GetAllMucDichSuDungs()
        //{
        //    var result = _danhMucSvcModelFactory.GetAllMucDichSuDungs();
        //    return Ok(result);
        //}
        #endregion
        #region Chế độ hao mòn
        [HttpGet]
        public IActionResult GetAllCheDoHaoMons()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllCheDoHaoMons();
            return Ok(result);
        }
        #endregion
        #region LoaiTaiSan
        [HttpGet]
        public IActionResult GetAllLoaiTaiSanNhaNuocs()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllLoaiTaiSanNhaNuocs().Select(x => new { ID = x.ID, MA = x.MA, TEN = x.TEN }).ToList();
            return Ok(result);
        }
        #endregion
        #region LyDoBienDong
        [HttpGet]
        public IActionResult GetAllLyDoBienDongs()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllLyDoBienDongs().Select(x => new { ID = x.ID, MA = x.MA, TEN = x.TEN }).ToList();
            return Ok(result);
        }
        #endregion
        #region Loại lý do biến động      

        #endregion
        #region Người dùng
        [HttpGet]
        public IActionResult GetAllNguoiDungs(int? pageSize, int? pageIndex)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (pageSize != null && pageIndex != null)
            {
                var result = _danhMucSvcModelFactory.GetAllNguoiDungs(pageSize.Value, pageIndex.Value);
                return Ok(result);
            }
            else
            {
                var result = _danhMucSvcModelFactory.GetAllNguoiDungs();
                return Ok(result);
            }
        }
        #endregion
        #region DiaBan
        [HttpGet]
        public IActionResult GetAllDiaBans()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllDiaBans();
            return Ok(result);
        }
        #endregion
        #region DuAn
        //[HttpGet]
        //public IActionResult GetAllDuAns()
        //{
        //    var result = _danhMucSvcModelFactory.GetAllDuAns();
        //    return Ok(result);
        //}
        #endregion

        #region DongXe
        [HttpGet]
        public IActionResult GetAllDongXes()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllDongXes().Select(x => new { ID = x.ID, MA = x.MA, TEN = x.TEN }).ToList();
            return Ok(result);
        }
        #endregion

        #region ChucDanh
        [HttpGet]
        public IActionResult GetAllChucDanhs()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllChucDanhs().Select(x => new { ID = x.ID, MA_CHUC_DANH = x.MA_CHUC_DANH, TEN_CHUC_DANH = x.TEN_CHUC_DANH }).ToList();
            return Ok(result);
        }
        #endregion

        #region NhanXe
        [HttpGet]
        public IActionResult GetAllNhanXes()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllNhanXe().Select(x => new { ID = x.ID, MA = x.MA, TEN = x.TEN }).ToList();
            return Ok(result);
        }
        #endregion
        #region NguonVon
        [HttpGet]
        public IActionResult GetAllNguonVons()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllNguonVons().Select(x => new { ID = x.ID, TEN = x.TEN }).ToList();
            return Ok(result);
        }
        #endregion
        #region Bộ phận sử dụng
        [HttpGet]
        public IActionResult GetAllDonViBoPhan()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllDonViBoPhan();
            return Ok(result);
        }
        #endregion


        #region Loại tài sản theo đơn vị        

        #endregion
        #region Phương án xử lý
        [HttpGet]
        public IActionResult GetAllPhuongAnXuLys()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllPhuongAnXuLys();
            return Ok(result);
        }

        #endregion
        #region Hình thức xử lý
        //[HttpGet]
        //public IActionResult GetAllHinhThucXuLys()
        //{
        //    var result = _danhMucSvcModelFactory.GetAllHinhThucXuLys();
        //    return Ok(result);

        //}
        //[HttpPost]
        //public IActionResult UpdateHinhThucXuLy([FromBody]HinhThucXuLyModel model)
        //{
        //    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật  Phương án xử lý", 0, "HinhThucXuLy", model);
        //    var result = _danhMucSvcModelFactory.UpdateHinhThucXuLy(model, currentUser);
        //    return Ok(result);

        //}

        #endregion
        #region Chức Danh
        //[HttpGet]
        //public IActionResult GetAllHinhThucXuLys()
        //{
        //    var result = _danhMucSvcModelFactory.GetAllHinhThucXuLys();
        //    return Ok(result);

        //}
        //[HttpPost]
        //public IActionResult UpdateHinhThucXuLy([FromBody]HinhThucXuLyModel model)
        //{
        //    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật  Phương án xử lý", 0, "HinhThucXuLy", model);
        //    var result = _danhMucSvcModelFactory.UpdateHinhThucXuLy(model, currentUser);
        //    return Ok(result);

        //}

        #endregion
        #region Hình thức mua sắm
        //[HttpGet]
        //public IActionResult GetAllHinhThucXuLys()
        //{
        //    var result = _danhMucSvcModelFactory.GetAllHinhThucXuLys();
        //    return Ok(result);

        //}
        //[HttpPost]
        //public IActionResult UpdateHinhThucXuLy([FromBody]HinhThucXuLyModel model)
        //{
        //    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật  Phương án xử lý", 0, "HinhThucXuLy", model);
        //    var result = _danhMucSvcModelFactory.UpdateHinhThucXuLy(model, currentUser);
        //    return Ok(result);

        //}

        #endregion
        #endregion

    }
}
