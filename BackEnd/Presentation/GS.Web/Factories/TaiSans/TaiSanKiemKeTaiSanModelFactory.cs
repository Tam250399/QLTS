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
using GS.Services;
using GS.Web.Factories.DanhMuc;
using GS.Core.Domain.DanhMuc;

namespace GS.Web.Factories.TaiSans
{
    public class TaiSanKiemKeTaiSanModelFactory:ITaiSanKiemKeTaiSanModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly ITaiSanKiemKeTaiSanService _itemService;
        private readonly ITaiSanService _taiSanService;
        private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
         #endregion
         
         #region Ctor

        public TaiSanKiemKeTaiSanModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            ITaiSanKiemKeTaiSanService itemService,
            ITaiSanService taiSanService,
            ILoaiTaiSanModelFactory loaiTaiSanModelFactory
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
            this._taiSanService = taiSanService;
            this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
        }
        #endregion
        
        #region TaiSanKiemKeTaiSan
        public TaiSanKiemKeTaiSanSearchModel PrepareTaiSanKiemKeTaiSanSearchModel(TaiSanKiemKeTaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public TaiSanKiemKeTaiSanListModel PrepareTaiSanKiemKeTaiSanListModel(TaiSanKiemKeTaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchTaiSanKiemKeTaiSans( Keysearch: searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize, KiemKeId: searchModel.KiemKeId, isTaiSanThua: searchModel.isTaiSanThua);
            
            //prepare list model
            var model = new TaiSanKiemKeTaiSanListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareKeKhaiTaiSanForList(new TaiSanKiemKeTaiSanModel(), c)),
                Total = items.TotalCount
            };
            return model;
        }
        public TaiSanKiemKeTaiSanModel PrepareKeKhaiTaiSanForList(TaiSanKiemKeTaiSanModel model, TaiSanKiemKeTaiSan item)
        {
            model = item.ToModel<TaiSanKiemKeTaiSanModel>();
            if (item.TAI_SAN_ID > 0)
            {
                var taisan = _taiSanService.GetTaiSanById((Decimal)item.TAI_SAN_ID);
                model.MaTaiSan = taisan.MA;
                model.TenTaiSan = taisan.TEN;
            }
            model.SoLuongText = item.SO_LUONG_KIEM_KE.ToVNStringNumber();
            model.NguyenGiaText = item.NGUYEN_GIA_KIEM_KE.ToVNStringNumber();
            model.GiaTriConLaiText = item.GIA_TRI_CON_LAI_KIEM_KE.ToVNStringNumber();
            model.TinhTrangText = item.TINH_TRANG_ID.ToStringTrangThaiCongCu();

            return model;
        }

        public TaiSanKiemKeTaiSanModel PrepareTaiSanKiemKeTaiSanModel(TaiSanKiemKeTaiSanModel model, TaiSanKiemKeTaiSan item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<TaiSanKiemKeTaiSanModel>();
                if (item.TAI_SAN_ID > 0)
                {
                    var taisan = _taiSanService.GetTaiSanById((Decimal)item.TAI_SAN_ID);
                    model.MaTaiSan = taisan.MA;
                    model.TenTaiSan = taisan.TEN;
                }
            }
            //more
            model.DDLTinhTrangEnum = ((enumTinhTrang)model.enumTinhTrang).ToSelectList(valuesToExclude:new int[] { (int)enumTinhTrang.THUA });
            model.DDLDeNghiXuLyEnum = ((enumDeNghiXuLy)model.enumDeNghiXuLy).ToSelectList();
            var listloaihinh = new List<decimal> { (int)enumLOAI_HINH_TAI_SAN.DAT, (int)enumLOAI_HINH_TAI_SAN.NHA, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC, (int)enumLOAI_HINH_TAI_SAN.OTO, (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV, (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC, (int)enumLOAI_HINH_TAI_SAN.VO_HINH, (int)enumLOAI_HINH_TAI_SAN.DAC_THU };
            model.DDLNhomTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(isAddFirst:true,valSelected:model.NHOM_TAI_SAN_ID,listLoaiHinhTaiSanId: listloaihinh,isDisabled:false).ToList();

            return model;
        }

        public void PrepareTaiSanKiemKeTaiSan(TaiSanKiemKeTaiSanModel model, TaiSanKiemKeTaiSan item)
        {
		    item.SO_LUONG_KIEM_KE = model.SO_LUONG_KIEM_KE;
            item.GIA_TRI_CON_LAI_KIEM_KE = model.GIA_TRI_CON_LAI_KIEM_KE;
            item.NGUYEN_GIA_KIEM_KE = model.NGUYEN_GIA_KIEM_KE;
            item.TINH_TRANG_ID = model.TINH_TRANG_ID;
            item.DE_NGHI_XU_LY_ID = model.DE_NGHI_XU_LY_ID;          
            item.GHI_CHU = model.GHI_CHU;

            item.KIEN_NGHI_GQTST = model.KIEN_NGHI_GQTST;

            if (item.IS_PHAT_HIEN_THUA)
            {
                item.NHOM_TAI_SAN_ID = model.NHOM_TAI_SAN_ID;
                item.TEN_TAI_SAN_THUA = model.TEN_TAI_SAN_THUA;
            }
        }
        #endregion	
     }
}		

