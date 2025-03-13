using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Kendoui;
using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Mvc;

namespace GS.Web.Controllers
{
    public class LoaiHoatDongController : BaseWorksController
    {
        #region Fields
        private readonly IQuyenService _quyenService;
        private readonly ILoaiHoatDongModelFactory _itemModelFactory;
        private readonly IHoatDongService _hoatDongService;
        #endregion
        #region Ctor
        public LoaiHoatDongController(
            IQuyenService quyenService,
            ILoaiHoatDongModelFactory loaiHoatDongModelFactory,
            IHoatDongService hoatDongService
            )
        {
            this._quyenService = quyenService;
            this._itemModelFactory = loaiHoatDongModelFactory;
            this._hoatDongService = hoatDongService;
        }

        #endregion
        #region Methods
        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLLoaiHoatDong))
                return AccessDeniedView();
            //prepare model
            var model = new LoaiHoatDongSearchModel();
            model = _itemModelFactory.PrepareLoaiHoatDongSearchModel(model);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(LoaiHoatDongSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLLoaiHoatDong))
                return AccessDeniedKendoGridJson();
            //prepare model
            if (searchModel.PageSize == 0) searchModel.PageSize = 10;
            var model = _itemModelFactory.PrepareLoaiHoatDongListModel(searchModel);
            var a = Json(model);
            return a;
        }


        public virtual IActionResult Update(LoaiHoatDongModel model)
        {

            var item = _hoatDongService.GetLoaiHoatDongById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                model.TU_KHOA_HE_THONG = model.TU_KHOA_HE_THONG.Trim().ToLower();
                //var _type = _hoatDongService.GetLoaiHoatDongByCode(model.TU_KHOA_HE_THONG);
                //if ( _type!= null && _type.ID != item.ID)
                //    return JsonErrorMessage("Tên nhãn đã tồn tại");
                _itemModelFactory.PrepareLoaiHoatDong(model, item);
                _hoatDongService.UpdateKieuHoatDong(item);
                SuccessNotification("Cập nhật dữ liệu thành công");
                return this.JsonSuccessMessage();
            }
            else
                return JsonErrorMessage(ModelState.SerializeErrors().ToString());
        }
        #endregion
    }
}
