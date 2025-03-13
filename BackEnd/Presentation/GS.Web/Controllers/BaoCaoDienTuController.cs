using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Data;
using GS.Core.Domain.BaoCaoDienTu;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.HeThong;
using GS.Core.Infrastructure;
using GS.Services;
using GS.Services.BaoCaoDienTus;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.BaoCao;
using GS.Web.Factories.BaoCaoDienTus;
using GS.Web.Factories.BaoCaos;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Models.BaoCaoDienTu;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.HeThong;
using GS.Web.Reports.Online;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Controllers
{
    public class BaoCaoDienTuController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IBaoCaoDienTuModelFactory _itemModelFactory;
        private readonly IBaoCaoDienTuService _itemService;
        private readonly IFileCongViecService _fileCongViecService;
        private readonly IGSFileProvider _fileProvider;
        private readonly IFileCongViecModelFactory _fileCongViecModelFactory;
        private readonly IReportModelFactory _reportModelFactory;
        #endregion

        #region Ctor
        public BaoCaoDienTuController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IBaoCaoDienTuModelFactory itemModelFactory,
            IBaoCaoDienTuService itemService,
            IFileCongViecService fileCongViecService,
            IGSFileProvider fileProvider,
            IFileCongViecModelFactory fileCongViecModelFactory,
            IReportModelFactory reportModelFactory
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
            this._reportModelFactory = reportModelFactory;
        }
        #endregion
        #region Methods
        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERBaoCaoDienTu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new BaoCaoDienTuSearchModel();
            var model = _itemModelFactory.PrepareBaoCaoDienTuSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(BaoCaoDienTuSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERBaoCaoDienTu))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareBaoCaoDienTuListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERBaoCaoDienTu))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareBaoCaoDienTuModel(new BaoCaoDienTuModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(BaoCaoDienTuModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERBaoCaoDienTu))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<BaoCaoDienTu>();
                item.TRANG_THAI_ID = 0;
                item.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                item.DON_VI_ID = _workContext.CurrentDonVi.ID;
                _itemService.InsertBaoCaoDienTu(item);
                _hoatdongService.InsertHoatDong("TaoMoi", "Tạo mới thông tin ", item.ToModel<BaoCaoDienTuModel>(), "BaoCaoDienTu");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareBaoCaoDienTuModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERBaoCaoDienTu))
                return AccessDeniedView();

            var item = _itemService.GetBaoCaoDienTuById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareBaoCaoDienTuModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(BaoCaoDienTuModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERBaoCaoDienTu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetBaoCaoDienTuById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                if(item.TRANG_THAI_ID == (int)enumTrangThaiBaoCao.TU_CHOI)
                {
                    item.TRANG_THAI_ID = (int)enumTrangThaiBaoCao.CHO_DUYET;
                }
                item.NGAY_BAO_CAO = DateTime.Now;
                _itemModelFactory.PrepareBaoCaoDienTu(model, item);
                _itemService.UpdateBaoCaoDienTu(item);
                _hoatdongService.InsertHoatDong("CapNhat", "Cập nhật thông tin ", item.ToModel<BaoCaoDienTuModel>(), "BaoCaoDoiChieu");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareBaoCaoDienTuModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERBaoCaoDienTu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetBaoCaoDienTuById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteBaoCaoDienTu(item);
                _hoatdongService.InsertHoatDong("Xoa", "Xóa dữ liệu ", item.ToModel<BaoCaoDienTuModel>(), "BaoCaoDienTu");
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
        
        // GET: ReportOnline
        public virtual IActionResult ViewReport(decimal? id = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERBaoCaoDienTu))
                return AccessDeniedView();
            BaoCaoDienTu item = new BaoCaoDienTu();
            if (id > 0)
                item = _itemService.GetBaoCaoDienTuById(id.Value);
            var model = _itemModelFactory.PrepareBaoCaoDienTuModelView(new BaoCaoDienTuModel(), item);
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.CDKT_KiemKeTaiSan);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            var searchModel = new BaoCaoDienTuSearchModel();
            searchModel.TEN_DON_VI = _workContext.CurrentDonVi.TEN_DON_VI;
            searchModel.MA_DON_VI = _workContext.CurrentDonVi.MA_DON_VI;
            searchModel.NGAY_BAO_CAO = model.NGAY_BAO_CAO;
            searchModel.SO_VAN_BAN = model.SO_VAN_BAN;
            searchModel.TINH_HINH_BAN_HANH_VAN_BAN = model.TINH_HINH_BAN_HANH_VAN_BAN;
            searchModel.TINH_HINH_TANG_GIAM = model.TINH_HINH_TANG_GIAM;
            searchModel.THUC_TRANG = model.THUC_TRANG;
            searchModel.TINH_HINH_THUC_HIEN = model.TINH_HINH_THUC_HIEN;
            searchModel.DANH_GIA_TICH_CUC = model.DANH_GIA_TICH_CUC;
            searchModel.DANH_GIA_TINH_HINH = model.DANH_GIA_TINH_HINH;
            searchModel.CONG_TAC_CHI_DAO = model.CONG_TAC_CHI_DAO;
            searchModel.KET_QUA_KHAC = model.KET_QUA_KHAC;
            searchModel.KIEN_NGHI = model.KIEN_NGHI;
            searchModel.NOI_NHAN = model.NOI_NHAN;
            searchModel.LY_DO_TU_CHOI = model.LY_DO_TU_CHOI;
            searchModel.FILE_ID = model.FILE_ID;
            model.ReportOnline = new Report_Online(searchModel, cauHinhModel, cauHinhChunghModel);
            return View(model);
        }
        public virtual IActionResult ListDuyetBaoCaoDienTu()
        {
            //prepare model
            var searchModel = new BaoCaoDienTuSearchModel();
            var model = _itemModelFactory.PrepareBaoCaoDienTuSearchModel(searchModel);
            var listExuclue = (new enumTrangThaiBaoCao()).ToSelectList(valuesToExclude: new int[] { (int)enumTrangThaiBaoCao.CHO_DUYET, (int)enumTrangThaiBaoCao.DA_GUI_CAP_TREN}).Select(c => c.Value.ToNumberInt32()).ToArray();
            searchModel.ddlTrangThai = (new enumTrangThaiBaoCao()).ToSelectList(valuesToExclude: listExuclue).ToList();
            //model.enumHanhDongListDuyetBaoCao = (enumHanhDongListDuyetBaoCao)HanhDong;
            //bỏ duyệt: danh sách chỉ có đã duyệt
            //if (model.enumHanhDongListDuyetBaoCao == enumHanhDongListDuyetBaoCao.BO_DUYET)
            //    model.TRANG_THAI_ID = (int)enumTrangThaiBaoCao.DA_DUYET;
            //còn lại mặc định danh sách là chờ duyệt
            //else
             //   model.TRANG_THAI_ID = (int)enumTrangThaiBaoCao.CHO_DUYET;
            return View(model);
        }
        [HttpPost]
        public virtual IActionResult ListDuyetBaoCaoDienTu(BaoCaoDienTuSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDuyetDangKy))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareBaoCaoChoDuyetListModel(searchModel);

            return Json(model);
        }
        #endregion
        [HttpPost]
        public virtual IActionResult GuiYeuCauDuyetBaoCao(decimal id)
        {
            var item = _itemService.GetBaoCaoDienTuById(id);
            if(item == null)
            {
                return JsonErrorMessage("Gửi báo cáo không thành công !", id);
            }
            if(item.TRANG_THAI_ID == (int)enumTrangThaiBaoCao.MOI_TAO)
            {
                item.TRANG_THAI_ID = (int)enumTrangThaiBaoCao.CHO_DUYET;
                _itemService.UpdateBaoCaoDienTu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<BaoCaoDienTuModel>(), "BaoCaoDienTu");
                return JsonSuccessMessage("Gửi báo cáo thành công !");
            }
            else
            {
                return JsonErrorMessage("Gửi báo cáo không thành công !", id);
            }
            
        }
        [HttpPost]
        public virtual IActionResult DuyetBaoCao(decimal id)
        {
            var item = _itemService.GetBaoCaoDienTuById(id);
            if (item == null)
            {
                return JsonErrorMessage("Duyệt báo cáo không thành công !", id);
            }
            if (item.TRANG_THAI_ID == (int)enumTrangThaiBaoCao.CHO_DUYET)
            {
                item.TRANG_THAI_ID = (int)enumTrangThaiBaoCao.DA_DUYET;
                item.NGUOI_DUYET_ID = _workContext.CurrentDonVi.ID;
                _itemService.UpdateBaoCaoDienTu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<BaoCaoDienTuModel>(), "BaoCaoDienTu");
                return JsonSuccessMessage("Duyệt báo cáo thành công !");
            }
            else
            {
                return JsonErrorMessage("Duyệt báo cáo không thành công !", id);
            }

        }
        [HttpPost]
        public virtual IActionResult TuChoiBaoCao(decimal id)
        {
            var item = _itemService.GetBaoCaoDienTuById(id);
            if (item == null)
            {
                return JsonErrorMessage("Cập nhật dữ liệu không thành công !", id);
            }
            if (item.TRANG_THAI_ID == (int)enumTrangThaiBaoCao.CHO_DUYET || item.TRANG_THAI_ID == (int)enumTrangThaiBaoCao.DA_GUI_CAP_TREN)
            {
                item.TRANG_THAI_ID = (int)enumTrangThaiBaoCao.TU_CHOI;
                _itemService.UpdateBaoCaoDienTu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<BaoCaoDienTuModel>(), "BaoCaoDienTu");
                return JsonSuccessMessage("Cập nhật dữ liệu thành công !");
            }
            else
            {
                return JsonErrorMessage("Cập nhật dữ liệu không thành công !", id);
            }

        }
        [HttpPost]
        public virtual IActionResult GuiBaoCaoCapTren(decimal id)
        {
            var item = _itemService.GetBaoCaoDienTuById(id);
            if (item == null)
            {
                return JsonErrorMessage("Gửi báo cáo không thành công !", id);
            }
            if (item.TRANG_THAI_ID == (int)enumTrangThaiBaoCao.DA_DUYET)
            {
                item.TRANG_THAI_ID = (int)enumTrangThaiBaoCao.DA_GUI_CAP_TREN;
                _itemService.UpdateBaoCaoDienTu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<BaoCaoDienTuModel>(), "BaoCaoDienTu");
                return JsonSuccessMessage("Gửi báo cáo thành công !");
            }
            else
            {
                return JsonErrorMessage("Gửi báo cáo không thành công !", id);
            }

        }
        [HttpPost]
        public virtual IActionResult CapTrenDuyet(decimal id)
        {
            var item = _itemService.GetBaoCaoDienTuById(id);
            if (item == null)
            {
                return JsonErrorMessage("Duyệt báo cáo không thành công !", id);
            }
            if (item.TRANG_THAI_ID == (int)enumTrangThaiBaoCao.DA_GUI_CAP_TREN)
            {
                item.TRANG_THAI_ID = (int)enumTrangThaiBaoCao.CAP_TREN_DA_DUYET;
                item.NGUOI_DUYET_ID = _workContext.CurrentDonVi.ID;
                _itemService.UpdateBaoCaoDienTu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<BaoCaoDienTuModel>(), "BaoCaoDienTu");
                return JsonSuccessMessage("Duyệt báo cáo thành công !");
            }
            else
            {
                return JsonErrorMessage("Duyệt báo cáo không thành công !", id);
            }

        }
        public virtual IActionResult CapTrenTuChoi(decimal id)
        {
            var item = _itemService.GetBaoCaoDienTuById(id);
            if (item == null)
            {
                return JsonErrorMessage("Cập nhật dữ liệu không thành công !", id);
            }
            if (item.TRANG_THAI_ID == (int)enumTrangThaiBaoCao.DA_GUI_CAP_TREN)
            {
                item.TRANG_THAI_ID = (int)enumTrangThaiBaoCao.CAP_TREN_DA_DUYET;
                _itemService.UpdateBaoCaoDienTu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<BaoCaoDienTuModel>(), "BaoCaoDienTu");
                return JsonSuccessMessage("Cập nhật dữ liệu thành công !");
            }
            else
            {
                return JsonErrorMessage("Cập nhật dữ liệu không thành công !", id);
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
                return Content("Chưa có file");
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
    }
}
