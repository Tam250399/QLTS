using DevExpress.XtraRichEdit.Model;
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Web.Framework.Kendoui;
using GS.NewAPI.Factories;
using GS.WebApi.Infrastructure.Mapper.Extensions;
using GS.NewAPI.Models;
using GS.NewAPI.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS.Web.Areas.Admin.Controllers;
namespace GS.WebApi.Controllers
{
    public class DanhMucController : BaseAdminController
    {
        #region Ctor

        private readonly IDanhMucModelFactory _danhMucModelFactory;
        private readonly IHoatDongService _hoatDongService;
        public DanhMucController(IDanhMucModelFactory danhMucSvcModelFactory,
            IHoatDongService hoatDongService)
        {
            this._danhMucModelFactory = danhMucSvcModelFactory;
            this._hoatDongService = hoatDongService;
        }
        #endregion
        #region method
        #region quốc gia
        [HttpGet]
        public IActionResult GetAllQuocGias()
        {
            #region check token
            //if (!CheckCurrentUser())
            //    return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucModelFactory.GetAllQuocGias();
            return Ok(result);
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
