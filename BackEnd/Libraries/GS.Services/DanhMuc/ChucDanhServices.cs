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
using GS.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.DanhMuc
{
    public partial class ChucDanhService : IChucDanhService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<ChucDanh> _itemRepository;
        #endregion

        #region Ctor

        public ChucDanhService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<ChucDanh> itemRepository
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
        const string key = "DM_CHUC_DANH";
        #region Methods
        public virtual IList<ChucDanh> GetTable()
        {
            return _staticCacheManager.Get(key, () =>
            {
                return _itemRepository.Table.ToList();
            });
        }
        public virtual IList<ChucDanh> GetAllChucDanhs()
        {
            return GetTable();
        }

        public virtual IPagedList<ChucDanh> SearchChucDanhs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? KhoiDonViIdSearch = 0)
        {
            var query = GetTable().AsQueryable<ChucDanh>();
            if (!String.IsNullOrEmpty(Keysearch))
                query = query.Where(c => c.TEN_CHUC_DANH.ToUpper().Contains(Keysearch.ToUpper()) || c.MA_CHUC_DANH.ToUpper().Contains(Keysearch.ToUpper()));
            query = query.OrderBy(c => c.MA_CHUC_DANH);
            if (KhoiDonViIdSearch > 0)
                query = query.Where(c => c.KHOI_DON_VI_ID == KhoiDonViIdSearch).OrderBy(c=>c.MA_CHUC_DANH);
            
            return new PagedList<ChucDanh>(query, pageIndex, pageSize); ;
        }

        public virtual ChucDanh GetChucDanhById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<ChucDanh> GetChucDanhByIds(decimal[] Ids)
        {
            var query = GetTable();
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertChucDanh(ChucDanh entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            _staticCacheManager.Remove(key);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateChucDanh(ChucDanh entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            _staticCacheManager.Remove(key);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }

        public virtual void InsertChucDanh(List<ChucDanh> entities)
        {
            if (entities == null || (entities != null && entities.Count == 0))
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Insert(entities);
            _staticCacheManager.Remove(key);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateChucDanh(List<ChucDanh> entities)
        {
            if (entities == null || (entities != null && entities.Count == 0))
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Update(entities);
            _staticCacheManager.Remove(key);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteChucDanh(ChucDanh entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            _staticCacheManager.Remove(key);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        public bool CheckMaChucDanh(decimal? id = 0, string ma = null)
        {
            return GetTable().Any(c => c.MA_CHUC_DANH == ma && c.ID != id);
        }
        public virtual ChucDanh GetChucDanhByMa(string ma)
        {
            if (string.IsNullOrEmpty(ma))
                return null;
            return GetTable().Where(c => c.MA_CHUC_DANH == ma).FirstOrDefault();
        }
        #endregion
    }
}

