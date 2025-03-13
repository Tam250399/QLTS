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
    public partial class DongXeService : IDongXeService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DongXe> _itemRepository;
        #endregion

        #region Ctor

        public DongXeService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<DongXe> itemRepository
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
        public virtual IList<DongXe> GetAllDongXes()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }
        public virtual IList<DongXe> GetListDongXes(decimal? nhanXeId = 0)
        {
            var query = _itemRepository.Table;
			if (nhanXeId > 0)
			{
                query = query.Where(c => c.NHAN_XE_ID == nhanXeId.Value);
            }
            return query.ToList();
        }
        public virtual IPagedList<DongXe> SearchDongXes(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table;
            if (!String.IsNullOrEmpty(Keysearch))
                query = query.Where(c => c.MA.ToUpper().Contains(Keysearch.ToUpper()) || c.TEN.ToUpper().Contains(Keysearch.ToUpper()));

            query = query.OrderBy(c => c.MA);
            return new PagedList<DongXe>(query, pageIndex, pageSize); ;
        }

        public virtual DongXe GetDongXeById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<DongXe> GetDongXeByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertDongXe(DongXe entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void InsertDongXe(List<DongXe> entities)
        {
            if (entities == null || (entities != null && entities.Count == 0))
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Insert(entities);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDongXe(DongXe entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void UpdateDongXe(List<DongXe> entities)
        {
            if (entities == null || (entities != null && entities.Count == 0))
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Update(entities);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteDongXe(DongXe entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        public bool CheckMaDongXe(decimal id = 0, string ma = null)
        {
            return _itemRepository.Table.Any(c => c.MA == ma && c.ID != id);
        }
        public virtual DongXe GetDongXeByMa(string ma=null, string Ten=null)
        {
            if (!string.IsNullOrEmpty(ma))
            {
                return _itemRepository.Table.Where(c => c.MA == ma).FirstOrDefault();
            }
            else if (!string.IsNullOrEmpty(Ten))
            {
                return _itemRepository.Table.Where(c => c.TEN == Ten).FirstOrDefault();
            }
            return null;

        }
        #endregion
    }
}

