//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
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
    public class NguonGocTaiSanModelFactory:INguonGocTaiSanModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly INguonGocTaiSanService _itemService;
         #endregion
         
         #region Ctor

        public NguonGocTaiSanModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            INguonGocTaiSanService itemService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
        }
        #endregion
        
        #region NguonGocTaiSan
        public NguonGocTaiSanSearchModel PrepareNguonGocTaiSanSearchModel(NguonGocTaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public NguonGocTaiSanListModel PrepareNguonGocTaiSanListModel(NguonGocTaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchNguonGocTaiSans(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize,ParentId:searchModel.ParentId);
            
            //prepare list model
            var model = new NguonGocTaiSanListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => {
					var res = c.ToModel<NguonGocTaiSanModel>();
					res.CountSub = _itemService.GetCountSub(c.ID);
                    res.IsUsed = _itemService.GetUsed(c.ID);
                    return res;
				}),
                Total = items.TotalCount
            };
            return model;
        }
        
        public NguonGocTaiSanModel PrepareNguonGocTaiSanModel(NguonGocTaiSanModel model, NguonGocTaiSan item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<NguonGocTaiSanModel>();
            }
            //more
            model.NguonGocTaiSanAvailable = PrepareSelectListNguonGocTaiSan(model.PARENT_ID.GetValueOrDefault(0),true,false);
			model.CountSub = _itemService.GetCountSub(model.ID);
			return model;
        }
        
        public void PrepareNguonGocTaiSan(NguonGocTaiSanModel model, NguonGocTaiSan item)
        {
		    item.MA = model.MA;
		    item.TEN = model.TEN;
            item.TREE_LEVEL = model.TREE_LEVEL;
            item.TREE_NODE = model.TREE_NODE;
        }

        public List<SelectListItem> PrepareSelectListNguonGocTaiSan(decimal selected = 0, bool isAddFirst = true,bool isDisable = false,bool isTichThu = true, bool isAll = true)
        {
            string tree = "";
            var ddl = new List<SelectListItem>();
            var listNG = new List<NguonGocTaiSan>();
            if (isAll)
            {
                listNG = _itemService.GetAllNguonGocTaiSans().ToList().OrderBy(c => c.TREE_NODE).ThenBy(c => c.TEN).ToList();
            }
            else
            {
                listNG = _itemService.GetNguonGocTaiSansForDDL(isTichThu).OrderBy(c => c.TREE_NODE).ThenBy(c => c.TEN).ToList();
            }
            if (isDisable)
            {
                ddl = listNG.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = tree.PadLeft((c.TREE_LEVEL.ToNumberInt32() - 1) * 5, '-') + c.MA + " - " + c.TEN,
                    Selected = c.ID == selected ? true : false,
                    Disabled = _itemService.CheckParent(c.ID) //Không cho chọn tài sản cha
                }).ToList();
            }
            else
            {
                ddl = listNG.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = tree.PadLeft((c.TREE_LEVEL.ToNumberInt32() - 1) * 5, '-') + c.MA + " - " + c.TEN,
                    Selected = c.ID == selected ? true : false
                }).ToList();
            }
            ddl.Insert(0, new SelectListItem { Value = "", Text = "Chọn nguồn gốc tài sản" });
            return ddl;
        }
        public String PrepareTenNguonGocByListId(string stringID)
        {
            var tenListTS = "Tất cả";
            if (!string.IsNullOrWhiteSpace(stringID))
            {
                tenListTS = "";
                var listID = stringID.Split(',').Select(c => c.ToNumber()).ToArray();
                tenListTS = string.Join(',', _itemService.GetNguonGocTaiSanByIds(listID).Select(c => c.TEN).ToList());
            }
            return tenListTS;
        }
       public bool CheckMaNguonGocTaiSan(decimal Id, string Ma)
        {
            var nguongocts = _itemService.GetNguonGocTaiSanByMa(Ma);
            if (nguongocts == null)
                return true;
            if (Id > 0 && nguongocts.ID == Id)
                return true;
            return false;
        }
        #endregion
    }
}		

