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
    public class NhanXeModelFactory : INhanXeModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly INhanXeService _itemService;
        #endregion

        #region Ctor

        public NhanXeModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            INhanXeService itemService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region NhanXe
        public NhanXeSearchModel PrepareNhanXeSearchModel(NhanXeSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public NhanXeListModel PrepareNhanXeListModel(NhanXeSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchNhanXes(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new NhanXeListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<NhanXeModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public NhanXeModel PrepareNhanXeModel(NhanXeModel model, NhanXe item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<NhanXeModel>();
            }
            //more

            return model;
        }
        public void PrepareNhanXe(NhanXeModel model, NhanXe item)
        {
            item.MA = model.MA;
            item.TEN = model.TEN;

        }

        public bool CheckMaNhanXe(decimal id = 0, string ma = null)
        {
            return !_itemService.CheckMaNhanXe(id: id, ma: ma);
        }

        public IList<SelectListItem> PrepareSelectListNhanXe(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn nhãn xe --")
        {
            var lstNhanXe = _itemService.GetAllNhanXes();
            var loaiKhac = lstNhanXe.Where(c => c.MA.ToLower().Contains("khac")).FirstOrDefault();
            //// xử lý yêu cầu bắt buộc chọn nhãn, nếu không chọn thì mặc định là loại khác
            //if ((valSelected == 0 || valSelected ==null) && loaiKhac != null)
            //{
            //    valSelected = loaiKhac.ID;
            //}
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

