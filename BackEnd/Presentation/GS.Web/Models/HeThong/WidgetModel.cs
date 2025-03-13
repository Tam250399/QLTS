//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 20/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.HeThong;

namespace GS.Web.Models.HeThong
{
    [Validator(typeof(WidgetValidator))]
	public class WidgetModel :BaseGSEntityModel
	{
        public WidgetModel(){
        
        }
		public String TEN {get;set;}
		public String CAU_HINH {get;set;}
		public String MO_TA {get;set;}
        public String PARTIAL_VIEW_NAME { get; set; }
    }
    public partial class WidgetSearchModel :BaseSearchModel {
        public WidgetSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class WidgetListModel : BasePagedListModel<WidgetModel>
    {
       
    }
}

