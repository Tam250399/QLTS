//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/3/2020
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
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Http;

namespace GS.Web.Factories.DanhMuc
{
    public class LoaiTaiSanVoHinhModelFactory:ILoaiTaiSanVoHinhModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly ILoaiTaiSanDonViServices _itemService;
            private readonly ICheDoHaoMonService _chedohaomonService;
            private readonly ILoaiTaiSanService _loaiTaiSanService;
            private readonly IDonViService _donViService;

        #endregion

        #region Ctor

        public LoaiTaiSanVoHinhModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            ILoaiTaiSanDonViServices itemService,
            ICheDoHaoMonService chedohaomonService,
            ILoaiTaiSanService loaitaisanService,
            IDonViService donViService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
            this._chedohaomonService = chedohaomonService;
            this._loaiTaiSanService = loaitaisanService;
            this._donViService = donViService;
        }
        #endregion
        
        #region LoaiTaiSanVoHinh
        public LoaiTaiSanVoHinhSearchModel PrepareLoaiTaiSanVoHinhSearchModel(LoaiTaiSanVoHinhSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.SetGridPageSize();
            return searchModel;
        }

        public LoaiTaiSanVoHinhListModel PrepareLoaiTaiSanVoHinhListModel(LoaiTaiSanVoHinhSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchLoaiTaiSanVoHinhs(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, PARENTID: searchModel.PARENTID, pageSize:searchModel.PageSize, forDonVi: searchModel.forDonVi, TREELEVEL: searchModel.TREELEVEL);

            //prepare list model
            var model = new LoaiTaiSanVoHinhListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<LoaiTaiSanVoHinhModel>();
                    m.CountSub = _itemService.GetCountSub(ParentId:c.ID,donViId:searchModel.forDonVi);
                    return m;
                }),
                Total = items.TotalCount
            };
            return model;
        }
        public LoaiTaiSanVoHinhModel PrepareLoaiTaiSanVoHinhModel(LoaiTaiSanVoHinhModel model, LoaiTaiSanDonVi item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<LoaiTaiSanVoHinhModel>();
                if(model.PARENT_ID > 0)
                {
                    var parent = _itemService.GetLoaiTaiSanVoHinhById((decimal)model.PARENT_ID);
                    model.TenLoaiTaiSanVoHinhCha = parent.MA + " - " + parent.TEN;
                }
				if (model.DON_VI_ID >0)
				{
                    var donvi = _donViService.GetDonViById(model.DON_VI_ID??0);
					if (donvi!= null)
					{
                        model.TenDonViApDung = donvi.TEN;
                    }
				}
            }
            model.DanhSachLoaiHinhTaiSan = PrepareSelectListLoaiTaiSanDonVi(valSelected:model.PARENT_ID,isAddFirst:true, strFirstRow:"--Chọn loại tài sản cha",donViId:model.DON_VI_ID);
            return model;
        }
       public void PrepareLoaiTaiSanDonVi(LoaiTaiSanVoHinhModel model, LoaiTaiSanDonVi item)
        {
		    item.MA = model.MA;
		    item.TEN = model.TEN;
		    item.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;
		    item.HM_THOI_HAN_SU_DUNG = model.HM_THOI_HAN_SU_DUNG;
		    item.HM_TY_LE = model.HM_TY_LE;
		    item.KH_THOI_HAN_SU_DUNG = model.KH_THOI_HAN_SU_DUNG;
		    item.KH_TY_LE = model.KH_TY_LE;
		    item.MO_TA = model.MO_TA;
		    item.CHE_DO_HAO_MON_ID = model.CHE_DO_HAO_MON_ID;
		    item.PARENT_ID = model.PARENT_ID;
		    item.TREE_NODE = model.TREE_NODE;
		    item.TREE_LEVEL = model.TREE_LEVEL;
		    item.DON_VI_TINH = model.DON_VI_TINH;
		    item.DON_VI_ID = model.DON_VI_ID;
        }

        public bool CheckMaLoaiTaiSanDonVi(decimal id, string Ma)
        {
            var loaiTaiSanVoHinh = _itemService.GetLoaiTaiSanVoHinhByMaAndDonVi(Ma);
            if (loaiTaiSanVoHinh == null)
            {
                return true;
            }
            if(id > 0 && loaiTaiSanVoHinh.ID == id)
            {
                return true;
            }
            return false;
        }

        public IList<SelectListItem> PrepareSelectListLoaiTaiSanDonVi(decimal? valSelected = 0, decimal? cheDoId = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn loại tài sản --", decimal? loaiHinhTaiSanId = 0, string valueFirstRow = "", decimal? donViId = 0)
        {
            string tree = "";
            var donViLonNhat = _donViService.GetDonViLonNhat(donViId??0);
            var lstLoaiTaiSan = _itemService.GetListLoaiTaiSanVoHinhTreeNodeByRoot(cheDoHaoMonId: cheDoId, LoaiHinhTaiSanId: loaiHinhTaiSanId, donViId:donViLonNhat?.ID);
            var selectList = lstLoaiTaiSan.Where(c => _itemService.GetCountSub(c.ID, donViLonNhat?.ID) == 0).OrderBy(c =>c.TREE_LEVEL).OrderBy(c => c.TREE_NODE).Select(c => new SelectListItem
            {
                Text = tree.PadLeft((c.TREE_LEVEL.ToNumberInt32() - 1) * 3, '-') + " - " + c.MA + " - " + tree.PadLeft((c.TREE_LEVEL.ToNumberInt32() - 1) * 5, '-') + c.TEN,
                Value = c.ID.ToString(),
                Selected = valSelected == c.ID,
                //Disabled = _itemService.GetCountSub(c.ID,c.DON_VI_ID) > 0 ? true : false,
            }).ToList();
            if (isAddFirst)
                selectList.AddFirstRow(strFirstRow, valueFirstRow);
            return selectList;
        }
        #endregion
    }
}		

