//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Linq;
using GS.Core;
using GS.Core.Caching;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Services.DanhMuc;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Factories.DanhMuc
{
    public class BaoCaoModelFactory:IBaoCaoModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IBaoCaoService _itemService;
         #endregion
         
         #region Ctor

        public BaoCaoModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IBaoCaoService itemService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
        }
        #endregion
        
        #region BaoCao
        public BaoCaoSearchModel PrepareBaoCaoSearchModel(BaoCaoSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public BaoCaoListModel PrepareBaoCaoListModel(BaoCaoSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchBaoCaos(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new BaoCaoListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<BaoCaoModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public BaoCaoModel PrepareBaoCaoModel(BaoCaoModel model, GS.Core.Domain.DanhMuc.BaoCao item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<BaoCaoModel>();
            }
            //more
            model.DDLTrueOrFalse.Insert(0, new SelectListItem { Text = "Không", Value = "0" });
            model.DDLTrueOrFalse.Insert(1, new SelectListItem { Text = "Có", Value = "1" });

            return model;
        }
        public void PrepareBaoCao(BaoCaoModel model, GS.Core.Domain.DanhMuc.BaoCao item)
        {
            item.MA_BAO_CAO = model.MA_BAO_CAO;
            item.NOI_DUNG = model.NOI_DUNG;
            item.HAS_ROW_ID = model.HAS_ROW_ID;
            item.HAS_COL_ID = model.HAS_COL_ID;
        }

        public bool CheckMaBaoCao(decimal id = 0, string ma = null)
        {
            return !_itemService.CheckMaBaoCao(id: id, ma: ma);
        }
        #endregion	
    }
}		

