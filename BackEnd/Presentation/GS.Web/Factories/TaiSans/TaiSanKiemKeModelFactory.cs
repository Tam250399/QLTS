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
using GS.Web.Factories.DanhMuc;
using GS.Services.DanhMuc;
using GS.Services.NghiepVu;
using GS.Core.Domain.BienDongs;
using GS.Services.BienDongs;
using GS.Services.KT;

namespace GS.Web.Factories.TaiSans
{
    public class TaiSanKiemKeModelFactory:ITaiSanKiemKeModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly ITaiSanKiemKeService _itemService;
        private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
        private readonly IDonViService _donViService;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly IYeuCauService _yeuCauService;
        private readonly IBienDongService _bienDongService;
        private readonly IBienDongChiTietService _bienDongChiTietService;
        private readonly IHaoMonTaiSanService _haoMonTaiSanService;
        private readonly IKhauHaoTaiSanService _khauHaoTaiSanService;
        #endregion

        #region Ctor

        public TaiSanKiemKeModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            ITaiSanKiemKeService itemService,
            IDonViBoPhanModelFactory donViBoPhanModelFactory,
            IDonViService donViService,
            IDonViBoPhanService donViBoPhanService,
            IYeuCauService yeuCauService,
            IBienDongService bienDongService,
            IBienDongChiTietService bienDongChiTietService,
            IHaoMonTaiSanService haoMonTaiSanService,
            IKhauHaoTaiSanService khauHaoTaiSanService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
            this._donViBoPhanModelFactory = donViBoPhanModelFactory;
            this._donViService = donViService;
            this._donViBoPhanService = donViBoPhanService;
            this._yeuCauService = yeuCauService;
            this._bienDongService = bienDongService;
            this._bienDongChiTietService = bienDongChiTietService;
            this._haoMonTaiSanService = haoMonTaiSanService;
            this._khauHaoTaiSanService = khauHaoTaiSanService;
        }
        #endregion
        
        #region TaiSanKiemKe
        public TaiSanKiemKeSearchModel PrepareTaiSanKiemKeSearchModel(TaiSanKiemKeSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.NgayKiemKeDen = DateTime.Now;
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public TaiSanKiemKeListModel PrepareTaiSanKiemKeListModel(TaiSanKiemKeSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchTaiSanKiemKes(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize,DonViId:searchModel.DonViId,NgayKiemKeDen:searchModel.NgayKiemKeDen,NgayKiemKeTu:searchModel.NgayKiemKeTu);
            
            //prepare list model
            var model = new TaiSanKiemKeListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareKiemKeTaiSanForList(new TaiSanKiemKeModel(), c)),
                Total = items.TotalCount
            };
            return model;
        }
        public TaiSanKiemKeModel PrepareKiemKeTaiSanForList(TaiSanKiemKeModel model, TaiSanKiemKe item)
        {
            model = item.ToModel<TaiSanKiemKeModel>();
            model.NgayKiemKeText = item.NGAY_KIEM_KE.toDateVNString();
            model.TenDonVi = _donViService.GetDonViById(item.DON_VI_ID).TEN;
            model.DonViBoPhanText = item.DON_VI_BO_PHAN_ID > 0 ? _donViBoPhanService.GetDonViBoPhanById((Decimal)item.DON_VI_BO_PHAN_ID).TEN : "";

            return model;
        }
        public TaiSanKiemKeModel PrepareTaiSanKiemKeModel(TaiSanKiemKeModel model, TaiSanKiemKe item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<TaiSanKiemKeModel>();
               
            }
            //more
            var donvi = _donViService.GetDonViById(model.DON_VI_ID>0?model.DON_VI_ID:_workContext.CurrentDonVi.ID);
            if (donvi != null)
            {
                model.TenDonVi = donvi.TEN;
                model.MaDonVi = donvi.MA;
            }
            model.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(valSelected:model.DON_VI_BO_PHAN_ID,DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true).ToList();

            return model;
        }

        public void PrepareKiemKeTaiSan(TaiSanKiemKeTaiSan item, TaiSan taisan, Decimal KiemKeId)
        {
            var yeucau = _yeuCauService.GetYeuCauMoiNhatByTaiSanId(taisan.ID);
            item.KIEM_KE_ID = KiemKeId;
            item.TAI_SAN_ID = taisan.ID;
            item.SO_LUONG_KIEM_KE = 1;
            item.SO_LUONG_SO_SACH = 1;
            item.NGUYEN_GIA_KIEM_KE = yeucau != null ? yeucau.NGUYEN_GIA : null;
            item.NGUYEN_GIA_SO_SACH = yeucau != null ? yeucau.NGUYEN_GIA : null;
            item.GIA_TRI_CON_LAI_KIEM_KE = yeucau != null ? yeucau.NGUYEN_GIA : null;
            item.GIA_TRI_CON_LAI_SO_SACH = yeucau != null ? yeucau.NGUYEN_GIA : null;
            item.IS_PHAT_HIEN_THUA = false;
            item.DON_VI_ID = taisan.DON_VI_ID;
            item.DON_VI_BO_PHAN_ID = taisan.DON_VI_BO_PHAN_ID;
            item.TINH_TRANG_ID = (Decimal)enumTinhTrang.DANG_SU_DUNG;
        }
        public void PrepareKiemKeTaiSanByBienDong(TaiSanKiemKeTaiSan item, BienDong biendong, Decimal KiemKeId,DateTime NgayKiemKe)
        {
            //var BienDongTong = _bienDongService.GetBienDongsByTaiSanId(taiSanId: biendong.TAI_SAN_ID, NgayBDDen: NgayKiemKe);
            //năm lấy hao mòn
            var Nam = NgayKiemKe.Year;
            Decimal NguyenGiaTrongNam = 0;
            //nếu là không phải ngày chốt thì lấy năm trc đó
            if (NgayKiemKe.Day != 31 && NgayKiemKe.Month != 12)
            { 
                Nam -= 1;
                NguyenGiaTrongNam = _bienDongService.GetBienDongsByTaiSanIdForKK(taiSanId: biendong.TAI_SAN_ID, NgayBDDen: NgayKiemKe, NgayBDTu: new DateTime(NgayKiemKe.Year,01,01)).Select(c=>(decimal)(c.NGUYEN_GIA!=null?c.NGUYEN_GIA:0)).Sum();
            }
            var hm = _haoMonTaiSanService.GetHaoMonTaiSanByTSIdandNamBaoCao(biendong.TAI_SAN_ID, namBaoCao: NgayKiemKe.Year);
            var kh = _khauHaoTaiSanService.GetListKhauHaoTaiSans(biendong.TAI_SAN_ID, namKhauHao: NgayKiemKe.Year).OrderByDescending(c => c.THANG_KHAU_HAO).FirstOrDefault();
            item.GIA_TRI_CON_LAI_KIEM_KE = (hm != null ? hm.TONG_GIA_TRI_CON_LAI : 0) + (kh != null ? kh.TONG_GIA_TRI_CON_LAI : 0)+ NguyenGiaTrongNam;
            item.NGUYEN_GIA_KIEM_KE= (hm != null ? hm.TONG_NGUYEN_GIA : 0) + (kh != null ? kh.TONG_NGUYEN_GIA : 0)+ NguyenGiaTrongNam;

            if (item.GIA_TRI_CON_LAI_KIEM_KE == null || item.GIA_TRI_CON_LAI_KIEM_KE == 0)
            {
                var BienDongChiTiet = _bienDongChiTietService.GetBienDongChiTietByBDIdForKiemKe(bienDongId: biendong.ID);
                item.GIA_TRI_CON_LAI_KIEM_KE = BienDongChiTiet != null ? BienDongChiTiet.HM_GIA_TRI_CON_LAI : null;
            }
            item.KIEM_KE_ID = KiemKeId;
            item.TAI_SAN_ID = biendong.TAI_SAN_ID;
            item.SO_LUONG_KIEM_KE = 1;
            item.SO_LUONG_SO_SACH = 1;
            item.NGUYEN_GIA_SO_SACH = item.NGUYEN_GIA_KIEM_KE;
            item.GIA_TRI_CON_LAI_SO_SACH = item.GIA_TRI_CON_LAI_KIEM_KE;
            item.IS_PHAT_HIEN_THUA = false;
            item.DON_VI_ID = biendong.DON_VI_ID;
            item.DON_VI_BO_PHAN_ID = biendong.DON_VI_BO_PHAN_ID;
            item.TINH_TRANG_ID = (Decimal)enumTinhTrang.DANG_SU_DUNG;
        }

       public void PrepareTaiSanKiemKe(TaiSanKiemKeModel model, TaiSanKiemKe item)
        {
		item.NGAY_KIEM_KE = model.NGAY_KIEM_KE;
		item.DON_VI_BO_PHAN_ID = model.DON_VI_BO_PHAN_ID;
		item.NGUOI_LAP_BIEU = model.NGUOI_LAP_BIEU;
		item.DAI_DIEN_BPSD = model.DAI_DIEN_BPSD;
		item.KE_TOAN = model.KE_TOAN;
		item.TRUONG_BAN = model.TRUONG_BAN;
            
        }
        #endregion	
     }
}		

