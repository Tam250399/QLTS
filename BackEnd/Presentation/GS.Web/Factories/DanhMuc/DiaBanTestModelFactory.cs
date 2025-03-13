//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/3/2020
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
    public class DiaBanTestModelFactory:IDiaBanTestModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IQuocGiaService _quocGiaService;
            private readonly IDiaBanTestService _itemService;
         #endregion
         
         #region Ctor

        public DiaBanTestModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IQuocGiaService quocGiaService,
            IDiaBanTestService itemService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._quocGiaService = quocGiaService;
            this._itemService = itemService;
        }
        #endregion
        
        #region DiaBanTest
        public DiaBanTestSearchModel PrepareDiaBanTestSearchModel(DiaBanTestSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public DiaBanTestListModel PrepareDiaBanTestListModel(DiaBanTestSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchDiaBanTests(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize, PARENTID: searchModel.PARENTID);
            
            //prepare list model
            var model = new DiaBanTestListModel
            {
                //fill in model values from the entity
                Data = items.Select( c =>
                {
                    var m = c.ToModel<DiaBanTestModel>();
                    m.TEN_QUOC_GIA = c.QuocGia.TEN;
                    return m;
                }),
                Total = items.TotalCount
            };
            return model;
        }
        public DiaBanTestModel PrepareDiaBanTestModel(DiaBanTestModel model, DiaBanTest item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DiaBanTestModel>();
            }
            model.DiaBanChaList = _itemService.GetAllDiaBanTests().Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.TEN.ToString(),
                Selected = c.ID == model.PARENT_ID
            }).ToList();
            model.DiaBanChaList.AddFirstRow("-- Chọn địa bàn cha --", "");
            model.QuocGiaList = _quocGiaService.GetAllQuocGias().Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.TEN,
                Selected = c.ID == model.QUOC_GIA_ID
            }).ToList();
            return model;
        }

        public bool CheckMaDiaBanTest(decimal id, string Ma)
        {
            var diaBanTest = _itemService.GetDiaBanTestByMa(Ma);
            if(diaBanTest == null)
            {
                return true;
            }
            if (id >= 0 && diaBanTest.ID == id)
            {
                return true;
            }
            return false;
        }

       public void PrepareDiaBanTest(DiaBanTestModel model, DiaBanTest item)
        {
		    item.MA = model.MA;
		    item.TEN = model.TEN;
		    item.MO_TA = model.MO_TA;
		    item.TREE_NODE = model.TREE_NODE;
		    item.TREE_LEVEL = model.TREE_LEVEL;
		    item.QUOC_GIA_ID = model.QUOC_GIA_ID;
            item.PARENT_ID = model.PARENT_ID;
        }
        #endregion	
     }
}		

