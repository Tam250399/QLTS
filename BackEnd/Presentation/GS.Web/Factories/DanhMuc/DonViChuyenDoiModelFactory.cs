//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 25/3/2020
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
using GS.Core.Domain.DanhMuc;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.DanhMuc;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.DanhMuc;
using GS.Services;

namespace GS.Web.Factories.DanhMuc
{
    public class DonViChuyenDoiModelFactory:IDonViChuyenDoiModelFactory
	{				
        #region Fields    		
        private readonly ICacheManager _cacheManager;            
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IDonViChuyenDoiService _itemService;
        private readonly IDonViService _donViService;
        private readonly ILoaiDonViService _loaiDonViService;
        private readonly ILoaiDonViModelFactory _itemLoaiDonViModelFactory;
        private readonly INhanHienThiService _nhanHienThiService;

        #endregion

        #region Ctor

        public DonViChuyenDoiModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IDonViChuyenDoiService itemService,
            ILoaiDonViModelFactory itemLoaiDonViModelFactory,
            IDonViService donViService,
            ILoaiDonViService loaiDonViService,
            INhanHienThiService nhanHienThiService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
            this._itemLoaiDonViModelFactory = itemLoaiDonViModelFactory;
            this._donViService = donViService;
            this._loaiDonViService = loaiDonViService;
            this._nhanHienThiService = nhanHienThiService;
        }
        #endregion
        
        #region DonViChuyenDoi
        public DonViChuyenDoiSearchModel PrepareDonViChuyenDoiSearchModel(DonViChuyenDoiSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.SetGridPageSize();
            if (searchModel.DonViId > 0)
            {
                var donvi = _donViService.GetDonViById(searchModel.DonViId);
                searchModel.LOAI_DON_VI = _loaiDonViService.GetLoaiDonViById(donvi.LOAI_DON_VI_ID.Value).TEN;
                searchModel.DIA_CHI = donvi.DIA_CHI;
                searchModel.MA = donvi.MA;
                searchModel.TEN = donvi.TEN;
                if (donvi.PARENT_ID != null)
                {
                    searchModel.DON_VI_CAP_TREN = _donViService.GetDonViById(donvi.PARENT_ID.Value).TEN;
                }
            }
            return searchModel;
        }


        public DonViChuyenDoiListModel PrepareDonViChuyenDoiListModel(DonViChuyenDoiSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchDonViChuyenDois(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize, forDonVi: searchModel.DonViId);
            
            //prepare list model
            var model = new DonViChuyenDoiListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => {
                    var m = c.ToModel<DonViChuyenDoiModel>();
                    m.LOAI_DON_VI = _loaiDonViService.GetLoaiDonViById(c.LOAI_DON_VI_ID).TEN;
                    m.NgayQuyetDinhDuDieuKien = m.QUYET_DINH_NGAY.toDateVNString();
                    m.NgayQuyetDinhGiaoVon = m.QUYET_DINH_GIAO_VON_NGAY.toDateVNString();
                    return m;
                }),
                Total = items.TotalCount
            };
            return model;
        }
        public DonViChuyenDoiModel PrepareDonViChuyenDoiModel(DonViChuyenDoiModel model, DonViChuyenDoi item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DonViChuyenDoiModel>();
                model.MA_DVQHNS_MOI = item.MA_DVQHNS;
                model.LOAI_CAP_DON_VI_ID_MOI = item.LOAI_CAP_DON_VI_ID;
                model.CAP_DON_VI_ID_MOI = item.CAP_DON_VI_ID;
                var donvi = _donViService.GetDonViById(item.DON_VI_ID);
                model.TENCU = donvi.TEN;
                model.MA = donvi.MA;
                model.DIA_CHI = donvi.DIA_CHI;
                model.MA_DVQHNS = donvi.MA_DVQHNS;
                model.TEN_LOAI_DON_VI_CU = _loaiDonViService.GetLoaiDonViById(Convert.ToDecimal(donvi.LOAI_DON_VI_ID??0))?.TEN;
                model.TEN_LOAI_CAP_DON_VI_CU = _nhanHienThiService.GetGiaTriEnum<EnumLoaiCapDonVi>((EnumLoaiCapDonVi)donvi.LOAI_CAP_DON_VI_ID);
                model.TEN_CAP_DON_VI_CU = _nhanHienThiService.GetGiaTriEnum<CapEnum>((CapEnum)donvi.CAP_DON_VI_ID);

                model.ddlLoaiCapDonViMoi = (new EnumLoaiCapDonVi()).ToSelectList();
                if (model.LOAI_CAP_DON_VI_ID_MOI == (int)EnumLoaiCapDonVi.CapDiaPhuong)
                    model.dllCapDonViMoi = ((EnumCapDonViDiaPhuong)(model.CAP_DON_VI_ID_MOI ?? 0)).ToSelectList();
                else
                    model.dllCapDonViMoi = ((EnumCapDonViTrungUong)(model.CAP_DON_VI_ID_MOI ?? 0)).ToSelectList();
                if (donvi.PARENT_ID != null)
                {
                    var donViCha = _donViService.GetDonViById(donvi.PARENT_ID.Value);
                    model.TenDonViCha = donViCha.TEN;
                }
                model.LoaiDonVi = _itemLoaiDonViModelFactory.PrepareSelectListLoaiDonVi(ValSelected: model.DON_VI_ID ,isAddFirst: true);           
            }
            //more
            return model;
        }
       public void PrepareDonViChuyenDoi(DonViChuyenDoiModel model, DonViChuyenDoi item)
        {
		    item.DON_VI_ID = model.DON_VI_ID;
		    item.TEN = model.TEN;
		    item.LOAI_DON_VI_ID = model.LOAI_DON_VI_ID;
		    item.QUYET_DINH_SO = model.QUYET_DINH_SO;
		    item.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY;
		    item.QUYET_DINH_GIAO_VON_SO = model.QUYET_DINH_GIAO_VON_SO;
		    item.QUYET_DINH_GIAO_VON_NGAY = model.QUYET_DINH_GIAO_VON_NGAY;
		    item.NGAY_TAO = model.NGAY_TAO;
		    item.NGUOI_TAO_ID = model.NGUOI_TAO_ID;
		    item.GHI_CHU = model.GHI_CHU;
            
        }
        #endregion	
     }
}		

