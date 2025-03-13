//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 22/3/2021
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
    public class LogsDongBoCsdlqgModelFactory:ILogsDongBoCsdlqgModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly ILogsDongBoCsdlqgService _itemService;
         #endregion
         
         #region Ctor

        public LogsDongBoCsdlqgModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            ILogsDongBoCsdlqgService itemService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
        }
        #endregion
        
        #region LogsDongBoCsdlqg
        public LogsDongBoCsdlqgSearchModel PrepareLogsDongBoCsdlqgSearchModel(LogsDongBoCsdlqgSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public LogsDongBoCsdlqgListModel PrepareLogsDongBoCsdlqgListModel(LogsDongBoCsdlqgSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchLogsDongBoCsdlqgs(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new LogsDongBoCsdlqgListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<LogsDongBoCsdlqgModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public LogsDongBoCsdlqgModel PrepareLogsDongBoCsdlqgModel(LogsDongBoCsdlqgModel model, LogsDongBoCsdlqg item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<LogsDongBoCsdlqgModel>();
            }
            //more
           
            return model;
        }
       public void PrepareLogsDongBoCsdlqg(LogsDongBoCsdlqgModel model, LogsDongBoCsdlqg item)
        {
		item.UUID = model.UUID;
		item.MA_QLTSC = model.MA_QLTSC;
		item.MA_CSDLQG = model.MA_CSDLQG;
		item.MO_TA = model.MO_TA;
            
        }
        #endregion	
     }
}		

