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
    public class QuocGiaModelFactory : IQuocGiaModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IQuocGiaService _itemService;
        #endregion

        #region Ctor

        public QuocGiaModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IQuocGiaService itemService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region QuocGia
        public QuocGiaSearchModel PrepareQuocGiaSearchModel(QuocGiaSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public QuocGiaListModel PrepareQuocGiaListModel(QuocGiaSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchQuocGias(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new QuocGiaListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => {
                    var m = c.ToModel<QuocGiaModel>();
                    m.pageIndex = searchModel.Page;
                    return m;
                }).ToList(),
                Total = items.TotalCount
            };
            return model;
        }
        public QuocGiaModel PrepareQuocGiaModel(QuocGiaModel model, QuocGia item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<QuocGiaModel>();
            }
            //more

            return model;
        }
        public void PrepareQuocGia(QuocGiaModel model, QuocGia item)
        {
            item.MA = model.MA;
            item.TEN = model.TEN;
            item.MO_TA = model.MO_TA;
        }

        public bool CheckMaQuocGia(string ma = null, decimal id = 0)
        {
            return !_itemService.CheckMaQuocGia(ma: ma, id: id);
        }
        public IList<SelectListItem> PrepareSelectListQuocGias(decimal? valSelected = 0, bool IsAddFirst = false, string strFirstRow = "-- Chọn Quốc gia --", string valueFirstRow = "")
        {
            var selectList = _itemService.GetAllQuocGias().Select(c => new SelectListItem
            {
                Text = c.TEN,
                Value = c.ID.ToString(),
                Selected = valSelected== c.ID
            }).OrderBy(c=>c.Text).ToList();
            if (IsAddFirst)
            {
                selectList.AddFirstRow(strFirstRow, valueFirstRow);
            }
            return selectList;
        }
        #endregion
    }
}

