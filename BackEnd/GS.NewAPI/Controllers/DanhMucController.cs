using GS.NewAPI.Factories;
using GS.NewAPI.Infrastruture.Response;
using GS.NewAPI.Models.DanhMuc;
using GS.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
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
        public IActionResult GetLyDoTangGiams(string tenDiaBan)
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


        #endregion
        #endregion

    }
}
