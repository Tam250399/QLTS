//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
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
    public class NhapXuatCongCuModelFactory : INhapXuatCongCuModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly INhapXuatCongCuService _itemService;
        private readonly INhomCongCuService _nhomCongCuService;
        #endregion

        #region Ctor

        public NhapXuatCongCuModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            INhapXuatCongCuService itemService,
            INhomCongCuService nhomCongCuService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._nhomCongCuService = nhomCongCuService;
        }
        #endregion

        #region NhapXuatCongCu
        public NhapXuatCongCuSearchModel PrepareNhapXuatCongCuSearchModel(NhapXuatCongCuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public NhapXuatCongCuListModel PrepareNhapXuatCongCuListModel(NhapXuatCongCuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchNhapXuatCongCus(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new NhapXuatCongCuListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<NhapXuatCongCuModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public NhapXuatCongCuModel PrepareNhapXuatCongCuModel(NhapXuatCongCuModel model, NhapXuatCongCu item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<NhapXuatCongCuModel>();
            }
            //more

            return model;
        }
        public void PrepareNhapXuatCongCu(NhapXuatCongCuModel model, NhapXuatCongCu item)
        {
            item.NHAP_XUAT_ID = model.NHAP_XUAT_ID;
            item.CONG_CU_ID = model.CONG_CU_ID;
            item.SO_LUONG = model.SO_LUONG;
        }

        public NhapXuatCongCuListModel PrepareForPhanBo(CongCuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var congcuPhanBo = _itemService.SearchPhanBo(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new NhapXuatCongCuListModel
            {
                //fill in model values from the entity
                Data = congcuPhanBo.Select(c => PrepareCongCuForPhanBo(c, new NhapXuatCongCuModel())),
                Total = congcuPhanBo.TotalCount
            };
            return model;
        }
        public NhapXuatCongCuModel PrepareCongCuForPhanBo(NhapXuatCongCu item, NhapXuatCongCuModel model)
        {
            model = item.ToModel<NhapXuatCongCuModel>();
            var nhomCCDC = _nhomCongCuService.GetNhomCongCuById(item.CongCu.NHOM_CONG_CU_ID.GetValueOrDefault(0));
            model.NhomCongCuText = nhomCCDC != null ? nhomCCDC.TEN : null;
            model.SO_LUONG = item.SoLuongCoThePhanBo;
            model.DonGiaText = item.DON_GIA.ToVNStringNumber();
            model.TRANG_THAI_ID = item.TRANG_THAI_ID;
            model.TenTrangThai = (model.TRANG_THAI_ID).ToStringTrangThaiCongCu();
            model.TenCongCuText = item.CongCu.TEN;
            model.MaCongCuText = item.CongCu.MA;
            model.MaLoCongCuText = item.XuatNhap.MA;

            return model;
        }
        #endregion
    }
}

