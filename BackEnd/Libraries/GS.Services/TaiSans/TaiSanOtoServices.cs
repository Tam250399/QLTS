//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.TaiSans;
using GS.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.TaiSans
{
    public partial class TaiSanOtoService : ITaiSanOtoService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<TaiSanOto> _itemRepository;
        #endregion

        #region Ctor

        public TaiSanOtoService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<TaiSanOto> itemRepository
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }

        #endregion

        #region Methods
        public virtual IList<TaiSanOto> GetAllTaiSanOtos()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<TaiSanOto> SearchTaiSanOtos(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table;
            return new PagedList<TaiSanOto>(query, pageIndex, pageSize); ;
        }

        public TaiSanOto GetTaiSanOtoByBKS(string bienKiemSoat)
        {
            var query = _itemRepository.Table.Where(c=>c.taisan.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET || c.taisan.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET);
            if (!String.IsNullOrEmpty(bienKiemSoat))
            {
				bienKiemSoat = bienKiemSoat.Replace(" ", "").Replace("-", "").Replace("_", "");
                query = query.Where(c => c.BIEN_KIEM_SOAT.Replace(" ", "").Replace("-", "").Replace("_", "").ToUpper() == bienKiemSoat.ToUpper());
                return query.FirstOrDefault();
            }
            else
                return null;
        }

        public virtual TaiSanOto GetTaiSanOtoById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<TaiSanOto> GetTaiSanOtoByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertTaiSanOto(TaiSanOto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateTaiSanOto(TaiSanOto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteTaiSanOto(TaiSanOto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        public TaiSanOto GetTaiSanOtoByTaiSanId(decimal taiSanId)
        {
            if (taiSanId == 0)
                return null;
            return _itemRepository.Table.Where(c => c.TAI_SAN_ID == taiSanId).FirstOrDefault();
        }
        public decimal CountOtoByChucDanh(decimal chucDanhId,decimal DonViId)
        {
            var count = _itemRepository.Table.Count(p => p.CHUC_DANH_ID == chucDanhId && p.taisan.DON_VI_ID == DonViId && p.taisan.TRANG_THAI_ID != (int)enumTRANG_THAI_TAI_SAN.XOA && p.taisan.TRANG_THAI_ID != (int)enumTRANG_THAI_TAI_SAN.NHAP && p.taisan.TRANG_THAI_ID != (int)enumTRANG_THAI_TAI_SAN.TRA_LAI);
            return count;
        }
        #endregion
    }
}

