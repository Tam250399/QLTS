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
    public partial class NguonVonService : INguonVonService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<NguonVon> _itemRepository;
        private readonly IRepository<TaiSanNguonVon> _taisannguonvonRepository;
        #endregion

        #region Ctor

        public NguonVonService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<NguonVon> itemRepository,
            IRepository<TaiSanNguonVon> taisannguonvonRepository
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._taisannguonvonRepository = taisannguonvonRepository;
        }

        #endregion

        #region Methods
        public virtual IList<NguonVon> GetNguonVons(decimal? TaiSanId = 0)
        {
            var query = _itemRepository.Table;
            if (TaiSanId > 0)
            {
                query = query.Join(_taisannguonvonRepository.Table, x => x.ID, y => y.NGUON_VON_ID, (x, y) => new
                {
                    nguonvon = x,
                    mapping = y
                }).Where(z => z.mapping.TAI_SAN_ID == TaiSanId).Select(c => c.nguonvon);
            }
            return query.ToList();
        }

        public virtual IPagedList<NguonVon> SearchNguonVons(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table;
            if (!String.IsNullOrEmpty(Keysearch))
                query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch.ToUpper()));
            return new PagedList<NguonVon>(query, pageIndex, pageSize); ;
        }

        public virtual NguonVon GetNguonVonById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<NguonVon> GetNguonVonByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertNguonVon(NguonVon entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateNguonVon(NguonVon entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteNguonVon(NguonVon entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
       public IList<NguonVon> GetAllNguonVon()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }
        public IList<NguonVon> GetAllNguonVonActive()
		{
            var q = _itemRepository.Table.Where(p => p.TRANG_THAI_ID == (int)ENUMTRANG_THAI_NGUON_VON.Active).OrderBy(p=>p.THU_TU);
			return q.ToList();
		}
        public virtual void InsertListNguonVon(List<NguonVon> entities)
        {
            if (entities.Count==0 )
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Insert(entities);
            //event notification
            //_eventPublisher.EntityInserted(entity);
        }
        public virtual void UpdateListNguonVon(List<NguonVon> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Update(entities);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        #endregion
    }
}

