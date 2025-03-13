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
using GS.Services;
using GS.Web.Factories.DanhMuc;

namespace GS.Web.Factories.CCDC
{
    public class KiemKeCongCuModelFactory:IKiemKeCongCuModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IKiemKeCongCuService _itemService;
        private readonly IXuatNhapService _xuatNhapService;
        private readonly INhapXuatCongCuService _nhapXuatCongCuService;
        private readonly ICongCuService _congCuService;
        private readonly INhomCongCuService _nhomCongCuService;
        private readonly IKiemKeService _kiemKeService;
        private readonly IKiemKeCongCuService _kiemKeCongCuService;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly INhomCongCuModelFactory _nhomCongCuModelFactory;
        #endregion

        #region Ctor

        public KiemKeCongCuModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IKiemKeCongCuService itemService,
            IXuatNhapService xuatNhapService,
            INhapXuatCongCuService nhapXuatCongCuService,
            ICongCuService congCuService,
            INhomCongCuService nhomCongCuService,
            IKiemKeService kiemKeService,
            IKiemKeCongCuService kiemKeCongCuService,
            IDonViBoPhanService donViBoPhanService,
            INhomCongCuModelFactory nhomCongCuModelFactory
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
            this._kiemKeService = kiemKeService;
            this._kiemKeCongCuService = kiemKeCongCuService;
            this._donViBoPhanService = donViBoPhanService;
            this._nhomCongCuModelFactory = nhomCongCuModelFactory;
        }
        #endregion
        
        #region KiemKeCongCu
        public KiemKeCongCuSearchModel PrepareKiemKeCongCuSearchModel(KiemKeCongCuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public KiemKeCongCuListModel PrepareKiemKeCongCuListModel(KiemKeCongCuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchKiemKeCongCus(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize, KiemKeId: searchModel.KiemKeId, isPhatHienThua: searchModel.isPhatHienThua);
            
            //prepare list model
            var model = new KiemKeCongCuListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareKiemKeCongCuModel(new KiemKeCongCuModel(), c)),
                Total = items.TotalCount
            };
            return model;
        }
        public KiemKeCongCuModel PrepareKiemKeCongCuModel(KiemKeCongCuModel model, KiemKeCongCu item)
        {
            model = item.ToModel<KiemKeCongCuModel>();
            if (!item.IS_PHAT_HIEN_THUA)
            {
                model.MaCongCu = item.CongCu.MA;
                model.TenCongCu = item.CongCu.TEN;
                model.DonGiaText = item.DON_GIA.ToVNStringNumber();
            }
            else {
                model.TenCongCu = item.TEN_CONG_CU_THUA;
                model.SoLuongText = item.SO_LUONG_THUA.ToVNStringNumber();
                model.DonGiaText = item.DON_GIA_THUA.ToVNStringNumber();
              
            }
            model.SoLuongText = item.SO_LUONG_KIEM_KE.ToVNStringNumber();
            if(model.TINH_TRANG_ID!=null)
            model.TrangThaiText = ((decimal)model.TINH_TRANG_ID).ToStringTrangThaiCongCuPhatHienThua();
            return model;
        }
        public KiemKeCongCuModel PrepareKiemKeCongCuModel(KiemKeCongCuModel model, KiemKeCongCu item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<KiemKeCongCuModel>();
            }
            model.DDLTinhTrangEnum = ((enumTinhTrang)model.enumTinhTrang).ToSelectList();
            model.DDLDeNghiXuLyEnum = ((enumDeNghiXuLy)model.enumDeNghiXuLy).ToSelectList();
            model.DDLNhomCongCu = _nhomCongCuModelFactory.PrepareDDLNhomCongCu(donViId: _workContext.CurrentDonVi.ID);
            return model;
        }
        public void PrepareKiemKeCongCu(KiemKeCongCuModel model, KiemKeCongCu item)
        {
            item.TINH_TRANG_ID = model.TINH_TRANG_ID;
            item.DE_NGHI_XU_LY = model.DE_NGHI_XU_LY > 0 ? model.DE_NGHI_XU_LY : item.DE_NGHI_XU_LY;
            item.SO_LUONG_KIEM_KE = model.SO_LUONG_KIEM_KE;
            item.GHI_CHU = model.GHI_CHU;
            if (item.IS_PHAT_HIEN_THUA)
            {
                item.SO_LUONG_THUA = model.SO_LUONG_THUA;
                item.TEN_CONG_CU_THUA = model.TEN_CONG_CU_THUA;
                item.NHOM_CONG_CU_ID = model.NHOM_CONG_CU_ID;
                item.DON_GIA_THUA = model.DON_GIA_THUA;
            }
        }

        public KiemKeCongCuListModel PrepareTimKiemCongCu(KiemKeCongCuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            var items = _nhapXuatCongCuService.GetNhapXuatCongCusByBaoHanh((Decimal)searchModel.BoPhanKiemKeId, KeySearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, listCongCuDaChon: searchModel.ListCongCuThuaDaChon, DonViId: _workContext.CurrentDonVi.ID);

            //prepare list model
            var model = new KiemKeCongCuListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareCongCu(new KiemKeCongCuModel(), c, (Decimal)searchModel.BoPhanKiemKeId)),
                Total = items.TotalCount
            };
            return model;
        }

        public KiemKeCongCuModel PrepareCongCu(KiemKeCongCuModel model, NhapXuatCongCu map, Decimal BoPhanId)
        {
            model.MapId = map.ID;
            model.MaCongCu = map.CongCu.MA;
            model.TenCongCu = map.CongCu.TEN;
            model.SoLuongText = map.SoLuongCoThePhanBo.ToVNStringNumber();
            model.NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((Decimal)map.CongCu.NHOM_CONG_CU_ID).TEN;
            model.DonGiaText = map.DON_GIA.ToVNStringNumber();
            model.DON_VI_BO_PHAN_ID = BoPhanId;
            model.BoPhanSuDungText = _donViBoPhanService.GetDonViBoPhanById(BoPhanId).TEN;
            model.SoLuongKiemKe = map.SoLuongCoThePhanBo;
            model.SoLuongSoSach = map.SoLuongCoThePhanBo;

            return model;
        }

        public virtual CongCuListModel PrepareListCongCu(CongCuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var items = _congCuService.SearchCongCus(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new CongCuListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareCongCu(new CongCuModel(), c)),
                Total = items.TotalCount
            };
            return model;
        }

        public CongCuModel PrepareCongCu(CongCuModel model, CongCu item)
        {
            model.ID = item.ID;
            model.GUID = item.GUID;
            model.MA = item.MA;
            model.TEN = item.TEN;
            //model.DonGiaText = item.DON_GIA.ToVNStringNumber();
            model.NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((Decimal)item.NHOM_CONG_CU_ID).TEN;

            return model;
        }
        #endregion
    }
}		

