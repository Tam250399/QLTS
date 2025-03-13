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
using GS.Web.Factories.DanhMuc;
using GS.Services.DanhMuc;

namespace GS.Web.Factories.CCDC
{
    public class KiemKeModelFactory:IKiemKeModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IKiemKeService _itemService;
        private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
        private readonly IDonViService _donViService;
        private readonly IDonViBoPhanService _donViBoPhanService;
         #endregion
         
         #region Ctor

        public KiemKeModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IKiemKeService itemService,
            IDonViBoPhanModelFactory donViBoPhanModelFactory,
            IDonViService donViService,
            IDonViBoPhanService donViBoPhanService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
            this._donViBoPhanModelFactory = donViBoPhanModelFactory;
            this._donViService = donViService;
            this._donViBoPhanService = donViBoPhanService;
        }
        #endregion
        
        #region KiemKe
        public KiemKeSearchModel PrepareKiemKeSearchModel(KiemKeSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();
            searchModel.ddlBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true).ToList();
            return searchModel;
        }

        public KiemKeListModel PrepareKiemKeListModel(KiemKeSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchKiemKes(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize,DonViId:_workContext.CurrentDonVi.ID,TuNgay:searchModel.TuNgay,DenNgay:searchModel.DenNgay,BoPhanId:searchModel.BoPhanId);
            
            //prepare list model
            var model = new KiemKeListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareKiemKeListView(new KiemKeModel(), c)),
                Total = items.TotalCount
            };
            return model;
        }
        public KiemKeModel PrepareKiemKeListView(KiemKeModel model, KiemKe item)
        {
            model = item.ToModel<KiemKeModel>();
            model.TenDonVi = _donViService.GetDonViById((Decimal)item.DON_VI_ID).TEN;
            if (item.DON_VI_BO_PHAN_ID != null)
            {
                model.DonViBoPhanText = _donViBoPhanService.GetDonViBoPhanById((Decimal)item.DON_VI_BO_PHAN_ID).TEN;
            }
            model.NgayKiemKeText = item.NGAY_KIEM_KE.toDateVNString();
            return model;
        }
        public KiemKeModel PrapareKiemKe(KiemKeModel model)
        {
            model.TenDonVi = _workContext.CurrentDonVi.TEN_DON_VI;
            model.MaDonVi = _workContext.CurrentDonVi.MA_DON_VI;
            model.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true);
            model.NGAY_KIEM_KE = DateTime.Now;
            return model;
        }
        public KiemKeModel PrepareKiemKeModel(KiemKeModel model, KiemKe item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<KiemKeModel>();
            }
            //more
            //model.NGAY_KIEM_KE = DateTime.Now;
            model.TenDonVi = _workContext.CurrentDonVi.TEN_DON_VI;
            model.MaDonVi = _workContext.CurrentDonVi.MA_DON_VI;
            model.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(valSelected:model.DON_VI_BO_PHAN_ID,isAddFirst: true,DonViId:_workContext.CurrentDonVi.ID);

            return model;
        }
        public void PrepareKiemKe(KiemKeModel model, KiemKe item)
        {
		    item.NGAY_KIEM_KE = model.NGAY_KIEM_KE;
		    item.DON_VI_BO_PHAN_ID = model.DON_VI_BO_PHAN_ID;
		    item.NGUOI_LAP_BIEU = model.NGUOI_LAP_BIEU;
		    item.DAI_DIEN_BO_PHAN = model.DAI_DIEN_BO_PHAN;
		    item.KE_TOAN = model.KE_TOAN;
		    item.TRUONG_BAN = model.TRUONG_BAN;
            item.DON_VI_ID = model.DON_VI_ID;
            item.NGUOI_LAP_BIEU = model.NGUOI_LAP_BIEU;
            item.SO_KIEM_KE = model.SO_KIEM_KE;            
        }
        #endregion	
     }
}		

