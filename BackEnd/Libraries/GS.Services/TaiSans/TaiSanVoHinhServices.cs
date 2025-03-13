//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/3/2020
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
    public partial class TaiSanVoHinhService : ITaiSanVoHinhService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<TaiSanVoHinh> _itemRepository;
        #endregion

        #region Ctor

        public TaiSanVoHinhService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<TaiSanVoHinh> itemRepository
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
        public virtual IList<TaiSanVoHinh> GetAllTaiSanVoHinhs()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<TaiSanVoHinh> SearchTaiSanVoHinhs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table;
            return new PagedList<TaiSanVoHinh>(query, pageIndex, pageSize); ;
        }

        public virtual TaiSanVoHinh GetTaiSanVoHinhById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<TaiSanVoHinh> GetTaiSanVoHinhByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertTaiSanVoHinh(TaiSanVoHinh entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateTaiSanVoHinh(TaiSanVoHinh entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteTaiSanVoHinh(TaiSanVoHinh entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual TaiSanVoHinh GetTaiSanVoHinhByTaiSanId(decimal taiSanId)
        {
            if (taiSanId == 0)
                return null;
            return _itemRepository.Table.Where(c => c.TAI_SAN_ID == taiSanId).FirstOrDefault();
        }
        #endregion
    }
}

