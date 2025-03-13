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
    public partial class QuocGiaService : IQuocGiaService
    {
        const string key = "DM_QUOC_GIA";

        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<QuocGia> _itemRepository;
        #endregion

        #region Ctor

        public QuocGiaService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<QuocGia> itemRepository
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
        public virtual IList<QuocGia> GetTable()
        {
            return _staticCacheManager.Get(key, () =>
            {
                return _itemRepository.Table.ToList();
            });
        }
        public virtual IList<QuocGia> GetAllQuocGiaChuaDB()
        {
            var query = GetTable().Where(c => c.DB_ID == null);
            return query.ToList();
        }
        public virtual IList<QuocGia> GetAllQuocGias()
        {
            var query = GetTable();
            return query.OrderByDescending(s => s.MA).ToList();
        }

        public virtual IPagedList<QuocGia> SearchQuocGias(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = GetTable().AsQueryable<QuocGia>();

            if (!String.IsNullOrEmpty(Keysearch))
                query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch.ToUpper()) || c.MA.ToUpper().Contains(Keysearch.ToUpper()));
            query = query.OrderBy(c => c.MA);
            return new PagedList<QuocGia>(query, pageIndex, pageSize); ;
        }

        public virtual QuocGia GetQuocGiaById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual QuocGia GetQuocGiaByMa(string ma)
        {
            if (string.IsNullOrEmpty(ma))
                return null;
            return _itemRepository.Table.Where(c => c.MA == ma).FirstOrDefault();
        }

        public virtual IList<QuocGia> GetQuocGiaByIds(decimal[] Ids)
        {
            var query = GetTable();
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertQuocGia(QuocGia entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            _staticCacheManager.Remove(key);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateQuocGia(QuocGia entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            _staticCacheManager.Remove(key);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteQuocGia(QuocGia entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            _staticCacheManager.Remove(key);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        public bool CheckMaQuocGia(string ma = null, decimal id = 0)
        {
            return GetTable().Any(c => c.MA == ma && c.ID != id);
        }

        public virtual QuocGia GetQuocGiaDB(string Ma = null, int ID_DB = 0, decimal ID = 0)
        {
            if (ID == 0)
                return null;
            QuocGia quocGia = new QuocGia();
            if (!string.IsNullOrEmpty(Ma))
            {
                quocGia = GetTable().Where(m => m.MA == Ma).FirstOrDefault();
            }
            if (ID_DB > 0)
            {
                quocGia = GetTable().Where(m => m.DB_ID == ID_DB).FirstOrDefault();
            }

            return quocGia;
        }
        public virtual void InsertListQuocGia(List<QuocGia> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Insert(entities);
            _staticCacheManager.Remove(key);
        }
        public virtual void UpdateListQuocGia(List<QuocGia> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Update(entities);
            _staticCacheManager.Remove(key);
        }
        public QuocGia GetQuocGia(string Ma = null, string Ten = null)
        {
            if (!string.IsNullOrEmpty(Ma))
            {
                return GetTable().Where(m => m.MA == Ma).FirstOrDefault();
            }
            else if (!string.IsNullOrEmpty(Ten))
            {
                return GetTable().Where(m => m.TEN == Ten).FirstOrDefault();
            }
            return null;
        }
        #endregion
    }
}

