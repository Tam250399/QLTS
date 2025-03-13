//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
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
using GS.Core.Domain.ThuocTinhs;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.ThuocTinhs;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.ThuocTinh;

namespace GS.Web.Factories.ThuocTinhs
{
    public class ThuocTinhTaiSanModelFactory:IThuocTinhTaiSanModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IThuocTinhTaiSanService _itemService;
         #endregion
         
         #region Ctor

        public ThuocTinhTaiSanModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IThuocTinhTaiSanService itemService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
        }
        #endregion
        
        #region ThuocTinhTaiSan
        public ThuocTinhTaiSanSearchModel PrepareThuocTinhTaiSanSearchModel(ThuocTinhTaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public ThuocTinhTaiSanListModel PrepareThuocTinhTaiSanListModel(ThuocTinhTaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchThuocTinhTaiSans(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new ThuocTinhTaiSanListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<ThuocTinhTaiSanModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public ThuocTinhTaiSanModel PrepareThuocTinhTaiSanModel(ThuocTinhTaiSanModel model, ThuocTinhTaiSan item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<ThuocTinhTaiSanModel>();
            }
            //more
           
            return model;
        }
       public void PrepareThuocTinhTaiSan(ThuocTinhTaiSanModel model, ThuocTinhTaiSan item)
        {
		item.THUOC_TINH_ID = model.THUOC_TINH_ID;
		item.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;
		item.SAP_XEP = model.SAP_XEP;
		item.TREE_NOTE = model.TREE_NOTE;
		item.LOAI_TAI_SAN_ID = model.LOAI_TAI_SAN_ID;
            
        }
        #endregion	
     }
}		

