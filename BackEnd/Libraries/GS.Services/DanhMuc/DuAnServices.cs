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
    public partial class DuAnService : IDuAnService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DuAn> _itemRepository;
        #endregion

        #region Ctor

        public DuAnService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<DuAn> itemRepository
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
        public virtual IList<DuAn> GetAllDuAns()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }
        public virtual IList<DuAn> GetAllDuAnsChuaDb()
        {
            var query = _itemRepository.Table.Where(c => c.DB_ID == null);
            return query.ToList();
        }

        public virtual IPagedList<DuAn> SearchDuAns(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, Decimal? donViId = 0)
        {
            var query = _itemRepository.Table;
            query = query.OrderBy(c => c.MA);
            if (Keysearch != null)
            {
                query = query.Where(c => c.TEN.ToLower().Contains(Keysearch.ToLower())).OrderBy(c => c.MA);
            }
            if (donViId > 0)
            {
                query = query.Where(c => c.DON_VI_ID == donViId);
            }
            return new PagedList<DuAn>(query, pageIndex, pageSize);
        }

        public virtual DuAn GetDuAnById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<DuAn> GetDuAnByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertDuAn(DuAn entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDuAn(DuAn entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void InsertDuAn(List<DuAn> entities)
        {
            if (entities == null || (entities != null && entities.Count == 0))
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Insert(entities);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDuAn(List<DuAn> entities)
        {
            if (entities == null || (entities != null && entities.Count == 0))
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Update(entities);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteDuAn(DuAn entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        public bool CheckMaDuAn(decimal id = 0, string ma = null, decimal? donViID = null)
        {
            return _itemRepository.Table.Any(c => c.MA == ma && c.ID != id && c.DON_VI_ID == donViID);
        }

        public virtual DuAn GetDuAnByMA(string ma)
        {
            if (string.IsNullOrEmpty(ma))
                return null;
            return _itemRepository.Table.Where(c => c.MA == ma).FirstOrDefault();
        }

        public IList<DuAn> GetDuAnByDonViId(decimal donViId)
        {
            var query = _itemRepository.Table;
            if (donViId > 0)
                query = query.Where(c => c.DON_VI_ID == donViId || c.DON_VI_ID == null);
            else
                query = query.Where(c => c.DON_VI_ID == null);
            return query.ToList();
        }
        #endregion
    }
}

