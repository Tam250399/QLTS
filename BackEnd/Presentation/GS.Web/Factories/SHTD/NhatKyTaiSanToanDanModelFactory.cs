//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
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
using GS.Core.Domain.SHTD;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.SHTD;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.SHTD;

namespace GS.Web.Factories.SHTD
{
    public class NhatKyTaiSanToanDanModelFactory:INhatKyTaiSanToanDanModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly INhatKyTaiSanToanDanService _itemService;
         #endregion
         
         #region Ctor

        public NhatKyTaiSanToanDanModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            INhatKyTaiSanToanDanService itemService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
        }
        #endregion
        
        #region NhatKyTaiSanToanDan
        public NhatKyTaiSanToanDanSearchModel PrepareNhatKyTaiSanToanDanSearchModel(NhatKyTaiSanToanDanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public NhatKyTaiSanToanDanListModel PrepareNhatKyTaiSanToanDanListModel(NhatKyTaiSanToanDanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchNhatKyTaiSanToanDans(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new NhatKyTaiSanToanDanListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<NhatKyTaiSanToanDanModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public NhatKyTaiSanToanDanModel PrepareNhatKyTaiSanToanDanModel(NhatKyTaiSanToanDanModel model, NhatKyTaiSanToanDan item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<NhatKyTaiSanToanDanModel>();
            }
            //more
           
            return model;
        }
       public void PrepareNhatKyTaiSanToanDan(NhatKyTaiSanToanDanModel model, NhatKyTaiSanToanDan item)
        {
		item.QUYET_DINH_ID = model.QUYET_DINH_ID;
		item.NGUOI_DUNG_ID = model.NGUOI_DUNG_ID;
		item.TRANG_THAI_ID = model.TRANG_THAI_ID;
		item.DATA_JSON = model.DATA_JSON;
            
        }
        #endregion	
     }
}		

