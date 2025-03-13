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
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Data.Extensions;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.HeThong;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.HeThong;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.HeThong;

namespace GS.Web.Factories.HeThong
{
    public class ScheduleTaskModelFactory:IScheduleTaskModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IScheduleTaskService _itemService;
         #endregion
         
         #region Ctor

        public ScheduleTaskModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IScheduleTaskService itemService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
        }
        #endregion
        
        #region ScheduleTask
        public ScheduleTaskSearchModel PrepareScheduleTaskSearchModel(ScheduleTaskSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public ScheduleTaskListModel PrepareScheduleTaskListModel(ScheduleTaskSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchScheduleTasks(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new ScheduleTaskListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<ScheduleTaskModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public ScheduleTaskModel PrepareScheduleTaskModel(ScheduleTaskModel model, ScheduleTask item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<ScheduleTaskModel>();
            }
            //more
           
            return model;
        }
       public void PrepareScheduleTask(ScheduleTaskModel model, ScheduleTask item)
        {
		item.NAME = model.NAME;
		item.SECONDS = model.SECONDS;
		item.ENABLED = model.ENABLED;
		item.STOP_OR_ERROR = model.STOP_OR_ERROR;
		item.LAST_START = model.LAST_START;
		item.LAST_END = model.LAST_END;
		item.LAST_SUCCESS = model.LAST_SUCCESS;
		item.TYPE = model.TYPE;
            
        }
        #endregion	
     }
}		

