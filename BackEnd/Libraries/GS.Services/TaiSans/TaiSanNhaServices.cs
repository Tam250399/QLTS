//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.TaiSans;
using GS.Data;
using GS.Services.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.TaiSans
{
	public partial class TaiSanNhaService : ITaiSanNhaService
	{
		#region Fields
		private readonly CauHinhChung _cauhinhChung;
		private readonly ICacheManager _cacheManager;
		private readonly IDataProvider _dataProvider;
		private readonly IDbContext _dbContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly IRepository<TaiSanNha> _itemRepository;
        private readonly IDiaBanService _diaBanService;
        #endregion

        #region Ctor

        public TaiSanNhaService(CauHinhChung cauhinhChung,
			ICacheManager cacheManager,
			IDataProvider dataProvider,
			IDbContext dbContext,
			IStaticCacheManager staticCacheManager,
			IRepository<TaiSanNha> itemRepository, 
			IDiaBanService _diaBanService
			)
		{
			this._cauhinhChung = cauhinhChung;
			this._cacheManager = cacheManager;
			this._dataProvider = dataProvider;
			this._dbContext = dbContext;
			this._staticCacheManager = staticCacheManager;
			this._itemRepository = itemRepository;
            this._diaBanService = _diaBanService;
        }

		#endregion

		#region Methods
		public virtual IList<TaiSanNha> GetTaiSanNhas(decimal? taisanDatId = 0)
		{
			var query = _itemRepository.Table;
			if (taisanDatId > 0)
			{
				query = query.Where(c => c.TAI_SAN_DAT_ID == taisanDatId);
			}
			return query.ToList();
		}

		public virtual IPagedList<TaiSanNha> SearchTaiSanNhas(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? taiSanDatId = 0)
		{
			var query = _itemRepository.Table.Where(c => c.taisan.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET || c.taisan.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET || c.taisan.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.TRA_LAI || c.taisan.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO);
			if (taiSanDatId > 0)
				query = query.Where(c => c.TAI_SAN_DAT_ID == taiSanDatId);
			return new PagedList<TaiSanNha>(query, pageIndex, pageSize); ;
		}

		public virtual TaiSanNha GetTaiSanNhaById(decimal Id)
		{
			if (Id == 0)
				return null;
			return _itemRepository.GetById(Id);
		}
		public virtual TaiSanNha GetTaiSanNhaByTaiSanId(decimal TaiSanId)
		{
			if (TaiSanId == 0)
				return null;
			return _itemRepository.Table.Where(c => c.TAI_SAN_ID == TaiSanId).FirstOrDefault();
		}
		public virtual IList<TaiSanNha> GetTaiSanNhaByIds(decimal[] Ids)
		{
			var query = _itemRepository.Table;
			return query.Where(c => Ids.Contains(c.ID)).ToList();
		}

		public virtual void InsertTaiSanNha(TaiSanNha entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Insert(entity);
			//event notification
			//_eventPublisher.EntityInserted(entity);

		}
		public virtual void UpdateTaiSanNha(TaiSanNha entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Update(entity);
			//event notification
			//_eventPublisher.EntityUpdated(entity);            
		}
		public virtual void DeleteTaiSanNha(TaiSanNha entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Delete(entity);
			//event notification
			//_eventPublisher.EntityDeleted(entity);
		}

		public IList<TaiSanNha> GetTaiSanNhaByDatId(decimal taiSanDatId)
		{
			var query = _itemRepository.Table.Where(c => c.taisan.TRANG_THAI_ID != (int)enumTRANG_THAI_TAI_SAN.NHAP && c.taisan.TRANG_THAI_ID != (int)enumTRANG_THAI_TAI_SAN.XOA);
			return query.Where(c => c.TAI_SAN_DAT_ID == taiSanDatId).OrderByDescending(c => c.NGAY_SU_DUNG).ToList();
		}
		public void DeleteTaiSanNhaByTaiSanDatId(decimal taiSanDatId)
		{
			var lstTaiSanNha = _itemRepository.Table.Where(c => c.TAI_SAN_DAT_ID == taiSanDatId).ToList();
			foreach (var item in lstTaiSanNha)
			{
				_itemRepository.Delete(item);
			}
		}

		/// <summary>
		/// Dựa vào mã địa bàn để lấy ra tỉnh, huyện, xã ID
		/// </summary>
		/// <param name="DiaBanId"></param>
		/// <param name="item"></param>
        public void GetDiaBanInfoByMaDiaBan(decimal? DiaBanId = 0, TaiSanNha item= null )
        {
			if (DiaBanId > 0 && item !=null )
			{
				var diaban = _diaBanService.GetDiaBanById(DiaBanId??0);
				switch (diaban.LOAI_DIA_BAN_ID)
				{
					case (int)enumLOAI_DIABAN.XA:
						var diaBanCha = _diaBanService.GetDiaBanById(diaban.PARENT_ID ?? 0);
						var diaBanChaParentId = diaBanCha != null ? (decimal?)diaBanCha.PARENT_ID : null;
						item.XA_ID = (int)diaban.ID;
						item.HUYEN_ID = (int)diaban.PARENT_ID;
						item.TINH_ID = (int)diaBanChaParentId;
						break;
					case (int)enumLOAI_DIABAN.HUYEN:

						item.HUYEN_ID = (int)diaban.ID;
						item.TINH_ID = (int)diaban.PARENT_ID;						
						break;
					case (int)enumLOAI_DIABAN.TINH:
						item.TINH_ID = (int)diaban.ID;
						break;
				}
			}
		}

        #endregion
    }
}

