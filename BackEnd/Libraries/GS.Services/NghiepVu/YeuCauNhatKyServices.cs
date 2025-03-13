//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.NghiepVu;
using GS.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.NghiepVu
{
    public partial class YeuCauNhatKyService : IYeuCauNhatKyService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<YeuCauNhatKy> _itemRepository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public YeuCauNhatKyService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<YeuCauNhatKy> itemRepository,
            IWorkContext workContext
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._workContext = workContext;
        }

        #endregion

        #region Methods
        public virtual IList<YeuCauNhatKy> GetAllYeuCauNhatKys()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<YeuCauNhatKy> SearchYeuCauNhatKys(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table;
            return new PagedList<YeuCauNhatKy>(query, pageIndex, pageSize); ;
        }

        public virtual YeuCauNhatKy GetYeuCauNhatKyById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<YeuCauNhatKy> GetYeuCauNhatKyByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertYeuCauNhatKy(YeuCauNhatKy entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.NGAY_TAO = DateTime.Now;

            entity.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateYeuCauNhatKy(YeuCauNhatKy entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteYeuCauNhatKy(YeuCauNhatKy entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        #endregion
    }
}

