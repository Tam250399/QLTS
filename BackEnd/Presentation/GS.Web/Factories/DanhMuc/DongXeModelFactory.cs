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
    public class DongXeModelFactory : IDongXeModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IDongXeService _itemService;
        private readonly INhanXeModelFactory _itemNhanXeModelFactory;
        #endregion

        #region Ctor

        public DongXeModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IDongXeService itemService,
            INhanXeModelFactory itemNhanXeModelFactory
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._itemNhanXeModelFactory = itemNhanXeModelFactory;
        }
        #endregion

        #region DongXe
        public DongXeSearchModel PrepareDongXeSearchModel(DongXeSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public DongXeListModel PrepareDongXeListModel(DongXeSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchDongXes(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new DongXeListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                    {
                        var m = c.ToModel<DongXeModel>();
                        m.TEN_NHAN_XE = c.NhanXe != null ? c.NhanXe.TEN : String.Empty;
                        m.pageIndex = searchModel.Page;
                        return m;
                    }),
                Total = items.TotalCount
            };
            return model;
        }
        public DongXeModel PrepareDongXeModel(DongXeModel model, DongXe item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DongXeModel>();
            }
            //more
            model.dllNhanXe = _itemNhanXeModelFactory.PrepareSelectListNhanXe(isAddFirst: true);

            return model;
        }
        public void PrepareDongXe(DongXeModel model, DongXe item)
        {
            item.MA = model.MA;
            item.TEN = model.TEN;
            item.MO_TA = model.MO_TA;
            item.NHAN_XE_ID = model.NHAN_XE_ID;

        }

        public bool CheckMaDongXe(decimal id = 0, string ma = null)
        {
            return !_itemService.CheckMaDongXe(id: id, ma: ma);
        }
        public IList<SelectListItem> PrepareSelectDongXe(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn dòng xe --", decimal? nhanXeId = 0)
        {
            var lstNhanXe = _itemService.GetListDongXes(nhanXeId:nhanXeId);
            var selectList = lstNhanXe.Select(c => new SelectListItem
            {
                Text = c.TEN,
                Value = c.ID.ToString(),
                Selected = valSelected == c.ID
            }).ToList();
            if (isAddFirst)
                selectList.AddFirstRow(TextDisplay: strFirstRow, ValueFirst: "");
            return selectList;
        }
        #endregion
    }
}

