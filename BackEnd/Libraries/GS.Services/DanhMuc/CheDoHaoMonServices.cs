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
    public partial class CheDoHaoMonService : ICheDoHaoMonService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<CheDoHaoMon> _itemRepository;
        #endregion

        #region Ctor

        public CheDoHaoMonService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<CheDoHaoMon> itemRepository
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
        public virtual IList<CheDoHaoMon> GetAllCheDoHaoMons()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }
        public virtual IList<CheDoHaoMon> GetAllCheDoHaoMonChuaDb()
        {
            var query = _itemRepository.Table.Where(c => c.DB_ID == null);
            return query.ToList();
        }

        public virtual IPagedList<CheDoHaoMon> SearchCheDoHaoMons(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table;
            if (Keysearch != null)
            {
                query = query.Where(c => c.TEN_CHE_DO.ToLower().Contains(Keysearch.ToLower()));
            }
            return new PagedList<CheDoHaoMon>(query, pageIndex, pageSize); ;
        }
        public virtual CheDoHaoMon GetCheDoHaoMonByMa(string Ma)
        {
            if (String.IsNullOrEmpty(Ma))
                return null;
            return _itemRepository.Table.Where(c=>c.MA_CHE_DO == Ma).FirstOrDefault();
        }
        public virtual CheDoHaoMon GetCheDoHaoMonById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }
        public virtual CheDoHaoMon GetCheDoHaoMonByNgayNhap(DateTime NgayNhap)
        {

            var query = _itemRepository.Table;
            if (NgayNhap < new DateTime(2015, 01, 01))
            {
                query = query.Where(c => c.MA_CHE_DO == "TT162");
            }
            else
                query = query.Where(c => (c.TU_NGAY <= NgayNhap || c.TU_NGAY == null) && (c.DEN_NGAY == null || c.DEN_NGAY >= NgayNhap));
            return query.FirstOrDefault();
        }
        public virtual CheDoHaoMon GetNewestCheDoHaoMon()
        {
            var query = _itemRepository.Table.OrderByDescending(p => p.DEN_NGAY).FirstOrDefault();
            return query;
        }
        public virtual IList<CheDoHaoMon> GetCheDoHaoMonByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }
        public bool CheckMaCheDoHaoMon(decimal id = 0, string ma = null)
        {
            return _itemRepository.Table.Any(c => c.MA_CHE_DO == ma && c.ID != id);
        }

        public virtual void InsertCheDoHaoMon(CheDoHaoMon entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateCheDoHaoMon(CheDoHaoMon entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteCheDoHaoMon(CheDoHaoMon entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual CheDoHaoMon GetCheDoHaoMonByIdDb(int ID_DB = 0)
        {
            if (ID_DB == 0)
                return null;
            return _itemRepository.Table.Where(m => m.DB_ID == ID_DB).FirstOrDefault();
        }
        public virtual void InsertListCheDoHaoMon(List<CheDoHaoMon> entity)
        {
            if (entity == null || entity.Count == 0)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
        }
        public virtual void UpdateListCheDoHaoMon(List<CheDoHaoMon> entity)
        {
            if (entity == null || entity.Count == 0)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        #endregion
    }
}

