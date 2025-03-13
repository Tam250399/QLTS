//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.DanhMuc;
using GS.Services;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Framework.Extensions;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.DanhMuc
{
    public class MucDichSuDungModelFactory : IMucDichSuDungModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IMucDichSuDungService _itemService;
        private readonly INhanHienThiService _nhanHienThiService;
        #endregion

        #region Ctor

        public MucDichSuDungModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IMucDichSuDungService itemService,
            INhanHienThiService nhanHienThiService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._nhanHienThiService = nhanHienThiService;
        }
        #endregion

        #region MucDichSuDung
        public MucDichSuDungSearchModel PrepareMucDichSuDungSearchModel(MucDichSuDungSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public MucDichSuDungListModel PrepareMucDichSuDungListModel(MucDichSuDungSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchMucDichSuDungs(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new MucDichSuDungListModel
            {
                //fill in model values from the entity
                //Data = items.Select(c => c.ToModel<MucDichSuDungModel>()),
                Data = items.Select(c =>
                {
                    var m = c.ToModel<MucDichSuDungModel>();
                    m.TenLoaiHinhTaiSan = _nhanHienThiService.GetGiaTriEnum(c.LoaiHinhTaiSan);
                    return m;
                }).ToList(),
                Total = items.TotalCount
            };
            return model;
        }
        public MucDichSuDungModel PrepareMucDichSuDungModel(MucDichSuDungModel model, MucDichSuDung item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<MucDichSuDungModel>();
            }
            //more
            model.LoaiHinhTaiSanAvaliable = model.enumLoaiHinhTaiSan.ToSelectList();
            if (model.ID > 0)
            {
                model.LoaiHinhTaiSanAvaliable = ((enumLOAI_HINH_TAI_SAN)model.LOAI_HINH_TAI_SAN_ID).ToSelectList();
                
            }
            return model;
        }
        public void PrepareMucDichSuDung(MucDichSuDungModel model, MucDichSuDung item)
        {
            item.MA = model.MA;
            item.TEN = model.TEN;
            item.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;

        }
        public IList<SelectListItem> PrepareSelectListMucDichSuDung(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn mục đích sử dụng --", string valueFirstRow = "")
        {
            var lstLoaiTaiSan = _itemService.GetMucDichSuDungs();
            var selectList = lstLoaiTaiSan.Select(c => new SelectListItem
            {
                Text = c.TEN,
                Value = c.ID.ToString(),
                Selected = valSelected == c.ID
            }).ToList();
            if (isAddFirst)
                selectList.AddFirstRow(strFirstRow,valueFirstRow);
            return selectList;
        }
        #endregion
    }
}

