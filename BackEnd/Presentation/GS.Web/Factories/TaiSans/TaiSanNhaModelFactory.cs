//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using DevExpress.CodeParser;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Services;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.NghiepVu;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Framework.Extensions;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.TaiSans
{
	public class TaiSanNhaModelFactory : ITaiSanNhaModelFactory
	{
		#region Fields    		
		private readonly ICacheManager _cacheManager;
		private readonly IWorkContext _workContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly ITaiSanNhaService _itemService;
		private readonly IQuocGiaModelFactory _quocGiaModelFactory;
		private readonly IDiaBanModelFactory _diaBanModelFactory;
		private readonly IHienTrangService _hienTrangService;
		private readonly ITaiSanService _taiSanService;
		private readonly IDiaBanService _diaBanService;
        private readonly IHienTrangModelFactory _hienTrangModelFactory;
        private readonly ILoaiTaiSanService _loaitaisanService;
		private readonly ILoaiTaiSanModelFactory _loaitaisanModelFactory;
		private readonly IYeuCauService _yeuCauService;
		private readonly IYeuCauChiTietService _yeuCauChiTietService;
		private readonly IYeuCauNhatKyService _yeuCauNhatKyService;
		private readonly IDonViService _donViService;
		private const int QuocGiaVietNamID = 33;
		private readonly INhanHienThiService _nhanHienThiService;
		private readonly IDonViBoPhanService _donViBoPhanService;
		private readonly IBienDongService _bienDongService;
		private readonly ITrungGianBDYCService _trungGianBDYCService;
		private readonly IBienDongChiTietService _bienDongChiTietService;
		#endregion

		#region Ctor

		public TaiSanNhaModelFactory(
			ICacheManager cacheManager,
			IWorkContext workContext,
			IStaticCacheManager staticCacheManager,
			ITaiSanNhaService itemService,
			IQuocGiaModelFactory quocGiaModelFactory,
			IDiaBanModelFactory diaBanModelFactory,
			IHienTrangService hienTrangService,
			ILoaiTaiSanService loaiTaiSanService,
			ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
			ITaiSanService taiSanService,
			IYeuCauService yeuCauService,
			IYeuCauChiTietService yeuCauChiTietService,
			IYeuCauNhatKyService yeuCauNhatKyService,
			IDonViService donViService,
			INhanHienThiService nhanHienThiService,
			IDonViBoPhanService donViBoPhanService,
			IBienDongService bienDongService,
			ITrungGianBDYCService trungGianBDYCService,
			IBienDongChiTietService bienDongChiTietService,
			IDiaBanService diabanService,
			IHienTrangModelFactory hienTrangModelFactory
			)
		{
			this._cacheManager = cacheManager;
			this._workContext = workContext;
			this._staticCacheManager = staticCacheManager;
			this._itemService = itemService;
			this._quocGiaModelFactory = quocGiaModelFactory;
			this._diaBanModelFactory = diaBanModelFactory;
			this._hienTrangService = hienTrangService;
			this._loaitaisanService = loaiTaiSanService;
			this._taiSanService = taiSanService;
			this._loaitaisanModelFactory = loaiTaiSanModelFactory;
			this._yeuCauService = yeuCauService;
			this._yeuCauChiTietService = yeuCauChiTietService;
			this._yeuCauNhatKyService = yeuCauNhatKyService;
			this._donViService = donViService;
			this._nhanHienThiService = nhanHienThiService;
			this._donViBoPhanService = donViBoPhanService;
			this._bienDongService = bienDongService;
			this._trungGianBDYCService = trungGianBDYCService;
			this._bienDongChiTietService = bienDongChiTietService;
			this._diaBanService = diabanService;
            _hienTrangModelFactory = hienTrangModelFactory;
        }
		#endregion

		#region TaiSanNha
		public TaiSanNhaSearchModel PrepareTaiSanNhaSearchModel(TaiSanNhaSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			searchModel.SetGridPageSize();
			return searchModel;
		}

		public TaiSanNhaListModel PrepareTaiSanNhaListModel(TaiSanNhaSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			var items = _itemService.SearchTaiSanNhas(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, taiSanDatId: searchModel.TAI_SAN_DAT_ID);

			//prepare list model
			var model = new TaiSanNhaListModel
			{
				Data = items.Select(c =>
				{
					var m = c.ToModel<TaiSanNhaModel>();
					if (c.taisan.LOAI_TAI_SAN_ID > 0)
					{
						var loaitaisan = _loaitaisanService.GetLoaiTaiSanById(c.taisan.LOAI_TAI_SAN_ID ?? 0);
						m.TenLoaiTaiSan = loaitaisan.TEN;
					}
					m.MaTaiSan = c.taisan.MA;
					m.TenTaiSan = c.taisan.TEN;
					//var thongTinNha = _bienDongService.Tinh_GiaTriTaiSan(c.TAI_SAN_ID);
					//m.strDienTichSanVN = thongTinNha.NHA_TONG_DIEN_TICH_XD_CU.ToVNStringNumber();
					//m.strNguyenGiaVN = thongTinNha.NGUYEN_GIA_CU.ToVNStringNumber();
					m.strNguyenGiaVN = _bienDongService.TinhNguyenGiaTaiSan(null, null, c.taisan.ID).ToVNStringNumber();
					m.strDienTichSanVN = c.DIEN_TICH_SAN_XAY_DUNG.ToVNStringNumber(true);
					m.TRANG_THAI_TAI_SAN_ID = c.taisan.TRANG_THAI_ID;
					return m;
				}).ToList(),
				Total = items.Count()
			};
			return model;
		}
		public TaiSanNhaModel PrepareTaiSanNhaModel(TaiSanNhaModel model, TaiSanNha item, bool excludeProperties = false, bool checkCoDat = false, bool isTangMoi = true, bool isCreateTSDA = false, bool IsPrepareForDDLDiaBan = true)
		{

			if (item != null)
			{
				model = item.ToModel<TaiSanNhaModel>();
				if (item.TAI_SAN_ID > 0)
				{
					var taisan = _taiSanService.GetTaiSanById(item.TAI_SAN_ID);
					model.TaiSanModel = taisan.ToModel<TaiSanModel>();
					//model.TaiSanModel = _taiSanModelFactory.PrepareTaiSanModel(model.TaiSanModel, taisan);
				}
				if (isTangMoi)// lấy ra biến động hoặc yêu cầu đầu tiên
				{
					//fill in model values from the entity

					var yeucau = _yeuCauService.GetYeuCauCuNhatByTSId(model.TAI_SAN_ID);
					var yeucauchitiet = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(yeucau.ID);
					if (yeucau.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
						PrepareHoSoNha(model, yeucauchitiet.DATA_JSON.toEntity<YeuCauChiTiet>());
					model.NHA_SO_TANG = yeucauchitiet.NHA_SO_TANG;
					model.NVYeuCauChiTietModel = yeucauchitiet.ToModel<YeuCauChiTietModel>();

					model.lstHienTrang = yeucauchitiet.HTSD_JSON.toEntity<HienTrangList>().lstObjHienTrang;

					// Nếu nhập sai hiện trạng thì mở tất ra cho sửa
					if (_hienTrangModelFactory.IsAnyHienTrangSai(model.lstHienTrang, _workContext.CurrentDonVi.ID))
					{
						model.lstHienTrang = model.lstHienTrang.Select(c =>
						{
							c.IsOpenAll = true;
							return c;
						}).ToList();
					}
					//Bổ sung để lưu tỉnh quận huyện của tài sản nhà không có đất
					model.DIA_BAN_ID = yeucauchitiet.DIA_BAN_ID;
				}
              
				else //lấy ra biến động hoặc yêu cầu gần nhất
				{
					model.NVYeuCauChiTietModel = new YeuCauChiTietModel();
					model.lstHienTrang = _trungGianBDYCService.GetHTSD_JSON_of_TS(model.TAI_SAN_ID).toEntity<HienTrangList>().lstObjHienTrang;
					var TrungGianBDYC = _trungGianBDYCService.GetYCBDGanNhat(model.TAI_SAN_ID);
					if (TrungGianBDYC != null)
						if (TrungGianBDYC.EnumBDorYC == enumBDorYC.BIEN_DONG)
						{
							var bdct = _bienDongChiTietService.GetBienDongChiTietByBDId(TrungGianBDYC.ID);
							model.NHA_SO_TANG = bdct.NHA_SO_TANG;
							//Bổ sung để lưu tỉnh quận huyện của tài sản nhà không có đất
							model.DIA_BAN_ID = bdct.DIA_BAN_ID;
						}
						else if (TrungGianBDYC.EnumBDorYC == enumBDorYC.YEU_CAU)
						{
							var ycct = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(TrungGianBDYC.ID);
							model.NHA_SO_TANG = ycct.NHA_SO_TANG;
							//Bổ sung để lưu tỉnh quận huyện của tài sản nhà không có đất
							model.DIA_BAN_ID = ycct.DIA_BAN_ID;
						}
				}
				//kiem tra co ho so
				if (model.HS_BIEN_BAN_NGHIEM_THU_NGAY != null || model.HS_PHAP_LY_KHAC_NGAY != null || model.HS_QUYET_DINH_BAN_GIAO_NGAY != null)
				{
					model.TaiSanModel.cohoso = true;
				}
				model.NHA_DIA_CHI = item.DIA_CHI;
				if (model.lstHienTrang != null)
				{
					foreach (var itemHT in model.lstHienTrang)
					{
						itemHT.TenHienTrang = _hienTrangService.GetHienTrangById(itemHT.HienTrangId ?? 0).TEN_HIEN_TRANG;
					}
				}
			}
			else
			{
				model.NVYeuCauChiTietModel = new YeuCauChiTietModel();
				var lstHienTrang = _hienTrangService.GetHienTrangs(LoaiHinhTsId: (int)enumLOAI_HINH_TAI_SAN.NHA, isTSDA: isCreateTSDA);
				var lstObjHienTrang = new List<ObjHienTrang>();
				foreach (var ht in lstHienTrang)
				{
					var obj = new ObjHienTrang();
					obj.HienTrangId = ht.ID;
					obj.TenHienTrang = ht.TEN_HIEN_TRANG;
					obj.KieuDuLieuId = ht.KIEU_DU_LIEU_ID;
					obj.NhomHienTrangId = ht.NHOM_HIEN_TRANG_ID;
					obj.GiaTriCheckbox = false;
					lstObjHienTrang.Add(obj);
				}
				model.lstHienTrang = lstObjHienTrang;
			}
			//more


			//prepare loaiTaiSan
			if (model.LoaiTaiSanModel != null && model.LoaiTaiSanModel.ID > 0)
			{
				var loaiTaiSan = _loaitaisanService.GetLoaiTaiSanById(model.LoaiTaiSanModel.ID);
				model.LoaiTaiSanModel = loaiTaiSan.ToModel<LoaiTaiSanModel>();
				// hien trang
				model.AvailableLoaiTaiSan = _loaitaisanModelFactory.PrepareSelectListLoaiTaiSan(loaiHinhTaiSanId: (int)enumLOAI_HINH_TAI_SAN.NHA, cheDoId: loaiTaiSan.CHE_DO_HAO_MON_ID, valSelected: loaiTaiSan.ID);
				var hienTrangList = _hienTrangService.ListHienTrangsByFields(loaiHinhTSId: model.LoaiTaiSanModel.LOAI_HINH_TAI_SAN_ID);
				model.ListHienTrangModel = hienTrangList.Select(c => c.ToModel<HienTrangModel>()).ToList();
			}

			
			if (model.TAI_SAN_DAT_ID > 0)
				model.isQuanLyDat = true;
			else
				model.isQuanLyDat = false;

			if ( model.DIA_BAN_ID > 0)
			{
				PrepareForDDLDiaBan(model, model.DIA_BAN_ID);
			
			}
            else
            {
				model.AvailableQuocGia = _quocGiaModelFactory.PrepareSelectListQuocGias(valSelected: QuocGiaVietNamID, IsAddFirst: true);
				model.AvailableTinh = _diaBanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: QuocGiaVietNamID, CapDiaBan: (int)enumLOAI_DIABAN.TINH, IsAddFirst: true, strFirstRow: "-- Chọn tỉnh/thành phố --", IsListChaCon: false);
				model.AvailableHuyen.AddFirstRow("-- Chọn quận/huyện --");
				model.AvailableXa.AddFirstRow("-- Chọn xã/phường --");
			}
			//prepare che do hach toan theo don vi
			var donvi = _workContext.CurrentDonVi;
			var donvikh = _donViService.GetDonViById(donvi.ID);
			model.NVYeuCauChiTietModel.PhuongPhapTinhKhauHaoAvailable = model.NVYeuCauChiTietModel.enumPhuongPhapTinhKhauHao.ToSelectList();
			model.CHE_DO_HACH_TOAN_ID = donvikh.CHE_DO_HACH_TOAN_ID ?? 0;
			if (model.NVYeuCauChiTietModel.KH_GIA_TINH_KHAU_HAO != null)
			{
				model.TRANG_THAI_KH = true;
			}
			//TH: Nha kem dat
			if (checkCoDat == true)
			{

			}
			return model;
		}
		public void PrepareTaiSanNha(TaiSanNhaModel model, TaiSanNha item)
		{
			item.TAI_SAN_DAT_ID = model.TAI_SAN_DAT_ID;
			item.DIEN_TICH_XAY_DUNG = model.DIEN_TICH_XAY_DUNG;
			item.DIEN_TICH_SAN_XAY_DUNG = model.DIEN_TICH_SAN_XAY_DUNG;
			item.NAM_XAY_DUNG = model.NAM_XAY_DUNG;
			item.NGAY_SU_DUNG = model.NGAY_SU_DUNG;
			item.DIA_CHI = model.NHA_DIA_CHI;
			item.TINH_ID = model.NHA_TinhId;
			item.HUYEN_ID = model.NHA_HuyenId;
			item.XA_ID = model.NHA_XaId;
 
		}
		public TaiSanListModel PrepareTaiSanListModelByDatId(decimal? DatId = 0)
		{
			var listNha = _itemService.GetTaiSanNhaByDatId(DatId ?? 0);
			var model = new TaiSanListModel();
			if (listNha.Count() > 0)
			{
				model = new TaiSanListModel
				{
					Data = listNha.Select(c =>
					{
						var m = c.taisan.ToModel<TaiSanModel>();
						m.TenLoaiHinhTaiSan = _nhanHienThiService.GetGiaTriEnum(c.taisan.enumLoaiHinhTaiSan);
						if (c.taisan.TRANG_THAI_ID != null)
						{
							m.tentrangthai = _nhanHienThiService.GetGiaTriEnum(c.taisan.TrangThaiTaiSan);
						}
						if (c.taisan.LOAI_TAI_SAN_ID > 0)
						{
							var loaitaisan = _loaitaisanService.GetLoaiTaiSanById(m.LOAI_TAI_SAN_ID ?? 0);
							m.TenLoaiTaiSan = loaitaisan.TEN;
						}
						if (c.taisan.DON_VI_BO_PHAN_ID > 0)
						{
							var bophansudung = _donViBoPhanService.GetDonViBoPhanById(m.DON_VI_BO_PHAN_ID ?? 0);
							m.TenBoPhanSuDung = bophansudung.TEN;
						}
						var yeuCauMoiNhat = _yeuCauService.GetYeuCauMoiNhatByTaiSanId(c.taisan.ID);
						m.strNguyenGiaVN = yeuCauMoiNhat.NGUYEN_GIA.ToVNStringNumber();
						m.strDienTichSanVN = c.DIEN_TICH_SAN_XAY_DUNG.ToVNStringNumber();
						return m;
					}).ToList(),
					Total = listNha.Count()
				};
			}
			return model;
		}

		public void PrepareHoSoNha(TaiSanNhaModel model, YeuCauChiTiet item)
		{
			if (model != null && item != null)
			{
				model.HS_BIEN_BAN_NGHIEM_THU = item.HS_BIEN_BAN_NGHIEM_THU;
				model.HS_BIEN_BAN_NGHIEM_THU_NGAY = item.HS_BIEN_BAN_NGHIEM_THU_NGAY;
				model.HS_PHAP_LY_KHAC = item.HS_PHAP_LY_KHAC;
				model.HS_PHAP_LY_KHAC_NGAY = item.HS_PHAP_LY_KHAC_NGAY;
				model.HS_QUYET_DINH_BAN_GIAO = item.HS_QUYET_DINH_BAN_GIAO;
				model.HS_QUYET_DINH_BAN_GIAO_NGAY = item.HS_QUYET_DINH_BAN_GIAO_NGAY;
			}
		}

        public string PrepareDiaChiNhaByDiaBan(string DiaChi, decimal? DiaBanId)
        {
           
				var item = new TaiSanNha() { };
			
            if (DiaBanId > 0)
            {
				_itemService.GetDiaBanInfoByMaDiaBan(DiaBanId, item);
				var TinhString = _diaBanService.GetDiaBanById(item.TINH_ID ?? 0)?.TEN;
				var HuyenString = _diaBanService.GetDiaBanById(item.HUYEN_ID ?? 0)?.TEN;
				var XaString = _diaBanService.GetDiaBanById(item.XA_ID ?? 0)?.TEN;
                
				
				if (!string.IsNullOrEmpty(XaString))
				{
					DiaChi = $"{DiaChi}, {XaString}";

				}
				if (HuyenString != null)
				{
					DiaChi = $"{DiaChi}, {HuyenString}";

				}
				if (TinhString != null)
				{
					DiaChi = $"{DiaChi}, {TinhString}"; 

				}
			}
			return DiaChi;
		}

        public void PrepareForDDLDiaBan(TaiSanNhaModel model, decimal? DiaBanId)
        {
            if (model != null && DiaBanId > 0)
            {
				model.DIA_BAN_ID = DiaBanId;
				var diaban = _diaBanService.GetDiaBanById(DiaBanId ?? 0);
				model.AvailableQuocGia = _quocGiaModelFactory.PrepareSelectListQuocGias(valSelected: diaban.QUOC_GIA_ID, IsAddFirst: true);
				switch (diaban.LOAI_DIA_BAN_ID)
				{
					case (int)enumLOAI_DIABAN.XA:
						var diaBanCha = _diaBanService.GetDiaBanById(diaban.PARENT_ID ?? 0);
						var diaBanChaParentId = diaBanCha != null ? (decimal?)diaBanCha.PARENT_ID : null;
						model.NHA_XaId = (int)diaban.ID;
						model.NHA_HuyenId = (int)diaban.PARENT_ID;
						model.NHA_TinhId = (int)diaBanChaParentId;
						model.NHA_QuocGiaId = (int)diaban.QUOC_GIA_ID;
						model.AvailableXa = _diaBanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.XA, IsAddFirst: true, strFirstRow: "-- Chọn xã/phường --", IsListChaCon: false, Valselected: diaban.ID, ParentId: diaban.PARENT_ID);
						model.AvailableHuyen = _diaBanModelFactory.PrepareDiaBanAvailabele(ParentId: diaBanChaParentId, QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.HUYEN, IsAddFirst: true, strFirstRow: "-- Chọn quận/huyện --", IsListChaCon: false, Valselected: diaban.PARENT_ID);
						model.AvailableTinh = _diaBanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.TINH, IsAddFirst: true, strFirstRow: "-- Chọn tỉnh/thành phố --", IsListChaCon: false, Valselected: diaBanChaParentId);
						break;
					case (int)enumLOAI_DIABAN.HUYEN:
						model.AvailableXa = _diaBanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.XA, IsAddFirst: true, strFirstRow: "-- Chọn xã/phường --", IsListChaCon: false, Valselected: 0);
						model.AvailableHuyen = _diaBanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.HUYEN, IsAddFirst: true, strFirstRow: "-- Chọn quận/huyện --", IsListChaCon: false, Valselected: diaban.ID, ParentId: diaban.PARENT_ID);
						model.AvailableTinh = _diaBanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.TINH, IsAddFirst: true, strFirstRow: "-- Chọn tỉnh/thành phố --", IsListChaCon: false, Valselected: diaban.PARENT_ID);
						model.NHA_HuyenId = (int)diaban.ID;
						model.NHA_TinhId = (int)diaban.PARENT_ID;
						model.NHA_QuocGiaId = (int)diaban.QUOC_GIA_ID;
						break;
					case (int)enumLOAI_DIABAN.TINH:
						model.AvailableXa = _diaBanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.XA, IsAddFirst: true, strFirstRow: "-- Chọn xã/phường --", IsListChaCon: false, Valselected: 0);
						model.AvailableHuyen = _diaBanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.HUYEN, IsAddFirst: true, strFirstRow: "-- Chọn quận/huyện --", IsListChaCon: false, Valselected: 0);
						model.AvailableTinh = _diaBanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.TINH, IsAddFirst: true, strFirstRow: "-- Chọn tỉnh/thành phố --", IsListChaCon: false, Valselected: diaban.ID);
						model.NHA_TinhId = (int)diaban.ID;
						model.NHA_QuocGiaId = (int)diaban.QUOC_GIA_ID;
						break;
				}

			}
        }
        #endregion
    }
}

