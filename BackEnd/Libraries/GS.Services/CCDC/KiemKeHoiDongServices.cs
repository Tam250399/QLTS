//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
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
    public partial class KiemKeHoiDongService:IKiemKeHoiDongService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<KiemKeHoiDong> _itemRepository;
         #endregion
         
         #region Ctor

        public KiemKeHoiDongService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<KiemKeHoiDong> itemRepository
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
        public virtual IList<KiemKeHoiDong> GetAllKiemKeHoiDongs(){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        
        public virtual IPagedList <KiemKeHoiDong> SearchKiemKeHoiDongs(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null){
             var query = _itemRepository.Table;
             return new PagedList<KiemKeHoiDong>(query, pageIndex, pageSize);;
         }
        public virtual IPagedList<KiemKeHoiDong> SearchKiemKeHoiDongsForKiemKe(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, int KiemKeId = 0)
        {
            var query = _itemRepository.Table.Where(c=>c.KIEM_KE_ID == KiemKeId);
            if (Keysearch != null)
            {
                query = query.Where(c => c.HO_TEN.Contains(Keysearch));
            }
            return new PagedList<KiemKeHoiDong>(query, pageIndex, pageSize); ;
        }

        public virtual KiemKeHoiDong GetKiemKeHoiDongById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
         
        public virtual IList<KiemKeHoiDong> GetKiemKeHoiDongByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            

        public virtual IList<KiemKeHoiDong> GetKiemKeHoiDongs(Decimal KiemKeId = 0)
        {
            var query = _itemRepository.Table;
            if (KiemKeId > 0)
            {
                query = query.Where(c => c.KIEM_KE_ID == KiemKeId);
            }
            return query.ToList();
        }
        
        public virtual void InsertKiemKeHoiDong(KiemKeHoiDong entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateKiemKeHoiDong(KiemKeHoiDong entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteKiemKeHoiDong(KiemKeHoiDong entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        } 
        
        #endregion	
     }
}		

