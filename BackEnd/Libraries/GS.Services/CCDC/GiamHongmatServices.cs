//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/2/2020
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
using GS.Core.Domain.CCDC;

namespace GS.Services.CCDC
{
    public partial class GiamHongmatService:IGiamHongmatService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<GiamHongmat> _itemRepository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public GiamHongmatService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<GiamHongmat> itemRepository,
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
        public virtual IList<GiamHongmat> GetAllGiamHongmats(){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        
        public virtual IPagedList <GiamHongmat> SearchGiamHongmats(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null){
             var query = _itemRepository.Table;
            if (Keysearch != null)
            {
                query = query.Where(c => c.CongCu.TEN.ToLower().Contains(Keysearch.ToLower()) || c.CongCu.MA.ToLower().Contains(Keysearch.ToLower()));
            }
            return new PagedList<GiamHongmat>(query.OrderByDescending(c=>c.NGAY_LAP), pageIndex, pageSize);;
         }    
        
        public virtual GiamHongmat GetGiamHongmatById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
        public virtual GiamHongmat GetGiamHongmatByNhapXuatIdAndCongCuId(decimal NhapXuatId = 0, decimal CongCuId = 0)
        {
            var query =  _itemRepository.Table;
            if (NhapXuatId > 0)
            {
                query = query.Where(c => c.NHAP_XUAT_ID == NhapXuatId);
            }
            if (CongCuId > 0)
            {
                query = query.Where(c => c.CONG_CU_ID == CongCuId);
            }
            return query.FirstOrDefault();
        }

        public virtual IList<GiamHongmat> GetGiamHongmatByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertGiamHongmat(GiamHongmat entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.NGAY_TAO = DateTime.Now;
            entity.NGUOI_TAO = _workContext.CurrentCustomer.ID;
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateGiamHongmat(GiamHongmat entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteGiamHongmat(GiamHongmat entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        } 
        
        #endregion	
     }
}		

