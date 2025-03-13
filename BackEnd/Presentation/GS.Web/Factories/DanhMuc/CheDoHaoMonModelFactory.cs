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
using GS.Web.Models.DanhMuc;
using System;
using System.Linq;

namespace GS.Web.Factories.DanhMuc
{
    public class CheDoHaoMonModelFactory : ICheDoHaoMonModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ICheDoHaoMonService _itemService;
        #endregion

        #region Ctor

        public CheDoHaoMonModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            ICheDoHaoMonService itemService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region CheDoHaoMon
        public CheDoHaoMonSearchModel PrepareCheDoHaoMonSearchModel(CheDoHaoMonSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public CheDoHaoMonListModel PrepareCheDoHaoMonListModel(CheDoHaoMonSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchCheDoHaoMons(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new CheDoHaoMonListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<CheDoHaoMonModel>()),
                Total = items.TotalCount
            };
            return model;

        }
        public bool CheckMaCheDoHaoMon(decimal id = 0, string ma = null)
        {
            return !_itemService.CheckMaCheDoHaoMon(id: id, ma: ma);
        }
        public CheDoHaoMonModel PrepareCheDoHaoMonModel(CheDoHaoMonModel model, CheDoHaoMon item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<CheDoHaoMonModel>();
            }
            //more

            return model;
        }
        public void PrepareCheDoHaoMon(CheDoHaoMonModel model, CheDoHaoMon item)
        {
            item.MA_CHE_DO = model.MA_CHE_DO;
            item.TEN_CHE_DO = model.TEN_CHE_DO;
            item.TU_NGAY = model.TU_NGAY;
            item.DEN_NGAY = model.DEN_NGAY;

        }
        #endregion
    }
}

