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
using GS.Core.Domain.TaiSans;
using GS.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.DanhMuc
{
    public partial class NguonTaiSanService : INguonTaiSanService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<NguonTaiSan> _itemRepository;
        //private readonly IRepository<TaiSanNguonTaiSan> _taisannguonvonRepository;
        #endregion

        #region Ctor

        public NguonTaiSanService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<NguonTaiSan> itemRepository
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
        //public virtual IList<NguonTaiSan> GetNguonTaiSan(decimal? TaiSanId = 0)
        //{
        //    var query = _itemRepository.Table;
        //    if (TaiSanId > 0)
        //    {
        //        query = query.Join(_taisannguonvonRepository.Table, x => x.ID, y => y.NGUON_VON_ID, (x, y) => new
        //        {
        //            nguonvon = x,
        //            mapping = y
        //        }).Where(z => z.mapping.TAI_SAN_ID == TaiSanId).Select(c => c.nguonvon);
        //    }
        //    return query.ToList();
        //}

        public virtual IPagedList<NguonTaiSan> SearchNguonTaiSan(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table;
            if (!String.IsNullOrEmpty(Keysearch))
                query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch.ToUpper()));
            return new PagedList<NguonTaiSan>(query, pageIndex, pageSize); ;
        }

        public virtual NguonTaiSan GetNguonTaiSanById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<NguonTaiSan> GetNguonTaiSanByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertNguonTaiSan(NguonTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateNguonTaiSan(NguonTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteNguonTaiSan(NguonTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public IList<NguonTaiSan> GetAllNguonTaiSan()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }
        public IList<NguonTaiSan> GetAllNguonTaiSanActive()
        {
            var q = _itemRepository.Table.Where(p => p.TRANG_THAI_ID == 1 && p.IS_REPORT).OrderBy(p => p.ID);
            return q.ToList();
        }
        //public virtual void InsertListNguonVon(List<NguonTaiSan> entities)
        //{
        //    if (entities.Count == 0)
        //        throw new ArgumentNullException(nameof(entities));
        //    _itemRepository.Insert(entities);
        //    //event notification
        //    //_eventPublisher.EntityInserted(entity);
        //}
        //public virtual void UpdateListNguonVon(List<NguonTaiSan> entities)
        //{
        //    if (entities.Count == 0)
        //        throw new ArgumentNullException(nameof(entities));
        //    _itemRepository.Update(entities);
        //    //event notification
        //    //_eventPublisher.EntityUpdated(entity);            
        //}
        #endregion
    }
}

