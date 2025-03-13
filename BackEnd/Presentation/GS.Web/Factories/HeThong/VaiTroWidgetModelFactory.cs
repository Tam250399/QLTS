//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 20/5/2020
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
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.HeThong;
using GS.Web.Models.HeThong;

namespace GS.Web.Factories.QL
{
    public class VaiTroWidgetModelFactory:IVaiTroWidgetModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IVaiTroWidgetService _itemService;
         #endregion
         
         #region Ctor

        public VaiTroWidgetModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IVaiTroWidgetService itemService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
        }
        #endregion
        
        #region VaiTroWidget
        public VaiTroWidgetSearchModel PrepareVaiTroWidgetSearchModel(VaiTroWidgetSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public VaiTroWidgetListModel PrepareVaiTroWidgetListModel(VaiTroWidgetSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchVaiTroWidgets(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new VaiTroWidgetListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<VaiTroWidgetModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public VaiTroWidgetModel PrepareVaiTroWidgetModel(VaiTroWidgetModel model, VaiTroWidget item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<VaiTroWidgetModel>();
            }
            //more
           
            return model;
        }
       public void PrepareVaiTroWidget(VaiTroWidgetModel model, VaiTroWidget item)
        {
		item.VAI_TRO_ID = model.VAI_TRO_ID;
		item.WIDGET_ID = model.WIDGET_ID;
            
        }
        #endregion	
     }
}		

