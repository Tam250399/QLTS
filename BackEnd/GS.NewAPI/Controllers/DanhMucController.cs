using GS.NewAPI.Factories;
using GS.NewAPI.Infrastruture.Response;
using GS.NewAPI.Models;
using GS.NewAPI.Models.DanhMuc;
//using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
namespace GS.WebApi.Controllers
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

        [HttpGet("diaBan")]
        public IActionResult GetAllDiaBans(string tenDiaBan)
        {
            #region check token
            //if (!CheckCurrentUser())
            //    return OkErrorMessage("Token hết hạn");
            #endregion
            var result = string.IsNullOrWhiteSpace(tenDiaBan)
                ? _danhMucModelFactory.GetAllQuocGias()
                : _danhMucModelFactory.SearchQuocGiasByName(tenDiaBan);
            return Ok(result);
        }

        [HttpGet("mucDichSuDung")]
        public IActionResult GetAllMucDichSuDungs()
        {
            var response = new BaseResponse<ListResponse<QuocGiaModel>>(
                success: false
            );
            var result = _danhMucModelFactory.GetAllQuocGias();
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
        public IActionResult GetAllQTinhTPs(int quocGiaId)
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

        #endregion
        #endregion

    }
}
