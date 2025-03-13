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
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.DongBo;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Controllers;
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
using System.Threading.Tasks;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class DonViBoPhanController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IDonViBoPhanModelFactory _itemModelFactory;
        private readonly IDonViBoPhanService _itemService;
        private readonly IDonViService _itemDonViService;
        private readonly IDongBoFactory _dongboFactory;
        private readonly IFileCongViecService _fileCongViecService;
        private readonly IGSFileProvider _fileProvider;
        private readonly IFileCongViecModelFactory _fileCongViecModelFactory;
        private readonly ITaiSanService _taiSanService;
        #endregion

        #region Ctor
        public DonViBoPhanController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IDonViBoPhanModelFactory itemModelFactory,
            IDonViBoPhanService itemService,
            IDonViService itemDonViService,
            IDongBoFactory dongBoFactory,
            IFileCongViecService fileCongViecService,
            IGSFileProvider fileProvider,
            IFileCongViecModelFactory fileCongViecModelFactory,
            ITaiSanService taiSanService
            )
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._itemDonViService = itemDonViService;
            this._dongboFactory = dongBoFactory;
            this._fileCongViecService = fileCongViecService;
            this._fileProvider = fileProvider;
            this._fileCongViecModelFactory = fileCongViecModelFactory;
            this._taiSanService = taiSanService;
        }
        #endregion

        #region Methods

        public virtual IActionResult List(int? pageIndex = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDonViBoPhan))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new DonViBoPhanSearchModel();
            var model = _itemModelFactory.PrepareDonViBoPhanSearchModel(searchmodel);
            if (pageIndex > 0)
            {
                searchmodel.Page = (int)pageIndex;
            }
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(DonViBoPhanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDonViBoPhan))
                return AccessDeniedKendoGridJson();
            //prepare model
            var donVi = _itemDonViService.GetProfileUser(_workContext.CurrentCustomer.GUID);

            //if (!_workContext.CurrentCustomer.IS_QUAN_TRI)
            searchModel.DonViId = donVi.ID;
            var model = _itemModelFactory.PrepareDonViBoPhanListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDonViBoPhan))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareDonViBoPhanModel(new DonViBoPhanModel(), null);

            //more
            var donVi = _itemDonViService.GetProfileUser(_workContext.CurrentCustomer.GUID);
            model.TEN_DON_VI = donVi.TEN_DON_VI;
            model.DON_VI_ID = donVi.ID;

            return View(model);
        }



        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(DonViBoPhanModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDonViBoPhan))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<DonViBoPhan>();
                _itemService.InsertDonViBoPhan(item);
                model.ID = item.ID;
                _dongboFactory.DongBoDanhMuc<DonViBoPhanModel>(new List<DonViBoPhanModel>() { model });
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<DonViBoPhanModel>(), "DonViBoPhan");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareDonViBoPhanModel(model, null);
            return View(model);
        }

        /// <summary>
        /// Description: popup them moi don vi bo phan
        /// </summary>
        /// <returns></returns>
        /// Create by BinhDC-20/02/2020
        public virtual IActionResult _Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDonViBoPhan) 
				&& !_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareDonViBoPhanModel(new DonViBoPhanModel(), null);

            //more
            var donVi = _itemDonViService.GetProfileUser(_workContext.CurrentCustomer.GUID);
            model.TEN_DON_VI = donVi.TEN_DON_VI;
            model.DON_VI_ID = donVi.ID;
            return PartialView(model);
        }

        /// <summary>
        /// Description: Xu ly du lieu tra ve tu popup them moi
        /// </summary>
        /// <param name="model"></param>
        /// <param name="continueEditing"></param>
        /// <returns></returns>
        /// Create by BinhDC-20/02/2020
        [HttpPost]
        public virtual IActionResult _Create(DonViBoPhanModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDonViBoPhan))
                return AccessDeniedView();
            if (ModelState.IsValid)
            {
                var item = model.ToEntity<DonViBoPhan>();
                _itemService.InsertDonViBoPhan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<DonViBoPhanModel>(), "DonViBoPhan");
                SuccessNotification("Tạo mới dữ liệu thành công!");
                model = _itemModelFactory.PrepareDonViBoPhanModel(model, item);
                model.dllDonViBoPhan = _itemModelFactory.PrepareSelectListDonViBoPhan(DonViId: model.DON_VI_ID, isAddFirst: true, valSelected: item.ID);
                // đồng bộ đơn vị bộ phận
              
                return JsonSuccessMessage("Tạo mới dữ liệu thành công!", model);

              
            }
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            //prepare model
            return JsonErrorMessage("Error", list);
        }

        public virtual IActionResult Edit(int id, int pageIndex)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDonViBoPhan))
                return AccessDeniedView();

            var item = _itemService.GetDonViBoPhanById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareDonViBoPhanModel(null, item);
            model.pageIndex = pageIndex;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(DonViBoPhanModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDonViBoPhan))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDonViBoPhanById(model.ID);
            if (item == null)
                return RedirectToAction("List", new { pageIndex = model.pageIndex });
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareDonViBoPhan(model, item);
                _itemService.UpdateDonViBoPhan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<DonViBoPhanModel>(), "DonViBoPhan");
                _dongboFactory.DongBoDanhMuc<DonViBoPhanModel>(new List<DonViBoPhanModel>() { model });
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID, pageIndex = model.pageIndex  }) : RedirectToAction("List",new { pageIndex = model.pageIndex });
            }
            //prepare model
            model = _itemModelFactory.PrepareDonViBoPhanModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDonViBoPhan))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDonViBoPhanById(id);
            if (item == null)
                return RedirectToAction("List");
            var tsdvbp = _taiSanService.GetTaiSanByDonViBoPhanId(item.ID);
            if (tsdvbp == false)
            {
                try
                {
                    DonViBoPhanModel model = item.ToModel<DonViBoPhanModel>();
                    _itemService.DeleteDonViBoPhan(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<DonViBoPhanModel>(), "DonViBoPhan");
                    //đồng bộ dữ liệu qua kho
                    _dongboFactory.XoaDanhMuc<DonViBoPhanModel>(model);
                    //activity log  
                    //SuccessNotification("Xoá dữ liệu thành công"); 
                    //  return RedirectToAction("List");
                    UpdateSessionSearchModel<DonViBoPhanSearchModel>(true);
                    return JsonSuccessMessage("Xoá dữ liệu thành công");
                }
                catch (Exception exc)
                {
                    ErrorNotification("Dữ liệu không được xóa. =>" + exc.Message);
                    return RedirectToAction("Edit", new { id = item.ID });
                }
            }
            else return JsonErrorMessage("Đơn vị bộ phận này không thể xóa!");
        }

        public virtual IActionResult ImportDonViBoPhan()
        {
            return View();
        }

        [HttpPost]
        //[RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        //[RequestSizeLimit(209715200)]
        public IActionResult ImportDonViBoPhanFromFile(IFormFile file)
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
                List<IMP_DonViBoPhanModel> lstDVBP = new List<IMP_DonViBoPhanModel>();
                List<IMP_DonViBoPhanModel> lstDVBPerr = new List<IMP_DonViBoPhanModel>();
                List<IMP_DonViBoPhanModel> lstDVBPsuccess = new List<IMP_DonViBoPhanModel>();
                lstDVBP = dataString.toEntities<IMP_DonViBoPhanModel>();
                if (lstDVBP != null && lstDVBP.Count > 0)
                {
                    foreach (IMP_DonViBoPhanModel item in lstDVBP)
                    {
                        MessageReturn mss = _itemModelFactory.ImportDonViBoPhan(item);
                        if (mss.Code != MessageReturn.SUCCESS_CODE)
                        {
                            item.MESSAGE = mss.Message;
							lstDVBPerr.Add(item);
                        }
                        else
                            lstDVBPsuccess.Add(item);
                    }
                }
                else
                    return JsonErrorMessage("Không có dữ liệu cập nhập!");
                if (lstDVBPerr.Count > 0)
                {
                    //_fileCongViecService.DeleteFileCongViec(fileCongViec);
                    string _pathStore = DateTime.Now.ToPathFolderStore();
                    _pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
                    _fileProvider.CreateDirectory(_pathStore);

                    string fName = "";
                    string fullpath = "";
                    fName = string.Format("LOG_ERR_IMPORT_DonViBoPhan_{0}({1}).json", lstDVBPerr.Count(), DateTime.Now.ToString("dd-MM-yyyy hh-mm"));
                    fullpath = _fileProvider.Combine(_pathStore, fName);
                    string json = "";
                    var serializerSettings = new JsonSerializerSettings();
                    serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                    serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    serializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    json = JsonConvert.SerializeObject(lstDVBPerr, serializerSettings);

                    _fileProvider.WriteAllText(fullpath, json, Encoding.UTF8);
                    return Json(new
                    {
                        success = false,
                        ListSuccess = lstDVBPsuccess,
                        ListError = lstDVBPerr,
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
                        ListSuccess = lstDVBPsuccess,
                        //ListError = ListResultError,
                    });
                }
            }
            return Json(dataString);

        }
        #endregion
    }
}

