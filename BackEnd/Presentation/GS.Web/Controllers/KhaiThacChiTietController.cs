using GS.Core;
using GS.Core.Domain.TaiSans;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.TaiSans;
using GS.Web.Models.TaiSans;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace GS.Web.Controllers
{
	public class KhaiThacChiTietController : BaseWorksController
	{
		private readonly IKhaiThacChiTietServices _khaiThacChiTietServices;
		private readonly IKhaiThacChiTietModelFactory _khaiThacChiTietModelFactory;
		private readonly IHoatDongService _hoatDongService;
        private readonly IKhaiThacModelFactory _khaiThacModelFactory;

        public KhaiThacChiTietController(IKhaiThacChiTietServices khaiThacChiTietServices,
			IKhaiThacChiTietModelFactory khaiThacChiTietModelFactory,
			IHoatDongService hoatDongService,
			IKhaiThacModelFactory khaiThacModelFactory)
		{
			_khaiThacChiTietServices = khaiThacChiTietServices;
			_khaiThacChiTietModelFactory = khaiThacChiTietModelFactory;
			_hoatDongService = hoatDongService;
            this._khaiThacModelFactory = khaiThacModelFactory;
        }

		public IActionResult KhaiThacChiTietCuaKhaiThac(KhaiThacChiTietSearchModel searchModel)
		{
			var model = _khaiThacChiTietModelFactory.PrepareKhaiThacChiTietListModel(searchModel);
			return Json(model);
		}
		public IActionResult Create(decimal khaiThacId)
		{
			var model = _khaiThacChiTietModelFactory.PrepareKhaiThacChiTietModel(new KhaiThacChiTietModel() { KHAI_THAC_ID = khaiThacId/*, NGAY_KHAI_THAC = DateTime.Now*/ }, null);
			return View(model);
		}
		[HttpPost]
		public IActionResult Create(KhaiThacChiTietModel model)
		{
			if (ModelState.IsValid)
			{
				KhaiThacChiTiet item = new KhaiThacChiTiet();
				_khaiThacChiTietModelFactory.PrepareKhaiThacChiTiet(item: item, model: model);
				_khaiThacChiTietServices.InsertKhaiThacChiTiet(item);
				return JsonSuccessMessage("Thêm mới thành công");
			}
			var listError = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			return JsonErrorMessage("Error", listError);
		}
		public IActionResult Update(decimal Id)
		{
			var ktct = _khaiThacChiTietServices.GetKhaiThacChiTietById(Id);
			var model = _khaiThacChiTietModelFactory.PrepareKhaiThacChiTietModel(null, ktct);
			return View(model);
		}
		[HttpPost]
		public IActionResult Update(KhaiThacChiTietModel model)
		{
			if (ModelState.IsValid)
			{
				var item = _khaiThacChiTietServices.GetKhaiThacChiTietById(model.ID);
				_khaiThacChiTietModelFactory.PrepareKhaiThacChiTiet(item: item, model: model);
				_khaiThacChiTietServices.UpdateKhaiThacChiTiet(item);
				return JsonSuccessMessage("Cập nhật thành công");
			}
			var listError = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			return JsonErrorMessage("Error", listError);
		}
		[HttpPost]
		public virtual IActionResult DeleteAjax(int id)
		{
			//if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
			//	return AccessDeniedView();
			//try to get a store with the specified id
			var item = _khaiThacChiTietServices.GetKhaiThacChiTietById(id);
			if (item == null)
				return JsonErrorMessage("Error");
			_khaiThacChiTietServices.DeleteKhaiThacChiTiet(item);

			_hoatDongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<KhaiThacChiTietModel>(), "KhaiThacChiTiet");
			//activity log  
			return JsonSuccessMessage("Xoá dữ liệu thành công");

		}
		[HttpPost]
		public IActionResult List(KhaiThacSearchModel searchModel)
		{
			var model = _khaiThacChiTietModelFactory.PrepareCapNhatSoTienKhaiThacChiTietListModel(searchModel);
			return Json(model);
		}
		public IActionResult List()
		{
			var  searchmodel = _khaiThacModelFactory.PrepareKhaiThacSearchModel(new KhaiThacSearchModel());
			return View(searchmodel);
		}
	}
}