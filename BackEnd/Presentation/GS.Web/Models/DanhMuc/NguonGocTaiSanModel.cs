//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Core.Domain.DanhMuc;
using GS.Web.Framework.Models;
using GS.Web.Validators.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(NguonGocTaiSanValidator))]
	public class NguonGocTaiSanModel :BaseGSEntityModel
	{
        public NguonGocTaiSanModel(){
            IsUsed = false;
        }
		public String MA {get;set;}
		public String TEN {get;set;}
        public Decimal? PARENT_ID { get; set; }
        public String TREE_NODE { get; set; }
        public Decimal? TREE_LEVEL { get; set; }
        public List<SelectListItem> NguonGocTaiSanAvailable { get; set; }
        public int? DB_ID { get; set; }
		public int CountSub { get; set; }
        public bool IsUsed { get; set; }
    }
    public partial class NguonGocTaiSanSearchModel :BaseSearchModel {
        public NguonGocTaiSanSearchModel()
        {
        }
        public string KeySearch {get;set;}
		public decimal? ParentId { get; set; }
	}
   public partial class NguonGocTaiSanListModel : BasePagedListModel<NguonGocTaiSanModel>
    {
       
    }
}

