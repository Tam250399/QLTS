//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/6/2020
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
using GS.Core.Domain.DanhMuc;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.DanhMuc;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Factories.DanhMuc
{
    public class LoaiTaiSanKhauHaoModelFactory:ILoaiTaiSanKhauHaoModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly ILoaiTaiSanKhauHaoService _itemService;
            private readonly ICheDoHaoMonService _chedohaomonService;
            private readonly ILoaiTaiSanService _loaiTaiSanService;
            private readonly INhanHienThiService _nhanhienthiService;
        #endregion

        #region Ctor

        public LoaiTaiSanKhauHaoModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            ILoaiTaiSanKhauHaoService itemService,
            ICheDoHaoMonService chedohaomonService,
            ILoaiTaiSanService loaiTaiSanService,
            INhanHienThiService nhanhienthiService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
            this._chedohaomonService = chedohaomonService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._nhanhienthiService = nhanhienthiService;

        }
        #endregion
        
        #region LoaiTaiSanKhauHao
        public LoaiTaiSanKhauHaoSearchModel PrepareLoaiTaiSanKhauHaoSearchModel(LoaiTaiSanKhauHaoSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            var lstCheDo = _chedohaomonService.GetAllCheDoHaoMons().OrderBy(c => c.ID);
            searchModel.CheDoAvaliable = lstCheDo.Select(c => new SelectListItem
            {
                Text = c.TEN_CHE_DO,
                Value = c.ID.ToString()
            }).ToList();
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public LoaiTaiSanListModel PrepareLoaiTaiSanKhauHaoListModel(LoaiTaiSanKhauHaoSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchLoaiTaiSanKhauHaos(Keysearch: searchModel.KeySearch, ParentId: searchModel.ParentId,
               ChedoId: searchModel.CheDoId, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new LoaiTaiSanListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<LoaiTaiSanModel>();
                    m.CountSub = _loaiTaiSanService.GetCountSub(c.ID);
                    if (c.LOAI_HINH_TAI_SAN_ID != null)
                    {
                        m.TenLoaiHinhTaiSan = _nhanhienthiService.GetGiaTriEnum(c.LoaiHinhTaiSan);
                    }
                    var itemltskhs = _itemService.GetLoaiTaiSanKhauHaoByDonViIdAndLoaiTaiSanId(m.ID,_workContext.CurrentDonVi.ID);
                    if(itemltskhs!= null)
                    {
                        m.KH_TY_LE = itemltskhs.TY_LE_KHAU_HAO;
                        m.KH_THOI_HAN_SU_DUNG = itemltskhs.THOI_HAN_SU_DUNG;
                    }
                    return m;
                }).ToList(),
                Total = items.TotalCount
            };
            //var model = new LoaiTaiSanListModel
            //{
            //    //fill in model values from the entity
            //    Data = items.Select(c => c.ToModel<LoaiTaiSanModel>()),
            //    Total = items.TotalCount
            //};
            return model;
        }
        public LoaiTaiSanKhauHaoModel PrepareLoaiTaiSanKhauHaoModel(LoaiTaiSanKhauHaoModel model, LoaiTaiSanKhauHao item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<LoaiTaiSanKhauHaoModel>();
            }
            //more
           
            return model;
        }
       public void PrepareLoaiTaiSanKhauHao(LoaiTaiSanKhauHaoModel model, LoaiTaiSanKhauHao item)
        {
		item.LOAI_TAI_SAN_ID = model.LOAI_TAI_SAN_ID;
		//item.DON_VI_ID = model.DON_VI_ID;
		item.TY_LE_KHAU_HAO = model.TY_LE_KHAU_HAO;
		item.THOI_HAN_SU_DUNG = model.THOI_HAN_SU_DUNG;
		item.MA_LOAI_TAI_SAN = model.MA_LOAI_TAI_SAN;
		item.MA_DON_VI = model.MA_DON_VI;
            item.TY_LE_KHAU_HAO = model.TY_LE_KHAU_HAO;
        }
        #endregion	
     }
}		

