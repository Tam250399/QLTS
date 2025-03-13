//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 25/6/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.HeThong;

namespace GS.Web.Models.HeThong
{
    [Validator(typeof(ScheduleTaskValidator))]
	public class ScheduleTaskModel :BaseGSEntityModel
	{
        public ScheduleTaskModel(){
        
        }
		public String NAME {get;set;}
		public int SECONDS {get;set;}
		public Boolean? ENABLED {get;set;}
		public Boolean? STOP_OR_ERROR {get;set;}
		public DateTime? LAST_START {get;set;}
		public DateTime? LAST_END {get;set;}
		public DateTime? LAST_SUCCESS {get;set;}
		public String TYPE {get;set;}
	}
    public partial class ScheduleTaskSearchModel :BaseSearchModel {
        public ScheduleTaskSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class ScheduleTaskListModel : BasePagedListModel<ScheduleTaskModel>
    {
       
    }
}

