//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2021
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
    public class DBTempTaiSanCuModelFactory:IDBTempTaiSanCuModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IDBTempTaiSanCuService _itemService;
         #endregion
         
         #region Ctor

        public DBTempTaiSanCuModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IDBTempTaiSanCuService itemService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
        }
        #endregion
        
        #region DBTempTaiSanCu
        public DBTempTaiSanCuSearchModel PrepareDBTempTaiSanCuSearchModel(DBTempTaiSanCuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public DBTempTaiSanCuListModel PrepareDBTempTaiSanCuListModel(DBTempTaiSanCuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchDBTempTaiSanCus(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new DBTempTaiSanCuListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<DBTempTaiSanCuModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public DBTempTaiSanCuModel PrepareDBTempTaiSanCuModel(DBTempTaiSanCuModel model, DBTempTaiSanCu item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DBTempTaiSanCuModel>();
            }
            //more
           
            return model;
        }
       public void PrepareDBTempTaiSanCu(DBTempTaiSanCuModel model, DBTempTaiSanCu item)
        {
		item.MA_TAI_SAN = model.MA_TAI_SAN;
		item.TEN_TAI_SAN = model.TEN_TAI_SAN;
		item.NGUON_ID = model.NGUON_ID;
		item.LOAI_TAI_SAN_MA = model.LOAI_TAI_SAN_MA;
		item.NGAY_NHAP = model.NGAY_NHAP;
		item.NGAY_DUYET = model.NGAY_DUYET;
		item.NGAY_SU_DUNG = model.NGAY_SU_DUNG;
		item.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;
		item.DON_VI_MA = model.DON_VI_MA;
		item.TRANG_THAI = model.TRANG_THAI;
		item.DON_VI_BO_PHAN_MA = model.DON_VI_BO_PHAN_MA;
		item.NUOC_SAN_XUAT_MA = model.NUOC_SAN_XUAT_MA;
		item.NAM_SAN_XUAT = model.NAM_SAN_XUAT;
		item.CHE_DO_HAO_MON_ID = model.CHE_DO_HAO_MON_ID;
		item.NHOM_DON_VI_MA = model.NHOM_DON_VI_MA;
		item.NAM = model.NAM;
		item.NGAY_TAO = model.NGAY_TAO;
		item.NGAY_SUA = model.NGAY_SUA;
		item.DATA_JSON = model.DATA_JSON;
		item.RESPONSE = model.RESPONSE;
		item.TRANG_THAI_DB = model.TRANG_THAI_DB;
            
        }
        #endregion	
     }
}		

