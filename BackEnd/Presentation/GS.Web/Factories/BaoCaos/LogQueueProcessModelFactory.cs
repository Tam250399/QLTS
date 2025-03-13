//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 28/6/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Data.Extensions;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.BaoCaos;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.BaoCaos;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.BaoCaos;

namespace GS.Web.Factories.BaoCaos
{
    public class LogQueueProcessModelFactory:ILogQueueProcessModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly ILogQueueProcessService _itemService;
         #endregion
         
         #region Ctor

        public LogQueueProcessModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            ILogQueueProcessService itemService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
        }
        #endregion
        
        #region LogQueueProcess
        public LogQueueProcessSearchModel PrepareLogQueueProcessSearchModel(LogQueueProcessSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public LogQueueProcessListModel PrepareLogQueueProcessListModel(LogQueueProcessSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchLogQueueProcesss(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new LogQueueProcessListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<LogQueueProcessModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public LogQueueProcessModel PrepareLogQueueProcessModel(LogQueueProcessModel model, LogQueueProcess item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<LogQueueProcessModel>();
            }
            //more
           
            return model;
        }
       public void PrepareLogQueueProcess(LogQueueProcessModel model, LogQueueProcess item)
        {
		item.STATUS = model.STATUS;
		item.ACTION = model.ACTION;
		item.OUTPUT = model.OUTPUT;
		item.TIME_START_SCAN = model.TIME_START_SCAN;
		item.TIME_STOP_SCAN = model.TIME_STOP_SCAN;
		item.QUEUE_ID = model.QUEUE_ID;
            
        }
        #endregion	
     }
}		

