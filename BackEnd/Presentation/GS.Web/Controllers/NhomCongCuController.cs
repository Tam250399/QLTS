//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Mvc;
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;

using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.DanhMuc;
using GS.Web.Controllers;
using GS.Web.Factories.DanhMuc;
using GS.Services.DanhMuc;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using GS.Web.Factories.HeThong;
using GS.Core.Infrastructure;
using System.IO;
using GS.Core.Data;
using System.Text;
using Newtonsoft.Json;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class NhomCongCuController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly INhomCongCuModelFactory _itemModelFactory;
        private readonly IDonViModelFactory _donViModelFactory;
        private readonly INhomCongCuService _itemService;
        private readonly IFileCongViecService _fileCongViecService;
        private readonly IGSFileProvider _fileProvider;
        private readonly IFileCongViecModelFactory _fileCongViecModelFactory;
        #endregion

        #region Ctor
        public NhomCongCuController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            INhomCongCuModelFactory itemModelFactory,
            INhomCongCuService itemService,
            IDonViModelFactory donViModelFactory,
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
            this._donViModelFactory = donViModelFactory;
            this._fileCongViecService = fileCongViecService;
            this._fileProvider = fileProvider;
            this._fileCongViecModelFactory = fileCongViecModelFactory;
        }
        #endregion

        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLNhomCongCu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new NhomCongCuSearchModel();
            var model = _itemModelFactory.PrepareNhomCongCuSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(NhomCongCuSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLNhomCongCu))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareNhomCongCuListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create(int ParentId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLNhomCongCu))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareNhomCongCuModel(new NhomCongCuModel(), null);
            model.PARENT_ID = ParentId;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(NhomCongCuModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLNhomCongCu))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<NhomCongCu>();

                _itemService.InsertNhomCongCu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<NhomCongCuModel>(), "NhomCongCu");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareNhomCongCuModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLNhomCongCu))
                return AccessDeniedView();

            var item = _itemService.GetNhomCongCuById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareNhomCongCuModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(NhomCongCuModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLNhomCongCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetNhomCongCuById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareNhomCongCu(model, item);
                _itemService.UpdateNhomCongCu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<NhomCongCuModel>(), "NhomCongCu");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareNhomCongCuModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLNhomCongCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetNhomCongCuById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteNhomCongCu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<NhomCongCuModel>(), "NhomCongCu");
                //activity log  
                //SuccessNotification("Xoá dữ liệu thành công");
                //return RedirectToAction("List");
                UpdateSessionSearchModel<NhomCongCuSearchModel>(true);
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                ErrorNotification("Dữ liệu không được xóa.");
                //ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = item.ID });
            }
        }

        public virtual IActionResult _ChonDonVi(decimal NguoiDungId)
        {
            var model = new DonViSearchModel();
            model.nguoiDungId = NguoiDungId;
            model = _donViModelFactory.PrepareDonViSearchModel(model);
            return PartialView(model);
        }
        public virtual IActionResult _GetJsonNhomCongCu(int DonViId, int? exceptID = null)
        {
            var objReturn = _itemModelFactory.PrepareDDLNhomCongCu(DonViId, new List<decimal> { exceptID ?? 0 });
            return Json(objReturn);
        }
        public virtual IActionResult Xoa(int ID)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLNhomCongCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetNhomCongCuById(ID);
            if (item == null)
                return JsonErrorMessage("Dữ liệu không tồn tại");
            try
            {
                var listcongcuChild = _itemService.GetNhomCongCus(ParentId: item.ID);
                if (listcongcuChild.Count() > 0)
                {
                    return JsonErrorMessage("Xoá dữ liệu không thành công");
                }
                _itemService.DeleteNhomCongCu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<NhomCongCuModel>(), "NhomCongCu");
                //activity log  
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {

                return JsonErrorMessage("Xoá dữ liệu không thành công");
            }
        }

        public virtual IActionResult ImportNhomCongCu()
        {
            return View();
        }

        [HttpPost]
        //[RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        //[RequestSizeLimit(209715200)]
        public IActionResult ImportNhomCongCuFromFile(IFormFile file)
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
                List<IMP_NhomCongCuModel> lstNhomCongCu = new List<IMP_NhomCongCuModel>();
                List<IMP_NhomCongCuModel> lstNhomCongCuerr = new List<IMP_NhomCongCuModel>();
                List<IMP_NhomCongCuModel> lstNhomCongCusuccess = new List<IMP_NhomCongCuModel>();
                lstNhomCongCu = dataString.toEntities<IMP_NhomCongCuModel>();
                if (lstNhomCongCu != null && lstNhomCongCu.Count > 0)
                {
                    List<string> maChas = lstNhomCongCu.Where(x => !string.IsNullOrEmpty(x.MA_CHA)).Select(x => x.MA_CHA).Distinct().ToList();
                    List<IMP_NhomCongCuModel> lstNhomCongCuCha = lstNhomCongCu.Where(c => maChas.Contains(c.MA)).ToList();
                    List<IMP_NhomCongCuModel> lstNhomCongCuCon = lstNhomCongCu.Where(c => !maChas.Contains(c.MA)).ToList();

                    foreach (IMP_NhomCongCuModel item in lstNhomCongCuCha)
                    {
                        MessageReturn mss = _itemModelFactory.ImportNhomCongCu(item);
                        if (mss.Code != MessageReturn.SUCCESS_CODE)
                        {
                            item.MESSAGE = mss.Message;
                            lstNhomCongCuerr.Add(item);
                        }
                        else
                            lstNhomCongCusuccess.Add(item);
                    }

                    foreach (IMP_NhomCongCuModel item in lstNhomCongCuCon)
                    {
                        MessageReturn mss = _itemModelFactory.ImportNhomCongCu(item);
                        if (mss.Code != MessageReturn.SUCCESS_CODE)
                        {
                            item.MESSAGE = mss.Message;
                            lstNhomCongCuerr.Add(item);
                        }
                        else
                            lstNhomCongCusuccess.Add(item);
                    }
                }
                else
                    return JsonErrorMessage("Không có dữ liệu cập nhập!");
                if (lstNhomCongCuerr.Count > 0)
                {
                    //_fileCongViecService.DeleteFileCongViec(fileCongViec);
                    string _pathStore = DateTime.Now.ToPathFolderStore();
                    _pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
                    _fileProvider.CreateDirectory(_pathStore);

                    string fName = "";
                    string fullpath = "";
                    fName = string.Format("LOG_ERR_IMPORT_NhomCongCu_{0}({1}).json", lstNhomCongCuerr.Count(), DateTime.Now.ToString("dd-MM-yyyy hh-mm"));
                    fullpath = _fileProvider.Combine(_pathStore, fName);
                    string json = "";
                    var serializerSettings = new JsonSerializerSettings();
                    serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                    serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    serializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    json = JsonConvert.SerializeObject(lstNhomCongCuerr, serializerSettings);

                    _fileProvider.WriteAllText(fullpath, json, Encoding.UTF8);
                    return Json(new
                    {
                        success = false,
                        ListSuccess = lstNhomCongCusuccess,
                        ListError = lstNhomCongCuerr,
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
                        ListSuccess = lstNhomCongCusuccess,
                        //ListError = ListResultError,
                    });
                }
            }
            return Json(dataString);

        }
        #endregion
    }
}

