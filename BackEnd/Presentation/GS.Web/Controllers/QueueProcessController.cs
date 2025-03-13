using GS.Core;
using GS.Core.Domain.BaoCaos;
using GS.Core.Domain.BaoCaos.TS_BCCT;
using GS.Core.Domain.CauHinh;
using GS.Services.BaoCaos;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Factories.BaoCaos;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace GS.Web.Controllers
{
    public class QueueProcessController : BaseWorksController
    {
        #region Fields

        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IQueueProcessModelFactory _itemModelFactory;
        private readonly IQueueProcessService _itemService;
        private readonly ICauHinhService _cauHinhService;
        private readonly IFileCongViecService _fileCongViecService;

        #endregion Fields

        #region Ctor

        public QueueProcessController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IQueueProcessModelFactory itemModelFactory,
            IQueueProcessService itemService,
            ICauHinhService cauHinhService,
            IFileCongViecService fileCongViecService
            )
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._cauHinhService = cauHinhService;
            this._fileCongViecService = fileCongViecService;
        }

        #endregion Ctor

        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new QueueProcessSearchModel();
            searchmodel.isDongBo = false;
            var model = _itemModelFactory.PrepareQueueProcessSearchModel(searchmodel);
            return View(model);
        }
        public virtual IActionResult ListDongBo()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new QueueProcessSearchModel();           
            var model = _itemModelFactory.PrepareQueueProcessSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(QueueProcessSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
                return AccessDeniedView();
            //prepare model
            searchModel.isDongBo = false;
            var model = _itemModelFactory.PrepareQueueProcessListModel(searchModel);
            return Json(model);
        }

        [HttpPost]
        public virtual IActionResult ListDongBo(QueueProcessSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
                return AccessDeniedView();
            //prepare model
            searchModel.isDongBo = true;
            var model = _itemModelFactory.PrepareQueueProcessListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Download(Guid guid)
        {
            var queue = _itemService.GetQueueProcessByGuid(guid);
            if (queue.TRANG_THAI_ID >= (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_TRANG_THAI_CHO)
                queue.TRANG_THAI_ID = (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DA_LUU_FILE;
            else
                queue.TRANG_THAI_ID = (int)enumTRANG_THAI_QUEUE_BAO_CAO.DA_LUU_FILE;
            _itemService.UpdateQueueProcessTrangThaiAndGUIDFILE(queue);
            return RedirectToAction("DownloadFileInDb", "FileCongViec", new { downloadGuid = queue.GUID_FILE });
        }

        public virtual IActionResult Detail(string MaBaoCao)
        {
            var queue = _itemService.GetQueueProcessGanNhat(_workContext.CurrentCustomer.ID, _workContext.CurrentDonVi.ID, MaBaoCao);
            if (queue == null)
                return PartialView(null);
            var model = _itemModelFactory.PrepareQueueProcessModel(new QueueProcessModel(), queue);
            return PartialView(model);
        }


        public virtual IActionResult Delete(Guid guid)
        {
            var queueProcess = _itemService.GetQueueProcessByGuid(guid);
            if (queueProcess != null)
            {
                if (queueProcess.GUID_FILE != null && queueProcess.GUID_FILE != Guid.Empty)
                {
                    var file = _fileCongViecService.GetFileByGuid(queueProcess.GUID_FILE);
                    if (file != null)
                        _fileCongViecService.DeleteFileCongViec(file);
                }
                _itemService.DeleteQueueProcess(queueProcess);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa file báo cáo", queueProcess, "QueueProcess");
                return JsonSuccessMessage("Xóa file thành công");
            }
            return JsonErrorMessage("Có lỗi xảy ra");
        }

        public virtual IActionResult AddToQueueDongbo(decimal ID)
        {


            return JsonSuccessMessage("Insert Queue đồng bộ");
        }
        #endregion Methods
    }
}