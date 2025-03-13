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
    public partial class TaiSanKiemKeHoiDongService:ITaiSanKiemKeHoiDongService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<TaiSanKiemKeHoiDong> _itemRepository;
         #endregion
         
         #region Ctor

        public TaiSanKiemKeHoiDongService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<TaiSanKiemKeHoiDong> itemRepository
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
        public virtual IList<TaiSanKiemKeHoiDong> GetAllTaiSanKiemKeHoiDongs(){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        public virtual IList<TaiSanKiemKeHoiDong> GetTaiSanKiemKeHoiDongs(decimal KiemKeId= 0)
        {
            var query = _itemRepository.Table;
            if (KiemKeId > 0)
            {
                query = query.Where(c => c.KIEM_KE_ID == KiemKeId);
            }
            return query.OrderBy(c => c.VI_TRI_ID).ToList();
        }

        public virtual IPagedList <TaiSanKiemKeHoiDong> SearchTaiSanKiemKeHoiDongs(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null, int KiemKeId = 0){
             var query = _itemRepository.Table.Where(c=>c.KIEM_KE_ID == KiemKeId);
            if (Keysearch != null)
            {
                query = query.Where(c => c.HO_TEN.ToUpper().Contains(Keysearch.ToUpper()));
            }
             return new PagedList<TaiSanKiemKeHoiDong>(query, pageIndex, pageSize);;
         }    
        
        public virtual TaiSanKiemKeHoiDong GetTaiSanKiemKeHoiDongById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
         
        public virtual IList<TaiSanKiemKeHoiDong> GetTaiSanKiemKeHoiDongByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertTaiSanKiemKeHoiDong(TaiSanKiemKeHoiDong entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateTaiSanKiemKeHoiDong(TaiSanKiemKeHoiDong entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteTaiSanKiemKeHoiDong(TaiSanKiemKeHoiDong entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        } 
        
        #endregion	
     }
}		

