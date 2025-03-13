//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 21/7/2020
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
using GS.Services.DanhMuc;
using System.Linq;
using GS.Core.Domain.DanhMuc;

namespace GS.Web.Controllers
{
	[HttpsRequirement(SslRequirement.No)]
	public partial class MuaSamController : BaseWorksController
	{
		#region Fields
		private readonly IHoatDongService _hoatdongService;
		private readonly INhanHienThiService _nhanHienThiService;
		private readonly IQuyenService _quyenService;
		private readonly IWorkContext _workContext;
		private readonly CauHinhChung _cauhinhChung;
		private readonly IMuaSamModelFactory _itemModelFactory;
		private readonly IMuaSamChiTietModelFactory _muaSamChiTietModelFactory;
		private readonly IMuaSamService _itemService;
		private readonly IMuaSamChiTietService _muaSamChiTietService;
		private readonly IDonViService _donViService;
		#endregion

		#region Ctor
		public MuaSamController(
			IHoatDongService hoatdongService,
			INhanHienThiService nhanHienThiService,
			IQuyenService quyenService,
			IWorkContext workContext,
			CauHinhChung cauhinhChung,
			IMuaSamModelFactory itemModelFactory,
			IMuaSamChiTietModelFactory muaSamChiTietModelFactory,
			IMuaSamChiTietService muaSamChiTietService,
			IDonViService donViService,
			IMuaSamService itemService
			)
		{
			this._hoatdongService = hoatdongService;
			this._nhanHienThiService = nhanHienThiService;
			this._quyenService = quyenService;
			this._workContext = workContext;
			this._cauhinhChung = cauhinhChung;
			this._itemModelFactory = itemModelFactory;
			this._muaSamChiTietModelFactory = muaSamChiTietModelFactory;
			this._muaSamChiTietService = muaSamChiTietService;
			this._itemService = itemService;
			this._donViService = donViService;
		}
		#endregion
		#region Methods

