//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/2/2020
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
using GS.Services.DanhMuc;

namespace GS.Web.Factories.CCDC
{
    public class GiamHongmatModelFactory:IGiamHongmatModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IGiamHongmatService _itemService;
        private readonly IXuatNhapService _xuatNhapService;
        private readonly INhapXuatCongCuService _nhapXuatCongCuService;
        private readonly ICongCuService _congCuService;
        private readonly INhomCongCuService _nhomCongCuService;
        private readonly IDonViBoPhanService _donViBoPhanService;
         #endregion
         
         #region Ctor

        public GiamHongmatModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IGiamHongmatService itemService,
            IXuatNhapService xuatNhapService,
            INhapXuatCongCuService nhapXuatCongCuService,
            ICongCuService congCuService,
            INhomCongCuService nhomCongCuService,
            IDonViBoPhanService donViBoPhanService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
            this._xuatNhapService = xuatNhapService;
            this._nhapXuatCongCuService = nhapXuatCongCuService;
            this._congCuService = congCuService;
            this._nhomCongCuService = nhomCongCuService;
            this._donViBoPhanService = donViBoPhanService;
        }
        #endregion
        
        #region GiamHongmat
        public GiamHongmatSearchModel PrepareGiamHongmatSearchModel(GiamHongmatSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public GiamHongmatListModel PrepareGiamHongmatListModel(GiamHongmatSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchGiamHongmats(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new GiamHongmatListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareGiamHongMatForList(new GiamHongmatModel(), c)),
                Total = items.TotalCount
            };
            return model;
        }
        public GiamHongmatModel PrepareGiamHongMatForList(GiamHongmatModel model, GiamHongmat item)
        {
            var map = _nhapXuatCongCuService.GetNhapXuatCongCu(CongCuId: item.CONG_CU_ID, NhapXuatId: item.NHAP_XUAT_ID);
            model = item.ToModel<GiamHongmatModel>();
            model.MaCongCuText = map.CongCu.MA;
            model.TenCongCuText = map.CongCu.TEN;
            model.BoPhanSuDungText = _donViBoPhanService.GetDonViBoPhanById((Decimal)item.DON_VI_BO_PHAN_ID).TEN;
            model.NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((Decimal)map.CongCu.NHOM_CONG_CU_ID).TEN;

            return model;
        }
        public GiamHongmatModel PrepareGiamHongmatModel(GiamHongmatModel model, GiamHongmat item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<GiamHongmatModel>();
            }
            //more
           
            return model;
        }
        public void PrepareGiamHongmat(GiamHongmatModel model, GiamHongmat item)
        {
		    item.LY_DO = model.LY_DO;
		    item.GHI_CHU = model.GHI_CHU;
            item.SO_LUONG = model.SO_LUONG;
            item.NGAY_LAP = model.NGAY_LAP;
        }

        public GiamHongmatModel PrepareGiamHongMatModel(GiamHongmatModel model, Decimal MapId, Decimal BoPhanId)
        {
            var map = _nhapXuatCongCuService.GetSoLuongDangCoByMapId(MapId);
            var xn = _xuatNhapService.GetXuatNhapById(map.NHAP_XUAT_ID);
            model.NgayPhanBo =xn.NGAY_XUAT_NHAP;
            model.MapId = map.ID;
            model.MaCongCuText = map.CongCu.MA;
            model.TenCongCuText = map.CongCu.TEN;
            model.SoLuongText = map.SoLuongCoThePhanBo.ToVNStringNumber();
            model.NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((Decimal)map.CongCu.NHOM_CONG_CU_ID).TEN;
            model.DonGiaText = map.DON_GIA.ToVNStringNumber();
            model.DON_VI_BO_PHAN_ID = BoPhanId;
            model.BoPhanSuDungText = _donViBoPhanService.GetDonViBoPhanById(BoPhanId).TEN;

            return model;
        }

        public GiamHongmatListModel PrepareGiamHongMatForChonCongCu(GiamHongmatSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var items = _nhapXuatCongCuService.GetNhapXuatCongCusByBaoHanh(searchModel.DonViBoPhanId,KeySearch: searchModel.KeySearchCongCu, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, DonViId: _workContext.CurrentDonVi.ID);

            //prepare list model
            var model = new GiamHongmatListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareCongCu(new GiamHongmatModel(), c, searchModel.DonViBoPhanId)),
                Total = items.TotalCount
            };
            return model;
        }

        public GiamHongmatModel PrepareCongCu(GiamHongmatModel model, NhapXuatCongCu map, Decimal BoPhanId)
        {
            model.MapId = map.ID;
            model.MaCongCuText = map.CongCu.MA;
            model.TenCongCuText = map.CongCu.TEN;
            model.SoLuongText = map.SoLuongCoThePhanBo.ToVNStringNumber();
            model.NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((Decimal)map.CongCu.NHOM_CONG_CU_ID).TEN;
            model.DonGiaText = map.DON_GIA.ToVNStringNumber();
            model.DON_VI_BO_PHAN_ID = BoPhanId;
            model.BoPhanSuDungText = _donViBoPhanService.GetDonViBoPhanById((decimal)map.XuatNhap.DON_VI_BO_PHAN_ID).TEN;

            return model;
        }
        #endregion
    }
}		

