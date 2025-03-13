//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Factories.DanhMuc
{
    public class HinhThucXuLyModelFactory : IHinhThucXuLyModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IHinhThucXuLyService _itemService;
        private readonly IPhuongAnXuLyService _phuongAnXuLyService;
        #endregion

        #region Ctor

        public HinhThucXuLyModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IHinhThucXuLyService itemService,
            IPhuongAnXuLyService phuongAnXuLyService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._phuongAnXuLyService = phuongAnXuLyService;
        }
        #endregion

        #region HinhThucXuLy
        public HinhThucXuLySearchModel PrepareHinhThucXuLySearchModel(HinhThucXuLySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public HinhThucXuLyListModel PrepareHinhThucXuLyListModel(HinhThucXuLySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchHinhThucXuLys(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new HinhThucXuLyListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => {
                    var htxl = c.ToModel<HinhThucXuLyModel>();
                    htxl.TEN_PHUONG_AN_XU_LY = _phuongAnXuLyService.GetPhuongAnXuLyById(htxl.PHUONG_AN_XU_LY_ID ?? 0).TEN;
                    return htxl;
                }),
                Total = items.TotalCount
            };
            return model;
        }
        public HinhThucXuLyModel PrepareHinhThucXuLyModel(HinhThucXuLyModel model, HinhThucXuLy item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<HinhThucXuLyModel>();
            }
            //more
            model.DDLPhuongAnXuLy = _phuongAnXuLyService.GetPhuongAnXuLys().Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.TEN
            }).ToList();
            return model;
        }
        public void PrepareHinhThucXuLy(HinhThucXuLyModel model, HinhThucXuLy item)
        {
            item.MA = model.MA;
            item.TEN = model.TEN;
            item.PHUONG_AN_XU_LY_ID = model.PHUONG_AN_XU_LY_ID;
            item.SAP_XEP = model.SAP_XEP;

        }
        public List<SelectListItem> DDLPhuongAnByHinhThuc(int HinhThucId = 0)
        {
            var ddl = new List<SelectListItem>();
            if (HinhThucId > 0)
            {
                ddl = _itemService.GetHinhThucXuLys(PhuongAnId: HinhThucId).Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.TEN }).ToList();
            }
            ddl.Insert(0, new SelectListItem { Value = "0", Text = "Chọn hình thức" });
            return ddl;
        }
        #endregion
        public IList<SelectListItem> PrepareSelectListHinhThucXuLy(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "Tất cả")
        {
            var lstHinhThucXuLy = _itemService.GetAllHinhThucXuLys();
           
            var selectList = lstHinhThucXuLy.Select(c => new SelectListItem
            {
                Text = c.TEN,
                Value = c.ID.ToString(),
                Selected = valSelected == c.ID
            }).ToList();
            if (isAddFirst)
                selectList.AddFirstRow(TextDisplay: strFirstRow, ValueFirst: "");
            return selectList;
        }
    }
}

