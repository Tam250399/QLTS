//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Services;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.NghiepVu;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.TaiSans;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.NghiepVu
{
    public class YeuCauChiTietModelFactory : IYeuCauChiTietModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IYeuCauChiTietService _itemService;
        private readonly IYeuCauService _yeucauService;
        private readonly ITaiSanService _taiSanService;
        private readonly IBienDongService _bienDongService;
        private readonly IBienDongChiTietService _bienDongChiTietService;
        private readonly ITrungGianBDYCService _trungGianBDYCService;
        private readonly IChucDanhService _chucDanhService;
        private readonly ITaiSanHienTrangSuDungService _taiSanHienTrangSuDungService;
        private readonly ILyDoBienDongService _lyDoBienDongService;
        private readonly IHienTrangService _hienTrangService;
        private readonly IHienTrangModelFactory _hienTrangModelFactory;

        // private readonly ITaiSanModelFactory _taiSanModelFactory;
        #endregion

        #region Ctor

        public YeuCauChiTietModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IYeuCauChiTietService itemService,
            IYeuCauService yeucauService,
            ITaiSanService taiSanService,
            IBienDongService bienDongService,
            IBienDongChiTietService bienDongChiTietService,
            ITrungGianBDYCService trungGianBDYCService,
            IChucDanhService chucDanhService,
            ITaiSanHienTrangSuDungService taiSanHienTrangSuDungService,
            ILyDoBienDongService lyDoBienDongService,
            IHienTrangService hienTrangService,
            IHienTrangModelFactory hienTrangModelFactory
            //ITaiSanModelFactory taiSanModelFactory
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._yeucauService = yeucauService;
            this._taiSanService = taiSanService;
            this._bienDongService = bienDongService;
            //this._taiSanModelFactory = taiSanModelFactory;
            this._bienDongChiTietService = bienDongChiTietService;
            this._trungGianBDYCService = trungGianBDYCService;
            this._chucDanhService = chucDanhService;
            this._taiSanHienTrangSuDungService = taiSanHienTrangSuDungService;
            this._lyDoBienDongService = lyDoBienDongService;
            this._hienTrangService = hienTrangService;
            _hienTrangModelFactory = hienTrangModelFactory;
        }
        #endregion

        #region Yêu cầu chỉ tiết
        public YeuCauChiTietSearchModel PrepareYeuCauChiTietSearchModel(YeuCauChiTietSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public YeuCauChiTietListModel PrepareYeuCauChiTietListModel(YeuCauChiTietSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchYeuCauChiTiets(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new YeuCauChiTietListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<YeuCauChiTietModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public YeuCauChiTietModel PrepareYeuCauChiTietModel(YeuCauChiTietModel model, YeuCauChiTiet item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<YeuCauChiTietModel>();
            }
            //more
            return model;
        }
        public YeuCauChiTiet PrepareYeuCauChiTiet(YeuCauChiTiet item, TaiSanModel taisanModel)
        {
            var yeucau = _yeucauService.GetYeuCauById(item.YEU_CAU_ID);
            var HienTrangList = new HienTrangList()
            {
                TaiSanId = taisanModel.ID,
                lstObjHienTrang = taisanModel.nvYeuCauChiTietModel.lstHienTrang,
            };
            item.HTSD_JSON = HienTrangList.toStringJson();
            switch (yeucau.LOAI_HINH_TAI_SAN_ID)
            {
                case (int)enumLOAI_HINH_TAI_SAN.DAT:
                    item.HINH_THUC_MUA_SAM_ID = taisanModel.nvYeuCauChiTietModel.HINH_THUC_MUA_SAM_ID;
                    item.MUC_DICH_SU_DUNG_ID = taisanModel.nvYeuCauChiTietModel.MUC_DICH_SU_DUNG_ID;
                    item.DIA_BAN_ID = taisanModel.taisandatModel.DIA_BAN_ID;
                    item.DAT_TONG_DIEN_TICH = taisanModel.taisandatModel.DIEN_TICH;
                    item.NGUYEN_GIA = taisanModel.nvYeuCauChiTietModel.NGUYEN_GIA;
                    item.DAT_GIA_TRI_QUYEN_SD_DAT = taisanModel.nvYeuCauChiTietModel.DAT_GIA_TRI_QUYEN_SD_DAT;
                    item.HS_CNQSD_SO = taisanModel.taisandatModel.HS_CNQSD_SO;
                    item.HS_CNQSD_NGAY = taisanModel.taisandatModel.HS_CNQSD_NGAY;
                    item.HS_CHUYEN_NHUONG_SD_SO = taisanModel.taisandatModel.HS_CHUYEN_NHUONG_SD_SO;
                    item.HS_CHUYEN_NHUONG_SD_NGAY = taisanModel.taisandatModel.HS_CHUYEN_NHUONG_SD_NGAY;
                    item.HS_QUYET_DINH_CHO_THUE_SO = taisanModel.taisandatModel.HS_QUYET_DINH_CHO_THUE_SO;
                    item.HS_QUYET_DINH_CHO_THUE_NGAY = taisanModel.taisandatModel.HS_QUYET_DINH_CHO_THUE_NGAY;
                    item.HS_HOP_DONG_CHO_THUE_SO = taisanModel.taisandatModel.HS_HOP_DONG_CHO_THUE_SO;
                    item.HS_HOP_DONG_CHO_THUE_NGAY = taisanModel.taisandatModel.HS_HOP_DONG_CHO_THUE_NGAY;
                    item.HS_QUYET_DINH_GIAO_SO = taisanModel.taisandatModel.HS_QUYET_DINH_GIAO_SO;
                    item.HS_QUYET_DINH_GIAO_NGAY = taisanModel.taisandatModel.HS_QUYET_DINH_GIAO_NGAY;
                    item.HS_PHAP_LY_KHAC = taisanModel.taisandatModel.HS_PHAP_LY_KHAC;
                    item.HS_PHAP_LY_KHAC_NGAY = taisanModel.taisandatModel.HS_PHAP_LY_KHAC_NGAY;

                    break;
                case (int)enumLOAI_HINH_TAI_SAN.NHA:
                    item.HINH_THUC_MUA_SAM_ID = taisanModel.nvYeuCauChiTietModel.HINH_THUC_MUA_SAM_ID;
                    item.MUC_DICH_SU_DUNG_ID = taisanModel.nvYeuCauChiTietModel.MUC_DICH_SU_DUNG_ID;
                    item.NGUYEN_GIA = taisanModel.nvYeuCauChiTietModel.NGUYEN_GIA;
                    item.HM_SO_NAM_CON_LAI = taisanModel.nvYeuCauChiTietModel.HM_SO_NAM_CON_LAI;
                    item.HM_LUY_KE = taisanModel.nvYeuCauChiTietModel.HM_LUY_KE;
                    item.HM_TY_LE_HAO_MON = taisanModel.nvYeuCauChiTietModel.HM_TY_LE_HAO_MON;
                    item.HM_GIA_TRI_CON_LAI = taisanModel.nvYeuCauChiTietModel.HM_GIA_TRI_CON_LAI;
                    item.KH_CON_LAI = taisanModel.nvYeuCauChiTietModel.KH_CON_LAI;
                    item.KH_GIA_TINH_KHAU_HAO = (taisanModel.nvYeuCauChiTietModel.KH_TY_LE_NGUYEN_GIA_KHAU_HAO ?? 0) * taisanModel.nvYeuCauChiTietModel.NGUYEN_GIA;
                    //item.KH_GIA_TINH_KHAU_HAO = taisanModel.nvYeuCauChiTietModel.KH_GIA_TINH_KHAU_HAO;
                    item.KH_GIA_TRI_TRICH_THANG = taisanModel.nvYeuCauChiTietModel.KH_GIA_TRI_TRICH_THANG;
                    item.KH_TY_LE_NGUYEN_GIA_KHAU_HAO = taisanModel.nvYeuCauChiTietModel.KH_TY_LE_NGUYEN_GIA_KHAU_HAO;
                    item.KH_LUY_KE = taisanModel.nvYeuCauChiTietModel.KH_LUY_KE;
                    item.KH_NGAY_BAT_DAU = taisanModel.nvYeuCauChiTietModel.KH_NGAY_BAT_DAU;
                    item.KH_THANG_CON_LAI = taisanModel.nvYeuCauChiTietModel.KH_THANG_CON_LAI;
                    item.NHA_NAM_XAY_DUNG = taisanModel.nvYeuCauChiTietModel.NHA_NAM_XAY_DUNG;
                    item.NHA_DIEN_TICH_XD = taisanModel.taisannhaModel.DIEN_TICH_XAY_DUNG;
                    item.NHA_SO_TANG = taisanModel.taisannhaModel.NHA_SO_TANG;
                    item.NHA_TONG_DIEN_TICH_XD = taisanModel.taisannhaModel.DIEN_TICH_SAN_XAY_DUNG;
                    item.DIA_BAN_ID = taisanModel.taisannhaModel.DIA_BAN_ID;
                    //item.HS_BIEN_BAN_NGHIEM_THU = taisanModel.taisannhaModel.HS_BIEN_BAN_NGHIEM_THU;
                    //item.HS_BIEN_BAN_NGHIEM_THU_NGAY = taisanModel.taisannhaModel.HS_BIEN_BAN_NGHIEM_THU_NGAY;
                    //item.HS_QUYET_DINH_BAN_GIAO = taisanModel.taisannhaModel.HS_QUYET_DINH_BAN_GIAO;
                    //item.HS_QUYET_DINH_BAN_GIAO_NGAY = taisanModel.taisannhaModel.HS_QUYET_DINH_BAN_GIAO_NGAY;
                    //item.HS_PHAP_LY_KHAC = taisanModel.taisannhaModel.HS_PHAP_LY_KHAC;
                    //item.HS_PHAP_LY_KHAC_NGAY = taisanModel.taisannhaModel.HS_PHAP_LY_KHAC_NGAY;
                    //item.NHA_SO_TANG = taisanModel.taisannhaModel.NHA_SO_TANG;
                    //item.NHA_DIEN_TICH_XD = taisanModel.taisannhaModel.DIEN_TICH_XAY_DUNG;
                    //item.NHA_TONG_DIEN_TICH_XD = taisanModel.taisannhaModel.DIEN_TICH_SAN_XAY_DUNG;
                    //var NhaHienTrangList = new HienTrangList()
                    //{
                    //    TaiSanId = taisanModel.ID,
                    //    lstObjHienTrang = taisanModel.taisannhaModel.lstHienTrang
                    //};
                    //item.HTSD_JSON = NhaHienTrangList.toStringJson();
                    break;
                case (int)enumLOAI_HINH_TAI_SAN.OTO:
                case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                    item.HINH_THUC_MUA_SAM_ID = taisanModel.nvYeuCauChiTietModel.HINH_THUC_MUA_SAM_ID;
                    item.MUC_DICH_SU_DUNG_ID = taisanModel.nvYeuCauChiTietModel.MUC_DICH_SU_DUNG_ID;
                    item.NGUYEN_GIA = taisanModel.nvYeuCauChiTietModel.NGUYEN_GIA;
                    item.HM_SO_NAM_CON_LAI = taisanModel.nvYeuCauChiTietModel.HM_SO_NAM_CON_LAI;
                    item.HM_LUY_KE = taisanModel.nvYeuCauChiTietModel.HM_LUY_KE;
                    item.HM_TY_LE_HAO_MON = taisanModel.nvYeuCauChiTietModel.HM_TY_LE_HAO_MON;
                    item.HM_GIA_TRI_CON_LAI = taisanModel.nvYeuCauChiTietModel.HM_GIA_TRI_CON_LAI;
                    item.KH_CON_LAI = taisanModel.nvYeuCauChiTietModel.KH_CON_LAI;
                    //item.KH_GIA_TINH_KHAU_HAO = taisanModel.nvYeuCauChiTietModel.KH_GIA_TINH_KHAU_HAO;
                    item.KH_GIA_TINH_KHAU_HAO = (taisanModel.nvYeuCauChiTietModel.KH_TY_LE_NGUYEN_GIA_KHAU_HAO ?? 0) * taisanModel.nvYeuCauChiTietModel.NGUYEN_GIA;
                    item.KH_GIA_TRI_TRICH_THANG = taisanModel.nvYeuCauChiTietModel.KH_GIA_TRI_TRICH_THANG;
                    item.KH_TY_LE_NGUYEN_GIA_KHAU_HAO = taisanModel.nvYeuCauChiTietModel.KH_TY_LE_NGUYEN_GIA_KHAU_HAO;
                    item.KH_LUY_KE = taisanModel.nvYeuCauChiTietModel.KH_LUY_KE;
                    item.KH_NGAY_BAT_DAU = taisanModel.nvYeuCauChiTietModel.KH_NGAY_BAT_DAU;
                    item.KH_THANG_CON_LAI = taisanModel.nvYeuCauChiTietModel.KH_THANG_CON_LAI;
                    //Thông tin riêng tài sản
                    item.OTO_BIEN_KIEM_SOAT = taisanModel.taisanOtoModel.BIEN_KIEM_SOAT;
                    item.OTO_LIST_CHUC_DANH_ID = taisanModel.taisanOtoModel.GetChucDanhsBySelectedList();
                    if (taisanModel.taisanOtoModel.CHUC_DANH_ID > 0)
                    {
                        item.OTO_CHUC_DANH_ID = taisanModel.taisanOtoModel.CHUC_DANH_ID;//_chucDanhService.GetChucDanhById(taisanModel.taisanOtoModel.CHUC_DANH_ID ?? 0).TEN_CHUC_DANH;
                    }
                    item.OTO_CONG_XUAT = taisanModel.taisanOtoModel.CONG_XUAT;
                    item.OTO_NHAN_XE_ID = taisanModel.taisanOtoModel.NHAN_XE_ID;
                    item.OTO_SO_CHO_NGOI = taisanModel.taisanOtoModel.SO_CHO_NGOI;
                    item.OTO_SO_CAU_XE = taisanModel.taisanOtoModel.SO_CAU_XE;
                    item.OTO_SO_KHUNG = taisanModel.taisanOtoModel.SO_KHUNG;
                    item.OTO_SO_MAY = taisanModel.taisanOtoModel.SO_MAY;
                    item.OTO_TAI_TRONG = taisanModel.taisanOtoModel.TAI_TRONG;
                    item.OTO_XI_LANH = taisanModel.taisanOtoModel.DUNG_TICH;
                    break;
                case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                    item.HINH_THUC_MUA_SAM_ID = taisanModel.nvYeuCauChiTietModel.HINH_THUC_MUA_SAM_ID;
                    item.MUC_DICH_SU_DUNG_ID = taisanModel.nvYeuCauChiTietModel.MUC_DICH_SU_DUNG_ID;
                    item.NGUYEN_GIA = taisanModel.nvYeuCauChiTietModel.NGUYEN_GIA;
                    item.HM_SO_NAM_CON_LAI = taisanModel.nvYeuCauChiTietModel.HM_SO_NAM_CON_LAI;
                    item.HM_LUY_KE = taisanModel.nvYeuCauChiTietModel.HM_LUY_KE;
                    item.HM_TY_LE_HAO_MON = taisanModel.nvYeuCauChiTietModel.HM_TY_LE_HAO_MON;
                    item.HM_GIA_TRI_CON_LAI = taisanModel.nvYeuCauChiTietModel.HM_GIA_TRI_CON_LAI;
                    item.HTSD_CHO_THUE = taisanModel.nvYeuCauChiTietModel.HTSD_CHO_THUE;
                    item.HTSD_HDSN_KINH_DOANH = taisanModel.nvYeuCauChiTietModel.HTSD_HDSN_KINH_DOANH;
                    item.HTSD_HDSN_KINH_DOANH_KHONG = taisanModel.nvYeuCauChiTietModel.HTSD_HDSN_KINH_DOANH_KHONG;
                    item.HTSD_LIEN_DOANH = taisanModel.nvYeuCauChiTietModel.HTSD_LIEN_DOANH;
                    item.HTSD_QUAN_LY_NHA_NUOC = taisanModel.nvYeuCauChiTietModel.HTSD_QUAN_LY_NHA_NUOC;
                    item.HTSD_SU_DUNG_HON_HOP = taisanModel.nvYeuCauChiTietModel.HTSD_SU_DUNG_HON_HOP;
                    item.HTSD_SU_DUNG_KHAC = taisanModel.nvYeuCauChiTietModel.HTSD_SU_DUNG_KHAC;
                    item.KH_CON_LAI = taisanModel.nvYeuCauChiTietModel.KH_CON_LAI;
                    //item.KH_GIA_TINH_KHAU_HAO = taisanModel.nvYeuCauChiTietModel.KH_GIA_TINH_KHAU_HAO;
                    item.KH_GIA_TINH_KHAU_HAO = (taisanModel.nvYeuCauChiTietModel.KH_TY_LE_NGUYEN_GIA_KHAU_HAO ?? 0) * taisanModel.nvYeuCauChiTietModel.NGUYEN_GIA;
                    item.KH_GIA_TRI_TRICH_THANG = taisanModel.nvYeuCauChiTietModel.KH_GIA_TRI_TRICH_THANG;
                    item.KH_TY_LE_NGUYEN_GIA_KHAU_HAO = taisanModel.nvYeuCauChiTietModel.KH_TY_LE_NGUYEN_GIA_KHAU_HAO;
                    item.KH_LUY_KE = taisanModel.nvYeuCauChiTietModel.KH_LUY_KE;
                    item.KH_NGAY_BAT_DAU = taisanModel.nvYeuCauChiTietModel.KH_NGAY_BAT_DAU;
                    item.KH_THANG_CON_LAI = taisanModel.nvYeuCauChiTietModel.KH_THANG_CON_LAI;
                    item.THONG_SO_KY_THUAT = taisanModel.taisanmaymocModel.THONG_SO_KY_THUAT;
                    //var HienTrangmmList = new HienTrangList()
                    //{
                    //    TaiSanId = taisanModel.ID,
                    //    lstObjHienTrang = taisanModel.taisanmaymocModel.lstHienTrang
                    //};
                    //item.HTSD_JSON = HienTrangmmList.toStringJson();
                    break;
                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                    item.HINH_THUC_MUA_SAM_ID = taisanModel.nvYeuCauChiTietModel.HINH_THUC_MUA_SAM_ID;
                    item.MUC_DICH_SU_DUNG_ID = taisanModel.nvYeuCauChiTietModel.MUC_DICH_SU_DUNG_ID;
                    item.NGUYEN_GIA = taisanModel.nvYeuCauChiTietModel.NGUYEN_GIA;
                    //---------
                    item.CLN_SO_NAM = taisanModel.NAM_SAN_XUAT;

                    item.HM_SO_NAM_CON_LAI = taisanModel.nvYeuCauChiTietModel.HM_SO_NAM_CON_LAI;
                    item.HM_LUY_KE = taisanModel.nvYeuCauChiTietModel.HM_LUY_KE;
                    item.HM_TY_LE_HAO_MON = taisanModel.nvYeuCauChiTietModel.HM_TY_LE_HAO_MON;
                    item.HM_GIA_TRI_CON_LAI = taisanModel.nvYeuCauChiTietModel.HM_GIA_TRI_CON_LAI;
                    item.HTSD_CHO_THUE = taisanModel.nvYeuCauChiTietModel.HTSD_CHO_THUE;
                    item.HTSD_HDSN_KINH_DOANH = taisanModel.nvYeuCauChiTietModel.HTSD_HDSN_KINH_DOANH;
                    item.HTSD_HDSN_KINH_DOANH_KHONG = taisanModel.nvYeuCauChiTietModel.HTSD_HDSN_KINH_DOANH_KHONG;
                    item.HTSD_LIEN_DOANH = taisanModel.nvYeuCauChiTietModel.HTSD_LIEN_DOANH;
                    item.HTSD_QUAN_LY_NHA_NUOC = taisanModel.nvYeuCauChiTietModel.HTSD_QUAN_LY_NHA_NUOC;
                    item.HTSD_SU_DUNG_HON_HOP = taisanModel.nvYeuCauChiTietModel.HTSD_SU_DUNG_HON_HOP;
                    item.HTSD_SU_DUNG_KHAC = taisanModel.nvYeuCauChiTietModel.HTSD_SU_DUNG_KHAC;
                    item.KH_CON_LAI = taisanModel.nvYeuCauChiTietModel.KH_CON_LAI;
                    //item.KH_GIA_TINH_KHAU_HAO = taisanModel.nvYeuCauChiTietModel.KH_GIA_TINH_KHAU_HAO;
                    item.KH_GIA_TINH_KHAU_HAO = (taisanModel.nvYeuCauChiTietModel.KH_TY_LE_NGUYEN_GIA_KHAU_HAO ?? 0) * taisanModel.nvYeuCauChiTietModel.NGUYEN_GIA;
                    item.KH_GIA_TRI_TRICH_THANG = taisanModel.nvYeuCauChiTietModel.KH_GIA_TRI_TRICH_THANG;
                    item.KH_TY_LE_NGUYEN_GIA_KHAU_HAO = taisanModel.nvYeuCauChiTietModel.KH_TY_LE_NGUYEN_GIA_KHAU_HAO;
                    item.KH_LUY_KE = taisanModel.nvYeuCauChiTietModel.KH_LUY_KE;
                    item.KH_NGAY_BAT_DAU = taisanModel.nvYeuCauChiTietModel.KH_NGAY_BAT_DAU;
                    item.KH_THANG_CON_LAI = taisanModel.nvYeuCauChiTietModel.KH_THANG_CON_LAI;
                    item.THONG_SO_KY_THUAT = taisanModel.nvYeuCauChiTietModel.THONG_SO_KY_THUAT;
                    //var HienTrangVVktList = new HienTrangList()
                    //{
                    //    TaiSanId = taisanModel.ID,
                    //    lstObjHienTrang = taisanModel.taisanClnModel.lstHienTrang
                    //};
                    break;
                case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                    item.HINH_THUC_MUA_SAM_ID = taisanModel.nvYeuCauChiTietModel.HINH_THUC_MUA_SAM_ID;
                    item.MUC_DICH_SU_DUNG_ID = taisanModel.nvYeuCauChiTietModel.MUC_DICH_SU_DUNG_ID;
                    item.NGUYEN_GIA = taisanModel.nvYeuCauChiTietModel.NGUYEN_GIA;
                    item.HM_SO_NAM_CON_LAI = taisanModel.nvYeuCauChiTietModel.HM_SO_NAM_CON_LAI;
                    item.HM_LUY_KE = taisanModel.nvYeuCauChiTietModel.HM_LUY_KE;
                    item.HM_TY_LE_HAO_MON = taisanModel.nvYeuCauChiTietModel.HM_TY_LE_HAO_MON;
                    item.HM_GIA_TRI_CON_LAI = taisanModel.nvYeuCauChiTietModel.HM_GIA_TRI_CON_LAI;
                    item.HTSD_CHO_THUE = taisanModel.nvYeuCauChiTietModel.HTSD_CHO_THUE;
                    item.HTSD_HDSN_KINH_DOANH = taisanModel.nvYeuCauChiTietModel.HTSD_HDSN_KINH_DOANH;
                    item.HTSD_HDSN_KINH_DOANH_KHONG = taisanModel.nvYeuCauChiTietModel.HTSD_HDSN_KINH_DOANH_KHONG;
                    item.HTSD_LIEN_DOANH = taisanModel.nvYeuCauChiTietModel.HTSD_LIEN_DOANH;
                    item.HTSD_QUAN_LY_NHA_NUOC = taisanModel.nvYeuCauChiTietModel.HTSD_QUAN_LY_NHA_NUOC;
                    item.HTSD_SU_DUNG_HON_HOP = taisanModel.nvYeuCauChiTietModel.HTSD_SU_DUNG_HON_HOP;
                    item.HTSD_SU_DUNG_KHAC = taisanModel.nvYeuCauChiTietModel.HTSD_SU_DUNG_KHAC;
                    item.KH_CON_LAI = taisanModel.nvYeuCauChiTietModel.KH_CON_LAI;
                    //item.KH_GIA_TINH_KHAU_HAO = taisanModel.nvYeuCauChiTietModel.KH_GIA_TINH_KHAU_HAO;
                    item.KH_GIA_TINH_KHAU_HAO = (taisanModel.nvYeuCauChiTietModel.KH_TY_LE_NGUYEN_GIA_KHAU_HAO ?? 0) * taisanModel.nvYeuCauChiTietModel.NGUYEN_GIA;
                    item.KH_GIA_TRI_TRICH_THANG = taisanModel.nvYeuCauChiTietModel.KH_GIA_TRI_TRICH_THANG;
                    item.KH_TY_LE_NGUYEN_GIA_KHAU_HAO = taisanModel.nvYeuCauChiTietModel.KH_TY_LE_NGUYEN_GIA_KHAU_HAO;
                    item.KH_LUY_KE = taisanModel.nvYeuCauChiTietModel.KH_LUY_KE;
                    item.KH_NGAY_BAT_DAU = taisanModel.nvYeuCauChiTietModel.KH_NGAY_BAT_DAU;
                    item.KH_THANG_CON_LAI = taisanModel.nvYeuCauChiTietModel.KH_THANG_CON_LAI;
                    item.THONG_SO_KY_THUAT = taisanModel.nvYeuCauChiTietModel.THONG_SO_KY_THUAT;
                    //var HienTrangmmList = new HienTrangList()
                    //{
                    //    TaiSanId = taisanModel.ID,
                    //    lstObjHienTrang = taisanModel.taisanmaymocModel.lstHienTrang
                    //};
                    //item.HTSD_JSON = HienTrangmmList.toStringJson();
                    break;
                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                    item.HINH_THUC_MUA_SAM_ID = taisanModel.nvYeuCauChiTietModel.HINH_THUC_MUA_SAM_ID;
                    item.MUC_DICH_SU_DUNG_ID = taisanModel.nvYeuCauChiTietModel.MUC_DICH_SU_DUNG_ID;
                    item.NGUYEN_GIA = taisanModel.nvYeuCauChiTietModel.NGUYEN_GIA;
                    //---------
                    item.VKT_DIEN_TICH = taisanModel.taisanVktModel.VKT_DIEN_TICH != null ? (decimal)taisanModel.taisanVktModel.VKT_DIEN_TICH : 0;
                    item.VKT_THE_TICH = taisanModel.taisanVktModel.THE_TICH;
                    item.VKT_CHIEU_DAI = taisanModel.taisanVktModel.CHIEU_DAI;
                    //---------
                    item.HM_SO_NAM_CON_LAI = taisanModel.nvYeuCauChiTietModel.HM_SO_NAM_CON_LAI;
                    item.HM_LUY_KE = taisanModel.nvYeuCauChiTietModel.HM_LUY_KE;
                    item.HM_TY_LE_HAO_MON = taisanModel.nvYeuCauChiTietModel.HM_TY_LE_HAO_MON;
                    item.HM_GIA_TRI_CON_LAI = taisanModel.nvYeuCauChiTietModel.HM_GIA_TRI_CON_LAI;
                    item.HTSD_CHO_THUE = taisanModel.nvYeuCauChiTietModel.HTSD_CHO_THUE;
                    item.HTSD_HDSN_KINH_DOANH = taisanModel.nvYeuCauChiTietModel.HTSD_HDSN_KINH_DOANH;
                    item.HTSD_HDSN_KINH_DOANH_KHONG = taisanModel.nvYeuCauChiTietModel.HTSD_HDSN_KINH_DOANH_KHONG;
                    item.HTSD_LIEN_DOANH = taisanModel.nvYeuCauChiTietModel.HTSD_LIEN_DOANH;
                    item.HTSD_QUAN_LY_NHA_NUOC = taisanModel.nvYeuCauChiTietModel.HTSD_QUAN_LY_NHA_NUOC;
                    item.HTSD_SU_DUNG_HON_HOP = taisanModel.nvYeuCauChiTietModel.HTSD_SU_DUNG_HON_HOP;
                    item.HTSD_SU_DUNG_KHAC = taisanModel.nvYeuCauChiTietModel.HTSD_SU_DUNG_KHAC;
                    item.KH_CON_LAI = taisanModel.nvYeuCauChiTietModel.KH_CON_LAI;
                    //item.KH_GIA_TINH_KHAU_HAO = taisanModel.nvYeuCauChiTietModel.KH_GIA_TINH_KHAU_HAO;
                    item.KH_GIA_TINH_KHAU_HAO = (taisanModel.nvYeuCauChiTietModel.KH_TY_LE_NGUYEN_GIA_KHAU_HAO ?? 0) * taisanModel.nvYeuCauChiTietModel.NGUYEN_GIA;
                    item.KH_GIA_TRI_TRICH_THANG = taisanModel.nvYeuCauChiTietModel.KH_GIA_TRI_TRICH_THANG;
                    item.KH_TY_LE_NGUYEN_GIA_KHAU_HAO = taisanModel.nvYeuCauChiTietModel.KH_TY_LE_NGUYEN_GIA_KHAU_HAO;
                    item.KH_LUY_KE = taisanModel.nvYeuCauChiTietModel.KH_LUY_KE;
                    item.KH_NGAY_BAT_DAU = taisanModel.nvYeuCauChiTietModel.KH_NGAY_BAT_DAU;
                    item.KH_THANG_CON_LAI = taisanModel.nvYeuCauChiTietModel.KH_THANG_CON_LAI;
                    item.THONG_SO_KY_THUAT = taisanModel.nvYeuCauChiTietModel.THONG_SO_KY_THUAT;
                    //var HienTrangVVktList = new HienTrangList()
                    //{
                    //    TaiSanId = taisanModel.ID,
                    //    lstObjHienTrang = taisanModel.taisanVktModel.lstHienTrang
                    //};
                    break;
            }
            //var arrMuaSamMuaMoi = (new enumLY_DO_GIAM_TOAN_BO()).ToSelectList().Select(c => c.Value.ToNumberInt32()).ToList();

            //         var arrMuaSamMuaMoi = _lyDoBienDongService.GetLyDoBienDongByLoaiLyDoBienDong(enum_LOAI_LY_DO_BIEN_DONG.LY_DO_GIAM).Select(p=>p.ID as decimal?);
            //if (arrMuaSamMuaMoi.Contains(taisanModel.LY_DO_BIEN_DONG_ID))
            //         {
            //             item.HINH_THUC_MUA_SAM_ID = taisanModel.nvYeuCauChiTietModel.HINH_THUC_MUA_SAM_ID;
            //         }
            //         else
            //         {
            //             item.HINH_THUC_MUA_SAM_ID = null;
            //         }
            return item;
        }

        public YeuCauChiTiet PrepareYCCTFromBDCT(BienDongChiTiet _bdct, YeuCauChiTiet _ycct, bool isCopy = false)
        {
            _ycct.HINH_THUC_MUA_SAM_ID = _bdct.HINH_THUC_MUA_SAM_ID;
            _ycct.MUC_DICH_SU_DUNG_ID = _bdct.MUC_DICH_SU_DUNG_ID;
            _ycct.NHAN_HIEU = _bdct.NHAN_HIEU;
            _ycct.SO_HIEU = _bdct.SO_HIEU;
            _ycct.DIA_BAN_ID = _bdct.DIA_BAN_ID;
            _ycct.DATA_JSON = _bdct.DATA_JSON;
            _ycct.NGUYEN_GIA = _bdct.NGUYEN_GIA;
            _ycct.DAT_TONG_DIEN_TICH = _bdct.DAT_TONG_DIEN_TICH;
            _ycct.HTSD_QUAN_LY_NHA_NUOC = _bdct.HTSD_QUAN_LY_NHA_NUOC;
            _ycct.HTSD_HDSN_KINH_DOANH_KHONG = _bdct.HTSD_HDSN_KINH_DOANH_KHONG;
            _ycct.HTSD_HDSN_KINH_DOANH = _bdct.HTSD_HDSN_KINH_DOANH;
            _ycct.HTSD_CHO_THUE = _bdct.HTSD_CHO_THUE;
            _ycct.HTSD_LIEN_DOANH = _bdct.HTSD_LIEN_DOANH;
            _ycct.HTSD_SU_DUNG_HON_HOP = _bdct.HTSD_SU_DUNG_HON_HOP;
            _ycct.HTSD_SU_DUNG_KHAC = _bdct.HTSD_SU_DUNG_KHAC;
            _ycct.HM_SO_NAM_CON_LAI = _bdct.HM_SO_NAM_CON_LAI;
            _ycct.HM_TY_LE_HAO_MON = _bdct.HM_TY_LE_HAO_MON;
            _ycct.HM_LUY_KE = _bdct.HM_LUY_KE;
            _ycct.HM_GIA_TRI_CON_LAI = _bdct.HM_GIA_TRI_CON_LAI;
            _ycct.KH_NGAY_BAT_DAU = _bdct.KH_NGAY_BAT_DAU;
            _ycct.KH_THANG_CON_LAI = _bdct.KH_THANG_CON_LAI;
            _ycct.KH_GIA_TINH_KHAU_HAO = _bdct.KH_GIA_TINH_KHAU_HAO;
            _ycct.KH_GIA_TRI_TRICH_THANG = _bdct.KH_GIA_TRI_TRICH_THANG;
            _ycct.KH_LUY_KE = _bdct.KH_LUY_KE;
            _ycct.KH_CON_LAI = _bdct.KH_CON_LAI;
            _ycct.NHA_SO_TANG = _bdct.NHA_SO_TANG;
            _ycct.NHA_NAM_XAY_DUNG = _bdct.NHA_NAM_XAY_DUNG;
            _ycct.NHA_DIEN_TICH_XD = _bdct.NHA_DIEN_TICH_XD;
            _ycct.NHA_TONG_DIEN_TICH_XD = _bdct.NHA_TONG_DIEN_TICH_XD;
            _ycct.VKT_DIEN_TICH = _bdct.VKT_DIEN_TICH;
            _ycct.VKT_THE_TICH = _bdct.VKT_THE_TICH;
            _ycct.VKT_CHIEU_DAI = _bdct.VKT_CHIEU_DAI;
            _ycct.OTO_BIEN_KIEM_SOAT = _bdct.OTO_BIEN_KIEM_SOAT;
            _ycct.OTO_SO_CHO_NGOI = _bdct.OTO_SO_CHO_NGOI;
            _ycct.OTO_SO_CAU_XE = _bdct.OTO_SO_CAU_XE;
            _ycct.OTO_CHUC_DANH_ID = _bdct.OTO_CHUC_DANH_ID;
            _ycct.OTO_LIST_CHUC_DANH_ID = _bdct.OTO_LIST_CHUC_DANH_ID;
            _ycct.OTO_NHAN_XE_ID = _bdct.OTO_NHAN_XE_ID;
            _ycct.OTO_TAI_TRONG = _bdct.OTO_TAI_TRONG;
            _ycct.OTO_CONG_XUAT = _bdct.OTO_CONG_XUAT;
            _ycct.OTO_XI_LANH = _bdct.OTO_XI_LANH;
            _ycct.OTO_SO_KHUNG = _bdct.OTO_SO_KHUNG;
            _ycct.OTO_SO_MAY = _bdct.OTO_SO_MAY;
            _ycct.THONG_SO_KY_THUAT = _bdct.THONG_SO_KY_THUAT;
            _ycct.CLN_SO_NAM = _bdct.CLN_SO_NAM;
            if (_bdct.HTSD_JSON != null)
            {
                _ycct.HTSD_JSON = _bdct.HTSD_JSON;
            }
            else
            {
                //code here lấy dữ liệu hiện trạng từ ts_tai_san_hien_trang
                var lstHienTrang = _taiSanHienTrangSuDungService.GetTaiSanHienTrangSuDungByBienDongId(BienDongId: _bdct.BIEN_DONG_ID);
                var lstObjHienTrang = new List<ObjHienTrang>();
                foreach (var ht in lstHienTrang)
                {
                    var obj = new ObjHienTrang();
                    obj.HienTrangId = ht.ID;
                    obj.TenHienTrang = ht.TEN_HIEN_TRANG;
                    obj.KieuDuLieuId = ht.KIEU_DU_LIEU_ID;
                    obj.NhomHienTrangId = ht.NHOM_HIEN_TRANG_ID;
                    obj.GiaTriCheckbox = ht.GIA_TRI_CHECKBOX ?? false;
                    lstObjHienTrang.Add(obj);
                }
                var hientrangList = new HienTrangList()
                {
                    TaiSanId = _bdct.ID,
                    lstObjHienTrang = lstObjHienTrang
                };
                _ycct.HTSD_JSON = hientrangList.toStringJson();
            }
            _ycct.PHI_THU = _bdct.PHI_THU;
            _ycct.PHI_BU_DAP = _bdct.PHI_BU_DAP;
            _ycct.PHI_NOP_NGAN_SACH = _bdct.PHI_NOP_NGAN_SACH;
            _ycct.PHI_KHAC = _bdct.PHI_KHAC;
            _ycct.DON_VI_NHAN_DIEU_CHUYEN_ID = _bdct.DON_VI_NHAN_DIEU_CHUYEN_ID;
            _ycct.HINH_THUC_XU_LY_ID = _bdct.HINH_THUC_XU_LY_ID;
            _ycct.IS_BAN_THANH_LY = _bdct.IS_BAN_THANH_LY;
            _ycct.HS_CNQSD_SO = _bdct.HS_CNQSD_SO;
            _ycct.HS_CNQSD_NGAY = _bdct.HS_CNQSD_NGAY;
            _ycct.HS_QUYET_DINH_GIAO_SO = _bdct.HS_QUYET_DINH_GIAO_SO;
            _ycct.HS_QUYET_DINH_GIAO_NGAY = _bdct.HS_QUYET_DINH_GIAO_NGAY;
            _ycct.HS_CHUYEN_NHUONG_SD_SO = _bdct.HS_CHUYEN_NHUONG_SD_SO;
            _ycct.HS_CHUYEN_NHUONG_SD_NGAY = _bdct.HS_CHUYEN_NHUONG_SD_NGAY;
            _ycct.HS_QUYET_DINH_CHO_THUE_SO = _bdct.HS_QUYET_DINH_CHO_THUE_SO;
            _ycct.HS_QUYET_DINH_CHO_THUE_NGAY = _bdct.HS_QUYET_DINH_CHO_THUE_NGAY;
            _ycct.HS_KHAC = _bdct.HS_KHAC;
            _ycct.HS_QUYET_DINH_BAN_GIAO = _bdct.HS_QUYET_DINH_BAN_GIAO;
            _ycct.HS_QUYET_DINH_BAN_GIAO_NGAY = _bdct.HS_QUYET_DINH_BAN_GIAO_NGAY;
            _ycct.HS_BIEN_BAN_NGHIEM_THU = _bdct.HS_BIEN_BAN_NGHIEM_THU;
            _ycct.HS_BIEN_BAN_NGHIEM_THU_NGAY = _bdct.HS_BIEN_BAN_NGHIEM_THU_NGAY;
            _ycct.HS_PHAP_LY_KHAC = _bdct.HS_PHAP_LY_KHAC;
            _ycct.HS_PHAP_LY_KHAC_NGAY = _bdct.HS_PHAP_LY_KHAC_NGAY;
            _ycct.DIA_CHI = _bdct.DIA_CHI;
            return _ycct;
        }
        public YeuCauModel PrepareYeuCauChiTietTruocBD(YeuCauModel model)
        {
            var kq = _bienDongService.Tinh_GiaTriTaiSan(model.TAI_SAN_ID, model.NGAY_BIEN_DONG);
            model.YCCTModel.NHA_TONG_DIEN_TICH_XD_CU = kq.NHA_TONG_DIEN_TICH_XD_CU;
            model.YCCTModel.NHA_DIEN_TICH_XD_CU = kq.NHA_DIEN_TICH_XD_CU;
            model.YCCTModel.NGUYEN_GIA_CU = kq.NGUYEN_GIA_CU;
            model.YCCTModel.DAT_TONG_DIEN_TICH_CU = kq.DAT_TONG_DIEN_TICH_CU;
            model.YCCTModel.VKT_CHIEU_DAI_CU = kq.VKT_CHIEU_DAI_CU;
            model.YCCTModel.VKT_DIEN_TICH_CU = kq.VKT_DIEN_TICH_CU;
            model.YCCTModel.VKT_THE_TICH_CU = kq.VKT_THE_TICH_CU;
            List<decimal> l_List_Except = new List<decimal>
                {
                    (decimal)enumTRANG_THAI_YEU_CAU.NHAP,
                    (decimal)enumTRANG_THAI_YEU_CAU.NHAP_LIEU,
                    (decimal)enumTRANG_THAI_YEU_CAU.TU_CHOI,
                    (decimal)enumTRANG_THAI_YEU_CAU.XOA,
                };
            var YCTruoc = _yeucauService.GetYeuCauMoiNhatByTaiSanId(model.TAI_SAN_ID, enumTRANG_THAI_YEU_CAU.TAT_CA, l_List_Except, model.NGAY_BIEN_DONG);
            if (YCTruoc != null)
            {

                var YCCTTruoc = _itemService.GetYeuCauChiTietByYeuCauId(YCTruoc.ID);
                //model.YCCTModel = YCCTTruoc.ToModel<YeuCauChiTietModel>();
                model.YCCTModel.HM_GIA_TRI_CON_LAI_CU = CommonCalculate.GiaTriConLaiCu(YCCTTruoc.HM_GIA_TRI_CON_LAI ?? 0, model.NGAY_BIEN_DONG ?? DateTime.Now, YCTruoc.NGAY_BIEN_DONG ?? DateTime.Now, YCTruoc.NGUYEN_GIA ?? 0, YCCTTruoc.HM_TY_LE_HAO_MON ?? 0);
                if ((model.YCCTModel.lstHienTrang == null || model.YCCTModel.lstHienTrang.Count == 0) && !String.IsNullOrEmpty(YCCTTruoc.HTSD_JSON))
                {
                    model.YCCTModel.lstHienTrang = YCCTTruoc.HTSD_JSON.toEntity<HienTrangList>().lstObjHienTrang;
                    // Nếu nhập sai hiện trạng thì mở tất ra cho sửa
                    if (_hienTrangModelFactory.IsAnyHienTrangSai(model.YCCTModel.lstHienTrang, _workContext.CurrentDonVi.ID))
                    {
                        model.YCCTModel.lstHienTrang = model.YCCTModel.lstHienTrang.Select(c =>
                        {
                            c.IsOpenAll = true;
                            return c;
                        }).ToList();
                    }
                    foreach (var itemHT in model.YCCTModel.lstHienTrang)
                    {
                        var hientrang = _hienTrangService.GetHienTrangById(itemHT.HienTrangId.Value);
                        if (hientrang != null)
                        {
                            itemHT.TenHienTrang = hientrang.TEN_HIEN_TRANG;
                        }
                    }
                }
                model.YCCTModel.HINH_THUC_MUA_SAM_ID = YCCTTruoc.HINH_THUC_MUA_SAM_ID;
                model.YCCTModel.MUC_DICH_SU_DUNG_ID = YCCTTruoc.MUC_DICH_SU_DUNG_ID;
                model.YCCTModel.HINH_THUC_MUA_SAM_ID = YCCTTruoc.HINH_THUC_MUA_SAM_ID;
                model.YCCTModel.MUC_DICH_SU_DUNG_ID = YCCTTruoc.MUC_DICH_SU_DUNG_ID;
                //hồ sơ đất
                model.YCCTModel.HS_CNQSD_SO = YCCTTruoc.HS_CNQSD_SO;
                model.YCCTModel.HS_CNQSD_NGAY = YCCTTruoc.HS_CNQSD_NGAY;
                model.YCCTModel.HS_CHUYEN_NHUONG_SD_SO = YCCTTruoc.HS_CHUYEN_NHUONG_SD_SO;
                model.YCCTModel.HS_CHUYEN_NHUONG_SD_NGAY = YCCTTruoc.HS_CHUYEN_NHUONG_SD_NGAY;
                model.YCCTModel.HS_QUYET_DINH_CHO_THUE_SO = YCCTTruoc.HS_QUYET_DINH_CHO_THUE_SO;
                model.YCCTModel.HS_QUYET_DINH_CHO_THUE_NGAY = YCCTTruoc.HS_QUYET_DINH_CHO_THUE_NGAY;
                model.YCCTModel.HS_QUYET_DINH_GIAO_SO = YCCTTruoc.HS_QUYET_DINH_GIAO_SO;
                model.YCCTModel.HS_QUYET_DINH_GIAO_NGAY = YCCTTruoc.HS_QUYET_DINH_GIAO_NGAY;
                //hồ sơ nhà
                model.YCCTModel.HS_QUYET_DINH_BAN_GIAO = YCCTTruoc.HS_QUYET_DINH_BAN_GIAO;
                model.YCCTModel.HS_QUYET_DINH_BAN_GIAO_NGAY = YCCTTruoc.HS_QUYET_DINH_BAN_GIAO_NGAY;
                model.YCCTModel.HS_BIEN_BAN_NGHIEM_THU = YCCTTruoc.HS_BIEN_BAN_NGHIEM_THU;
                model.YCCTModel.HS_BIEN_BAN_NGHIEM_THU_NGAY = YCCTTruoc.HS_BIEN_BAN_NGHIEM_THU_NGAY;
                model.YCCTModel.HS_PHAP_LY_KHAC = YCCTTruoc.HS_PHAP_LY_KHAC;
                model.YCCTModel.HS_PHAP_LY_KHAC_NGAY = YCCTTruoc.HS_PHAP_LY_KHAC_NGAY;

            }
            else
            {
                var bienDongTruoc = _bienDongService.GetBienDongMoiNhatByTaiSanId(taiSanId: model.TAI_SAN_ID, ngayBienDong: model.NGAY_BIEN_DONG);
                var BDChiTietTruoc = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDongTruoc.ID);
                model.YCCTModel.HM_GIA_TRI_CON_LAI_CU = CommonCalculate.GiaTriConLaiCu(BDChiTietTruoc.HM_GIA_TRI_CON_LAI ?? 0, model.NGAY_BIEN_DONG ?? DateTime.Now, bienDongTruoc.NGAY_BIEN_DONG ?? DateTime.Now, bienDongTruoc.NGUYEN_GIA ?? 0, BDChiTietTruoc.HM_TY_LE_HAO_MON ?? 0);
                if ((model.YCCTModel.lstHienTrang == null || model.YCCTModel.lstHienTrang.Count == 0) && !String.IsNullOrEmpty(BDChiTietTruoc.HTSD_JSON))
                {
                    model.YCCTModel.lstHienTrang = BDChiTietTruoc.HTSD_JSON.toEntity<HienTrangList>().lstObjHienTrang;
                    // Nếu nhập sai hiện trạng thì mở tất ra cho sửa
                    if (_hienTrangModelFactory.IsAnyHienTrangSai(model.YCCTModel.lstHienTrang, _workContext.CurrentDonVi.ID))
                    {
                        model.YCCTModel.lstHienTrang = model.YCCTModel.lstHienTrang.Select(c =>
                        {
                            c.IsOpenAll = true;
                            return c;
                        }).ToList();
                    }
                    foreach (var itemHT in model.YCCTModel.lstHienTrang)
                    {
                        var hientrang = _hienTrangService.GetHienTrangById(itemHT.HienTrangId.Value);
                        if (hientrang != null)
                        {
                            itemHT.TenHienTrang = hientrang.TEN_HIEN_TRANG;
                        }
                    }
                }
                model.YCCTModel.HINH_THUC_MUA_SAM_ID = BDChiTietTruoc.HINH_THUC_MUA_SAM_ID;
                model.YCCTModel.MUC_DICH_SU_DUNG_ID = BDChiTietTruoc.MUC_DICH_SU_DUNG_ID;
                model.YCCTModel.HS_CNQSD_SO = BDChiTietTruoc.HS_CNQSD_SO;
                model.YCCTModel.HS_CNQSD_NGAY = BDChiTietTruoc.HS_CNQSD_NGAY;
                //Hồ sơ đất
                model.YCCTModel.HS_CHUYEN_NHUONG_SD_SO = BDChiTietTruoc.HS_CHUYEN_NHUONG_SD_SO;
                model.YCCTModel.HS_CHUYEN_NHUONG_SD_NGAY = BDChiTietTruoc.HS_CHUYEN_NHUONG_SD_NGAY;
                model.YCCTModel.HS_QUYET_DINH_CHO_THUE_SO = BDChiTietTruoc.HS_QUYET_DINH_CHO_THUE_SO;
                model.YCCTModel.HS_QUYET_DINH_CHO_THUE_NGAY = BDChiTietTruoc.HS_QUYET_DINH_CHO_THUE_NGAY;
                model.YCCTModel.HS_QUYET_DINH_GIAO_SO = BDChiTietTruoc.HS_QUYET_DINH_GIAO_SO;
                model.YCCTModel.HS_QUYET_DINH_GIAO_NGAY = BDChiTietTruoc.HS_QUYET_DINH_GIAO_NGAY;
                //Hồ sơ nhà
                model.YCCTModel.HS_QUYET_DINH_BAN_GIAO = BDChiTietTruoc.HS_QUYET_DINH_BAN_GIAO;
                model.YCCTModel.HS_QUYET_DINH_BAN_GIAO_NGAY = BDChiTietTruoc.HS_QUYET_DINH_BAN_GIAO_NGAY;
                model.YCCTModel.HS_BIEN_BAN_NGHIEM_THU = BDChiTietTruoc.HS_BIEN_BAN_NGHIEM_THU;
                model.YCCTModel.HS_BIEN_BAN_NGHIEM_THU_NGAY = BDChiTietTruoc.HS_BIEN_BAN_NGHIEM_THU_NGAY;
                model.YCCTModel.HS_PHAP_LY_KHAC = BDChiTietTruoc.HS_PHAP_LY_KHAC;
                model.YCCTModel.HS_PHAP_LY_KHAC_NGAY = BDChiTietTruoc.HS_PHAP_LY_KHAC_NGAY;
            }


            return model;
        }
        public YeuCauChiTietModel PrepareYeuCauChiTietTruocBDByYCTT(YeuCauChiTiet YCTTtruoc, YeuCauChiTietModel model)
        {
            model.NHA_TONG_DIEN_TICH_XD_CU = YCTTtruoc.NHA_TONG_DIEN_TICH_XD;
            model.NHA_DIEN_TICH_XD_CU = YCTTtruoc.NHA_DIEN_TICH_XD;
            model.NGUYEN_GIA_CU = YCTTtruoc.NGUYEN_GIA;
            model.HM_GIA_TRI_CON_LAI_CU = YCTTtruoc.HM_GIA_TRI_CON_LAI;
            model.DAT_TONG_DIEN_TICH_CU = YCTTtruoc.DAT_TONG_DIEN_TICH;
            return model;
        }

        /// <summary>
        /// Description: Lấy thông tin của loại tài sản
        /// </summary>
        /// <param name="loaiTaiSan"></param>
        /// <param name="nvYeuCauChiTiet"></param>
        /// <returns></returns>
        public YeuCauChiTietModel PrepareValueFromLoaiTStoYeuCauCT(LoaiTaiSanModel loaiTaiSan, YeuCauChiTietModel nvYeuCauChiTiet)
        {
            if (loaiTaiSan != null)
            {
                nvYeuCauChiTiet.HM_TY_LE_HAO_MON = loaiTaiSan.HM_TY_LE;
                if (loaiTaiSan.KH_THOI_HAN_SU_DUNG != null)
                {
                    nvYeuCauChiTiet.KH_THANG_THEO_QD = loaiTaiSan.KH_THOI_HAN_SU_DUNG.Value;
                }
            }

            return nvYeuCauChiTiet;
        }

        public YeuCauChiTiet PrepareYCCTForBDEdit(YeuCauChiTietModel model, YeuCauChiTiet item)
        {
            //switch (model.LOAI_TAI_SAN_ID)
            //{
            //    case (int)enumLOAI_HINH_TAI_SAN.NHA:

            //        break;
            //    case (int)enumLOAI_HINH_TAI_SAN.DAT:

            //        break;

            //    default:
            //        break;
            //}
            item.NHA_TONG_DIEN_TICH_XD = model.NHA_TONG_DIEN_TICH_XD;
            item.NHA_DIEN_TICH_XD = model.NHA_DIEN_TICH_XD;
            item.DAT_TONG_DIEN_TICH = model.DAT_TONG_DIEN_TICH;
            item.NGUYEN_GIA = model.NGUYEN_GIA;
            item.HM_GIA_TRI_CON_LAI = model.HM_GIA_TRI_CON_LAI;
            item.DIA_BAN_ID = model.DIA_BAN_ID;
            item.DIA_CHI = model.DIA_CHI;
            item.NHA_SO_TANG = model.NHA_SO_TANG;
            item.OTO_BIEN_KIEM_SOAT = model.OTO_BIEN_KIEM_SOAT;
            item.OTO_LIST_CHUC_DANH_ID = model.OTO_LIST_CHUC_DANH_ID;
            item.OTO_CONG_XUAT = model.OTO_CONG_XUAT;
            item.OTO_NHAN_XE_ID = model.OTO_NHAN_XE_ID;
            item.OTO_SO_CHO_NGOI = model.OTO_SO_CHO_NGOI;
            item.OTO_SO_CAU_XE = model.OTO_SO_CAU_XE;
            item.OTO_TAI_TRONG = model.OTO_TAI_TRONG;
            item.VKT_CHIEU_DAI = model.VKT_CHIEU_DAI;
            item.VKT_DIEN_TICH = model.VKT_DIEN_TICH;
            item.VKT_THE_TICH = model.VKT_THE_TICH;
            return item;
        }
        #endregion


        #region giảm tài sản
        public YeuCauChiTiet PrepareYeuCauChiTietGiamTaiSan(YeuCauChiTietModel model, YeuCauChiTiet item, YeuCau yeucau)
        {
            item.YEU_CAU_ID = yeucau.ID;
            item.DON_VI_NHAN_DIEU_CHUYEN_ID = model.DON_VI_NHAN_DIEU_CHUYEN_ID;
            item.PHI_THU = model.PHI_THU;
            item.PHI_KHAC = model.PHI_KHAC;
            item.PHI_BU_DAP = model.PHI_BU_DAP;
            item.PHI_NOP_NGAN_SACH = model.PHI_NOP_NGAN_SACH;
            item.IS_BAN_THANH_LY = model.IS_BAN_THANH_LY;
            item.DIEU_CHUYEN_KEM_THEO = model.DIEU_CHUYEN_KEM_THEO;
            item.HINH_THUC_XU_LY_ID = model.HINH_THUC_XU_LY_ID;
            item.DATA_JSON = yeucau.ToModel<YeuCauModel>().toStringJson();
            return item;
        }
        #endregion

        #region Func
        public virtual decimal CalculateGTCLDieuChuyenMotPhan(decimal TaiSanId, DateTime? NgayBienDong, decimal OLD_GTCL, decimal NguyenGiaDieuChuyen)
        {
            decimal giaTriConLai = 0;
            if (NguyenGiaDieuChuyen > 0)
            {
                //tính tỉ lệ nguyên giá điều chuyển sang đơn vị mới
                var nguyenGiaTS = _bienDongService.TinhNguyenGiaTaiSan(taiSanId: TaiSanId, To_Date_BienDong: NgayBienDong ?? DateTime.Now, isEqualDate: false);
                decimal tyLeDieuChuyen = NguyenGiaDieuChuyen / nguyenGiaTS;
                giaTriConLai = OLD_GTCL - (OLD_GTCL * tyLeDieuChuyen);
            }
            else
                giaTriConLai = OLD_GTCL;

            return giaTriConLai;
        }
        #endregion
    }
}

