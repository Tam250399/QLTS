//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.DanhMuc;
using GS.Services;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Framework.Extensions;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.DanhMuc
{
    public class ChucDanhModelFactory : IChucDanhModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IChucDanhService _itemService;
        private readonly INhanHienThiService _itemNhanHienThiService;
        private readonly IDonViService _donViService;
        #endregion

        #region Ctor

        public ChucDanhModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IChucDanhService itemService,
            INhanHienThiService itemNhanHienThiService,
            IDonViService donViService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._itemNhanHienThiService = itemNhanHienThiService;
            this._donViService = donViService;
        }
        #endregion

        #region ChucDanh
        public ChucDanhSearchModel PrepareChucDanhSearchModel(ChucDanhSearchModel searchModel)
        {
            
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();

            //add more
            searchModel.ddlKhoiDonVi = ((KhoiDonViEnum)searchModel.KhoiDonViEnum).ToSelectList();

            return searchModel;
        }

        public ChucDanhListModel PrepareChucDanhListModel(ChucDanhSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchChucDanhs(Keysearch: searchModel.KeySearch, KhoiDonViIdSearch: searchModel.KhoiDonViIdSearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new ChucDanhListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<ChucDanhModel>();
                    m.TEN_KHOI_DON_VI = _itemNhanHienThiService.GetGiaTriEnum(m.KhoiDonViEnumId);
                    return m;
                }),
                Total = items.TotalCount
            };
            return model;
        }
        public ChucDanhModel PrepareChucDanhModel(ChucDanhModel model, ChucDanh item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<ChucDanhModel>();
            }
            //more
            model.ddlKhoiDonVi = ((KhoiDonViEnum)model.KhoiDonViEnum).ToSelectList();

            return model;
        }
        public void PrepareChucDanh(ChucDanhModel model, ChucDanh item)
        {
            item.TEN_CHUC_DANH = model.TEN_CHUC_DANH;
            item.MO_TA = model.MO_TA;
            item.MA_CHUC_DANH = model.MA_CHUC_DANH;
            item.KHOI_DON_VI_ID = model.KHOI_DON_VI_ID;
            item.DINH_MUC = model.DINH_MUC;

        }
            
        public bool CheckMaChucDanh(decimal? id = 0, string ma = null)
        {

            return !_itemService.CheckMaChucDanh(id: id, ma: ma);
        }

        public IList<SelectListItem> PrepareSelectListChucDanh(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn chức danh --", bool isMultiple = false, List<int> valSelecteds = null, decimal? valueExcuted = null, bool IsPhanCap = false)
        {
            var lstNhanXe = _itemService.GetAllChucDanhs();
            // nếu chỉ lấy theo cấp đơn vị khối trung ương, địa phương
            if (IsPhanCap)
            {
                var KhoiDonViHienTai = _donViService.GetDonViById(_workContext.CurrentDonVi.ID)?.LOAI_CAP_DON_VI_ID;
                lstNhanXe = lstNhanXe.Where(c => c.KHOI_DON_VI_ID == KhoiDonViHienTai).ToList();
            }

            var selectList = new List<SelectListItem>();
            if (isMultiple)
            {               
                selectList = lstNhanXe.Select(c => new SelectListItem
                {
                    Text = c.TEN_CHUC_DANH,
                    Value = c.ID.ToString(),
                    Selected = valSelecteds == null ? false : valSelecteds.Contains((int)c.ID)
                }).OrderBy(c => c.Text).ToList();
               
                if (valueExcuted != null || valueExcuted != 0)
                {
                    selectList = selectList.Where(c => c.Value != valueExcuted.ToString()).ToList();
                  
                }
                selectList.AddFirstRow(TextDisplay: strFirstRow, ValueFirst: "");
            }
            else
            {
                selectList = lstNhanXe.Select(c => new SelectListItem
                {
                    Text = c.TEN_CHUC_DANH,
                    Value = c.ID.ToString(),
                    Selected = valSelected == c.ID
                }).OrderBy(c => c.Text).ToList();
                if (isAddFirst)
                    selectList.AddFirstRow(TextDisplay: strFirstRow, ValueFirst: "");
            }
                 
            
           
            return selectList;
        }
        #endregion
    }
}

