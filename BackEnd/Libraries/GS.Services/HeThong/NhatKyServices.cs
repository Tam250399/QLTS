//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
//
using GS.Core.Domain.HeThong;
using GS.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.HeThong
{
    public partial class NhatKyService : INhatKyService
    {
        #region Fields
        private readonly CauHinhNguoiDung _customerSettings;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        //private readonly IEventPublisher _eventPublisher;    
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<NhatKy> _itemRepository;
        #endregion

        #region Ctor

        public NhatKyService(CauHinhNguoiDung customerSettings,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            //IEventPublisher eventPublisher,
            IStaticCacheManager staticCacheManager,
            IRepository<NhatKy> itemRepository
            )
        {
            this._customerSettings = customerSettings;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            //this._eventPublisher = eventPublisher;   
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }

        #endregion

        #region Methods
        public virtual IList<NhatKy> GetAllNhatKys()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<NhatKy> SearchNhatKys(int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null, string Username = null, int CAP_DO = 0, DateTime? Fromdate = null, DateTime? Todate = null)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(KeySearch))
            {
                query = query.Where(c => c.NOI_DUNG.Contains(KeySearch) || c.TEN_DAY_DU.Contains(KeySearch) || c.MO_TA.Contains(KeySearch) || c.TEN_DANG_NHAP.Contains(KeySearch));
            }
            if (!string.IsNullOrEmpty(Username))
            {
                query = query.Where(s => s.TEN_DANG_NHAP == Username);
            }
            if (Fromdate.HasValue)
            {
                var _tungay = Fromdate.Value.Date;
                query = query.Where(x => x.NGAY_TAO >= _tungay);
            }
            if (Todate.HasValue && Todate != DateTime.MinValue)
            {
                var _denngay = Todate.Value.Date.AddDays(1);
                query = query.Where(x => x.NGAY_TAO < _denngay);
            }
            if (CAP_DO != 0)
            {
                query = query.Where(s => s.CAP_DO_ID == CAP_DO);
            }
            query = query.OrderByDescending(x => x.NGAY_TAO);
            return new PagedList<NhatKy>(query, pageIndex, pageSize);
        }

        public virtual NhatKy GetNhatKyById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual void InsertNhatKy(NhatKy entity)
        {
            //if (entity == null)
            //    throw new ArgumentNullException(nameof(entity));
            //_itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateNhatKy(NhatKy entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteNhatKy(NhatKy entity)
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

