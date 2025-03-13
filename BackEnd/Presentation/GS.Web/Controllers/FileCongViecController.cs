using GS.Core;
using GS.Core.Data;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Infrastructure;
using GS.Services.HeThong;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.HeThong;
using GS.Web.Models.DongBoTaiSan;
using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace GS.Web.Controllers
{
    public class FileCongViecController : BaseWorksController
    {
        #region Fields
        private readonly IFileCongViecService _itemService;
        private readonly IWorkContext _workContext;
        private readonly IGSFileProvider _fileProvider;
        private readonly IFileCongViecModelFactory _fileCongViecModelFactory;
        #endregion

        #region Ctor
        public FileCongViecController(IFileCongViecService itemService,
            IWorkContext workContext,
            IGSFileProvider fileProvider,
            IFileCongViecModelFactory fileCongViecModelFactory
            )
        {
            this._itemService = itemService;
            this._workContext = workContext;
            this._fileProvider = fileProvider;
            this._fileCongViecModelFactory = fileCongViecModelFactory;
        }
        #endregion

        #region Methods
        public virtual IActionResult Index()
        {
            var searchmodel = new FileCongViecSearchModel();
            var model = _fileCongViecModelFactory.PrepareFileCongViecSearchModel(searchmodel);
            return View(model);
        }
        [HttpPost]
        public virtual IActionResult Index(FileCongViecSearchModel searchModel)
		{
            var model = _fileCongViecModelFactory.PrepareFileCongViecListModel(searchModel);
            return Json(model);
        }
        byte[] GetWorkFileContentOnDisk(FileCongViec item)
        {
            var _fileStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), item.NGAY_TAO.ToPathFolderStore(), item.GUID.ToString() + item.DUOI_FILE);
            return _fileProvider.ReadAllBytes(_fileStore);
        }
        void DeleteWorkFileOnDisk(FileCongViec item)
        {
            var _fileStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), item.NGAY_TAO.ToPathFolderStore(), item.GUID.ToString() + item.DUOI_FILE);
            _fileProvider.DeleteFile(_fileStore, true);
        }

        public virtual IActionResult DownloadFile(Guid downloadGuid)
        {
            var download = _itemService.GetFileByGuid(downloadGuid);
            if (download == null)
                return Content("No download record found with the specified id");
			if (download.NOI_DUNG_FILE== null)
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
            var download = _itemService.GetFileByGuid(downloadGuid);
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
        [HttpPost]
        public ActionResult DeleteWorkFile(int Id)
        {
            
            // lưu ý: xóa các mapping nếu có trước khi thực hiện xóa file !!
            var item = _itemService.GetFileCongViecById(Id);
            _itemService.DeleteFileCongViec(item);
            DeleteWorkFileOnDisk(item);
            return JsonSuccessMessage();
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
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public FileCongViec UploadFile(IFormFile httpPostedFile)
        {
            if (httpPostedFile == null)
            {
                return null;
            }

            var fileBinary = _itemService.GetWorkFileBits(httpPostedFile);

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
            _itemService.InsertFileCongViec(fwork);
            //luu file content ra ngoai
            _fileCongViecModelFactory.SaveWorkFileOnDisk(fwork, fileBinary);
            //when returning JSON the mime-type must be set to text/plain
            //otherwise some browsers will pop-up a "Save As" dialog.
            return fwork;
        }
        [HttpPost]
        //do not validate request token (XSRF)
        public virtual IActionResult AsyncUpload()
        {
            var httpPostedFile = Request.Form.Files.FirstOrDefault();
            if (httpPostedFile == null)
            {
                return Json(new
                {
                    success = false,
                    message = "No file uploaded",
                    downloadGuid = Guid.Empty
                });
            }

            var fileBinary = _itemService.GetWorkFileBits(httpPostedFile);

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
                NGUOI_TAO = _workContext.CurrentCustomer.ID
            };
            _itemService.InsertFileCongViec(fwork);
            _fileCongViecModelFactory.SaveWorkFileOnDisk(fwork, fileBinary);
            //when returning JSON the mime-type must be set to text/plain
            //otherwise some browsers will pop-up a "Save As" dialog.
            return Json(fwork.ToModel<FileCongViecModel>());
        }

        [HttpPost]
        //[RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        // [RequestSizeLimit(209715200)]
        public IActionResult UpFileToDataBase(IFormFile file)
        {

            if (file == null)
            {
                ErrorNotification("Bạn chưa Nhập file");
                return JsonErrorMessage("Bạn chưa Nhập file");
            }
           
            var dataByte = _itemService.GetWorkFileBits(file);
            var fileName = file.FileName;
            var fileExtension = _fileProvider.GetFileExtension(fileName);
            if (!string.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();
            //  lưu file 
            var contentType = file.ContentType;
            var fwork = new FileCongViec
            {
                GUID = Guid.NewGuid(),
                NOI_DUNG_FILE = dataByte,
                LOAI_FILE = contentType,
                //we store filename without extension for downloads
                TEN_FILE = _fileProvider.GetFileNameWithoutExtension(fileName),
                DUOI_FILE = fileExtension,
                NGAY_TAO = DateTime.Now,
                NGUOI_TAO = _workContext.CurrentCustomer.ID
            };
            _itemService.InsertFileCongViec(fwork);
            return JsonSuccessMessage("Done",fwork);
        }
            #endregion
        }
}
