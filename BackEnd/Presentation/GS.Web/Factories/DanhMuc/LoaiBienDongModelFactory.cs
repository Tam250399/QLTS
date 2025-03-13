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
    public class LoaiBienDongModelFactory : ILoaiBienDongModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ILoaiBienDongService _itemService;
        #endregion

        #region Ctor

        public LoaiBienDongModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            ILoaiBienDongService itemService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region LoaiBienDong
        public LoaiBienDongSearchModel PrepareLoaiBienDongSearchModel(LoaiBienDongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public LoaiBienDongListModel PrepareLoaiBienDongListModel(LoaiBienDongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchLoaiBienDongs(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new LoaiBienDongListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<LoaiBienDongModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public LoaiBienDongModel PrepareLoaiBienDongModel(LoaiBienDongModel model, LoaiBienDong item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<LoaiBienDongModel>();
            }
            //more

            return model;
        }
        public void PrepareLoaiBienDong(LoaiBienDongModel model, LoaiBienDong item)
        {
            item.TEN = model.TEN;
            item.MO_TA = model.MO_TA;

        }
        #endregion
    }
}

