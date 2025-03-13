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
    public partial class HinhThucMuaSamService : IHinhThucMuaSamService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<HinhThucMuaSam> _itemRepository;
        #endregion

        #region Ctor

        public HinhThucMuaSamService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<HinhThucMuaSam> itemRepository
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
        const string key = "DM_HINH_THUC_MUA_SAM";
        #region Methods
        public virtual IList<HinhThucMuaSam> GetTable()
        {
            return _staticCacheManager.Get(key, () =>
            {
                return _itemRepository.Table.ToList();
            });
        }
        public virtual IList<HinhThucMuaSam> GetHinhThucMuaSams()
        {
            var query = GetTable();
            return query.OrderBy(c => c.TEN).ToList();
        }
        public bool CheckMaHinhThucMuaSam(decimal id = 0, string ma = null)
        {
            return _itemRepository.Table.Any(c => c.MA == ma && c.ID != id);
        }


        public virtual IPagedList<HinhThucMuaSam> SearchHinhThucMuaSams(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = GetTable().AsQueryable<HinhThucMuaSam>();
            if (Keysearch != null)
            {
                query = query.Where(c => c.TEN.ToLower().Contains(Keysearch.ToLower()));
            }
            return new PagedList<HinhThucMuaSam>(query.OrderBy(c => c.TEN), pageIndex, pageSize); ;
        }

        public virtual HinhThucMuaSam GetHinhThucMuaSamById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<HinhThucMuaSam> GetHinhThucMuaSamByIds(decimal[] Ids)
        {
            var query = GetTable();
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertHinhThucMuaSam(HinhThucMuaSam entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            _staticCacheManager.Remove(key);

        }
        public virtual void UpdateHinhThucMuaSam(HinhThucMuaSam entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);   
            _staticCacheManager.Remove(key);
        }
        public virtual void DeleteHinhThucMuaSam(HinhThucMuaSam entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
            _staticCacheManager.Remove(key);
        }
        public virtual HinhThucMuaSam GetHinhThucMuaSamByMa(string MA)
        {
            if (string.IsNullOrEmpty(MA))
                return null;
            var query = GetTable();
            return query.Where(m => m.MA == MA).FirstOrDefault();
        }
        #endregion
    }
}

