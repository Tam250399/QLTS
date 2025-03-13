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
using GS.Core.Domain.HeThong;
using GS.Core.Infrastructure;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Extensions;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GS.Web.Controllers
{
	[HttpsRequirement(SslRequirement.No)]
	public partial class DoiTacController : BaseWorksController
	{
		#region Fields

		private readonly IHoatDongService _hoatdongService;
		private readonly INhanHienThiService _nhanHienThiService;
		private readonly IQuyenService _quyenService;
		private readonly IWorkContext _workContext;
		private readonly CauHinhChung _cauhinhChung;
		private readonly IDoiTacModelFactory _itemModelFactory;
		private readonly IDoiTacService _itemService;
		private readonly IDonViBoPhanService _donViBoPhanService;
		private readonly IFileCongViecService _fileCongViecService;
		private readonly IGSFileProvider _fileProvider;
		private readonly IFileCongViecModelFactory _fileCongViecModelFactory;
		private readonly ICauHinhModelFactory _cauHinhModelFactory;

		#endregion Fields

		#region Ctor

		public DoiTacController(
			IHoatDongService hoatdongService,
			INhanHienThiService nhanHienThiService,
			IQuyenService quyenService,
			IWorkContext workContext,
			CauHinhChung cauhinhChung,
			IDoiTacModelFactory itemModelFactory,
			IDoiTacService itemService,
			IDonViBoPhanService donViBoPhanService,
			IFileCongViecService fileCongViecService,
			IGSFileProvider fileProvider,
			IFileCongViecModelFactory fileCongViecModelFactory,
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
			this._donViBoPhanService = donViBoPhanService;
			this._fileCongViecService = fileCongViecService;
			this._fileProvider = fileProvider;
			this._fileCongViecModelFactory = fileCongViecModelFactory;
			_cauHinhModelFactory = cauHinhModelFactory;
		}

		#endregion Ctor

		#region Methods

		public virtual IActionResult List()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDoiTac))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new DoiTacSearchModel();
			var model = _itemModelFactory.PrepareDoiTacSearchModel(searchmodel);
			var _searchModel = HttpContext.Session.GetObject<DoiTacSearchModel>(enumTENCAUHINH.KeySearch+typeof(DoiTacSearchModel).Name);
			if (_searchModel != null && _searchModel.IsLoadSession)
			{
				_cauHinhModelFactory.PrePareModelBySession(model, _searchModel);
				UpdateSessionSearchModel<DoiTacSearchModel>(false);
			}
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult List(DoiTacSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDoiTac))
				return AccessDeniedKendoGridJson();
			//prepare model
			var model = _itemModelFactory.PrepareDoiTacListModel(searchModel);
			HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch+searchModel.GetType().Name, searchModel);
			return Json(model);
		}

		public virtual IActionResult Create()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDoiTac))
				return AccessDeniedView();
			//prepare model
			var model = _itemModelFactory.PrepareDoiTacModel(new DoiTacModel(), null);
			return View(model);
		}

		[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
		public virtual IActionResult Create(DoiTacModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDoiTac))
				return AccessDeniedView();

			if (ModelState.IsValid)
			{
				var item = model.ToEntity<DoiTac>();				
				item.DON_VI_ID = model.DON_VI_ID;
                if (model.DON_VI_ID == 0)
                {
					item.DON_VI_ID = _workContext.CurrentDonVi.ID;

				}
				_itemService.InsertDoiTac(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<DoiTacModel>(), "DoiTac");
				SuccessNotification("Tạo mới dữ liệu thành công !");
				return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
			}
			//prepare model
			model = _itemModelFactory.PrepareDoiTacModel(model, null);
			return View(model);
		}

		public virtual IActionResult _Create()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDoiTac))
				return AccessDeniedView();
			//prepare model
			var model = _itemModelFactory.PrepareDoiTacModel(new DoiTacModel(), null);
			return PartialView(model);
		}

		[HttpPost]
		public virtual IActionResult _Create(DoiTacModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDoiTac))
				return AccessDeniedView();

			if (ModelState.IsValid)
			{
				var item = model.ToEntity<DoiTac>();
				//var phongban = _donViBoPhanService.GetDonViBoPhanById(item.DON_VI_BO_PHAN_ID ?? 0);
				item.DON_VI_ID = model.DON_VI_ID;
				if (model.DON_VI_ID == 0)
				{
					item.DON_VI_ID = _workContext.CurrentDonVi.ID;

				}
				_itemService.InsertDoiTac(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<DoiTacModel>(), "DoiTac");
				return JsonSuccessMessage("Tạo mới dữ liệu thành công!", item.ToModel<DoiTacModel>());
			}
			//prepare model
			var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			return JsonErrorMessage("Error", list);
		}

		public virtual IActionResult Edit(int id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDoiTac))
				return AccessDeniedView();

			var item = _itemService.GetDoiTacById(id);
			if (item == null)
				return RedirectToAction("List");
			//prepare model
			var model = _itemModelFactory.PrepareDoiTacModel(null, item);
			return View(model);
		}

		[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
		[FormValueRequired("save", "save-continue")]
		public virtual IActionResult Edit(DoiTacModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDoiTac))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetDoiTacById(model.ID);
			if (item == null)
				return RedirectToAction("List");
			if (ModelState.IsValid)
			{
				_itemModelFactory.PrepareDoiTac(model, item);
				_itemService.UpdateDoiTac(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<DoiTacModel>(), "DoiTac");
				SuccessNotification("Cập nhật dữ liệu thành công !");
				UpdateSessionSearchModel<DoiTacSearchModel>(true);
				return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
			}
			//prepare model
			model = _itemModelFactory.PrepareDoiTacModel(model, item, true);
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult Delete(int id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDoiTac))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetDoiTacById(id);
			if (item == null)
				return RedirectToAction("List");
			try
			{
				_itemService.DeleteDoiTac(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<DoiTacModel>(), "DoiTac");
				//activity log
				UpdateSessionSearchModel<DoiTacSearchModel>(true);
				return JsonSuccessMessage("Xoá dữ liệu thành công");
			}
			catch (Exception exc)
			{
				ErrorNotification(exc);
				return RedirectToAction("Edit", new { id = item.ID });
			}
		}
		public virtual IActionResult GetDoiTacsForInput()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucDoiTac))
				return AccessDeniedView();
			//try to get a store with the specified id
			var ddlDoiTac = _itemModelFactory.PrepareSelectListDoiTac(isAddFirst: true,strFirstRow: "---Chọn Đơn vị thuê---");
			return Json(ddlDoiTac);
			
		}

		public virtual IActionResult ImportDoiTac()
		{
			return View();
		}

		[HttpPost]
		//[RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
		//[RequestSizeLimit(209715200)]
		public IActionResult ImportDoiTacFromFile(IFormFile file)
		{
			if (file == null)
				return JsonErrorMessage("Bạn chưa nhập file đối tác");
			var dataByte = _fileCongViecService.GetWorkFileBits(file);
			var fileName = file.FileName;
			var fileExtension = _fileProvider.GetFileExtension(fileName);
			if (!string.IsNullOrEmpty(fileExtension))
				fileExtension = fileExtension.ToLowerInvariant();
			//  lưu file
			var contentType = file.ContentType;
			var fwork = new FileCongViec
			{
				GUID = Guid.NewGuid(),
				NOI_DUNG_FILE = null,
				LOAI_FILE = contentType,
				TEN_FILE = _fileProvider.GetFileNameWithoutExtension(fileName),
				DUOI_FILE = fileExtension,
				NGAY_TAO = DateTime.Now,
				NGUOI_TAO = _workContext.CurrentCustomer.ID
			};
			_fileCongViecModelFactory.SaveWorkFileOnDisk(fwork, dataByte);
			//Đọc file
			var DataImport = _fileProvider.GetWorkFileContentOnDisk(fwork);
			var path = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), fwork.NGAY_TAO.ToPathFolderStore(), fwork.GUID.ToString() + fileExtension);
			string dataString = _fileProvider.ReadAllText(path, Encoding.UTF8);
			if (fwork.DUOI_FILE == ".xls" || fwork.DUOI_FILE == ".xlsx")
			{
				Stream stream = new MemoryStream(DataImport);
				//var result = _dBTaiSanService.ImportExcel(stream);
			}
			else if (fwork.DUOI_FILE == ".json")
			{
				List<IMP_DoiTacModel> lstDoiTac = new List<IMP_DoiTacModel>();
				List<IMP_DoiTacModel> lstDoiTacerr = new List<IMP_DoiTacModel>();
				List<IMP_DoiTacModel> lstDoiTacsuccess = new List<IMP_DoiTacModel>();
				lstDoiTac = dataString.toEntities<IMP_DoiTacModel>();
				if (lstDoiTac != null && lstDoiTac.Count > 0)
				{
					foreach (IMP_DoiTacModel item in lstDoiTac)
					{
						MessageReturn mss = _itemModelFactory.ImportDoiTac(item);
						if (mss.Code != MessageReturn.SUCCESS_CODE)
						{
							item.MESSAGE = mss.Message;
							lstDoiTacerr.Add(item);
						}
						else
							lstDoiTacsuccess.Add(item);
					}
				}
				else
					return JsonErrorMessage("Không có dữ liệu cập nhập!");
				if (lstDoiTacerr.Count > 0)
				{
					//_fileCongViecService.DeleteFileCongViec(fileCongViec);
					string _pathStore = DateTime.Now.ToPathFolderStore();
					_pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
					_fileProvider.CreateDirectory(_pathStore);

					string fName = "";
					string fullpath = "";
					fName = string.Format("LOG_ERR_IMPORT_CCDC_{0}({1}).json", lstDoiTacerr.Count(), DateTime.Now.ToString("dd-MM-yyyy hh-mm"));
					fullpath = _fileProvider.Combine(_pathStore, fName);
					string json = "";
					var serializerSettings = new JsonSerializerSettings();
					serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
					serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
					serializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
					serializerSettings.NullValueHandling = NullValueHandling.Ignore;
					json = JsonConvert.SerializeObject(lstDoiTacerr, serializerSettings);

					_fileProvider.WriteAllText(fullpath, json, Encoding.UTF8);
					return Json(new
					{
						success = false,
						ListSuccess = lstDoiTacsuccess,
						ListError = lstDoiTacerr,
						filePath = fullpath,
						fileName = fName,
						fileType = MimeTypes.ApplicationJson
					});
				}
				else
				{
					//_fileCongViecService.DeleteFileCongViec(fileCongViec);
					//SuccessNotification("Import tài sản thành công");
					return Json(new
					{
						success = false,
						ListSuccess = lstDoiTacsuccess,
						//ListError = ListResultError,
					});
				}
			}
			return Json(dataString);
		}

		#endregion Methods
	}
}