//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/10/2020
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
using GS.Core.Domain.DanhMuc;
using GS.Services.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Services;

namespace GS.Web.Factories.DB
{
    public class DBTaiSanModelFactory : IDBTaiSanModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IDBTaiSanService _itemService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        #endregion

        #region Ctor

        public DBTaiSanModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IDBTaiSanService itemService,
            INhanHienThiService nhanHienThiService,
            ILoaiTaiSanService loaiTaiSanService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._nhanHienThiService = nhanHienThiService;
            this._loaiTaiSanService = loaiTaiSanService;
        }
        #endregion

        #region DBTaiSan
        public DBTaiSanSearchModel PrepareDBTaiSanSearchModel(DBTaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.ddlLoaiHinhTaiSan = new List<SelectListItem>();
            searchModel.ddlLoaiHinhTaiSan = (new enumLOAI_HINH_TAI_SAN()).ToSelectList().ToList();

            searchModel.ddlTrangThai = new List<SelectListItem>();
            searchModel.ddlTrangThai = (new enumTrangThaiDbTaiSan()).ToSelectList().ToList();
            searchModel.TrangThai = (decimal)enumTrangThaiDbTaiSan.LoiDL;

            searchModel.ddlLoaiTaiSan = new List<SelectListItem>();
            searchModel.ddlLoaiTaiSan = _loaiTaiSanService.GetAllLoaiTaiSans().Select(c => new SelectListItem()
            {
                Value = c.ID.ToString(),
                Text = c.TEN
            }).ToList();
            searchModel.ddlLoaiTaiSan.Insert(0, new SelectListItem()
            {
                Value = "0",
                Text = "Tất cả"
            });
            searchModel.SetGridPageSize();
            return searchModel;
        }

        public DBTaiSanListModel PrepareDBTaiSanListModel(DBTaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            if (searchModel.PageSize == 0)
                searchModel.PageSize = 10;
            //get items
            var items = _itemService.SearchTaiSans(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, MaTaiSan: searchModel.MaTaiSan, MaTaiSanDB: searchModel.MaTaiSanDB, LoaiHinhTaiSan: searchModel.LoaiHinhTaiSan, LoaiTaiSan: searchModel.LoaiTaiSan, TrangThai: searchModel.TrangThai, NgayDB: searchModel.NgayDB);

            //prepare list model
            var model = new DBTaiSanListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareDBTaiSanModel(new DBTaiSanModel(), c)),
                Total = items.TotalCount
            };
            return model;
        }
        public DBTaiSanModel PrepareDBTaiSanModel(DBTaiSanModel model, DBTaiSan item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DBTaiSanModel>();
                if (model.LOAI_HINH_TAI_SAN_ID.GetValueOrDefault(0) != 0)
                    model.LOAI_HINH_TAI_SAN_TEN = _nhanHienThiService.GetGiaTriEnum((enumLOAI_HINH_TAI_SAN)model.LOAI_HINH_TAI_SAN_ID.GetValueOrDefault(0));
                model.TRANG_THAI_TEN = _nhanHienThiService.GetGiaTriEnum((enumTrangThaiDbTaiSan)model.TRANG_THAI_ID.GetValueOrDefault(0));
                LoaiTaiSan lts = _loaiTaiSanService.GetLoaiTaiSanById(item.LOAI_TAI_SAN_ID.GetValueOrDefault(0));
                if (lts != null)
                    model.LOAI_TAI_SAN_TEN = lts.TEN;
            }
            //more

            return model;
        }
        public void PrepareDBTaiSan(DBTaiSanModel model, DBTaiSan item)
        {
            item.GUID = model.GUID;
            item.QLDKTS_MA = model.QLDKTS_MA;
            item.DB_MA = model.DB_MA;
            item.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;
            item.LOAI_TAI_SAN_ID = model.LOAI_TAI_SAN_ID;
            item.PHAN_MEM_DONG_BO_ID = model.PHAN_MEM_DONG_BO_ID;
            item.DATA_JSON = model.DATA_JSON;
            item.NGAY_DONG_BO = model.NGAY_DONG_BO;
            item.IS_TAI_SAN_TOAN_DAN = model.IS_TAI_SAN_TOAN_DAN;
            item.QUYET_DINH_TICH_THU_SO = model.QUYET_DINH_TICH_THU_SO;
            item.QUYET_DINH_TICH_THU_NGAY = model.QUYET_DINH_TICH_THU_NGAY;
            item.TAI_KHOAN_DONG_BO_ID = model.TAI_KHOAN_DONG_BO_ID;
            item.IS_BIEN_DONG = model.IS_BIEN_DONG;
            item.TRANG_THAI_ID = model.TRANG_THAI_ID;

        }
        #endregion
    }
}

