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
using GS.Services.DanhMuc;
using GS.Web.Factories.DanhMuc;
using GS.Core.Domain.DanhMuc;

namespace GS.Web.Factories.CCDC
{
    public class ChoThueModelFactory: IChoThueModelFactory
    {				
        #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IChoThueService _itemService;
            private readonly INhapXuatCongCuService _nhapXuatCongCuService;
            private readonly ICongCuService _congCuService;
            private readonly IXuatNhapService _xuatNhapService;
            private readonly INhomCongCuService _nhomCongCuService;
            private readonly IDonViService _donViService;
            private readonly IDonViBoPhanService _donViBoPhanService;
            private readonly IDoiTacModelFactory _doiTacModelFactory;
            private readonly ICongCuModelFactory _congCuModelFactory;
            private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
        #endregion

        #region Ctor

        public ChoThueModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IChoThueService itemService,
            INhapXuatCongCuService nhapXuatCongCuService,
            ICongCuService congCuService,
            IXuatNhapService xuatNhapService,
            INhomCongCuService nhomCongCuService,
            IDonViService donViService,
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
            this._nhapXuatCongCuService = nhapXuatCongCuService;
            this._congCuService = congCuService;
            this._xuatNhapService = xuatNhapService;
            this._nhomCongCuService = nhomCongCuService;
            this._donViService = donViService;
            this._donViBoPhanService = donViBoPhanService;
            this._doiTacModelFactory = doiTacModelFactory;
            this._congCuModelFactory = congCuModelFactory;
            this._donViBoPhanModelFactory = donViBoPhanModelFactory;
        }
        #endregion
        
        #region ChoThue
        public ChoThueSearchModel PrepareChoThueSearchModel(ChoThueSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.DDLCongCu = _congCuModelFactory.PrepareDDLCongCuDonVi(donviId:_workContext.CurrentDonVi.ID);
            searchModel.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(isAddFirst: true, DonViId: _workContext.CurrentDonVi.ID).ToList();
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public ChoThueListModel PrepareChoThueListModel(ChoThueSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchChoThues(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize, CongCuId:searchModel.CongCuId,DonViBoPhanId:searchModel.DonViBoPhanId,NgayChoThueDen:searchModel.NgayChoThueDen,NgayChoThueTu:searchModel.NgayChoThueTu,DonViId:_workContext.CurrentDonVi.ID);
            
            //prepare list model
            var model = new ChoThueListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepapreChoThueModelForList(new ChoThueModel(), c)),
                Total = items.TotalCount
            };
            return model;
        }
        public ChoThueModel PrepapreChoThueModelForList(ChoThueModel model, ChoThue item)
        {
            var congcu = _congCuService.GetCongCuById(item.CONG_CU_ID);
            var xuatnhap = _xuatNhapService.GetXuatNhapById(item.NHAP_XUAT_ID);
            model = item.ToModel<ChoThueModel>();
            model.MaCongCuText = congcu.MA;
            model.TenCongCuText = congcu.TEN;
            model.NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((Decimal)congcu.NHOM_CONG_CU_ID).TEN;
            model.BoPhanSuDungText = _donViBoPhanService.GetDonViBoPhanById((decimal)xuatnhap.DON_VI_BO_PHAN_ID).TEN;
            model.DonGiaText = item.SO_TIEN_THU_DUOC.ToVNStringNumber();
            model.NgaySuDungText = xuatnhap.NGAY_XUAT_NHAP.toDateVNString();
            model.stringGuid = congcu.GUID + "," + xuatnhap.GUID;

            return model;
        }
        public void PrepareChoThue(ChoThueModel model, ChoThue item)
        {
            item.QUYET_DINH_SO = model.QUYET_DINH_SO;
            item.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY;
            item.CAP_QUYET_DINH = model.CAP_QUYET_DINH;
            item.NGAY_CHO_THUE_FROM = model.NGAY_CHO_THUE_FROM;
            item.NGAY_CHO_THUE_TO = model.NGAY_CHO_THUE_TO;
            item.KHACH_HANG_ID = model.KHACH_HANG_ID;
            item.HOP_DONG_SO = model.HOP_DONG_SO;
            item.HOP_DONG_NGAY = model.HOP_DONG_NGAY;
            item.SO_LUONG = model.SO_LUONG;
            item.SO_TIEN_THU_DUOC = model.SO_TIEN_THU_DUOC;
            item.GHI_CHU = model.GHI_CHU;
        }

        public ChoThueListModel PrepareChoThueModelForChonCongCu(ChoThueSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var items = _nhapXuatCongCuService.GetNhapXuatCongCusByBaoHanh(searchModel.DonViBoPhanId, KeySearch: searchModel.KeySearchCongCu, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize,DonViId:_workContext.CurrentDonVi.ID);

            //prepare list model
            var model = new ChoThueListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareCongCu(new ChoThueModel(), c, searchModel.DonViBoPhanId)),
                Total = items.TotalCount
            };
            return model;
        }

        public ChoThueModel PrepareCongCu(ChoThueModel model, NhapXuatCongCu map, Decimal BoPhanId)
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

        public ChoThueModel PrepareChoThueModel(ChoThueModel model, Decimal MapId)
        {
            var map = new NhapXuatCongCu();
            if (MapId > 0)
            {
                 map = _nhapXuatCongCuService.GetNhapXuatCongCuById(MapId);
            }
            else
            {
                 map = _nhapXuatCongCuService.GetNhapXuatCongCu(CongCuId: model.CONG_CU_ID, NhapXuatId: model.NHAP_XUAT_ID);
            }
            if (map != null)
            {
                
                model.MapId = map.ID;
                model.MaCongCuText = map.CongCu.MA;
                model.TenCongCuText = map.CongCu.TEN;
                model.SoLuongCon = _nhapXuatCongCuService.GetSoLuongDangCoByMapId(map.ID).SoLuongCoThePhanBo;
                model.SoLuongText = model.SoLuongCon.ToString();
                model.NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((Decimal)map.CongCu.NHOM_CONG_CU_ID).TEN;
                model.DonGiaText = map.DON_GIA.ToVNStringNumber();
                model.DON_VI_BO_PHAN_ID = (Decimal)map.XuatNhap.DON_VI_BO_PHAN_ID;
                model.BoPhanSuDungText = _donViBoPhanService.GetDonViBoPhanById((Decimal)map.XuatNhap.DON_VI_BO_PHAN_ID).TEN;
                model.DDLDoiTac = _doiTacModelFactory.PrepareSelectListDoiTac(LoaiDoiTacId: (Decimal)enumLOAI_DOI_TAC.KhachHang, isAddFirst: true);
                model.NgayXuatNhap = map.XuatNhap.NGAY_XUAT_NHAP;
            }

            return model;
        }
        #endregion
    }
}		

