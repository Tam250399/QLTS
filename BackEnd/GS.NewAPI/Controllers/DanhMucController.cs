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

        [HttpGet("danhmucquocgias")]
        public IActionResult GetAllQuocGias(string tenQuocGia)
        {
            #region check token
            //if (!CheckCurrentUser())
            //    return OkErrorMessage("Token hết hạn");
            #endregion
            var response = new BaseResponse<ListResponse<QuocGiaModel>>(
                success: false
            );
            var result = string.IsNullOrWhiteSpace(tenQuocGia) 
                ? _danhMucModelFactory.GetAllQuocGias() 
                : _danhMucModelFactory.SearchQuocGiasByName(tenQuocGia);
            response.Success = true;
            response.Data = new ListResponse<QuocGiaModel>(data: result, count: result.Count);
            response.StatusCode = 200;
            return Ok(response);
        }
        //[HttpPost]
        //public IActionResult UpdateQuocGia([FromBody] QuocGiaModel model)
        //{
        //    #region check token
        //    //if (!CheckCurrentUser())
        //    //    return OkErrorMessage("Token hết hạn");
        //    #endregion
        //    if (!ModelState.IsValid)
        //    {
        //        var ListError = ModelState.SerializeErrors();
        //        return OkErrorMessage("Error", ListError);
        //    }
        //    if (model == null)
        //        return this.NullModel();
        //    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật quốc gia", 0, "QuocGia", model);
        //    var result = _danhMucModelFactory.UpdateQuocGia(model, currentUser);
        //    return Ok(result);
        //}


        #endregion
        #endregion
    
    }
}
