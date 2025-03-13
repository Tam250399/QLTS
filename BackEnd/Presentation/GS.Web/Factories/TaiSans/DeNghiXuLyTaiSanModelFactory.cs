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
using GS.Services.BienDongs;
using GS.Core.Domain.BienDongs;
using GS.Web.Factories.DanhMuc;
using GS.Core.Domain.DanhMuc;

namespace GS.Web.Factories.TaiSans
{
    public class DeNghiXuLyTaiSanModelFactory : IDeNghiXuLyTaiSanModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IDeNghiXuLyTaiSanService _itemService;
        private readonly ITaiSanService _taiSanService;
        private readonly IBienDongService _biendongService;
        private readonly ILyDoBienDongModelFactory _lyDoBienDongModelFactory;
        #endregion

        #region Ctor

        public DeNghiXuLyTaiSanModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IDeNghiXuLyTaiSanService itemService,
            ITaiSanService taiSanService,
            IBienDongService biendongService,
            ILyDoBienDongModelFactory lyDoBienDongModelFactory
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._taiSanService = taiSanService;
            this._biendongService = biendongService;
            _lyDoBienDongModelFactory = lyDoBienDongModelFactory;
        }
        #endregion

        #region DeNghiXuLyTaiSan
        public DeNghiXuLyTaiSanSearchModel PrepareDeNghiXuLyTaiSanSearchModel(DeNghiXuLyTaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public DeNghiXuLyTaiSanListModel PrepareDeNghiXuLyTaiSanListModel(DeNghiXuLyTaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            if (searchModel.PageSize == 0)
                searchModel.PageSize = 10;
            //get items
            var items = _itemService.SearchDeNghiXuLyTaiSans(Keysearch: searchModel.KeySearch, DE_NGHI_XU_LY_ID: searchModel.DE_NGHI_XU_LY_ID, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new DeNghiXuLyTaiSanListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareDeNghiXuLyTaiSanModelForList(c)),
                Total = items.TotalCount
            };
            return model;
        }

        public DeNghiXuLyTaiSanModel PrepareDeNghiXuLyTaiSanModelForList(DeNghiXuLyTaiSan item)
        {
            DeNghiXuLyTaiSanModel model = new DeNghiXuLyTaiSanModel();
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DeNghiXuLyTaiSanModel>();

                TaiSan taiSan = _taiSanService.GetTaiSanById(item.TAI_SAN_ID);
                model.MA_TAI_SAN = taiSan.MA;
                model.TEN_TAI_SAN = taiSan.TEN;
                model.TAI_SAN_NGAY_SU_DUNG = taiSan.NGAY_SU_DUNG;
                model.TAI_SAN_NGUYEN_GIA = _biendongService.TinhNguyenGiaTaiSan(null, null, taiSan.ID).ToVNStringNumber();
                model.TAI_SAN_GTCL = _taiSanService.Get_GTLC_Cua_TS(taiSan.ID, DateTime.Now, true).ToVNStringNumber(); //_biendongService.getGiaTriConLai(taiSan.ID).ToVNStringNumber();

            }
            //more

            return model;
        }
        public DeNghiXuLyTaiSanModel PrepareDeNghiXuLyTaiSanModel(DeNghiXuLyTaiSanModel model, DeNghiXuLyTaiSan item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DeNghiXuLyTaiSanModel>();
                TaiSan taiSan = _taiSanService.GetTaiSanById(item.TAI_SAN_ID);
                model.MA_TAI_SAN = taiSan.MA;
                model.TEN_TAI_SAN = taiSan.TEN;
            }
            model.ListLyDo = _lyDoBienDongModelFactory.PrepareSelectListLyDoBienDong( loailydoId: (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO, isAddFirst: true, valSelected: model.LY_DO_BIEN_DONG_ID, strFirstRow: "-- Chọn phương thức xử lý --");
            //more

            return model;
        }
        public void PrepareDeNghiXuLyTaiSan(DeNghiXuLyTaiSanModel model, DeNghiXuLyTaiSan item)
        {
            item.DE_NGHI_XU_LY_ID = model.DE_NGHI_XU_LY_ID;
            item.TAI_SAN_ID = model.TAI_SAN_ID;
            item.PHUONG_THUC_XU_LY = model.PHUONG_THUC_XU_LY;

        }
        #endregion
    }
}

