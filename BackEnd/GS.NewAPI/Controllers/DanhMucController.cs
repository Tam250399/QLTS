using GS.NewAPI.Factories;
using GS.NewAPI.Infrastruture.Response;
using GS.NewAPI.Models;
using GS.NewAPI.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc;
using System;
namespace GS.NewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhMucController : BaseApiController
    {
        #region Ctor

        private readonly IDanhMucModelFactory _danhMucModelFactory;
        public DanhMucController(IDanhMucModelFactory danhMucSvcModelFactory)
        {
            this._danhMucModelFactory = danhMucSvcModelFactory;
        }
        #endregion
        #region method
        #region quốc gia

        [HttpGet("quocGia")]
        public IActionResult GetAllQuocGias()
        {
            #region check token
            //if (!CheckCurrentUser())
            //    return OkErrorMessage("Token hết hạn");
            #endregion
            var result =  _danhMucModelFactory.GetAllQuocGias();
            return OkSuccessMessage("Lấy dữ liệu thành công", new ListResponse<QuocGiaModel>(result));
        }

        
        [HttpGet("lyDoTangGiam")]
        public IActionResult GetLyDoTangGiams(decimal? loaiLyDoBienDongId, decimal? loaiHinhTaiSanId, Boolean isTangMoi)
        {
            var result = _danhMucModelFactory.GetLyDoTangGiams(loaiLyDoBienDongId, loaiHinhTaiSanId, isTangMoi);
            return OkSuccessMessage("Lấy dữ liệu thành công", new ListResponse<LyDoBienDongModel>(result));
        }


        #endregion
        #region địa bàn

        [HttpGet("tinhthanhpho")]
        public IActionResult GetAllTinhTPs(int quocGiaId)
        {
            #region check token
            //if (!CheckCurrentUser())
            //    return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucModelFactory.GetTinhThanhPhosByQuocGiaId(quocGiaId);
            return OkSuccessMessage("Lấy dữ liệu thành công", new ListResponse<DiaBanModel>(result));
        }

        [HttpGet("diaBanDuoiTinhTP")]
        public IActionResult GetAllDiaBanDuoiTinhTPs(string maCha)
        {
            #region check token
            //if (!CheckCurrentUser())
            //    return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucModelFactory.GetDiaBansByMaCha(maCha);
            return OkSuccessMessage("Lấy dữ liệu thành công", new ListResponse<DiaBanModel>(result));
        }

        #endregion

        #region mục đích sử dụng

        [HttpGet("mucDichSuDung")]
        public IActionResult GetMucDichSuDungs(decimal? loaiHinhTaiSanId)
        {
            #region check token
            //if (!CheckCurrentUser())
            //    return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucModelFactory.GetMucDichSuDungsByLoaiHinhTSId(loaiHinhTaiSanId);
            return OkSuccessMessage("Lấy dữ liệu thành công", new ListResponse<MucDichSuDungModel>(result));
        }
        #endregion

        [HttpGet("donViBoPhan")]
        public IActionResult GetDonViBoPhans(decimal donViId)
        {
            #region check token
            //if (!CheckCurrentUser())
            //    return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucModelFactory.GetDonViBoPhans(donViId);
            return OkSuccessMessage("Lấy dữ liệu thành công", new ListResponse<DonViBoPhanModel>(result));
        }
        #endregion

    }
}
