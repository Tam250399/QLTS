using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS.Core;
using GS.Core.Data;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.HeThong;
using GS.Core.Infrastructure;
using GS.Services.Authentication;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DongBo;
using GS.Web.Factories.SHTD;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.CCDC;
using GS.Web.Models.DongBoTaiSan;
using GS.Web.Models.HeThong;
using GS.Web.Models.ImportTaiSan;
using GS.Web.Models.SHTD;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Spire.Xls;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public class ImportController : BaseWorksController
    {
        private readonly IGSFileProvider _fileProvider;
        private readonly IQuyetDinhTichThuModelFactory _quyetDinhTichThuModelFactory;
        private readonly IDongBoFactory _dongBoFactory;
        private readonly INhanXeService _nhanXeService;
        private readonly IDongXeService _dongXeService;
        private readonly IEncryptionService _encryptionService;
        private readonly IVaiTroService _vaiTroService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IDonViService _donViService;
        public ImportController(
            IGSFileProvider gSFileProvider,
            IQuyetDinhTichThuModelFactory quyetDinhTichThuModelFactory,
            IDongBoFactory dongBoFactory,
            IDongXeService dongXeService,
            INhanXeService nhanXeService,
            IEncryptionService encryptionService,
            IVaiTroService vaiTroService,
            INguoiDungService nguoiDungService,
            IDonViService donViService
            )
        {
            this._fileProvider = gSFileProvider;
            this._quyetDinhTichThuModelFactory = quyetDinhTichThuModelFactory;
            this._dongBoFactory = dongBoFactory;
            this._nhanXeService = nhanXeService;
            this._dongXeService = dongXeService;
            this._encryptionService = encryptionService;
            this._vaiTroService = vaiTroService;
            this._nguoiDungService = nguoiDungService;
            this._donViService = donViService;
        }
        public IActionResult ImportFile()
        {
            DongBoTaiSanModel model = new DongBoTaiSanModel();
            return View(model);
        }
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public virtual Task<IActionResult> ImportExcelCCDC(IFormFile file)
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
            string strSettings = _fileProvider.ReadAllText(_fileProvider.MapPath("~/App_Data/jsonSettings_ImpExcelCCDC.json"), Encoding.UTF8);
            lstSettings = strSettings.toEntities<ExcelModel>();
            int maxCol = lstSettings != null ? lstSettings.Max(c => c.COL) : 0;
            if (maxCol < workSheet1.Dimension.End.Column)
                ErrorNotification("Không đúng định dạng");
            List<DongBoCCDCModel> lstImport = new List<DongBoCCDCModel>();
            for (int rowNumber = 2; rowNumber <= workSheet1.Dimension.End.Row; rowNumber++)
            {
                ExcelRange row = workSheet1.Cells[rowNumber, 1, rowNumber, workSheet1.Dimension.End.Column];
                DongBoCCDCModel item = new DongBoCCDCModel();
                item = row.toEntity<DongBoCCDCModel>(item, rowNumber, lstSettings);
                lstImport.Add(item);
            }
            return null;
        }
        
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public virtual IActionResult ImportExcelTaiSanToanDan(IFormFile file)
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
            string strSettings = _fileProvider.ReadAllText(_fileProvider.MapPath("~/App_Data/jsonSetting_ImpExcelTaiSanToanDan.json"), Encoding.UTF8);
            lstSettings = strSettings.toEntities<ExcelModel>();
            int maxCol = lstSettings != null ? lstSettings.Max(c => c.COL) : 0;
            if (maxCol < workSheet1.Dimension.End.Column)
                ErrorNotification("Không đúng định dạng");
            List<ImportTaiSanToanDanModel> lstImport = new List<ImportTaiSanToanDanModel>();
            for (int rowNumber = 4; rowNumber <= workSheet1.Dimension.End.Row; rowNumber++)
            {
                ExcelRange row = workSheet1.Cells[rowNumber, 1, rowNumber, workSheet1.Dimension.End.Column];
                ImportTaiSanToanDanModel item = new ImportTaiSanToanDanModel();
                item = row.toEntity<ImportTaiSanToanDanModel>(item, rowNumber, lstSettings);
                lstImport.Add(item);
            }
            var Listsuccess = _quyetDinhTichThuModelFactory.ImportExcel(lstImport);
            return Json(Listsuccess);
        }
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public virtual IActionResult ImportExcelTaiSan(IFormFile file)
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
            var fileInfo = new FileInfo(_fileStore);
            var package = new ExcelPackage(fileInfo);
            ExcelWorksheets workSheets = package.Workbook.Worksheets;
            List<MessageReturn> ListResult = new List<MessageReturn>();
            if (workSheets.Count < 1)
            {
                //ErrorMessageJson("File không chuẩn dữ liệu");
                return null;
            }
            ExcelWorksheet DatNha = workSheets[0];
            List<ExcelModel> lstSettings = new List<ExcelModel>();
            string strSettings = _fileProvider.ReadAllText(_fileProvider.MapPath("~/App_Data/jsonSetting_ImportExcel_TaiSanDatNha.json"), Encoding.UTF8);
            lstSettings = strSettings.toEntities<ExcelModel>();
            int maxCol = lstSettings != null ? lstSettings.Max(c => c.COL) : 0;
            if (maxCol < DatNha.Dimension.End.Column)
                ErrorNotification("Thông tin tài sản đất-nhà Không đúng định dạng");
            List<ImportExcelTaiSanDatNhaModel> lstImport = new List<ImportExcelTaiSanDatNhaModel>();
            for (int rowNumber = 2; rowNumber <= DatNha.Dimension.End.Row; rowNumber++)
            {
                ExcelRange row = DatNha.Cells[rowNumber, 1, rowNumber, DatNha.Dimension.End.Column];
                ImportExcelTaiSanDatNhaModel item = new ImportExcelTaiSanDatNhaModel();
                item = row.toEntity<ImportExcelTaiSanDatNhaModel>(item, rowNumber, lstSettings);
                if (string.IsNullOrEmpty(item.TEN))
                {
                    continue;
                }
                lstImport.Add(item);
            }
            List<ImportExcelTaiSanKhacModel> lstImportKhac = new List<ImportExcelTaiSanKhacModel>();
            ExcelWorksheet TaiSanKhac = workSheets[1];
            lstSettings = new List<ExcelModel>();
            strSettings = _fileProvider.ReadAllText(_fileProvider.MapPath("~/App_Data/jsonSetting_ImportExcel_TaiSanKhac.json"), Encoding.UTF8);
            lstSettings = strSettings.toEntities<ExcelModel>();
            maxCol = lstSettings != null ? lstSettings.Max(c => c.COL) : 0;
            if (maxCol < TaiSanKhac.Dimension.End.Column)
                ErrorNotification("Thông tin tài sản khác Không đúng định dạng");
            for (int rowNumber = 2; rowNumber <= TaiSanKhac.Dimension.End.Row; rowNumber++)
            {
                ExcelRange row = TaiSanKhac.Cells[rowNumber, 1, rowNumber, TaiSanKhac.Dimension.End.Column];
                ImportExcelTaiSanKhacModel item = new ImportExcelTaiSanKhacModel();
                item = row.toEntity<ImportExcelTaiSanKhacModel>(item, rowNumber, lstSettings);
                if (string.IsNullOrEmpty(item.TEN))
                {
                    continue;
                }
                lstImportKhac.Add(item);
            }
            var ListError = _dongBoFactory.ImportFileExcel(lstImport);
            if (ListError.Count > 0)
            {
                return Json(new
                {
                    success = false,
                    ListError = ListError,
                    SuccessCount = lstImport.Count - ListError.Count
                });
            }
            return Json(new
            {
                success = true,
                ListSuccess = lstImport,
                SuccessCount = lstImport.Count
            });
        }



    }

}