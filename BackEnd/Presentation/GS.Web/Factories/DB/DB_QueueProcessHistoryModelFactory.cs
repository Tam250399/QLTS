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
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Data.Extensions;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.DB;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.DB;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.DB;

namespace GS.Web.Factories.DB
{
    public class DB_QueueProcessHistoryModelFactory:IDB_QueueProcessHistoryModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IDB_QueueProcessHistoryService _itemService;
         #endregion
         
         #region Ctor

        public DB_QueueProcessHistoryModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IDB_QueueProcessHistoryService itemService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
        }
        #endregion
        
        #region DB_QueueProcessHistory
        public DB_QueueProcessHistorySearchModel PrepareDB_QueueProcessHistorySearchModel(DB_QueueProcessHistorySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public DB_QueueProcessHistoryListModel PrepareDB_QueueProcessHistoryListModel(DB_QueueProcessHistorySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchDB_QueueProcessHistorys(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new DB_QueueProcessHistoryListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<DB_QueueProcessHistoryModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public DB_QueueProcessHistoryModel PrepareDB_QueueProcessHistoryModel(DB_QueueProcessHistoryModel model, DB_QueueProcessHistory item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DB_QueueProcessHistoryModel>();
            }
            //more
           
            return model;
        }
       public void PrepareDB_QueueProcessHistory(DB_QueueProcessHistoryModel model, DB_QueueProcessHistory item)
        {
		item.TIME_REQUEST = model.TIME_REQUEST;
		item.RESPONSE = model.RESPONSE;
		item.TRANG_THAI_ID = model.TRANG_THAI_ID;
		item.GUID_RESPONSE = model.GUID_RESPONSE;
		item.QUEUE_PROCESS_ID = model.QUEUE_PROCESS_ID;
            
        }
        #endregion	
     }
}		

