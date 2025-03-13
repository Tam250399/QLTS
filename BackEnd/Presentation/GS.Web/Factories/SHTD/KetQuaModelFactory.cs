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
using GS.Web.Factories.DanhMuc;

namespace GS.Web.Factories.SHTD
{
    public class KetQuaModelFactory:IKetQuaModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IKetQuaService _itemService;
            private readonly IDonViModelFactory _donViModelFactory;
         #endregion
         
         #region Ctor

        public KetQuaModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IKetQuaService itemService,
            IDonViModelFactory donViModelFactory
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
            this._donViModelFactory = donViModelFactory;
        }
        #endregion
        
        #region KetQua
        public KetQuaSearchModel PrepareKetQuaSearchModel(KetQuaSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public KetQuaListModel PrepareKetQuaListModel(KetQuaSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchKetQuas(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new KetQuaListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<KetQuaModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public KetQuaModel PrepareKetQuaModel(KetQuaModel model, KetQua item, bool excludeProperties = false,bool isDDL = false)
        {
            if (item != null)
            {
                var isDelete = model.is_Delete;
                //fill in model values from the entity
                model = item.ToModel<KetQuaModel>();
                model.is_Delete = isDelete;
                model.SoLuongXuLy = (decimal)item.taiSanTdXuLy.SO_LUONG;
            }
            //more
            if (isDDL)
            {
                model.DDLDonVi = _donViModelFactory.PrepareSelectListBoNganhTinh(valSelected: model.TAI_SAN_TD_XU_LY_ID, isAddFirst: true).ToList();
            }
            return model;
        }
        public void PrepareKetQua(KetQuaModel model, KetQua item)
        {
            item.TAI_SAN_TD_XU_LY_ID = model.TAI_SAN_TD_XU_LY_ID;
            item.DON_VI_CHUYEN_ID = model.DON_VI_CHUYEN_ID;
            item.GIA_TRI_NSNN = model.GIA_TRI_NSNN;
            item.GIA_TRI_TKTG = model.GIA_TRI_TKTG;
            item.CHI_PHI_XU_LY = model.CHI_PHI_XU_LY;
            item.HOP_DONG_SO = model.HOP_DONG_SO;
            item.HOP_DONG_NGAY = model.HOP_DONG_NGAY;
            item.GUID = model.GUID;
            item.GIA_TRI_BAN = model.GIA_TRI_BAN;
            item.HO_SO_GIAY_TO_KHAC = model.HO_SO_GIAY_TO_KHAC;
            item.NGAY_XU_LY = model.NGAY_XU_LY;
            item.SO_LUONG = model.SO_LUONG;

        }
        #endregion	
     }
}		

