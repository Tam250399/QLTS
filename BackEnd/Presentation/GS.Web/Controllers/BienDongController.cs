//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Data;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Core.Infrastructure;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.KT;
using GS.Services.NghiepVu;
using GS.Services.Security;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.BienDongs;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.HeThong;
using GS.Web.Factories.KeToan;
using GS.Web.Factories.NghiepVu;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Extensions;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.BienDongs;
using GS.Web.Models.ImportTaiSan;
using GS.Web.Models.NghiepVu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GS.Web.Controllers
{
	[HttpsRequirement(SslRequirement.No)]
	public partial class BienDongController : BaseWorksController
	{
		#region Fields

		private readonly IHoatDongService _hoatdongService;
		private readonly INhanHienThiService _nhanHienThiService;
		private readonly IQuyenService _quyenService;
		private readonly IWorkContext _workContext;
		private readonly CauHinhChung _cauhinhChung;
		private readonly IBienDongModelFactory _itemModelFactory;
		private readonly IBienDongService _itemService;
		private readonly IYeuCauService _yeuCauService;
		private readonly IBienDongChiTietService _bienDongChiTietService;
		private readonly IYeuCauChiTietService _yeuCauChiTietService;
		private readonly IYeuCauNhatKyService _yeuCauNhatKyService;
		private readonly ITaiSanService _taiSanService;
		private readonly IYeuCauNhatKyModelFactory _yeuCauNhatKyModelFactory;
		private readonly ITaiSanNguonVonService _taiSanNguonVonService;
		private readonly IYeuCauModelFactory _yeuCauModelFactory;
		private readonly IBienDongChiTietModelFactory _bienDongChiTietModelFactory;
		private readonly ITaiSanNguonVonModelFactory _taiSanNguonVonModelFactory;
		private readonly ITaiSanModelFactory _taiSanModelFactory;
		private readonly ITrungGianBDYCService _trungGianBDYCService;
		private readonly IYeuCauChiTietModelFactory _yeuCauChiTietModelFactory;
		private readonly IThaoTacBienDongModelFactory _thaoTacBienDongModelFactory;
		private readonly IDonViService _donViService;
        private readonly ICauHinhModelFactory _cauHinhModelFactory;
        private readonly IDonViModelFactory _donViModelFactory;
        private readonly IHienTrangModelFactory _hienTrangModelFactory;
		private readonly IGSFileProvider _fileProvider;
		private readonly ITaiSanHienTrangSuDungModelFactory _taiSanHienTrangSuDungModelFactory;

		#endregion Fields

		#region Ctor

		public BienDongController(
			IHoatDongService hoatdongService,
			INhanHienThiService nhanHienThiService,
			IQuyenService quyenService,
			IWorkContext workContext,
			CauHinhChung cauhinhChung,
			IBienDongModelFactory itemModelFactory,
			IBienDongService itemService,
			IYeuCauService yeuCauService,
			IBienDongChiTietService bienDongChiTietService,
			IYeuCauChiTietService yeuCauChiTietService,
			IYeuCauNhatKyService yeuCauNhatKyService,
			ITaiSanService taiSanService,
			IYeuCauNhatKyModelFactory yeuCauNhatKyModelFactory,
			ITaiSanNguonVonService taiSanNguonVonService,
			IYeuCauModelFactory yeuCauModelFactory,
			IBienDongChiTietModelFactory bienDongChiTietModelFactory,
			ITaiSanNguonVonModelFactory taiSanNguonVonModelFactory,
			ITaiSanModelFactory taiSanModelFactory,
			IYeuCauChiTietModelFactory yeuCauChiTietModelFactory,
			ITrungGianBDYCService trungGianBDYCService,
			IThaoTacBienDongModelFactory thaoTacBienDongModelFactory,
            IDonViService donViService,
            ICauHinhModelFactory cauHinhModelFactory,
            IDonViModelFactory donViModelFactory,
			IHienTrangModelFactory hienTrangModelFactory,
            IGSFileProvider gSFileProvider, 
			ITaiSanHienTrangSuDungModelFactory taiSanHienTrangSuDungModelFactory
            )
		{
			this._hoatdongService = hoatdongService;
			this._nhanHienThiService = nhanHienThiService;
			this._quyenService = quyenService;
			this._workContext = workContext;
			this._cauhinhChung = cauhinhChung;
			this._itemModelFactory = itemModelFactory;
			this._itemService = itemService;
			this._yeuCauService = yeuCauService;
			this._bienDongChiTietService = bienDongChiTietService;
			this._yeuCauChiTietService = yeuCauChiTietService;
			this._yeuCauNhatKyService = yeuCauNhatKyService;
			this._taiSanService = taiSanService;
			this._yeuCauNhatKyModelFactory = yeuCauNhatKyModelFactory;
			this._taiSanNguonVonService = taiSanNguonVonService;
			this._yeuCauModelFactory = yeuCauModelFactory;
			this._bienDongChiTietModelFactory = bienDongChiTietModelFactory;
			this._taiSanNguonVonModelFactory = taiSanNguonVonModelFactory;
			this._taiSanModelFactory = taiSanModelFactory;
			this._trungGianBDYCService = trungGianBDYCService;
			this._yeuCauChiTietModelFactory = yeuCauChiTietModelFactory;
			this._thaoTacBienDongModelFactory = thaoTacBienDongModelFactory;
            this._donViService = donViService;
            this._cauHinhModelFactory = cauHinhModelFactory;
            this._donViModelFactory = donViModelFactory;
            this._hienTrangModelFactory = hienTrangModelFactory;
			this._fileProvider = gSFileProvider;
			this._taiSanHienTrangSuDungModelFactory = taiSanHienTrangSuDungModelFactory;
		}

		#endregion Ctor

		#region Methods

		//hủy duyệt yc biến động gần nhất
		public virtual IActionResult _KhongDuyetTaiSan(decimal TaiSanId, string Note)
		{
			var trungGian = _trungGianBDYCService.GetYCBDGanNhat(TaiSanId);
			if (trungGian != null)
				if (trungGian.EnumBDorYC == enumBDorYC.BIEN_DONG)
				{
					var res = _thaoTacBienDongModelFactory.HuyDuyetBienDongFunc(trungGian.ID, Note);
					if (res.Result)
						return JsonSuccessMessage(res.Message);
					else
						return JsonErrorMessage(res.Message);
				}	
					
				else if (trungGian.EnumBDorYC == enumBDorYC.YEU_CAU)
				{
					var res = _thaoTacBienDongModelFactory.KhongDuyetYeuCauFunc(trungGian.ID, Note);
					if (res.Result)
						return JsonSuccessMessage(res.Message);
					else
						return JsonErrorMessage(res.Message);
				}	
					
			return JsonErrorMessage("Từ chối thất bại");
		}

		/// <summary>
		/// hàm Từ chối yêu cầu (khác với bỏ duyệt biến động)
		/// </summary>
		/// <param name="YeuCauId"></param>
		/// <param name="Note"></param>
		/// <returns></returns>
		[HttpPost]
		public virtual IActionResult _KhongDuyetYeuCau(decimal YeuCauId, string Note)
		{
			var res = _thaoTacBienDongModelFactory.KhongDuyetYeuCauFunc(YeuCauId, Note);
			if (res.Result)
				return JsonSuccessMessage(res.Message);
			return JsonErrorMessage(res.Message);
		}

		/// <summary>
		/// hủy bỏ biến động (khác với Từ chối yêu cầu)
		/// </summary>
		/// <param name="BienDongId"></param>
		/// <param name="Note"></param>
		/// <returns></returns>
		[HttpPost]
		public virtual IActionResult _HuyDuyetBienDong(decimal BienDongId, string Note)
		{
			var res = _thaoTacBienDongModelFactory.HuyDuyetBienDongFunc(BienDongId, Note);
			if (res.Result)
				return JsonSuccessMessage(res.Message);
			return JsonErrorMessage(res.Message);
		}

		[HttpPost]
		public virtual IActionResult _HuyDuyetBienDongs(decimal[] BienDongIds, string Note)
		{
			var res = new ResultAction(true, "Bỏ duyệt thành công");
			var suc = 0; int err = 0;
			if(BienDongIds.Length > 0)
            {				
				foreach(var BienDongId in BienDongIds)
				{
					res = _thaoTacBienDongModelFactory.HuyDuyetBienDongFunc(BienDongId, Note);
					if (res.Result)
					{
						suc++;
                    }
                    else
                    {
						err++;
                    }
				}
            }
			if (suc > 0 && err == 0)
            {
                return JsonSuccessMessage(res.Message);
            }
			else
            {
				if (suc > 0)
                {
					return JsonSuccessMessage(new ResultAction(true, $"Bỏ duyệt thành công {suc} tài sản và Bỏ duyệt thất bại {err} tài sản").Message);
				}
				return JsonErrorMessage(res.Message);
			}
        }

        [HttpPost]
		public virtual IActionResult DuyetTaiSan(string strTaiSanIds)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDuyetDangKy))
				return AccessDeniedView();
			var lstTaiSanId = strTaiSanIds.Split("-").ToList();
			foreach (var _strID in lstTaiSanId)
			{
				//sắp xếp theo ngày biến động tăng dần để duyệt từ từ
				var yeucaus = _yeuCauService.GetYeuCaus(TaiSanId: Convert.ToDecimal(_strID), TrangThaiId: (decimal)enumTRANG_THAI_YEU_CAU.CHO_DUYET).OrderBy(p => p.NGAY_BIEN_DONG).ToList();
				foreach (var yc in yeucaus)
				{
					var res = _thaoTacBienDongModelFactory.DuyetYeuCauFunc(yc.ID, yc);
					if (!res.Result)
						return JsonErrorMessage(res.Message);
				}	
					
			}
			return JsonSuccessMessage(string.Format("Duyệt thành công {0} tài sản", lstTaiSanId.Count));
		}

		[HttpPost]
		public virtual IActionResult DuyetYeuCau(decimal YeuCauId)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDuyetDangKy))
				return AccessDeniedView();
			var res = _thaoTacBienDongModelFactory.DuyetYeuCauFunc(YeuCauId);
			if (res.Result)
				return JsonSuccessMessage(res.Message);
			return JsonErrorMessage(res.Message);
		}

		public virtual IActionResult khongduyet(int TaiSanId)
		{
			var BDYC = _trungGianBDYCService.GetAllYCBDKhacTuChoi(TaiSanId).OrderByDescending(p=>p.BDorYC).ThenByDescending(p=>p.NGAY_BIEN_DONG).Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.BDorYC.ToString() }).ToList();
			return JsonSuccessMessage("True", BDYC);
		}

		[HttpPost]
		public virtual IActionResult khongduyets(int[] TaiSanIds)
		{
			List<SelectListItem> BDYCs = new List<SelectListItem>();
			
			foreach(var TaiSanId in TaiSanIds)
            {
				var BDYC = _trungGianBDYCService.GetAllYCBDKhacTuChoi(TaiSanId).OrderByDescending(p => p.BDorYC).ThenByDescending(p => p.NGAY_BIEN_DONG).Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.BDorYC.ToString() }).ToList();
				BDYCs = BDYCs.Union(BDYC).ToList();
			}
			return JsonSuccessMessage("True", BDYCs);
		}

		public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new BienDongSearchModel();
            var model = _itemModelFactory.PrepareBienDongSearchModel(searchmodel);
            return View(model);
        }
	
		[HttpPost]
        public virtual IActionResult List(BienDongSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareBienDongListModel(searchModel);
            return Json(model);
        }

		public virtual IActionResult _ListBienDongSaiHienTrang(Guid? TaiSanGuid, decimal TaiSanId)
		{
            if (TaiSanGuid == null)
            {
				var taiSan = _taiSanService.GetTaiSanById(TaiSanId);
                if (taiSan != null)
                {
					TaiSanGuid = taiSan.GUID;
				}
				
            }
			//prepare model
			var searchmodel = new BienDongSearchModel();
			searchmodel.TaiSanGuid = TaiSanGuid??Guid.NewGuid();
			return PartialView(searchmodel);
		}
		[HttpPost]
		public virtual IActionResult _ListBienDongSaiHienTrang(BienDongSearchModel searchModel)
		{
			//prepare model
			var model = _itemModelFactory.PrepareForListBienDongSaiHienTrang(searchModel);
			return Json(model);
		}
		public virtual IActionResult _CapNhatBienDongSaiHienTrang(decimal BienDongID)
		{
			//prepare model
			var model = _itemModelFactory.PrepareForBienDongSaiHienTrangModel(BienDongID);
			return PartialView(model);
		}

		[HttpPost]
		public virtual IActionResult _CapNhatBienDongSaiHienTrang(YeuCauChiTietModel model)
		{
			ValidateListHienTrang(model);
			if (ModelState.IsValid)
			{
				var result = _thaoTacBienDongModelFactory.CapNhatBienDongSaiHienTrang(model);
                if (!result.Result)
                {
					return JsonErrorMessage(result.Message);
					
				}
				return JsonSuccessMessage(result.Message);
			}
			var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			return JsonErrorMessage("Error", list);
		}
		private void ValidateListHienTrang(YeuCauChiTietModel model)
		{
			if (model.lstHienTrang != null && model.lstHienTrang.Count() > 0)
			{
				decimal TongHienTrangNumber = 0;
				decimal TongHienTrangCheckbox = 0;
				foreach (var hienTrang in model.lstHienTrang)
				{
					if (hienTrang != null)
					{
						// nếu mà hiện trạng không đúng với loại hình đơn vị nhưng có giá trị thì báo validate
						if (_hienTrangModelFactory.CheckIsHienTrangNhapSai(model.DonViSaiHienTrangId ?? 0, hienTrang))
						{
							ModelState.AddModelError($"HienTrang_{hienTrang.HienTrangId}", "Hiện trạng không đúng với loại hình đơn vị");
						}
						TongHienTrangNumber = TongHienTrangNumber + (hienTrang.GiaTriNumber ?? 0);
						TongHienTrangCheckbox = TongHienTrangCheckbox + ((hienTrang.GiaTriCheckbox??false)?1:0);
					}
				}
				switch (model.LoaiHinhTaiSanSaiHienTrangId)
				{
					case (int)enumLOAI_HINH_TAI_SAN.DAT:
						if (TongHienTrangNumber != model.DAT_TONG_DIEN_TICH_CU)
						{
							ModelState.AddModelError($"DAT_TONG_DIEN_TICH_CU", "Không được thay đổi tổng diện tích đất");
						}
						break;
					case (int)enumLOAI_HINH_TAI_SAN.NHA:
						if (TongHienTrangNumber != model.NHA_TONG_DIEN_TICH_XD_CU)
						{
							ModelState.AddModelError($"NHA_TONG_DIEN_TICH_XD_CU", "Không được thay đổi tổng diện tích xây dựng");
						}
						break;
					default:
						break;
				}
			}
           
        }
		#endregion Methods

		#region importBdGiamTS

		[HttpPost]
		[RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
		[RequestSizeLimit(209715200)]
		public virtual IActionResult ImportGiamTaiSanExcel(IFormFile file)
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
			string strSettings = _fileProvider.ReadAllText(_fileProvider.MapPath("~/App_Data/jsonSetting_ImportExcel_BdGiamTaiSan.json"), Encoding.UTF8);
			lstSettings = strSettings.toEntities<ExcelModel>();
			int maxCol = lstSettings != null ? lstSettings.Max(c => c.COL) : 0;
			if (maxCol < taiSan.Dimension.End.Column || taiSan.Dimension.End.Column < maxCol)
				ErrorNotification("Thông tin tài sản không đúng định dạng");
			List<ImportBdGiamTaiSanModel> lstImport = new List<ImportBdGiamTaiSanModel>();
			List<string> ListErr = new List<string>();
			List<string> ListExists = new List<string>();
			List<string> unionList = new List<string>();
			for (int rowNumber = 3; rowNumber <= taiSan.Dimension.End.Row; rowNumber++)
			{
				ExcelRange row = taiSan.Cells[rowNumber, 1, rowNumber, taiSan.Dimension.End.Column];
				ImportBdGiamTaiSanModel item = new ImportBdGiamTaiSanModel();
				item = row.toEntity<ImportBdGiamTaiSanModel>(item, rowNumber, lstSettings);
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
					ImportBdGiamTaiSanModel gtts = item;
					BienDongModel bd = new BienDongModel();
					BienDongChiTietModel bdct = new BienDongChiTietModel();
					YeuCau yeuCau = new YeuCau();
					//gtts = item.ToEntity<ImportExcelTaiSan>();
					item.LOAI_TAI_SAN_MA = item.LOAI_TAI_SAN_MA.Split(new string[] { "-" }, StringSplitOptions.None)[0];
					item.LOAI_BIEN_DONG_ID = item.LOAI_BIEN_DONG_ID.Split(new string[] { "-" }, StringSplitOptions.None)[0];
					item.LY_DO_GIAM_ID = item.LY_DO_GIAM_ID.Split(new string[] { "-" }, StringSplitOptions.None)[0];
					var rs = _itemModelFactory.ValidateListImportBdGiamTaiSan(item);
					if (rs.Count > 0)
					{
						unionList = unionList.Union(rs).ToList();
						continue;
					}
					if (item.HINH_THUC_XU_LY_ID != null)
					{
						item.HINH_THUC_XU_LY_ID = item.HINH_THUC_XU_LY_ID.Split(new string[] { "-" }, StringSplitOptions.None)[0];
					}
					var tsgiam = _taiSanService.GetTaiSanByMaDBPmCu(item.TAI_SAN_MA_DB, item.DON_VI_MA);
					item.TAI_SAN_ID = tsgiam.ID;
					item.TAI_SAN_MA = tsgiam.MA;
					item.NGAY_SU_DUNG = tsgiam.NGAY_SU_DUNG;
					_itemModelFactory.InsertToBienDongGiamTS(item, bd);
					var biendong = _itemService.GetBienDongCuoiByTaiSanId(tsgiam.ID);
					if(item.DON_VI_NHAN_DIEU_CHUYEN_MA != null)
                    {
						item.DON_VI_NHAN_DIEU_CHUYEN_ID = _donViService.GetDonViByMa(item.DON_VI_NHAN_DIEU_CHUYEN_MA).ID;
					}						
					var biendongchitiet = _itemModelFactory.InsertToBienDongChiTiet(item, bdct, biendong.ToModel<BienDongModel>());

					tsgiam.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO;
					_taiSanService.UpdateTaiSan(tsgiam);
					biendong.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.DA_DUYET;
					biendong.NGAY_DUYET = DateTime.Now;
					_itemService.UpdateBienDong(biendong);
					soTsImport++;
				}
				catch (Exception ex)
				{
					ListErr.Add(item.TEN);
				}
			}
			if (soTsImport > 0)
			{

				if (ListErr.Count == 0 && unionList.Count == 0)
				{
					return JsonSuccessMessage("Import biến động thành công ");/*, "Hàng tài sản bị trùng trong file excel: " + string.Join("; ", listRowIdentical)*/
				}
				string Error = "";
				if (unionList.Count > 0)
				{
					Error = String.Concat(Error, $"{string.Join("; ", unionList)}; ");
				}
				if (ListErr.Count > 0)
				{
					Error = String.Concat(Error, $"{ListErr.Count} biến động import lỗi: {string.Join("; ", ListErr)}");
				}
				return JsonSuccessMessage("Import biến động thành công " + soTsImport + " tài sản!", Error);
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
					Error = String.Concat(Error, $"{ListErr.Count} biến động import lỗi: {string.Join("; ", ListErr)}");
				}
				return JsonErrorMessage("Import biến động thất bại", Error);
			}
		}
		private List<string> ValidateListImportBdGiamTaiSan(ImportBdGiamTaiSanModel model)
		{
			List<string> ListError = new List<string>();
			string Error = $"Hàng {model.Row}:";
			int countError = 0;
			if (string.IsNullOrEmpty(model.DON_VI_MA))
			{
				Error = String.Concat(Error, $" - Mã đơn vị không được để trống");
				countError++;
			}
			if (string.IsNullOrEmpty(model.TAI_SAN_MA_DB))
			{
				Error = String.Concat(Error, $" - Mã tài sản DB không được để trống");
				countError++;
			}
			if (string.IsNullOrEmpty(model.LOAI_TAI_SAN_MA))
			{
				Error = String.Concat(Error, $" - Mã loại không được để trống");
				countError++;
			}
			if (string.IsNullOrEmpty(model.TEN))
			{
				Error = String.Concat(Error, $" - Tên không được để trống");
				countError++;
			}
			/*if (model.NGAY_SU_DUNG == null)
			{
				Error = String.Concat(Error, $" - Ngày sử dụng không được để trống");
				countError++;
			}*/
			if (String.IsNullOrEmpty(model.QUYET_DINH_SO))
			{
				Error = String.Concat(Error, $" - Số quyết định không được để trống");
				countError++;
			}
			if (model.QUYET_DINH_NGAY == null)
			{
				Error = String.Concat(Error, $" - Ngày quyết định không được để trống");
				countError++;
			}
			if (model.NGAY_GIAM == null)
			{
				Error = String.Concat(Error, $" - Ngày Giảm không được để trống");
				countError++;
			}
			if (string.IsNullOrEmpty(model.LOAI_BIEN_DONG_ID))
			{
				Error = String.Concat(Error, $" - Loại biến động giảm không được để trống");
				countError++;
			}
			if (string.IsNullOrEmpty(model.LY_DO_GIAM_ID))
			{
				Error = String.Concat(Error, $" - Lý do giảm không được để trống");
				countError++;
            }
			if (countError > 0)
			{
				ListError.Add(Error);
			}
			return ListError;
		}
		#endregion
		#region importBdGiamTS

		[HttpPost]
		[RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
		[RequestSizeLimit(209715200)]
		public virtual IActionResult ImportBDTangGiamNguyenGiaExcel(IFormFile file)
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
			string strSettings = _fileProvider.ReadAllText(_fileProvider.MapPath("~/App_Data/jsonSetting_ImportExcel_TangGiamNguyenGia.json"), Encoding.UTF8);
			lstSettings = strSettings.toEntities<ExcelModel>();
			int maxCol = lstSettings != null ? lstSettings.Max(c => c.COL) : 0;
			if (maxCol < taiSan.Dimension.End.Column || taiSan.Dimension.End.Column < maxCol)
				ErrorNotification("Thông tin tài sản không đúng định dạng");
			List<ImportBdTangGiamNguyenGiaModel> lstImport = new List<ImportBdTangGiamNguyenGiaModel>();
			List<string> ListErr = new List<string>();
			List<string> ListExists = new List<string>();
			List<string> unionList = new List<string>();
			for (int rowNumber = 4; rowNumber <= taiSan.Dimension.End.Row; rowNumber++)
			{
				ExcelRange row = taiSan.Cells[rowNumber, 1, rowNumber, taiSan.Dimension.End.Column];
				ImportBdTangGiamNguyenGiaModel item = new ImportBdTangGiamNguyenGiaModel();
				item = row.toEntity<ImportBdTangGiamNguyenGiaModel>(item, rowNumber, lstSettings);
				var rows = rowNumber;
				item.Row = rows;
				var rs = ValidateListImportBdTangGiamNguyenGia(item);
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
					ImportBdTangGiamNguyenGiaModel gtts = item;
					BienDongModel bd = new BienDongModel();
					BienDongChiTietModel bdct = new BienDongChiTietModel();
					YeuCau yeuCau = new YeuCau();
					//gtts = item.ToEntity<ImportExcelTaiSan>();
					item.LOAI_TAI_SAN_MA = item.LOAI_TAI_SAN_MA.Split(new string[] { "-" }, StringSplitOptions.None)[0];
					item.LOAI_BIEN_DONG_ID = item.LOAI_BIEN_DONG_ID.Split(new string[] { "-" }, StringSplitOptions.None)[0];
					item.LY_DO_BIEN_DONG_MA = item.LY_DO_BIEN_DONG_MA.Split(new string[] { "-" }, StringSplitOptions.None)[0];
					var rs = _itemModelFactory.ValidateBdTangGiamNguyenGia(item);
					if (rs.Count > 0)
					{
						unionList = unionList.Union(rs).ToList();
						continue;
					}
					var tsTangGiam = _taiSanService.GetTaiSanByMaDBPmCu(item.TAI_SAN_MA_DB, item.DON_VI_MA);
					item.TAI_SAN_ID = tsTangGiam.ID;
					item.TAI_SAN_MA = tsTangGiam.MA;
					item.NGAY_SU_DUNG = tsTangGiam.NGAY_SU_DUNG;
					bdct.HTSD_JSON = _trungGianBDYCService.GetHTSD_JSON_of_TS(tsTangGiam.ID);
					var biendong = _itemModelFactory.InsertToBdTangGiamNguyenGia(item, bd);
					var biendongModel = biendong.ToModel<BienDongModel>();
					var biendongchitiet = _itemModelFactory.InsertToBdTangGiamNguyenGiaChiTiet(item, bdct, biendongModel);
					_itemModelFactory.InsertTaiSanNguonVonFromBienDong(item, biendongModel);
					_taiSanHienTrangSuDungModelFactory.InsertHienTrangSuDungForBienDong(biendong.ID, biendong.TAI_SAN_ID, biendongchitiet.HTSD_JSON);
					biendong.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.DA_DUYET;
					biendong.NGAY_DUYET = DateTime.Now;
					_itemService.UpdateBienDong(biendong);
					soTsImport++;
				}
				catch (Exception ex)
				{
					ListErr.Add(item.TEN);
				}
			}
			if (soTsImport > 0)
			{

				if (ListErr.Count == 0 && unionList.Count == 0)
				{
					return JsonSuccessMessage("Import biến động thành công ");/*, "Hàng tài sản bị trùng trong file excel: " + string.Join("; ", listRowIdentical)*/
				}
				string Error = "";
				if (unionList.Count > 0)
				{
					Error = String.Concat(Error, $"{string.Join("; ", unionList)}; ");
				}
				if (ListErr.Count > 0)
				{
					Error = String.Concat(Error, $"{ListErr.Count} biến động import lỗi: {string.Join("; ", ListErr)}");
				}
				return JsonSuccessMessage("Import biến động thành công " + soTsImport + " tài sản!", Error);
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
					Error = String.Concat(Error, $"{ListErr.Count} biến động import lỗi: {string.Join("; ", ListErr)}");
				}
				return JsonErrorMessage("Import biến động thất bại", Error);
			}
		}
		private List<string> ValidateListImportBdTangGiamNguyenGia(ImportBdTangGiamNguyenGiaModel model)
		{
			List<string> ListError = new List<string>();
			string Error = $"Hàng {model.Row}:"; 
			int countError = 0;
			return ListError;
		}
		#endregion
	}
}