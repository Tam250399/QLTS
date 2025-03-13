//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.HeThong;
//using GS.Services.Events;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Controllers;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Linq;

namespace GS.Web.HeThong.Controllers
{
	[HttpsRequirement(SslRequirement.No)]
	public partial class VaiTroController : BaseWorksController
	{
		#region Fields
		private readonly INhanHienThiService _NhanHienThiService;
		private readonly IWorkContext _workContext;
		private readonly IVaiTroModelFactory _itemModelFactory;
		private readonly IVaiTroService _itemService;
		private readonly IQuyenVaiTroService _quyenVaiTroService;
		private readonly IQuyenService _quyenService;
		private readonly IQuyenModelFactory _quyenModelFactory;
		private readonly IHoatDongService _hoatdongService;
		private readonly INguoiDungModelFactory _nguoiDungModelFactory;
		private readonly INguoiDungService _nguoiDungService;
		private readonly IVaiTroNguoiDungService _nguoiDungVaiTroService;
		private readonly IVaiTroWidgetService _vaiTroWidgetService;
		private readonly ICauHinhService _cauHinhService;
		private readonly ICacheManager _cacheManager;

		#endregion

		#region Ctor
		public VaiTroController(
			INhanHienThiService NhanHienThiService,
			IWorkContext workContext,
			IVaiTroModelFactory itemModelFactory,
			IVaiTroService itemService,
			IQuyenVaiTroService quyenVaiTroService,
			IQuyenService quyenService,
			IHoatDongService hoatDongService,
			IQuyenModelFactory quyenModelFactory,
			INguoiDungModelFactory nguoiDungModelFactory,
			INguoiDungService nguoiDungService,
			IVaiTroNguoiDungService nguoiDungVaiTroService,
			IVaiTroWidgetService vaiTroWidgetService,
			ICauHinhService cauHinhService,
			ICacheManager cacheManager
			)
		{
			this._hoatdongService = hoatDongService;
			this._quyenService = quyenService;
			this._quyenVaiTroService = quyenVaiTroService;
			this._NhanHienThiService = NhanHienThiService;
			this._workContext = workContext;
			this._itemModelFactory = itemModelFactory;
			this._itemService = itemService;
			this._quyenModelFactory = quyenModelFactory;
			this._nguoiDungModelFactory = nguoiDungModelFactory;
			this._nguoiDungService = nguoiDungService;
			this._nguoiDungVaiTroService = nguoiDungVaiTroService;
			this._vaiTroWidgetService = vaiTroWidgetService;
			this._cauHinhService = cauHinhService;
			this._cacheManager = cacheManager;
		}
		#endregion
		#region Methods

