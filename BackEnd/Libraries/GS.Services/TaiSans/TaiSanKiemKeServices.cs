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
using GS.Core.Domain.TaiSans;

namespace GS.Services.TaiSans
{
    public partial class TaiSanKiemKeService:ITaiSanKiemKeService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<TaiSanKiemKe> _itemRepository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public TaiSanKiemKeService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<TaiSanKiemKe> itemRepository,
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
        public virtual IList<TaiSanKiemKe> GetAllTaiSanKiemKes(){
            var query = _itemRepository.Table;
             return query.ToList();
         }

        public virtual IPagedList<TaiSanKiemKe> SearchTaiSanKiemKes(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal DonViId = 0, DateTime? NgayKiemKeTu = null, DateTime? NgayKiemKeDen = null)
        {
            var query = _itemRepository.Table.Where(c => c.DON_VI_ID == DonViId);
            if (Keysearch != null && Keysearch.Trim() != "")
            {
                query = query.Where(c => c.SO_BIEN_BAN.ToUpper().Contains(Keysearch.ToUpper()));
            };
            if (NgayKiemKeDen != null)
            {
                query = query.Where(c => c.NGAY_KIEM_KE <= NgayKiemKeDen);
            }
            if (NgayKiemKeTu != null)
            {
                query = query.Where(c => c.NGAY_KIEM_KE >= NgayKiemKeTu);
            }
            return new PagedList<TaiSanKiemKe>(query.OrderByDescending(c => c.NGAY_KIEM_KE).ThenByDescending(m => m.SO_BIEN_BAN), pageIndex, pageSize);
        }

        public virtual TaiSanKiemKe GetTaiSanKiemKeById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
         
        public virtual IList<TaiSanKiemKe> GetTaiSanKiemKeByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }        
        
        public virtual TaiSanKiemKe GetMaxValueId()
        {
            return _itemRepository.Table.Where(c=>c.DON_VI_ID == _workContext.CurrentDonVi.ID).OrderByDescending(c => c.ID).FirstOrDefault();
        }
        
        public virtual void InsertTaiSanKiemKe(TaiSanKiemKe entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.NGAY_TAO = DateTime.Now;
            entity.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateTaiSanKiemKe(TaiSanKiemKe entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteTaiSanKiemKe(TaiSanKiemKe entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        } 
        
        #endregion	
     }
}		

