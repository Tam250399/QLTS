//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
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
using GS.Web.Factories.NghiepVu;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Extensions;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.BienDongs;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Controllers
{
	[HttpsRequirement(SslRequirement.No)]
	public partial class YeuCauController : BaseWorksController
	{
		#region Fields

		private readonly IHoatDongService _hoatdongService;
		private readonly INhanHienThiService _nhanHienThiService;
		private readonly IQuyenService _quyenService;
		private readonly IWorkContext _workContext;
		private readonly CauHinhChung _cauhinhChung;
		private readonly IYeuCauModelFactory _itemModelFactory;
		private readonly IYeuCauService _itemService;
		private readonly IYeuCauChiTietService _yeuCauChiTietService;
		private readonly IBienDongService _bienDongService;
		private readonly IBienDongChiTietService _bienDongChiTietService;
		private readonly ITaiSanService _taiSanService;
		private readonly ITaiSanModelFactory _taiSanModelFactory;
		private readonly ITrungGianBDYCService _trungGianBDYCService;
		private readonly IHaoMonTaiSanService _haoMonTaiSanService;
		private readonly IKhauHaoTaiSanService _khauHaoTaiSanService;
		private readonly IDiaBanService _diaBanService;

		//TaiSan
		private readonly ITaiSanNhaService _taiSanNhaService;

		//YeuCau/TrungGian
		private readonly ITrungGianBDYCModelFactory _trungGianBDYCModelFactory;

		private readonly IYeuCauChiTietModelFactory _yeuCauChiTietModelFactory;
		private readonly IYeuCauNhatKyService _yeuCauNhatKyService;
		private readonly IYeuCauNhatKyModelFactory _yeuCauNhatKyModelFactory;
		private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViServices;
		private readonly IThaoTacBienDongModelFactory _thaoTacBienDongModelFactory;
		private readonly IDonViService _donViService;
		private readonly IDonViModelFactory _donViModelFactory;
		private readonly ICauHinhModelFactory _cauHinhModelFactory;
        private readonly IHienTrangService _hienTrangService;
        private readonly IHienTrangModelFactory _hienTrangModelFactory;
        #endregion Fields

        #region Ctor

        public YeuCauController(
			IHoatDongService hoatdongService,
			INhanHienThiService nhanHienThiService,
			IQuyenService quyenService,
			IWorkContext workContext,
			CauHinhChung cauhinhChung,
			IYeuCauModelFactory itemModelFactory,
			IYeuCauService itemService,
			IYeuCauChiTietService yeuCauChiTietService,
			IBienDongService bienDongService,
			IBienDongChiTietService bienDongChiTietService,
			ITaiSanService taiSanService,
			ITrungGianBDYCModelFactory trungGianBDYCModelFactory,
			IYeuCauChiTietModelFactory yeuCauChiTietModelFactory,
			IYeuCauNhatKyService yeuCauNhatKyService,
			IYeuCauNhatKyModelFactory yeuCauNhatKyModelFactory,
			ITaiSanNhaService taiSanNhaService,
			ILoaiTaiSanDonViServices loaiTaiSanVoHinhService,
			ITaiSanModelFactory taiSanModelFactory,
			IThaoTacBienDongModelFactory thaoTacBienDongModelFactory,
			ITrungGianBDYCService trungGianBDYCService,
			IHaoMonTaiSanService haoMonTaiSanService,
			IKhauHaoTaiSanService khauHaoTaiSanService,
			IDiaBanService diaBanService,
			IDonViService donViService,
			IDonViModelFactory donViModelFactory,
			ICauHinhModelFactory cauHinhModelFactory,
			IHienTrangService hienTrangService,
			IHienTrangModelFactory hienTrangModelFactory
			)
		{
			this._hoatdongService = hoatdongService;
			this._nhanHienThiService = nhanHienThiService;
			this._quyenService = quyenService;
			this._workContext = workContext;
			this._cauhinhChung = cauhinhChung;
			this._itemModelFactory = itemModelFactory;
			this._itemService = itemService;
			this._yeuCauChiTietService = yeuCauChiTietService;
			this._bienDongService = bienDongService;
			this._bienDongChiTietService = bienDongChiTietService;
			this._taiSanService = taiSanService;
			this._trungGianBDYCModelFactory = trungGianBDYCModelFactory;
			this._yeuCauChiTietModelFactory = yeuCauChiTietModelFactory;
			this._yeuCauNhatKyService = yeuCauNhatKyService;
			this._yeuCauNhatKyModelFactory = yeuCauNhatKyModelFactory;
			this._taiSanNhaService = taiSanNhaService;
			this._loaiTaiSanDonViServices = loaiTaiSanVoHinhService;
			this._taiSanModelFactory = taiSanModelFactory;
			this._thaoTacBienDongModelFactory = thaoTacBienDongModelFactory;
			this._trungGianBDYCService = trungGianBDYCService;
			this._haoMonTaiSanService = haoMonTaiSanService;
			this._khauHaoTaiSanService = khauHaoTaiSanService;
			this._diaBanService = diaBanService;
			this._donViService = donViService;
			this._donViModelFactory = donViModelFactory;
			this._cauHinhModelFactory = cauHinhModelFactory;
            this._hienTrangService = hienTrangService;
            this._hienTrangModelFactory = hienTrangModelFactory;
        }

		#endregion Ctor

		#region Methods

		public virtual IActionResult Edit(int id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
				return AccessDeniedView();

			var item = _itemService.GetYeuCauById(id);
			if (item == null)
				return RedirectToAction("List");
			//prepare model
			var model = _itemModelFactory.PrepareYeuCauModel(null, item);
			return View(model);
		}

		[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
		[FormValueRequired("save", "save-continue")]
		public virtual IActionResult Edit(YeuCauModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetYeuCauById(model.ID);
			if (item == null)
				return RedirectToAction("List");
			if (ModelState.IsValid)
			{
				//_itemModelFactory.PrepareYeuCau(model, item);
				_itemService.UpdateYeuCau(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<YeuCauModel>(), "YeuCau");
				SuccessNotification("Cập nhật dữ liệu thành công !");
				return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
			}
			//prepare model
			model = _itemModelFactory.PrepareYeuCauModel(model, item, true);
			return View(model);
		}

		public virtual IActionResult _ViewYeuCauPopup(decimal yeuCauId, decimal? TrangThaiId = 0)
		{
			var model = new YeuCauModel();
			if (TrangThaiId == (int)enumTRANG_THAI_YEU_CAU.DA_DUYET)
			{
				var _bd = _bienDongService.GetBienDongById(yeuCauId);
				var _bdct = _bienDongChiTietService.GetBienDongChiTietByBDId(_bd.ID);
				model = _itemModelFactory.PrepareYeuCauModel(model: model, item: new YeuCau(_bd));
			}
			else
			{
				var itemYeuCau = _itemService.GetYeuCauById(yeuCauId);
				var itemYeuCauChiTiet = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(itemYeuCau.ID);
				model = _itemModelFactory.PrepareYeuCauModel(model: model, item: itemYeuCau);
				model.YCCTModel = _yeuCauChiTietModelFactory.PrepareYeuCauChiTietModel(model: new YeuCauChiTietModel(), item: itemYeuCauChiTiet);
			}
			return PartialView(model);
		}

		/// <summary>
		/// Description: Xóa biến động trạng thái != CHO_DUYET && != DA_DUYET
		/// </summary>
		/// <param name="yeuCauId">YeuCauID</param>
		/// <param name="loaiBienDongId">loaiBienDongId</param>
		/// <returns></returns>
		[HttpDelete]
		public virtual IActionResult DeleteYeuCauBienDong(decimal yeuCauId, decimal? loaiBienDongId = 0)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
				&& !_quyenService.Authorize(StandardPermissionProvider.USERQLBDTangNguyenGia)
				&& !_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamNguyenGia)
				&& !_quyenService.Authorize(StandardPermissionProvider.USERQLBDDieuChuyenMotPhan)
				&& !_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan))
				return AccessDeniedView();
			//try to get a store with the specified id
			var itemYeuCau = _itemService.GetYeuCauById(yeuCauId);
			if (itemYeuCau == null)
				return RedirectToAction("ListBienDongTaiSan", new { loaiLyDoId = loaiBienDongId });
			try
			{
				if (itemYeuCau.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.DA_DUYET)
				{
					itemYeuCau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.NHAP;
					_itemService.UpdateYeuCau(itemYeuCau);
					//ghi log
					_yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(itemYeuCau.ToModel<YeuCauModel>(), enumLOAI_NHATKY_YEUCAU.XOA);
					_hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Trạng thái nháp", itemYeuCau.ToModel<YeuCauModel>(), "YeuCau");
					UpdateSessionSearchModel<TrungGianBDYCSearchModel>(true);
					//activity log
					return JsonSuccessMessage("Xoá dữ liệu thành công", itemYeuCau.ToModel<YeuCauModel>());
				}
				else
				{
					return JsonErrorMessage("Bản ghi này không được xóa", itemYeuCau.ToModel<YeuCauModel>());
				}
			}
			catch (Exception exc)
			{
				ErrorNotification(exc);
				return RedirectToAction("ListBienDongTaiSan", new { loaiLyDoId = loaiBienDongId });
			}
		}

		[HttpPost]
		public virtual IActionResult Delete(int id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
				&& !_quyenService.Authorize(StandardPermissionProvider.USERQLBDTangNguyenGia)
				&& !_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamNguyenGia)
				&& !_quyenService.Authorize(StandardPermissionProvider.USERQLBDDieuChuyenMotPhan)
				&& !_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetYeuCauById(id);
			if (item == null)
				return RedirectToAction("List");
			try
			{
				_itemService.DeleteYeuCau(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<YeuCauModel>(), "YeuCau");
				//activity log
				SuccessNotification("Xoá dữ liệu thành công");
				UpdateSessionSearchModel<TrungGianBDYCSearchModel>(true);
				return RedirectToAction("List");
			}
			catch (Exception exc)
			{
				ErrorNotification(exc);
				return RedirectToAction("Edit", new { id = item.ID });
			}
		}

		/// <summary>
		/// Tính toán giá trị còn lại của tài sản khi ngày biến động đổi<br></br>
		/// Tính toán giá trị còn lại khi tăng, giảm nguyên giá
		/// </summary>
		/// <param name="ngayBD">Ngày biến động</param>
		/// <param name="TAI_SAN_ID">id tài sản</param>
		/// <returns></returns>

		public virtual IActionResult CalculateGTCL(DateTime? ngayBD, decimal TAI_SAN_ID, decimal? nguyenGiaBienDong = 0, decimal? loaiBienDongId = 0)
		{
			if (!ngayBD.HasValue)
				return Json("0");
			var bienDongTruoc = _bienDongService.GetBienDongMoiNhatByTaiSanId(taiSanId: TAI_SAN_ID, ngayBienDong: ngayBD);
			BD_GTCL tinhGiaTriTSModel = new BD_GTCL();
			if (bienDongTruoc != null)
			{
				var BDChiTietTruoc = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDongTruoc.ID);
				if (BDChiTietTruoc == null)
					return Json("0");
				var ts = _taiSanService.GetTaiSanById(TAI_SAN_ID);
				if (ts.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || ts.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
					return Json(bienDongTruoc.NGUYEN_GIA);
				decimal? tyLeHM = 0;
				if (ts.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
				{
					var dm = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(ts.LOAI_TAI_SAN_DON_VI_ID.GetValueOrDefault());
					if (dm != null)
						tyLeHM = dm.HM_TY_LE;
				}
				else
				{
					tyLeHM = ts.loaitaisan.HM_TY_LE;
				}
				//Tính tổng nguyên giá hiện tại
				var nguyenGia = _bienDongService.TinhNguyenGiaTaiSan(taiSanId: TAI_SAN_ID, To_Date_BienDong: ngayBD);
				var l_ngayBienDong = ngayBD;
				decimal HM_GIA_TRI_CON_LAI_CU = 0;
				if (bienDongTruoc.NGAY_BIEN_DONG.Value.Year != ngayBD.Value.Year) //Kiểm tra biến động trước có xảy ra cùng năm với biến động hiện tại hay không
				{
					//Lấy giá trị chốt năm trước trong bảng KT hao mòn
					var kt_hmts = new GS.Core.Domain.KT.HaoMonTaiSan();
					/*if (ngayBD.Value.Month == 12 && ngayBD.Value.Day == 31)
						kt_hmts = _haoMonTaiSanService.GetHaoMonTaiSanByTSIdandNamBaoCao(tsId: TAI_SAN_ID, namBaoCao: ngayBD.Value.Year);
					else*/
						kt_hmts = _haoMonTaiSanService.GetHaoMonTaiSanByTSIdandNamBaoCao(tsId: TAI_SAN_ID, namBaoCao: (ngayBD.Value.Year - 1));
					//Lấy giá trị chốt còn lại tháng trước trong kt khấu hao
					//var kt_khts = _khauHaoTaiSanService
					if (kt_hmts != null)
						HM_GIA_TRI_CON_LAI_CU = kt_hmts.TONG_GIA_TRI_CON_LAI.GetValueOrDefault();
				}
				else
				{
					//HM_GIA_TRI_CON_LAI_CU = CommonCalculate.GiaTriConLaiCu(BDChiTietTruoc.HM_GIA_TRI_CON_LAI ?? 0, l_ngayBienDong ?? DateTime.Now, bienDongTruoc.NGAY_BIEN_DONG ?? DateTime.Now, nguyenGia, tyLeHM ?? 0) ?? 0;
					HM_GIA_TRI_CON_LAI_CU = BDChiTietTruoc.HM_GIA_TRI_CON_LAI??0;
				}
				var giaTriConLai = HM_GIA_TRI_CON_LAI_CU;//Biến local chứa giá trị còn lại mới
				tinhGiaTriTSModel.TS_GTCLTruocBD = HM_GIA_TRI_CON_LAI_CU;
				if (nguyenGiaBienDong > 0)
				{
					if (ngayBD.Value.Month == 12 && ngayBD.Value.Day == 31)
					{
						//tính hao mòn tài sản
						if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI)
							nguyenGia = nguyenGia + nguyenGiaBienDong.GetValueOrDefault();
						else if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
							nguyenGia = nguyenGia - nguyenGiaBienDong.GetValueOrDefault();
						if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI)
							giaTriConLai = (HM_GIA_TRI_CON_LAI_CU + nguyenGiaBienDong.GetValueOrDefault());
						else if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
							giaTriConLai = (HM_GIA_TRI_CON_LAI_CU - nguyenGiaBienDong.GetValueOrDefault());
						//else if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
						//	giaTriConLai = _yeuCauChiTietModelFactory.CalculateGTCLDieuChuyenMotPhan(TAI_SAN_ID, ngayBD, HM_GIA_TRI_CON_LAI_CU, nguyenGiaBienDong ?? 0);
						
						//giaTriConLai = CommonCalculate.GiaTriConLaiCu(giaTriConLai, l_ngayBienDong ?? DateTime.Now, l_ngayBienDong.Value.AddDays(-1), nguyenGia, tyLeHM ?? 0) ?? 0;
						if (giaTriConLai < 0)
							giaTriConLai = 0;

					}
					else
					{

						if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI)
							nguyenGia = nguyenGia + nguyenGiaBienDong.GetValueOrDefault();
						else if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
							nguyenGia = nguyenGia - nguyenGiaBienDong.GetValueOrDefault();
						if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI)
							giaTriConLai = (HM_GIA_TRI_CON_LAI_CU + nguyenGiaBienDong.GetValueOrDefault());
						else if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
							giaTriConLai = (HM_GIA_TRI_CON_LAI_CU - nguyenGiaBienDong.GetValueOrDefault());
						//else if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
						//	giaTriConLai = _yeuCauChiTietModelFactory.CalculateGTCLDieuChuyenMotPhan(TAI_SAN_ID, ngayBD, HM_GIA_TRI_CON_LAI_CU, nguyenGiaBienDong ?? 0);
						if (giaTriConLai < 0)
							giaTriConLai = 0;
					}
				}

				tinhGiaTriTSModel.TS_GTCLTruocBD = tinhGiaTriTSModel.TS_GTCLTruocBD > 0 ? tinhGiaTriTSModel.TS_GTCLTruocBD : 0;
				tinhGiaTriTSModel.TS_GTCLSauBD = giaTriConLai;
				return Json(tinhGiaTriTSModel);
			}
			else
			{
				var YCTruoc = _itemService.GetYeuCauMoiNhatByTaiSanId(TAI_SAN_ID, enumTRANG_THAI_YEU_CAU.CHO_DUYET, null, ngayBD);
				if (YCTruoc != null)
				{
					var YCCTTruoc = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(YCTruoc.ID);
					decimal? tyLeHM = 0;
					if (YCTruoc.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || YCTruoc.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
						return Json(bienDongTruoc?.NGUYEN_GIA);
					if (YCTruoc.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
					{
						var dm = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(YCTruoc.LOAI_TAI_SAN_DON_VI_ID.GetValueOrDefault());
						if (dm != null)
							tyLeHM = dm.HM_TY_LE;
					}
					else
					{
						tyLeHM = YCTruoc.loaitaisan.HM_TY_LE;
					}
					//Tính tổng nguyên giá hiện tại
					var nguyenGia = _bienDongService.TinhNguyenGiaTaiSan(taiSanId: TAI_SAN_ID, To_Date_BienDong: ngayBD);
					var l_ngayBienDong = ngayBD;
					var HM_GIA_TRI_CON_LAI_CU = CommonCalculate.GiaTriConLaiCu(YCCTTruoc.HM_GIA_TRI_CON_LAI ?? 0, l_ngayBienDong ?? DateTime.Now, YCTruoc.NGAY_BIEN_DONG ?? DateTime.Now, nguyenGia, tyLeHM ?? 0);
					var giaTriConLai = HM_GIA_TRI_CON_LAI_CU;//Biến local chứa giá trị còn lại mới
					tinhGiaTriTSModel.TS_GTCLTruocBD = HM_GIA_TRI_CON_LAI_CU;
					if (nguyenGiaBienDong > 0)
					{
						if (ngayBD.Value.Month == 12 && ngayBD.Value.Day == 31)
						{
							//tính hao mòn tài sản
							if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI)
								nguyenGia = nguyenGia + nguyenGiaBienDong.GetValueOrDefault();
							else if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
								nguyenGia = nguyenGia - nguyenGiaBienDong.GetValueOrDefault();
							if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI)
								giaTriConLai = (HM_GIA_TRI_CON_LAI_CU + nguyenGiaBienDong.GetValueOrDefault());
							else if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
								giaTriConLai = (HM_GIA_TRI_CON_LAI_CU - nguyenGiaBienDong.GetValueOrDefault());
							//else if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
							//	giaTriConLai = _yeuCauChiTietModelFactory.CalculateGTCLDieuChuyenMotPhan(TAI_SAN_ID, ngayBD, HM_GIA_TRI_CON_LAI_CU, nguyenGiaBienDong ?? 0);

							giaTriConLai = CommonCalculate.GiaTriConLaiCu(giaTriConLai??0, l_ngayBienDong ?? DateTime.Now, l_ngayBienDong.Value.AddDays(-1), nguyenGia, tyLeHM ?? 0) ?? 0;
							if (giaTriConLai < 0)
								giaTriConLai = 0;

						}
						else
						{

							if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI)
								nguyenGia = nguyenGia + nguyenGiaBienDong.GetValueOrDefault();
							else if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
								nguyenGia = nguyenGia - nguyenGiaBienDong.GetValueOrDefault();

							if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI)
								giaTriConLai = (HM_GIA_TRI_CON_LAI_CU.GetValueOrDefault() + nguyenGiaBienDong.GetValueOrDefault());
							else if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
								giaTriConLai = (HM_GIA_TRI_CON_LAI_CU.GetValueOrDefault() - nguyenGiaBienDong.GetValueOrDefault());
							//else if (loaiBienDongId == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
							//	giaTriConLai = _yeuCauChiTietModelFactory.CalculateGTCLDieuChuyenMotPhan(TAI_SAN_ID, ngayBD, HM_GIA_TRI_CON_LAI_CU.GetValueOrDefault(), nguyenGiaBienDong ?? 0);

							if (giaTriConLai < 0)
								giaTriConLai = 0;
						}
					}
					tinhGiaTriTSModel.TS_GTCLSauBD = giaTriConLai ?? 0;

					return Json(tinhGiaTriTSModel);
				}
				return Json(0);
			}
		}

	   [HttpGet]
		public virtual IActionResult _GetListYeuCauByTSId(decimal taisanId, bool? isTraCuu = false)
		{
			var yeuCauSearch = new YeuCauSearchModel();
			yeuCauSearch.taisanId = taisanId;
			yeuCauSearch.isTraCuu = isTraCuu;
			return PartialView(yeuCauSearch);
		}

		/// <summary>
		/// Description: Controller điều hướng theo loại lý do biến động
		/// </summary>
		/// <param name="guid"></param>
		/// <param name="loaiLyDoBienDong"></param>
		/// <returns></returns>
		/// Create by BinhDC
		public virtual IActionResult BienDongTaiSan(Guid guid, decimal loaiLyDoBienDong, decimal? trangThaiId = 0)
		{
			if (_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
				|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDTangNguyenGia)
				|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamNguyenGia)
				|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan)
				|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDDieuChuyenMotPhan)
				|| trangThaiId != (int)enumTRANG_THAI_TAI_SAN.TRA_LAI)
			{
				var ts = _taiSanService.GetTaiSanByGuId(guid);

				switch (loaiLyDoBienDong)
				{
					case (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI:
						//dieu huong den controller create tang nguyen gia
						return RedirectToAction("CreateOrUpdateTangNguyenGia", new { guidTS = guid });

					case (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI:
						//dieu huong den controller create giam nguyen gia
						return RedirectToAction("CreateOrUpdateGiamNguyenGia", new { guidTS = guid });

					case (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO:
					case (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN:
						if (ts.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT && 
							ts.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.NHA && 
							ts.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC)
							return RedirectToAction("GiamTaiSan", new { guidTS = guid, loaiLyDoGiam = (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO });
						else
							return RedirectToAction("EntryGiamTaiSan", new { guidTS = guid });
					case (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN:
						return RedirectToAction("CreateThayDoiThongTin", "ThayDoiThongTin", new { guidTS = guid });

					default:
						return RedirectToAction("ListBienDongTaiSan", new { loaiLyDoId = loaiLyDoBienDong });
				}
			}
			else
				return AccessDeniedView();
		}
		public virtual IActionResult GiamNhieuTaiSan(string TaiSanIds)
		{
			if (_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan))
			{
				var yeuCauModel = new YeuCauModel();
				yeuCauModel.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO;
				//yeuCauModel.LOAI_HINH_TAI_SAN_ID = taisan.LOAI_HINH_TAI_SAN_ID;
				//yeuCauModel.TAI_SAN_ID = taisan.ID;
				yeuCauModel = _itemModelFactory.PrepareYeuCauModelGiamNhieuTaiSan(model: yeuCauModel, item: null);
				yeuCauModel.YCCTModel.IS_BAN_THANH_LY = yeuCauModel.YCCTModel.IS_BAN_THANH_LY ?? false;
				yeuCauModel.strTaiSanIds = TaiSanIds;
				return View(yeuCauModel);
			}
			return AccessDeniedView();
		}
		[HttpPost]
		public virtual IActionResult GiamNhieuTaiSan(YeuCauModel model)
		{
			if (ModelState.IsValid)
			{
				var taiSanIds = model.strTaiSanIds.Split(',').Select(p => int.Parse(p)).ToList();
				foreach (var taiSanId in taiSanIds)
				{
					var taiSan = _taiSanService.GetTaiSanById(taiSanId);
					if (taiSan != null)
					{
						var item = new YeuCau();
						var ycct = new YeuCauChiTiet();
						var list_ly_do_giam_can_thu_tien = Enum.GetValues(typeof(enumLY_DO_GIAM_THU_TIEN)).Cast<enumLY_DO_GIAM_THU_TIEN>().Select(p => (decimal?)p);
						//tạm thời với từng vòng lặp
						model.TAI_SAN_ID = taiSanId;

						item = _itemModelFactory.PrepareYeuCauGiamTaiSan(model, item);
						item.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
						_itemService.InsertYeuCau(item);
						ycct = _yeuCauChiTietModelFactory.PrepareYeuCauChiTietGiamTaiSan(model.YCCTModel, ycct, item);
						//nếu không phải là bán thanh lý thì is bán thanh lý là null
						if (!list_ly_do_giam_can_thu_tien.Contains(item.LY_DO_BIEN_DONG_ID))
							ycct.IS_BAN_THANH_LY = null;
						//nếu đã bán thanh lý thu được tiền thì ngày bán thanh lý là hiện tại
						if (ycct.IS_BAN_THANH_LY ?? false)
							ycct.NGAY_BAN_THANH_LY = item.NGAY_BIEN_DONG;
						else
							ycct.NGAY_BAN_THANH_LY = null;
						_yeuCauChiTietService.InsertYeuCauChiTiet(ycct);
						_yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(model, enumLOAI_NHATKY_YEUCAU.THEM_MOI);
						_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin biến động", item.ToModel<YeuCauModel>(), "YeuCau");
						if (_taiSanModelFactory.CheckAutoDuyet(taiSan.LOAI_HINH_TAI_SAN_ID, taiSan.NGUYEN_GIA_BAN_DAU))
							_thaoTacBienDongModelFactory.DuyetYeuCauFunc(item.ID, item);
					}
				}
				return JsonSuccessMessage("Thêm mới biến động thành công");
			}
			var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			return JsonErrorMessage("Error", list);
		}
		/// <summary>
		/// danh sách biến động của tài sản
		/// </summary>
		/// <param name="loaiLyDoId"></param>
		/// <param name="pageIndex"></param>
		/// <returns></returns>
		public virtual IActionResult ListBienDongTaiSan(decimal loaiLyDoId, int? pageIndex = 0, bool? isDanhSachTaiSanDuAn = false)
		{
			if (_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
				|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDTangNguyenGia)
				|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamNguyenGia)
				|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDDieuChuyenMotPhan)
				|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan))
			{
				//prepare model
				var searchmodel = new YeuCauSearchModel();
				searchmodel.LOAI_LY_DO_BD_ID = loaiLyDoId;
				if (searchmodel.LOAI_LY_DO_BD_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
				{
					searchmodel.LOAI_LY_DO_BD_ID = (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO;
				}
				var hasDonViQLDA = _donViService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
				if (hasDonViQLDA)
				{
					var donvi = _donViModelFactory.GetDonViWithConditions(donViId: _workContext.CurrentDonVi.ID, isBanQuanLyDuAn: hasDonViQLDA, isCreateTSDA: isDanhSachTaiSanDuAn);
					searchmodel.DON_VI_ID = donvi.ID;
				}
				else
				{
					searchmodel.DON_VI_ID = _workContext.CurrentDonVi.ID;
				}
				searchmodel.IsDanhSachTaiSanDuAn = isDanhSachTaiSanDuAn;
				var model = _itemModelFactory.PrepareYeuCauSearchModel(searchmodel);
				if (pageIndex > 0)
				{
					model.Page = (int)pageIndex;
				}
				var _searchModel = HttpContext.Session.GetObject<TrungGianBDYCSearchModel>(enumTENCAUHINH.KeySearch+typeof(TrungGianBDYCSearchModel).Name);
				if (_searchModel != null && _searchModel.IsLoadSession)
				{
					_cauHinhModelFactory.PrePareModelBySession(model, _searchModel);
					UpdateSessionSearchModel<TrungGianBDYCSearchModel>(false);
				}
				return View(model);
			}
			else
				return AccessDeniedView();
		}

		/// <summary>
		/// Description: Insert yeu cau bien dong tai san
		/// </summary>
		/// <param name="model">YeuCauModel</param>
		/// <returns></returns>
		[HttpPost]
		public virtual IActionResult InsertOrUpdateBienDongTS(YeuCauModel model)
		{
			//if (_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
			//	|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDTangNguyenGia)
			//	|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamNguyenGia)
			//	|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan))
			//	return AccessDeniedView();
			ValidateListHienTrang(model);
			if (ModelState.IsValid)
			{
				TaiSan taiSanItem = _taiSanService.GetTaiSanById(model.TAI_SAN_ID);
				model.DU_AN_ID = taiSanItem.DU_AN_ID;
				if (model.DU_AN_ID.HasValue && model.DU_AN_ID > 0)
					model.IsTaiSanDuAn = true;
				else
					model.IsTaiSanDuAn = false;
				//Prepare thông tin chung của tài sản vào yêu cầu
				//Thay đổi thông tin đất thì update lại thông tin đất theo địa chỉ vs địa bàn
				if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT && model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
				{
					model.TAI_SAN_TEN = model.YCCTModel.DIA_CHI;
					var tinhItem = _diaBanService.GetDiaBanById(model.YCCTModel.DatTinhId ?? 0);
					var huyenItem = _diaBanService.GetDiaBanById(model.YCCTModel.DatHuyenId ?? 0);
					var xaItem = _diaBanService.GetDiaBanById(model.YCCTModel.DatXaId ?? 0);
					if (xaItem != null)
						model.TAI_SAN_TEN = model.TAI_SAN_TEN + ", " + xaItem.TEN;
					if (huyenItem != null)
						model.TAI_SAN_TEN = model.TAI_SAN_TEN + ", " + huyenItem.TEN;
					if (tinhItem != null)
						model.TAI_SAN_TEN = model.TAI_SAN_TEN + ", " + tinhItem.TEN;
				}
				if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA && ( model.taisanNhaModel?.TAI_SAN_DAT_ID??0) <= 0 && model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
				{
					// Lưu ý: TaiSanNha,ycct.NHA_DIA_CHI, bdct.NHA_DIA_CHI  sẽ lưu Địa chỉ gốc còn ycct.DIA_CHI, bdct.DIA_CHI sẽ lưu địa chỉ đầy đủ phục vụ báo cáo dễ dàng hơn
					var tinhItem = _diaBanService.GetDiaBanById(model.YCCTModel.NHA_TinhId ?? 0);
					var huyenItem = _diaBanService.GetDiaBanById(model.YCCTModel.NHA_HuyenId ?? 0);
					var xaItem = _diaBanService.GetDiaBanById(model.YCCTModel.NHA_XaId ?? 0);
					model.YCCTModel.NHA_DIA_CHI = model.YCCTModel.DIA_CHI;
					model.YCCTModel.NHA_SO_TANG = model.YCCTModel.NHA_SO_TANG;
					if (xaItem != null)
						model.YCCTModel.DIA_CHI = model.YCCTModel.DIA_CHI + ", " + xaItem.TEN;
					if (huyenItem != null)
						model.YCCTModel.DIA_CHI = model.YCCTModel.DIA_CHI + ", " + huyenItem.TEN;
					if (tinhItem != null)
						model.YCCTModel.DIA_CHI = model.YCCTModel.DIA_CHI + ", " + tinhItem.TEN;
				}

				//------
				if (model.ID > 0)
				{
                   
					UpdateBD_NV_YeuCau(model);

					return JsonSuccessMessage("Cập nhật biến động thành công", model);
				}
				else
				{
					Insert_NV_YeuCau(model);			
					
					return JsonSuccessMessage("Thêm mới biến động thành công", model);
				}
			}
			//----
			var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			return JsonErrorMessage("Error", list);
		}
		public virtual IActionResult InsertOrUpdateBienDongTSV2(YeuCauModel model)
		{
			//if (_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
			//	|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDTangNguyenGia)
			//	|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamNguyenGia)
			//	|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan))
			//	return AccessDeniedView();
			ValidateListHienTrang(model);
			if (ModelState.IsValid)
			{
				TaiSan taiSanItem = _taiSanService.GetTaiSanById(model.TAI_SAN_ID);
				model.DU_AN_ID = taiSanItem.DU_AN_ID;
				if (model.DU_AN_ID.HasValue && model.DU_AN_ID > 0)
					model.IsTaiSanDuAn = true;
				else
					model.IsTaiSanDuAn = false;
				//Prepare thông tin chung của tài sản vào yêu cầu
				//Thay đổi thông tin đất thì update lại thông tin đất theo địa chỉ vs địa bàn
				if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT && model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
				{
					model.TAI_SAN_TEN = model.YCCTModel.DIA_CHI;
					var tinhItem = _diaBanService.GetDiaBanById(model.YCCTModel.DatTinhId ?? 0);
					var huyenItem = _diaBanService.GetDiaBanById(model.YCCTModel.DatHuyenId ?? 0);
					var xaItem = _diaBanService.GetDiaBanById(model.YCCTModel.DatXaId ?? 0);
					if (xaItem != null)
						model.TAI_SAN_TEN = model.TAI_SAN_TEN + ", " + xaItem.TEN;
					if (huyenItem != null)
						model.TAI_SAN_TEN = model.TAI_SAN_TEN + ", " + huyenItem.TEN;
					if (tinhItem != null)
						model.TAI_SAN_TEN = model.TAI_SAN_TEN + ", " + tinhItem.TEN;
				}
				if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA && (model.taisanNhaModel?.TAI_SAN_DAT_ID ?? 0) <= 0 && model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
				{
					// Lưu ý: TaiSanNha,ycct.NHA_DIA_CHI, bdct.NHA_DIA_CHI  sẽ lưu Địa chỉ gốc còn ycct.DIA_CHI, bdct.DIA_CHI sẽ lưu địa chỉ đầy đủ phục vụ báo cáo dễ dàng hơn
					var tinhItem = _diaBanService.GetDiaBanById(model.YCCTModel.NHA_TinhId ?? 0);
					var huyenItem = _diaBanService.GetDiaBanById(model.YCCTModel.NHA_HuyenId ?? 0);
					var xaItem = _diaBanService.GetDiaBanById(model.YCCTModel.NHA_XaId ?? 0);
					model.YCCTModel.NHA_DIA_CHI = model.YCCTModel.DIA_CHI;
					model.YCCTModel.NHA_SO_TANG = model.YCCTModel.NHA_SO_TANG;
					if (xaItem != null)
						model.YCCTModel.DIA_CHI = model.YCCTModel.DIA_CHI + ", " + xaItem.TEN;
					if (huyenItem != null)
						model.YCCTModel.DIA_CHI = model.YCCTModel.DIA_CHI + ", " + huyenItem.TEN;
					if (tinhItem != null)
						model.YCCTModel.DIA_CHI = model.YCCTModel.DIA_CHI + ", " + tinhItem.TEN;
				}

				//------
				if (model.ID > 0)
				{

					UpdateBD_NV_YeuCau(model);

					return JsonSuccessMessage("Cập nhật biến động thành công", model);
				}
				else
				{
					Insert_NV_YeuCau(model);

					return JsonSuccessMessage("Thêm mới biến động thành công", model);
				}
			}
			//----
			var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			return JsonErrorMessage("Error", list);
		}
		private void ValidateListHienTrang(YeuCauModel model) 
		{
			if (model.YCCTModel.lstHienTrang != null && model.YCCTModel.lstHienTrang.Count() > 0)
			{
				foreach (var hienTrang in model.YCCTModel.lstHienTrang)
				{
					if (hienTrang != null)
					{
						// nếu mà hiện trạng không đúng với loại hình đơn vị nhưng có giá trị thì báo validate
						if (_hienTrangModelFactory.CheckIsHienTrangNhapSai(model.DON_VI_ID ?? _workContext.CurrentDonVi.ID, hienTrang))
						{
							ModelState.AddModelError($"HienTrang_{hienTrang.HienTrangId}", "Hiện trạng không đúng với loại hình đơn vị");
						}
					}
				}
			}

		}
		private void ValidateYeuCauAuToDuyet(YeuCauModel model)
		{
			var CountYC_Chua_Duyet_TruocDo = _itemService.CountYeuCauCuaTaiSan(model.TAI_SAN_ID, new List<decimal> { (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET, (int)enumTRANG_THAI_YEU_CAU.TU_CHOI }, null, model.NGAY_BIEN_DONG);
			if (CountYC_Chua_Duyet_TruocDo > 0)
			{
				ModelState.AddModelError("NGAY_BIEN_DONG", "Không cập nhật được địa bàn do tồn tại biến động của tài sản chưa được duyệt");
			}

		}

		[HttpPost]
		public virtual IActionResult ListBienDongTaiSan(TrungGianBDYCSearchModel searchModel)
		{
			if (_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
				|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDTangNguyenGia)
				|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamNguyenGia)
				|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDDieuChuyenMotPhan)
				|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan))
			{
				//prepare model
				if (searchModel.PageSize == 0) searchModel.PageSize = 10;
				searchModel.isIgnoreTraLai = true;
				searchModel.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
				searchModel.DON_VI_ID = _workContext.CurrentDonVi.ID;
				var listYeuCau = _trungGianBDYCModelFactory.PrepareTrungGianBDYCListModel(searchModel);
				HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch+ searchModel.GetType().Name, searchModel);
				return Json(listYeuCau);
			}
			else
				return AccessDeniedView();
		}

		/// <summary>
		/// Kiểm tra xem có tồn tại biến động giảm tài sản hay không
		/// </summary>
		/// <returns></returns>
		public virtual IActionResult IsHasBienDongGiam(Guid guid)
		{
			var res = _taiSanService.IsNotHasBDGiamTaiSan(guid);
			if (res)
				return JsonSuccessMessage("Thành công");
			else
				return JsonErrorMessage("Tài sản có biến động giảm");
		}
		#endregion Methods

		#region BienDongTangGiamNguyenGia

		public virtual IActionResult CreateOrUpdateTangNguyenGia(Guid guidTS, Guid guidYC, int? pageIndex = 0, bool isTaiSanDuAn = false)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDTangNguyenGia))
				return AccessDeniedView();
			if (guidYC != Guid.Empty)
			{
				if (!_itemService.IsYCNeedToEdit(guidYC))
					return RedirectToAction("InsertEditDeniedView", new { yeuCauGuid = guidYC });
				var yeucau = _itemService.GetYeuCauByGuid(guidYC);
				var model = _itemModelFactory.PrepareYeuCauModelFromBienDong(new YeuCauModel(), null, yeucau);
				if (model.DU_AN_ID > 0)
				{
					model.IsTaiSanDuAn = true;
				}
				else
				{
					model.IsTaiSanDuAn = isTaiSanDuAn;
				}
				return View(model);
			}
			//try to get a store with the specified guid
			var taiSanItem = _taiSanService.GetTaiSanByGuId(guidTS);
			if (taiSanItem == null)
				return RedirectToAction("ListBienDongTaiSan", (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI);
			var yeuCauModel = new YeuCauModel();
			yeuCauModel.TaiSanGuid = taiSanItem.GUID;
			yeuCauModel.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI;
			if (taiSanItem.DU_AN_ID > 0)
				yeuCauModel.IsTaiSanDuAn = true;
			else
				yeuCauModel.IsTaiSanDuAn = isTaiSanDuAn;
			yeuCauModel = _itemModelFactory.PrepareYeuCauModelFromBienDong(model: yeuCauModel, taiSan: taiSanItem, item: null);
			yeuCauModel.pageIndex = pageIndex;
			
			
			return View(yeuCauModel);
		}

		public virtual IActionResult CreateOrUpdateGiamNguyenGia(Guid guidTS, Guid guidYC, int? pageIndex = 0, bool isTaiSanDuAn = false)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamNguyenGia))
				return AccessDeniedView();
			if (guidYC != Guid.Empty)
			{
				if (!_itemService.IsYCNeedToEdit(guidYC))
					return RedirectToAction("InsertEditDeniedView", new { yeuCauGuid = guidYC });
				var yeucau = _itemService.GetYeuCauByGuid(guidYC);
				var model = _itemModelFactory.PrepareYeuCauModelFromBienDong(new YeuCauModel(), null, yeucau);
				if (model.DU_AN_ID > 0)
				{
					model.IsTaiSanDuAn = true;
				}
				else
				{
					model.IsTaiSanDuAn = isTaiSanDuAn;
				}
				return View(model);
			}
			//try to get a store with the specified guid
			var taiSanItem = _taiSanService.GetTaiSanByGuId(guidTS);
			if (taiSanItem == null)
				return RedirectToAction("ListBienDongTaiSan", (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI);
			var yeuCauModel = new YeuCauModel();
			yeuCauModel.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI;
			yeuCauModel = _itemModelFactory.PrepareYeuCauModelFromBienDong(model: yeuCauModel, taiSan: taiSanItem, item: null);
			yeuCauModel.pageIndex = pageIndex;
			if (_trungGianBDYCService.GetGTCLGanNhatDaDuyet(yeuCauModel.TAI_SAN_ID) > 0)
			{
				if (taiSanItem.DU_AN_ID > 0)
					yeuCauModel.IsTaiSanDuAn = true;
				else
					yeuCauModel.IsTaiSanDuAn = isTaiSanDuAn;
				return View(yeuCauModel);
			}
			ErrorNotification("Tài sản phải có GTCL lớn hơn không mới được giảm NG");
			return RedirectToAction("ListBienDongTaiSan", "YeuCau", new { loaiLyDoId = (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI });
		}

		#endregion BienDongTangGiamNguyenGia

		#region GiamTaiSan

		public virtual IActionResult GiamTaiSan(Guid guidTS, Guid guidYC, decimal? loaiLyDoGiam = 0, int? pageIndex = 0)
		{
			//if (_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
			//	|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDTangNguyenGia)
			//	|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamNguyenGia)
			//	|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan))
			//	return AccessDeniedView();
			//try to get a store with the specified guid
			var taisan = _taiSanService.GetTaiSanByGuId(guidTS);
			if (guidYC != Guid.Empty)
			{
				if (!_itemService.IsYCNeedToEdit(guidYC))
					return RedirectToAction("InsertEditDeniedView", new { yeuCauGuid = guidYC });
				var yeucau = _itemService.GetYeuCauByGuid(guidYC);
				var model = _itemModelFactory.PrepareYeuCauModelGiamTaiSan(new YeuCauModel(), yeucau);
				model.YCCTModel.IS_BAN_THANH_LY = model.YCCTModel.IS_BAN_THANH_LY ?? false;
				if (taisan != null && taisan.DU_AN_ID > 0)
				{
					model.DU_AN_ID = taisan.DU_AN_ID;
					model.IsTaiSanDuAn = true;
				}
				else
				{
					model.IsTaiSanDuAn = false;
				}				
				return View(model);
			}
			if (taisan == null)
				return RedirectToAction("ListBienDongTaiSan", loaiLyDoGiam);
			var yeuCauModel = new YeuCauModel();
			yeuCauModel.LOAI_BIEN_DONG_ID = loaiLyDoGiam;
			yeuCauModel.LOAI_HINH_TAI_SAN_ID = taisan.LOAI_HINH_TAI_SAN_ID;
			yeuCauModel.TAI_SAN_ID = taisan.ID;
			yeuCauModel = _itemModelFactory.PrepareYeuCauModelGiamTaiSan(model: yeuCauModel, item: null);
			yeuCauModel.pageIndex = pageIndex;
			yeuCauModel.YCCTModel.IS_BAN_THANH_LY = yeuCauModel.YCCTModel.IS_BAN_THANH_LY ?? false;
			if (taisan != null && taisan.DU_AN_ID > 0)
			{
				yeuCauModel.DU_AN_ID = taisan.DU_AN_ID;
				yeuCauModel.IsTaiSanDuAn = true;
			}
			else
			{
				yeuCauModel.IsTaiSanDuAn = false;
			}
			return View(yeuCauModel);
		}
		[HttpPost]
		public IActionResult InsertGiamTaiSan(YeuCauModel model)
		{
			if (ModelState.IsValid)
			{
				var item = new YeuCau();
				var ycct = new YeuCauChiTiet();
				if (model.ID > 0)
				{
					item = _itemService.GetYeuCauById(model.ID);
					var _ycct = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(model.ID);
					ycct = _yeuCauChiTietModelFactory.PrepareYeuCauChiTietGiamTaiSan(model.YCCTModel, _ycct, item);
				}
				var list_ly_do_giam_can_thu_tien = Enum.GetValues(typeof(enumLY_DO_GIAM_THU_TIEN)).Cast<enumLY_DO_GIAM_THU_TIEN>().Select(p => (decimal?)p);

				item = _itemModelFactory.PrepareYeuCauGiamTaiSan(model, item);
				var taiSan = _taiSanService.GetTaiSanById(item.TAI_SAN_ID);
				if (item.ID > 0)
				{
					_itemService.UpdateYeuCau(item);
					//nếu không phải là bán thanh lý thì is bán thanh lý là null
					if (!list_ly_do_giam_can_thu_tien.Contains(item.LY_DO_BIEN_DONG_ID))
						ycct.IS_BAN_THANH_LY = null;
					//nếu đã bán thanh lý thu được tiền thì ngày bán thanh lý là hiện tại
					if (ycct.IS_BAN_THANH_LY ?? false)
						ycct.NGAY_BAN_THANH_LY = item.NGAY_BIEN_DONG;
					else
						ycct.NGAY_BAN_THANH_LY = null;
					_yeuCauChiTietService.UpdateYeuCauChiTiet(ycct);
					DeleteYeuCauDieuChuyenKem(model);
					if (model.YCCTModel.DIEU_CHUYEN_KEM_THEO)
					{
						ThemYeuCauDieuChuyenKem(ycct);
					}
					//ghi log
					_yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(model, enumLOAI_NHATKY_YEUCAU.THEM_MOI);
					_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin biến động", item.ToModel<YeuCauModel>(), "YeuCau");
					if (_taiSanModelFactory.CheckAutoDuyet(taiSan.LOAI_HINH_TAI_SAN_ID, taiSan.NGUYEN_GIA_BAN_DAU))
						_thaoTacBienDongModelFactory.DuyetYeuCauFunc(item.ID, item);
					UpdateSessionSearchModel<TrungGianBDYCSearchModel>(true);
					return JsonSuccessMessage("Cập nhật thành công", model);
				}
				item.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
				_itemService.InsertYeuCau(item);
				ycct = _yeuCauChiTietModelFactory.PrepareYeuCauChiTietGiamTaiSan(model.YCCTModel, ycct, item);
				//nếu không phải là bán thanh lý thì is bán thanh lý là null
				if (!list_ly_do_giam_can_thu_tien.Contains(item.LY_DO_BIEN_DONG_ID))
					ycct.IS_BAN_THANH_LY = null;
				//nếu đã bán thanh lý thu được tiền thì ngày bán thanh lý là hiện tại
				if (ycct.IS_BAN_THANH_LY ?? false)
					ycct.NGAY_BAN_THANH_LY = item.NGAY_BIEN_DONG;
				else
					ycct.NGAY_BAN_THANH_LY = null;
				_yeuCauChiTietService.InsertYeuCauChiTiet(ycct);
				_yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(model, enumLOAI_NHATKY_YEUCAU.THEM_MOI);
				_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin biến động", item.ToModel<YeuCauModel>(), "YeuCau");

				// doi voi loai dieu chuyen (Dat hoac Nha) có dieu chuyen kem theo
				if (model.YCCTModel.DIEU_CHUYEN_KEM_THEO)
				{
					ThemYeuCauDieuChuyenKem(ycct);
				}
				if (_taiSanModelFactory.CheckAutoDuyet(taiSan.LOAI_HINH_TAI_SAN_ID, taiSan.NGUYEN_GIA_BAN_DAU))
					_thaoTacBienDongModelFactory.DuyetYeuCauFunc(item.ID, item);
				return JsonSuccessMessage("Thêm mới yêu cầu thành công", model);
			}
			var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			return JsonErrorMessage("Error", list);
		}

		public void DeleteYeuCauDieuChuyenKem(YeuCauModel model)
		{
			if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
			{
				var _lstNha = _taiSanNhaService.GetTaiSanNhaByDatId(model.TAI_SAN_ID);
				if (_lstNha != null && _lstNha.Count > 0)
				{
					foreach (var _nha in _lstNha)
					{
						var ycDieuChuyenKem = _itemService.GetYeuCauDieuChuyenKem(_nha.TAI_SAN_ID);
						if (ycDieuChuyenKem != null)
						{
							ycDieuChuyenKem.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.NHAP;
							_itemService.UpdateYeuCau(ycDieuChuyenKem);
						}
					}
				}
			}
			if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA)
			{
				var tsNha = _taiSanNhaService.GetTaiSanNhaById(model.TAI_SAN_ID);
				var ycDieuChuyenKem = _itemService.GetYeuCauDieuChuyenKem(tsNha.TAI_SAN_DAT_ID.GetValueOrDefault());
				if (ycDieuChuyenKem != null)
				{
					ycDieuChuyenKem.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.NHAP;
					_itemService.UpdateYeuCau(ycDieuChuyenKem);
				}
			}
		}

		public void ThemYeuCauDieuChuyenKem(YeuCauChiTiet item)
		{
			var yeucau = _itemService.GetYeuCauById(item.YEU_CAU_ID);
			var ycctModel = item.ToModel<YeuCauChiTietModel>();
			if (yeucau.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
			{
				var _lstNha = _taiSanNhaService.GetTaiSanNhaByDatId(yeucau.TAI_SAN_ID);
				if (_lstNha != null && _lstNha.Count > 0)
				{
					foreach (var _tsnha in _lstNha)
					{
						var ts = _taiSanService.GetTaiSanById(_tsnha.TAI_SAN_ID);
						var _yeucauNha = _itemModelFactory.PrepareYeuCauForDieuChuyenKemTheo(ycDieuChuyenKem: yeucau, taisan: ts);
						_yeucauNha.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
						_itemService.InsertYeuCau(_yeucauNha);

						var ycctNha = _yeuCauChiTietModelFactory.PrepareYeuCauChiTietGiamTaiSan(ycctModel, new YeuCauChiTiet(), _yeucauNha);
						_yeuCauChiTietService.InsertYeuCauChiTiet(ycctNha);
						_yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(_yeucauNha.ToModel<YeuCauModel>(), enumLOAI_NHATKY_YEUCAU.THEM_MOI);
						_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin biến động", _yeucauNha.ToModel<YeuCauModel>(), "YeuCau");
					}
				}
			}
			if (yeucau.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA)
			{
				var tsNha = _taiSanNhaService.GetTaiSanNhaByTaiSanId(yeucau.TAI_SAN_ID);
				var ts = _taiSanService.GetTaiSanById(tsNha.TAI_SAN_DAT_ID.GetValueOrDefault());
				if (ts != null)
				{
					var _yeucauDat = _itemModelFactory.PrepareYeuCauForDieuChuyenKemTheo(ycDieuChuyenKem: yeucau, taisan: ts);
					_yeucauDat.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
					_itemService.InsertYeuCau(_yeucauDat);

					var ycctNha = _yeuCauChiTietModelFactory.PrepareYeuCauChiTietGiamTaiSan(ycctModel, new YeuCauChiTiet(), _yeucauDat);
					_yeuCauChiTietService.InsertYeuCauChiTiet(ycctNha);
					_yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(_yeucauDat.ToModel<YeuCauModel>(), enumLOAI_NHATKY_YEUCAU.THEM_MOI);
					_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin biến động", _yeucauDat.ToModel<YeuCauModel>(), "YeuCau");
				}
			}
		}

		public virtual IActionResult EntryGiamTaiSan(Guid guidTS)
		{
			return View(model: guidTS.ToString());
		}
		#endregion GiamTaiSan

		#region Biến động điều chuyển một phần

		public virtual IActionResult CreateDieuChuyenTungPhan(Guid guidTS)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDDieuChuyenMotPhan))
				return AccessDeniedView();
			//try to get a store with the specified guid
			var taiSanItem = _taiSanService.GetTaiSanByGuId(guidTS);
			if (taiSanItem == null)
				return RedirectToAction("ListBienDongTaiSan", (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN);
			var yeuCauModel = new YeuCauModel();
			yeuCauModel.TaiSanGuid = taiSanItem.GUID;
			yeuCauModel.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN;
			yeuCauModel.NGAY_SU_DUNG = taiSanItem.NGAY_SU_DUNG;
			yeuCauModel = _itemModelFactory.PrepareYeuCauModelFromBienDong(model: yeuCauModel, taiSan: taiSanItem, item: null);
			return View(yeuCauModel);
		}

		public virtual IActionResult UpdateDieuChuyenTungPhan(Guid guid, int? pageIndex = 0)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDDieuChuyenMotPhan))
				return AccessDeniedView();
			if (!_itemService.IsYCNeedToEdit(guid))
				return RedirectToAction("InsertEditDeniedView", new { yeuCauGuid = guid });
			var yeuCauItem = _itemService.GetYeuCauByGuid(guid);
			if (yeuCauItem == null)
				return RedirectToAction("ListBienDongTaiSan", (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN);
			var taiSanItem = _taiSanService.GetTaiSanById(yeuCauItem.TAI_SAN_ID);
			if (taiSanItem == null)
				return RedirectToAction("ListBienDongTaiSan", (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN);
			var yeuCauModel = _itemModelFactory.PrepareYeuCauModelFromBienDong(model: new YeuCauModel(), taiSan: taiSanItem, item: yeuCauItem);
			yeuCauModel.pageIndex = pageIndex;
			return View(yeuCauModel);
		}

		#endregion Biến động điều chuyển một phần

		#region Cập nhật số tiền bán, thanh lý

		public virtual IActionResult ListCapNhatSoTienBanThanhLy(int? pageIndex = 0)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDCapNhatTien))
				return AccessDeniedView();

			//prepare model
			var searchModel = _itemModelFactory.PrepareYeuCauSearchModel(new YeuCauSearchModel());
			searchModel.Page = pageIndex ?? 0;
			return View(searchModel);
		}
		[HttpPost]
		public virtual IActionResult ListCapNhatSoTienBanThanhLy(TrungGianBDYCSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin))
				return AccessDeniedKendoGridJson();
			//prepare model
			if (searchModel.PageSize == 0) searchModel.PageSize = 10;
			searchModel.isIgnoreTraLai = true;
			searchModel.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
			searchModel.DON_VI_ID = _workContext.CurrentDonVi.ID;
			var listYeuCau = _trungGianBDYCModelFactory.PrepareListGiamBanThanhLy(searchModel);
			return Json(listYeuCau);
		}
		public virtual IActionResult UpdateSoTien(Guid guid, int BDorYC, int? pageIndex = null)
		{
			if (BDorYC == (int)enumBDorYC.BIEN_DONG && guid != Guid.Empty)
			{
				var bienDong = _bienDongService.GetBienDongByGuid(guid);
				var model = _trungGianBDYCModelFactory.PrepareBienDongModelGiamTaiSan(new TrungGianBDYCModel(), new TrungGianBDYC(bienDong));
				return View(model);
			}
			else if (BDorYC == (int)enumBDorYC.YEU_CAU && guid != Guid.Empty)
			{
				var yeuCau = _itemService.GetYeuCauByGuid(guid);
				var model = _trungGianBDYCModelFactory.PrepareBienDongModelGiamTaiSan(new TrungGianBDYCModel(), new TrungGianBDYC(yeuCau));
				return View(model);
			}

			return View();
		}
		[HttpPost]
		public virtual IActionResult UpdateSoTien(TrungGianBDYCModel trungGianBDYC)
		{
			if (ModelState.IsValid && trungGianBDYC != null && trungGianBDYC.TrungGianBDYCChiTietModel != null)
			{
				if (trungGianBDYC.BDorYC == (int)enumBDorYC.BIEN_DONG)
				{
					var bd = _bienDongService.GetBienDongByGuid(trungGianBDYC.GUID);
					if (bd != null)
					{
						bd.QUYET_DINH_SO = trungGianBDYC.QUYET_DINH_SO;
						bd.QUYET_DINH_NGAY = trungGianBDYC.QUYET_DINH_NGAY;
						_bienDongService.UpdateBienDong(bd);

						var bdct = _bienDongChiTietService.GetBienDongChiTietByBDId(bd.ID);

						bdct.HINH_THUC_XU_LY_ID = trungGianBDYC.TrungGianBDYCChiTietModel.HINH_THUC_XU_LY_ID;
						bdct.PHI_THU = trungGianBDYC.TrungGianBDYCChiTietModel.PHI_THU;
						bdct.IS_BAN_THANH_LY = true;
						bdct.PHI_BU_DAP = trungGianBDYC.TrungGianBDYCChiTietModel.PHI_BU_DAP;
						bdct.PHI_NOP_NGAN_SACH = trungGianBDYC.TrungGianBDYCChiTietModel.PHI_NOP_NGAN_SACH;
						bdct.NGAY_BAN_THANH_LY = trungGianBDYC.TrungGianBDYCChiTietModel.NGAY_BAN_THANH_LY;

						_bienDongChiTietService.UpdateBienDongChiTiet(bdct);

						//ghi log
						var bdModel = bd.ToModel<BienDongModel>();
						_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin biến động", bdModel, "BienDong");
						UpdateSessionSearchModel<TrungGianBDYCSearchModel>(true);
						return JsonSuccessMessage("Cập nhật thành công");
					}
				}
				else if (trungGianBDYC.BDorYC == (int)enumBDorYC.YEU_CAU)
				{
					var yc = _itemService.GetYeuCauByGuid(trungGianBDYC.GUID);
					if (yc != null)
					{
						yc.QUYET_DINH_SO = trungGianBDYC.QUYET_DINH_SO;
						yc.QUYET_DINH_NGAY = trungGianBDYC.QUYET_DINH_NGAY;
						_itemService.UpdateYeuCau(yc);

						var ycct = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(yc.ID);
						ycct.HINH_THUC_XU_LY_ID = trungGianBDYC.TrungGianBDYCChiTietModel.HINH_THUC_XU_LY_ID;
						ycct.PHI_THU = trungGianBDYC.TrungGianBDYCChiTietModel.PHI_THU;
						ycct.IS_BAN_THANH_LY = true;
						ycct.NGAY_BAN_THANH_LY = trungGianBDYC.TrungGianBDYCChiTietModel.NGAY_BAN_THANH_LY;
						ycct.PHI_BU_DAP = trungGianBDYC.TrungGianBDYCChiTietModel.PHI_BU_DAP;
						ycct.PHI_NOP_NGAN_SACH = trungGianBDYC.TrungGianBDYCChiTietModel.PHI_NOP_NGAN_SACH;
						_yeuCauChiTietService.UpdateYeuCauChiTiet(ycct);

						//ghi log 
						var ycModel = yc.ToModel<YeuCauModel>();
						_yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(ycModel, enumLOAI_NHATKY_YEUCAU.SUA);
						_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin biến động", ycModel, "YeuCau");
						UpdateSessionSearchModel<TrungGianBDYCSearchModel>(true);
						return JsonSuccessMessage("Cập nhật thành công");
					}
				}
			}
			var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			return JsonErrorMessage("Cập nhật thất bại", list);
		}
		#endregion Cập nhật số tiền bán, thanh lý

		#region Function

		private bool Insert_NV_YeuCau(YeuCauModel model)
		{

			var itemTaiSan = _taiSanService.GetTaiSanById(model.TAI_SAN_ID);
			var itemYeuCau = new YeuCau();
			var itemYeuCauCT = new YeuCauChiTiet();

			_itemModelFactory.PrepareThongTinChungTS(model, itemTaiSan);
			itemYeuCau = model.ToEntity<YeuCau>(); itemYeuCau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
			//Thông tin yêu cầu chi tiết
			itemYeuCauCT = model.YCCTModel.ToEntity<YeuCauChiTiet>();
			var hientrangList = new HienTrangList()
			{
				TaiSanId = itemTaiSan.ID,
				lstObjHienTrang = model.YCCTModel.lstHienTrang,
			};
			itemYeuCauCT.HTSD_JSON = hientrangList.toStringJson();
			if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
			{
				foreach (var item in model.lstNguonVonModel)
					item.GiaTri *= -1;
				SetGiaTriChoBienDongGiamGiaTri(yeuCau: itemYeuCau, yeuCauChiTiet: itemYeuCauCT);
			}
			itemYeuCau.NGUON_VON_JSON = model.lstNguonVonModel.toStringJson();
			itemYeuCau.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
			_itemService.InsertYeuCau(itemYeuCau);
			//Thông tin yêu cầu chi tiết
			itemYeuCauCT.YEU_CAU_ID = itemYeuCau.ID;
			model.ID = itemYeuCau.ID;
			itemYeuCauCT.DATA_JSON = model.toStringJson();
			if (itemYeuCauCT.HM_GIA_TRI_CON_LAI == null)
			{
				var ycTruoc = _itemService.GetYeuCauMoiNhatByTaiSanId(itemTaiSan.ID, enumTRANG_THAI_YEU_CAU.CHO_DUYET, null, itemYeuCau.NGAY_BIEN_DONG);
				if (ycTruoc != null)
				{
					var ycctTruoc = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(ycTruoc.ID);
					itemYeuCauCT.HM_GIA_TRI_CON_LAI = ycctTruoc != null ? ycctTruoc.HM_GIA_TRI_CON_LAI : null;
				}
				else
				{
					var bdTruoc = _bienDongService.GetBienDongCuoiByTaiSanId(itemTaiSan.ID);
					if (bdTruoc != null)
					{
						var bdctTruoc = _bienDongChiTietService.GetBienDongChiTietByBDId(bdTruoc.ID);
						itemYeuCauCT.HM_GIA_TRI_CON_LAI = bdctTruoc != null ? bdctTruoc.HM_GIA_TRI_CON_LAI : null;
					}

				}
			}
			_yeuCauChiTietService.InsertYeuCauChiTiet(itemYeuCauCT);
			//duyệt tự động tài sản thay đổi địa bàn
			if (model.IsCapNhatThongTinDiaBan)
			{
				_thaoTacBienDongModelFactory.DuyetYeuCauFunc(itemYeuCau.ID, itemYeuCau);
				_yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(model, enumLOAI_NHATKY_YEUCAU.THEM_MOI);
				_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Cập nhật địa chỉ tài sản đất, nhà", itemYeuCau.ToModel<YeuCauModel>(), "YeuCau");
				return true;
			}
			if (model.IsCapNhatSoChoNgoiOto)
			{
				_thaoTacBienDongModelFactory.DuyetYeuCauFunc(itemYeuCau.ID, itemYeuCau);
				_yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(model, enumLOAI_NHATKY_YEUCAU.THEM_MOI);
				_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Cập nhật số chỗ ngồi ô tô", itemYeuCau.ToModel<YeuCauModel>(), "YeuCau");
				return true;
			}
			var taiSan = _taiSanService.GetTaiSanById(itemYeuCau.TAI_SAN_ID);
			if (_taiSanModelFactory.CheckAutoDuyet(taiSan.LOAI_HINH_TAI_SAN_ID, taiSan.NGUYEN_GIA_BAN_DAU))
				
            
			//ghi log
			_yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(model, enumLOAI_NHATKY_YEUCAU.THEM_MOI);
			_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin biến động", itemYeuCau.ToModel<YeuCauModel>(), "YeuCau");
			return true;
		}
		[HttpPost]
		public IActionResult ThayDoiThongTinDiaBan(YeuCauModel model)
		{

			//if (_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
			//	|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDTangNguyenGia)
			//	|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamNguyenGia)
			//	|| _quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan))
			//	return AccessDeniedView();
			// Trường hợp auto duyệt thay đổi thông tin địa bàn thì phải check yêu cầu có hợp lệ không mới auto duyệt được
			if (model.IsCapNhatThongTinDiaBan)
			{
				ValidateListHienTrang(model);
				ValidateYeuCauAuToDuyet(model);
			}

			if (ModelState.IsValid)
			{
				TaiSan taiSanItem = _taiSanService.GetTaiSanById(model.TAI_SAN_ID);
				//Prepare thông tin chung của tài sản vào yêu cầu
				//Thay đổi thông tin đất thì update lại thông tin đất theo địa chỉ vs địa bàn
				if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT && model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
				{
					model.TAI_SAN_TEN = model.YCCTModel.DIA_CHI;
					var tinhItem = _diaBanService.GetDiaBanById(model.YCCTModel.DatTinhId ?? 0);
					var huyenItem = _diaBanService.GetDiaBanById(model.YCCTModel.DatHuyenId ?? 0);
					var xaItem = _diaBanService.GetDiaBanById(model.YCCTModel.DatXaId ?? 0);
					if (xaItem != null)
						model.TAI_SAN_TEN = model.TAI_SAN_TEN + ", " + xaItem.TEN;
					if (huyenItem != null)
						model.TAI_SAN_TEN = model.TAI_SAN_TEN + ", " + huyenItem.TEN;
					if (tinhItem != null)
						model.TAI_SAN_TEN = model.TAI_SAN_TEN + ", " + tinhItem.TEN;
				}
				if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA && (model.taisanNhaModel?.TAI_SAN_DAT_ID ?? 0) <= 0 && model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
				{
					// Lưu ý: TaiSanNha,ycct.NHA_DIA_CHI, bdct.NHA_DIA_CHI  sẽ lưu Địa chỉ gốc còn ycct.DIA_CHI, bdct.DIA_CHI sẽ lưu địa chỉ đầy đủ phục vụ báo cáo dễ dàng hơn
					var tinhItem = _diaBanService.GetDiaBanById(model.YCCTModel.NHA_TinhId ?? 0);
					var huyenItem = _diaBanService.GetDiaBanById(model.YCCTModel.NHA_HuyenId ?? 0);
					var xaItem = _diaBanService.GetDiaBanById(model.YCCTModel.NHA_XaId ?? 0);
					model.YCCTModel.NHA_DIA_CHI = model.YCCTModel.DIA_CHI;
					if (xaItem != null)
						model.YCCTModel.DIA_CHI = model.YCCTModel.DIA_CHI + ", " + xaItem.TEN;
					if (huyenItem != null)
						model.YCCTModel.DIA_CHI = model.YCCTModel.DIA_CHI + ", " + huyenItem.TEN;
					if (tinhItem != null)
						model.YCCTModel.DIA_CHI = model.YCCTModel.DIA_CHI + ", " + tinhItem.TEN;
				}
			  var result = CapNhatThongTinDiaBanAndHienTrang(model);
                if (!result)
                {
					return JsonErrorMessage("Có lỗi xảy ra ");
				}
				return JsonSuccessMessage("Thêm mới biến động thành công", model);
				
			}
			//----
			var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			return JsonErrorMessage("Error", list);
		}
		[HttpPost]
		public IActionResult ThayDoiThongTinSoChoNgoiOto(YeuCauModel model)
        {
			if (model.IsCapNhatSoChoNgoiOto)
			{
				ValidateListHienTrang(model);
				ValidateYeuCauAuToDuyet(model);
			}

			if (ModelState.IsValid)
			{
				TaiSan taiSanItem = _taiSanService.GetTaiSanById(model.TAI_SAN_ID);
				//Prepare thông tin chung của tài sản vào yêu cầu
				//Thay đổi thông tin đất thì update lại thông tin đất theo địa chỉ vs địa bàn
				var result = CapNhatThongTinSoChoNgoiOto(model);
				if (!result)
				{
					return JsonErrorMessage("Có lỗi xảy ra ");
				}
				return JsonSuccessMessage("Thêm mới biến động thành công", model);

			}
			//----
			var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			return JsonErrorMessage("Error", list);
		}

		private bool CapNhatThongTinDiaBanAndHienTrang(YeuCauModel model) 
		{
			var itemTaiSan = _taiSanService.GetTaiSanById(model.TAI_SAN_ID);
			var itemYeuCau = new YeuCau();
			var itemYeuCauCT = new YeuCauChiTiet();

			itemYeuCau = model.ToEntity<YeuCau>(); itemYeuCau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
			//Thông tin yêu cầu chi tiết
			itemYeuCauCT = model.YCCTModel.ToEntity<YeuCauChiTiet>();
			var hientrangList = new HienTrangList()
			{
				TaiSanId = itemTaiSan.ID,
				lstObjHienTrang = model.YCCTModel.lstHienTrang,
			};
			itemYeuCauCT.HTSD_JSON = hientrangList.toStringJson();
			itemYeuCau.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
			_itemService.InsertYeuCau(itemYeuCau);
			//Thông tin yêu cầu chi tiết
			itemYeuCauCT.YEU_CAU_ID = itemYeuCau.ID;
			model.ID = itemYeuCau.ID;
			itemYeuCauCT.DATA_JSON = model.toStringJson();
			_yeuCauChiTietService.InsertYeuCauChiTiet(itemYeuCauCT);
			//duyệt tự động tài sản thay đổi địa bàn

			_thaoTacBienDongModelFactory.CapNhatTaiSanThayDoiDiaBanHienTrang(itemYeuCau.ID, itemYeuCau);
			_yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(model, enumLOAI_NHATKY_YEUCAU.THEM_MOI);
			_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Cập nhật địa chỉ tài sản đất, nhà", itemYeuCau.ToModel<YeuCauModel>(), "YeuCau");
			return true;
		}

		private bool CapNhatThongTinSoChoNgoiOto(YeuCauModel model)
        {
			var itemTaiSan = _taiSanService.GetTaiSanById(model.TAI_SAN_ID);
			var itemYeuCau = new YeuCau();
			var itemYeuCauCT = new YeuCauChiTiet();

			itemYeuCau = model.ToEntity<YeuCau>(); itemYeuCau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
			//Thông tin yêu cầu chi tiết
			itemYeuCauCT = model.YCCTModel.ToEntity<YeuCauChiTiet>();
			itemYeuCau.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
			_itemService.InsertYeuCau(itemYeuCau);
			//Thông tin yêu cầu chi tiết
			itemYeuCauCT.YEU_CAU_ID = itemYeuCau.ID;
			model.ID = itemYeuCau.ID;
			itemYeuCauCT.DATA_JSON = model.toStringJson();
			_yeuCauChiTietService.InsertYeuCauChiTiet(itemYeuCauCT);
			//duyệt tự động tài sản thay đổi địa bàn

			_thaoTacBienDongModelFactory.CapNhatSoChoNgoiOto(itemYeuCau.ID, itemYeuCau);
			_yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(model, enumLOAI_NHATKY_YEUCAU.THEM_MOI);
			_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Cập nhật số chỗ ngồi ô tô", itemYeuCau.ToModel<YeuCauModel>(), "YeuCau");
			return true;
		}

		private bool UpdateBD_NV_YeuCau(YeuCauModel model)
		{
			var itemTaiSan = _taiSanService.GetTaiSanById(model.TAI_SAN_ID);
			var itemYeuCau = new YeuCau();
			var itemYeuCauCT = new YeuCauChiTiet();

			itemYeuCau = _itemService.GetYeuCauById(model.ID);
			itemYeuCauCT = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(model.ID);
			itemYeuCau = _itemModelFactory.PrepareYeuCauForBDEdit(model: model, item: itemYeuCau);

			
			itemYeuCauCT = _yeuCauChiTietModelFactory.PrepareYCCTForBDEdit(model: model.YCCTModel, item: itemYeuCauCT);
			var hientrangList = new HienTrangList()
			{
				TaiSanId = itemTaiSan.ID,
				lstObjHienTrang = model.YCCTModel.lstHienTrang,
			};
			itemYeuCauCT.HTSD_JSON = hientrangList.toStringJson();
			if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
			{
				foreach (var item in model.lstNguonVonModel)
					item.GiaTri *= -1;
				SetGiaTriChoBienDongGiamGiaTri(yeuCau: itemYeuCau, yeuCauChiTiet: itemYeuCauCT);
			}
			itemYeuCau.NGUON_VON_JSON = model.lstNguonVonModel.toStringJson();
			if (itemYeuCau.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.TU_CHOI)
				itemYeuCau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
			_itemService.UpdateYeuCau(itemYeuCau);
			model.ID = itemYeuCau.ID;
			itemYeuCauCT.DATA_JSON = model.toStringJson();
			_yeuCauChiTietService.UpdateYeuCauChiTiet(itemYeuCauCT);
			//duyệt tự động
			var taiSan = _taiSanService.GetTaiSanById(itemYeuCau.TAI_SAN_ID);
			if (_taiSanModelFactory.CheckAutoDuyet(taiSan.LOAI_HINH_TAI_SAN_ID, taiSan.NGUYEN_GIA_BAN_DAU))
				_thaoTacBienDongModelFactory.DuyetYeuCauFunc(itemYeuCau.ID, itemYeuCau);

			//ghi log
			_yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(model, enumLOAI_NHATKY_YEUCAU.SUA);
			_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin biến động", itemYeuCau.ToModel<YeuCauModel>(), "YeuCau");
			UpdateSessionSearchModel<TrungGianBDYCSearchModel>(true);
			return true;
		}
		private void SetGiaTriChoBienDongGiamGiaTri(YeuCau yeuCau, YeuCauChiTiet yeuCauChiTiet)
		{
			yeuCau.NGUYEN_GIA = yeuCau.NGUYEN_GIA * -1;
			yeuCauChiTiet.NGUYEN_GIA = yeuCau.NGUYEN_GIA ;
			yeuCauChiTiet.NHA_DIEN_TICH_XD = yeuCauChiTiet.NHA_DIEN_TICH_XD * -1;
			yeuCauChiTiet.NHA_TONG_DIEN_TICH_XD = yeuCauChiTiet.NHA_TONG_DIEN_TICH_XD * -1;
			yeuCauChiTiet.DAT_TONG_DIEN_TICH = yeuCauChiTiet.DAT_TONG_DIEN_TICH * -1;
			yeuCauChiTiet.VKT_CHIEU_DAI = yeuCauChiTiet.VKT_CHIEU_DAI * -1;
			yeuCauChiTiet.VKT_DIEN_TICH = yeuCauChiTiet.VKT_DIEN_TICH * -1;
			yeuCauChiTiet.VKT_THE_TICH = yeuCauChiTiet.VKT_THE_TICH * -1;
		}
		#endregion Function

		#region DeniedView

		public IActionResult InsertEditDeniedView(Guid yeuCauGuid)
		{
			InsertEditDeniedModel model = new InsertEditDeniedModel();
			if (yeuCauGuid != Guid.Empty)
			{
				YeuCau yc = _itemService.GetYeuCauByGuid(yeuCauGuid);
				YeuCauModel yeuCauModel = yc.ToModel<YeuCauModel>();
				if (yc != null)
				{
					model.TEN_BIEN_DONG = yeuCauModel.TenLyDoBienDong;
					model.MA_TAI_SAN = yeuCauModel.TAI_SAN_MA;
					model.TEN_TAI_SAN = yeuCauModel.TAI_SAN_TEN;
					YeuCau ycNeedToEdit = _itemService.GetYCNeedToEdit(yc.TAI_SAN_ID);
					if (ycNeedToEdit != null)
					{
						var ycNeedToEditModel = ycNeedToEdit.ToModel<YeuCauModel>();
						model.TEN_BIEN_DONG_CAN_SUA = ycNeedToEditModel.TenLyDoBienDong;

						switch (ycNeedToEditModel.LOAI_BIEN_DONG_ID)
						{
							case (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO:
								model.RedirectUrl = Url.Action("Edit", "TaiSan", new { guid = ycNeedToEditModel.TaiSanGuid });
								break;

							case (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI:
								model.RedirectUrl = Url.Action("CreateOrUpdateTangNguyenGia", "YeuCau", new { guidYC = ycNeedToEditModel.GUID });
								break;

							case (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI:
								model.RedirectUrl = Url.Action("CreateOrUpdateGiamNguyenGia", "YeuCau", new { guidYC = ycNeedToEditModel.GUID });
								break;

							case (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO:
								model.RedirectUrl = Url.Action("GiamTaiSan", "YeuCau", new { guidYC = ycNeedToEditModel.GUID });
								break;

							case (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN:
								model.RedirectUrl = Url.Action("UpdateDieuChuyenTungPhan", "YeuCau", new { guid = ycNeedToEditModel.GUID });
								break;

							case (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN:
								model.RedirectUrl = Url.Action("UpdateThayDoiThongTin", "ThayDoiThongTin", new { guid = ycNeedToEditModel.GUID });
								break;

							default:
								break;
						}
					}
				}
			}
			return View(model);
		}

		#endregion DeniedView
	}
}