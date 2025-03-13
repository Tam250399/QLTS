//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.DanhMuc;
using GS.Services.DanhMuc;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Framework.Extensions;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.DanhMuc
{
    public class HinhThucMuaSamModelFactory : IHinhThucMuaSamModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IHinhThucMuaSamService _itemService;
        #endregion

        #region Ctor

        public HinhThucMuaSamModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IHinhThucMuaSamService itemService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region HinhThucMuaSam
        public HinhThucMuaSamSearchModel PrepareHinhThucMuaSamSearchModel(HinhThucMuaSamSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public HinhThucMuaSamListModel PrepareHinhThucMuaSamListModel(HinhThucMuaSamSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchHinhThucMuaSams(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new HinhThucMuaSamListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<HinhThucMuaSamModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public HinhThucMuaSamModel PrepareHinhThucMuaSamModel(HinhThucMuaSamModel model, HinhThucMuaSam item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<HinhThucMuaSamModel>();
            }
            //more

            return model;
        }
        public void PrepareHinhThucMuaSam(HinhThucMuaSamModel model, HinhThucMuaSam item)
        {
            item.MA = model.MA;
            item.TEN = model.TEN;

        }
        public IList<SelectListItem> PrepareSelectListHinhThucMuaSam(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn hình thức mua sắm --", string valueFirstRow = "")
        {
            var lstLoaiTaiSan = _itemService.GetHinhThucMuaSams();
            var selectList = lstLoaiTaiSan.Select(c => new SelectListItem
            {
                Text = c.TEN,
                Value = c.ID.ToString(),
                Selected = valSelected == c.ID
            }).ToList();
            if (isAddFirst)
                selectList.AddFirstRow(strFirstRow,valueFirstRow);
            return selectList;
        }
        public bool CheckMaLoaiHinhThucMuaSam(decimal id = 0, string ma = null)
        {
            return !_itemService.CheckMaHinhThucMuaSam(id: id, ma: ma);
        }
        #endregion
    }
}

