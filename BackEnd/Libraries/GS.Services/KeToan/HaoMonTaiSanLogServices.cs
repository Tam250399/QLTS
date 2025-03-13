//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 22/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Data;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Data.Extensions;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Core.Domain.KT;
using GS.Core.Domain.TaiSans;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.Api.TaiSanDBApi;
using Oracle.ManagedDataAccess.Client;

namespace GS.Services.KT
{
	public partial class HaoMonTaiSanLogService : IHaoMonTaiSanLogService
	{
		#region Fields
		private readonly CauHinhChung _cauhinhChung;
		private readonly ICacheManager _cacheManager;
		private readonly IDataProvider _dataProvider;
		private readonly IDbContext _dbContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly IRepository<HaoMonTaiSanLog> _itemRepository;
		private readonly IRepository<HaoMonTaiSan> _haoMonTaiSanRepository;
		private readonly IRepository<KhauHaoTaiSan> _khauHaoTaiSanRepository;
		private readonly IRepository<TaiSan> _taiSanRepository;
		private readonly IRepository<LoaiTaiSan> _loaiTaiSanRepository;
		private readonly IRepository<LoaiTaiSanKhauHao> _loaiTaiSanKhauHaoRepository;
		private readonly IRepository<BienDong> _bienDongRepository;
		private readonly IRepository<BienDongChiTiet> _bienDongChiTietRepository;
		#endregion

		#region Ctor

		public HaoMonTaiSanLogService(CauHinhChung cauhinhChung,
			ICacheManager cacheManager,
			IDataProvider dataProvider,
			IDbContext dbContext,
			IStaticCacheManager staticCacheManager,
			IRepository<HaoMonTaiSanLog> itemRepository,
			IRepository<HaoMonTaiSan> haoMonTaiSanRepository,
			IRepository<KhauHaoTaiSan> khauHaoTaiSanRepository,
			IRepository<TaiSan> taiSanRepository,
			IRepository<LoaiTaiSan> loaiTaiSanRepository,
			IRepository<LoaiTaiSanKhauHao> loaiTaiSanKhauHaoRepository,
			IRepository<BienDong> bienDongRepository,
			IRepository<BienDongChiTiet> bienDongChiTietRepository
			)
		{
			this._cauhinhChung = cauhinhChung;
			this._cacheManager = cacheManager;
			this._dataProvider = dataProvider;
			this._dbContext = dbContext;
			this._staticCacheManager = staticCacheManager;
			this._itemRepository = itemRepository;
			this._haoMonTaiSanRepository = haoMonTaiSanRepository;
			this._khauHaoTaiSanRepository = khauHaoTaiSanRepository;
			this._taiSanRepository = taiSanRepository;
			this._loaiTaiSanRepository = loaiTaiSanRepository;
			this._loaiTaiSanKhauHaoRepository = loaiTaiSanKhauHaoRepository;
			this._bienDongRepository = bienDongRepository;
			this._bienDongChiTietRepository = bienDongChiTietRepository;
		}

		#endregion

		#region Methods
		public virtual IList<HaoMonTaiSanLog> GetAllHaoMonTaiSanLogs()
		{
			var query = _itemRepository.Table;
			return query.ToList();
		}

		public virtual IPagedList<HaoMonTaiSanLog> SearchHaoMonTaiSanLogs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
		{
			var query = _itemRepository.Table;
			return new PagedList<HaoMonTaiSanLog>(query, pageIndex, pageSize); ;
		}

		public virtual HaoMonTaiSanLog GetHaoMonTaiSanLogById(decimal Id)
		{
			if (Id == 0)
				return null;
			return _itemRepository.GetById(Id);
		}

		public virtual IList<HaoMonTaiSanLog> GetHaoMonTaiSanLogByIds(decimal[] Ids)
		{
			var query = _itemRepository.Table;
			return query.Where(c => Ids.Contains(c.ID)).ToList();
		}
		public virtual HaoMonTaiSanLog GetHaoMonTaiSanLogByTaiSanId(decimal TaiSanId)
		{
			if (TaiSanId > 0)
				return _itemRepository.Table.Where(p => p.TAI_SAN_ID == TaiSanId).OrderBy(p => p.NAM_TINH).FirstOrDefault();
			return null;
		}


