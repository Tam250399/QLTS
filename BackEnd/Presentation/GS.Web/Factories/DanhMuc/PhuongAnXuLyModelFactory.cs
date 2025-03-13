//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
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
using GS.Core.Domain.DanhMuc;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.DanhMuc;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Factories.DanhMuc
{
    public class PhuongAnXuLyModelFactory:IPhuongAnXuLyModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IPhuongAnXuLyService _itemService;
         #endregion
         
         #region Ctor

        public PhuongAnXuLyModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IPhuongAnXuLyService itemService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
        }
        #endregion
        
        #region PhuongAnXuLy
        public PhuongAnXuLySearchModel PreparePhuongAnXuLySearchModel(PhuongAnXuLySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public PhuongAnXuLyListModel PreparePhuongAnXuLyListModel(PhuongAnXuLySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchPhuongAnXuLys(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new PhuongAnXuLyListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<PhuongAnXuLyModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public PhuongAnXuLyModel PreparePhuongAnXuLyModel(PhuongAnXuLyModel model, PhuongAnXuLy item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<PhuongAnXuLyModel>();
            }
            //more
           
            return model;
        }
        public List<SelectListItem> DDLPhuongAn()
        {
            var ddl = _itemService.GetAllPhuongAnXuLys().Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.TEN }).ToList();
            ddl.Insert(0, new SelectListItem { Value = "0", Text = "Chọn phương án" });
            return ddl;
        }
        public void PreparePhuongAnXuLy(PhuongAnXuLyModel model, PhuongAnXuLy item)
        {
            item.MA = model.MA;
            item.TEN = model.TEN;
            item.SAP_XEP = model.SAP_XEP;
            item.CONFIG_CAU_HINH = model.CONFIG_CAU_HINH;

        }
        #endregion	
     }
}		

