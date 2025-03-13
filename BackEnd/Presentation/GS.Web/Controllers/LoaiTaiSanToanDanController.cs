using GS.Core;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.DongBo;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Extensions;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace GS.Web.Controllers
{
	[HttpsRequirement(SslRequirement.No)]
	public class LoaiTaiSanToanDanController : BaseWorksController
	{
		// GET: LoaiTaiSanToanDan
		#region Fields

		private readonly IHoatDongService _hoatdongService;
		private readonly INhanHienThiService _nhanHienThiService;
		private readonly IQuyenService _quyenService;
		private readonly IWorkContext _workContext;
		private readonly CauHinhChung _cauhinhChung;
		private readonly ILoaiTaiSanModelFactory _itemModelFactory;
		private readonly ILoaiTaiSanService _itemService;
		private readonly ICheDoHaoMonService _chedohaomonService;
		private readonly IDongBoFactory _dongBoFactory;
		private readonly ICauHinhModelFactory _cauHinhModelFactory;

		#endregion Fields

		#region Ctor

		public LoaiTaiSanToanDanController(
			IHoatDongService hoatdongService,
			INhanHienThiService nhanHienThiService,
			IQuyenService quyenService,
			IWorkContext workContext,
			CauHinhChung cauhinhChung,
			ILoaiTaiSanModelFactory itemModelFactory,
			ILoaiTaiSanService itemService,
			ICheDoHaoMonService chedohaomonService,
			IDongBoFactory dongBoFactory,
			ICauHinhModelFactory cauHinhModelFactory
			)
		{
			this._hoatdongService = hoatdongService;
			this._nhanHienThiService = nhanHienThiService;
			this._quyenService = quyenService;
			this._workContext = workContext;
			this._cauhinhChung = cauhinhChung;
			this._itemModelFactory = itemModelFactory;
			this._itemService = itemService;
			this._chedohaomonService = chedohaomonService;
			this._dongBoFactory = dongBoFactory;
			_cauHinhModelFactory = cauHinhModelFactory;
		}

		#endregion Ctor

		#region Methods

		public virtual IActionResult List()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiTaiSanToanDan))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new LoaiTaiSanSearchModel();
			var model = _itemModelFactory.PrepareLoaiTaiSanSearchModel(searchmodel);

			//session
			var _searchModel = HttpContext.Session.GetObject<LoaiTaiSanSearchModel>(enumTENCAUHINH.KeySearch + typeof(LoaiTaiSanSearchModel).Name);
			if (_searchModel != null && _searchModel.IsLoadSession)
			{
				_cauHinhModelFactory.PrePareModelBySession(model, _searchModel);
				UpdateSessionSearchModel<LoaiTaiSanSearchModel>(false);
			}
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult List(LoaiTaiSanSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiTaiSanToanDan))
				return AccessDeniedKendoGridJson();
			//prepare model
			var model = _itemModelFactory.PrepareLoaiTaiSanToanDanListModel(searchModel);
			//session
			if (searchModel.ParentId == null)
				HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch + searchModel.GetType().Name, searchModel);

			return Json(model);
		}
		[HttpPost]
		public virtual IActionResult ListChonLoaiTaiSan(LoaiTaiSanSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiTaiSanToanDan))
				return AccessDeniedKendoGridJson();
			//prepare model
			var model = _itemModelFactory.PrepareChonLoaiTaiSanListModel(searchModel);
			return Json(model);
		}
		public virtual IActionResult Create()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiTaiSanToanDan))
				return AccessDeniedView();
			//prepare model
			var model = _itemModelFactory.PrepareLoaiTaiSanModel(new LoaiTaiSanModel(), null);
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult Create(LoaiTaiSanModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiTaiSanToanDan))
				return AccessDeniedView();

			if (ModelState.IsValid)
			{
				var item = new LoaiTaiSan();
				_itemModelFactory.PrepareLoaiTaiSan(model, item);
				_itemService.InsertLoaiTaiSan(item);
				model.ID = item.ID;
				_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<LoaiTaiSanModel>(), "LoaiTaiSan");
				_dongBoFactory.DongBoDanhMuc<LoaiTaiSanModel>(new List<LoaiTaiSanModel>() { model });
				return JsonSuccessMessage("Tạo mới dữ liệu thành công !", item.ToModel<LoaiTaiSanModel>());
			}

			var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			return JsonErrorMessage("Error", list);
		}

		public virtual IActionResult Edit(int id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiTaiSanToanDan))
				return AccessDeniedView();

			var item = _itemService.GetLoaiTaiSanById(id);
			if (item == null)
				return RedirectToAction("List");
			//prepare model
			var model = _itemModelFactory.PrepareLoaiTaiSanModel(null, item);
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult Edit(LoaiTaiSanModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiTaiSanToanDan))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetLoaiTaiSanById(model.ID);
			if (item == null)
				return RedirectToAction("List");
			if (ModelState.IsValid)
			{
				_itemModelFactory.PrepareLoaiTaiSan(model, item);
				_itemService.UpdateLoaiTaiSan(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<LoaiTaiSanModel>(), "LoaiTaiSan");
				_dongBoFactory.DongBoDanhMuc<LoaiTaiSanModel>(new List<LoaiTaiSanModel>() { model });
				//session
				UpdateSessionSearchModel<LoaiTaiSanSearchModel>(true);
				return JsonSuccessMessage("Cập nhật dữ liệu thành công !", item.ToModel<LoaiTaiSanModel>());
			}
			var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			return JsonErrorMessage("Error", list);
		}

		[HttpPost]
		public virtual IActionResult Delete(int id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiTaiSanToanDan))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetLoaiTaiSanById(id);

			if (item == null)
				return RedirectToAction("List");
			var taisancon = _itemService.GetCountSub(item.ID);
			if (taisancon == 0)
			{
				try
				{
					LoaiTaiSanModel model = item.ToModel<LoaiTaiSanModel>();
					_itemService.DeleteLoaiTaiSan(item);
					_hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<LoaiTaiSanModel>(), "LoaiTaiSan");
					// đồng bộ dữ liệu sang kho
					_dongBoFactory.XoaDanhMuc<LoaiTaiSanModel>(model);
					//activity log
					//cấu hình 
					UpdateSessionSearchModel<LoaiTaiSanSearchModel>(true);
					return JsonSuccessMessage("Xoá dữ liệu thành công");
				}
				catch (Exception exc)
				{
					ErrorNotification(exc);
					return RedirectToAction("Edit", new { id = item.ID });
				}

			}
			ErrorNotification("Bạn phải xoá tài sản con");
			return RedirectToAction("Edit", new { id = item.ID });

		}

		public virtual IActionResult SearchLoaiTaiSan(string KeySearch = null, decimal CHE_DO_HAO_MON_ID = 0, decimal? LOAI_HINH_TAI_SAN_ID = 0)
		{
			var items = _itemService.GetForInputSearch(KeySearch, loaiHinhTSId: LOAI_HINH_TAI_SAN_ID, cheDoId: CHE_DO_HAO_MON_ID);
			return Json(items.Select(c =>
			{
				var m = c.ToModel<LoaiTaiSanModel>();
				var chedo = _chedohaomonService.GetCheDoHaoMonById(m.CHE_DO_HAO_MON_ID ?? 0);
				if (chedo != null)
				{
					m.TEN = m.TEN + " (" + chedo.TEN_CHE_DO + ")";
				}
				if (CHE_DO_HAO_MON_ID > 0)
				{
					m.CHE_DO_HAO_MON_ID = CHE_DO_HAO_MON_ID;
				}
				if (LOAI_HINH_TAI_SAN_ID > 0)
				{ m.LOAI_HINH_TAI_SAN_ID = LOAI_HINH_TAI_SAN_ID; }
				return m;
			}
			).ToList());
		}

		public virtual IActionResult CheckLoaiOtoId(decimal LoaiTaiSanId)
		{
			var lts = _itemService.GetLoaiTaiSanById(LoaiTaiSanId);
			if (lts != null && lts.OTO_LOAI_XE_ID != null)
				return Json(lts.OTO_LOAI_XE_ID);
			return Json((int)enumLoaiXe.Khac);
		}
		public virtual IActionResult GetDDLLoaiTaiSan()
		{
			var data = _itemModelFactory.PrepareSelectListLoaiTaiSan(loaiHinhTaiSanId: (int)enumLOAI_HINH_TAI_SAN.OTO, ParentLoaiTaiSanId: (int)enumLoaiTaiSanOto.CHUYEN_DUNG, isAddFirst: true);
			return Json(data);
		}
		[HttpPost]
		public virtual IActionResult GetMaLoaiTaiSan(decimal LoaiTaiSanId)
		{
			var lts = _itemService.GetLoaiTaiSanById(LoaiTaiSanId);

			return Json(lts.MA);
		}

		#endregion Methods
	}


}
