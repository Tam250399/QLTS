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
    public class DanhMucController : ControllerBase
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
            var response = new BaseResponse<ListResponse<QuocGiaModel>>(
                success: false
            );
            var result =  _danhMucModelFactory.GetAllQuocGias();
            response.Success = true;
            response.Data = new ListResponse<QuocGiaModel>(data: result, count: result.Count);
            response.StatusCode = 200;
            return Ok(response);
        }

        
        [HttpGet("lyDoTangGiam")]
        public IActionResult GetLyDoTangGiams(decimal? loaiLyDoBienDongId, decimal? loaiHinhTaiSanId, Boolean isTangMoi)
        {
            var response = new BaseResponse<ListResponse<LyDoBienDongModel>>(
               success: false
           );
            var result = _danhMucModelFactory.GetLyDoTangGiams(loaiLyDoBienDongId, loaiHinhTaiSanId, isTangMoi);
            response.Success = true;
            response.Data = new ListResponse<LyDoBienDongModel>(data: result, count: result.Count);
            response.StatusCode = 200;
            return Ok(response);
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
            var response = new BaseResponse<ListResponse<DiaBanModel>>(
                success: false
            );
            var result = _danhMucModelFactory.GetTinhThanhPhosByQuocGiaId(quocGiaId);
            response.Success = true;
            response.Data = new ListResponse<DiaBanModel>(data: result, count: result.Count);
            response.StatusCode = 200;
            return Ok(response);
        }

        [HttpGet("diaBanDuoiTinhTP")]
        public IActionResult GetAllDiaBanDuoiTinhTPs(string maCha)
        {
            #region check token
            //if (!CheckCurrentUser())
            //    return OkErrorMessage("Token hết hạn");
            #endregion
            var response = new BaseResponse<ListResponse<DiaBanModel>>(
                success: false
            );
            var result = _danhMucModelFactory.GetDiaBansByMaCha(maCha);
            response.Success = true;
            response.Data = new ListResponse<DiaBanModel>(data: result, count: result.Count);
            response.StatusCode = 200;
            return Ok(response);
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
            var response = new BaseResponse<ListResponse<MucDichSuDungModel>>(
                success: false
            );
            var result = _danhMucModelFactory.GetMucDichSuDungsByLoaiHinhTSId(loaiHinhTaiSanId);
            response.Success = true;
            response.Data = new ListResponse<MucDichSuDungModel>(data: result, count: result.Count);
            response.StatusCode = 200;
            return Ok(response);
        }
        #endregion

        [HttpGet("donViBoPhan")]
        public IActionResult GetDonViBoPhans(decimal donViId)
        {
            #region check token
            //if (!CheckCurrentUser())
            //    return OkErrorMessage("Token hết hạn");
            #endregion
            var response = new BaseResponse<ListResponse<DonViBoPhanModel>>(
                success: false
            );
            var result = _danhMucModelFactory.GetDonViBoPhans(donViId);
            response.Success = true;
            response.Data = new ListResponse<DonViBoPhanModel>(data: result, count: result.Count);
            response.StatusCode = 200;
            return Ok(response);
        }
        #endregion

    }
}
