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
    [Validator(typeof(VaiTroWidgetValidator))]
	public class VaiTroWidgetModel :BaseGSEntityModel
	{
        public VaiTroWidgetModel(){
        
        }
		public Decimal VAI_TRO_ID {get;set;}
		public Decimal WIDGET_ID {get;set;}
	}
    public partial class VaiTroWidgetSearchModel :BaseSearchModel {
        public VaiTroWidgetSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class VaiTroWidgetListModel : BasePagedListModel<VaiTroWidgetModel>
    {
       
    }
}

