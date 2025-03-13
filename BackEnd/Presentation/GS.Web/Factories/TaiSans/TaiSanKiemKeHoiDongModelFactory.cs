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
using GS.Core.Domain.TaiSans;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.TaiSans;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.TaiSans;

namespace GS.Web.Factories.TaiSans
{
    public class TaiSanKiemKeHoiDongModelFactory:ITaiSanKiemKeHoiDongModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly ITaiSanKiemKeHoiDongService _itemService;
         #endregion
         
         #region Ctor

        public TaiSanKiemKeHoiDongModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            ITaiSanKiemKeHoiDongService itemService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
        }
        #endregion
        
        #region TaiSanKiemKeHoiDong
        public TaiSanKiemKeHoiDongSearchModel PrepareTaiSanKiemKeHoiDongSearchModel(TaiSanKiemKeHoiDongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public TaiSanKiemKeHoiDongListModel PrepareTaiSanKiemKeHoiDongListModel(TaiSanKiemKeHoiDongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchTaiSanKiemKeHoiDongs(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize,KiemKeId: searchModel.KiemKeId);
            
            //prepare list model
            var model = new TaiSanKiemKeHoiDongListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareTaiSanKiemKeHoiDongModel(new TaiSanKiemKeHoiDongModel(),c)),
                Total = items.TotalCount
            };
            return model;
        }
        public TaiSanKiemKeHoiDongModel PrepareTaiSanKiemKeHoiDongModel(TaiSanKiemKeHoiDongModel model, TaiSanKiemKeHoiDong item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<TaiSanKiemKeHoiDongModel>();
                model.TenViTri = item.VI_TRI_ID!=null?((decimal)item.VI_TRI_ID).ToStringViTriHoiDongKiemKe():null;
            }
            //more
           
            return model;
        }
       public void PrepareTaiSanKiemKeHoiDong(TaiSanKiemKeHoiDongModel model, TaiSanKiemKeHoiDong item)
        {
		item.KIEM_KE_ID = model.KIEM_KE_ID;
		item.HO_TEN = model.HO_TEN;
		item.CHUC_VU = model.CHUC_VU;
		item.DAI_DIEN = model.DAI_DIEN;
		item.VI_TRI_ID = model.VI_TRI_ID;
            
        }
        #endregion	
     }
}		