		public virtual IActionResult List()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLVaiTro))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new VaiTroSearchModel();
			var model = _itemModelFactory.PrepareVaiTroSearchModel(searchmodel);
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult List(VaiTroSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLVaiTro))
				return AccessDeniedView();
			//prepare model
			if (searchModel.PageSize == 0) searchModel.PageSize = 10;
			var model = _itemModelFactory.PrepareVaiTroListModel(searchModel);
			return Json(model);
		}

		public virtual IActionResult Create()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLVaiTro))
				return AccessDeniedView();
			//prepare model
			var model = _itemModelFactory.PrepareVaiTroModel(new VaiTroModel(), null);
			return View(model);
		}

		[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
		public virtual IActionResult Create(VaiTroModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLVaiTro))
				return AccessDeniedView();
			if (ModelState.IsValid)
			{
				var item = model.ToEntity<VaiTro>();
				item.NGAY_TAO = DateTime.Now;

				_itemService.InsertVaiTro(item);
				if (model.SelectedWidgetIds != null)
				{
					foreach (var w in model.SelectedWidgetIds)
					{
						var vtwg = new VaiTroWidget();
						vtwg.VAI_TRO_ID = item.ID;
						vtwg.WIDGET_ID = w;
						_vaiTroWidgetService.InsertVaiTroWidget(vtwg);
					}
				}
				_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin", item.ToModel<VaiTroModel>(), "VaiTro");
				SuccessNotification("Tạo mới dữ liệu thành công !");
				return RedirectToAction("Edit", new { id = item.ID });
			}
			//prepare model
			model = _itemModelFactory.PrepareVaiTroModel(model, null);
			return View(model);
		}

		public virtual IActionResult Edit(int Id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLVaiTro))
				return AccessDeniedView();
			var item = _itemService.GetVaiTroById(Id);
			if (item == null)
				return RedirectToAction("List");
			//prepare model
			var model = _itemModelFactory.PrepareVaiTroModel(null, item);
			return View(model);
		}

		[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
		[FormValueRequired("save", "save-continue")]
		public virtual IActionResult Edit(VaiTroModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLVaiTro))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetVaiTroById(model.ID);
			if (item == null)
				return RedirectToAction("List");
			if (ModelState.IsValid)
			{
				//var obj = item;
				_itemModelFactory.PrepareVaiTro(model, item);
				item.NGAY_TAO = DateTime.Now;
				_itemService.UpdateVaiTro(item);
				if (model.SelectedWidgetIds != null)
				{
					if (model.SelectedWidgetIds.Count > 0)
					{
						var allItem = _vaiTroWidgetService.GetVaiTroWidgetsByVaiTro(item.ID);
						foreach (var oldItem in allItem)
						{
							_vaiTroWidgetService.DeleteVaiTroWidget(oldItem);
						}
						if (model.SelectedWidgetIds[0] != 0)
						{
							foreach (var w in model.SelectedWidgetIds)
							{
								var newVTWG = new VaiTroWidget();
								newVTWG.VAI_TRO_ID = item.ID;
								newVTWG.WIDGET_ID = w;
								_vaiTroWidgetService.InsertVaiTroWidget(newVTWG);
							}
						}
					}
				}

				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<VaiTroModel>(), "VaiTro");
				SuccessNotification("Cập nhật dữ liệu thành công !");
				return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
			}
			//prepare model
			model = _itemModelFactory.PrepareVaiTroModel(model, item, true);
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult Delete(int id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLVaiTro))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetVaiTroById(id);
			if (item == null)
				return RedirectToAction("List");
			try
			{
				var ndmaps = _nguoiDungVaiTroService.GetMapByVaiTroId(id);
				if (ndmaps != null && ndmaps.Count() > 0)
				{
					ErrorNotification("Vai trò đang gán cho người dùng");
					return RedirectToAction("Edit", new { id = item.ID });
				}
				var maps = _quyenVaiTroService.GetMapByVaiTroId(id);
				if (maps != null && maps.Count() > 0)
				{
					foreach (var map in maps)
					{
						_quyenVaiTroService.DeleteQuyenVaiTro(map);
					}
				}
				_itemService.DeleteVaiTro(item);
				//activity log  
				_hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<VaiTroModel>(), "VaiTro");
				SuccessNotification("Xoá dữ liệu thành công");
				return RedirectToAction("List");
			}
			catch (Exception exc)
			{
				ErrorNotification(exc);
				return RedirectToAction("Edit", new { id = item.ID });
			}
		}
		public virtual IActionResult _ListQuyen(decimal idVaiTro)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLVaiTro))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new QuyenSearchModel();
			var model = _quyenModelFactory.PrepareQuyenSearchModel(searchmodel);
			searchmodel.idVaiTro = idVaiTro;
			return PartialView(model);
		}

		[HttpPost]
		public virtual IActionResult _ListQuyen(QuyenSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLVaiTro))
				return AccessDeniedView();
			//prepare model 
			var model = _quyenModelFactory.PrepareQuyenListModel(searchModel);
			return Json(model);
		}

		[HttpPost]
		public virtual IActionResult _Addquyen(decimal QUYEN_ID, decimal Id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLVaiTro))
				return AccessDeniedView();
			//try to get a store with the specified id

			var quyenvaitro = new QuyenVaiTroMapping();
			quyenvaitro.QUYEN_ID = QUYEN_ID;
			quyenvaitro.VAI_TRO_ID = Id;
			_quyenVaiTroService.InsertQuyenVaiTro(quyenvaitro);
			var item = _quyenService.GetQuyenById(QUYEN_ID);
			var model = item.ToModel<QuyenModel>();
			SuccessNotification("Cập nhật dữ liệu thành công");
			//clear Cache

			

			return PartialView(model);
		}
		[HttpPost]
		public virtual IActionResult _deleteQuyenvaitro(decimal QUYEN_ID, decimal Id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLVaiTro))
				return AccessDeniedView();
			//try to get a store with the specified id
			var maps = _quyenVaiTroService.GetMapByVaiTroId(Id);
			_quyenVaiTroService.DeleteQuyenVaiTroId(Id, QUYEN_ID);
			//activity log
			SuccessNotification("Xoá dữ liệu thành công");
			
			return Json(maps);
		}
		public virtual IActionResult _ListNguoiDung(decimal idVaiTro)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLVaiTro))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new NguoiDungSearchModel();
			var model = _nguoiDungModelFactory.PrepareNguoiDungSearchModel(searchmodel);
			searchmodel.idVaiTro = idVaiTro;
			return PartialView(model);
		}

		[HttpPost]
		public virtual IActionResult _ListNguoiDung(NguoiDungSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLVaiTro))
				return AccessDeniedView();
			//prepare model 
			var model = _nguoiDungModelFactory.PrepareNguoiDungListModel(searchModel);
			return Json(model);
		}
		[HttpPost]
		public virtual IActionResult _AddNguoiDung(decimal NGUOI_DUNG_ID, decimal Id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLVaiTro))
				return AccessDeniedView();
			//try to get a store with the specified id

			var nguoiDungVaiTro = new VaiTroNguoiDungMapping();
			nguoiDungVaiTro.NGUOI_DUNG_ID = NGUOI_DUNG_ID;
			nguoiDungVaiTro.VAI_TRO_ID = Id;
			_nguoiDungVaiTroService.InsertVaiTroNguoiDung(nguoiDungVaiTro);
			var item = _nguoiDungService.GetNguoiDungById(NGUOI_DUNG_ID);
			var model = item.ToModel<NguoiDungModel>();
			SuccessNotification("Cập nhật dữ liệu thành công");
			return PartialView(model);
		}
		[HttpPost]
		public virtual IActionResult _deleteNguoiDungvaitro(decimal NGUOI_DUNG_ID, decimal Id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLVaiTro))
				return AccessDeniedView();
			//try to get a store with the specified id
			var maps = _nguoiDungVaiTroService.GetMapByVaiTroId(Id);
			_nguoiDungVaiTroService.DeleteVaiTroNguoiDung(Id, NGUOI_DUNG_ID);
			//activity log
			SuccessNotification("Xoá dữ liệu thành công");
			return Json(maps);
		}
		#endregion
	}
}

