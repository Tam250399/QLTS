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
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.HeThong
{
    public class QuyenModelFactory : IQuyenModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IQuyenService _itemService;
        private readonly IQuyenVaiTroService _quyenVaiTroService;
        #endregion

        #region Ctor

        public QuyenModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IQuyenService itemService,
            IQuyenVaiTroService quyenVaiTroService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._quyenVaiTroService = quyenVaiTroService;
        }
        #endregion

        #region Quyen
        public QuyenSearchModel PrepareQuyenSearchModel(QuyenSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.ListQuyenDaChon.Count();
            searchModel.SetGridPageSize();
            return searchModel;
        }

        public QuyenListModel PrepareQuyenListModel(QuyenSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            //get items
            var items = _itemService.SearchQuyens(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, ListQuyenDaChon: searchModel.ListQuyenDaChon, idVaiTro: searchModel.idVaiTro);
            if (searchModel.idVaiTro != 0)
            {
                var lstquyenchon = _quyenVaiTroService.GetMapByVaiTroId(searchModel.idVaiTro).Select(c => c.QUYEN_ID).ToList();
                var model = new QuyenListModel
                {
                    Data = items.Select(c => PrepareQuyenModel(new QuyenModel(), c, false, lstquyenchon)),
                    Total = items.TotalCount
                };
                return model;
            }
            //prepare list model
            else
            {
                var model = new QuyenListModel
                {
                    //fill in model values from the entity
                    Data = items.Select(c => c.ToModel<QuyenModel>()),
                    Total = items.TotalCount
                };
                return model;
            }
        }
        public QuyenModel PrepareQuyenModel(QuyenModel model, Quyen item, bool excludeProperties = false, List<decimal> lstquyenchon = null)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<QuyenModel>();
            }
            //more
            if (lstquyenchon != null)
            {
                if (lstquyenchon.Where(c => c == item.ID).Count() > 0)
                {
                    model.IdChon = true;
                }
            }
            return model;
        }
        public void PrepareQuyen(QuyenModel model, Quyen item)
        {
            item.MA = model.MA;
            item.TEN = model.TEN;
            item.PHAN_HE = model.PHAN_HE;
            item.ID = model.ID;

        }

        public bool CheckMaQuyen(string MA, decimal id = 0)
        {
            return !_itemService.KiemTraTrungMa(MA, id);
        }
        #endregion
    }
}

