//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/2/2020
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
using GS.Core.Domain.DanhMuc;
using GS.Web.Factories.DanhMuc;

namespace GS.Web.Factories.CCDC
{
    public class SuaChuaBaoDuongModelFactory:ISuaChuaBaoDuongModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly ISuaChuaBaoDuongService _itemService;
            private readonly ICongCuService _congCuService;
            private readonly IDonViService _donViService;
            private readonly INhomCongCuService _nhomCongCuService;
            private readonly IXuatNhapService _xuatNhapService;
            private readonly INhapXuatCongCuService _nhapXuatCongCuService;
            private readonly IDonViBoPhanService _donViBoPhanService;
            private readonly IDoiTacModelFactory _doiTacModelFactory;
            private readonly ICongCuModelFactory _congCuModelFactory;
            private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
        #endregion

        #region Ctor

        public SuaChuaBaoDuongModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            ISuaChuaBaoDuongService itemService,
            ICongCuService congCuService,
            IDonViService donViService,
            INhomCongCuService nhomCongCuService,
            IXuatNhapService xuatNhapService,
            INhapXuatCongCuService nhapXuatCongCuService,
            IDonViBoPhanService donViBoPhanService,
            IDoiTacModelFactory doiTacModelFactory,
            ICongCuModelFactory congCuModelFactory,
            IDonViBoPhanModelFactory donViBoPhanModelFactory
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
            this._congCuService = congCuService;
            this._donViService = donViService;
            this._nhomCongCuService = nhomCongCuService;
            this._xuatNhapService = xuatNhapService;
            this._nhapXuatCongCuService = nhapXuatCongCuService;
            this._donViBoPhanService = donViBoPhanService;
            this._doiTacModelFactory = doiTacModelFactory;
            this._congCuModelFactory = congCuModelFactory;
            this._donViBoPhanModelFactory = donViBoPhanModelFactory;
    }
        #endregion
        
        #region SuaChuaBaoDuong
        public SuaChuaBaoDuongSearchModel PrepareSuaChuaBaoDuongSearchModel(SuaChuaBaoDuongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.DDLCongCu = _congCuModelFactory.PrepareDDLCongCuDonVi(donviId: _workContext.CurrentDonVi.ID);
            searchModel.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(isAddFirst: true, DonViId: _workContext.CurrentDonVi.ID).ToList();
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public SuaChuaBaoDuongListModel PrepareSuaChuaBaoDuongListModel(SuaChuaBaoDuongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchSuaChuaBaoDuongs(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize,CongCuId:searchModel.CongCuId, BoPhanId:searchModel.BoPhanId, NgayDen:searchModel.NgayDen, NgayTu:searchModel.NgayTu);
            
            //prepare list model
            var model = new SuaChuaBaoDuongListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareListSuaChua(new SuaChuaBaoDuongModel(), c)),
                Total = items.TotalCount
            };
            return model;
        }
        public SuaChuaBaoDuongModel PrepareListSuaChua(SuaChuaBaoDuongModel model, SuaChuaBaoDuong item)
        {
            var congcu = _congCuService.GetCongCuById(item.CONG_CU_ID);
            var xuatnhap = _xuatNhapService.GetXuatNhapById(item.NHAP_XUAT_ID);
            model = item.ToModel<SuaChuaBaoDuongModel>();
            model.MaCongCuText = congcu.MA;
            model.TenCongCuText = congcu.TEN;
            model.NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((Decimal)congcu.NHOM_CONG_CU_ID).TEN;
            model.BoPhanSuDungText = _donViBoPhanService.GetDonViBoPhanById((decimal)xuatnhap.DON_VI_BO_PHAN_ID).TEN;
            model.NgaySuaChuaText = model.NGAY_SUA_CHUA_FROM.toDateVNString();
            model.DonGiaText = item.GIA_TRI_SUA_CHUA.ToVNStringNumber();

            return model;
        }
        public SuaChuaBaoDuongListModel PrepareSuaChuaModelForChonCongCu(SuaChuaBaoDuongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var items = _nhapXuatCongCuService.GetNhapXuatCongCusByBaoHanh(searchModel.BoPhanId, KeySearch: searchModel.KeySearchCongCu, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, DonViId: _workContext.CurrentDonVi.ID);

            //prepare list model
            var model = new SuaChuaBaoDuongListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareCongCu(new SuaChuaBaoDuongModel(), c, searchModel.BoPhanId)),
                Total = items.TotalCount
            };
            return model;
        }
        public SuaChuaBaoDuongModel PrepareCongCu(SuaChuaBaoDuongModel model, NhapXuatCongCu map, Decimal BoPhanId)
        {
            model.MapId = map.ID;
            model.MaCongCuText = map.CongCu.MA;
            model.TenCongCuText = map.CongCu.TEN;
            model.SoLuongText = map.SoLuongCoThePhanBo.ToVNStringNumber();
            model.NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((Decimal)map.CongCu.NHOM_CONG_CU_ID).TEN;
            model.DonGiaText = map.DON_GIA.ToVNStringNumber();
            model.DON_VI_BO_PHAN_ID = (decimal)map.XuatNhap.DON_VI_BO_PHAN_ID;
            model.BoPhanSuDungText = _donViBoPhanService.GetDonViBoPhanById((decimal)map.XuatNhap.DON_VI_BO_PHAN_ID).TEN;
            model.MaLoCongCuText = map.XuatNhap.MA;
            return model;
        }

        public SuaChuaBaoDuongModel PrepareSuaChuaBaoDuongModel(SuaChuaBaoDuongModel model, Decimal MapId, Decimal BoPhanId)
        {
            var map = _nhapXuatCongCuService.GetSoLuongDangCoByMapId(MapId);
            model.MapId = map.ID;
            model.MaCongCuText = map.CongCu.MA;
            model.TenCongCuText = map.CongCu.TEN;
            var x = map.SoLuongCoThePhanBo + (model.ID > 0 ? model.SO_LUONG_SUA_CHUA : 0);
            model.SoLuongText = (map.SoLuongCoThePhanBo + (model.ID>0?_itemService.GetSuaChuaBaoDuongById(model.ID).SO_LUONG_SUA_CHUA:0)).ToVNStringNumber();
            model.NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((Decimal)map.CongCu.NHOM_CONG_CU_ID).TEN;
            model.DonGiaText = map.DON_GIA.ToVNStringNumber();
            model.DON_VI_BO_PHAN_ID = BoPhanId;
            model.BoPhanSuDungText = _donViBoPhanService.GetDonViBoPhanById(BoPhanId).TEN;
            model.DDLDoiTac = _doiTacModelFactory.PrepareSelectListDoiTac(LoaiDoiTacId: (Decimal)enumLOAI_DOI_TAC.KhachHang, isAddFirst: true,strFirstRow:"-- Chọn đơn vị sửa chữa --");
            model.TongNguyenGia = (decimal)map.DON_GIA *(map.SoLuongCoThePhanBo + (model.ID > 0 ? _itemService.GetSuaChuaBaoDuongById(model.ID).SO_LUONG_SUA_CHUA : 0));
            var xn = _xuatNhapService.GetXuatNhapById(map.NHAP_XUAT_ID);
            if (xn != null)
                model.NgayXnBefore = xn.NGAY_XUAT_NHAP;
            return model;
        }

        public void PrepareSuaChuaBaoDuong(SuaChuaBaoDuongModel model, SuaChuaBaoDuong item)
        {
		    item.SO_QUYET_DINH = model.SO_QUYET_DINH;
		    item.NGAY_QUYET_DINH = model.NGAY_QUYET_DINH;
		    item.CAP_QUYET_DINH = model.CAP_QUYET_DINH;
		    item.NGAY_SUA_CHUA_FROM = model.NGAY_SUA_CHUA_FROM??DateTime.Now;
		    item.NGAY_SUA_CHUA_TO = model.NGAY_SUA_CHUA_TO??DateTime.Now;
		    item.SO_LUONG_SUA_CHUA = model.SO_LUONG_SUA_CHUA;
		    item.GIA_TRI_SUA_CHUA = model.GIA_TRI_SUA_CHUA;
		    item.KHACH_HANG_ID = model.KHACH_HANG_ID;
		    item.CHUNG_TU_SO = model.CHUNG_TU_SO;
		    item.CHUNG_TU_NGAY = model.CHUNG_TU_NGAY;
		    item.GHI_CHU = model.GHI_CHU;
        }
        #endregion	
    }
}		

