//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.HeThong;
using GS.Services.HeThong;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Models.HeThong;
using System;
using System.Linq;

namespace GS.Web.Factories.HeThong
{
    public class NhanHienThiModelFactory : INhanHienThiModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly INhanHienThiService _itemService;
        private readonly INhanHienThiService _NhanHienThiService;
        #endregion

        #region Ctor

        public NhanHienThiModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            INhanHienThiService NhanHienThiService,
            IStaticCacheManager staticCacheManager,
            INhanHienThiService itemService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._NhanHienThiService = NhanHienThiService;
        }
        #endregion

        #region NhanHienThi
        public NhanHienThiSearchModel PrepareNhanHienThiSearchModel(NhanHienThiSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public NhanHienThiListModel PrepareNhanHienThiListModel(NhanHienThiSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchNhanHienThis(Ma: searchModel.MA, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, giaTri: searchModel.GIA_TRI);

            //prepare list model
            var model = new NhanHienThiListModel
            {
                //fill in model values from the entity
                //Data = items.Select(c => c.ToModel<NhanHienThiModel>()),
                Data = items.Select(c => c.ToModel<NhanHienThiModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public NhanHienThiModel PrepareNhanHienThiModel(NhanHienThiModel model, NhanHienThi item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = model ?? item.ToModel<NhanHienThiModel>();
            }
            //more

            return model;
        }
        public void PrepareNhanHienThi(NhanHienThiModel model, NhanHienThi item)
        {
            item.MA = model.MA;
            item.GIA_TRI = model.GIA_TRI;

        }

        public bool CheckTrungMa(string Ma, decimal id = 0)
        {
            var obj = _itemService.GetAllNhanHienThis().Where(c => c.MA.Equals(Ma.ToLower())).FirstOrDefault();
            if (id == 0)
            {
                if (obj != null)
                {
                    return false;
                    //return View(model);
                }
            }
            else
            {
                if (obj != null && obj.ID != id)
                {
                    return false;
                    //return View(model);
                }
            }
            return true;
        }
        #endregion
    }
}

