//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 18/6/2021
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
using GS.Core.Domain.HeThong;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.HeThong;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Services.DanhMuc;
using GS.Web.Factories.DanhMuc;

namespace GS.Web.Factories.HeThong
{
    public class DinhMucChiTietModelFactory:IDinhMucChiTietModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IDinhMucChiTietService _itemService;
            private readonly IChucDanhService _chucDanhService;
            private readonly ILoaiTaiSanService _loaiTaiSanService;
            private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
            private readonly IChucDanhModelFactory _chucDanhModelFactory;
         #endregion
         
         #region Ctor

        public DinhMucChiTietModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IDinhMucChiTietService itemService,
            IChucDanhService chucDanhService,
            ILoaiTaiSanService loaiTaiSanService,
            ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
            IChucDanhModelFactory chucDanhModelFactory
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
            this._chucDanhService = chucDanhService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
            this._chucDanhModelFactory = chucDanhModelFactory;
        }
        #endregion
        
        #region DinhMucChiTiet
        public DinhMucChiTietSearchModel PrepareDinhMucChiTietSearchModel(DinhMucChiTietSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public DinhMucChiTietListModel PrepareDinhMucChiTietListModel(DinhMucChiTietSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchDinhMucChiTiets(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new DinhMucChiTietListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<DinhMucChiTietModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public DinhMucChiTietModel PrepareDinhMucChiTietModel(DinhMucChiTietModel model, DinhMucChiTiet item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DinhMucChiTietModel>();
                model.DDLNhomTaiSan = new List<SelectListItem>();
                model.DDLNhomTaiSan.AddFirstRow("--Chọn loại tài sản");
                model.DDLNhomTaiSan = new List<SelectListItem>();
                model.DDLNhomTaiSan.AddFirstRow("--Chọn loại tài sản");
                if (model.CHUC_DANH_ID > 0)
                {
                    model.TEN_CHUC_DANH = _chucDanhService.GetChucDanhById(model.CHUC_DANH_ID).TEN_CHUC_DANH;
                    model.DDLChucDanh = _chucDanhModelFactory.PrepareSelectListChucDanh(model.CHUC_DANH_ID);
                }
                //model.TEN_NHOM_TAI_SAN = ChiTietDinhMuc.TEN_NHOM_TAI_SAN;
                if (model.LOAI_TAI_SAN_ID > 0) { 
                    model.TEN_NHOM_TAI_SAN = _loaiTaiSanService.GetLoaiTaiSanById(model.LOAI_TAI_SAN_ID).TEN;
                    model.DDLNhomTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(valSelected: model.LOAI_TAI_SAN_ID);
                }

                var loaiHinhTaiSanModels = _loaiTaiSanModelFactory.PrepareListLoaiHinhTaiSanDinhMucModel();
                if (model.LOAI_HINH_TAI_SAN_ID > 0) { 
                    model.DDLloaiHinhTaiSan = loaiHinhTaiSanModels.Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString(),
                        Selected = model.LOAI_HINH_TAI_SAN_ID == c.Id
                    }).OrderBy(c => c.Text).ToList();
                }else
                    model.DDLloaiHinhTaiSan = loaiHinhTaiSanModels.Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString(),
                    }).OrderBy(c => c.Text).ToList();
                model.DDLloaiHinhTaiSan.AddFirstRow("--Chọn loại hình tài sản");
                if (model.LOAI_TAI_SAN_ID > 0)
                {
                    var loaihinhtaisanid = _loaiTaiSanService.GetLoaiTaiSanById(model.LOAI_TAI_SAN_ID).LOAI_HINH_TAI_SAN_ID;
                    model.DDLNhomTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(valSelected: model.LOAI_TAI_SAN_ID);
                }
            }
            //more
            else { 
                model.DDLChucDanh = _chucDanhModelFactory.PrepareSelectListChucDanh();
                model.DDLChucDanh.AddFirstRow("--Chọn chức danh");
                //model.DDLNhomTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan();
                model.DDLNhomTaiSan = new List<SelectListItem>();
                model.DDLNhomTaiSan.AddFirstRow("--Chọn loại tài sản");
                model.DDLloaiHinhTaiSan = _loaiTaiSanModelFactory.PrepareListLoaiHinhTaiSanDinhMucModel().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList();
                model.DDLloaiHinhTaiSan.AddFirstRow("--Chọn loại hình tài sản");
            }
            return model;
        }
       public void PrepareDinhMucChiTiet(DinhMucChiTietModel model, DinhMucChiTiet item)
        {
		item.DINH_MUC_ID = model.DINH_MUC_ID;
		item.LOAI_TAI_SAN_ID = model.LOAI_TAI_SAN_ID;
		item.CHUC_DANH_ID = model.CHUC_DANH_ID;
		item.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;
		item.DINH_MUC = model.DINH_MUC;
		item.SO_LUONG = model.SO_LUONG;
            
        }
        #endregion	
     }
}		

