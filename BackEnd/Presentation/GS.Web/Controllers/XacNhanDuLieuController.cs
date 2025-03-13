using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GS.Core;
using GS.Core.Domain.CauHinh;
using GS.Services.HeThong;
using GS.Services.KT;
using GS.Services.Security;
using GS.Web.Factories.HeThong;
using GS.Web.Models.XacNhanDuLieu;
using GS.Services.DanhMuc;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.HeThong;
using GS.Core.Domain.HeThong;
using System.Collections.Generic;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Core.Infrastructure;
using System;
using System.IO;
using GS.Core.Domain.DanhMuc;

namespace GS.Web.Controllers
{
    public class XacNhanDuLieuController : BaseWorksController
    {
        #region Fields

        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICauHinhModelFactory _cauHinhModelFactory;
        private readonly IDonViService _donViService;
        private readonly IFileCongViecService _fileCongViecService;
        private readonly IGSFileProvider _fileProvider;
        private readonly IFileCongViecModelFactory _fileCongViecModelFactory;
        #endregion Fields

        #region Ctor

        public XacNhanDuLieuController(IHoatDongService hoatdongService,
                                INhanHienThiService nhanHienThiService,
                                IQuyenService quyenService,
                                IWorkContext workContext,
                                CauHinhChung cauhinhChung,
                                IDonViService donViService,
                                IFileCongViecService fileCongViecService,
                                IGSFileProvider fileProvider,
                                IFileCongViecModelFactory fileCongViecModelFactory,
                                ICauHinhModelFactory cauHinhModelFactory)
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._donViService = donViService;
            this._fileCongViecService = fileCongViecService;
            this._fileProvider = fileProvider;
            this._fileCongViecModelFactory = fileCongViecModelFactory;
            this._cauHinhModelFactory = cauHinhModelFactory;
        }

        #endregion Ctor
        // GET: XacNhanDuLieu
        public ActionResult Index()
        {

            return View();
        }

        // GET: XacNhanDuLieu/Details/5
        public virtual IActionResult XacNhan(XacNhanDuLieuModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERXacNhanDuLieu))
                return AccessDeniedView();
            var donvi = _workContext.CurrentDonVi.ID;
            model.DonVi = _donViService.GetDonViById(donvi);
            model.IS_XAC_NHAN_DU_LIEU = model.DonVi.IS_XAC_NHAN_DU_LIEU??false;
            model.TREE_LEVEL = model.DonVi.TREE_LEVEL;
            return View(model);
        }

        // POST: XacNhanDuLieu/XacNhan
        [HttpPost]
        public virtual IActionResult XacNhan(XacNhanDuLieuModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERXacNhanDuLieu))
                return AccessDeniedView();
            //var file = _fileCongViecService.GetFileCongViecById(model.FILE_DOI_CHIEU_ID);
            if (model.FILE_DOI_CHIEU_1 != 0 || model.FILE_DOI_CHIEU_2 != 0 || model.FILE_DOI_CHIEU_3 != 0 || model.FILE_DOI_CHIEU_4 != 0
                || model.FILE_DOI_CHIEU_5 != 0 || model.FILE_DOI_CHIEU_6 != 0 || model.FILE_DOI_CHIEU_7 != 0 || model.FILE_DOI_CHIEU_8 != 0
                || model.FILE_DOI_CHIEU_9 != 0 || model.FILE_DOI_CHIEU_10 != 0 || model.FILE_DOI_CHIEU_11 != 0 || model.FILE_DOI_CHIEU_12 != 0)
            {
                DonVi donVi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
                var listFile = new List<decimal>() {
                model.FILE_DOI_CHIEU_1,
                model.FILE_DOI_CHIEU_2,
                model.FILE_DOI_CHIEU_3,
                model.FILE_DOI_CHIEU_4,
                model.FILE_DOI_CHIEU_5,
                model.FILE_DOI_CHIEU_6,
                model.FILE_DOI_CHIEU_7,
                model.FILE_DOI_CHIEU_8,
                model.FILE_DOI_CHIEU_9,
                model.FILE_DOI_CHIEU_10,
                model.FILE_DOI_CHIEU_11,
                model.FILE_DOI_CHIEU_12
                };
               
                 if (donVi != null)
                {
                    donVi.IS_XAC_NHAN_DU_LIEU = true;
                    donVi.LIST_FILE_XAC_NHAN = string.Join(",", listFile);
                    donVi.NGAY_XAC_NHAN = DateTime.Today;
                    _donViService.UpdateDonVi(donVi);
                }
                return JsonSuccessMessage("Xác nhận đối chiếu thành công!");
            }
            else
            {
                return JsonErrorMessage("Chưa nhập đủ file");
            }
        }
        // GET: XacNhanDuLieu/Edit/5
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


    }
}
