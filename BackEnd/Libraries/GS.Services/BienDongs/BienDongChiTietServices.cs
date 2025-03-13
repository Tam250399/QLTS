//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.KT;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Data;
using GS.Services.Common;
using GS.Services.KT;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.BienDongs
{
	public partial class BienDongChiTietService : IBienDongChiTietService
	{
		#region Fields
		private readonly CauHinhChung _cauhinhChung;
		private readonly ICacheManager _cacheManager;
		private readonly IDataProvider _dataProvider;
		private readonly IDbContext _dbContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly IRepository<BienDongChiTiet> _itemRepository;
		private readonly IHaoMonTaiSanService _haoMonTaiSanService;
		private readonly IKhauHaoTaiSanService _khauHaoTaiSanService;
		private readonly IHaoMonTaiSanLogService _haoMonTaiSanLogService;
		private readonly IRepository<BienDong> _bienDongRepository;
		private readonly IRepository<TaiSan> _taiSanRepository;
		private readonly IGSAPIService _gSAPIService;
		#endregion

		#region Ctor

		public BienDongChiTietService(CauHinhChung cauhinhChung,
			ICacheManager cacheManager,
			IDataProvider dataProvider,
			IDbContext dbContext,
			IStaticCacheManager staticCacheManager,
			IRepository<BienDongChiTiet> itemRepository,
			IHaoMonTaiSanService haoMonTaiSanService,
			IKhauHaoTaiSanService khauHaoTaiSanService,
			IHaoMonTaiSanLogService haoMonTaiSanLogService,
			IRepository<BienDong> bienDongRepository,
			IRepository<TaiSan> taiSanRepository,
			IGSAPIService gSAPIService
			)
		{
			this._cauhinhChung = cauhinhChung;
			this._cacheManager = cacheManager;
			this._dataProvider = dataProvider;
			this._dbContext = dbContext;
			this._staticCacheManager = staticCacheManager;
			this._itemRepository = itemRepository;
			this._khauHaoTaiSanService = khauHaoTaiSanService;
			this._haoMonTaiSanService = haoMonTaiSanService;
			this._haoMonTaiSanLogService = haoMonTaiSanLogService;
			this._bienDongRepository = bienDongRepository;
			this._taiSanRepository = taiSanRepository;
			this._gSAPIService = gSAPIService;
		}
		#endregion

		#region Methods
		public virtual IList<BienDongChiTiet> GetAllBienDongChiTiets()
		{
			var query = _itemRepository.Table;
			return query.OrderBy(c => c.BIEN_DONG_ID).ToList();
		}

		public virtual IPagedList<BienDongChiTiet> SearchBienDongChiTiets(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
		{
			var query = _itemRepository.Table;
			return new PagedList<BienDongChiTiet>(query, pageIndex, pageSize); ;
		}

		public virtual BienDongChiTiet GetBienDongChiTietById(decimal Id)
		{
			if (Id == 0)
				return null;
			var res = _itemRepository.GetById(Id);
			if (res != null)
				GetRowFromDataJsonToEntity(res.DATA_JSON, res);
			return res;
		}

		public virtual IList<BienDongChiTiet> GetBienDongChiTietByIds(decimal[] Ids)
		{
			var query = _itemRepository.Table;
			return query.Where(c => Ids.Contains(c.ID)).ToList();
		}

		public virtual void InsertBienDongChiTiet(BienDongChiTiet entity, bool isTinhKhauHao = false)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			YeuCauChiTiet yeuCau_json = new YeuCauChiTiet(entity);
			yeuCau_json.DATA_JSON = null;
			entity.DATA_JSON = yeuCau_json.toStringJson();
			if (entity.HINH_THUC_XU_LY_ID == 0)
			{
				entity.HINH_THUC_XU_LY_ID = null;
			}
			_itemRepository.Insert(entity);
			if (entity.DAT_TONG_DIEN_TICH != null || entity.NHA_TONG_DIEN_TICH_XD != null || entity.VKT_DIEN_TICH != null)
			{
				BienDong bienDongUpdate = _bienDongRepository.GetById(entity.BIEN_DONG_ID);
				bienDongUpdate.DAT_TONG_DIEN_TICH = entity.DAT_TONG_DIEN_TICH;
				bienDongUpdate.NHA_TONG_DIEN_TICH_XD = entity.NHA_TONG_DIEN_TICH_XD;
				bienDongUpdate.VKT_DIEN_TICH = entity.VKT_DIEN_TICH;
				_bienDongRepository.Update(bienDongUpdate);
			}
			//Khi có thay đổi biến động, set lại ts_tai_san.is_duyet = false
			var taiSanEntity = _taiSanRepository.GetById(entity.biendong.TAI_SAN_ID);
			taiSanEntity.IS_DUYET = false;
			_taiSanRepository.Update(taiSanEntity);
			if (isTinhKhauHao)
				// Để chốt tạm thời cho đội test kiểm tra dữ liệu báo cáo
				ChotHMKHTaiSan(bienDongId: entity.BIEN_DONG_ID, taiSanId: entity.biendong.TAI_SAN_ID);//funtion call proc
				//_haoMonTaiSanLogService.ChotToanBoTaiSan(taiSanId: entity.biendong.TAI_SAN_ID);//funtion code
				//event notification
				//_eventPublisher.EntityInserted(entity);

		}
		public virtual void InsertToBienDongChiTiet(BienDongChiTiet entity)
        {
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Insert(entity);
		}
		public virtual void UpdateBienDongChiTiet(BienDongChiTiet entity, bool isTinhKhauHao = true)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			YeuCauChiTiet yeuCau_json = new YeuCauChiTiet(entity);
			yeuCau_json.DATA_JSON = null;
			entity.DATA_JSON = yeuCau_json.toStringJson();
			_itemRepository.Update(entity);
            if (entity.DAT_TONG_DIEN_TICH != null || entity.NHA_TONG_DIEN_TICH_XD != null || entity.VKT_DIEN_TICH != null)
			{
				BienDong bienDongUpdate = _bienDongRepository.GetById(entity.BIEN_DONG_ID);
				bienDongUpdate.DAT_TONG_DIEN_TICH = entity.DAT_TONG_DIEN_TICH;
				bienDongUpdate.NHA_TONG_DIEN_TICH_XD = entity.NHA_TONG_DIEN_TICH_XD;
				bienDongUpdate.VKT_DIEN_TICH = entity.VKT_DIEN_TICH;
				_bienDongRepository.Update(bienDongUpdate);
			}
			//Khi có thay đổi biến động, set lại ts_tai_san.is_duyet = false
			var taiSanEntity = _taiSanRepository.GetById(entity.biendong.TAI_SAN_ID);
			taiSanEntity.IS_DUYET = false;
			_taiSanRepository.Update(taiSanEntity);
			if (isTinhKhauHao)
				ChotHMKHTaiSan(bienDongId: entity.BIEN_DONG_ID, taiSanId: entity.biendong.TAI_SAN_ID);//funtion call proc
				//_haoMonTaiSanLogService.ChotToanBoTaiSan(taiSanId: entity.biendong.TAI_SAN_ID);//function code
				//event notification
				//_eventPublisher.EntityUpdated(entity);            
		}
		public virtual void DeleteBienDongChiTiet(BienDongChiTiet entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Delete(entity);
			//Khi có thay đổi biến động, set lại ts_tai_san.is_duyet = false
			var taiSanEntity = _taiSanRepository.GetById(entity.biendong.TAI_SAN_ID);
			taiSanEntity.IS_DUYET = false;
			_taiSanRepository.Update(taiSanEntity);
			//event notification
			//_eventPublisher.EntityDeleted(entity);
		}

		public BienDongChiTiet GetBienDongChiTietByBDId(decimal bienDongId)
		{
			var query = _itemRepository.Table;
			var res = query.Where(c => c.BIEN_DONG_ID == bienDongId).FirstOrDefault();
			if (res != null)
				GetRowFromDataJsonToEntity(res.DATA_JSON, res);
			return res;
		}
		public BienDongChiTiet GetBienDongChiTietByBDIdForKiemKe(decimal bienDongId)
		{
			var query = _itemRepository.Table.Where(c => c.BIEN_DONG_ID == bienDongId).FirstOrDefault();
			if (query != null)
				GetRowFromDataJsonToEntity(query.DATA_JSON, query);
			return query;
		}
		public IList<BienDongChiTiet> GetBienDongChiTietByBDIds(IList<decimal> bienDongIds)
		{
			if (bienDongIds == null || bienDongIds.Count == 0)
				return new List<BienDongChiTiet>();
			var query = _itemRepository.Table;
			return query.Where(c => bienDongIds.Contains(c.BIEN_DONG_ID)).ToList();
		}
		private void GetRowFromDataJsonToEntity(string dataJson, BienDongChiTiet entity)
		{
			if (!string.IsNullOrEmpty(dataJson) && entity != null)
			{
				var ycct_json = dataJson.toEntity<YeuCauChiTiet>();
				if (ycct_json != null)
				{
					entity.HS_CNQSD_SO = ycct_json.HS_CNQSD_SO;
					entity.HS_CNQSD_NGAY = ycct_json.HS_CNQSD_NGAY;
					entity.HS_QUYET_DINH_GIAO_SO = ycct_json.HS_QUYET_DINH_GIAO_SO;
					entity.HS_QUYET_DINH_GIAO_NGAY = ycct_json.HS_QUYET_DINH_GIAO_NGAY;
					entity.HS_CHUYEN_NHUONG_SD_SO = ycct_json.HS_CHUYEN_NHUONG_SD_SO;
					entity.HS_CHUYEN_NHUONG_SD_NGAY = ycct_json.HS_CHUYEN_NHUONG_SD_NGAY;
					entity.HS_QUYET_DINH_CHO_THUE_SO = ycct_json.HS_QUYET_DINH_CHO_THUE_SO;
					entity.HS_QUYET_DINH_CHO_THUE_NGAY = ycct_json.HS_QUYET_DINH_CHO_THUE_NGAY;
					entity.HS_HOP_DONG_CHO_THUE_SO = ycct_json.HS_HOP_DONG_CHO_THUE_SO;
					entity.HS_HOP_DONG_CHO_THUE_NGAY = ycct_json.HS_HOP_DONG_CHO_THUE_NGAY;
					entity.HS_KHAC = ycct_json.HS_KHAC;
					entity.HS_QUYET_DINH_BAN_GIAO = ycct_json.HS_QUYET_DINH_BAN_GIAO;
					entity.HS_QUYET_DINH_BAN_GIAO_NGAY = ycct_json.HS_QUYET_DINH_BAN_GIAO_NGAY;
					entity.HS_BIEN_BAN_NGHIEM_THU = ycct_json.HS_BIEN_BAN_NGHIEM_THU;
					entity.HS_BIEN_BAN_NGHIEM_THU_NGAY = ycct_json.HS_BIEN_BAN_NGHIEM_THU_NGAY;
					entity.HS_PHAP_LY_KHAC = ycct_json.HS_PHAP_LY_KHAC;
					entity.HS_PHAP_LY_KHAC_NGAY = ycct_json.HS_PHAP_LY_KHAC_NGAY;
					//
					entity.NGAY_BAN_THANH_LY = ycct_json.NGAY_BAN_THANH_LY;
					entity.TAI_SAN_TRUOC_DIEU_CHUYEN_ID = ycct_json.TAI_SAN_TRUOC_DIEU_CHUYEN_ID;
					entity.KH_TY_LE_NGUYEN_GIA_KHAU_HAO = ycct_json.KH_TY_LE_NGUYEN_GIA_KHAU_HAO;

					entity.DAT_GIA_TRI_QUYEN_SD_DAT = ycct_json.DAT_GIA_TRI_QUYEN_SD_DAT;
					entity.OTO_SO_CAU_XE = ycct_json.OTO_SO_CAU_XE;
				}
			}
		}
		/// <summary>
		///  Description: Chốt giá trị hao mòn khấu hao tài sản khi có biến động update, insert
		/// </summary>
		/// <param name="bienDongId">Id biến động thay đổi</param>
		public virtual void ChotHMKHTaiSan(decimal bienDongId, decimal taiSanId)
		{
			var bienDongChiTiet = GetBienDongChiTietByBDId(bienDongId);
			var ngayBienDong = bienDongChiTiet.biendong.NGAY_BIEN_DONG;
			if (bienDongChiTiet.biendong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO)
			{
				//List danh sách các bản ghi chốt giá trị tài sản sau ngày giảm toàn bộ cần xóa bỏ
				var list_KTHM = _haoMonTaiSanService.GetListHaoMonTaiSans(taiSanId: taiSanId).Where(c => c.NAM_HAO_MON > bienDongChiTiet.biendong.NGAY_BIEN_DONG.Value.Year).ToList();
				var list_KTKH = _khauHaoTaiSanService.GetListKhauHaoTaiSans(taiSanId: taiSanId).Where(c => c.NAM_KHAU_HAO > bienDongChiTiet.biendong.NGAY_BIEN_DONG.Value.Year || (c.NAM_KHAU_HAO >= bienDongChiTiet.biendong.NGAY_BIEN_DONG.Value.Year && c.THANG_KHAU_HAO >= bienDongChiTiet.biendong.NGAY_BIEN_DONG.Value.Month)).ToList();
				if (list_KTKH != null)
					foreach (var itemKH in list_KTKH)
						_khauHaoTaiSanService.DeleteKhauHaoTaiSan(itemKH);
				if (list_KTHM != null)
					foreach (var itemHM in list_KTHM)
						_haoMonTaiSanService.DeleteHaoMonTaiSan(itemHM);
				//Update lại bản ghi hao mòn năm có giảm toàn bộ
				//Cập nhật lái số tiền hao mòn năm
				var thisYearHM = _haoMonTaiSanService.GetHaoMonTaiSanByTSIdandNamBaoCao(tsId: taiSanId, namBaoCao: bienDongChiTiet.biendong.NGAY_BIEN_DONG.Value.Year);
				thisYearHM.TONG_GIA_TRI_CON_LAI = thisYearHM.TONG_GIA_TRI_CON_LAI + thisYearHM.GIA_TRI_HAO_MON;
				thisYearHM.TONG_HAO_MON_LUY_KE = thisYearHM.TONG_HAO_MON_LUY_KE - thisYearHM.GIA_TRI_HAO_MON;
				thisYearHM.GIA_TRI_HAO_MON = 0;
				_haoMonTaiSanService.UpdateHaoMonTaiSan(thisYearHM);
			}
			else
			{
				//cập nhật giá trị KT_HAO_MON_TAI_SAN
				var moment = DateTime.Now;
				if (bienDongChiTiet.biendong.NGAY_BIEN_DONG.Value.Year < moment.Year)
				{
					for (int namHaoMon = bienDongChiTiet.biendong.NGAY_BIEN_DONG.Value.Year; namHaoMon <= moment.Year; namHaoMon++)
					{
						_haoMonTaiSanService.InsertOrUpdateRecordTblKTHM(p_TaiSanId: bienDongChiTiet.biendong.TAI_SAN_ID, p_NamHaoMon: namHaoMon);
					}
				}
				else
				{
					_haoMonTaiSanService.InsertOrUpdateRecordTblKTHM(p_TaiSanId: bienDongChiTiet.biendong.TAI_SAN_ID, p_NamHaoMon: bienDongChiTiet.biendong.NGAY_BIEN_DONG.Value.Year);
				}

				//Lấy bản ghi biến động tăng mới hoặc điều chỉnh khấu hao gần nhất để kiếm tra tài sản có khấu hao hay không.
				//Hiện tại chưa có biến động: điều chỉnh chế độ khấu hao
				var _bd_check = _bienDongRepository.Table.Where(c => c.TAI_SAN_ID == taiSanId && c.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO).OrderByDescending(c => c.NGAY_BIEN_DONG).FirstOrDefault();
				var _bdct_check = GetBienDongChiTietByBDId(_bd_check.ID);

				if (_bdct_check != null && _bdct_check.KH_GIA_TINH_KHAU_HAO > 0)
				{

					//cập nhật giá trị KT_KHAU_HAO_TAI_SAN
					for (int month = ngayBienDong.Value.Month; month <= 12; month++)
					{
						_khauHaoTaiSanService.InsertOrUpdateRecordTblKTKH(p_TaiSanId: bienDongChiTiet.biendong.TAI_SAN_ID, p_NamKhauHao: ngayBienDong.Value.Year, p_ThangKhauHao: month);
					}
					for (int year = ngayBienDong.Value.Year + 1; year <= moment.Year; year++)
					{
						//Lặp theo tháng
						for (int month = 1; month <= 12; month++)
						{
							_khauHaoTaiSanService.InsertOrUpdateRecordTblKTKH(p_TaiSanId: bienDongChiTiet.biendong.TAI_SAN_ID, p_NamKhauHao: year, p_ThangKhauHao: month);
						}
					}
					//for (int month = 1; month <= moment.Month; month++)
					//{
					//    _khauHaoTaiSanService.InsertOrUpdateRecordTblKTKH(p_TaiSanId: bienDongChiTiet.biendong.TAI_SAN_ID, p_NamKhauHao: moment.Year, p_ThangKhauHao: month);
					//}

				}

			}

		}
		/// <summary>
		/// Description: Chốt giá trị hao mòn, khấu hao
		/// </summary>
		/// <param name="ngayBienDong">ngày biến động của bản ghi bị hủy</param>
		public virtual void ChotHMKHTaiSanKhiHuyDuyet(decimal? taiSanId = 0, DateTime? ngayBienDong = null)
		{
			if (taiSanId == 0 || ngayBienDong == null)
				return;
			//Cập nhật thông tin giá trị tài sản vào bảng KT
			//cập nhật giá trị KT_HAO_MON_TAI_SAN
			var moment = DateTime.Now;
			if (ngayBienDong.Value.Year < moment.Year)
			{
				for (int namHaoMon = ngayBienDong.Value.Year; namHaoMon <= moment.Year; namHaoMon++)
				{
					_haoMonTaiSanService.InsertOrUpdateRecordTblKTHM(p_TaiSanId: taiSanId.Value, p_NamHaoMon: namHaoMon);
				}
			}
			else
			{
				_haoMonTaiSanService.InsertOrUpdateRecordTblKTHM(p_TaiSanId: taiSanId.Value, p_NamHaoMon: ngayBienDong.Value.Year);
			}
			//cập nhật giá trị KT_KHAU_HAO_TAI_SAN
			for (int month = ngayBienDong.Value.Month; month <= 12; month++)
			{
				_khauHaoTaiSanService.InsertOrUpdateRecordTblKTKH(p_TaiSanId: taiSanId.Value, p_NamKhauHao: ngayBienDong.Value.Year, p_ThangKhauHao: month);
			}
			for (int year = ngayBienDong.Value.Year + 1; year <= moment.Year; year++)
			{
				//Lặp theo tháng
				for (int month = 1; month <= 12; month++)
				{
					_khauHaoTaiSanService.InsertOrUpdateRecordTblKTKH(p_TaiSanId: taiSanId.Value, p_NamKhauHao: year, p_ThangKhauHao: month);
				}
			}
			//for (int month = 1; month <= moment.Month; month++)
			//{
			//    _khauHaoTaiSanService.InsertOrUpdateRecordTblKTKH(p_TaiSanId: taiSanId.Value, p_NamKhauHao: moment.Year, p_ThangKhauHao: month);
			//}

			return;
		}
		public virtual void UpdateHienTrangBienDongChiTiet(BienDongChiTiet entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			YeuCauChiTiet yeuCau_json = new YeuCauChiTiet(entity);
			yeuCau_json.DATA_JSON = null;
			entity.DATA_JSON = yeuCau_json.toStringJson();
			_itemRepository.Update(entity);           
		}
		#endregion
	}
}

