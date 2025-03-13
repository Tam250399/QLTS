//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 25/6/2021
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
using GS.Core.Domain.HeThong;

namespace GS.Services.HeThong
{
    public partial class ScheduleTaskService:IScheduleTaskService
	{				
        #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<ScheduleTask> _itemRepository;
         #endregion
         
        #region Ctor

        public ScheduleTaskService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<ScheduleTask> itemRepository
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
        public virtual IList<ScheduleTask> GetAllScheduleTasks(){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        
        public virtual IPagedList <ScheduleTask> SearchScheduleTasks(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null){
             var query = _itemRepository.Table;
             return new PagedList<ScheduleTask>(query, pageIndex, pageSize);;
         }    
        
        public virtual ScheduleTask GetScheduleTaskById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }

        public virtual ScheduleTask GetScheduleTaskByName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
                return null;
            return _itemRepository.Table.Where(c => c.NAME == Name).FirstOrDefault();
        }

        public virtual IList<ScheduleTask> GetScheduleTaskByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertScheduleTask(ScheduleTask entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateScheduleTask(ScheduleTask entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteScheduleTask(ScheduleTask entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        } 
        
        #endregion	
     }
}		

