//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(BaoCaoValidator))]
	public class BaoCaoModel :BaseGSEntityModel
	{
        public BaoCaoModel()
        {
            DDLTrueOrFalse = new List<SelectListItem>();
        }
		public String MA_BAO_CAO {get;set;}
		public String NOI_DUNG {get;set;}
		public Decimal? HAS_ROW_ID {get;set;}
		public Decimal? HAS_COL_ID {get;set;}
        public List<SelectListItem> DDLTrueOrFalse { get; set; }
	}
    public partial class BaoCaoSearchModel :BaseSearchModel {
        public BaoCaoSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class BaoCaoListModel : BasePagedListModel<BaoCaoModel>
    {
       
    }
}

