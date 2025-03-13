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
    public class NhatKyModelFactory : INhatKyModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly INhatKyService _itemService;
        #endregion

        #region Ctor

        public NhatKyModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            INhatKyService itemService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region NhatKy
        public NhatKySearchModel PrepareNhatKySearchModel(NhatKySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public NhatKyListModel PrepareNhatKyListModel(NhatKySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchNhatKys(Keysearch: searchModel.KeySearch, Username: searchModel.Username, Fromdate: searchModel.Fromdate, Todate: searchModel.Todate, CAP_DO: searchModel.CAP_DO, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);
            //prepare list model
            var model = new NhatKyListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<NhatKyModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public NhatKyModel PrepareNhatKyModel(NhatKyModel model, NhatKy item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = model ?? item.ToModel<NhatKyModel>();
            }
            //more

            return model;
        }
        public void PrepareNhatKy(NhatKyModel model, NhatKy item)
        {
            item.TEN_DANG_NHAP = model.TEN_DANG_NHAP;
            item.MO_TA = model.MO_TA;
            item.NGAY_TAO = model.NGAY_TAO;
            item.TEN_DAY_DU = model.TEN_DAY_DU;
            item.DU_LIEU = model.DU_LIEU;
            item.PHAN_LOAI = model.PHAN_LOAI;
            item.IP_ADDRESS = model.IP_ADDRESS;
            item.GUID = model.GUID;
            item.UNG_DUNG = model.UNG_DUNG;
            item.PAGE_URL = model.PAGE_URL;
            item.CAP_DO_ID = model.CAP_DO;
            item.ID = model.ID;

        }
        #endregion
    }
}

