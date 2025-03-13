//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 3/10/2020
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
using GS.Core.Domain.DM;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.DM;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.DM;

namespace GS.Web.Factories.DM
{
    public class LoaiLyDoBienDongModelFactory:ILoaiLyDoBienDongModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly ILoaiLyDoBienDongService _itemService;
         #endregion
         
         #region Ctor

        public LoaiLyDoBienDongModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            ILoaiLyDoBienDongService itemService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
        }
        #endregion
        
        #region LoaiLyDoBienDong
        public LoaiLyDoBienDongSearchModel PrepareLoaiLyDoBienDongSearchModel(LoaiLyDoBienDongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public LoaiLyDoBienDongListModel PrepareLoaiLyDoBienDongListModel(LoaiLyDoBienDongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchLoaiLyDoBienDongs(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new LoaiLyDoBienDongListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<LoaiLyDoBienDongModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public LoaiLyDoBienDongModel PrepareLoaiLyDoBienDongModel(LoaiLyDoBienDongModel model, LoaiLyDoBienDong item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<LoaiLyDoBienDongModel>();
            }
            //more
           
            return model;
        }
       public void PrepareLoaiLyDoBienDong(LoaiLyDoBienDongModel model, LoaiLyDoBienDong item)
        {
		item.MA = model.MA;
		item.TEN = model.TEN;
		item.PARENT_ID = model.PARENT_ID;
		item.TREE_NODE = model.TREE_NODE;
		item.TREE_LEVEL = model.TREE_LEVEL;
            
        }
        #endregion	
     }
}		

