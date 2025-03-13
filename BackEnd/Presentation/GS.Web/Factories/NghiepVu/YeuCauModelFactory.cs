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
using GS.Services;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.NghiepVu;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.BienDongs;
using GS.Web.Factories.DanhMuc;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GS.Web.Factories.NghiepVu
{
	public class YeuCauModelFactory : IYeuCauModelFactory
	{
		#region Fields

		private readonly ICacheManager _cacheManager;
		private readonly IWorkContext _workContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly IYeuCauService _itemService;
		private readonly ILyDoBienDongService _lydobiendongService;
		private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
		private readonly IDonViBoPhanService _donViBoPhanService;
		private readonly ILoaiTaiSanService _loaiTaiSanService;
		private readonly IYeuCauChiTietService _yeuCauChiTietService;
		private readonly IYeuCauChiTietModelFactory _yeuCauChiTietModelFactory;
		private readonly IDonViService _donViService;
		private readonly ILyDoBienDongModelFactory _lyDoBienDongModelFactory;
		private readonly INguonVonModelFactory _nguonVonModelFactory;
		private readonly IBienDongService _bienDongService;
		private readonly IBienDongChiTietService _bienDongChiTietService;
		private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
		private readonly ICheDoHaoMonService _cheDoHaoMonService;
		private readonly INhanHienThiService _nhanhienthiService;
		private readonly INguoiDungService _nguoiDungService;
		private readonly ITaiSanService _taiSanService;
		private readonly ITaiSanNhaService _taisannhaService;
		private readonly ITaiSanDatService _taisandatService;
		private readonly ITaiSanNguonVonService _taiSanNguonVonService;
		private readonly INguonVonService _NguonVonService;
		private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViServices;
		private readonly ILoaiTaiSanVoHinhModelFactory _loaiTaiSanVoHinhModelFactory;
		private readonly IDuAnService _duAnService;
		private readonly IHienTrangService _hienTrangService;
        #endregion Fields

        #region Ctor

        public YeuCauModelFactory(
			ICacheManager cacheManager,
			IWorkContext workContext,
			IStaticCacheManager staticCacheManager,
			IYeuCauService itemService,
			ILyDoBienDongService lydobiendongService,
			IDonViBoPhanModelFactory donViBoPhanModelFactory,
			IDonViBoPhanService donViBoPhanService,
			ILoaiTaiSanService loaiTaiSanService,
			IYeuCauChiTietModelFactory yeuCauChiTietModelFactory,
			IYeuCauChiTietService yeuCauChiTietService,
			IDonViService donViService,
			ILyDoBienDongModelFactory lyDoBienDongModelFactory,
			INguonVonModelFactory nguonVonModelFactory,
			IBienDongService bienDongService,
			IBienDongChiTietService bienDongChiTietService,
			ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
			ICheDoHaoMonService cheDoHaoMonService,
			INhanHienThiService nhanhienthiService,
			INguoiDungService nguoiDungService,
			ITaiSanService taiSanService,
			ITaiSanNhaService taisannhaService,
			ITaiSanNguonVonService taiSanNguonVonService,
			ITaiSanDatService taisandatService,
			INguonVonService nguonVonService,
			ILoaiTaiSanDonViServices loaiTaiSanVoHinhService,
			ILoaiTaiSanVoHinhModelFactory loaiTaiSanVoHinhModelFactory,
			IDuAnService duAnService,
			IHienTrangService hienTrangService 

			)
		{
			this._cacheManager = cacheManager;
			this._workContext = workContext;
			this._staticCacheManager = staticCacheManager;
			this._itemService = itemService;
			this._lydobiendongService = lydobiendongService;
			this._donViBoPhanModelFactory = donViBoPhanModelFactory;
			this._donViBoPhanService = donViBoPhanService;
			this._loaiTaiSanService = loaiTaiSanService;
			this._yeuCauChiTietModelFactory = yeuCauChiTietModelFactory;
			this._yeuCauChiTietService = yeuCauChiTietService;
			this._donViService = donViService;
			this._lyDoBienDongModelFactory = lyDoBienDongModelFactory;
			this._nguonVonModelFactory = nguonVonModelFactory;
			this._bienDongService = bienDongService;
			this._bienDongChiTietService = bienDongChiTietService;
			this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
			this._cheDoHaoMonService = cheDoHaoMonService;
			this._nhanhienthiService = nhanhienthiService;
			this._nguoiDungService = nguoiDungService;
			this._taiSanService = taiSanService;
			this._taisannhaService = taisannhaService;
			this._taisandatService = taisandatService;
			this._taiSanNguonVonService = taiSanNguonVonService;
			this._NguonVonService = nguonVonService;
			this._loaiTaiSanDonViServices = loaiTaiSanVoHinhService;
			this._loaiTaiSanVoHinhModelFactory = loaiTaiSanVoHinhModelFactory;
			this._duAnService = duAnService;
			this._hienTrangService = hienTrangService;
        }

		#endregion Ctor

		#region YeuCau

		public YeuCau PrepareYeuCauFromBienDong(BienDong _bd, YeuCau _yc, bool isCopy = false)
		{
			//k có prepare trạng thái
			_yc.TAI_SAN_ID = _bd.TAI_SAN_ID;
			_yc.TAI_SAN_MA = _bd.TAI_SAN_MA;
			_yc.TAI_SAN_TEN = _bd.TAI_SAN_TEN;
			_yc.LOAI_TAI_SAN_ID = _bd.LOAI_TAI_SAN_ID;
			_yc.LOAI_HINH_TAI_SAN_ID = _bd.LOAI_HINH_TAI_SAN_ID;
			_yc.DON_VI_BO_PHAN_ID = _bd.DON_VI_BO_PHAN_ID;
			_yc.CHUNG_TU_SO = _bd.CHUNG_TU_SO;
			_yc.CHUNG_TU_NGAY = _bd.CHUNG_TU_NGAY;
			_yc.NGAY_BIEN_DONG = _bd.NGAY_BIEN_DONG;
			_yc.NGAY_SU_DUNG = _bd.NGAY_SU_DUNG;
			_yc.LY_DO_BIEN_DONG_ID = _bd.LY_DO_BIEN_DONG_ID;
			_yc.NGAY_TAO = _bd.NGAY_TAO;
			_yc.GHI_CHU = _bd.GHI_CHU;
			_yc.QUYET_DINH_NGAY = _bd.QUYET_DINH_NGAY;
			_yc.QUYET_DINH_SO = _bd.QUYET_DINH_SO;
			_yc.NGUYEN_GIA = _bd.NGUYEN_GIA;
			_yc.HOA_HONG_DE_LAI_DON_VI = _bd.HOA_HONG_DE_LAI_DON_VI;
			_yc.HOA_HONG_NOP_NSNN = _bd.HOA_HONG_NOP_NSNN;
			//prepare NGUON_VON_JSON
			var _listNguonVonBD = _taiSanNguonVonService.GetTaiSanNguonVons(_bd.TAI_SAN_ID, _bd.ID);
			var _listNguonVonModel = _listNguonVonBD.Select(c => c.ToModel<NguonVonModel>());
			if (_listNguonVonModel != null && _listNguonVonModel.Count() > 0)
			{
				_yc.NGUON_VON_JSON = _listNguonVonModel.toStringJson();
			}
			else
				return null;
			if (isCopy)
			{
				_yc.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO;
			}
			else
			{
				_yc.NGUOI_TAO_ID = _bd.NGUOI_TAO_ID;
				_yc.TAI_SAN_ID = _bd.TAI_SAN_ID;
				_yc.TAI_SAN_MA = _bd.TAI_SAN_MA;
				_yc.LOAI_BIEN_DONG_ID = _bd.LOAI_BIEN_DONG_ID;
				_yc.DON_VI_ID = _bd.DON_VI_ID;
			}
			return _yc;
		}

		public YeuCauSearchModel PrepareYeuCauSearchModel(YeuCauSearchModel searchModel)
		{
			int[] valueExclude = new int[] { (int)enumLOAI_HINH_TAI_SAN.KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT, 
											(int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA, 
											(int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC,
			                                (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_QUAN_LY_NHU_TSCD,(int)enumLOAI_HINH_TAI_SAN.TSCD_KHAC  };
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));
			searchModel.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
			searchModel.LoaiHinhTaiSanAvailable = ((enumLOAI_HINH_TAI_SAN)searchModel.enumLoaiHinhTaiSan).ToSelectList(valuesToExclude: valueExclude);
			int[] trangThaiNhapId = { (int)enumTRANG_THAI_YEU_CAU.NHAP, (int)enumTRANG_THAI_YEU_CAU.NHAP_LIEU, (int)enumTRANG_THAI_YEU_CAU.XOA };
			searchModel.Trangthailist = searchModel.enumtrangthaiyeucau.ToSelectList(valuesToExclude: trangThaiNhapId);
			var donviID = _workContext.CurrentDonVi.ID;
			searchModel.BoPhanSuDungAvailable = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: donviID, isAddFirst: true, valSelected: searchModel.DON_VI_BO_PHAN_ID);
			searchModel.SetGridPageSize();
			return searchModel;
		}

		public YeuCauListModel PrepareYeuCauListModel(YeuCauSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			var items = _itemService.SearchYeuCaus(pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, keySearch: searchModel.KeySearch, fromdate: searchModel.FromDate, todate: searchModel.ToDate, loaiHinhTSId: searchModel.LOAI_HINH_TAI_SAN_ID, loaiTSId: searchModel.LOAI_TAI_SAN_ID, chungTuSo: searchModel.CHUNG_TU_SO, nguoiTaoId: searchModel.NGUOI_TAO_ID, boPhanId: searchModel.DON_VI_BO_PHAN_ID, donViId: searchModel.DON_VI_ID, loaiBienDongId: searchModel.LOAI_LY_DO_BD_ID, lyDoBienDongId: searchModel.LY_DO_BIEN_DONG_ID, taisanId: searchModel.taisanId);

			//prepare list model
			var model = new YeuCauListModel
			{
				//fill in model values from the entity
				Data = items.Select(c =>
				{
					var m = c.ToModel<YeuCauModel>();
					if (m.NGUOI_TAO_ID > 0)
					{
						var nguoidung = _nguoiDungService.GetNguoiDungById(m.NGUOI_TAO_ID);
						m.TenNguoiTao = nguoidung.TEN_DAY_DU;
					}
					m.TenTrangThai = _nhanhienthiService.GetGiaTriEnum(c.TrangThaiYeuCau);
					if (m.DON_VI_BO_PHAN_ID > 0)
					{
						var donViBoPhan = _donViBoPhanService.GetDonViBoPhanById(m.DON_VI_BO_PHAN_ID ?? 0);
						m.TenDonViBoPhan = donViBoPhan.TEN;
					}
					if (m.LY_DO_BIEN_DONG_ID.HasValue && m.LY_DO_BIEN_DONG_ID > 0)
					{
						var lyDoBienDong = _lydobiendongService.GetLyDoBienDongById(m.LY_DO_BIEN_DONG_ID ?? 0);
						m.TenLyDoBienDong = lyDoBienDong.TEN;
						m.TenLoaiLyDoBienDong = _nhanhienthiService.GetGiaTriEnum(lyDoBienDong.loaiLyDoTangGiam);
					}
					if (m.LOAI_TAI_SAN_ID.HasValue && m.LOAI_TAI_SAN_ID > 0)
					{
						if (m.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
						{
							var loaiTSVH = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(m.LOAI_TAI_SAN_DON_VI_ID ?? 0);
							m.TenLoaiTaiSan = loaiTSVH != null ? loaiTSVH.TEN : null;
						}
						else
						{
							var loaiTS = _loaiTaiSanService.GetLoaiTaiSanById(m.LOAI_TAI_SAN_ID ?? 0);
							m.TenLoaiTaiSan = loaiTS != null ? loaiTS.TEN : "";
						}
					}
					m.NguyenGiaVNStringNumber = m.NGUYEN_GIA.ToVNStringNumber();
					if (m.TAI_SAN_ID > 0)
					{
						var taisan = _taiSanService.GetTaiSanById(m.TAI_SAN_ID);
						m.TaiSanGuid = taisan.GUID;
						m.TAI_SAN_TRANG_THAI_ID = taisan.TRANG_THAI_ID ?? 0;
					}
					m.SoYCTruocChuaDuyet = _itemService.CountYeuCauCuaTaiSan(m.TAI_SAN_ID, new List<decimal> { (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET, (int)enumTRANG_THAI_YEU_CAU.TU_CHOI }, null, m.NGAY_BIEN_DONG);
					m.pageIndex = searchModel.Page;
					return m;
				}).ToList(),
				Total = items.TotalCount
			};
			return model;
		}

		public YeuCauModel PrepareYeuCauModel(YeuCauModel model, YeuCau item, bool isInfoBienDong = false)
		{
			if (item != null)
			{
				//fill in model values from the entity
				model = item.ToModel<YeuCauModel>();
				if (item.DON_VI_BO_PHAN_ID > 0)
				{
					var donViBoPhan = _donViBoPhanService.GetDonViBoPhanById(model.DON_VI_BO_PHAN_ID ?? 0);
					model.TenDonViBoPhan = donViBoPhan.TEN;
				}
				if (item.LY_DO_BIEN_DONG_ID.HasValue && item.LY_DO_BIEN_DONG_ID > 0)
				{
					var lyDoBienDong = _lydobiendongService.GetLyDoBienDongById(model.LY_DO_BIEN_DONG_ID ?? 0);
					model.TenLyDoBienDong = lyDoBienDong.TEN;
					model.TenLoaiLyDoBienDong = _nhanhienthiService.GetGiaTriEnum(lyDoBienDong.loaiLyDoTangGiam);
				}
				if (item.LOAI_TAI_SAN_ID.HasValue && item.LOAI_TAI_SAN_ID > 0)
				{
					var loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(model.LOAI_TAI_SAN_ID ?? 0);
					model.TenLoaiTaiSan = loaiTaiSan.TEN;
				}
				if (item.DON_VI_ID.HasValue && item.DON_VI_ID > 0)
				{
					var donVi = _donViService.GetDonViById(model.DON_VI_ID ?? 0);
					model.TenDonVi = donVi.TEN;
				}
				model.NguyenGiaVNStringNumber = model.NGUYEN_GIA.ToVNStringNumber();
				if (item.taisan != null)
				{
					model.TaiSanGuid = item.taisan.GUID;
				}
				if (model.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.DA_DUYET)
				{
					var yeuCauCTEntity = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(item.ID);
					model.YCCTModel = yeuCauCTEntity.ToModel<YeuCauChiTietModel>();
					if (!String.IsNullOrEmpty(yeuCauCTEntity.HTSD_JSON))
					{
						model.YCCTModel.lstHienTrang = yeuCauCTEntity.HTSD_JSON.toEntity<HienTrangList>().lstObjHienTrang;
					}
				}

				model.LyDoTangAvailable = _lyDoBienDongModelFactory.PrepareSelectListLyDoBienDong(LoaiHinhTaiSanId: model.LOAI_HINH_TAI_SAN_ID, loailydoId: model.LOAI_BIEN_DONG_ID, valSelected: model.LY_DO_BIEN_DONG_ID, isAddFirst: true);

				model.lstNguonVonModel = item.NGUON_VON_JSON.toEntities<NguonVonModel>();
			}
			else
			{
				model.LyDoTangAvailable = _lyDoBienDongModelFactory.PrepareSelectListLyDoBienDong(LoaiHinhTaiSanId: model.LOAI_HINH_TAI_SAN_ID, loailydoId: model.LOAI_BIEN_DONG_ID, isAddFirst: true);
			}
			if (isInfoBienDong)
			{
				switch (model.LOAI_HINH_TAI_SAN_ID)
				{
					case (int)GS.Core.Domain.DanhMuc.enumLOAI_HINH_TAI_SAN.DAT:
						var tsDat = _taisandatService.GetTaiSanDatById(item.TAI_SAN_ID);
						model.taisanDatModel = tsDat.ToModel<TaiSanDatModel>();
						break;

					case (int)GS.Core.Domain.DanhMuc.enumLOAI_HINH_TAI_SAN.NHA:
						var tsNha = _taisannhaService.GetTaiSanNhaByTaiSanId(item.TAI_SAN_ID);
						model.taisanNhaModel = tsNha.ToModel<TaiSanNhaModel>();
						break;
				}
			}
			//more
			return model;
		}

		/// <summary>
		/// Description: Prepare thông tin từ tài sản sang yêu cầu
		/// Sd: Tăng mới tài sản và nhập số dư đầu kỳ
		/// </summary>
		/// <param name="item"></param>
		/// <param name="taisan"></param>
		/// <returns></returns>
		public YeuCau PrepareYeuCau(YeuCau item, TaiSan taisan)
		{
			item.TAI_SAN_ID = taisan.ID;
			item.TAI_SAN_MA = taisan.MA;
			item.TAI_SAN_TEN = taisan.TEN;
			item.LOAI_TAI_SAN_ID = taisan.LOAI_TAI_SAN_ID;
			item.LOAI_TAI_SAN_DON_VI_ID = taisan.LOAI_TAI_SAN_DON_VI_ID;
			item.DON_VI_BO_PHAN_ID = taisan.DON_VI_BO_PHAN_ID;
			item.QUYET_DINH_SO = taisan.QUYET_DINH_SO;
			item.QUYET_DINH_NGAY = taisan.QUYET_DINH_NGAY;
			item.CHUNG_TU_SO = taisan.CHUNG_TU_SO;
			item.CHUNG_TU_NGAY = taisan.CHUNG_TU_NGAY;
			item.NGAY_BIEN_DONG = taisan.NGAY_NHAP;
			item.NGAY_SU_DUNG = taisan.NGAY_SU_DUNG;
			var LDBD = _lydobiendongService.GetLyDoBienDongById(item.LY_DO_BIEN_DONG_ID ?? 0);
			if (LDBD != null)
			{
				item.LOAI_BIEN_DONG_ID = (int)LDBD.loaiLyDoTangGiam;
			}
			item.NGAY_TAO = taisan.NGAY_TAO ?? DateTime.Now;
			return item;
		}

		public YeuCauModel PrepareThongTinChungTS(YeuCauModel yeuCauModel, TaiSan taiSan)
		{
			yeuCauModel.TAI_SAN_ID = taiSan.ID;
			yeuCauModel.TAI_SAN_MA = taiSan.MA;
			yeuCauModel.DU_AN_ID = taiSan.DU_AN_ID;
			if (yeuCauModel.DU_AN_ID.HasValue && yeuCauModel.DU_AN_ID > 0)
				yeuCauModel.IsTaiSanDuAn = true;
			else
				yeuCauModel.IsTaiSanDuAn = false;
			if (yeuCauModel.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
			{
				yeuCauModel.TAI_SAN_TEN = taiSan.TEN;
				yeuCauModel.LOAI_TAI_SAN_ID = taiSan.LOAI_TAI_SAN_ID;
				yeuCauModel.LOAI_HINH_TAI_SAN_ID = taiSan.LOAI_HINH_TAI_SAN_ID;
				yeuCauModel.DON_VI_BO_PHAN_ID = taiSan.DON_VI_BO_PHAN_ID;
				yeuCauModel.LOAI_TAI_SAN_DON_VI_ID = taiSan.LOAI_TAI_SAN_DON_VI_ID;
			}
			yeuCauModel.NGAY_SU_DUNG = taiSan.NGAY_SU_DUNG;
			yeuCauModel.NGAY_TAO = DateTime.Now;
			return yeuCauModel;
		}

		public YeuCau PrepareYeuCauForBDEdit(YeuCauModel model, YeuCau item)
		{
			item.LY_DO_BIEN_DONG_ID = model.LY_DO_BIEN_DONG_ID;
			item.LOAI_BIEN_DONG_ID = model.LOAI_BIEN_DONG_ID;
			item.NGAY_BIEN_DONG = model.NGAY_BIEN_DONG;
			item.CHUNG_TU_SO = model.CHUNG_TU_SO;
			item.CHUNG_TU_NGAY = model.CHUNG_TU_NGAY;
			item.GHI_CHU = model.GHI_CHU;
			item.NGUYEN_GIA = model.NGUYEN_GIA;
			switch (model.LOAI_BIEN_DONG_ID)
			{
				case (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI:
				case (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI:
					//prepare thông tin biến động nguyên giá tài sản
					item.NGUYEN_GIA = model.NGUYEN_GIA;
					break;

				case (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN:
					item.QUYET_DINH_SO = model.QUYET_DINH_SO;
					item.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY;
					break;

				case (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN:
					item.TAI_SAN_TEN = model.TAI_SAN_TEN;
					item.LOAI_TAI_SAN_ID = model.LOAI_TAI_SAN_ID;
					item.DON_VI_BO_PHAN_ID = model.DON_VI_BO_PHAN_ID;
					break;

				default:
					break;
			}
			return item;
		}

		public YeuCau PrepareYeuCauEntity(YeuCauModel model, YeuCau item)
		{
			item.LY_DO_BIEN_DONG_ID = model.LY_DO_BIEN_DONG_ID;
			item.LOAI_BIEN_DONG_ID = model.LOAI_BIEN_DONG_ID;
			item.NGAY_BIEN_DONG = model.NGAY_BIEN_DONG;
			item.CHUNG_TU_SO = model.CHUNG_TU_SO;
			item.CHUNG_TU_NGAY = model.CHUNG_TU_NGAY;
			item.GHI_CHU = model.GHI_CHU;
			item.DON_VI_BO_PHAN_ID = model.DON_VI_BO_PHAN_ID;
			item.DON_VI_ID = model.DON_VI_ID;
			item.GHI_CHU = model.GHI_CHU;

			item.LOAI_BIEN_DONG_ID = model.LOAI_BIEN_DONG_ID;
			item.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;
			item.LOAI_TAI_SAN_ID = model.LOAI_TAI_SAN_ID;
			item.NGAY_SU_DUNG = model.NGAY_SU_DUNG;
			item.NGUON_VON_JSON = model.NGUON_VON_JSON;
			item.NGUYEN_GIA = model.NGUYEN_GIA;
			item.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY;
			item.QUYET_DINH_SO = model.QUYET_DINH_SO;
			item.TAI_SAN_ID = model.TAI_SAN_ID;
			item.TAI_SAN_MA = model.TAI_SAN_MA;
			item.TAI_SAN_TEN = model.TAI_SAN_TEN;

			return item;
		}

		#endregion YeuCau

		#region Prepare YeuCauModel from BienDong

		/// <summary>
		/// Description: Function prepare YeuCauModel from an BienDong's entity <br />
		/// Re
		/// </summary>
		/// <param name="model">YeuCauModel</param>
		/// <param name="entityBD">an BienDong's entity </param>
		/// <returns></returns>
		public YeuCauModel PrepareYeuCauModelFromBienDong(YeuCauModel model, TaiSan taiSan, YeuCau item)
		{
			var donviID = _workContext.CurrentDonVi.ID;
			decimal CheDoId = 0;
			model.YCCTModel = new YeuCauChiTietModel();

			#region Prepare Edit

			if (item != null)   //Edit
			{
				if (taiSan == null)
					taiSan = _taiSanService.GetTaiSanById(item.TAI_SAN_ID);
				model = item.ToModel<YeuCauModel>();
				Prepare_BD_ThongTin(model, taiSan);

				if (!String.IsNullOrEmpty(item.NGUON_VON_JSON))
				{
					model.lstNguonVonModel = item.NGUON_VON_JSON.toEntities<NguonVonModel>();
					model.SelectedNguonVonIds = model.lstNguonVonModel.Select(c => (int)c.ID).ToList();
				}
				var YCCT = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(model.ID);
				model.YCCTModel = YCCT.ToModel<YeuCauChiTietModel>();
				if (YCCT.DON_VI_NHAN_DIEU_CHUYEN_ID > 0)
				{
					model.DonViNhanDieuChuyenId = YCCT.DON_VI_NHAN_DIEU_CHUYEN_ID;
					model.DonViNhanDieuChuyenTen = YCCT.donvinhandieuchuyen.TEN;
				}

				if (!String.IsNullOrEmpty(model.YCCTModel.HTSD_JSON))
				{
					model.YCCTModel.lstHienTrang = model.YCCTModel.HTSD_JSON.toEntity<HienTrangList>().lstObjHienTrang;
					foreach (var itemHT in model.YCCTModel.lstHienTrang)
					{
						var hientrang = _hienTrangService.GetHienTrangById(itemHT.HienTrangId.Value);
						if (hientrang != null)
						{
							itemHT.TenHienTrang = hientrang.TEN_HIEN_TRANG;
						}
					}
				}

				if (item.LOAI_TAI_SAN_ID > 0)
					CheDoId = _loaiTaiSanService.GetLoaiTaiSanById(item.LOAI_TAI_SAN_ID ?? 0).CHE_DO_HAO_MON_ID ?? 0;
				else
					CheDoId = _cheDoHaoMonService.GetCheDoHaoMonByNgayNhap(model.NGAY_BIEN_DONG ?? DateTime.Now).ID;
			}
			if (model.lstNguonVonModel != null)
				foreach (var nv in model.lstNguonVonModel)
					nv.GiaTri = Math.Abs(nv.GiaTri ?? 0);


			#endregion Prepare Edit

			#region Prepare Insert

			else                //Insert
			{
				model.SelectedNguonVonIds = ((enumNGUON_VON_DEFAULT[])Enum.GetValues(typeof(enumNGUON_VON_DEFAULT))).Select(c => (int)c).ToList();

				model.lstNguonVonModel = _nguonVonModelFactory.PrepareListNguonVonDefault();
				// nếu tài sản dự án thì lấy hết list nguồn vốn
				if (model.IsTaiSanDuAn == true)
				{
					var lstNguonVon = _NguonVonService.GetAllNguonVonActive();
					if (lstNguonVon != null && lstNguonVon.Count > 0)
					{
						model.lstNguonVonModel = lstNguonVon.Select(p => p.ToModel<NguonVonModel>()).ToList();
						model.SelectedNguonVonIds = lstNguonVon.Select(p => Decimal.ToInt32(p.ID)).ToList();
					}
				}

				//model.NGAY_BIEN_DONG = DateTime.Now;

				#region Prepare biến động thông tin

				Prepare_BD_ThongTin(model, taiSan);

				//CheDoId = _cheDoHaoMonService.GetCheDoHaoMonByNgayNhap(taiSan.NGAY_NHAP ?? DateTime.Now).ID;
				CheDoId = _cheDoHaoMonService.GetNewestCheDoHaoMon().ID;
				#endregion Prepare biến động thông tin
			}

			#endregion Prepare Insert

			model = _yeuCauChiTietModelFactory.PrepareYeuCauChiTietTruocBD(model);
			//nếu là đất thì giá trị còn lại cũ bằng nguyên giá
			if (taiSan != null && taiSan.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
			{
				model.YCCTModel.HM_GIA_TRI_CON_LAI_CU = model.YCCTModel.NGUYEN_GIA_CU;
			}

			//prepare chung drop down list

			#region prepare chung drop down list

			model.LyDoTangAvailable = _lyDoBienDongModelFactory.PrepareSelectListLyDoBienDong(LoaiHinhTaiSanId: model.LOAI_HINH_TAI_SAN_ID, loailydoId: model.LOAI_BIEN_DONG_ID, isAddFirst: true, valSelected: model.LY_DO_BIEN_DONG_ID, strFirstRow: GetStringDefaultDDLLyDoBienDong(model.LOAI_BIEN_DONG_ID));
			model.NguonVonAvailable = _nguonVonModelFactory.PrepareMultiSelectNguonVon();
			if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
			{
				if (model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
					model.LoaiTaiSanAvailable = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(isAddFirst: true, loaiHinhTaiSanId: model.LOAI_HINH_TAI_SAN_ID, valSelected: model.LOAI_TAI_SAN_ID, cheDoId: CheDoId, isCreateOrEditTaiSan:true);
				else
					model.LoaiTaiSanAvailable = _loaiTaiSanVoHinhModelFactory.PrepareSelectListLoaiTaiSanDonVi(isAddFirst: true, loaiHinhTaiSanId: model.LOAI_HINH_TAI_SAN_ID, valSelected: model.LOAI_TAI_SAN_DON_VI_ID, cheDoId: CheDoId);
				model.BoPhanSuDungAvailable = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: donviID, isAddFirst: true, valSelected: model.DON_VI_BO_PHAN_ID);
			}

			#endregion prepare chung drop down list

			if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
			{
				var sumForEeachNguonVon = _taiSanNguonVonService.GetAllSumNguonVonNumberOfTaiSan(model.TAI_SAN_ID, model.NGAY_BIEN_DONG ?? DateTime.Now);
				model.lstNguonVonBD = ToListNguonVonBDModel(sumForEeachNguonVon, model.lstNguonVonModel);
				model.NGUYEN_GIA = Math.Abs(model.NGUYEN_GIA ?? 0);
				model.NGUYEN_GIA = Math.Abs(model.NGUYEN_GIA ?? 0);
				model.YCCTModel.NHA_DIEN_TICH_XD = Math.Abs(model.YCCTModel.NHA_DIEN_TICH_XD ?? 0);
				model.YCCTModel.NHA_TONG_DIEN_TICH_XD = Math.Abs(model.YCCTModel.NHA_TONG_DIEN_TICH_XD ?? 0);
				model.YCCTModel.DAT_TONG_DIEN_TICH = Math.Abs(model.YCCTModel.DAT_TONG_DIEN_TICH ?? 0);
				model.YCCTModel.VKT_CHIEU_DAI = Math.Abs(model.YCCTModel.VKT_CHIEU_DAI ?? 0);
				model.YCCTModel.VKT_DIEN_TICH = Math.Abs(model.YCCTModel.VKT_DIEN_TICH.GetValueOrDefault());
				model.YCCTModel.VKT_THE_TICH = Math.Abs(model.YCCTModel.VKT_THE_TICH ?? 0);
				//prepare thông tin đất (trụ sở) mà tài sản nhà được gắn 
				if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA)
				{
					var ts_Nha = _taisannhaService.GetTaiSanNhaByTaiSanId(model.TAI_SAN_ID);
					if (ts_Nha.TAI_SAN_DAT_ID > 0)
					{
						var ts_Dat = _taiSanService.GetTaiSanById(ts_Nha.TAI_SAN_DAT_ID.Value);
						if (ts_Dat != null)
						{
							model.NhaMaTruSo = ts_Dat.MA;
							model.NhaTenTruSo = ts_Dat.TEN;
						}
					}
				}
			}
			model.IsHaveHS = CheckHaveHS(model.YCCTModel);


			return model;
		}

		private bool CheckHaveHS(YeuCauChiTietModel model)
		{
			if (model.HS_QUYET_DINH_BAN_GIAO != null ||
				model.HS_QUYET_DINH_BAN_GIAO_NGAY != null ||
				model.HS_BIEN_BAN_NGHIEM_THU != null ||
				model.HS_BIEN_BAN_NGHIEM_THU_NGAY != null ||
				model.HS_PHAP_LY_KHAC != null ||
				model.HS_PHAP_LY_KHAC_NGAY != null ||
				model.HS_CNQSD_SO != null ||
				model.HS_CNQSD_NGAY != null ||
				model.HS_QUYET_DINH_GIAO_SO != null ||
				model.HS_QUYET_DINH_GIAO_NGAY != null ||
				model.HS_CHUYEN_NHUONG_SD_SO != null ||
				model.HS_CHUYEN_NHUONG_SD_NGAY != null ||
				model.HS_QUYET_DINH_CHO_THUE_SO != null ||
				model.HS_QUYET_DINH_CHO_THUE_NGAY != null ||
				model.HS_KHAC != null
				)
				return true;
			return false;
		}

		private void Prepare_BD_ThongTin(YeuCauModel yeuCauModel, TaiSan taiSan)
		{
			yeuCauModel.TAI_SAN_ID = taiSan.ID;
			yeuCauModel.TAI_SAN_MA = taiSan.MA;
			yeuCauModel.TAI_SAN_TEN = yeuCauModel.TAI_SAN_TEN ?? taiSan.TEN;
			yeuCauModel.LOAI_TAI_SAN_ID = yeuCauModel.LOAI_TAI_SAN_ID ?? taiSan.LOAI_TAI_SAN_ID;
			yeuCauModel.LOAI_TAI_SAN_DON_VI_ID = yeuCauModel.LOAI_TAI_SAN_DON_VI_ID ?? taiSan.LOAI_TAI_SAN_DON_VI_ID;
			yeuCauModel.DON_VI_BO_PHAN_ID = yeuCauModel.DON_VI_BO_PHAN_ID ?? taiSan.DON_VI_BO_PHAN_ID;
			yeuCauModel.QUYET_DINH_SO = yeuCauModel.QUYET_DINH_SO ?? string.Empty;
			yeuCauModel.QUYET_DINH_NGAY = yeuCauModel.QUYET_DINH_NGAY ?? null;
			yeuCauModel.LOAI_HINH_TAI_SAN_ID = taiSan.LOAI_HINH_TAI_SAN_ID;
			if (taiSan.DON_VI_BO_PHAN_ID > 0)
			{
				var donViBoPhan = _donViBoPhanService.GetDonViBoPhanById(taiSan.DON_VI_BO_PHAN_ID ?? 0);
				yeuCauModel.TenDonViBoPhan = donViBoPhan?.TEN;
			}
			if (taiSan.LY_DO_BIEN_DONG_ID.HasValue && taiSan.LY_DO_BIEN_DONG_ID > 0)
			{
				var lyDoBienDong = _lydobiendongService.GetLyDoBienDongById(taiSan.LY_DO_BIEN_DONG_ID ?? 0);
				yeuCauModel.TenLyDoBienDong = lyDoBienDong?.TEN;
			}
			if ((taiSan.LOAI_TAI_SAN_ID.HasValue && taiSan.LOAI_TAI_SAN_ID > 0) || (taiSan.LOAI_TAI_SAN_DON_VI_ID.HasValue && taiSan.LOAI_TAI_SAN_DON_VI_ID > 0))
			{
				var loaiTaiSan = new LoaiTaiSanModel();
				if (taiSan.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
					loaiTaiSan = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(taiSan.LOAI_TAI_SAN_DON_VI_ID ?? 0).ToModel<LoaiTaiSanModel>();
				else
					loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(taiSan.LOAI_TAI_SAN_ID ?? 0).ToModel<LoaiTaiSanModel>();
				yeuCauModel.TenLoaiTaiSan = loaiTaiSan?.TEN;
			}
			if (taiSan.DON_VI_ID.HasValue && taiSan.DON_VI_ID > 0)
			{
				var donVi = _donViService.GetDonViById(taiSan.DON_VI_ID ?? 0);
				yeuCauModel.TenDonVi = donVi?.TEN;
			}
		}

		public YeuCau PrepareYeuCauFromYeuCauCu(YeuCau _new, YeuCau _old = null)
		{
			if (_new == null)
				return new YeuCau();
			if (_old == null)
				_old = _itemService.GetYeuCauMoiNhatByTaiSanId(_new.TAI_SAN_ID);
			_new.TAI_SAN_ID = _new.TAI_SAN_ID;
			_new.TAI_SAN_MA = _new.TAI_SAN_MA ?? _old.TAI_SAN_MA;
			_new.TAI_SAN_TEN = _new.TAI_SAN_TEN ?? _old.TAI_SAN_TEN;
			_new.LOAI_TAI_SAN_ID = _new.LOAI_TAI_SAN_ID ?? _old.LOAI_TAI_SAN_ID;
			_new.LOAI_HINH_TAI_SAN_ID = _new.LOAI_HINH_TAI_SAN_ID ?? _old.LOAI_HINH_TAI_SAN_ID;
			_new.NGUYEN_GIA = _new.NGUYEN_GIA ?? _old.NGUYEN_GIA;
			_new.DON_VI_BO_PHAN_ID = _new.DON_VI_BO_PHAN_ID ?? _old.DON_VI_BO_PHAN_ID;
			_new.CHUNG_TU_SO = _new.CHUNG_TU_SO ?? _old.CHUNG_TU_SO;
			_new.CHUNG_TU_NGAY = _new.CHUNG_TU_NGAY ?? _old.CHUNG_TU_NGAY;
			_new.NGAY_BIEN_DONG = _new.NGAY_BIEN_DONG ?? _old.NGAY_BIEN_DONG;
			_new.NGAY_SU_DUNG = _new.NGAY_SU_DUNG ?? _old.NGAY_SU_DUNG;
			_new.LOAI_BIEN_DONG_ID = _new.LOAI_BIEN_DONG_ID ?? _old.LOAI_BIEN_DONG_ID;
			_new.LY_DO_BIEN_DONG_ID = _new.LY_DO_BIEN_DONG_ID ?? _old.LY_DO_BIEN_DONG_ID;
			_new.TRANG_THAI_ID = _new.TRANG_THAI_ID;
			_new.DON_VI_ID = _new.DON_VI_ID ?? _old.DON_VI_ID;
			_new.GHI_CHU = _new.GHI_CHU ?? _old.GHI_CHU;
			_new.NGUON_VON_JSON = _new.NGUON_VON_JSON ?? _old.NGUON_VON_JSON;
			return _new;
		}

		public void UpdateYeuCauTuChoi(YeuCau yeuCau, string Note)
		{
			yeuCau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.TU_CHOI;
			yeuCau.LY_DO_KHONG_DUYET = Note;
			_itemService.UpdateYeuCau(yeuCau);
		}

		#endregion Prepare YeuCauModel from BienDong

		#region GiamTaiSan
		/// <summary>
		/// Chuẩn bị giảm nhiều tài sản. Chỉ cho tăng mới, không cho sửa
		/// </summary>
		/// <param name="model"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		public YeuCauModel PrepareYeuCauModelGiamNhieuTaiSan(YeuCauModel model, YeuCau item)
		{
			model.YCCTModel = new YeuCauChiTietModel();
			model.ddlPhuongAnXuLy = ((enumHINH_THUC_XU_LY_TSC)model.enumPhuongAnXuLy).ToSelectList();
			model.LyDoTangAvailable = _lyDoBienDongModelFactory.PrepareSelectListLyDoBienDong(LoaiHinhTaiSanId: (int)enumLOAI_HINH_TAI_SAN.ALL, loailydoId: (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO, isAddFirst: true, valSelected: model.LY_DO_BIEN_DONG_ID, strFirstRow: "-- Chọn lý do giảm --");
			return model;
		}
		public YeuCauModel PrepareYeuCauModelGiamTaiSan(YeuCauModel model, YeuCau item)
		{
			model.YCCTModel = new YeuCauChiTietModel();
			if (item != null)
			{
				model = item.ToModel<YeuCauModel>();
				var ycct = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(item.ID);
				model.YCCTModel = ycct.ToModel<YeuCauChiTietModel>();
				model.HINH_THUC_XU_LY_ID = ycct.HINH_THUC_XU_LY_ID;
				model.DonViNhanDieuChuyenId = ycct.DON_VI_NHAN_DIEU_CHUYEN_ID;
				if (ycct.DON_VI_NHAN_DIEU_CHUYEN_ID > 0)
				{
					var dvNhanDieuChuyen = _donViService.GetDonViById(ycct.DON_VI_NHAN_DIEU_CHUYEN_ID ?? 0);
					model.DonViNhanDieuChuyenTen = dvNhanDieuChuyen.TEN;
				}
			}
			var ts = _taiSanService.GetTaiSanById(model.TAI_SAN_ID);
			model.LOAI_HINH_TAI_SAN_ID = ts.LOAI_HINH_TAI_SAN_ID;
			model.LOAI_TAI_SAN_ID = ts.LOAI_TAI_SAN_ID;
			model.TenDonVi = ts.donvi.TEN;
			model.TAI_SAN_MA = ts.MA;
			model.TAI_SAN_TEN = ts.TEN;
			model.DU_AN_ID = ts.DU_AN_ID;
			if (ts.DU_AN_ID > 0)
			{
				var duAn = _duAnService.GetDuAnById(ts.DU_AN_ID ?? 0);
				model.DU_AN_TEN = duAn.TEN;
			}
			if (model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
				model.TenLoaiTaiSan = ts.loaitaisan.TEN;
			else
			{
				var loaiTSVoHinh = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(model.LOAI_TAI_SAN_DON_VI_ID ?? 0);
				if (loaiTSVoHinh != null)
					model.TenLoaiTaiSan = loaiTSVoHinh.TEN;
			}
			if (ts.DON_VI_BO_PHAN_ID > 0)
			{
				model.TenDonViBoPhan = ts.donvibophan.TEN;
			}
			if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
			{
                //int[] hinhThucLoaiBo = { (int)enumHINH_THUC_XU_LY_TSC.PHA_DO_HUY_BO };
                //model.ddlPhuongAnXuLy = ((enumHINH_THUC_XU_LY_TSC)model.enumPhuongAnXuLy).ToSelectList(valuesToExclude: hinhThucLoaiBo);
                model.ddlPhuongAnXuLy = ((enumHINH_THUC_XU_LY_TSC)model.enumPhuongAnXuLy).ToSelectList();
			}
			else
				model.ddlPhuongAnXuLy = ((enumHINH_THUC_XU_LY_TSC)model.enumPhuongAnXuLy).ToSelectList();
			model.LyDoTangAvailable = _lyDoBienDongModelFactory.PrepareSelectListLyDoBienDong(LoaiHinhTaiSanId: model.LOAI_HINH_TAI_SAN_ID, loailydoId: model.LOAI_BIEN_DONG_ID, isAddFirst: true, valSelected: model.LY_DO_BIEN_DONG_ID, strFirstRow: "-- Chọn lý do giảm --");
			model.YCCTModel.NGUYEN_GIA_CU = _bienDongService.TinhNguyenGiaTaiSanOnlyBD(taiSanId: model.TAI_SAN_ID, To_Date_BienDong: model.NGAY_BIEN_DONG ?? DateTime.Now);
			return model;
		}

		public YeuCau PrepareYeuCauGiamTaiSan(YeuCauModel model, YeuCau item)
		{
			var taisan = _taiSanService.GetTaiSanById(model.TAI_SAN_ID);
			item.TAI_SAN_ID = taisan.ID;
			item.TAI_SAN_MA = taisan.MA;
			item.TAI_SAN_TEN = taisan.TEN;
			item.LOAI_TAI_SAN_ID = taisan.LOAI_TAI_SAN_ID;
			item.LOAI_HINH_TAI_SAN_ID = taisan.LOAI_HINH_TAI_SAN_ID;
			item.DON_VI_BO_PHAN_ID = taisan.DON_VI_BO_PHAN_ID;
			item.QUYET_DINH_SO = model.QUYET_DINH_SO;
			item.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY;
			item.CHUNG_TU_SO = model.CHUNG_TU_SO;
			item.CHUNG_TU_NGAY = model.CHUNG_TU_NGAY;
			item.NGAY_BIEN_DONG = model.NGAY_BIEN_DONG;
			item.LOAI_BIEN_DONG_ID = model.LOAI_BIEN_DONG_ID;
			item.LY_DO_BIEN_DONG_ID = model.LY_DO_BIEN_DONG_ID;
			item.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
			item.GHI_CHU = model.GHI_CHU;
			item.NGAY_SU_DUNG = taisan.NGAY_SU_DUNG;
			return item;
		}

		public YeuCau PrepareYeuCauForDieuChuyenKemTheo(YeuCau ycDieuChuyenKem, TaiSan taisan)
		{
			var _yeucau = new YeuCau(ycDieuChuyenKem);
			_yeucau.ID = 0;
			_yeucau.TAI_SAN_ID = taisan.ID;
			_yeucau.TAI_SAN_MA = taisan.MA;
			_yeucau.TAI_SAN_TEN = taisan.TEN;
			_yeucau.LOAI_HINH_TAI_SAN_ID = taisan.LOAI_HINH_TAI_SAN_ID;
			_yeucau.LOAI_TAI_SAN_ID = taisan.LOAI_TAI_SAN_ID;
			return _yeucau;
		}

		#endregion GiamTaiSan

		#region Validation

		public bool IsTrungNgayBD(DateTime? ngay_BD, decimal TaiSanId, decimal YCID)
		{
			var count = _itemService.CountYeuCauTrungNgayBD(TaiSanId, ngay_BD, YCID);
			if (count > 0)
				return false;
			return true;
		}

		public bool IsLonHonNgayBD(DateTime? ngay_BD, decimal TaiSanId, decimal YCID)
		{
			var count = _itemService.CountYeuCauLonHonNgayBD(TaiSanId, ngay_BD, YCID);
			if (count > 0)
				return true;
			return false;
		}
		public bool IsNgayBienDongMoiNhat(DateTime? ngay_BD, string strTaiSanIds)
		{
			var listTaiSanId = strTaiSanIds.Split(',').Select(p => int.Parse(p)).ToList();
			foreach (var taiSanId in listTaiSanId)
			{
				if (!IsNgayBienDongMoiNhat(ngay_BD, taiSanId, 0))
					return false;
			}
			return true;
		}
		public bool IsNgayBienDongMoiNhat(DateTime? ngay_BD, decimal TaiSanId, decimal YCID)
		{
			if (ngay_BD == null || TaiSanId <= 0)
				return false;
			//nếu là sửa yêu cầu mà không sửa ngày
			if (YCID > 0)
			{
				var yc_edit = _itemService.GetYeuCauById(YCID);
				if (yc_edit != null && yc_edit.NGAY_BIEN_DONG == ngay_BD)//edit không thay đổi ngày biến động
					return true;
			}
			BienDong bdGanNhat = _bienDongService.GetBienDongMoiNhatByTaiSanId(taiSanId: TaiSanId);
			//ngày biến động mới phải có số ngày lớn hơn ngày biến động cũ. Nếu không có biến động cũ thì cho tạo
			if (bdGanNhat != null && bdGanNhat.NGAY_BIEN_DONG >= ngay_BD.Value)
				return false;
			else if (bdGanNhat == null)
			{
				var yc_TangMoi = _itemService.GetYeuCauCuNhatByTSId(TaiSanId);
				if (yc_TangMoi == null || yc_TangMoi.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO || (yc_TangMoi.NGAY_BIEN_DONG >= ngay_BD.Value))
					return false;
			}
			return true;
		}

		public bool CheckDonViDieuChuyen(YeuCauModel model)
		{
			var lyDoEntity = _lydobiendongService.GetLyDoBienDongById(model.LY_DO_BIEN_DONG_ID ?? 0);
			if (lyDoEntity == null)
				return false;
			var idLyDoDieuChuyen = _lydobiendongService.GetIdLyDoBienDongByMa(enum_LY_DO_BIEN_DONG.DIEU_CHUYEN);
			if (lyDoEntity.ID == idLyDoDieuChuyen || lyDoEntity.LOAI_LY_DO_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
			{
				if (lyDoEntity != null
					&& model.YCCTModel != null && model.YCCTModel.DON_VI_NHAN_DIEU_CHUYEN_ID > 0)
					return true;
				else
					return false;
			}
			else
				return true;
		}
		public bool CheckTrungDonViDieuChuyen(YeuCauModel model)
		{
			var lyDoEntity = _lydobiendongService.GetLyDoBienDongById(model.LY_DO_BIEN_DONG_ID ?? 0);
			if (lyDoEntity == null)
				return false;
			var idLyDoDieuChuyen = _lydobiendongService.GetIdLyDoBienDongByMa(enum_LY_DO_BIEN_DONG.DIEU_CHUYEN);
			if (lyDoEntity.ID == idLyDoDieuChuyen || lyDoEntity.LOAI_LY_DO_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
				if (model.YCCTModel != null && model.YCCTModel.DON_VI_NHAN_DIEU_CHUYEN_ID > 0 && _workContext.CurrentDonVi.ID == model.YCCTModel.DON_VI_NHAN_DIEU_CHUYEN_ID)
					return false;
			return true;
		}

		public bool CheckEachNguonVonMinusIsValid(decimal TaiSanId, string NguonVonJson)
		{
			if (!string.IsNullOrEmpty(NguonVonJson))
			{
				var lstNguonVon = NguonVonJson.toEntities<NguonVonModel>();
				if (lstNguonVon != null)
				{
					//lấy ra danh sách nguồn vốn hiện tại của tài sản
					var lstNguonVonOfTS = _taiSanNguonVonService.GetAllSumNguonVonNumberOfTaiSan(TaiSanId);
					if (lstNguonVonOfTS == null || lstNguonVonOfTS.Count == 0)
						return false;
					foreach (var tsNguonVon in lstNguonVonOfTS)
					{
						var nguonVon = lstNguonVon.Where(p => p.ID == tsNguonVon.NGUON_VON_ID).FirstOrDefault();
						//nếu có nguồn vốn tức là có thay đổi phần nguồn vốn này
						//nếu giá trị giảm lớn hơn cho phép thì sẽ trả về false
						if (nguonVon != null && Math.Abs(nguonVon.GiaTri ?? 0) > Math.Abs(tsNguonVon.GIA_TRI))
						{
							return false;
						}
					}
				}
			}
			return true;
		}
		public bool CheckDienTichBienDong(YeuCauModel model)
		{
			var maLyDoBienDong = _lydobiendongService.GetLyDoBienDongById(model.LY_DO_BIEN_DONG_ID.GetValueOrDefault())?.MA;
			if (string.IsNullOrEmpty(maLyDoBienDong))
				return false;
			if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
			{
				if (maLyDoBienDong == enum_LY_DO_BIEN_DONG.TANG_DIEN_TICH_DAT ||
					//maLyDoBienDong == enum_LY_DO_BIEN_DONG.TANG_NANG_CAP_MO_RONG_SUA_CHUA ||
					maLyDoBienDong == enum_LY_DO_BIEN_DONG.GIAM_DIEN_TICH_DAT ||
					maLyDoBienDong == enum_LY_DO_BIEN_DONG.GIAM_CAI_TAO_THU_HEP_DIEN_TICH)
				{
					if (model.YCCTModel?.DAT_TONG_DIEN_TICH > 0)
						return true;
					return false;
				}
			}
			else if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA)
			{
				if (maLyDoBienDong == enum_LY_DO_BIEN_DONG.TANG_DIEN_TICH_DAT ||
					//maLyDoBienDong == enum_LY_DO_BIEN_DONG.TANG_NANG_CAP_MO_RONG_SUA_CHUA ||
					maLyDoBienDong == enum_LY_DO_BIEN_DONG.GIAM_DIEN_TICH_DAT ||
					maLyDoBienDong == enum_LY_DO_BIEN_DONG.GIAM_CAI_TAO_THU_HEP_DIEN_TICH)
				{
					if (model.YCCTModel?.NHA_TONG_DIEN_TICH_XD > 0)
						return true;
					return false;
				}
			}

			return true;
		}

		#endregion Validation

		#region Function
		private IList<NguonVonBDModel> ToListNguonVonBDModel(IList<TaiSanNguonVon> taiSanNguonVons = null, IList<NguonVonModel> nguonVonModels = null)
		{
			var res = new List<NguonVonBDModel>();
			if (taiSanNguonVons == null)
				return res;
			if (nguonVonModels != null)
			{
				foreach (var item in taiSanNguonVons)
				{
					var nguonVon = _NguonVonService.GetNguonVonById(item.NGUON_VON_ID);
					var TenNguonVon = nguonVon != null ? nguonVon.TEN : "";
					var nguonVonModel = nguonVonModels.Where(p => p.ID == item.NGUON_VON_ID).FirstOrDefault();
					var nguonVonBDModel = new NguonVonBDModel(item, nguonVonModel, TenNguonVon);
					res.Add(nguonVonBDModel);
				}
			}
			else
			{
				foreach (var item in taiSanNguonVons)
				{
					var nguonVon = _NguonVonService.GetNguonVonById(item.NGUON_VON_ID);
					var TenNguonVon = nguonVon != null ? nguonVon.TEN : "";
					var nguonVonBDModel = new NguonVonBDModel(item, null, TenNguonVon);
					res.Add(nguonVonBDModel);
				}
			}
			return res;
		}
		private string GetStringDefaultDDLLyDoBienDong(decimal? LoaiBienDong)
		{
			string defaultString = "-- Chọn lý do biến động -- ";
			switch (LoaiBienDong)
			{
				case (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO:
				case (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI:
					defaultString = "-- Chọn lý do tăng --";
					break;
				case (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI:
				case (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO:
					defaultString = "-- Chọn lý do giảm --";
					break;
				case (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN:
					defaultString = "-- Chọn lý do điều chuyển --";
					break;
				case (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN:
					defaultString = "-- Chọn lý do thay đổi --";
					break;
				default:
					break;
			}
			return defaultString;
		}

        public bool CapNhatThongTinDiaBanAndHienTrang(YeuCauModel model)
        {
			return true;
		}

        #endregion
    }
}