//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(DiaBanTestValidator))]
	public class DiaBanTestModel :BaseGSEntityModel
	{
        public DiaBanTestModel(){
            QuocGiaList = new List<SelectListItem>();
        }
		public String MA {get;set;}
		public String TEN {get;set;}
		public String MO_TA {get;set;}
		public String TREE_NODE {get;set;}
		public String TREE_LEVEL {get;set;}
		public Decimal QUOC_GIA_ID {get;set;}
        public Decimal? PARENT_ID { get; set; }
        public IList<SelectListItem> QuocGiaList { get; set; }
        public String TEN_QUOC_GIA { get; set; }
        public IList<SelectListItem> DiaBanChaList { get; set; }
    }
    public partial class DiaBanTestSearchModel :BaseSearchModel {
        public DiaBanTestSearchModel()
        {
        }
        public string KeySearch {get;set;}
        public int? pageIndex { get; set; }
        public decimal? PARENTID { get; set; }

    }
    public partial class DiaBanTestListModel : BasePagedListModel<DiaBanTestModel>
    {
       
    }
}

