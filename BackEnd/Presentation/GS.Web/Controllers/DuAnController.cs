//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.DongBo;
using GS.Web.Framework.Controllers;
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
	public partial class DuAnController : BaseWorksController
	{
		#region Fields
		private readonly IHoatDongService _hoatdongService;
		private readonly INhanHienThiService _nhanHienThiService;
		private readonly IQuyenService _quyenService;
		private readonly IWorkContext _workContext;
		private readonly CauHinhChung _cauhinhChung;
		private readonly IDuAnModelFactory _itemModelFactory;
		private readonly IDuAnService _itemService;
		private readonly IDonViService _donViService;
		private readonly IDonViModelFactory _donViModelFactory;
		private readonly IDongBoFactory _dongBoFactory;
		#endregion

		#region Ctor
		public DuAnController(
			IHoatDongService hoatdongService,
			INhanHienThiService nhanHienThiService,
			IQuyenService quyenService,
			IWorkContext workContext,
			CauHinhChung cauhinhChung,
			IDuAnModelFactory itemModelFactory,
			IDuAnService itemService,
			IDonViService donViService,
			IDonViModelFactory donViModelFactory,
			IDongBoFactory dongBoFactory
			)
		{
			this._hoatdongService = hoatdongService;
			this._nhanHienThiService = nhanHienThiService;
			this._quyenService = quyenService;
			this._workContext = workContext;
			this._cauhinhChung = cauhinhChung;
			this._itemModelFactory = itemModelFactory;
			this._itemService = itemService;
			this._donViService = donViService;
			this._donViModelFactory = donViModelFactory;
			this._dongBoFactory = dongBoFactory;
		}
		#endregion
		#region Methods

		public virtual IActionResult List(int? pageindex = 1)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDuAn))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new DuAnSearchModel();
			var model = _itemModelFactory.PrepareDuAnSearchModel(searchmodel);
			if (_donViService.isDonViBanQuanLyDuAn(donViId: _workContext.CurrentDonVi.ID))
			{
				DonVi donViDuAn = _donViService.GetDonViBanQuanLyDuAn(donViId: _workContext.CurrentDonVi.ID, isDonViBanQuanLyDuAn: true);
				if (donViDuAn != null)
				{
					searchmodel.donviId = donViDuAn.ID;
				}
			}
			else
			{
				searchmodel.donviId = _workContext.CurrentDonVi.ID;
			}
			if (pageindex > 0)
			{
				searchmodel.Page = pageindex??1;
			}
			var _searchModel = HttpContext.Session.GetObject<DuAnSearchModel>(enumTENCAUHINH.KeySearch + typeof(DuAnSearchModel).Name);
			if (_searchModel != null && _searchModel.IsLoadSession)
			{
				model.pageIndex = _searchModel.pageIndex;
				model.Page = _searchModel.Page;
				model.PageSize = _searchModel.PageSize;
				model.KeySearch = _searchModel.KeySearch;
				model.donviId = _searchModel.donviId;
				model.TRANG_THAI_ID = _searchModel.TRANG_THAI_ID;
				model.LOAI_DU_AN_ID = _searchModel.LOAI_DU_AN_ID;
				UpdateSessionSearchModel<DuAnSearchModel>(false);
			}
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult List(DuAnSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDuAn))
				return AccessDeniedKendoGridJson();
			//prepare model
			var model = _itemModelFactory.PrepareDuAnListModel(searchModel);

			return Json(model);
		}

		public virtual IActionResult Create()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDuAn))
				return AccessDeniedView();
			//prepare model
			var model = _itemModelFactory.PrepareDuAnModel(new DuAnModel(), null);
			return View(model);
		}

		[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
		public virtual IActionResult Create(DuAnModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDuAn))
				return AccessDeniedView();

			if (ModelState.IsValid)
			{
				DonVi donViQuanLyDuAn = _donViService.GetDonViBanQuanLyDuAn(donViId: _workContext.CurrentDonVi.ID, isDonViBanQuanLyDuAn: true);
				if (donViQuanLyDuAn != null)
				{
					model.DON_VI_ID = donViQuanLyDuAn.ID;
				}
				else
				{
					//Nếu đơn vị này k có đơn vị con quản lý tsc và tsda
					//Tạo 2 đơn vị con
					DonVi donViTSC = _donViModelFactory.PrepareDonViConChoBQLDA(parentId: _workContext.CurrentDonVi.ID, pLA_BAN_QL_DU_AN: false);
					_donViService.InsertDonVi(donViTSC);
					DonVi donViTSDA = _donViModelFactory.PrepareDonViConChoBQLDA(parentId: _workContext.CurrentDonVi.ID, pLA_BAN_QL_DU_AN: true);
					_donViService.InsertDonVi(donViTSDA);
					model.DON_VI_ID = donViTSDA.ID;
				}
				var item = model.ToEntity<DuAn>();
				_itemService.InsertDuAn(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<DuAnModel>(), "DuAn");
				model.ID = item.ID;
				_dongBoFactory.DongBoDanhMuc<DuAnModel>(new List<DuAnModel>() { model });
				SuccessNotification("Tạo mới dữ liệu thành công !");
				return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
			}

			//prepare model
			model = _itemModelFactory.PrepareDuAnModel(model, null);
			return View(model);
		}


		public virtual IActionResult _Create()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDuAn))
				return AccessDeniedView();
			//prepare model
			var model = _itemModelFactory.PrepareDuAnModel(new DuAnModel(), null);
			return PartialView(model);
		}
		[HttpPost]
		public virtual IActionResult _Create(DuAnModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDuAn))
				return AccessDeniedView();

			if (ModelState.IsValid)
			{
				DonVi donViQuanLyDuAn = _donViService.GetDonViBanQuanLyDuAn(donViId: _workContext.CurrentDonVi.ID, isDonViBanQuanLyDuAn: true);
				if (donViQuanLyDuAn != null)
				{
					model.DON_VI_ID = donViQuanLyDuAn.ID;
				}
				else
				{
					//Nếu đơn vị này k có đơn vị con quản lý tsc và tsda
					//Tạo 2 đơn vị con
					DonVi donViTSC = _donViModelFactory.PrepareDonViConChoBQLDA(parentId: _workContext.CurrentDonVi.ID, pLA_BAN_QL_DU_AN: false);
					_donViService.InsertDonVi(donViTSC);
					DonVi donViTSDA = _donViModelFactory.PrepareDonViConChoBQLDA(parentId: _workContext.CurrentDonVi.ID, pLA_BAN_QL_DU_AN: true);
					_donViService.InsertDonVi(donViTSDA);
					model.DON_VI_ID = donViTSDA.ID;
				}
				var item = model.ToEntity<DuAn>();
				_itemService.InsertDuAn(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<DuAnModel>(), "DuAn");
				model.ID = item.ID;
				model.dllDuAn = _itemModelFactory.PrepareSelectListDuAn(donViId: model.DON_VI_ID, isAddFirst: true, valSelected: item.ID);
				//  model.ID = item.ID;
				_dongBoFactory.DongBoDanhMuc<DuAnModel>(new List<DuAnModel>() { model });
				return JsonSuccessMessage("Tạo mới dữ liệu thành công!", model);
			}

			//prepare model
			var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			//prepare model
			return JsonErrorMessage("Error", list);
		}
		public virtual IActionResult Edit(int id, int? pageIndex = 1)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDuAn))
				return AccessDeniedView();

			var item = _itemService.GetDuAnById(id);
			if (item == null)
				return RedirectToAction("List");
			//prepare model
			var model = _itemModelFactory.PrepareDuAnModel(null, item);
			model.pageIndex = pageIndex ?? 1;
			return View(model);
		}

		[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
		[FormValueRequired("save", "save-continue")]
		public virtual IActionResult Edit(DuAnModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDuAn))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetDuAnById(model.ID);
			if (item == null)
				return RedirectToAction("List");
			if (ModelState.IsValid)
			{
				_itemModelFactory.PrepareDuAn(model, item);
				_itemService.UpdateDuAn(item);
				_dongBoFactory.DongBoDanhMuc<DuAnModel>(new List<DuAnModel>() { model });
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<DuAnModel>(), "DuAn");

				SuccessNotification("Cập nhật dữ liệu thành công !");
				return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List", new { pageIndex = model.pageIndex });
			}
			//prepare model
			model = _itemModelFactory.PrepareDuAnModel(model, item, true);
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult Delete(int id, int? pageIndex = 1)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDuAn))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetDuAnById(id);
			if (item == null)
				return RedirectToAction("List");
			try
			{
				DuAnModel model = item.ToModel<DuAnModel>();
				model.pageIndex = pageIndex ?? 1;
				_itemService.DeleteDuAn(item);
				_dongBoFactory.XoaDanhMuc<DuAnModel>(model);
				_hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<DuAnModel>(), "DuAn");
				//activity log  
				return JsonSuccessMessage("Xoá dữ liệu thành công");
				//return RedirectToAction("List");
			}
			catch (Exception exc)
			{
				ErrorNotification(exc);
				return RedirectToAction("Edit", new { id = item.ID });
			}
		}


		#endregion
	}
}

