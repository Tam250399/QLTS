//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 5/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Mvc;
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.TaiSans;

using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.TaiSans;
using GS.Web.Controllers;
using GS.Web.Factories.TaiSans;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Services.DanhMuc;
using System.Linq;
using GS.Web.Models.DanhMuc;

namespace GS.Web.Controllers
{
	[HttpsRequirement(SslRequirement.No)]
	public partial class TaiSanChoThueController : BaseWorksController
	{
		#region Fields
		private readonly IHoatDongService _hoatdongService;
		private readonly INhanHienThiService _nhanHienThiService;
		private readonly IQuyenService _quyenService;
		private readonly IWorkContext _workContext;
		private readonly CauHinhChung _cauhinhChung;
		private readonly ITaiSanChoThueModelFactory _itemModelFactory;
		private readonly ITaiSanChoThueService _itemService;
		private readonly ITaiSanService _taisanService;
		private readonly ITaiSanModelFactory _taisanModelFactory;
		private readonly IDoiTacModelFactory _doitacModelFactory;
		private readonly IDoiTacService _doitacService;
		#endregion

		#region Ctor
		public TaiSanChoThueController(
			IHoatDongService hoatdongService,
			INhanHienThiService nhanHienThiService,
			IQuyenService quyenService,
			IWorkContext workContext,
			CauHinhChung cauhinhChung,
			ITaiSanChoThueModelFactory itemModelFactory,
			ITaiSanChoThueService itemService,
			ITaiSanService taisanService,
			ITaiSanModelFactory taisanModelFactory,
			IDoiTacModelFactory doitacModelFactory,
			IDoiTacService doitacService
			)
		{
			this._hoatdongService = hoatdongService;
			this._nhanHienThiService = nhanHienThiService;
			this._quyenService = quyenService;
			this._workContext = workContext;
			this._cauhinhChung = cauhinhChung;
			this._itemModelFactory = itemModelFactory;
			this._itemService = itemService;
			this._taisanService = taisanService;
			this._taisanModelFactory = taisanModelFactory;
			this._doitacModelFactory = doitacModelFactory;
			this._doitacService = doitacService;
		}
		#endregion
		#region Methods

		public virtual IActionResult List()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new TaiSanChoThueSearchModel();
			var model = _itemModelFactory.PrepareTaiSanChoThueSearchModel(searchmodel);
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult List(TaiSanChoThueSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan))
				return AccessDeniedKendoGridJson();
			//prepare model
			var model = _itemModelFactory.PrepareTaiSanChoThueListModel(searchModel);
			return Json(model);
		}

		public virtual IActionResult Create(Guid guid)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan))
				return AccessDeniedView();
			//prepare model
			var model = new TaiSanChoThueModel();
			var ts = _taisanService.GetTaiSanByGuId(guid);
			model.TAI_SAN_ID = ts.ID;
			model.ngaykekhaits = ts.NGAY_NHAP;
			model = _itemModelFactory.PrepareTaiSanChoThueModel(model, null);
			return View(model);
		}

		[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
		public virtual IActionResult Create(TaiSanChoThueModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan))
				return AccessDeniedView();

			if (ModelState.IsValid)
			{
				var item = model.ToEntity<TaiSanChoThue>();
				_itemService.InsertTaiSanChoThue(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<TaiSanChoThueModel>(), "TaiSanChoThue");
				SuccessNotification("Tạo mới dữ liệu thành công !");
				return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
			}
			var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			//prepare model
			model = _itemModelFactory.PrepareTaiSanChoThueModel(model, null);
			return View(model);
		}

		public virtual IActionResult Edit(decimal Id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan))
				return AccessDeniedView();

			var item = _itemService.GetTaiSanChoThueById(Id);
			var ts = _taisanService.GetTaiSanById(item.TAI_SAN_ID);
			if (item == null)
				return RedirectToAction("List");
			//prepare model
			var model = _itemModelFactory.PrepareTaiSanChoThueModel(null, item);
			model.ngaykekhaits = ts.NGAY_NHAP;
			return View(model);
		}

		[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
		[FormValueRequired("save", "save-continue")]
		public virtual IActionResult Edit(TaiSanChoThueModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetTaiSanChoThueById(model.ID);
			if (item == null)
				return RedirectToAction("List");
			if (ModelState.IsValid)
			{
				_itemModelFactory.PrepareTaiSanChoThue(model, item);
				_itemService.UpdateTaiSanChoThue(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<TaiSanChoThueModel>(), "TaiSanChoThue");
				SuccessNotification("Cập nhật dữ liệu thành công !");
				return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
			}
			//prepare model
			model = _itemModelFactory.PrepareTaiSanChoThueModel(model, item, true);
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult Delete(int id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetTaiSanChoThueById(id);
			if (item == null)
				return RedirectToAction("List");
			try
			{
				_itemService.DeleteTaiSanChoThue(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<TaiSanChoThueModel>(), "TaiSanChoThue");
				//activity log  
				return JsonSuccessMessage("Xoá dữ liệu thành công");
			}
			catch (Exception exc)
			{
				ErrorNotification(exc);
				return RedirectToAction("Edit", new { id = item.ID });
			}
		}

		[HttpPost]
		public virtual IActionResult CheckLoaiDonViToChuc(decimal id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan))
				return AccessDeniedView();
			var result = _itemService.CheckLoaiDonViToChuc(id);
			var message = "Loại hình này không thuộc đối tượng sử dụng chung";
			return JsonSuccessMessage(objdata:result,msg:message);
		}

		public virtual IActionResult _ChonTaiSan()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan))
				return AccessDeniedView();
			var searchmodel = new TaiSanSearchModel();
			var model = _taisanModelFactory.PrepareTaiSanSearchModel(searchmodel);
			return PartialView(model);
		}

		[HttpPost]
		public virtual IActionResult _ChonTaiSan(TaiSanSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan))
				return AccessDeniedView();
			searchModel.donviId = _workContext.CurrentDonVi.ID;
			searchModel.TRANG_THAI_ID = (int)GS.Core.Domain.TaiSans.enumTRANG_THAI_TAI_SAN.DA_DUYET;
            searchModel.isDuyet = false;
			var model = _taisanModelFactory.PrepareDanhSachTaiSan(searchModel);
			return Json(model);
		}
		public virtual IActionResult SearchDoiTac(string TenDoiTac = null)

		{
			var items = _doitacService.GetDoiTacs(DonViId: _workContext.CurrentDonVi.ID, isComboBox: true, keySearch: TenDoiTac);
			var md = Json(items.Select(c =>
			{
				var m = c.ToModel<DoiTacModel>();
				return m;
			}
			).ToList());
			return md;
		}

		public virtual IActionResult _DetailTaiSanChoThue(decimal id)
		{
			var entity = _itemService.GetTaiSanChoThueById(id);
			var model = _itemModelFactory.PrepareTaiSanChoThueModel(model: new TaiSanChoThueModel(), item: entity);
			return PartialView(model);
		}
		#endregion
	}
}

