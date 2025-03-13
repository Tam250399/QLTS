//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Configuration;
using GS.Core.Data;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.HeThong;
using GS.Core.Infrastructure;
using GS.Services;
using GS.Services.Authentication;
using GS.Services.DanhMuc;
//using GS.Services.Events;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Controllers;
using GS.Web.Factories.BaoCaos;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.DongBo;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using OfficeOpenXml;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Web.HeThong.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class NguoiDungController : BaseWorksController
    {
        #region Fields
        private readonly INhanHienThiService _NhanHienThiService;
        private readonly IWorkContext _workContext;
        private readonly INguoiDungModelFactory _itemModelFactory;
        private readonly INguoiDungService _itemService;
        private readonly IVaiTroNguoiDungService _vaiTroNguoiDungService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IVaiTroService _vaiTroService;
        private readonly IEncryptionService _encryptionService;
        private readonly IQuyenService _quyenService;
        private readonly IHoatDongService _hoatdongService;
        private readonly IHoatDongModelFactory _hoatDongModelFactory;
        private readonly IDonViService _DonViService;
        private readonly IDonViModelFactory _DonViModelFactory;
        private readonly INguoiDungDonViService _nguoiDungDonViService;
        private readonly IDongBoFactory _dongBoFactory;
        private readonly GSConfig _gSConfig;
        private readonly IDonViService _donViService;
        private readonly IGSFileProvider _fileProvider;
        private readonly IReportModelFactory _reportModelFactory;
        private readonly IFileCongViecService _fileCongViecService;
        private readonly ICauHinhService _cauHinhService;
        #endregion

        #region Ctor
        public NguoiDungController(
            IQuyenService quyenService,
            IEncryptionService encryptionService,
            INhanHienThiService NhanHienThiService,
            IWorkContext workContext,
            INguoiDungModelFactory itemModelFactory,
            INguoiDungService itemService,
            IVaiTroNguoiDungService vaiTroNguoiDungService,
            IAuthenticationService authenticationService,
            IVaiTroService vaiTroService,
            IHoatDongService hoatDongService,
            IHoatDongModelFactory hoatDongModelFactory,
            IDonViService DonViService,
            IDonViModelFactory DonViModelFactory,
            INguoiDungDonViService nguoiDungDonViService,
            IDongBoFactory dongBoFactory,
            GSConfig gSConfig,
            IDonViService donViService,
            IGSFileProvider fileProvider,
            IReportModelFactory reportModelFactory,
            IFileCongViecService fileCongViecService,
            ICauHinhService cauHinhService
            )
        {
            this._hoatdongService = hoatDongService;
            this._hoatDongModelFactory = hoatDongModelFactory;
            this._quyenService = quyenService;
            this._encryptionService = encryptionService;
            this._vaiTroService = vaiTroService;
            this._authenticationService = authenticationService;
            this._vaiTroNguoiDungService = vaiTroNguoiDungService;
            this._NhanHienThiService = NhanHienThiService;
            this._workContext = workContext;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._DonViService = DonViService;
            this._DonViModelFactory = DonViModelFactory;
            this._nguoiDungDonViService = nguoiDungDonViService;
            this._dongBoFactory = dongBoFactory;
            this._gSConfig = gSConfig;
            this._donViService = donViService;
            this._fileProvider = fileProvider;
            this._fileCongViecService = fileCongViecService;
            this._reportModelFactory = reportModelFactory;
            this._cauHinhService = cauHinhService;

        }
        #endregion
        #region Methods

        #region Nguoi Dung

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDung))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new NguoiDungSearchModel();
            var model = _itemModelFactory.PrepareNguoiDungSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(NguoiDungSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDung))
                return AccessDeniedKendoGridJson();
            //prepare model
            if (searchModel.PageSize == 0) searchModel.PageSize = 10;
            //searchModel.DonViId = _workContext.CurrentDonVi.ID;
            //searchModel.DonViBoTinhId = _workContext.CurrentDonVi.ID;
            var isDpac = _workContext.CurrentDonVi.ID == (decimal)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG;
            //Dpac với quản trị thì cho xem hết người dùng
            if (!(isDpac && _workContext.CurrentCustomer.IS_QUAN_TRI))
            {
                //searchModel.DonViId = 0;
                searchModel.DonViId = _donViService.GetDonViLonNhat(_workContext.CurrentDonVi.ID)?.ID ?? 0;
            }

            var model = _itemModelFactory.PrepareNguoiDungListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDung))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareNguoiDungModel(new NguoiDungModel(), null);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(NguoiDungModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDung))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<NguoiDung>();
                var saltKey = _encryptionService.CreateSaltKey(GSCustomerServiceDefaults.PasswordSaltKeySize);
                item.TMP_MAT_KHAU = model.MAT_KHAU;
                item.MAT_KHAU = _encryptionService.CreatePasswordHash(model.MAT_KHAU, saltKey, "SHA512");
                item.MAT_KHAU_KEY = saltKey;
                item.PASSWORD_HASH = _encryptionService.CreatePasswordHashKhoCSDLTSC(model.MAT_KHAU);
                item.NGAY_TAO = DateTime.Now;
                item.NGUOI_TAO = _workContext.CurrentCustomer.ID;
                item.TEN_DANG_NHAP = item.TEN_DANG_NHAP;
                if (model.SelectedVaiTro != null && model.SelectedVaiTro.Count() > 0)
                {
                    var VaiTros = _vaiTroService.GetVaiTros(model.SelectedVaiTro);
                    foreach (var vt in VaiTros)
                    {
                        item.vaiTroNguoiDungMappings.Add(new VaiTroNguoiDungMapping { vaiTro = vt });
                    }
                }
                _itemService.InsertNguoiDung(item, _workContext.CurrentDonVi.ID);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin", item.ToModel<NguoiDungModel>(), "NguoiDung");
                model.ID = item.ID;
                model.GUID = item.GUID;
                _dongBoFactory.DongBoDanhMuc<NguoiDungModel>(new List<NguoiDungModel>() { model });
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { guid = item.GUID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareNguoiDungModel(model, null);
            return View(model);
        }



        public virtual IActionResult Edit(Guid guid)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDung))
                return AccessDeniedView();

            var item = _itemService.GetNguoiDungByGuid(guid);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareNguoiDungModel(null, item);
            model.IsEdit = true;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(NguoiDungModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDung))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetNguoiDungById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                var obj = item;
                _itemModelFactory.PrepareNguoiDung(model, item);
                item.vaiTroNguoiDungMappings.Clear();
                if (model.SelectedVaiTro != null && model.SelectedVaiTro.Count() > 0)
                {
                    var VaiTros = _vaiTroService.GetVaiTros(model.SelectedVaiTro);
                    foreach (var vt in VaiTros)
                    {
                        item.vaiTroNguoiDungMappings.Add(new VaiTroNguoiDungMapping { vaiTro = vt });
                    }
                }
                _itemService.UpdateNguoiDung(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", obj.ToModel<NguoiDungModel>(), "NguoiDung");
                _dongBoFactory.DongBoDanhMuc<NguoiDungModel>(new List<NguoiDungModel>() { model });
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { guid = item.GUID }) : RedirectToAction("List");
                //return JsonSuccessMessage("Cập nhật dữ liệu thành công !", model);
            }
            //prepare model
            model = _itemModelFactory.PrepareNguoiDungModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id, bool isUnlock = false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDung))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetNguoiDungById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                if (isUnlock)
                {
                    item.TRANG_THAI_ID = (int)NguoiDungStatusID.ok;
                    _itemService.UpdateNguoiDung(item);
                    //activity log  
                    _hoatdongService.InsertHoatDong(enumHoatDong.Khoa, "Mở khóa tài khoản người dùng", item.ToModel<NguoiDungModel>(), "NguoiDung");
                    return JsonSuccessMessage("Mở khóa tài khoản thành công");
                }
                item.TRANG_THAI_ID = (int)NguoiDungStatusID.Lock;
                _itemService.UpdateNguoiDung(item);
                //activity log  
                _hoatdongService.InsertHoatDong(enumHoatDong.Khoa, "Khóa tài khoản người dùng", item.ToModel<NguoiDungModel>(), "NguoiDung");
                return JsonSuccessMessage("Khóa tài khoản thành công");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = item.ID });
            }
        }

        public virtual IActionResult Logout()
        {

            _authenticationService.DangXuat();
            _workContext.CurrentCustomer = null;
            //HttpContext.Session.Remove("CurrentCustomer");
            HttpContext.Session.Clear();
            return RedirectToRoute("HomePage");
        }
        public virtual IActionResult LogoutAjax()
        {

            _authenticationService.DangXuat();
            _workContext.CurrentCustomer = null;
            //HttpContext.Session.Remove("CurrentCustomer");
            HttpContext.Session.Clear();
            return JsonSuccessMessage(_gSConfig.IsUsingSSO ? "true" : "false");
        }

        public virtual IActionResult DoiMatKhau()
        {
            var model = new DoiMatKhauModel();
            var _NguoiDung = _workContext.CurrentCustomer;
            model.IDNguoiDung = _NguoiDung.ID;
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult DoiMatKhau(DoiMatKhauModel model)
        {
            var _NguoiDung = _workContext.CurrentCustomer;
            if (ModelState.IsValid)
            {
                var checkOldPass = _itemService.ValidateNguoiDung(_NguoiDung.TEN_DANG_NHAP, model.MATKHAUCU);
                if (checkOldPass == NguoiDungKetQuaDangNhap.Successful)
                {
                    var _nguoidungupd = _itemService.GetNguoiDungById(_workContext.CurrentCustomer.ID);
                    var saltKey = _encryptionService.CreateSaltKey(GSCustomerServiceDefaults.PasswordSaltKeySize);

                    _nguoidungupd.MAT_KHAU = _encryptionService.CreatePasswordHash(model.MATKHAUMOI, saltKey, "SHA512");
                    _nguoidungupd.MAT_KHAU_KEY = saltKey;
                    _nguoidungupd.PASSWORD_HASH = _encryptionService.CreatePasswordHashKhoCSDLTSC(model.MATKHAUMOI);
                    _itemService.UpdateNguoiDung(_nguoidungupd);
                    model.KETQUA = "Thay đổi thành công";
                    _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Đổi mật khẩu", _nguoidungupd.ToModel<NguoiDungModel>(), "NguoiDung");
                    SuccessNotification("Thay đổi thành công");
                    return View(model);
                }
                JsonErrorMessage("Mật khẩu cũ không đúng");
                return RedirectToAction("DoiMatKhau");
            }

            return View(model);
        }

        public virtual IActionResult Detail(Guid guid)
        {


            var item = _itemService.GetNguoiDungByGuid(guid);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareNguoiDungModel(null, item);
            return View(model);
        }
        // Đơn vị
        public virtual IActionResult _AddDonVi(int nguoiDungId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDung))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new DonViSearchModel();
            var model = _DonViModelFactory.PrepareDonViSearchModel(searchmodel);
            searchmodel.nguoiDungId = nguoiDungId;
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult _AddDonVi(DonViSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDung))
                return AccessDeniedKendoGridJson();
            //prepare model 
            //searchModel.donViId = _workContext.CurrentDonVi.ID;
            var isDpac = _workContext.CurrentDonVi.ID == (decimal)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG;
            //Dpac với quản trị thì cho xem hết người dùng
            //if ((isDpac && _workContext.CurrentCustomer.IS_QUAN_TRI))
            if (!(isDpac && _workContext.CurrentCustomer.IS_QUAN_TRI))
            {
                //searchModel.DonViId = 0;
                searchModel.donViId = _donViService.GetDonViLonNhat(_workContext.CurrentDonVi.ID)?.ID ?? 0;
            }
            var models = _DonViModelFactory.PrepareDonViListModel(searchModel);
            return Json(models);
        }

        [HttpPost]
        public virtual IActionResult _ListDonVi(decimal DON_VI_ID, decimal Id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDung))
                return AccessDeniedKendoGridJson();
            //try to get a store with the specified id

            var nguoiDungDonVi = new NguoiDungDonViMapping();
            nguoiDungDonVi.DON_VI_ID = DON_VI_ID;
            nguoiDungDonVi.NGUOI_DUNG_ID = Id;
            _nguoiDungDonViService.InsertNguoiDungDonVi(nguoiDungDonVi);
            var item = _DonViService.GetDonViById(DON_VI_ID);
            var model = item.ToModel<DonViModel>();
            SuccessNotification("Cập nhật dữ liệu thành công");
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult _deleteNguoiDungDonVi(decimal DON_VI_ID, decimal Id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDung))
                return AccessDeniedKendoGridJson();
            //try to get a store with the specified id
            var nguoidungDonViMapping = _nguoiDungDonViService.GetNguoiDungDonViMapping(NguoiDungId: Id, DonViId: DON_VI_ID);
            _nguoiDungDonViService.DeleteNguoiDungDonVi(nguoidungDonViMapping);
            var maps = _nguoiDungDonViService.GetMapByNguoiDungId(Id);
            //activity log

            SuccessNotification("Xoá dữ liệu thành công");
            return Json(maps);
        }
        public virtual IActionResult _ListHoatDong(decimal nguoiDungId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDung))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new HoatDongSearchModel();
            var model = _hoatDongModelFactory.PrepareHoatDongSearchModel(searchmodel);
            searchmodel.nguoiDungId = nguoiDungId;
            return PartialView(model);
        }
        //Reset mật khẩu
        public virtual IActionResult _ResetMatKhau(decimal IDNguoiDung)
        {
            var model = new DoiMatKhauModel();
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult _ResetMatKhau(DoiMatKhauModel model, decimal IDNguoiDung)
        {
            var _NguoiDung = _itemService.GetNguoiDungById(IDNguoiDung);

            if (ModelState.IsValid)
            {
                var NguoiDungLogin = _workContext.CurrentCustomer;
                var checkOldPass = _itemService.validateResetUser(_NguoiDung.TEN_DANG_NHAP);
                //check pass cũ nếu đang đổi mật khẩu của chính mình
                if (NguoiDungLogin.ID == _NguoiDung.ID)
                {
                    checkOldPass = _itemService.ValidateNguoiDung(_NguoiDung.TEN_DANG_NHAP, model.MATKHAUCU);
                }
                else
                {
                    //quản trị viên này không được reset mật khẩu của quản trị viên khác
                    if (_NguoiDung.IS_QUAN_TRI == true)
                    {
                        return JsonErrorMessage("Bạn không được khôi phục mật khẩu của quản trị khác", model);
                    }
                }
                if (checkOldPass == NguoiDungKetQuaDangNhap.Successful)
                {
                    var _nguoidungupd = _itemService.GetNguoiDungById(IDNguoiDung);
                    var saltKey = _encryptionService.CreateSaltKey(GSCustomerServiceDefaults.PasswordSaltKeySize);
                    _nguoidungupd.MAT_KHAU = _encryptionService.CreatePasswordHash(model.MATKHAUMOI, saltKey, "SHA512");
                    _nguoidungupd.MAT_KHAU_KEY = saltKey;
                    _nguoidungupd.PASSWORD_HASH = _encryptionService.CreatePasswordHashKhoCSDLTSC(model.MATKHAUMOI);
                    _itemService.UpdateNguoiDung(_nguoidungupd);
                    model.KETQUA = "Thay đổi thành công";
                    _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Đổi mật khẩu", _nguoidungupd.ToModel<NguoiDungModel>(), "NguoiDung");
                    return JsonSuccessMessage("Cập nhật mật khẩu thành công !", model);
                }
                else if (checkOldPass == NguoiDungKetQuaDangNhap.WrongPassword)
                {
                    return JsonErrorMessage("Mật khẩu cũ không chính xác", model);
                }
                else if (checkOldPass == NguoiDungKetQuaDangNhap.CustomerNotExist)
                {
                    return JsonErrorMessage("Tài khoản không tồn tại", model);
                }
                else if (checkOldPass == NguoiDungKetQuaDangNhap.LockedOut)
                {
                    return JsonErrorMessage("Tài khoản chờ duyệt", model);
                }
            }
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            return JsonErrorMessage("Error", list);
        }
        public virtual IActionResult _ChangeMatKhauByKhoCSDL(decimal IDNguoiDung, bool isResetPassword)
        {
            var model = new DoiMatKhauModel();
            model.isResetmk = isResetPassword;
            model.isNotCurrent = _workContext.CurrentCustomer.ID != IDNguoiDung;
            return PartialView(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> _ChangeMatKhauByKhoCSDL(DoiMatKhauModel model, decimal IDNguoiDung)
        {
            var _NguoiDung = _itemService.GetNguoiDungById(IDNguoiDung);
            if (ModelState.IsValid)
            {
                if (_gSConfig.IsUsingSSO)
                {
                    var request = new PasswordRequestModel() { username = _NguoiDung.TEN_DANG_NHAP, password = model.MATKHAUCU, newPassword = model.MATKHAUMOI };
                    var response = await _dongBoFactory.ChangePasswordByKhoCSDL(request);
                    if (response.Status)
                    {
                        return JsonSuccessMessage("Đổi mật khẩu thành công");
                    }
                    ModelState.AddModelError(nameof(DoiMatKhauModel.KhoCSDLValidation), response.Message);
                    return JsonErrorMessage("Error", ModelState.Values.Where(c => c.Errors.Count > 0).ToList());
                }
                else
                {
                    var NguoiDungLogin = _workContext.CurrentCustomer;
                    NguoiDungKetQuaDangNhap checkOldPass = NguoiDungKetQuaDangNhap.Successful;
                    //nếu không là quản trị thì check pass cũ, hoặc là quản trị và đang đổi mật khẩu của chính mình
                    if (NguoiDungLogin.IS_QUAN_TRI != true || NguoiDungLogin.ID == _NguoiDung.ID)
                    {
                        checkOldPass = _itemService.ValidateNguoiDung(_NguoiDung.TEN_DANG_NHAP, model.MATKHAUCU);
                    }
                    else
                    {
                        //quản trị viên này không được reset mật khẩu của quản trị viên khác
                        if (_NguoiDung.IS_QUAN_TRI == true)
                        {
                            return JsonErrorMessage("Bạn không được khôi phục mật khẩu của quản trị khác", model);
                        }
                    }
                    //nếu mật khẩu cũ chính xác hoặc các điều kiện trên đạt thì tiến hành đổi mật khẩu
                    if (checkOldPass == NguoiDungKetQuaDangNhap.Successful)
                    {
                        var _nguoidungupd = _itemService.GetNguoiDungById(IDNguoiDung);
                        var saltKey = _encryptionService.CreateSaltKey(GSCustomerServiceDefaults.PasswordSaltKeySize);
                        _nguoidungupd.MAT_KHAU = _encryptionService.CreatePasswordHash(model.MATKHAUMOI, saltKey, "SHA512");
                        _nguoidungupd.MAT_KHAU_KEY = saltKey;
                        _nguoidungupd.PASSWORD_HASH = _encryptionService.CreatePasswordHashKhoCSDLTSC(model.MATKHAUMOI);
                        _itemService.UpdateNguoiDung(_nguoidungupd);
                        model.KETQUA = "Thay đổi thành công";
                        _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Đổi mật khẩu", _nguoidungupd.ToModel<NguoiDungModel>(), "NguoiDung");
                        return JsonSuccessMessage("Cập nhật mật khẩu thành công !", model);
                    }
                    else
                    {
                        return JsonErrorMessage("Mật khẩu cũ không chính xác", model);
                    }
                }

            }
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            return JsonErrorMessage("Error", list);
        }
        [HttpPost]
        public virtual async Task<IActionResult> _ResetMatKhauByKhoCSDL(DoiMatKhauModel model, decimal IDNguoiDung)
        {
            var _NguoiDung = _itemService.GetNguoiDungById(IDNguoiDung);
            if (ModelState.IsValid)
            {
                var request = new PasswordRequestModel() { username = _NguoiDung.TEN_DANG_NHAP, password = model.MATKHAUCU, newPassword = model.MATKHAUMOI, TokenSSO = model.TokenSSO };
                var response = await _dongBoFactory.ResetPasswordByKhoCSDL(request);
                if (response.Status)
                {
                    return JsonSuccessMessage("Đặt lại mật khẩu thành công");
                }
                ModelState.AddModelError(nameof(DoiMatKhauModel.KhoCSDLValidation), response.Message);
                return JsonErrorMessage("Error", ModelState.Values.Where(c => c.Errors.Count > 0).ToList());
            }
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            return JsonErrorMessage("Error", list);
        }
        [HttpGet]
        public virtual IActionResult ImportExcelNguoiDung()
        {
            return View(new NguoiDungSearchModel());
        }
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public virtual IActionResult ImportExcelNguoiDung(IFormFile file)
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
            //_fileCongViecModelFactory.SaveWorkFileOnDisk(fwork, dataByte);

            var fileInfo = new FileInfo(_fileStore);
            var package = new ExcelPackage(fileInfo);

            ExcelWorksheets workSheets = package.Workbook.Worksheets;
            List<MessageReturn> ListResult = new List<MessageReturn>();
            if (workSheets.Count < 1)
            {
                //ErrorMessageJson("File không chuẩn dữ liệu");
                return null;
            }
            ExcelWorksheet workSheet1 = workSheets.First();
            List<ExcelModel> lstSettings = new List<ExcelModel>();
            string strSettings = _fileProvider.ReadAllText(_fileProvider.MapPath("~/App_Data/jsonSettings_ImpExcelNguoiDung.json"), Encoding.UTF8);
            lstSettings = strSettings.toEntities<ExcelModel>();
            int maxCol = lstSettings != null ? lstSettings.Max(c => c.COL) : 0;
            if (maxCol < workSheet1.Dimension.End.Column)
                ErrorNotification("Không đúng định dạng");
            List<NguoiDungExcelModel> lstErr = new List<NguoiDungExcelModel>();
            List<NguoiDungModel> lstImp = new List<NguoiDungModel>();
            for (int rowNumber = 2; rowNumber <= workSheet1.Dimension.End.Row; rowNumber++)
            {
                ExcelRange row = workSheet1.Cells[rowNumber, 1, rowNumber, workSheet1.Dimension.End.Column];
                NguoiDungExcelModel model = new NguoiDungExcelModel();
                model = row.toEntity<NguoiDungExcelModel>(model, rowNumber, lstSettings);
                if (String.IsNullOrEmpty(model.TEN_DANG_NHAP))
                {
                    model.ERROR_MESSGAE = "TEN_DANG_NHAP is null";
                    lstErr.Add(model);
                    continue;
                }
                if (String.IsNullOrEmpty(model.MA_VAI_TRO))
                {
                    model.ERROR_MESSGAE = "MA_VAI_TRO is null";
                    lstErr.Add(model);
                    continue;
                }
                else
                {
                    var vaiTro = _vaiTroService.GetVaiTroByMa(model.MA_VAI_TRO);
                    if (vaiTro != null)
                        model.VAI_TRO_ID = vaiTro.ID;
                    else
                    {
                        model.ERROR_MESSGAE = "MA_VAI_TRO is not found";
                        lstErr.Add(model);
                        continue;
                    }
                }
                if (String.IsNullOrEmpty(model.MA_DON_VI))
                {
                    model.ERROR_MESSGAE = "MA_DON_VI is null";
                    lstErr.Add(model);
                    continue;
                }
                else
                {
                    var donvi = _donViService.GetDonViByMa(model.MA_DON_VI);
                    if (donvi != null)
                        model.CURRENT_DON_VI_ID = donvi.ID;
                    else
                    {
                        model.ERROR_MESSGAE = "MA_DON_VI is not found";
                        lstErr.Add(model);
                        continue;
                    }
                }
                NguoiDung entity = _itemService.GetNguoiDungByUsername(model.TEN_DANG_NHAP);
                if (entity == null)
                {
                    entity = new NguoiDung();
                    entity = model.ToEntity<NguoiDung>();
                    entity.NGAY_TAO = DateTime.Now;
                }
                else
                {
                    var mapNguoiDungDonVi = _nguoiDungDonViService.GetMapByNguoiDungId(entity.ID);
                    if (mapNguoiDungDonVi != null && mapNguoiDungDonVi.Count > 0)
                        _nguoiDungDonViService.DeleteNguoiDungDonVi(mapNguoiDungDonVi.ToList());

                    var vaiTroNguoiDungs = _vaiTroNguoiDungService.GetMapByNguoiDungId(entity.ID);
                    if (vaiTroNguoiDungs != null && vaiTroNguoiDungs.Count > 0)
                        _vaiTroNguoiDungService.DeleteVaiTroNguoiDung(vaiTroNguoiDungs.ToList());
                }
                var saltKey = _encryptionService.CreateSaltKey(GSCustomerServiceDefaults.PasswordSaltKeySize);
                entity.MAT_KHAU = _encryptionService.CreatePasswordHash(model.MAT_KHAU, saltKey, "SHA512");
                entity.MAT_KHAU_KEY = saltKey;
                entity.PASSWORD_HASH = _encryptionService.CreatePasswordHashKhoCSDLTSC(model.MAT_KHAU);

                //entity.TEN_DANG_NHAP = entity.TEN_DANG_NHAP.ToLower();

                if (entity.ID == 0)
                    _itemService.InsertNguoiDung(entity, model.CURRENT_DON_VI_ID);
                else
                {
                    _itemService.UpdateNguoiDung(entity);
                    var nguoiDungDonVi = new NguoiDungDonViMapping();
                    nguoiDungDonVi.DON_VI_ID = model.CURRENT_DON_VI_ID.Value;
                    nguoiDungDonVi.NGUOI_DUNG_ID = entity.ID;
                    _nguoiDungDonViService.InsertNguoiDungDonVi(nguoiDungDonVi);
                }
                VaiTroNguoiDungMapping vaiTroNguoiDung = new VaiTroNguoiDungMapping()
                {
                    NGUOI_DUNG_ID = entity.ID,
                    VAI_TRO_ID = model.VAI_TRO_ID
                };
                _vaiTroNguoiDungService.InsertVaiTroNguoiDung(vaiTroNguoiDung);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin", entity.ToModel<NguoiDungModel>(), "NguoiDung");
                var nguoiDungModel = entity.ToModel<NguoiDungModel>();
                nguoiDungModel.MAT_KHAU = model.MAT_KHAU;
                nguoiDungModel.MA_VAI_TRO = model.MA_VAI_TRO;
                nguoiDungModel.MA_DON_VI = model.MA_DON_VI;
                lstImp.Add(nguoiDungModel);
            }
            if (lstImp.Count > 0)
            {
                _dongBoFactory.DongBoDanhMuc<NguoiDungModel>(lstImp);
            }
            if (lstErr != null && lstErr.Count > 0)
            {
                string fName = $"NguoiDung_{lstErr.Count}({DateTime.Now.ToString("dd-MM-yyyy hh-mm")})";
                MemoryStream stream = new MemoryStream();
                bool addSTT = true;
                stream = _reportModelFactory.prepareExcelEntity<NguoiDungExcelModel>(stream, lstErr, "NguoiDung", addSTT);

                var dataByte = stream.ToArray();
                var fileExtension = ".xlsx";
                //  lưu file 
                var contentType = MimeTypes.TextXlsx;
                var fwork = new FileCongViec
                {
                    GUID = Guid.NewGuid(),
                    NOI_DUNG_FILE = dataByte,
                    LOAI_FILE = contentType,
                    //we store filename without extension for downloads
                    TEN_FILE = fName,
                    DUOI_FILE = fileExtension,
                    NGAY_TAO = DateTime.Now,
                    NGUOI_TAO = _workContext.CurrentCustomer.ID
                };
                _fileCongViecService.InsertFileCongViec(fwork);
                return Json(new
                {
                    success = false,
                    ListSuccess = lstImp,
                    ListError = lstErr,
                    fileGuid = fwork.GUID.ToString(),
                });
            }

            return Json(new
            {
                success = true,
                ListSuccess = lstImp,
            });
        }

        [HttpPost]
        public virtual async Task<IActionResult> Check_username(string name)
        {
            /*var check = _itemService.GetNguoiDungByUsername(name);
            if (check == null)
            {
                return JsonSuccessMessage("Done");
            }
            return JsonErrorMessage("Tên đăng nhập đã tồn tại", check);*/
            var response = await _dongBoFactory.GetNguoiDungByKhoCSDL(name);
            if (response.Status)
            {
                return JsonSuccessMessage("Done");
            }
            return JsonErrorMessage("Tên đăng nhập đã tồn tại");
        }
        #endregion

        #region nguoi dung cap don vi
        //danh sach don vi truc thuoc don vi
        public virtual IActionResult _ListDonViTT()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDungDonVi))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new DonViSearchModel();
            var model = _DonViModelFactory.PrepareDonViSearchModel(searchmodel);
            searchmodel.nguoiDungId = _workContext.CurrentCustomer.ID;
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult _ListDonViTT(DonViSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDungDonVi))
                return AccessDeniedKendoGridJson();
            //prepare model 
            //searchModel.donViId = _workContext.CurrentDonVi.ID;
            var isDpac = _workContext.CurrentDonVi.ID == (decimal)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG;
            //Dpac với quản trị thì cho xem hết người dùng
            //if ((isDpac && _workContext.CurrentCustomer.IS_QUAN_TRI))
            if (!(isDpac && _workContext.CurrentCustomer.IS_QUAN_TRI))
            {
                //searchModel.DonViId = 0;
                searchModel.donViId = _donViService.GetDonViTrucThuoc(_workContext.CurrentDonVi.ID)?.ID ?? 0;
            }
            var models = _DonViModelFactory.PrepareDonViListModel(searchModel);
            return Json(models);
        }
        //Don vi da chon 
        [HttpGet]
        public virtual IActionResult _ListDonViDaChon(decimal DON_VI_ID)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDungDonVi))
                return AccessDeniedKendoGridJson();
            DonViModel model = new DonViModel();
            var tmp = _DonViService.CheckDonVi(DON_VI_ID, _workContext.CurrentCustomer.ID);
            if (tmp)
            {
                var item = _DonViService.GetDonViById(DON_VI_ID);
                model = item.ToModel<DonViModel>();
                //SuccessNotification("Cập nhật dữ liệu thành công");
                //return PartialView(model);
            }
            return PartialView(model);
        }
        //them don vi quan ly cho nguoi dung  
        [HttpPost]
        public virtual IActionResult _ListDonViDaChon(decimal DON_VI_ID, decimal Id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDungDonVi))
                return AccessDeniedKendoGridJson();
            var nguoidungDonViMapping = _nguoiDungDonViService.GetNguoiDungDonViMapping(NguoiDungId: Id, DonViId: DON_VI_ID);
            if (nguoidungDonViMapping != null)
            {
                return AccessDeniedKendoGridJson();
            }
            DonViModel model = new DonViModel();
            var tmp = _DonViService.CheckDonVi(DON_VI_ID, _workContext.CurrentCustomer.ID);
            if (tmp)
            {
                var nguoiDungDonVi = new NguoiDungDonViMapping();
                nguoiDungDonVi.DON_VI_ID = DON_VI_ID;
                nguoiDungDonVi.NGUOI_DUNG_ID = Id;
                _nguoiDungDonViService.InsertNguoiDungDonVi(nguoiDungDonVi);
                var item = _DonViService.GetDonViById(DON_VI_ID);
                model = item.ToModel<DonViModel>();
                //SuccessNotification("Cập nhật dữ liệu thành công");
                //return PartialView(model);
            }
            return PartialView(model);
        }
        //xóa don vi quan ly cua nguoi dung 
        [HttpPost]
        public virtual IActionResult _deleteDonViDaChon(decimal DON_VI_ID, decimal Id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDungDonVi))
                return AccessDeniedKendoGridJson();
            //try to get a store with the specified id
            var nguoidungDonViMapping = _nguoiDungDonViService.GetNguoiDungDonViMapping(NguoiDungId: Id, DonViId: DON_VI_ID);
            _nguoiDungDonViService.DeleteNguoiDungDonVi(nguoidungDonViMapping);
            var maps = _nguoiDungDonViService.GetMapByNguoiDungId(Id);
            //activity log

            SuccessNotification("Xoá dữ liệu thành công");
            return Json(maps);
        }
        //lấy theo đơn vị trực thu
        public virtual IActionResult ListByDonVi()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDungDonVi))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new NguoiDungSearchModel();
            var model = _itemModelFactory.PrepareNguoiDungSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult ListByDonVi(NguoiDungSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDungDonVi))
                return AccessDeniedKendoGridJson();
            //prepare model
            if (searchModel.PageSize == 0) searchModel.PageSize = 10;
            //searchModel.DonViId = _workContext.CurrentDonVi.ID;
            //searchModel.DonViBoTinhId = _workContext.CurrentDonVi.ID;
            var isDpac = _workContext.CurrentDonVi.ID == (decimal)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG;
            //Dpac với quản trị thì cho xem hết người dùng
            //if ((isDpac && _workContext.CurrentCustomer.IS_QUAN_TRI))
            if (!(isDpac && _workContext.CurrentCustomer.IS_QUAN_TRI))
            {
                //searchModel.DonViId = 0;
                searchModel.DonViId = _donViService.GetDonViLonNhat(_workContext.CurrentDonVi.ID)?.ID ?? 0;
            }

            var model = _itemModelFactory.PrepareNguoiDungListModel(searchModel);
            return Json(model);
        }
        //Thêm mới cấp đơn vị
        public virtual IActionResult CreateByDonVi()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDungDonVi))
                return AccessDeniedView();
            //prepare model
            NguoiDungModel nguoiDungModel = new NguoiDungModel();
            //nguoiDungModel.ID = _workContext.CurrentCustomer.ID;
            var model = _itemModelFactory.PrepareNguoiDungModelDuToanCap2(nguoiDungModel, null);
            model.IsCapDonVi = true;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> CreateByDonVi(NguoiDungModel model, string[] dvma, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDungDonVi))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                //if (!model.IsCapDonVi)
                //{
                //    return JsonErrorMessage("Tạo mới thất bại !", ModelState.Values);
                //}
                //var response = await _dongBoFactory.GetNguoiDungByKhoCSDL(model.TEN_DANG_NHAP);
                //if (response.Status)
                //{
                //    ModelState.AddModelError("TEN_DANG_NHAP", "Tên nhập vào đã tồn tại trên kho");
                //    return JsonErrorMessage("Tạo mới thất bại !", ModelState.Values);
                //}
                //else
                //{
                //    if (response.Data == 1)
                //    {
                //        return JsonErrorMessage(response.Message, ModelState.Values);
                //    }
                //}
                var item = model.ToEntity<NguoiDung>();
                var saltKey = _encryptionService.CreateSaltKey(GSCustomerServiceDefaults.PasswordSaltKeySize);
                item.TMP_MAT_KHAU = model.MAT_KHAU;
                item.MAT_KHAU = _encryptionService.CreatePasswordHash(model.MAT_KHAU, saltKey, "SHA512");
                item.PASSWORD_HASH = _encryptionService.CreatePasswordHashKhoCSDLTSC(model.MAT_KHAU);
                item.MAT_KHAU_KEY = saltKey;
                item.NGAY_TAO = DateTime.Now;
                item.NGUOI_TAO = _workContext.CurrentCustomer.ID;
                item.TEN_DANG_NHAP = item.TEN_DANG_NHAP;
                if (model.SelectedVaiTro != null && model.SelectedVaiTro.Count() > 0)
                {
                    var VaiTros = _vaiTroService.GetVaiTros(model.SelectedVaiTro);
                    foreach (var vt in VaiTros)
                    {
                        item.vaiTroNguoiDungMappings.Add(new VaiTroNguoiDungMapping { vaiTro = vt });
                    }
                }
                List<decimal> donViId = new List<decimal>();
                if (dvma.Length > 0)
                {
                    for (int i = 0; i < dvma.Length; i++)
                    {
                        DonVi donvi = new DonVi();
                        donvi = _donViService.GetReadOnlyDonViByMa(dvma[i]);
                        donViId.Add(donvi.ID);
                    }
                }
                else
                {
                    ModelState.AddModelError("DON_VI_QUAN_LY", "Bạn cần kéo xuống để chọn đơn vị trước khi lưu");
                    return JsonErrorMessage("Tạo mới thất bại !", ModelState.Values);
                }
                _itemService.InsertNguoiDungV2(item, donViId);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin", item.ToModel<NguoiDungModel>(), "NguoiDung");
                //model.ID = item.ID;
                //model.GUID = item.GUID;
                //_dongBoFactory.DongBoDanhMuc<NguoiDungModel>(new List<NguoiDungModel>() { model });

                return Json(MessageReturn.CreateSuccessMessage("Tạo mới dữ liệu thành công !", true));
            }
            return JsonErrorMessage("Tạo mới thất bại !", ModelState.Values);
        }

        //update cấp đơn vị
        public virtual IActionResult EditByDonVi(Guid guid)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDungDonVi))
                return AccessDeniedView();

            var item = _itemService.GetNguoiDungByGuid(guid);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareNguoiDungModelDuToanCap2(null, item);
            model.IsEdit = true;
            model.IsCapDonVi = true;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult EditByDonVi(NguoiDungModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDungDonVi))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetNguoiDungById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {

                if (!model.IsCapDonVi)
                {
                    ErrorNotification("Cập nhật dữ liệu thất bại !");
                    return RedirectToAction("EditByDonVi", new { guid = item.GUID });
                }
                /*var vaitros = _cauHinhService.GetCauHinh("cauhinh.danhsachvaitro").GIA_TRI.toEntities<int>();
                foreach(var tmp in item.vaiTroNguoiDungMappings)
                {
                    if (!vaitros.Contains(Convert.ToInt32(tmp.VAI_TRO_ID)))
                    {
                        vaitros.Add(Convert.ToInt32(tmp.VAI_TRO_ID));
                    }
                }    */
                var obj = item;
                _itemModelFactory.PrepareNguoiDung(model, item);
                item.vaiTroNguoiDungMappings.Clear();
                if (model.SelectedVaiTro != null && model.SelectedVaiTro.Count() > 0)
                {
                    var VaiTros = _vaiTroService.GetVaiTros(model.SelectedVaiTro);
                    foreach (var vt in VaiTros)
                    {
                        item.vaiTroNguoiDungMappings.Add(new VaiTroNguoiDungMapping { vaiTro = vt });
                        /*bool containsItem = vaitros.Any(c => c == vt.ID);
                        if (containsItem)
                        {
                            item.vaiTroNguoiDungMappings.Add(new VaiTroNguoiDungMapping { vaiTro = vt });
                        }
                        else
                        {
                            ErrorNotification("Xảy ra vi phạm về cấp quyền !");
                            return RedirectToAction("EditByDonVi", new { guid = item.GUID });
                        }*/
                    }
                }
                _itemService.UpdateNguoiDung(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", obj.ToModel<NguoiDungModel>(), "NguoiDung");
                //_dongBoFactory.DongBoDanhMuc<NguoiDungModel>(new List<NguoiDungModel>() { model });
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("EditByDonVi", new { guid = item.GUID }) : RedirectToAction("ListByDonVi");
                //return JsonSuccessMessage("Cập nhật dữ liệu thành công !", model);
            }
            /*ErrorNotification("Cập nhật dữ liệu thất bại !");
            return RedirectToAction("EditByDonVi", new { guid = item.GUID });*/
            //prepare model
            model = _itemModelFactory.PrepareNguoiDungModelDuToanCap2(model, item, true);
            ErrorNotification("Cập nhật dữ liệu thất bại !");
            return View(model);
        }
        public virtual IActionResult ListYeuCauDuyetNguoiDung(int HanhDong)
        {
            if ((enumHanhDongListDuyetTaiSan)HanhDong == enumHanhDongListDuyetTaiSan.DUYET)
            {
                if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDuyetNguoiDung))
                    return AccessDeniedView();
            }
            var searchModel = new NguoiDungSearchModel();
            var listExuclue = (new NguoiDungStatusID()).ToSelectList(valuesToExclude: new int[] { (int)NguoiDungStatusID.choduyet, (int)NguoiDungStatusID.ok }).Select(c => c.Value.ToNumberInt32()).ToArray();
            searchModel.ddlTrangThai = (new NguoiDungStatusID()).ToSelectList(valuesToExclude: listExuclue).ToList();
            var model = _itemModelFactory.PrepareNguoiDungSearchModel(searchModel: searchModel);
            return View(model);
        }
        [HttpPost]
        public virtual IActionResult ListYeuCauDuyetNguoiDung(NguoiDungSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDuyetNguoiDung))
                return AccessDeniedView();
            //searchModel.DonViId = _workContext.CurrentDonVi.ID;
            var isDpac = _workContext.CurrentDonVi.ID == (decimal)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG;
            //Dpac với quản trị thì cho xem hết người dùng
            if (!(isDpac && _workContext.CurrentCustomer.IS_QUAN_TRI))
            {
                //searchModel.DonViId = 0;
                searchModel.DonViId = _donViService.GetDonViLonNhat(_workContext.CurrentDonVi.ID)?.ID ?? 0;
            }
            //prepare model
            var model = _itemModelFactory.PrepareDuyetNguoiDungListModel(searchModel);

            return Json(model);
        }
        [HttpPost]
        public virtual async Task<IActionResult> DuyetTaiKhoan(string strTaiKhoanIds)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDuyetNguoiDung))
                return AccessDeniedView();
            var lstTaiKhoanId = strTaiKhoanIds.Split("-").ToList();
            ResponseApi response = new ResponseApi();
            foreach (var _strID in lstTaiKhoanId)
            {
                NguoiDungModel nguoiDung = new NguoiDungModel();
                var tk = _itemService.GetNguoiDungById(_strID.ToNumberInt32());
                if (!_gSConfig.IsUsingSSO)
                {
                    tk.TRANG_THAI_ID = 1;
                    _itemService.UpdateNguoiDung(tk);
                }
                else
                {
                    if (tk.TMP_MAT_KHAU != null)
                    {
                        nguoiDung.TMP_MAT_KHAU = tk.TMP_MAT_KHAU;
                    }
                    //var res = _itemModelFactory.DuyetTaiKhoan(tk.ID);
                    nguoiDung.TEN_DANG_NHAP = tk.TEN_DANG_NHAP;
                    nguoiDung.TEN_DAY_DU = tk.TEN_DAY_DU;
                    nguoiDung.EMAIL = tk.EMAIL;
                    nguoiDung.TRANG_THAI_ID = (int)GS.Core.Domain.HeThong.NguoiDungStatusID.ok;
                    nguoiDung.IS_QUAN_TRI = tk.IS_QUAN_TRI;
                    nguoiDung.MOBILE = tk.MOBILE;
                    nguoiDung.MAT_KHAU = tk.MAT_KHAU;
                    nguoiDung.PASSWORD_HASH = tk.PASSWORD_HASH;

                    response = await _dongBoFactory.UpdateNguoiDungs(new List<NguoiDungModel>() { nguoiDung });
                    if (!response.Status)
                    {
                        if (response.Data == 1)
                        {
                            return JsonErrorMessage(string.Format(response.Message));
                        }
                        else
                        {
                            return JsonErrorMessage(string.Format("Đồng bộ sang kho thất bại"));
                        }
                    }
                }

            }
            return JsonSuccessMessage(string.Format("Duyệt và đồng bộ thành công {0} tài khoản", lstTaiKhoanId.Count));
        }

        [HttpPost]
        public virtual IActionResult HuyDuyetTaiKhoan(string strTaiKhoanIds)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDuyetNguoiDung))
                return AccessDeniedView();
            var lstTaiKhoanId = strTaiKhoanIds.Split("-").ToList();
            foreach (var _strID in lstTaiKhoanId)
            {
                NguoiDungModel nguoiDung = new NguoiDungModel();
                var tk = _itemService.GetNguoiDungById(_strID.ToNumberInt32());
                var res = _itemModelFactory.HuyDuyetTaiKhoan(tk.ID);
                nguoiDung.TRANG_THAI_ID = tk.TRANG_THAI_ID;
                if (!res.Result)
                    return JsonErrorMessage(res.Message);
                else _dongBoFactory.DongBoDanhMuc<NguoiDungModel>(new List<NguoiDungModel>() { nguoiDung });
            }

            return JsonSuccessMessage(string.Format("Hủy duyệt thành công {0} tài khoản", lstTaiKhoanId.Count));
        }

        [HttpPost]
        public virtual IActionResult XoaTaiKhoan(string strTaiKhoanIds)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDuyetNguoiDung))
                return AccessDeniedView();
            var lstTaiKhoanId = strTaiKhoanIds.Split("-").ToList();
            foreach (var _strID in lstTaiKhoanId)
            {
                decimal ID = Convert.ToDecimal(_strID);
                var result = _itemService.XoaNguoiDungChuaDuyet(ID);
                if (!result)
                {
                    return JsonErrorMessage(string.Format("Tài khoản đã duyệt hoặc đã được đồng bộ từ kho"));
                }
            }
            return JsonSuccessMessage(string.Format("Xóa thành công {0} tài khoản", lstTaiKhoanId.Count));
        }
        public virtual IActionResult _ThongTinTaiKhoan(decimal Id)
        {
            NguoiDung nguoiDung = null;
            nguoiDung = _itemService.GetNguoiDungById(Id);
            var model = _itemModelFactory.PrepareNguoiDungModelDuToanCap2(new NguoiDungModel(), nguoiDung);
            return PartialView(model);
        }
        #endregion

        #endregion
    }
}

