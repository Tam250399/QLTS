using GS.Services.HeThong;
using GS.Web.Framework.Kendoui;
using GS.WebApi.Factories;
using GS.WebApi.Models.DMDC;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Controllers
{
    public class DMDCController : BaseAdminController
    {
        #region Ctor
        private readonly IDMDC_DanhMucSvcModelFactory _dMDC_DanhMucSvcModelFactory;
        public DMDCController(IDMDC_DanhMucSvcModelFactory dMDC_DanhMucSvcModelFactory)
        {
            this._dMDC_DanhMucSvcModelFactory = dMDC_DanhMucSvcModelFactory;
        }
        #endregion
        //#region DiaBan
        //[HttpPost]
        //public IActionResult InsertOrUpdateDMDCDiaBan([FromBody] DMDC_DiaBanModel model)
        //{
        //    #region check token
        //    if (!CheckCurrentUser())
        //        return OkErrorMessage("Token hết hạn");
        //    #endregion
        //    if (!ModelState.IsValid)
        //    {
        //        var ListError = ModelState.SerializeErrors();
        //        return OkErrorMessage("Error", ListError);
        //    }
        //    if (model == null)
        //        return this.NullModel();
        //    var result = _dMDC_DanhMucSvcModelFactory.InsertOrUpdateDMDCDiaBan(model);
        //    return Ok(result);
        //}
        //[HttpPost]
        //public IActionResult InsertOrUpdateListDMDC_DiaBan([FromBody] List<DMDC_DiaBanModel> ListModel)
        //{
        //    #region check token
        //    if (!CheckCurrentUser())
        //        return OkErrorMessage("Token hết hạn");
        //    #endregion
        //    if (!ModelState.IsValid)
        //    {
        //        var ListError = ModelState.SerializeErrors();
        //        return OkErrorMessage("Error", ListError);
        //    }
        //    //var result = _dMDC_DanhMucSvcModelFactory.InsertOrUpdateListDMDC_DiaBan(ListModel);
        //    return Ok();
        //}
        //#endregion
        //#region DonViNganSach
        //[HttpPost]
        //public IActionResult InsertOrUpdateDMDCDonViNganSach([FromBody] DMDC_DonViNganSachModel model)
        //{
        //    #region check token
        //    if (!CheckCurrentUser())
        //        return OkErrorMessage("Token hết hạn");
        //    #endregion
        //    if (!ModelState.IsValid)
        //    {
        //        var ListError = ModelState.SerializeErrors();
        //        return OkErrorMessage("Error", ListError);
        //    }
        //    if (model == null)
        //        return this.NullModel();
        //    var result = _dMDC_DanhMucSvcModelFactory.InsertOrUpdateDMDCDonViNganSach(model);
        //    return Ok(result);
        //}
        //[HttpPost]
        //public IActionResult InsertOrUpdateListDMDC_DonViNganSach([FromBody] List<DMDC_DonViNganSachModel> ListModel)
        //{
        //    #region check token
        //    if (!CheckCurrentUser())
        //        return OkErrorMessage("Token hết hạn");
        //    #endregion
        //    if (!ModelState.IsValid)
        //    {
        //        var ListError = ModelState.SerializeErrors();
        //        return OkErrorMessage("Error", ListError);
        //    }
        //    var result = _dMDC_DanhMucSvcModelFactory.InsertOrUpdateListDMDC_DonViNganSach(ListModel);
        //    return Ok(result);
        //}
        //#endregion
        //#region QuocGia
        //[HttpPost]
        //public IActionResult InsertOrUpdateDMDCQuocGia([FromBody] DMDC_QuocGiaModel model)
        //{
        //    #region check token
        //    if (!CheckCurrentUser())
        //        return OkErrorMessage("Token hết hạn");
        //    #endregion
        //    if (!ModelState.IsValid)
        //    {
        //        var ListError = ModelState.SerializeErrors();
        //        return OkErrorMessage("Error", ListError);
        //    }
        //    if (model == null)
        //        return this.NullModel();
        //    var result = _dMDC_DanhMucSvcModelFactory.InsertOrUpdateDMDCQuocGia(model);
        //    return Ok(result);
        //}
        //[HttpPost]
        //public IActionResult InsertOrUpdateListDMDCQuocGia([FromBody] List<DMDC_QuocGiaModel> ListModel)
        //{
        //    #region check token
        //    if (!CheckCurrentUser())
        //        return OkErrorMessage("Token hết hạn");
        //    #endregion
        //    if (!ModelState.IsValid)
        //    {
        //        var ListError = ModelState.SerializeErrors();
        //        return OkErrorMessage("Error", ListError);
        //    }

        //    var result = _dMDC_DanhMucSvcModelFactory.InsertOrUpdateListDMDCQuocGia(ListModel);
        //    return Ok(result);
        //}
        //#endregion
    }
}
