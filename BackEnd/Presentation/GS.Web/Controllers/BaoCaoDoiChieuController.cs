//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/10/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Mvc;
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.BaoCaoDoiChieus;

using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.BaoCao;
using GS.Web.Controllers;
using GS.Web.Factories.BaoCao;
using GS.Services.BaoCaoDoiChieus;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Microsoft.AspNetCore.Http;
using GS.Web.Models.HeThong;
using System.Collections.Generic;
using GS.Core.Infrastructure;
using System.IO;
using GS.Web.Factories.HeThong;
using GS.Core.Data;

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class BaoCaoDoiChieuController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly IBaoCaoDoiChieuModelFactory _itemModelFactory;
            private readonly IBaoCaoDoiChieuService _itemService;
            private readonly IFileCongViecService _fileCongViecService;
            private readonly IGSFileProvider _fileProvider;
            private readonly IFileCongViecModelFactory _fileCongViecModelFactory;
        #endregion

        #region Ctor
        public BaoCaoDoiChieuController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IBaoCaoDoiChieuModelFactory itemModelFactory,
            IBaoCaoDoiChieuService itemService,
            IFileCongViecService fileCongViecService,
            IGSFileProvider fileProvider,
            IFileCongViecModelFactory fileCongViecModelFactory
            )
        {            
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;            
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._fileCongViecService = fileCongViecService;
            this._fileProvider = fileProvider;
            this._fileCongViecModelFactory = fileCongViecModelFactory;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new BaoCaoDoiChieuSearchModel ();
            var model = _itemModelFactory.PrepareBaoCaoDoiChieuSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(BaoCaoDoiChieuSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareBaoCaoDoiChieuListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareBaoCaoDoiChieuModel(new BaoCaoDoiChieuModel(), null);
            model.NAM_BAO_CAO = 2020;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(BaoCaoDoiChieuModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<BaoCaoDoiChieu>();                
                _itemService.InsertBaoCaoDoiChieu(item);
                _hoatdongService.InsertHoatDong("TaoMoi", "Tạo mới thông tin ", item.ToModel<BaoCaoDoiChieuModel>(), "BaoCaoDoiChieu");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareBaoCaoDoiChieuModel(model, null);            
            return View(model);
        } 
        
        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
                
            var item = _itemService.GetBaoCaoDoiChieuById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareBaoCaoDoiChieuModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(BaoCaoDoiChieuModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetBaoCaoDoiChieuById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareBaoCaoDoiChieu(model,item);
                _itemService.UpdateBaoCaoDoiChieu(item); 
                _hoatdongService.InsertHoatDong("CapNhat", "Cập nhật thông tin ", item.ToModel<BaoCaoDoiChieuModel>(), "BaoCaoDoiChieu");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareBaoCaoDoiChieuModel(model, item, true);
            return View(model);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetBaoCaoDoiChieuById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteBaoCaoDoiChieu(item);
                _hoatdongService.InsertHoatDong("Xoa", "Xóa dữ liệu ", item.ToModel<BaoCaoDoiChieuModel>(), "BaoCaoDoiChieu");
                //activity log  
                //SuccessNotification("Xoá dữ liệu thành công");
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = item.ID });
            }
        }
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public ActionResult SaveDropzoneJsUploadedFiles()
        {
            List<FileCongViecModel> ls = new List<FileCongViecModel>();
            foreach (var file in Request.Form.Files)
            {
                string fname = file.FileName;

                //You can Save the file content here
                var item = UploadFile(file);
                if (item != null)
                {
                    var model = item.ToModel<FileCongViecModel>();
                    ls.Add(model);
                }

            }

            return Json(ls);

        }

        public ActionResult ImportBaoCaoFromFolder(string folderName, int loai)
        {
            var listError = _itemModelFactory.InsertBaoCaoDoiChieuFromFolder(folderName,loai);
            return Json(listError);

        }
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public FileCongViec UploadFile(IFormFile httpPostedFile)
        {
            if (httpPostedFile == null)
            {
                return null;
            }

            var fileBinary = _fileCongViecService.GetWorkFileBits(httpPostedFile);

            var qqFileNameParameter = "qqfilename";
            var fileName = httpPostedFile.FileName;
            if (string.IsNullOrEmpty(fileName) && Request.Form.ContainsKey(qqFileNameParameter))
                fileName = Request.Form[qqFileNameParameter].ToString();
            //remove path (passed in IE)
            fileName = _fileProvider.GetFileName(fileName);

            var contentType = httpPostedFile.ContentType;

            var fileExtension = _fileProvider.GetFileExtension(fileName);
            if (!string.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();

            var fwork = new FileCongViec
            {
                GUID = Guid.NewGuid(),
                NOI_DUNG_FILE = null,
                LOAI_FILE = contentType,
                //we store filename without extension for downloads
                TEN_FILE = _fileProvider.GetFileNameWithoutExtension(fileName),
                DUOI_FILE = fileExtension,
                NGAY_TAO = DateTime.Now,
                NGUOI_TAO = _workContext.CurrentCustomer.ID,
                KICH_THUOC = Convert.ToInt32(fileBinary.LongLength / 1024) //luu thanh kb
            };
            using (var fileStream = httpPostedFile.OpenReadStream())
            {
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms)
;
                    fwork.NOI_DUNG_FILE = ms.ToArray();

                }
            }
            _fileCongViecService.InsertFileCongViec(fwork);
            //luu file content ra ngoai
            _fileCongViecModelFactory.SaveWorkFileOnDisk(fwork, fileBinary);
            //when returning JSON the mime-type must be set to text/plain
            //otherwise some browsers will pop-up a "Save As" dialog.
            return fwork;
        }
        byte[] GetWorkFileContentOnDisk(FileCongViec item)
        {
            var _fileStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), item.NGAY_TAO.ToPathFolderStore(), item.GUID.ToString() + item.DUOI_FILE);
            return _fileProvider.ReadAllBytes(_fileStore);
        }
        public virtual IActionResult DownloadFile(Guid downloadGuid)
        {
            var download = _fileCongViecService.GetFileByGuid(downloadGuid);
            if (download == null)
                return Content("No download record found with the specified id");
            if (download.NOI_DUNG_FILE == null)
                download.NOI_DUNG_FILE = GetWorkFileContentOnDisk(download);
            //use stored data
            if (download.NOI_DUNG_FILE == null || download.NOI_DUNG_FILE.Length < 2)
                return Content($"Download data is not available any more. Download GD={download.ID}");

            var fileName = !string.IsNullOrWhiteSpace(download.TEN_FILE) ? download.TEN_FILE : download.ID.ToString();
            var contentType = !string.IsNullOrWhiteSpace(download.LOAI_FILE)
                ? download.LOAI_FILE
                : MimeTypes.ApplicationOctetStream;
            return new FileContentResult(download.NOI_DUNG_FILE, contentType)
            {
                FileDownloadName = fileName + download.DUOI_FILE
            };
        }
        public virtual IActionResult DownloadFileInDb(Guid downloadGuid)
        {
            var download = _fileCongViecService.GetFileByGuid(downloadGuid);
            if (download == null)
                return Content("No download record found with the specified id");
            if (download.NOI_DUNG_FILE == null || download.NOI_DUNG_FILE.Length < 2)
                return Content($"Download data is not available any more. Download GD={download.ID}");

            var fileName = !string.IsNullOrWhiteSpace(download.TEN_FILE) ? download.TEN_FILE : download.ID.ToString();
            var contentType = !string.IsNullOrWhiteSpace(download.LOAI_FILE)
                ? download.LOAI_FILE
                : MimeTypes.ApplicationOctetStream;
            return new FileContentResult(download.NOI_DUNG_FILE, contentType)
            {
                FileDownloadName = fileName + download.DUOI_FILE
            };
        }
        #endregion
    }
}