		public virtual IActionResult List()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKeHoachMuaSam))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new MuaSamSearchModel();
			var model = _itemModelFactory.PrepareMuaSamSearchModel(searchmodel);
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult List(MuaSamSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKeHoachMuaSam))
				return AccessDeniedKendoGridJson();
			//prepare model
			var donvi = _workContext.CurrentDonVi;
			searchModel.DonviSD_ID = donvi.ID;
			var model = _itemModelFactory.PrepareMuaSamListModel(searchModel);
			return Json(model);
		}

		public virtual IActionResult Create()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKeHoachMuaSam))
				return AccessDeniedView();
			//create new muaSam record
			var item = new MuaSam();
			item.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.NHAP;
			var nguoidung = _workContext.CurrentCustomer;
			item.NGUOI_TAO_ID = nguoidung.ID;
			var donvi = _workContext.CurrentDonVi;
			var donvisd = _donViService.GetDonViById(donvi.ID);
			var iddvc1 = donvisd.TREE_NODE.Split("-").ToList();
			item.DVSDTS_ID = donvi.ID;
			item.DVLQTS_ID = Convert.ToDecimal(iddvc1.FirstOrDefault());
			item.NGAY_TAO = DateTime.Now;
			item.NAM = item.NGAY_TAO.Value.Year;
			_itemService.InsertMuaSam(item);
			//prepare model
			var model = _itemModelFactory.PrepareMuaSamModel(new MuaSamModel(), item);
			return View(model);
		}

		[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
		public virtual IActionResult Create(MuaSamModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKeHoachMuaSam))
				return AccessDeniedView();

			if (ModelState.IsValid)
			{

				var item = new MuaSam();
				item = model.ToEntity<MuaSam>();
				item.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.DA_DUYET;
				_itemService.InsertMuaSam(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<MuaSamModel>(), "MuaSam");
				SuccessNotification("Tạo mới dữ liệu thành công !");
				return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
			}

			//prepare model
			model = _itemModelFactory.PrepareMuaSamModel(model, null);
			return View(model);
		}

		[HttpGet]
		public virtual IActionResult Edit(int id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKeHoachMuaSam))
				return AccessDeniedView();

			var item = _itemService.GetMuaSamById(id);
			if (item == null)
				return RedirectToAction("List");
			//prepare model
			var model = _itemModelFactory.PrepareMuaSamModel(null, item);
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult Edit(MuaSamModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKeHoachMuaSam))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetMuaSamById(model.ID);
			if (item == null)
				return RedirectToAction("List");
			if (ModelState.IsValid)
			{
				_itemModelFactory.PrepareMuaSam(model, item);
				item.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.DA_DUYET;
				_itemService.UpdateMuaSam(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<MuaSamModel>(), "MuaSam");
				//SuccessNotification("Cập nhật dữ liệu thành công !");
				//return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
				return JsonSuccessMessage("Cập nhật dữ liệu thành công.", item.ToModel<MuaSamModel>());
			}
			//prepare model
			var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			return JsonErrorMessage("Error", list);
		}

		[HttpDelete]
		public virtual IActionResult Delete(int id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKeHoachMuaSam))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetMuaSamById(id);
			if (item == null)
				return RedirectToAction("List");
			try
			{
				//_muaSamChiTietService.DeleteMuaSamChiTietsByMuaSamId(id);
				//_itemService.DeleteMuaSam(item);
				item.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.XOA;
				_itemService.UpdateMuaSam(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu", item.ToModel<MuaSamModel>(), "MuaSam");
				//activity log  
				return JsonSuccessMessage("Xoá dữ liệu thành công");
				//SuccessNotification("Xoá dữ liệu thành công");
				//return RedirectToAction("List");
			}
			catch (Exception exc)
			{
				var err = exc.Message;
				return JsonErrorMessage(err);
				//return RedirectToAction("Edit", new { id = item.ID });
			}
		}
		[HttpDelete]
		public virtual IActionResult DeleteMuaSam(decimal? muaSamId = 0)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKeHoachMuaSam))
				return AccessDeniedView();
			//try to get a store with the specified id
			if (muaSamId > 0)
			{
				var ms = _itemService.GetMuaSamById((decimal)muaSamId);
				if (ms != null)
				{
					//xóa mua sắm chi tiết
					_muaSamChiTietService.DeleteMuaSamChiTietsByMuaSamId(muaSamId: ms.ID);
					//xóa mua sắm
					_itemService.DeleteMuaSam(ms);				
				}
				return JsonSuccessMessage("Xoá dữ liệu thành công");
			}
			//activity log
			//SuccessNotification("Xoá dữ liệu thành công");
			return JsonErrorMessage("Xoá dữ liệu không thành công");
		}
		#endregion
		#region Duyet Tai San mua sam
		public virtual IActionResult ListYeuCauDuyetMuaSam()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDuyetDangKy))
				return AccessDeniedView();
			//prepare model
			//var searchmodel = new DuyetDangKyTaiSanSearchModel();
			var searchModel = new MuaSamSearchModel();
			var model = _itemModelFactory.PrepareMuaSamSearchModel(searchModel);
			model.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET;
			return View(model);
		}

        


		[HttpPost]
		public virtual IActionResult ListYeuCauDuyetMuaSam(MuaSamSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDuyetDangKy))
				return AccessDeniedView();
			//prepare model
			//searchModel.isDuyet = true;
			var model = _itemModelFactory.PrepareMuaSamListModel(searchModel);

			return Json(model);
		}

		[HttpPost]
		public virtual IActionResult DuyetMuaSam(decimal MuaSamId)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDuyetDangKy))
				return AccessDeniedView();
			//var res = _thaoTacBienDongModelFactory.DuyetYeuCauFunc(YeuCauId);
			var res = _itemModelFactory.DuyetMuaSamFunc(MuaSamId);
			if (res.Result)
				return JsonSuccessMessage(res.Message);
			return JsonErrorMessage(res.Message);
		}
			
		[HttpPost]
		public virtual IActionResult DuyetMuaSams(string strMuaSamIds)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDuyetDangKy))
				return AccessDeniedView();
			var lstMuaSamId = strMuaSamIds.Split("-").ToList();
			foreach (var _strID in lstMuaSamId)
			{
				//sắp xếp theo ngày biến động tăng dần để duyệt từ từ
				var res = _itemModelFactory.DuyetMuaSamFunc(Convert.ToDecimal(_strID));
				if (!res.Result)
					return JsonErrorMessage(res.Message);
			}
			return JsonSuccessMessage(string.Format("Duyệt thành công {0} tài sản", lstMuaSamId.Count));
		}

		public virtual IActionResult _KhongDuyetMuaSam(decimal MuaSamId, string Note)
		{
			var muaSam = _itemService.GetMuaSamById(MuaSamId);
			if (muaSam != null)
			{
				var res = _itemModelFactory.KhongDuyetMuaSamFunc(muaSam.ID, Note);
				if (res.Result)
					return JsonSuccessMessage(res.Message);
				else
					return JsonErrorMessage(res.Message);
			}
			return JsonErrorMessage("Từ chối thất bại");
		}
		#endregion

		#region MuaSamChiTiet
		public virtual IActionResult _CreateMuaSamChiTiet(decimal? muaSamId = 0, decimal? taiSanMuaSamChiTietId = 0)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKeHoachMuaSam))
				return AccessDeniedView();
			//prepare model
			var model = new MuaSamChiTietModel();


			if (taiSanMuaSamChiTietId > 0)
			{
				var taisan = _muaSamChiTietService.GetMuaSamChiTietById(taiSanMuaSamChiTietId ?? 0);
				model = _muaSamChiTietModelFactory.PrepareMuaSamChiTietModel(model, taisan);
			}
			else
			{
				model.MUA_SAM_ID = muaSamId.Value;
				//var model = _taiSanModelFactory.PrepareTaiSanSearchModel(searchmodel);
				model = _muaSamChiTietModelFactory.PrepareMuaSamChiTietModel(model, null);
			}
			return PartialView(model);
		}

		[HttpPost]
		public virtual IActionResult CreateMuaSamChiTiet(MuaSamChiTietModel model)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKeHoachMuaSam))
				return AccessDeniedView();
			//prepare model 
			if (ModelState.IsValid)
			{
				var item = model.ToEntity<MuaSamChiTiet>();
				_muaSamChiTietService.InsertMuaSamChiTiet(item);
				return JsonSuccessMessage("Cập nhật dữ liệu thành công !");
			}
			//prepare model
			var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			return JsonErrorMessage("Error", list);
		}

		public virtual IActionResult _EditMuaSamChiTiet(decimal? muaSamId = 0, decimal? taiSanMuaSamChiTietId = 0)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKeHoachMuaSam))
				return AccessDeniedView();
			//prepare model
			
			var model = new MuaSamChiTietModel();
			if (taiSanMuaSamChiTietId > 0)
			{
				var taisan = _muaSamChiTietService.GetMuaSamChiTietById(taiSanMuaSamChiTietId ?? 0);
				model = _muaSamChiTietModelFactory.PrepareMuaSamChiTietModel(model, taisan);
			}
			else
			{
				model.MUA_SAM_ID = muaSamId.Value;
				//var model = _taiSanModelFactory.PrepareTaiSanSearchModel(searchmodel);
				model = _muaSamChiTietModelFactory.PrepareMuaSamChiTietModel(model, null);
			}
			return PartialView(model);
		}

		[HttpPost]
		public virtual IActionResult EditMuaSamChiTiet(MuaSamChiTietModel model)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKeHoachMuaSam))
				return AccessDeniedView();
			//prepare model 
			if (ModelState.IsValid)
			{

				var item = _muaSamChiTietService.GetMuaSamChiTietById(model.ID);
				_muaSamChiTietModelFactory.PrepareMuaSamChiTiet(model: model, item: item);
				_muaSamChiTietService.UpdateMuaSamChiTiet(item);
				return JsonSuccessMessage("Cập nhật dữ liệu thành công.");
			}
			//prepare model
			var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			return JsonErrorMessage("Error", list);
		}

		public virtual IActionResult _ListMuaSamChiTiet(decimal muaSamId)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKeHoachMuaSam))
				return AccessDeniedView();
			//prepare model
			MuaSamChiTietSearchModel searchModel = new MuaSamChiTietSearchModel();
			if (muaSamId > 0)
			{
				searchModel.MUA_SAM_ID = muaSamId;
			}

			var model = _muaSamChiTietModelFactory.PrepareMuaSamChiTietSearchModel(searchModel);
			return PartialView(model);
		}


		[HttpPost]
		public virtual IActionResult ListMuaSamChiTiet(MuaSamChiTietSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKeHoachMuaSam))
				return AccessDeniedKendoGridJson();
			//prepare model
			var model = _muaSamChiTietModelFactory.PrepareMuaSamChiTietListModel(searchModel);
			return Json(model);
		}

		[HttpDelete]
		public virtual IActionResult DeleteMuaSamChiTiet(decimal? muaSamId = 0, decimal? muaSamChiTietId = 0)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKeHoachMuaSam))
				return AccessDeniedView();
			//try to get a store with the specified id
			if (muaSamChiTietId > 0)
			{
				var itemMSCT = _muaSamChiTietService.GetMuaSamChiTietById(muaSamChiTietId.Value);
				_muaSamChiTietService.DeleteMuaSamChiTiet(itemMSCT);
				return JsonSuccessMessage("Xoá dữ liệu thành công");
			}
			else if (muaSamId > 0)
			{
				_muaSamChiTietService.DeleteMuaSamChiTietsByMuaSamId(muaSamId.Value);
				return JsonSuccessMessage("Xoá dữ liệu thành công");
			}
			//activity log
			//SuccessNotification("Xoá dữ liệu thành công");
			return JsonErrorMessage("Xoá dữ liệu không thành công");
		}


		#endregion
	}
}

