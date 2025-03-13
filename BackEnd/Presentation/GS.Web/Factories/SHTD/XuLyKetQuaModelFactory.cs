using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.SHTD;
using GS.Services.SHTD;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Models.SHTD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Factories.SHTD
{
    public class XuLyKetQuaModelFactory: IXuLyKetQuaModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IXuLyKetQuaServices _itemService;
        private readonly IXuLyService _xuLyService;
        #endregion

        #region Ctor

        public XuLyKetQuaModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IXuLyKetQuaServices itemService,
            IXuLyService xuLyService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._xuLyService = xuLyService;
        }
        #endregion

        #region XuLyKetQua
        public XuLyKetQuaSearchModel PrepareXuLyKetQuaSearchModel(XuLyKetQuaSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public XuLyKetQuaListModel PrepareXuLyKetQuaListModel(XuLyKetQuaSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchXuLyKetQuas(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new XuLyKetQuaListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareXuLyKetQuaModel(new XuLyKetQuaModel(), c)),
                Total = items.TotalCount
            };
            return model;
        }
        public XuLyKetQuaModel PrepareXuLyKetQuaModel(XuLyKetQuaModel model, XuLyKetQua item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<XuLyKetQuaModel>();
                if (item.xulyTD == null)
                {
                    item.xulyTD= _xuLyService.GetXuLyById((decimal)item.XU_LY_ID);
                }
                model.XuLyTen = item.xulyTD.QUYET_DINH_SO + "(" + item.xulyTD.QUYET_DINH_NGAY.toDateVNString() + ")";                
            }
            return model;
        }
        public void PrepareXuLyKetQua(XuLyKetQuaModel model, XuLyKetQua item)
        {
            item.CHUNG_TU_NOP_TIEN_NGAY = model.CHUNG_TU_NOP_TIEN_NGAY;
            item.CHUNG_TU_NOP_TIEN_SO = model.CHUNG_TU_NOP_TIEN_SO;
            item.GHI_CHU = model.GHI_CHU;
            item.NGAY_NOP_TIEN = model.NGAY_NOP_TIEN;
            item.NGAY_TAO = model.NGAY_TAO;
            item.NGUOI_TAO_ID = model.NGUOI_TAO_ID;
            item.SO_TIEN = model.SO_TIEN;
            item.XU_LY_ID = model.XU_LY_ID;
        }
        #endregion
    }
}
