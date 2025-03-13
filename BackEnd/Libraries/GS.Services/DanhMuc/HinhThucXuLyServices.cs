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
    public partial class HinhThucXuLyService : IHinhThucXuLyService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<HinhThucXuLy> _itemRepository;
        #endregion

        #region Ctor

        public HinhThucXuLyService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<HinhThucXuLy> itemRepository
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
        public virtual IList<HinhThucXuLy> GetTable()
        {
            return _staticCacheManager.Get(key, () =>
            {
                return _itemRepository.Table.ToList();
            });
        }
        public virtual IList<HinhThucXuLy> GetAllHinhThucXuLys()
        {
            var query = GetTable().AsQueryable<HinhThucXuLy>();
            return query.ToList();
        }
        public virtual IList<HinhThucXuLy> GetAllHinhThucXuLysChuaDb()
        {
            var query = _itemRepository.Table.Where(c=>c.DB_ID ==null);
            return query.ToList();
        }
        public virtual IList<HinhThucXuLy> GetHinhThucXuLys(string Ma = null, int PhuongAnId = 0)
        {
            var query = GetTable().AsQueryable<HinhThucXuLy>();
            if (Ma != null && Ma.Trim() != "")
            {
                query = query.Where(c => c.MA == Ma.ToUpper());
            }
            if (PhuongAnId > 0)
            {
                query = query.Where(c => c.PHUONG_AN_XU_LY_ID == PhuongAnId);
            }
            return query.ToList();
        }

        public virtual IPagedList<HinhThucXuLy> SearchHinhThucXuLys(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = GetTable().AsQueryable<HinhThucXuLy>();
            return new PagedList<HinhThucXuLy>(query, pageIndex, pageSize); ;
        }

        public virtual HinhThucXuLy GetHinhThucXuLyById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<HinhThucXuLy> GetHinhThucXuLyByIds(decimal[] Ids)
        {
            var query = GetTable().AsQueryable<HinhThucXuLy>();
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertHinhThucXuLy(HinhThucXuLy entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            _staticCacheManager.Remove(key);

        }
        public virtual void UpdateHinhThucXuLy(HinhThucXuLy entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);   
            _staticCacheManager.Remove(key);
        }
        public virtual void DeleteHinhThucXuLy(HinhThucXuLy entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
            _staticCacheManager.Remove(key);
        }
        public virtual HinhThucXuLy GetHinhThucXuLyByMa(string Ma = null)
        {
            if (string.IsNullOrEmpty(Ma))
                return null;
            var query = GetTable();
            return query.Where(m => m.MA == Ma).FirstOrDefault();
        }
        public virtual void InsertListHinhThucXuLy(List<HinhThucXuLy> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Insert(entities);
            _staticCacheManager.Remove(key);
        }
        public virtual void UpdateListHinhThucXuLy(List<HinhThucXuLy> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Update(entities);
            _staticCacheManager.Remove(key);
        }
        #endregion
    }
}

