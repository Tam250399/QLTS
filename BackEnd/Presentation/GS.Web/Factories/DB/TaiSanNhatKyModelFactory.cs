//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/10/2020
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
using GS.Core.Domain.DB;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.DB;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.DB;
using GS.Services;
using GS.Core.Domain.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Factories.DB
{
    public class TaiSanNhatKyModelFactory : ITaiSanNhatKyModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ITaiSanNhatKyService _itemService;
        private readonly INhanHienThiService _nhanHienThiService;
        #endregion

        #region Ctor

        public TaiSanNhatKyModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            ITaiSanNhatKyService itemService,
            INhanHienThiService nhanHienThiService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._nhanHienThiService = nhanHienThiService;
        }
        #endregion

        #region TaiSanNhatKy
        public TaiSanNhatKySearchModel PrepareTaiSanNhatKySearchModel(TaiSanNhatKySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            searchModel.ddlLoaiHinhTaiSan = new List<SelectListItem>();
            searchModel.ddlLoaiHinhTaiSan = (new enumLOAI_HINH_TAI_SAN()).ToSelectList().ToList();
            searchModel.ddlTrangThai = new List<SelectListItem>();
            searchModel.ddlTrangThai = (new enumTrangThaiTaiSanNhatKy()).ToSelectList().ToList();
            searchModel.ddlTrangThai.Insert(0, new SelectListItem()
            {
                Value = "-1",
                Text = "Tất cả"
            });
            searchModel.TrangThai = -1;
            return searchModel;
        }

        public TaiSanNhatKyListModel PrepareTaiSanNhatKyListModel(TaiSanNhatKySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            if (searchModel.PageSize == 0)
                searchModel.PageSize = 10;
            //get items
            var items = _itemService.SearchTaiSanNhatKys(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, DonViId: _workContext.CurrentDonVi.ID, TrangThai: searchModel.TrangThai, MaTaiSan: searchModel.MaTaiSan, MaTaiSanDB: searchModel.MaTaiSanDB, NgayDB: searchModel.NgayDB, LoaiHinhTaiSan: searchModel.LoaiHinhTaiSan, isTaiSanToanDan: Convert.ToBoolean(searchModel.IS_TaiSanToanDan));

            //prepare list model
            var model = new TaiSanNhatKyListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareTaiSanNhatKyModel(new TaiSanNhatKyModel(), c)),
                Total = items.TotalCount
            };
            return model;
        }
        public TaiSanNhatKyModel PrepareTaiSanNhatKyModel(TaiSanNhatKyModel model, TaiSanNhatKy item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<TaiSanNhatKyModel>();
                model.MA_TAI_SAN_DB = item.Taisan.MA_DB;
                model.MA_TAI_SAN = item.Taisan.MA;
                model.LOAI_HINH_TAI_SAN_TEN = _nhanHienThiService.GetGiaTriEnum((enumLOAI_HINH_TAI_SAN)model.LOAI_HINH_TAI_SAN_ID.GetValueOrDefault(0));
                model.TRANG_THAI_TEN = _nhanHienThiService.GetGiaTriEnum((enumTrangThaiTaiSanNhatKy)model.TRANG_THAI_ID.GetValueOrDefault(0));
            }
            //more
            return model;
        }
        public void PrepareTaiSanNhatKy(TaiSanNhatKyModel model, TaiSanNhatKy item)
        {
            item.TAI_SAN_ID = model.TAI_SAN_ID;
            item.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;
            item.TRANG_THAI_ID = model.TRANG_THAI_ID;
            item.NGAY_DONG_BO = model.NGAY_DONG_BO;
            item.NGAY_CAP_NHAT = model.NGAY_CAP_NHAT;
            item.IS_TAI_SAN_TOAN_DAN = model.IS_TAI_SAN_TOAN_DAN;
            item.QUYET_DINH_TICH_THU_ID = model.QUYET_DINH_TICH_THU_ID;
            item.BD_IDS = model.BD_IDS;
            item.RESPONSE = model.RESPONSE;
            item.BD_IDS_DANG_DB = model.BD_IDS_DANG_DB;

        }
        #endregion
    }
}

