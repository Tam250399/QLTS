//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.NghiepVu;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Framework.Extensions;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.TaiSans
{
	public class TaiSanDatModelFactory : ITaiSanDatModelFactory
	{
		#region Fields    		
		private readonly ICacheManager _cacheManager;
		private readonly IWorkContext _workContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly ITaiSanDatService _itemService;
		private readonly IDiaBanModelFactory _diabanModelFactory;
		private readonly IDiaBanService _diabanService;
		private readonly IYeuCauService _yeucauService;
		private readonly IYeuCauChiTietService _yeucauchitietService;
		private readonly IQuocGiaModelFactory _quocgiaModelFactory;
		private const int QuocGiaVietNamID = 33;
		private readonly ITrungGianBDYCService _trungGianBDYCService;
		private readonly IBienDongChiTietService _bienDongChiTietService;
		private readonly IDiaBanService _diaBanService;
		private readonly IHienTrangService _hienTrangService;
        private readonly IHienTrangModelFactory _hienTrangModelFactory;
        #endregion

        #region Ctor

        public TaiSanDatModelFactory(
			ICacheManager cacheManager,
			IWorkContext workContext,
			IStaticCacheManager staticCacheManager,
			ITaiSanDatService itemService,
			IDiaBanModelFactory diabanModelFactory,
			IQuocGiaModelFactory quocgiaModelFactory,
			IDiaBanService diabanService,
			IYeuCauService yeucauService,
			IYeuCauChiTietService yeucauchitietService,
			ITrungGianBDYCService trungGianBDYCService,
			IBienDongChiTietService bienDongChiTietService,
			IDiaBanService diaBanService,
			IHienTrangService hienTrangService,
			IHienTrangModelFactory hienTrangModelFactory // Lưu ý không inject TaiSanDatModelFactory vào hienTrangModelFactory để tránh vòng lặp
			)
		{
			this._cacheManager = cacheManager;
			this._workContext = workContext;
			this._staticCacheManager = staticCacheManager;
			this._itemService = itemService;
			this._diabanModelFactory = diabanModelFactory;
			this._quocgiaModelFactory = quocgiaModelFactory;
			this._diabanService = diabanService;
			this._yeucauService = yeucauService;
			this._yeucauchitietService = yeucauchitietService;
			this._trungGianBDYCService = trungGianBDYCService;
			this._bienDongChiTietService = bienDongChiTietService;
			this._diaBanService = diaBanService;
			this._hienTrangService = hienTrangService;
            _hienTrangModelFactory = hienTrangModelFactory;
        }
		#endregion

		#region TaiSanDat
		public TaiSanDatSearchModel PrepareTaiSanDatSearchModel(TaiSanDatSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			searchModel.SetGridPageSize();
			return searchModel;
		}

		public TaiSanDatListModel PrepareTaiSanDatListModel(TaiSanDatSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			var items = _itemService.SearchTaiSanDats(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

			//prepare list model
			var model = new TaiSanDatListModel
			{
				//fill in model values from the entity
				Data = items.Select(c => c.ToModel<TaiSanDatModel>()),
				Total = items.TotalCount
			};
			return model;
		}
		public TaiSanDatModel PrepareTaiSanDatModel(TaiSanDatModel model, TaiSanDat item, bool excludeProperties = false, bool isTangMoi = true)
		{
			if (item != null)
			{
				//fill in model values from the entity
				model = item.ToModel<TaiSanDatModel>();
				if (isTangMoi)// lấy ra biến động hoặc yêu cầu đầu tiên
				{
					var yeucau = _yeucauService.GetYeuCauCuNhatByTSId(model.TAI_SAN_ID);
					var ycct = _yeucauchitietService.GetYeuCauChiTietByYeuCauId(yeucau.ID);
					if (yeucau.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
						PrepareHoSoDat(model, ycct.DATA_JSON.toEntity<YeuCauChiTiet>());
					if (ycct.HTSD_JSON != null)
					{
						model.lstHienTrang = ycct.HTSD_JSON.toEntity<HienTrangList>().lstObjHienTrang;

						// Nếu nhập sai hiện trạng thì mở tất ra cho sửa
						if (_hienTrangModelFactory.IsAnyHienTrangSai(model.lstHienTrang,_workContext.CurrentDonVi.ID))
                        {
							model.lstHienTrang = model.lstHienTrang.Select(c =>
							{
								c.IsOpenAll = true;
								return c;
							}).ToList();
						}
					}
					model.DIEN_TICH = ycct.DAT_TONG_DIEN_TICH ?? 0;
				}
				else //lấy ra biến động hoặc yêu cầu gần nhất
				{
					model.lstHienTrang = _trungGianBDYCService.GetHTSD_JSON_of_TS(model.TAI_SAN_ID).toEntity<HienTrangList>().lstObjHienTrang;
					var TrungGianBDYC = _trungGianBDYCService.GetYCBDGanNhat(model.TAI_SAN_ID);
					if (TrungGianBDYC != null)
						if (TrungGianBDYC.EnumBDorYC == enumBDorYC.BIEN_DONG)
						{
							var bdct = _bienDongChiTietService.GetBienDongChiTietByBDId(TrungGianBDYC.ID);
							model.DIEN_TICH = bdct.DAT_TONG_DIEN_TICH ?? 0;
							if (TrungGianBDYC.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
								PrepareHoSoDat(model, bdct.DATA_JSON.toEntity<YeuCauChiTiet>());
						}
						else if (TrungGianBDYC.EnumBDorYC == enumBDorYC.YEU_CAU)
						{
							var ycct = _yeucauchitietService.GetYeuCauChiTietByYeuCauId(TrungGianBDYC.ID);
							if (TrungGianBDYC.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
								PrepareHoSoDat(model, ycct.DATA_JSON.toEntity<YeuCauChiTiet>());
							model.DIEN_TICH = ycct.DAT_TONG_DIEN_TICH ?? 0;
						}
				}
				if (item.DIA_BAN_ID > 0)
				{
					var diaban = _diabanService.GetDiaBanById(item.DIA_BAN_ID ?? 0);
					model.AvailableQuocGia = _quocgiaModelFactory.PrepareSelectListQuocGias(valSelected: diaban.QUOC_GIA_ID, IsAddFirst: true);
					//if (diaban.LOAI_DIA_BAN_ID == (int)enumLOAI_DIABAN.XA)
					//{
					//	model.AvailableXa = _diabanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.XA, IsAddFirst: true, strFirstRow: "-- Chọn xã/phường --", IsListChaCon: false, Valselected: diaban.ID, ParentId: diaban.PARENT_ID);
					//	model.AvailableHuyen = _diabanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.HUYEN, IsAddFirst: true, strFirstRow: "-- Chọn quận/huyện --", IsListChaCon: false, Valselected: diaban.PARENT_ID);
					//	model.AvailableTinh = _diabanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.TINH, IsAddFirst: true, strFirstRow: "-- Chọn tỉnh/thành phố --", IsListChaCon: false, Valselected: diaban.DiaBanCha.PARENT_ID);
					//}
					switch (diaban.LOAI_DIA_BAN_ID)
					{
						case (int)enumLOAI_DIABAN.XA:
							var diaBanCha = _diaBanService.GetDiaBanById(diaban.PARENT_ID ?? 0);
							var diaBanChaParentId = diaBanCha != null ? (decimal?)diaBanCha.PARENT_ID : null;
							model.XaId = (int)diaban.ID;
							model.HuyenId = (int)diaban.PARENT_ID;
							model.TinhId = (int)diaBanChaParentId;
							model.QuocGiaId = (int)diaban.QUOC_GIA_ID;
							model.AvailableXa = _diabanModelFactory.PrepareDiaBanAvailabele( QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.XA, IsAddFirst: true, strFirstRow: "-- Chọn xã/phường --", IsListChaCon: false, Valselected: diaban.ID, ParentId: diaban.PARENT_ID);
							model.AvailableHuyen = _diabanModelFactory.PrepareDiaBanAvailabele(ParentId: diaBanChaParentId, QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.HUYEN, IsAddFirst: true, strFirstRow: "-- Chọn quận/huyện --", IsListChaCon: false, Valselected: diaban.PARENT_ID);
							model.AvailableTinh = _diabanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.TINH, IsAddFirst: true, strFirstRow: "-- Chọn tỉnh/thành phố --", IsListChaCon: false, Valselected: diaBanChaParentId);
							break;
						case (int)enumLOAI_DIABAN.HUYEN:
							model.AvailableXa = _diabanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.XA, IsAddFirst: true, strFirstRow: "-- Chọn xã/phường --", IsListChaCon: false, Valselected: 0);
							model.AvailableHuyen = _diabanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.HUYEN, IsAddFirst: true, strFirstRow: "-- Chọn quận/huyện --", IsListChaCon: false, Valselected: diaban.ID, ParentId: diaban.PARENT_ID);
							model.AvailableTinh = _diabanModelFactory.PrepareDiaBanAvailabele( QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.TINH, IsAddFirst: true, strFirstRow: "-- Chọn tỉnh/thành phố --", IsListChaCon: false, Valselected: diaban.PARENT_ID);
							model.HuyenId = (int)diaban.ID;
							model.TinhId = (int)diaban.PARENT_ID;
							model.QuocGiaId = (int)diaban.QUOC_GIA_ID;
							break;
						case (int)enumLOAI_DIABAN.TINH:
							model.AvailableXa = _diabanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.XA, IsAddFirst: true, strFirstRow: "-- Chọn xã/phường --", IsListChaCon: false, Valselected: 0);
							model.AvailableHuyen = _diabanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.HUYEN, IsAddFirst: true, strFirstRow: "-- Chọn quận/huyện --", IsListChaCon: false, Valselected: 0);
							model.AvailableTinh = _diabanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: diaban.QUOC_GIA_ID, CapDiaBan: (int)enumLOAI_DIABAN.TINH, IsAddFirst: true, strFirstRow: "-- Chọn tỉnh/thành phố --", IsListChaCon: false, Valselected: diaban.ID);
							model.TinhId = (int)diaban.ID;
							model.QuocGiaId = (int)diaban.QUOC_GIA_ID;
							break;
					}
					
				}
				else
				{
					//mặc định là VN
					model.QuocGiaId = QuocGiaVietNamID;
					model.AvailableQuocGia = _quocgiaModelFactory.PrepareSelectListQuocGias(valSelected: QuocGiaVietNamID, IsAddFirst: true);
					model.AvailableTinh = _diabanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: QuocGiaVietNamID, CapDiaBan: (int)enumLOAI_DIABAN.TINH, IsAddFirst: true, strFirstRow: "-- Chọn tỉnh/thành phố --", IsListChaCon: false);
					model.AvailableHuyen.AddFirstRow("-- Chọn quận/huyện --");
					model.AvailableXa.AddFirstRow("-- Chọn xã/phường --");
				}
				if (model.lstHienTrang == null)
				{
					var lstHienTrang = _hienTrangService.GetHienTrangs(LoaiHinhTsId: (int)enumLOAI_HINH_TAI_SAN.DAT);
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
					var hientrangList = new HienTrangList()
					{
						TaiSanId = item.ID,
						lstObjHienTrang = lstObjHienTrang
					};
					model.lstHienTrang = hientrangList.lstObjHienTrang;
				}
				else
				{
					foreach (var itemHT in model.lstHienTrang)
					{
						itemHT.TenHienTrang = _hienTrangService.GetHienTrangById(itemHT.HienTrangId ?? 0).TEN_HIEN_TRANG;
					}
				}
				//more
				if (model.HS_CNQSD_SO != null || model.HS_QUYET_DINH_GIAO_SO != null || model.HS_CHUYEN_NHUONG_SD_SO != null || model.HS_QUYET_DINH_CHO_THUE_SO != null)
				{
					model.cohoso = true;
				}
			}

			return model;
		}
		public void PrepareTaiSanDat(TaiSanDatModel model, TaiSanDat item)
		{
			if (model != null && item != null)
			{
				item.DIA_CHI = model.DIA_CHI;
				item.DIA_BAN_ID = model.DIA_BAN_ID;
				item.DIEN_TICH = model.DIEN_TICH;
				item.DIEN_TICH_XAY_NHA = model.DIEN_TICH_XAY_NHA;
				item.TINH_ID = model.TinhId;
				item.HUYEN_ID = model.HuyenId;
				item.XA_ID = model.XaId;
				
			}

		}

		public void PrepareHoSoDat(TaiSanDatModel model, YeuCauChiTiet item)
		{
			if (model != null && item != null)
			{
				model.HS_CNQSD_SO = item.HS_CNQSD_SO;
				model.HS_CNQSD_NGAY = item.HS_CNQSD_NGAY;
				model.HS_QUYET_DINH_GIAO_SO = item.HS_QUYET_DINH_GIAO_SO;
				model.HS_QUYET_DINH_GIAO_NGAY = item.HS_QUYET_DINH_GIAO_NGAY;
				model.HS_CHUYEN_NHUONG_SD_SO = item.HS_CHUYEN_NHUONG_SD_SO;
				model.HS_CHUYEN_NHUONG_SD_NGAY = item.HS_CHUYEN_NHUONG_SD_NGAY;
				model.HS_QUYET_DINH_CHO_THUE_SO = item.HS_QUYET_DINH_CHO_THUE_SO;
				model.HS_QUYET_DINH_CHO_THUE_NGAY = item.HS_QUYET_DINH_CHO_THUE_NGAY;
				model.HS_PHAP_LY_KHAC = item.HS_PHAP_LY_KHAC;
				model.HS_PHAP_LY_KHAC_NGAY = item.HS_PHAP_LY_KHAC_NGAY;
				model.HS_HOP_DONG_CHO_THUE_SO = item.HS_HOP_DONG_CHO_THUE_SO;
				model.HS_HOP_DONG_CHO_THUE_NGAY = item.HS_HOP_DONG_CHO_THUE_NGAY;
			}

		}
		public void PrepareHoSoDat(TaiSanDatModel model, BienDongChiTiet item)
		{
			if (model != null && item != null)
			{
				model.HS_CNQSD_SO = item.HS_CNQSD_SO;
				model.HS_CNQSD_NGAY = item.HS_CNQSD_NGAY;
				model.HS_QUYET_DINH_GIAO_SO = item.HS_QUYET_DINH_GIAO_SO;
				model.HS_QUYET_DINH_GIAO_NGAY = item.HS_QUYET_DINH_GIAO_NGAY;
				model.HS_CHUYEN_NHUONG_SD_SO = item.HS_CHUYEN_NHUONG_SD_SO;
				model.HS_CHUYEN_NHUONG_SD_NGAY = item.HS_CHUYEN_NHUONG_SD_NGAY;
				model.HS_QUYET_DINH_CHO_THUE_SO = item.HS_QUYET_DINH_CHO_THUE_SO;
				model.HS_QUYET_DINH_CHO_THUE_NGAY = item.HS_QUYET_DINH_CHO_THUE_NGAY;
			}

		}
		public bool CheckDiaChiTaiSanDat(string diaChi, decimal? diaBanId = 0, decimal? donViId = 0, decimal? id = 0)
		{
			var tsDat = _itemService.CheckDiaChiTaiSanDat(diaChi: diaChi, diaBanId: diaBanId, donViId: donViId);
			if (tsDat != null && tsDat.taisan.ID != id)
				return false;
			else return true;
		}

		private bool IsAnyHienTrangSai(string listHienTrangJson,decimal donViId)
		{
			var lstHienTrang = listHienTrangJson?.toEntities<ObjHienTrang>();
			var countError = 0;
			if (lstHienTrang != null && lstHienTrang.Count() > 0)
			{
				
				foreach (var hienTrang in lstHienTrang)
				{
					if (hienTrang != null)
					{
						// nếu mà hiện trạng không đúng với loại hình đơn vị nhưng có giá trị thì báo validate
						if (CheckIsHienTrangNhapSai(donViId, hienTrang))
						{
							countError += 1;
						}
					}
				}
				
			}
			return countError > 0;

		}
		/// <summary>
		/// Truyền vào hiện trạng hiện trạng và donViID, check xem hiện trạng đó có bị nhập sai so với loại hình đơn vi không
		/// true là có nhập sai, false là không sai
		/// </summary>
		/// <param name="donViID"></param>
		/// <param name="ObjHienTrang"></param>
		/// <returns></returns>
		public bool CheckIsHienTrangNhapSai(decimal donViID, ObjHienTrang ObjHienTrang = null)
		{
			if (donViID <= 0 || ObjHienTrang == null)
			{
				return true; // Coi như nhập sai
			}

			return (!_hienTrangService.CheckHienTrangTheoLoaiDonVi(donViID, ObjHienTrang.HienTrangId ?? 0) && (ObjHienTrang.GiaTriCheckbox == true || ObjHienTrang.GiaTriNumber > 0 || ObjHienTrang.GiaTriNumber < 0));
		}
		#endregion

	}
}

