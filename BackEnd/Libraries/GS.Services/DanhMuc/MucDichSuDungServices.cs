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
    public partial class MucDichSuDungService : IMucDichSuDungService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<MucDichSuDung> _itemRepository;
        #endregion

        #region Ctor

        public MucDichSuDungService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<MucDichSuDung> itemRepository
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
        const string key = "DM_MUC_DICH_SU_DUNG";
        #region Methods
        public virtual IList<MucDichSuDung> GetTable()
        {
            return _staticCacheManager.Get(key, () =>
            {
                return _itemRepository.Table.ToList();
            });
        }
        public virtual IList<MucDichSuDung> GetMucDichSuDungChuaDb()
        {
            var query = GetTable().Where(c => c.DB_ID == null);
            return query.ToList();
        }
        public virtual IList<MucDichSuDung> GetMucDichSuDungs()
        {
            var query = GetTable();
            return query.ToList();
        }

        public virtual IPagedList<MucDichSuDung> SearchMucDichSuDungs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = GetTable().AsQueryable<MucDichSuDung>();
            if (Keysearch != null)
            {
                query = query.Where(c => c.TEN.ToLower().Contains(Keysearch.ToLower()));
            }
            return new PagedList<MucDichSuDung>(query, pageIndex, pageSize); ;
        }

        public virtual MucDichSuDung GetMucDichSuDungById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<MucDichSuDung> GetMucDichSuDungByIds(decimal[] Ids)
        {
            var query = GetTable();
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertMucDichSuDung(MucDichSuDung entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            _staticCacheManager.Remove(key);

        }
        public virtual void UpdateMucDichSuDung(MucDichSuDung entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            _staticCacheManager.Remove(key);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteMucDichSuDung(MucDichSuDung entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            _staticCacheManager.Remove(key);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual MucDichSuDung GetMucDichSuDungByMa(string MA)
        {
            if (string.IsNullOrEmpty(MA))
                return null;
            var query = GetTable();
            return query.Where(m => m.MA == MA).FirstOrDefault();
        }
        public virtual MucDichSuDung GetMucDichSuDungByTen(string Ten)
        {
            if (string.IsNullOrEmpty(Ten))
                return null;
            var query = GetTable();
            Ten = Ten.Trim();
            return query.Where(m => m.TEN.Equals(Ten)).FirstOrDefault();
        }
        public virtual void InsertListMucDichSuDung(List<MucDichSuDung> entity)
        {
            if (entity == null || entity.Count == 0)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
        }
        public virtual void UpdateListMucDichSuDung(List<MucDichSuDung> entity)
        {
            if (entity == null || entity.Count == 0)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual MucDichSuDung GetMucDichSuDungByID_DB(int ID_DB = 0)
        {
            if (ID_DB == 0)
                return null;
            var query = GetTable();
            return query.Where(m => m.DB_ID == ID_DB).FirstOrDefault();
        }
        #endregion
    }
}

