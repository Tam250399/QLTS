//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
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
using GS.Core.Domain.DanhMuc;

namespace GS.Services.DanhMuc
{
    public partial class PhuongAnXuLyService : IPhuongAnXuLyService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<PhuongAnXuLy> _itemRepository;
        #endregion

        #region Ctor

        public PhuongAnXuLyService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<PhuongAnXuLy> itemRepository
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
        const string key = "DM_PHUONG_AN_XU_LY";
        #region Methods
        public virtual IList<PhuongAnXuLy> GetTable()
        {
            return _staticCacheManager.Get(key, () =>
            {
                return _itemRepository.Table.ToList();
            });
        }
        public virtual IList<PhuongAnXuLy> GetAllPhuongAnXuLys()
        {
            var query = GetTable().AsQueryable<PhuongAnXuLy>(); ;
            return query.ToList();
        }
        public virtual IList<PhuongAnXuLy> GetAllPhuongAnXuLysChuaDb()
        {
            var query = _itemRepository.Table.Where(c => c.DB_ID == null);
            return query.ToList();
        }
        public virtual IList<PhuongAnXuLy> GetPhuongAnXuLys(string Ma = null)
        {
            var query = GetTable().AsQueryable<PhuongAnXuLy>();
            if (Ma != null && Ma.Trim() != "")
            {
                query = query.Where(c => c.MA == Ma.ToUpper());
            }
            return query.ToList();
        }

        public virtual IPagedList<PhuongAnXuLy> SearchPhuongAnXuLys(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = GetTable().AsQueryable<PhuongAnXuLy>(); ;
            return new PagedList<PhuongAnXuLy>(query, pageIndex, pageSize); ;
        }

        public virtual PhuongAnXuLy GetPhuongAnXuLyById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<PhuongAnXuLy> GetPhuongAnXuLyByIds(decimal[] Ids)
        {
            var query = GetTable().AsQueryable<PhuongAnXuLy>(); ;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertPhuongAnXuLy(PhuongAnXuLy entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            _staticCacheManager.Remove(key);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdatePhuongAnXuLy(PhuongAnXuLy entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            _staticCacheManager.Remove(key);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeletePhuongAnXuLy(PhuongAnXuLy entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            _staticCacheManager.Remove(key);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual PhuongAnXuLy GetPhuongAnXuLyByMa(string Ma = null)
        {
            if (string.IsNullOrEmpty(Ma))
                return null;
            var query = GetTable();
            return query.Where(m => m.MA == Ma).FirstOrDefault();
        }
        public virtual void UpdateListPhuongAnXuLy(List<PhuongAnXuLy> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Insert(entities);
            _staticCacheManager.Remove(key);
        }
        public virtual void InsertListPhuongAnXuLy(List<PhuongAnXuLy> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Update(entities);
            _staticCacheManager.Remove(key);
        }
        #endregion
    }
}

