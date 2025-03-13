//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
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
using GS.Core.Domain.CCDC;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.CCDC;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.CCDC;

namespace GS.Web.Factories.CCDC
{
    public class CongCuDonViModelFactory:ICongCuDonViModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly ICongCuDonViService _itemService;
         #endregion
         
         #region Ctor

        public CongCuDonViModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            ICongCuDonViService itemService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
        }
        #endregion
        
        #region CongCuDonVi
        public CongCuDonViSearchModel PrepareCongCuDonViSearchModel(CongCuDonViSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public CongCuDonViListModel PrepareCongCuDonViListModel(CongCuDonViSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchCongCuDonVis(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new CongCuDonViListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<CongCuDonViModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public CongCuDonViModel PrepareCongCuDonViModel(CongCuDonViModel model, CongCuDonVi item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<CongCuDonViModel>();
            }
            //more
           
            return model;
        }
       public void PrepareCongCuDonVi(CongCuDonViModel model, CongCuDonVi item)
        {
		item.CONG_CU_ID = model.CONG_CU_ID;
		item.DON_VI_BO_PHAN_ID = model.DON_VI_BO_PHAN_ID;
		item.DON_VI_ID = model.DON_VI_ID;
		item.SO_LUONG_ID = model.SO_LUONG_ID;
            
        }
        #endregion	
     }
}		

