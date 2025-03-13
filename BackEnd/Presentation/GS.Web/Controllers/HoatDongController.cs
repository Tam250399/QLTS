using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Factories.HeThong;
using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Mvc;

namespace GS.Web.Controllers
{
    public partial class HoatDongController : BaseWorksController
    {
        #region Fields

        private readonly IHoatDongModelFactory _hoatdongModelFactory;
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;

        #endregion

        #region Ctor

        public HoatDongController(IHoatDongModelFactory hoatdongModelFactory,
            IHoatDongService hoatdongService,
            INhanHienThiService NhanHienThiService,
            IQuyenService QuyenService
            )
        {
            this._hoatdongModelFactory = hoatdongModelFactory;
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = NhanHienThiService;
            this._quyenService = QuyenService;
        }

        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IActionResult List()
        {
            //check 
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhatKyHoatDong))
                return AccessDeniedView();

            //prepare model
            var searchmodel = new HoatDongSearchModel();
            var model = _hoatdongModelFactory.PrepareHoatDongSearchModel(searchmodel);
            return View(model);
        }

        /// <summary>
        /// Search Activities 
        /// </summary>
        /// <param name="searchModel">search conditions</param>
        /// <returns>HoatDongListModel</returns>
        [HttpPost]
        public virtual IActionResult List(HoatDongSearchModel searchModel)
        {
            //check 
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhatKyHoatDong))
                return AccessDeniedView();
            //prepare model
            var model = _hoatdongModelFactory.PrepareHoatDongListModel(searchModel);
            return Json(model);
        }

        /// <summary>
        /// Show detail activity selected
        /// </summary>
        /// <param name="id">HoatDongiD</param>
        /// <returns></returns>
        public virtual IActionResult _Detail(decimal id)
        {
            //check 
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhatKyHoatDong))
                return AccessDeniedView();
            var item = _hoatdongService.GetHoatDongById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _hoatdongModelFactory.PrepareHoatDongModel(null, item);
            //return ParView(model);
            return PartialView(model);
        }
        #endregion
    }
}
