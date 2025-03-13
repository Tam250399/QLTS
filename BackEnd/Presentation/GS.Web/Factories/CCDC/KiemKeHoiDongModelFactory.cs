//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
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
    public class KiemKeHoiDongModelFactory:IKiemKeHoiDongModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IKiemKeHoiDongService _itemService;
         #endregion
         
         #region Ctor

        public KiemKeHoiDongModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IKiemKeHoiDongService itemService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
        }
        #endregion
        
        #region KiemKeHoiDong
        public KiemKeHoiDongSearchModel PrepareKiemKeHoiDongSearchModel(KiemKeHoiDongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public KiemKeHoiDongListModel PrepareKiemKeHoiDongListModel(KiemKeHoiDongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchKiemKeHoiDongs(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new KiemKeHoiDongListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<KiemKeHoiDongModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public KiemKeHoiDongListModel PrepareKiemKeHoiDongListModelForKiemKe(KiemKeHoiDongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchKiemKeHoiDongsForKiemKe(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize,KiemKeId:searchModel.KiemKeId);

            //prepare list model
            var model = new KiemKeHoiDongListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareKiemKeHoiDongModel(new KiemKeHoiDongModel(),c,false)),
                Total = items.TotalCount
            };
            return model;
        }
        public KiemKeHoiDongModel PrepareKiemKeHoiDongModel(KiemKeHoiDongModel model, KiemKeHoiDong item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<KiemKeHoiDongModel>();
                model.TenViTri =(item.VI_TRI_ID).ToStringViTriHoiDongKiemKe();
            }
            //more
           
            return model;
        }
       public void PrepareKiemKeHoiDong(KiemKeHoiDongModel model, KiemKeHoiDong item)
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

