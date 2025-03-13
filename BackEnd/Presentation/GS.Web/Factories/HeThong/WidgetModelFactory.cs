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
using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Factories.HeThong
{
    public class WidgetModelFactory:IWidgetModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IWidgetService _itemService;
         #endregion
         
         #region Ctor

        public WidgetModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IWidgetService itemService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
        }
        #endregion
        
        #region Widget
        public WidgetSearchModel PrepareWidgetSearchModel(WidgetSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public WidgetListModel PrepareWidgetListModel(WidgetSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchWidgets(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new WidgetListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<WidgetModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public WidgetModel PrepareWidgetModel(WidgetModel model, Widget item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<WidgetModel>();
            }
            //more
           
            return model;
        }
       public void PrepareWidget(WidgetModel model, Widget item)
        {
		item.TEN = model.TEN;
		item.CAU_HINH = model.CAU_HINH;
		item.MO_TA = model.MO_TA;
            
        }

        public IList<SelectListItem> PrepareMultiSelectListWidget(IList<int> valSelecteds = null)
        {
            var lstWidgets = _itemService.GetAllWidgets();
			if (lstWidgets!= null)
			{
                var selectList = lstWidgets.Select(c => new SelectListItem
                {
                    Text = c.TEN,
                    Value = c.ID.ToString(),
                    Selected = (valSelecteds != null && valSelecteds.Count>0) ?   valSelecteds.Contains(c.ID.ToNumberInt32()):false
                }).ToList();
                if (selectList == null)
                {
                    selectList = new List<SelectListItem>();
                }
                selectList.AddFirstRow("Chọn widget", "0");
                selectList[0].Selected = true;
                return selectList;
            }
			else
			{
                var selectList = new List<SelectListItem>();
                selectList.AddFirstRow("Chọn widget", "0");
                selectList[0].Selected = true;
                return selectList;
            }
            
        }
        #endregion
    }
}		

