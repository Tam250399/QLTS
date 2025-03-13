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
    public partial class NhanXeService : INhanXeService
    {
        const string key = "DM_NHAN_XE";

        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<NhanXe> _itemRepository;
        #endregion

        #region Ctor

        public NhanXeService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<NhanXe> itemRepository
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
        public virtual IList<NhanXe> GetTable()
        {
            return _staticCacheManager.Get(key, () =>
            {
                return _itemRepository.Table.ToList();
            });
        }
        public virtual IList<NhanXe> GetAllNhanXes()
        {
            var queryCache = GetTable();
            return queryCache.OrderBy(s => s.TEN).OrderBy(m => m.TEN).ToList();
        }

        public virtual IPagedList<NhanXe> SearchNhanXes(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = GetTable().AsQueryable<NhanXe>();
            if (!String.IsNullOrEmpty(Keysearch))
                query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch.ToUpper()) || c.MA.ToUpper().Contains(Keysearch.ToUpper()));
            query = query.OrderBy(c => c.MA);
            return new PagedList<NhanXe>(query, pageIndex, pageSize); ;
        }

        public virtual NhanXe GetNhanXeById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<NhanXe> GetNhanXeByIds(decimal[] Ids)
        {
            var query = GetTable();
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertNhanXe(NhanXe entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            _staticCacheManager.Remove(key);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateNhanXe(NhanXe entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            _staticCacheManager.Remove(key);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteNhanXe(NhanXe entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            _staticCacheManager.Remove(key);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        public bool CheckMaNhanXe(decimal id = 0, string ma = null)
        {
            return GetTable().Any(c => c.MA == ma && c.ID != id);
        }
        public virtual NhanXe GetNhanXeByMaTen(string ma = null, string ten = null)
        {
            if (!string.IsNullOrEmpty(ma))
            {
                return GetTable().Where(c => c.MA == ma).FirstOrDefault();
            }
            else if (!string.IsNullOrEmpty(ten))
            {
                return GetTable().Where(c => c.TEN == ten).FirstOrDefault();
            }
            return null;
        }
        public virtual void InsertListNhanXe(List<NhanXe> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Insert(entities);
            _staticCacheManager.Remove(key);

        }
        public virtual void UpdateListNhanXe(List<NhanXe> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Update(entities);
            _staticCacheManager.Remove(key);
        }
        #endregion
    }
}

