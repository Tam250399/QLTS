//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 12/12/2020
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
using GS.Core.Domain.DB;

namespace GS.Services.DB
{
    public partial class DB_QueueProcessHistoryService:IDB_QueueProcessHistoryService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<DB_QueueProcessHistory> _itemRepository;
         #endregion
         
         #region Ctor

        public DB_QueueProcessHistoryService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<DB_QueueProcessHistory> itemRepository
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
        public virtual IList<DB_QueueProcessHistory> GetAllDB_QueueProcessHistorys(){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        
        public virtual IPagedList <DB_QueueProcessHistory> SearchDB_QueueProcessHistorys(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null){
             var query = _itemRepository.Table;
             return new PagedList<DB_QueueProcessHistory>(query, pageIndex, pageSize);;
         }    
        
        public virtual DB_QueueProcessHistory GetDB_QueueProcessHistoryById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
         
        public virtual IList<DB_QueueProcessHistory> GetDB_QueueProcessHistoryByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertDB_QueueProcessHistory(DB_QueueProcessHistory entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
			entity.TRANG_THAI_ID = (int)enumTrangThaiQueueProcessDB.DA_GUI_REQUEST;
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateDB_QueueProcessHistory(DB_QueueProcessHistory entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteDB_QueueProcessHistory(DB_QueueProcessHistory entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        } 
        
        #endregion	
     }
}		

