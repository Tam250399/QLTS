//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Infrastructure;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.DongBo;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Extensions;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.ImportTaiSan;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GS.Web.Controllers
{
	[HttpsRequirement(SslRequirement.No)]
	public partial class DongXeController : BaseWorksController
	{
		#region Fields
		private readonly IHoatDongService _hoatdongService;
		private readonly INhanHienThiService _nhanHienThiService;
		private readonly IQuyenService _quyenService;
		private readonly IWorkContext _workContext;
		private readonly CauHinhChung _cauhinhChung;
		private readonly IDongXeModelFactory _itemModelFactory;
		private readonly IDongXeService _itemService;
		private readonly IGSFileProvider _fileProvider;
		private readonly INhanXeService _nhanXeService;
		private readonly IDongBoFactory _dongBoFactory;
		private readonly ICauHinhModelFactory _cauHinhModelFactory;
		#endregion

		#region Ctor
		public DongXeController(
			IHoatDongService hoatdongService,
			INhanHienThiService nhanHienThiService,
			IQuyenService quyenService,
			IWorkContext workContext,
			CauHinhChung cauhinhChung,
			IDongXeModelFactory itemModelFactory,
			IDongXeService itemService,
			INhanXeService nhanXeService,
			IGSFileProvider gSFileProvider,
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
			this._nhanXeService = nhanXeService;
			this._fileProvider = gSFileProvider;
			this._dongBoFactory = dongBoFactory;
			_cauHinhModelFactory = cauHinhModelFactory;
		}
		#endregion
		#region Methods

		public virtual IActionResult List(int? pageIndex = 0)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMDongXe))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new DongXeSearchModel();
			var model = _itemModelFactory.PrepareDongXeSearchModel(searchmodel);
			if (pageIndex > 0)
			{
				searchmodel.Page = (int)pageIndex;
			}
			var _searchModel = HttpContext.Session.GetObject<DongXeSearchModel>(enumTENCAUHINH.KeySearch + typeof(DongXeSearchModel).Name);
			if (_searchModel != null && _searchModel.IsLoadSession)
			{
				_cauHinhModelFactory.PrePareModelBySession(model, _searchModel);
				UpdateSessionSearchModel<DongXeSearchModel>(false);
			}
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult List(DongXeSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMDongXe))
				return AccessDeniedKendoGridJson();
			//prepare model
			var model = _itemModelFactory.PrepareDongXeListModel(searchModel);
			HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch + searchModel.GetType().Name, searchModel);
			return Json(model);
		}

		public virtual IActionResult Create()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMDongXe))
				return AccessDeniedView();
			//prepare model
			var model = _itemModelFactory.PrepareDongXeModel(new DongXeModel(), null);
			return View(model);
		}

		[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
		public virtual IActionResult Create(DongXeModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMDongXe))
				return AccessDeniedView();

			if (ModelState.IsValid)
			{
				var item = model.ToEntity<DongXe>();
				_itemService.InsertDongXe(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<DongXeModel>(), "DongXe");
				model.ID = item.ID;
				_dongBoFactory.DongBoDanhMuc<DongXeModel>(new List<DongXeModel>() { model });
				SuccessNotification("Tạo mới dữ liệu thành công !");
				return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
			}

			//prepare model
			model = _itemModelFactory.PrepareDongXeModel(model, null);
			return View(model);
		}

		public virtual IActionResult Edit(int id, int pageIndex)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMDongXe))
				return AccessDeniedView();

			var item = _itemService.GetDongXeById(id);
			if (item == null)
				return RedirectToAction("List");
			//prepare model
			var model = _itemModelFactory.PrepareDongXeModel(null, item);
			model.pageIndex = pageIndex;
			return View(model);
		}

		[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
		[FormValueRequired("save", "save-continue")]
		public virtual IActionResult Edit(DongXeModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMDongXe))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetDongXeById(model.ID);
			if (item == null)
				return RedirectToAction("List");
			if (ModelState.IsValid)
			{
				_itemModelFactory.PrepareDongXe(model, item);
				_itemService.UpdateDongXe(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<DongXeModel>(), "DongXe");
				model.ID = item.ID;
				_dongBoFactory.DongBoDanhMuc<DongXeModel>(new List<DongXeModel>() { model });
				SuccessNotification("Cập nhật dữ liệu thành công !");
				UpdateSessionSearchModel<DongXeSearchModel>(true);
				return continueEditing ? RedirectToAction("Edit", new { id = item.ID, pageIndex = model.pageIndex }) : RedirectToAction("List", new { pageIndex = model.pageIndex });
			}
			//prepare model
			model = _itemModelFactory.PrepareDongXeModel(model, item, true);
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult Delete(int id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMDongXe))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetDongXeById(id);
			if (item == null)
				return RedirectToAction("List");
			try
			{
				DongXeModel model = item.ToModel<DongXeModel>();
				_itemService.DeleteDongXe(item);
				_dongBoFactory.XoaDanhMuc<DongXeModel>(model);
				_hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<DongXeModel>(), "DongXe");
				//activity log  
				UpdateSessionSearchModel<DongXeSearchModel>(true);
				return JsonSuccessMessage("Xoá dữ liệu thành công");
			}
			catch (Exception exc)
			{
				ErrorNotification(exc);
				return RedirectToAction("Edit", new { id = item.ID });
			}
		}

		public virtual IActionResult GetDongXesByNhanXeId(decimal nhanXeId)
		{
			IList<SelectListItem> selectList = new List<SelectListItem>();
			if (nhanXeId > 0)
			{
				selectList = _itemModelFactory.PrepareSelectDongXe(nhanXeId: nhanXeId, isAddFirst: true);

			}else
				selectList.AddFirstRow(TextDisplay: "-- Chọn số loại --", ValueFirst: "");
			return Json(selectList);
		}
		public virtual IActionResult ImportDongXe()
        {
			return View();
        }
		[HttpPost]
		[RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
		[RequestSizeLimit(209715200)]
		public virtual IActionResult ImportDongXe(IFormFile file)
		{
			if (file == null)
			{
				ErrorNotification("Bạn chưa Nhập file Import");
				// return RedirectToAction("DongBoTaiSan");
			}
			Workbook workbook = new Workbook();
			workbook.LoadFromStream(file.OpenReadStream());
			//  lưu file 
			string _fileStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), DateTime.Now.ToPathFolderStore(), Guid.NewGuid().ToString() + ".xlsx");
			workbook.SaveToFile(_fileStore, ExcelVersion.Version2013);
			var fileInfo = new FileInfo(_fileStore);
			var package = new ExcelPackage(fileInfo);
			ExcelWorksheets workSheets = package.Workbook.Worksheets;
			List<MessageReturn> ListResult = new List<MessageReturn>();
			if (workSheets.Count < 1)
			{
				//ErrorMessageJson("File không chuẩn dữ liệu");
				return null;
			}
			ExcelWorksheet dongxe = workSheets[0];
			List<ExcelModel> lstSettings = new List<ExcelModel>();
			string strSettings = _fileProvider.ReadAllText(_fileProvider.MapPath("~/App_Data/jsonSetting_ImportExcel_DmDongXe.json"), Encoding.UTF8);
			lstSettings = strSettings.toEntities<ExcelModel>();
			int maxCol = lstSettings != null ? lstSettings.Max(c => c.COL) : 0;
			if (maxCol < dongxe.Dimension.End.Column)
				ErrorNotification("Thông tin tài sản đất-nhà Không đúng định dạng");
			List<ImportDmDongXeModel> lstImport = new List<ImportDmDongXeModel>();
			List<ImportDmDongXeModel> ListErr = new List<ImportDmDongXeModel>();
			for (int rowNumber = 2; rowNumber <= dongxe.Dimension.End.Row; rowNumber++)
			{
				ExcelRange row = dongxe.Cells[rowNumber, 1, rowNumber, dongxe.Dimension.End.Column];
				ImportDmDongXeModel item = new ImportDmDongXeModel();
				item = row.toEntity<ImportDmDongXeModel>(item, rowNumber, lstSettings);
				if (string.IsNullOrEmpty(item.TenDongXe) && string.IsNullOrEmpty(item.TenNhanXe))
				{

					continue;
				}
				lstImport.Add(item);
			}
			var IdMaxNhanXe = _nhanXeService.GetAllNhanXes().Max(m => m.ID);
			var IdMaxDongXe = _itemService.GetAllDongXes().Max(m => m.ID);
			foreach (var item in lstImport)
			{
				try
				{
					// check nhãn xe theo tên
					NhanXe nhanXe = _nhanXeService.GetNhanXeByMaTen(Ten: item.TenNhanXe);
					
					if (nhanXe == null)
					{
						nhanXe = new NhanXe();
						nhanXe.MA = (IdMaxNhanXe+1).ToString();
						nhanXe.TEN = item.TenNhanXe;
						_nhanXeService.InsertNhanXe(nhanXe);
						IdMaxNhanXe = nhanXe.ID;
					}
					DongXe dongXe = _itemService.GetDongXeByMa(Ten: item.TenDongXe);
					if (dongXe == null)
					{
						dongXe = new DongXe();
						dongXe.TEN = item.TenDongXe;
						dongXe.NHAN_XE_ID = nhanXe.ID;
						dongXe.MA = (IdMaxDongXe+1).ToString();
						_itemService.InsertDongXe(dongXe);
						IdMaxDongXe = dongXe.ID;
					}
                    else
                    {
						if(dongXe.NHAN_XE_ID!=nhanXe.ID)
                        {
							dongXe = new DongXe();
							dongXe.TEN = item.TenDongXe;
							dongXe.NHAN_XE_ID = nhanXe.ID;
							dongXe.MA = (IdMaxDongXe + 1).ToString();
							_itemService.InsertDongXe(dongXe);
							IdMaxDongXe = dongXe.ID;
						}							
                    }
				}
				catch (Exception ex)
				{
					ListErr.Add(item);

				}
			}
		return Json(ListErr);
		}
		#endregion
	}
}

