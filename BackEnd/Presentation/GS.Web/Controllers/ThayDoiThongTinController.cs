using GS.Core;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.NghiepVu;
using GS.Services.Security;
using GS.Services.TaiSans;
using GS.Web.Factories.BienDongs;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.NghiepVu;
using GS.Web.Factories.TaiSans;
using GS.Web.Models.BienDongs;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Controllers
{
	public partial class ThayDoiThongTinController : BaseWorksController
	{
		#region Fields

		private readonly IHoatDongService _hoatdongService;
		private readonly INhanHienThiService _nhanHienThiService;
		private readonly IQuyenService _quyenService;
		private readonly IWorkContext _workContext;
		private readonly CauHinhChung _cauhinhChung;
		private readonly ITaiSanModelFactory _itemModelFactory;
		private readonly ITaiSanService _itemService;
		private readonly ITaiSanDatModelFactory _taisandatModelFactory;
		private readonly ITaiSanDatService _taisandatService;
		private readonly IDiaBanModelFactory _diabanModelFactory;
		private readonly ILoaiTaiSanModelFactory _loaitaisanModelFactory;
		private readonly ILoaiTaiSanService _loaitaisanService;
		private readonly ICheDoHaoMonService _chedohaomonService;
		private readonly ILyDoBienDongService _lydobiendongService;
		private readonly ILyDoBienDongModelFactory _lydobiendongModelFactory;
		private readonly ITaiSanNguonVonService _taisannguonvonService;
		private readonly INguonVonService _nguonvonService;
		private readonly INguonVonModelFactory _nguonvonModelFactory;
		private readonly IHienTrangModelFactory _hienTrangModelFactory;
		private readonly ITaiSanMayMocModelFactory _taiSanMayMocModelFactory;
		private readonly ITaiSanNhaModelFactory _taiSanNhaModelFactory;
		private readonly ITaiSanOtoModelFactory _taiSanOtoModelFactory;
		private readonly IYeuCauChiTietModelFactory _yeuCauChiTietModelFactory;
		private readonly IYeuCauModelFactory _yeucauModelFactory;
		private readonly IYeuCauService _yeucauService;
		private readonly ITaiSanVktModelFactory _taiSanVktModelFactory;
		private readonly ITaiSanClnModelFactory _taiSanClnModelFactory;
		private readonly ITaiSanNhaService _taisannhaService;
		private readonly ITaiSanOtoService _taisanOtoService;
		private readonly ITaiSanClnService _taisanClnService;
		private readonly ITaiSanMayMocService _taisanmaymocService;
		private readonly ITaiSanVktService _taisanVKTService;
		private readonly IYeuCauChiTietService _yeucauchitietService;
		private readonly IHienTrangService _hientrangService;
		private readonly IBienDongService _biendongService;
		private readonly IBienDongChiTietService _biendongchitietService;
		private readonly IDonViService _donviService;
		private readonly IYeuCauNhatKyModelFactory _yeucaunhatkyModelFactory;
		private readonly IYeuCauNhatKyService _yeucaunhatkyService;
		private readonly IDiaBanService _diaBanService;
		private readonly IBienDongService _bienDongService;
		private readonly ITrungGianBDYCModelFactory _trungGianBDYCModelFactory;

		#endregion Fields

		#region Ctor

		public ThayDoiThongTinController(
			IHoatDongService hoatdongService,
			INhanHienThiService nhanHienThiService,
			IQuyenService quyenService,
			IWorkContext workContext,
			CauHinhChung cauhinhChung,
			ITaiSanModelFactory itemModelFactory,
			ITaiSanService itemService,
			ITaiSanDatModelFactory taisandatModelFactory,
			ITaiSanDatService taisandatService,
			IDiaBanModelFactory diabanModelFactory,
			ILoaiTaiSanModelFactory loaitaisanModelFactory,
			ICheDoHaoMonService chedohaomonService,
			ILyDoBienDongService lydobiendongService,
			ILoaiTaiSanService loaitaisanService,
			ILyDoBienDongModelFactory lydobiendongModelFactory,
			ITaiSanNguonVonService taisannguonvonService,
			INguonVonService nguonvonService,
			INguonVonModelFactory nguonvonModelFactory,
			IHienTrangModelFactory hienTrangModelFactory,
			ITaiSanMayMocModelFactory taiSanMayMocModelFactory,
			ITaiSanOtoModelFactory taiSanOtoModelFactory,
			IYeuCauChiTietModelFactory yeuCauChiTietModelFactory,
			ITaiSanNhaModelFactory taiSanNhaModelFactory,
			IYeuCauModelFactory yeucauModelFactory,
			IYeuCauService yeucauService,
			ITaiSanNhaService taisannhaService,
			ITaiSanOtoService taisanOtoService,
			ITaiSanClnService taisanClnService,
			ITaiSanMayMocService taisanmaymocService,
			ITaiSanVktService taisanVKTService,
			IYeuCauChiTietService yeucauchitietService,
			ITaiSanClnModelFactory taiSanClnModelFactory,
			ITaiSanVktModelFactory taiSanVktModelFactory,
			IHienTrangService hientrangService,
			IBienDongService biendongService,
			IBienDongChiTietService biendongchitietService,
			IDonViService donviService,
			IYeuCauNhatKyModelFactory yeucaunhatkyModelFactory,
			IYeuCauNhatKyService yeucaunhatkyService,
			IDiaBanService diaBanService,
			IBienDongService bienDongService,
			ITrungGianBDYCModelFactory trungGianBDYCModelFactory
			)
		{
			this._hoatdongService = hoatdongService;
			this._nhanHienThiService = nhanHienThiService;
			this._quyenService = quyenService;
			this._workContext = workContext;
			this._cauhinhChung = cauhinhChung;
			this._taisandatModelFactory = taisandatModelFactory;
			this._taisandatService = taisandatService;
			this._diabanModelFactory = diabanModelFactory;
			this._loaitaisanModelFactory = loaitaisanModelFactory;
			this._loaitaisanService = loaitaisanService;
			this._itemModelFactory = itemModelFactory;
			this._itemService = itemService;
			this._chedohaomonService = chedohaomonService;
			this._lydobiendongService = lydobiendongService;
			this._lydobiendongModelFactory = lydobiendongModelFactory;
			this._taisannguonvonService = taisannguonvonService;
			this._nguonvonService = nguonvonService;
			this._nguonvonModelFactory = nguonvonModelFactory;
			this._hienTrangModelFactory = hienTrangModelFactory;
			this._taiSanMayMocModelFactory = taiSanMayMocModelFactory;
			this._taiSanOtoModelFactory = taiSanOtoModelFactory;
			this._yeuCauChiTietModelFactory = yeuCauChiTietModelFactory;
			this._taiSanNhaModelFactory = taiSanNhaModelFactory;
			this._yeucauModelFactory = yeucauModelFactory;
			this._yeucauService = yeucauService;
			this._taiSanClnModelFactory = taiSanClnModelFactory;
			this._taisannhaService = taisannhaService;
			this._taisanOtoService = taisanOtoService;
			this._taisanClnService = taisanClnService;
			this._taisanmaymocService = taisanmaymocService;
			this._taisanVKTService = taisanVKTService;
			this._yeucauchitietService = yeucauchitietService;
			this._taiSanVktModelFactory = taiSanVktModelFactory;
			this._hientrangService = hientrangService;
			this._biendongService = biendongService;
			this._biendongchitietService = biendongchitietService;
			this._donviService = donviService;
			this._yeucaunhatkyModelFactory = yeucaunhatkyModelFactory;
			this._yeucaunhatkyService = yeucaunhatkyService;
			this._diaBanService = diaBanService;
			this._bienDongService = bienDongService;
			this._trungGianBDYCModelFactory = trungGianBDYCModelFactory;
		}

		#endregion Ctor

		#region Methods

		/// <summary>
		/// Biến động thay đổi thông tin tài sản
		/// </summary>
		/// <param name="guidTS"></param>
		/// <returns></returns>

		public virtual IActionResult CreateThayDoiThongTin(Guid guidTS)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin))
				return AccessDeniedView();
			//try to get a store with the specified guid
			var taiSanItem = _itemService.GetTaiSanByGuId(guidTS);
			if (taiSanItem == null)
				return RedirectToAction("ListBienDongTaiSan", (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN);
			var yeuCauModel = new YeuCauModel();
			yeuCauModel.TaiSanGuid = taiSanItem.GUID;
			yeuCauModel.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN;
			yeuCauModel.NGAY_SU_DUNG = taiSanItem.NGAY_SU_DUNG;
			yeuCauModel = _yeucauModelFactory.PrepareYeuCauModelFromBienDong(model: yeuCauModel, taiSan: taiSanItem, item: null);
			if (taiSanItem.DU_AN_ID.HasValue && taiSanItem.DU_AN_ID > 0)
			{
				yeuCauModel.DU_AN_ID = taiSanItem.DU_AN_ID;
				yeuCauModel.IsTaiSanDuAn = true;
			}
			else
			{
				yeuCauModel.IsTaiSanDuAn = false;
			}
			return View(yeuCauModel);
		}
		public virtual IActionResult ThayDoiThongTinDiaBan(Guid guidTS)
		{
			var taiSanItem = _itemService.GetTaiSanByGuId(guidTS);
			if (taiSanItem == null)
				return RedirectToAction("TaiSan", "ListTaiSanThayDoiDiaBan");
			var yeuCauModel = new YeuCauModel();
			yeuCauModel.TaiSanGuid = taiSanItem.GUID;
			yeuCauModel.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN;
			yeuCauModel.NGAY_SU_DUNG = taiSanItem.NGAY_SU_DUNG;
			yeuCauModel = _yeucauModelFactory.PrepareYeuCauModelFromBienDong(model: yeuCauModel, taiSan: taiSanItem, item: null);
			if (taiSanItem.DU_AN_ID.HasValue && taiSanItem.DU_AN_ID > 0)
			{
				yeuCauModel.DU_AN_ID = taiSanItem.DU_AN_ID;
				yeuCauModel.IsTaiSanDuAn = true;
			}
			else
			{
				yeuCauModel.IsTaiSanDuAn = false;
			}
			// lấy đơn vị của chính tài sản đó
			yeuCauModel.DON_VI_ID = taiSanItem.DON_VI_ID;
			yeuCauModel.IsCapNhatThongTinDiaBan = true;
			var listHienTrang = yeuCauModel.YCCTModel.lstHienTrang;
			// Nếu list hiện trạng tồn tại hiện trạng bị nhập sai thì hiển thị hiện trạng để sửa
			if (_hienTrangModelFactory.CheckIsListHienTrangNhapSai(taiSanItem.DON_VI_ID ??0, listHienTrang))
            {
				yeuCauModel.IsShowHienTrang = true;

			}

			return View(yeuCauModel);
		}

		public virtual IActionResult ThayDoiThongTinSoChoNgoiOto(Guid guidTS)
		{
			var taiSanItem = _itemService.GetTaiSanByGuId(guidTS);
			if (taiSanItem == null)
				return RedirectToAction("TaiSan", "ListTaiSanOtoSaiSoChoNgoi");
			var yeuCauModel = new YeuCauModel();
			yeuCauModel.TaiSanGuid = taiSanItem.GUID;
			yeuCauModel.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN;
			yeuCauModel.NGAY_SU_DUNG = taiSanItem.NGAY_SU_DUNG;
			yeuCauModel = _yeucauModelFactory.PrepareYeuCauModelFromBienDong(model: yeuCauModel, taiSan: taiSanItem, item: null);
			if (taiSanItem.DU_AN_ID.HasValue && taiSanItem.DU_AN_ID > 0)
			{
				yeuCauModel.DU_AN_ID = taiSanItem.DU_AN_ID;
				yeuCauModel.IsTaiSanDuAn = true;
			}
			else
			{
				yeuCauModel.IsTaiSanDuAn = false;
			}
			// lấy đơn vị của chính tài sản đó
			yeuCauModel.DON_VI_ID = taiSanItem.DON_VI_ID;
			yeuCauModel.IsCapNhatNguyenGia = true;

			return View(yeuCauModel);
		}

		public virtual IActionResult _ThayDoiThongTinNguyenGia(Guid guid)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin))
				return AccessDeniedView();
			var taiSanItem = _itemService.GetTaiSanByGuId(guid);
			var yeuCauModel = new YeuCauModel();
			yeuCauModel.TaiSanGuid = taiSanItem.GUID;
			yeuCauModel.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN;
			yeuCauModel.NGAY_SU_DUNG = taiSanItem.NGAY_SU_DUNG;
			yeuCauModel = _yeucauModelFactory.PrepareYeuCauModelFromBienDong(model: yeuCauModel, taiSan: taiSanItem, item: null);
			// lấy đơn vị của chính tài sản đó
			yeuCauModel.DON_VI_ID = taiSanItem.DON_VI_ID;
			yeuCauModel.IsCapNhatSoChoNgoiOto = true;

			TrungGianBDYC trungGianBDYC = null;  
			trungGianBDYC = new TrungGianBDYC(_bienDongService.GetBienDongCuNhatByTaiSanId(yeuCauModel.TAI_SAN_ID));
			var modelTrungGian = _trungGianBDYCModelFactory.PrepareTrungGianBDYCModel(new TrungGianBDYCModel(), trungGianBDYC, true);
			yeuCauModel.lstNguonVonModel = modelTrungGian.lstNguonVonModel;
			yeuCauModel.NGUYEN_GIA = modelTrungGian.NGUYEN_GIA;

			//return PartialView(model);

			return PartialView(yeuCauModel);
		} 
		[HttpPost]
		public virtual IActionResult ThayDoiThongTinNguyenGia(NguonVonModel[] nguons, decimal? nguyengia, decimal taiSanId)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin))
				return AccessDeniedView();
            try
            {
				BienDongChiTiet bienDongChiTiet = new BienDongChiTiet();
				YeuCau nvYeuCau = new YeuCau();
				YeuCauChiTiet nvYeuCauChiTiet = new YeuCauChiTiet();

				var taiSan = _itemService.GetTaiSanById(taiSanId);
				taiSan.NGUYEN_GIA_BAN_DAU = nguyengia;
				var bienDong = _biendongService.GetBienDongCuNhatByTaiSanId(taiSanId);
				if (bienDong != null)
				{
					bienDong.NGUYEN_GIA = nguyengia;
					//_biendongService.UpdateBienDong(bienDong);
					bienDongChiTiet = _biendongchitietService.GetBienDongChiTietByBDId(bienDong.ID);
					if (bienDongChiTiet != null)
					{
						bienDongChiTiet.NGUYEN_GIA = nguyengia;
						//_biendongchitietService.UpdateBienDongChiTiet(bienDongChiTiet);
					}
				}
				else
				{
					nvYeuCau = _yeucauService.GetYeuCauCuNhatByTSId(taiSanId);
					if (nvYeuCau != null)
					{
						nvYeuCau.NGUYEN_GIA = nguyengia;
						//_yeucauService.UpdateYeuCau(nvYeuCau);
						nvYeuCauChiTiet = _yeucauchitietService.GetYeuCauChiTietByYeuCauId(nvYeuCau.ID);
						if (nvYeuCauChiTiet != null)
						{
							nvYeuCauChiTiet.NGUYEN_GIA = nguyengia;
							//_yeucauchitietService.UpdateYeuCauChiTiet(nvYeuCauChiTiet);
						}
					}
				}
				List<TaiSanNguonVon> lstTSNguonVon = new List<TaiSanNguonVon>();
				foreach (var nguon in nguons)
				{
					var taiSanNguonVon = _taisannguonvonService.GetTaiSanNguonVonByTaiSanIdFirst(taiSanId, nguon.ID);
					if (taiSanNguonVon != null)
					{
						taiSanNguonVon.GIA_TRI = Convert.ToDecimal(nguon.GiaTri);
						lstTSNguonVon.Add(taiSanNguonVon);
						//_taisannguonvonService.UpdateTaiSanNguonVon(taiSanNguonVon);
					}
				}

				_itemService.UpdateTaiSan(taiSan);
				if (bienDong != null)
				{
					_biendongService.UpdateBienDong(bienDong);
					if (bienDongChiTiet != null)
					{
						_biendongchitietService.UpdateBienDongChiTiet(bienDongChiTiet);
					}
				}
				else
				{
					if (nvYeuCau != null)
					{
						_yeucauService.UpdateYeuCau(nvYeuCau);
						if (nvYeuCauChiTiet != null)
						{
							_yeucauchitietService.UpdateYeuCauChiTiet(nvYeuCauChiTiet);
						}
					}
				}
				foreach(var TSNguonVon in lstTSNguonVon)
                {
					_taisannguonvonService.UpdateTaiSanNguonVon(TSNguonVon);
				}					
				return JsonSuccessMessage("Cập nhật nguyên giá thành công");
			}
			catch (Exception exc)
			{
				ErrorNotification(exc);
				return JsonErrorMessage("Cập nhật nguyên giá thất bại");
			}
		}

		public virtual IActionResult UpdateThayDoiThongTin(Guid guid, int? pageIndex = 0)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin))
				return AccessDeniedView();
			if (!_yeucauService.IsYCNeedToEdit(guid))
				return RedirectToAction("InsertEditDeniedView","YeuCau", new { yeuCauGuid = guid });
			var yeuCauItem = _yeucauService.GetYeuCauByGuid(guid);
			if (yeuCauItem == null)
				return RedirectToAction("ListBienDongTaiSan", (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN);
			var taiSanItem = _itemService.GetTaiSanById(yeuCauItem.TAI_SAN_ID);
			if (taiSanItem == null)
				return RedirectToAction("ListBienDongTaiSan", (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN);
			var yeuCauModel = _yeucauModelFactory.PrepareYeuCauModelFromBienDong(model: new YeuCauModel(), taiSan: taiSanItem, item: yeuCauItem);
			yeuCauModel.pageIndex = pageIndex;
			if (taiSanItem.DU_AN_ID.HasValue && taiSanItem.DU_AN_ID > 0)
			{
				yeuCauModel.DU_AN_ID = taiSanItem.DU_AN_ID;
				yeuCauModel.IsTaiSanDuAn = true;
			}
			else
			{
				yeuCauModel.IsTaiSanDuAn = false;
			}
			yeuCauModel.YCCTModel.lstHienTrang = yeuCauModel.YCCTModel.lstHienTrang.Select(c =>
			{
				var donViCheck = Convert.ToDecimal(taiSanItem.DON_VI_ID);
				var checkHienTrang = !(_hientrangService.CheckHienTrangTheoLoaiDonVi(donViCheck, Convert.ToDecimal(c.HienTrangId)));
				if (c.HienTrangId == 99 && checkHienTrang)
				{
					c.GiaTriCheckbox = false;
				}
				return c;
			}).ToList();
			return View(yeuCauModel);
		}

		public virtual IActionResult _ThayDoiThongTinTaiSanOto(decimal taiSanId, decimal YeuCauID)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin))
				return AccessDeniedView();
			var model = new TaiSanOtoModel();
			var item = _taisanOtoService.GetTaiSanOtoByTaiSanId(taiSanId);
			model = _taiSanOtoModelFactory.PrepareTaiSanOtoModel(model: model,item: item,isTangMoi:false);

			var ycct = _yeucauchitietService.GetYeuCauChiTietByYeuCauId(YeuCauID);
			if (YeuCauID > 0 && ycct != null)
			{
				model.CHUC_DANH_ID = ycct.OTO_CHUC_DANH_ID;
				model.BIEN_KIEM_SOAT = ycct.OTO_BIEN_KIEM_SOAT;
				model.CONG_XUAT = ycct.OTO_CONG_XUAT;
				model.NHAN_XE_ID = ycct.OTO_NHAN_XE_ID;
				model.SO_CHO_NGOI = ycct.OTO_SO_CHO_NGOI;
				model.SO_CAU_XE = ycct.OTO_SO_CAU_XE;
				model.TAI_TRONG = ycct.OTO_TAI_TRONG;
			}
			return PartialView(model);
		}

		public virtual IActionResult _ThayDoiThongTinTaiSanDat(decimal taiSanId, decimal YeuCauID, DateTime? ngayBienDong = null)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin))
				return AccessDeniedView();
			var item = _taisandatService.GetTaiSanDatByTaiSanId(taiSanId);
			var model = _taisandatModelFactory.PrepareTaiSanDatModel(model: new TaiSanDatModel(), item: item, isTangMoi: false);
			var ycct = _yeucauchitietService.GetYeuCauChiTietByYeuCauId(YeuCauID);
			var kq = _bienDongService.Tinh_GiaTriTaiSan(model.TAI_SAN_ID, ngayBienDong??DateTime.Now);
            model.DAT_DIEN_TICH = kq.DAT_TONG_DIEN_TICH_CU;
			if (YeuCauID > 0 && ycct != null)
			{
				model.DIA_BAN_ID = (int)(ycct.DIA_BAN_ID ?? 0);
				if (model.DIA_BAN_ID > 0)
				{
					var diaban = _diaBanService.GetDiaBanById(model.DIA_BAN_ID ?? 0);
                    var diaBanCha = _diaBanService.GetDiaBanById(diaban.PARENT_ID ?? 0);
                    var diaBanChaParentId = diaBanCha != null ? (decimal?)diaBanCha.PARENT_ID : null;
                    model.TinhId = (int)diaBanChaParentId;
                    model.AvailableXa = _diabanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.XA, IsAddFirst: true, strFirstRow: "-- Chọn xã/phường --", IsListChaCon: false, Valselected: diaban.ID, ParentId: diaban.PARENT_ID);
					model.AvailableHuyen = _diabanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.HUYEN, IsAddFirst: true, strFirstRow: "-- Chọn quận/huyện --", IsListChaCon: false, Valselected: diaban.PARENT_ID, ParentId: diaBanChaParentId );
					model.AvailableTinh = _diabanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.TINH, IsAddFirst: true, strFirstRow: "-- Chọn tỉnh/thành phố --", IsListChaCon: false, Valselected: diaBanChaParentId);
				}
				model.DIA_CHI = ycct.DIA_CHI;
			}
			return PartialView(model);
		}

		public virtual IActionResult _ThayDoiThongTinTaiSanNha(decimal taiSanId, decimal YeuCauID, DateTime? ngayBienDong = null, decimal? DiaBanId = 0)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin))
				return AccessDeniedView();
			var item = _taisannhaService.GetTaiSanNhaByTaiSanId(taiSanId);

			var model = _taiSanNhaModelFactory.PrepareTaiSanNhaModel(model: new TaiSanNhaModel(), item: item, isTangMoi: false);
			// Địa bàn lấy theo yêu cầu chi tiết nếu y
			var ycct = _yeucauchitietService.GetYeuCauChiTietByYeuCauId(YeuCauID);
			if (YeuCauID > 0 && ycct != null && ycct.DIA_BAN_ID > 0)
			{
				_taiSanNhaModelFactory.PrepareForDDLDiaBan(model, ycct.DIA_BAN_ID);
            }
  
			var kq = _bienDongService.Tinh_GiaTriTaiSan(model.TAI_SAN_ID, ngayBienDong ?? DateTime.Now);
			model.DIEN_TICH_SAN_XAY_DUNG = kq.NHA_TONG_DIEN_TICH_XD_CU;
			model.NVYeuCauChiTietModel.NHA_SO_TANG = ycct == null ? model.NHA_SO_TANG : ycct.NHA_SO_TANG;
			return PartialView(model);
		}

		public virtual IActionResult _ThayDoiThongTinTaiSanVatKienTruc(decimal taiSanId, decimal YeuCauID, DateTime? ngayBienDong = null)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin))
				return AccessDeniedView();
			var item = _taisanVKTService.GetTaiSanVktByTaiSanId(taiSanId);
			var model = _taiSanVktModelFactory.PrepareTaiSanVktModel(model: new TaiSanVktModel(), item: item, isTangMoi: false);
			var kq = _bienDongService.Tinh_GiaTriTaiSan(model.TAI_SAN_ID, ngayBienDong ?? DateTime.Now);
			model.CHIEU_DAI = kq.VKT_CHIEU_DAI_CU;
			model.VKT_DIEN_TICH = kq.VKT_DIEN_TICH_CU;
			model.THE_TICH = kq.VKT_THE_TICH_CU;
			return PartialView(model);
		}

		public virtual IActionResult _ThayDoiThongTinTaiSanCayLauNam(decimal taiSanId, decimal YeuCauID)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin))
				return AccessDeniedView();
			return PartialView(new TaiSanClnModel());
		}

		public virtual IActionResult _ThayDoiThongTinTaiSanMayMocThietBi(decimal taiSanId, decimal YeuCauID)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin))
				return AccessDeniedView();
			return PartialView(new TaiSanMayMocModel());
		}

		#endregion Methods
	}
}