		public virtual void InsertHaoMonTaiSanLog(HaoMonTaiSanLog entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Insert(entity);
			//event notification
			//_eventPublisher.EntityInserted(entity);

		}
		public virtual void UpdateHaoMonTaiSanLog(HaoMonTaiSanLog entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Update(entity);
			//event notification
			//_eventPublisher.EntityUpdated(entity);            
		}
		public virtual void DeleteHaoMonTaiSanLog(HaoMonTaiSanLog entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Delete(entity);
			//event notification
			//_eventPublisher.EntityDeleted(entity);
		}

		public HaoMonTaiSan ChotHaoMon(decimal taiSanId, decimal namHaoMon)
		{
			if (!(taiSanId > 0))
				return null;
			HaoMonTaiSan haoMonTaiSan_new = new HaoMonTaiSan();
			bool has_hmts_LastYear = false;
			bool has_BDChot_ThisYear = false;
			DateTime BDChot_NgayBienDong = new DateTime(); //ngày biến động được người nhập chốt gtcl => k cần tính lại gtcl của bd này. Tính tiếp từ ngày này
			BienDongChiTiet BDChot_bdct = new BienDongChiTiet(); //Lưu thông tin biến động chốt giá trị còn lại
																 //Lấy các thông tin liên quan tới tài sản: các biến động, loại tài sản,...
			KTGiaTriTaiSan ktGiaTriTaiSan = new KTGiaTriTaiSan(); //Lưu trữ các thông tin cần để tính toán các giá trị
			TaiSan taiSanEntity = _taiSanRepository.Table.Where(c => c.ID == taiSanId).FirstOrDefault();
			LoaiTaiSan loaiTaiSan = _loaiTaiSanRepository.Table.Where(c => c.ID == taiSanEntity.LOAI_TAI_SAN_ID).FirstOrDefault();
			var listBienDongTaiSan = _bienDongRepository.Table.Where(c => c.TAI_SAN_ID == taiSanId);
			var listBienDongChiTietTaiSan = _bienDongChiTietRepository.Table.Where(c => c.biendong.TAI_SAN_ID == taiSanId && c.biendong.NGAY_BIEN_DONG.Value.Year <= namHaoMon);
			//Lấy biến động đầu tiên (bd tăng mới, nhập số dư sinh ra ts)
			BienDong bdTangTS = listBienDongTaiSan.OrderBy(c => c.NGAY_BIEN_DONG).FirstOrDefault();
			BienDongChiTiet bdctTangTS = _bienDongChiTietRepository.Table.Where(c => c.BIEN_DONG_ID == bdTangTS.ID).FirstOrDefault();
			//Tỷ lệ nguyên giá hao mòn, khấu hao
			ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH = ((bdctTangTS.KH_GIA_TINH_KHAU_HAO ?? 0) / bdTangTS.NGUYEN_GIA);
			ktGiaTriTaiSan.TS_TyLeNguyenGiaTinhHM = 1 - (ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH);
			//
			ktGiaTriTaiSan.LoaiHinhTaiSanId = taiSanEntity.LOAI_HINH_TAI_SAN_ID;
			if (taiSanEntity != null && taiSanEntity.HM_TY_LE > 0)
			{
				ktGiaTriTaiSan.HM_TyLe = taiSanEntity.HM_TY_LE;
			}
			else
			{
				if (taiSanEntity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
				{
					ktGiaTriTaiSan.HM_TyLe = taiSanEntity.loaitaisandonvi.HM_TY_LE;
				}
				else
				{
					ktGiaTriTaiSan.HM_TyLe = taiSanEntity.loaitaisan.HM_TY_LE;
				}
			}
			//Lấy bản ghi hao mòn kt năm trước (nếu có)
			HaoMonTaiSan hmts_lastYear = _haoMonTaiSanRepository.Table.Where(c => c.TAI_SAN_ID == taiSanId && c.NAM_HAO_MON == namHaoMon - 1).FirstOrDefault();

			//không có bản ghi trước
			if (hmts_lastYear == null)
			{
				has_hmts_LastYear = false;
				hmts_lastYear = new HaoMonTaiSan();
				hmts_lastYear.TONG_NGUYEN_GIA = bdTangTS.NGUYEN_GIA * ktGiaTriTaiSan.TS_TyLeNguyenGiaTinhHM;
				if (taiSanEntity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || taiSanEntity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
				{
					hmts_lastYear.TONG_GIA_TRI_CON_LAI = bdTangTS.NGUYEN_GIA;
					hmts_lastYear.TONG_HAO_MON_LUY_KE = 0;
				}
				else
				{
					//bdctDauTien.HM_Gia_tri_con_lai: là giá trị còn lại sau khấu hao lẫn hao mòn
					hmts_lastYear.TONG_GIA_TRI_CON_LAI = bdctTangTS.HM_GIA_TRI_CON_LAI * ktGiaTriTaiSan.TS_TyLeNguyenGiaTinhHM;
					hmts_lastYear.TONG_HAO_MON_LUY_KE = bdTangTS.NGUYEN_GIA * ktGiaTriTaiSan.TS_TyLeNguyenGiaTinhHM - bdctTangTS.HM_GIA_TRI_CON_LAI * ktGiaTriTaiSan.TS_TyLeNguyenGiaTinhHM;
				}
				BDChot_NgayBienDong = bdTangTS.NGAY_BIEN_DONG.Value;
			}
			else
			{
				has_hmts_LastYear = true;
			}
			//check bien dong trong năm
			var listBDCTthisYear = listBienDongChiTietTaiSan.Where(c => c.biendong.NGAY_BIEN_DONG.Value.Year == namHaoMon);
			if (listBDCTthisYear != null && listBDCTthisYear.Count() > 0)
			{
				//lấy biến động đã được chốt giá trị
				var listBDCTChotGTCL = listBDCTthisYear.Where(c => c.biendong.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO && c.biendong.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY && c.HM_GIA_TRI_CON_LAI != null).OrderBy(c => c.biendong.NGAY_BIEN_DONG);
				if (listBDCTChotGTCL != null && listBDCTChotGTCL.Count() > 0)
				{
					has_BDChot_ThisYear = true;
					BDChot_bdct = listBDCTChotGTCL.FirstOrDefault();
					hmts_lastYear.TONG_GIA_TRI_CON_LAI = BDChot_bdct.HM_GIA_TRI_CON_LAI;
					BDChot_NgayBienDong = BDChot_bdct.biendong.NGAY_BIEN_DONG.Value;
				}
				else
				{
					has_BDChot_ThisYear = false;
				}
				//lấy các thông tin chốt + các biến động tiếp sau
				var list_bdctSauChot = listBDCTthisYear.Where(c => c.biendong.NGAY_BIEN_DONG > BDChot_NgayBienDong);
				if (list_bdctSauChot != null && list_bdctSauChot.Count() > 0)
				{
					ktGiaTriTaiSan.TS_NguyenGiaBienDong = list_bdctSauChot.Sum(c => c.biendong.NGUYEN_GIA);
					hmts_lastYear.TONG_HAO_MON_LUY_KE = ((hmts_lastYear.TONG_NGUYEN_GIA ?? 0) + (ktGiaTriTaiSan.TS_NguyenGiaBienDong ?? 0) * ktGiaTriTaiSan.TS_TyLeNguyenGiaTinhHM) - (hmts_lastYear.TONG_GIA_TRI_CON_LAI ?? 0);
				}
			}
			//Tính toán
			ktGiaTriTaiSan.HM_GiaTriTinh = hmts_lastYear.TONG_GIA_TRI_CON_LAI + ((ktGiaTriTaiSan.TS_NguyenGiaBienDong ?? 0) * ktGiaTriTaiSan.TS_TyLeNguyenGiaTinhHM);
			ktGiaTriTaiSan.TS_NguyenGiaTinhHM = (listBienDongTaiSan.Where(c => c.NGAY_BIEN_DONG.Value.Year <= namHaoMon).Sum(c => c.NGUYEN_GIA)) * ktGiaTriTaiSan.TS_TyLeNguyenGiaTinhHM;
			haoMonTaiSan_new.TONG_NGUYEN_GIA = ktGiaTriTaiSan.TS_NguyenGiaTinhHM;
			haoMonTaiSan_new.TY_LE_HAO_MON = ktGiaTriTaiSan.HM_TyLe;
			haoMonTaiSan_new.MA_TAI_SAN = taiSanEntity.MA;
			if (BDChot_NgayBienDong.Day == 31 && BDChot_NgayBienDong.Month == 12)
			{
				haoMonTaiSan_new.GIA_TRI_HAO_MON = 0;
			}
			else
			{
				haoMonTaiSan_new.GIA_TRI_HAO_MON = Math.Round((ktGiaTriTaiSan.HM_TyLe ?? 0) * (ktGiaTriTaiSan.TS_NguyenGiaTinhHM ?? 0) / 100);
			}
			if (haoMonTaiSan_new.GIA_TRI_HAO_MON > ktGiaTriTaiSan.HM_GiaTriTinh)
			{
				haoMonTaiSan_new.TONG_HAO_MON_LUY_KE = hmts_lastYear.TONG_HAO_MON_LUY_KE + ktGiaTriTaiSan.HM_GiaTriTinh;
				haoMonTaiSan_new.TONG_GIA_TRI_CON_LAI = 0;
			}
			else
			{
				haoMonTaiSan_new.TONG_HAO_MON_LUY_KE = hmts_lastYear.TONG_HAO_MON_LUY_KE + haoMonTaiSan_new.GIA_TRI_HAO_MON;
				haoMonTaiSan_new.TONG_GIA_TRI_CON_LAI = ktGiaTriTaiSan.HM_GiaTriTinh - haoMonTaiSan_new.GIA_TRI_HAO_MON;
			}
			if (!has_hmts_LastYear || has_BDChot_ThisYear)
				haoMonTaiSan_new.GIA_TRI_HAO_MON = Math.Round((ktGiaTriTaiSan.HM_TyLe ?? 0) * (ktGiaTriTaiSan.TS_NguyenGiaTinhHM ?? 0) / 100);
			if (ktGiaTriTaiSan.HM_GiaTriTinh == 0 || hmts_lastYear.TONG_HAO_MON_LUY_KE == ktGiaTriTaiSan.TS_NguyenGiaTinhHM)
				haoMonTaiSan_new.GIA_TRI_HAO_MON = 0;
			if (haoMonTaiSan_new.TONG_GIA_TRI_CON_LAI == 0 && ktGiaTriTaiSan.HM_GiaTriTinh > 0)
			{
				haoMonTaiSan_new.GIA_TRI_HAO_MON = ktGiaTriTaiSan.HM_GiaTriTinh;
			}
			//thông tin tài sản, năm
			haoMonTaiSan_new.TAI_SAN_ID = taiSanId;
			haoMonTaiSan_new.NAM_HAO_MON = namHaoMon;
			return haoMonTaiSan_new;
		}

		public KhauHaoTaiSan ChotKhauHao(decimal taiSanId, decimal namKhauHao, decimal thangKhauHao)
		{
			if (!(taiSanId > 0))
				return null;
			KhauHaoTaiSan khauHaoTaiSan_new = new KhauHaoTaiSan();
			//Điều kiện lấy bản ghi tháng trước
			var preMonth = thangKhauHao;
			var preYear = namKhauHao;
			bool has_kh_lastMonth = false;
			bool has_BDChot_thisMonth = false;
			if ((thangKhauHao - 1) <= 0)
			{
				preMonth = 12;
				preYear = namKhauHao - 1;
			}
			else
			{
				preMonth = thangKhauHao - 1;
				preYear = namKhauHao;
			}
			DateTime BDChot_NgayBienDong = new DateTime(); //ngày biến động được người nhập chốt gtcl => k cần tính lại gtcl của bd này. Tính tiếp từ ngày này
			BienDongChiTiet BDChot_bdct = new BienDongChiTiet(); //Lưu thông tin biến động chốt giá trị còn lại
																 //Lấy các thông tin liên quan tới tài sản: các biến động, loại tài sản,...
			KTGiaTriTaiSan ktGiaTriTaiSan = new KTGiaTriTaiSan(); //Lưu trữ các thông tin cần để tính toán các giá trị
			TaiSan taiSanEntity = _taiSanRepository.Table.Where(c => c.ID == taiSanId).FirstOrDefault();
			LoaiTaiSan loaiTaiSan = _loaiTaiSanRepository.Table.Where(c => c.ID == taiSanEntity.LOAI_TAI_SAN_ID).FirstOrDefault();
			LoaiTaiSanKhauHao loaiTaiSanKhauHao = _loaiTaiSanKhauHaoRepository.Table.Where(c => c.LOAI_TAI_SAN_ID == taiSanEntity.LOAI_TAI_SAN_ID.Value && c.DON_VI_ID == taiSanEntity.DON_VI_ID.Value).FirstOrDefault();
			var listBienDongTaiSan = _bienDongRepository.Table.Where(c => c.TAI_SAN_ID == taiSanId);
			var listBienDongChiTietTaiSan = _bienDongChiTietRepository.Table.Where(c => c.biendong.TAI_SAN_ID == taiSanId && c.biendong.NGAY_BIEN_DONG.Value.Year <= namKhauHao);
			//Lấy biến động đầu tiên (bd tăng mới, nhập số dư sinh ra ts)
			BienDong bdTangTS = listBienDongTaiSan.OrderBy(c => c.NGAY_BIEN_DONG).FirstOrDefault();
			BienDongChiTiet bdctTangTS = _bienDongChiTietRepository.Table.Where(c => c.BIEN_DONG_ID == bdTangTS.ID).FirstOrDefault();
			//Tỷ lệ nguyên giá hao mòn, khấu hao
			ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH = ((bdctTangTS.KH_GIA_TINH_KHAU_HAO ?? 0) / bdTangTS.NGUYEN_GIA);
			ktGiaTriTaiSan.TS_TyLeNguyenGiaTinhHM = 1 - (ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH);
			ktGiaTriTaiSan.KH_ThangConLai = bdctTangTS.KH_THANG_CON_LAI;

			//
			ktGiaTriTaiSan.LoaiHinhTaiSanId = taiSanEntity.LOAI_HINH_TAI_SAN_ID;
			if (taiSanEntity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
			{
				ktGiaTriTaiSan.KH_TyLe = taiSanEntity.loaitaisandonvi.KH_TY_LE;
			}
			else
			{
				if (loaiTaiSanKhauHao != null)
				{
					ktGiaTriTaiSan.KH_TyLe = loaiTaiSanKhauHao.TY_LE_KHAU_HAO;
				}
				else
				{
					ktGiaTriTaiSan.KH_TyLe = loaiTaiSan.HM_TY_LE;
				}
			}
			//Lấy bản ghi khấu hao kt tháng trước (nếu có)
			KhauHaoTaiSan khts_lastMonth = _khauHaoTaiSanRepository.Table.Where(c => c.TAI_SAN_ID == taiSanId && c.NAM_KHAU_HAO == preYear && c.THANG_KHAU_HAO == preMonth).FirstOrDefault();
			if (khts_lastMonth == null)
			{
				has_kh_lastMonth = false;
				khts_lastMonth = new KhauHaoTaiSan();
				khts_lastMonth.TONG_NGUYEN_GIA = bdTangTS.NGUYEN_GIA * ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH;
				if (taiSanEntity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || taiSanEntity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
				{
					khts_lastMonth.TONG_GIA_TRI_CON_LAI = bdTangTS.NGUYEN_GIA;
					khts_lastMonth.TONG_KHAU_HAO_LUY_KE = 0;
				}
				else
				{
					//bdctDauTien.HM_Gia_tri_con_lai: là giá trị còn lại sau khấu hao lẫn hao mòn
					khts_lastMonth.TONG_GIA_TRI_CON_LAI = bdctTangTS.HM_GIA_TRI_CON_LAI * ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH;
					khts_lastMonth.TONG_KHAU_HAO_LUY_KE = bdTangTS.NGUYEN_GIA * ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH - bdctTangTS.HM_GIA_TRI_CON_LAI * ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH;
				}
				BDChot_NgayBienDong = bdTangTS.NGAY_BIEN_DONG.Value;

			}
			else
			{
				has_kh_lastMonth = true;

			}
			//check bien dong trong tháng
			var listBDCTThisMonth = listBienDongChiTietTaiSan.Where(c => c.biendong.NGAY_BIEN_DONG.Value.Year == namKhauHao && c.biendong.NGAY_BIEN_DONG.Value.Month == thangKhauHao);
			if (listBDCTThisMonth != null && listBDCTThisMonth.Count() > 0)
			{
				//lấy biến động đã được chốt giá trị
				var listBDCTChotGTCL = listBDCTThisMonth.Where(c => c.biendong.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO && c.biendong.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY && c.HM_GIA_TRI_CON_LAI != null).OrderBy(c => c.biendong.NGAY_BIEN_DONG);
				if (listBDCTChotGTCL != null && listBDCTChotGTCL.Count() > 0)
				{
					has_BDChot_thisMonth = true;
					BDChot_bdct = listBDCTChotGTCL.FirstOrDefault();
					khts_lastMonth.TONG_GIA_TRI_CON_LAI = BDChot_bdct.HM_GIA_TRI_CON_LAI * ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH;
					BDChot_NgayBienDong = BDChot_bdct.biendong.NGAY_BIEN_DONG.Value;
				}
				else
				{
					has_BDChot_thisMonth = false;
				}
				//lấy các thông tin chốt + các biến động tiếp sau(trong tháng)
				var list_bdctSauChot = listBDCTThisMonth.Where(c => c.biendong.NGAY_BIEN_DONG > BDChot_NgayBienDong);
				if (list_bdctSauChot != null && list_bdctSauChot.Count() > 0)
				{
					ktGiaTriTaiSan.TS_NguyenGiaBienDong = list_bdctSauChot.Sum(c => c.biendong.NGUYEN_GIA);
					khts_lastMonth.TONG_KHAU_HAO_LUY_KE = ((khts_lastMonth.TONG_NGUYEN_GIA ?? 0) + (ktGiaTriTaiSan.TS_NguyenGiaBienDong ?? 0) * ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH) - (khts_lastMonth.TONG_GIA_TRI_CON_LAI ?? 0);
				}
			}

			//Tính toán
			ktGiaTriTaiSan.KH_GiaTriTinh = khts_lastMonth.TONG_GIA_TRI_CON_LAI + ((ktGiaTriTaiSan.TS_NguyenGiaBienDong ?? 0) * ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH);
			ktGiaTriTaiSan.TS_NguyenGiaTinhKH = listBienDongTaiSan.Where(c => c.NGAY_BIEN_DONG.Value.Year < namKhauHao || (c.NGAY_BIEN_DONG.Value.Year == namKhauHao && c.NGAY_BIEN_DONG.Value.Month <= thangKhauHao)).Sum(c => c.NGUYEN_GIA) * ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH;
			khauHaoTaiSan_new.TONG_NGUYEN_GIA = ktGiaTriTaiSan.TS_NguyenGiaTinhKH;
			khauHaoTaiSan_new.TY_LE_KHAU_HAO = ktGiaTriTaiSan.KH_TyLe;
			khauHaoTaiSan_new.MA_TAI_SAN = taiSanEntity.MA;
			if (BDChot_NgayBienDong.Day == 31 && BDChot_NgayBienDong.Month == 12)
			{
				khauHaoTaiSan_new.GIA_TRI_KHAU_HAO = 0;
			}
			else
			{
				khauHaoTaiSan_new.GIA_TRI_KHAU_HAO = Math.Round((ktGiaTriTaiSan.KH_TyLe ?? 0) * (ktGiaTriTaiSan.TS_NguyenGiaTinhKH ?? 0) / 100);
			}
			if (khauHaoTaiSan_new.GIA_TRI_KHAU_HAO > ktGiaTriTaiSan.HM_GiaTriTinh)
			{
				khauHaoTaiSan_new.TONG_KHAU_HAO_LUY_KE = khts_lastMonth.TONG_KHAU_HAO_LUY_KE + ktGiaTriTaiSan.KH_GiaTriTinh;
				khauHaoTaiSan_new.TONG_GIA_TRI_CON_LAI = 0;
			}
			else
			{
				khauHaoTaiSan_new.TONG_KHAU_HAO_LUY_KE = khts_lastMonth.TONG_KHAU_HAO_LUY_KE + khauHaoTaiSan_new.GIA_TRI_KHAU_HAO;
				khauHaoTaiSan_new.TONG_GIA_TRI_CON_LAI = ktGiaTriTaiSan.KH_GiaTriTinh - khauHaoTaiSan_new.GIA_TRI_KHAU_HAO;
			}
			if (!has_kh_lastMonth || has_BDChot_thisMonth)
				khauHaoTaiSan_new.GIA_TRI_KHAU_HAO = Math.Round((ktGiaTriTaiSan.KH_TyLe ?? 0) * (ktGiaTriTaiSan.TS_NguyenGiaTinhKH ?? 0) / 100);
			if (ktGiaTriTaiSan.KH_GiaTriTinh == 0 || khts_lastMonth.TONG_KHAU_HAO_LUY_KE == ktGiaTriTaiSan.TS_NguyenGiaTinhKH)
				khauHaoTaiSan_new.GIA_TRI_KHAU_HAO = 0;
			if (khauHaoTaiSan_new.TONG_GIA_TRI_CON_LAI == 0 && ktGiaTriTaiSan.KH_GiaTriTinh > 0)
			{
				khauHaoTaiSan_new.GIA_TRI_KHAU_HAO = ktGiaTriTaiSan.KH_GiaTriTinh;
			}
			//thông tin tài sản, tháng, nam
			khauHaoTaiSan_new.TAI_SAN_ID = taiSanId;
			khauHaoTaiSan_new.THANG_KHAU_HAO = thangKhauHao;
			khauHaoTaiSan_new.NAM_KHAU_HAO = namKhauHao;
			return khauHaoTaiSan_new;
		}

		public bool ChotToanBoTaiSan(decimal? donViId = 0, decimal? namKeKhai = 0, decimal? taiSanId = 0, decimal? loaiHinhTaiSanId = 0)
		{

			DateTime currentDate = DateTime.Now;
			var taiSans_list = _taiSanRepository.Table.Where(c => c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET || c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO);
			//var taiSans_list = _taiSanRepository.Table.Where(c => c.IS_DUYET == true);
			if (taiSanId > 0)
				taiSans_list = taiSans_list.Where(c => c.ID == taiSanId);
			if (donViId > 0)
				taiSans_list = taiSans_list.Where(c => c.DON_VI_ID == donViId);
			if (namKeKhai > 0)
				taiSans_list = taiSans_list.Where(c => c.NGAY_NHAP.Value.Year == namKeKhai);
			if (loaiHinhTaiSanId > 0)
				taiSans_list = taiSans_list.Where(c => c.LOAI_HINH_TAI_SAN_ID == loaiHinhTaiSanId);
			foreach (var item in taiSans_list)
			{
				//chốt hao mòn qua các năm
				for (int year = item.NGAY_NHAP.Value.Year; year <= currentDate.Year; year++)
				{
					HaoMonTaiSan hmts_new = new HaoMonTaiSan();
					HaoMonTaiSan hmts_old = new HaoMonTaiSan();
					hmts_new = ChotHaoMon(item.ID, year);
					hmts_old = _haoMonTaiSanRepository.Table.Where(c => c.TAI_SAN_ID == item.ID && c.NAM_HAO_MON == year).FirstOrDefault();
					if (hmts_old != null)
					{
						hmts_old = PrepareHMTSEntity(hmtsEntity: hmts_old, hmtsData: hmts_new);
						_haoMonTaiSanRepository.Update(hmts_old);
					}
					else
					{
						_haoMonTaiSanRepository.Insert(hmts_new);
					}
				}
				//chốt khấu hao
				BienDongChiTiet bdctTangTS = _bienDongChiTietRepository.Table.Where(c => c.biendong.TAI_SAN_ID == item.ID && (c.biendong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO || c.biendong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)).FirstOrDefault();
				if (bdctTangTS != null && bdctTangTS.KH_GIA_TINH_KHAU_HAO > 0)
				{

					for (int month = item.NGAY_NHAP.Value.Month; month <= 12; month++)
					{
						//Tính khấu hao từ ngày nhập => tháng 12 cùng năm
						KhauHaoTaiSan khts_new = new KhauHaoTaiSan();
						khts_new = ChotKhauHao(item.ID, item.NGAY_NHAP.Value.Year, month);
						KhauHaoTaiSan khts_old = new KhauHaoTaiSan();
						khts_old = _khauHaoTaiSanRepository.Table.Where(c => c.TAI_SAN_ID == item.ID && c.NAM_KHAU_HAO == item.NGAY_NHAP.Value.Year && c.THANG_KHAU_HAO == month).FirstOrDefault();
						if (khts_old != null)
						{
							khts_old = PrepareKHTSEntity(khtsEntity: khts_old, khtsData: khts_new);
							_khauHaoTaiSanRepository.Update(khts_old);
						}
						else
						{
							_khauHaoTaiSanRepository.Insert(khts_new);
						}
					}
					for (int year = (item.NGAY_NHAP.Value.Year + 1); year < currentDate.Year; year++)
					{
						//Tính khấu hao các năm tiếp theo => năm hiện tại - 1
						for (int month = item.NGAY_NHAP.Value.Month; month <= 12; month++)
						{
							KhauHaoTaiSan khts_new = new KhauHaoTaiSan();
							KhauHaoTaiSan khts_old = new KhauHaoTaiSan();
							khts_new = ChotKhauHao(item.ID, year, month);
							khts_old = _khauHaoTaiSanRepository.Table.Where(c => c.TAI_SAN_ID == item.ID && c.NAM_KHAU_HAO == year && c.THANG_KHAU_HAO == month).FirstOrDefault();
							if (khts_old != null)
							{
								khts_old = PrepareKHTSEntity(khtsEntity: khts_old, khtsData: khts_new);
								_khauHaoTaiSanRepository.Update(khts_old);
							}
							else
							{
								_khauHaoTaiSanRepository.Insert(khts_new);
							}
						}
					}
					for (int month = 1; month <= 12; month++)
					{
						//Tính khấu hao các tháng năm hiện tại
						KhauHaoTaiSan khts_new = new KhauHaoTaiSan();
						KhauHaoTaiSan khts_old = new KhauHaoTaiSan();
						khts_new = ChotKhauHao(item.ID, currentDate.Year, month);
						khts_old =  _khauHaoTaiSanRepository.Table.Where(c => c.TAI_SAN_ID == item.ID && c.NAM_KHAU_HAO == currentDate.Year && c.THANG_KHAU_HAO == month).FirstOrDefault();
						if (khts_old != null)
						{
							khts_old = PrepareKHTSEntity(khtsEntity: khts_old, khtsData: khts_new);
							_khauHaoTaiSanRepository.Update(khts_old);
						}
						else
						{
							_khauHaoTaiSanRepository.Insert(khts_new);
						}
					}
				}
				//Khi chốt xong xóa log trong haoMonTaiSanLog nếu có
				HaoMonTaiSanLog logHMTS = GetHaoMonTaiSanLogByTaiSanId(item.ID);
				if (logHMTS != null)
				{
					DeleteHaoMonTaiSanLog(logHMTS);
				}
				//update lại trạng thái chốt giá trị ()
				//TaiSan taiSanEntity = _taiSanRepository.GetById(item.ID);
				//taiSanEntity.IS_DUYET = true;
				//_taiSanRepository.Update(taiSanEntity);
			}
			return true;
		}

		KhauHaoTaiSan PrepareKHTSEntity(KhauHaoTaiSan khtsEntity, KhauHaoTaiSan khtsData)
		{
			khtsEntity.MA_TAI_SAN = khtsData.MA_TAI_SAN;
			khtsEntity.GIA_TRI_KHAU_HAO = khtsData.GIA_TRI_KHAU_HAO;
			khtsEntity.TONG_KHAU_HAO_LUY_KE = khtsData.TONG_KHAU_HAO_LUY_KE;
			khtsEntity.TONG_GIA_TRI_CON_LAI = khtsData.TONG_GIA_TRI_CON_LAI;
			khtsEntity.TY_LE_KHAU_HAO = khtsData.TY_LE_KHAU_HAO;
			khtsEntity.TONG_NGUYEN_GIA = khtsData.TONG_NGUYEN_GIA;
			return khtsEntity;
		}
		HaoMonTaiSan PrepareHMTSEntity(HaoMonTaiSan hmtsEntity, HaoMonTaiSan hmtsData)
		{
			hmtsEntity.MA_TAI_SAN = hmtsData.MA_TAI_SAN;
			hmtsEntity.GIA_TRI_HAO_MON = hmtsData.GIA_TRI_HAO_MON;
			hmtsEntity.TONG_HAO_MON_LUY_KE = hmtsData.TONG_HAO_MON_LUY_KE;
			hmtsEntity.TONG_GIA_TRI_CON_LAI = hmtsData.TONG_GIA_TRI_CON_LAI;
			hmtsEntity.TY_LE_HAO_MON = hmtsData.TY_LE_HAO_MON;
			hmtsEntity.TONG_NGUYEN_GIA = hmtsData.TONG_NGUYEN_GIA;
			return hmtsEntity;
		}
		
		#endregion
	}
}

