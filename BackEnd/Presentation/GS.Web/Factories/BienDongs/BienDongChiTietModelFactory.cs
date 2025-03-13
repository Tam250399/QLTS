//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.BienDongs;
using GS.Services.BienDongs;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Models.BienDongs;
using System;
using System.Linq;

namespace GS.Web.Factories.BienDongs
{
    public class BienDongChiTietModelFactory : IBienDongChiTietModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IBienDongChiTietService _itemService;
        #endregion

        #region Ctor

        public BienDongChiTietModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IBienDongChiTietService itemService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region BienDongChiTiet
        public BienDongChiTietSearchModel PrepareBienDongChiTietSearchModel(BienDongChiTietSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public BienDongChiTietListModel PrepareBienDongChiTietListModel(BienDongChiTietSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchBienDongChiTiets(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new BienDongChiTietListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<BienDongChiTietModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public BienDongChiTietModel PrepareBienDongChiTietModel(BienDongChiTietModel model, BienDongChiTiet item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<BienDongChiTietModel>();
            }
            //more

            return model;
        }
        public void PrepareBienDongChiTiet(BienDongChiTietModel model, BienDongChiTiet item)
        {
            item.BIEN_DONG_ID = model.BIEN_DONG_ID;
            item.HINH_THUC_MUA_SAM_ID = model.HINH_THUC_MUA_SAM_ID;
            item.MUC_DICH_SU_DUNG_ID = model.MUC_DICH_SU_DUNG_ID;
            item.NHAN_HIEU = model.NHAN_HIEU;
            item.SO_HIEU = model.SO_HIEU;
            item.DIA_BAN_ID = model.DIA_BAN_ID;
            item.DATA_JSON = model.DATA_JSON;
            item.NGUYEN_GIA = model.NGUYEN_GIA;
            item.DAT_TONG_DIEN_TICH = model.DAT_TONG_DIEN_TICH;
            item.HTSD_QUAN_LY_NHA_NUOC = model.HTSD_QUAN_LY_NHA_NUOC;
            item.HTSD_HDSN_KINH_DOANH_KHONG = model.HTSD_HDSN_KINH_DOANH_KHONG;
            item.HTSD_HDSN_KINH_DOANH = model.HTSD_HDSN_KINH_DOANH;
            item.HTSD_CHO_THUE = model.HTSD_CHO_THUE;
            item.HTSD_LIEN_DOANH = model.HTSD_LIEN_DOANH;
            item.HTSD_SU_DUNG_HON_HOP = model.HTSD_SU_DUNG_HON_HOP;
            item.HTSD_SU_DUNG_KHAC = model.HTSD_SU_DUNG_KHAC;
            item.HM_SO_NAM_CON_LAI = model.HM_SO_NAM_CON_LAI;
            item.HM_TY_LE_HAO_MON = model.HM_TY_LE_HAO_MON;
            item.HM_LUY_KE = model.HM_LUY_KE;
            item.HM_GIA_TRI_CON_LAI = model.HM_GIA_TRI_CON_LAI;
            item.KH_NGAY_BAT_DAU = model.KH_NGAY_BAT_DAU;
            item.KH_THANG_CON_LAI = model.KH_THANG_CON_LAI;
            item.KH_GIA_TINH_KHAU_HAO = model.KH_GIA_TINH_KHAU_HAO;
            item.KH_GIA_TRI_TRICH_THANG = model.KH_GIA_TRI_TRICH_THANG;
            item.KH_LUY_KE = model.KH_LUY_KE;
            item.KH_CON_LAI = model.KH_CON_LAI;
            item.NHA_SO_TANG = model.NHA_SO_TANG;
            item.NHA_NAM_XAY_DUNG = model.NHA_NAM_XAY_DUNG;
            item.NHA_DIEN_TICH_XD = model.NHA_DIEN_TICH_XD;
            item.NHA_TONG_DIEN_TICH_XD = model.NHA_TONG_DIEN_TICH_XD;
            item.VKT_DIEN_TICH = model.VKT_DIEN_TICH;
            item.VKT_THE_TICH = model.VKT_THE_TICH;
            item.VKT_CHIEU_DAI = model.VKT_CHIEU_DAI;
            item.OTO_BIEN_KIEM_SOAT = model.OTO_BIEN_KIEM_SOAT;
            item.OTO_SO_CHO_NGOI = model.OTO_SO_CHO_NGOI;
            item.OTO_SO_CAU_XE = model.OTO_SO_CAU_XE;
            item.OTO_CHUC_DANH_ID = model.OTO_CHUC_DANH_ID;
            item.OTO_NHAN_XE_ID = model.OTO_NHAN_XE_ID;
            item.OTO_TAI_TRONG = model.OTO_TAI_TRONG;
            item.OTO_CONG_XUAT = model.OTO_CONG_XUAT;
            item.OTO_XI_LANH = model.OTO_XI_LANH;
            item.OTO_SO_KHUNG = model.OTO_SO_KHUNG;
            item.OTO_SO_MAY = model.OTO_SO_MAY;
            item.THONG_SO_KY_THUAT = model.THONG_SO_KY_THUAT;
            item.CLN_SO_NAM = model.CLN_SO_NAM;
            item.NHA_DIA_CHI = model.NHA_DIA_CHI;

        }
        public virtual void PrepareBienDongChiTietFromBDCT_TDTT(BienDongChiTiet item, BienDongChiTiet bienDongCT_TDTT = null)
        {
            if (bienDongCT_TDTT!= null)
            {
                //đất, nhà
                item.DIA_CHI = bienDongCT_TDTT.DIA_CHI;
                item.DIA_BAN_ID = bienDongCT_TDTT.DIA_BAN_ID;
                item.NHA_DIA_CHI = bienDongCT_TDTT.NHA_DIA_CHI;
                //ô tô
                item.OTO_BIEN_KIEM_SOAT = bienDongCT_TDTT.OTO_BIEN_KIEM_SOAT;
                item.OTO_CONG_XUAT = bienDongCT_TDTT.OTO_CONG_XUAT;
                item.OTO_NHAN_XE_ID = bienDongCT_TDTT.OTO_NHAN_XE_ID;
                item.OTO_SO_CHO_NGOI = bienDongCT_TDTT.OTO_SO_CHO_NGOI;
                item.OTO_SO_CAU_XE = bienDongCT_TDTT.OTO_SO_CAU_XE;
                item.OTO_TAI_TRONG = bienDongCT_TDTT.OTO_TAI_TRONG;
                item.OTO_CHUC_DANH_ID = bienDongCT_TDTT.OTO_CHUC_DANH_ID;
                //hồ sơ
                item.HS_QUYET_DINH_BAN_GIAO = bienDongCT_TDTT.HS_QUYET_DINH_BAN_GIAO;
                item.HS_QUYET_DINH_BAN_GIAO_NGAY = bienDongCT_TDTT.HS_QUYET_DINH_BAN_GIAO_NGAY;
                item.HS_BIEN_BAN_NGHIEM_THU = bienDongCT_TDTT.HS_BIEN_BAN_NGHIEM_THU;
                item.HS_BIEN_BAN_NGHIEM_THU_NGAY = bienDongCT_TDTT.HS_BIEN_BAN_NGHIEM_THU_NGAY;
                item.HS_PHAP_LY_KHAC = bienDongCT_TDTT.HS_PHAP_LY_KHAC;
                item.HS_PHAP_LY_KHAC_NGAY = bienDongCT_TDTT.HS_PHAP_LY_KHAC_NGAY;
                item.HS_CNQSD_SO = bienDongCT_TDTT.HS_CNQSD_SO;
                item.HS_CNQSD_NGAY = bienDongCT_TDTT.HS_CNQSD_NGAY;
                item.HS_QUYET_DINH_GIAO_SO = bienDongCT_TDTT.HS_QUYET_DINH_GIAO_SO;
                item.HS_QUYET_DINH_GIAO_NGAY = bienDongCT_TDTT.HS_QUYET_DINH_GIAO_NGAY;
                item.HS_CHUYEN_NHUONG_SD_SO = bienDongCT_TDTT.HS_CHUYEN_NHUONG_SD_SO;
                item.HS_CHUYEN_NHUONG_SD_NGAY = bienDongCT_TDTT.HS_CHUYEN_NHUONG_SD_NGAY;
                item.HS_QUYET_DINH_CHO_THUE_SO = bienDongCT_TDTT.HS_QUYET_DINH_CHO_THUE_SO;
                item.HS_QUYET_DINH_CHO_THUE_NGAY = bienDongCT_TDTT.HS_QUYET_DINH_CHO_THUE_NGAY;
            }
        }
        #endregion
    }
}

