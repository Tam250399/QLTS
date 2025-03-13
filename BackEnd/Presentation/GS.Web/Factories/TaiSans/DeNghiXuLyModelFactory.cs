//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/11/2020
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
using GS.Core.Domain.TaiSans;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.TaiSans;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.TaiSans;
using GS.Core.Domain.DanhMuc;
using GS.Services.DanhMuc;

namespace GS.Web.Factories.TaiSans
{
    public class DeNghiXuLyModelFactory : IDeNghiXuLyModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IDeNghiXuLyService _itemService;
        private readonly IDonViService _donViService;
        #endregion

        #region Ctor

        public DeNghiXuLyModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IDeNghiXuLyService itemService,
            IDonViService donViService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._donViService = donViService;
        }
        #endregion

        #region DeNghiXuLy
        public DeNghiXuLySearchModel PrepareDeNghiXuLySearchModel(DeNghiXuLySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public DeNghiXuLyListModel PrepareDeNghiXuLyListModel(DeNghiXuLySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            if (searchModel.PageSize == 0)
                searchModel.PageSize = 10;
            //get items
            var items = _itemService.SearchDeNghiXuLys(Keysearch: searchModel.KeySearch, soPhieu: searchModel.SO_PHIEU, ngayDeNghi: searchModel.NGAY_DE_NGHI,
                ngayXuLy: searchModel.NGAY_XU_LY, donViId: searchModel.DON_VI_ID, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new DeNghiXuLyListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<DeNghiXuLyModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public DeNghiXuLyModel PrepareDeNghiXuLyModel(DeNghiXuLyModel model, DeNghiXuLy item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DeNghiXuLyModel>();
                DonVi donVi = _donViService.GetDonViById(item.DON_VI_ID);
                if (donVi != null)
                {
                    model.MA_DON_VI = donVi.MA;
                    model.TEN_DON_VI = donVi.TEN;
                    model.DON_VI_ID = donVi.ID;
                }
            }
            else
            {
                model.MA_DON_VI = _workContext.CurrentDonVi.MA_DON_VI;
                model.TEN_DON_VI = _workContext.CurrentDonVi.TEN_DON_VI;
                model.DON_VI_ID = _workContext.CurrentDonVi.ID;
                model.NGAY_DE_NGHI = model.NGAY_XU_LY = DateTime.Now;
            }
            //more

            return model;
        }
        public void PrepareDeNghiXuLy(DeNghiXuLyModel model, DeNghiXuLy item)
        {
            item.DON_VI_ID = model.DON_VI_ID;
            item.NGAY_DE_NGHI = model.NGAY_DE_NGHI;
            item.NGAY_XU_LY = model.NGAY_XU_LY;
            item.NOI_DUNG = model.NOI_DUNG;
            item.GHI_CHU = model.GHI_CHU;
            item.SO_PHIEU = model.SO_PHIEU;

        }

        public bool CheckTrungSoPhieuTrongNgay(Decimal ID,String soPhieu, DateTime ngay,Decimal donViId)
        {
            return _itemService.CheckTrungSoPhieuTrongNgay(ID,soPhieu, ngay, donViId);
        }
        #endregion
    }
}

