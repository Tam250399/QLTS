//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 5/6/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Mvc;
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.KT;

using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.KeToan;
using GS.Web.Controllers;
using GS.Web.Factories.KeToan;
using GS.Services.KT;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Services.DanhMuc;
using GS.Services.BienDongs;
using GS.Services.TaiSans;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Spire.Xls;
using GS.Core.Data;
using GS.Core.Infrastructure;
using System.IO;
using OfficeOpenXml;
using GS.Core.Domain.DB;
using System.Text;
using GS.Web.Models.ImportTaiSan;
using System.Linq;
using GS.Core.Domain.TaiSans;
using GS.Web.Factories.BaoCaos;

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class KhauHaoTaiSanController : BaseWorksController
	{    
         #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService; 
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IKhauHaoTaiSanModelFactory _itemModelFactory;
        private readonly IKhauHaoTaiSanService _itemService;
        private readonly IDonViService _donViService;
        private readonly IBienDongService _bienDongService;
        private readonly ITaiSanService _taiSanService;
        private readonly IGSFileProvider _fileProvider;
        private readonly IReportModelFactory _reportModelFactory;
        #endregion

        #region Ctor
        public KhauHaoTaiSanController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IKhauHaoTaiSanModelFactory itemModelFactory,
            IKhauHaoTaiSanService itemService,
            IDonViService donViService,
            IBienDongService bienDongService,
            ITaiSanService taiSanService,
            IReportModelFactory reportModelFactory,
            IGSFileProvider gSFileProvider
            )
        {            
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;            
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._donViService = donViService;
            this._bienDongService = bienDongService;
            this._taiSanService = taiSanService;
            this._fileProvider = gSFileProvider;
            this._reportModelFactory = reportModelFactory;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDieuChinhKhauHao))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new KhauHaoTaiSanSearchModel ();
            var model = _itemModelFactory.PrepareKhauHaoTaiSanSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(KhauHaoTaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDieuChinhKhauHao))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareKhauHaoTaiSanListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDieuChinhKhauHao))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareKhauHaoTaiSanModel(new KhauHaoTaiSanModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(KhauHaoTaiSanModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDieuChinhKhauHao))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<KhauHaoTaiSan>();                
                _itemService.InsertKhauHaoTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<KhauHaoTaiSanModel>(), "KhauHaoTaiSan");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareKhauHaoTaiSanModel(model, null);            
            return View(model);
        } 
        
        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDieuChinhKhauHao))
                return AccessDeniedView();
                
            var item = _itemService.GetKhauHaoTaiSanById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareKhauHaoTaiSanModel(null, item);
            return PartialView(model);
        }

        /*[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]*/
        [HttpPost]
        public virtual IActionResult Edit(KhauHaoTaiSanModel model)//, bool continueEditing
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDieuChinhKhauHao))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKhauHaoTaiSanById(model.ID);
            model.TAI_SAN_ID = item.TAI_SAN_ID;
            model.MA_TAI_SAN = item.MA_TAI_SAN;
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareKhauHaoTaiSan(model,item);
                _itemService.UpdateKhauHaoTaiSan(item); 
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<KhauHaoTaiSanModel>(), "KhauHaoTaiSan");

                var list_KHTS = _itemService.GetListKHTSSau(item.TAI_SAN_ID, item.THANG_KHAU_HAO, item.NAM_KHAU_HAO);

                if (list_KHTS != null && list_KHTS.Count > 0)
                {
                    foreach (var khts in list_KHTS)
                    {
                        //tính khấu hao lũy kế mới
                        var khLuyKeTruoc = _itemService.GetLKKHTruocDo(khts);
                        khts.TONG_KHAU_HAO_LUY_KE = khLuyKeTruoc + item.GIA_TRI_KHAU_HAO;

                        //tính giá trị còn lại mới
                        khts.TONG_GIA_TRI_CON_LAI = khts.TONG_NGUYEN_GIA - khts.TONG_KHAU_HAO_LUY_KE;
                        khts.GIA_TRI_KHAU_HAO = item.GIA_TRI_KHAU_HAO;
                        khts.TY_LE_KHAU_HAO = item.TY_LE_KHAU_HAO;

                        _itemService.UpdateKhauHaoTaiSan(khts);
                    }
                }
                /*SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");*/
                return JsonSuccessMessage("Thành công");
            }
            //prepare model
            //model = _itemModelFactory.PrepareKhauHaoTaiSanModel(model, item, true);
            return JsonErrorMessage("Fail");
        }

        public virtual IActionResult ExportKhauHao(String KeySearch, decimal ThangKhauHaoSearch, decimal NamKhauHaoSearch, decimal LoaiHinhTaiSanSearch)
        {
            KhauHaoTaiSanSearchModel searchModel = new KhauHaoTaiSanSearchModel()
            {
                KeySearch = KeySearch,
                LoaiHinhTaiSan = LoaiHinhTaiSanSearch,
                ThangKhauHao = ThangKhauHaoSearch,
                NamKhauHao = NamKhauHaoSearch
            };

            var nguoiDung = _workContext.CurrentCustomer;
            //searchModel.nguoiDungId = nguoiDung.ID;
            //searchModel.IsQuangTri = nguoiDung.IS_QUAN_TRI;
            //searchModel.isSelectList = true;
            //searchModel.isIncludeAll = true;
            //searchModel.TreeLevel = 2;
            //searchModel.xuatExcel = true;
            var model = _itemModelFactory.PrepareKhauHaoTaiSanExport(searchModel);
            int total = model != null ? model.Count() : 0;
            bool addSTT = false;
            string fName = $"KhauHao_{total}({DateTime.Now.ToString("dd-MM-yyyy hh24-mm-ss")})";
            MemoryStream stream = new MemoryStream();
            stream = _reportModelFactory.prepareExcelEntity<KhauHaoExport>(stream, model, "DonVi", addSTT);
            var bytes = stream.ToArray();
            return new FileContentResult(bytes, MimeTypes.TextXlsx)
            {
                FileDownloadName = fName + ".xlsx"
            };
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDieuChinhKhauHao))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKhauHaoTaiSanById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteKhauHaoTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<KhauHaoTaiSanModel>(), "KhauHaoTaiSan");
                //activity log  
                SuccessNotification("Xoá dữ liệu thành công");
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = item.ID });
            }
        }
        #endregion

        #region Chốt khấu hao tài sản của đơn vị
        public virtual IActionResult ChotKhauHaoTaiSanDonVi()
        {
            var model = new KhauHaoTaiSanModel();
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult ChotKhauHaoTaiSanDonVi(string maDonVi)
        {
            var donVi = _donViService.GetDonViByMa(maDonVi);
            if (donVi != null)
            {
                var result = _itemService.ChotToanBoKhauHaoDonVi(donVi.ID);
                if (result)
                {
                    return Json(MessageReturn.CreateSuccessMessage("Chốt khấu hao thành công", true));
                }
            }
            return Json(MessageReturn.CreateErrorMessage("Chốt khấu hao không thành công", false));
        }

        #endregion
        #region Chốt khấu hao 1 tài sản
        public virtual IActionResult ChotKhauHaoOneTaiSan()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDieuChinhKhauHao))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareKhauHaoTaiSanModel(new KhauHaoTaiSanModel(), null);
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult ChotKhauHaoOneTaiSan(string maTaiSan, decimal namBatDau, decimal thangBatDau, decimal namKetThuc, decimal thangKetThuc)
        {
            
            if(maTaiSan == null || namBatDau == 0 || thangBatDau == 0 || namKetThuc == 0 || thangKetThuc == 0)
            {
                return Json(MessageReturn.CreateErrorMessage("Dữ liệu không đc để trống", false));
            }
            if (thangBatDau <= 0 || thangBatDau > 12 || thangKetThuc <= 0 || thangKetThuc > 12)
            {
                return Json(MessageReturn.CreateErrorMessage("Tháng không được nhỏ hơn 1 và lớn hơn 12", false));
            }
            if (namBatDau > namKetThuc)
            {
                return Json(MessageReturn.CreateErrorMessage("Năm đầu không được lớn hơn năm kết thúc", false));
            }
            if (namBatDau < 0 && namKetThuc < 0)
            {
                return Json(MessageReturn.CreateErrorMessage("Năm không được nhận giá trị âm", false));
            }
            var taiSan = _taiSanService.GetTaiSanByMa(maTaiSan);  
            if (taiSan != null)
            {
                DateTime nGAY_SU_DUNG = Convert.ToDateTime(taiSan.NGAY_SU_DUNG);
                string strNgaySuDung = nGAY_SU_DUNG.ToString("dd/MM/yyyy");
                string strmonth = strNgaySuDung.Substring(3, 2);
                string stryear = strNgaySuDung.Substring(6, 4);
                decimal month = decimal.Parse(strmonth);
                decimal year = decimal.Parse(stryear);
                if ((namBatDau < year) || (namBatDau == year && thangBatDau < month))
                {
                    return Json(MessageReturn.CreateErrorMessage("Giá trị bắt đầu tính lớn hơn ngày sử dụng", false));
                }
                var result = _itemService.ChotKhauHaoOneTs(taiSan.ID, namBatDau, thangBatDau, namKetThuc, thangKetThuc);
                if (result)
                {
                    var list_KHTS = _itemService.GetListKHTSSau(taiSan.ID, thangKetThuc, namKetThuc);

                    if (list_KHTS != null && list_KHTS.Count > 0)
                    {
                        foreach (var khts in list_KHTS)
                        {
                            //tính hao mòn lũy kế mới
                            var khLuyKeTruoc = _itemService.GetLKKHTruocDo(khts);
                            khts.TONG_KHAU_HAO_LUY_KE = khLuyKeTruoc + khts.GIA_TRI_KHAU_HAO;

                            //tính giá trị còn lại mới
                            khts.TONG_GIA_TRI_CON_LAI = khts.TONG_NGUYEN_GIA - khts.TONG_KHAU_HAO_LUY_KE;
                            khts.GIA_TRI_KHAU_HAO = khts.GIA_TRI_KHAU_HAO;
                            khts.TY_LE_KHAU_HAO = khts.TY_LE_KHAU_HAO;

                            _itemService.UpdateKhauHaoTaiSan(khts);
                        }
                    }
                    return Json(MessageReturn.CreateSuccessMessage("Chốt khấu hao thành công", true));
                }
            }
            return Json(MessageReturn.CreateErrorMessage("Chốt khấu hao không thành công", false));
        }
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public virtual IActionResult ImportKhauHaoTsExcel(IFormFile file)
        {
            if (file == null)
            {
                ErrorNotification("Bạn chưa Nhập file Import");
                // return RedirectToAction("DongBoTaiSan");
            }
            List<int> listRowIdentical = new List<int>();
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
            ExcelWorksheet taiSan = workSheets[0];
            List<ExcelModel> lstSettings = new List<ExcelModel>();
            string strSettings = _fileProvider.ReadAllText(_fileProvider.MapPath("~/App_Data/jsonSetting_ImportExcel_KhTaiSan.json"), Encoding.UTF8);
            lstSettings = strSettings.toEntities<ExcelModel>();
            int maxCol = lstSettings != null ? lstSettings.Max(c => c.COL) : 0;
            if (maxCol < taiSan.Dimension.End.Column || taiSan.Dimension.End.Column < maxCol)
                ErrorNotification("Thông tin tài sản không đúng định dạng");
            List<ImportKhTaiSanModel> lstImport = new List<ImportKhTaiSanModel>();
            List<string> ListErr = new List<string>();
            List<string> ListExists = new List<string>();
            List<string> unionList = new List<string>();
            for (int rowNumber = 3; rowNumber <= taiSan.Dimension.End.Row; rowNumber++)
            {
                ExcelRange row = taiSan.Cells[rowNumber, 1, rowNumber, taiSan.Dimension.End.Column];
                ImportKhTaiSanModel item = new ImportKhTaiSanModel();
                item = row.toEntity<ImportKhTaiSanModel>(item, rowNumber, lstSettings);
                var rows = rowNumber;
                item.Row = rows;
                var rs = ValidateListImportBdGiamTaiSan(item);
                if (rs.Count > 0)
                {
                    unionList = unionList.Union(rs).ToList();
                    continue;
                }
                else
                {
                    lstImport.Add(item);
                }
            }
            if (unionList.Count > 0)
            {
                var Error = string.Join("; ", unionList);
                return JsonErrorMessage("Thông tin tài sản trong excel chưa đúng!", Error);
            }
            int soTsImport = 0;
            foreach (var item in lstImport)
            {
                try
                {
                    ImportKhTaiSanModel khts = item;
                    TaiSan taiSanDB = _taiSanService.GetTaiSanByMaDBPmCu(item.TAI_SAN_MA_DB, item.DON_VI_MA);
                    var khauHaoTaiSan = _itemModelFactory.InsertToKhauHaoTaiSan(khts, taiSanDB);
                    soTsImport++; 
                }
                catch (Exception ex)
                {
                    ListErr.Add(item.TAI_SAN_MA_DB);
                }
            }
            if (soTsImport > 0)
            {
                 
                if (ListErr.Count == 0 && unionList.Count == 0)
                {
                    return JsonSuccessMessage("Import thành công");
                }
                string Error = "";
                if (unionList.Count > 0)
                {
                    Error = String.Concat(Error, $"{string.Join("; ", unionList)}; ");
                }
                if (ListErr.Count > 0)
                {
                    Error = String.Concat(Error, $"{ListErr.Count} tài sản import lỗi: {string.Join("; ", ListErr)}");
                }
                return JsonSuccessMessage("Import tài sản thành công " + soTsImport + " tài sản!", Error);
            }
            else
            {
                string Error = "";
                if (unionList.Count > 0)
                {
                    Error = String.Concat(Error, $"{string.Join("; ", unionList)}; ");
                }
                if (ListErr.Count > 0)
                {
                    Error = String.Concat(Error, $"{ListErr.Count} tài sản import lỗi: {string.Join("; ", ListErr)}");
                }
                return JsonErrorMessage("Import tài sản thất bại", Error);
            }
        }

        private List<string> ValidateListImportBdGiamTaiSan(ImportKhTaiSanModel model)
        {
            List<string> ListError = new List<string>();
            string Error = $"Hàng {model.Row}:";
            int countError = 0;
            if (countError > 0)
            {
                ListError.Add(Error);
            }
            return ListError;
        }
        #endregion
    }
}

