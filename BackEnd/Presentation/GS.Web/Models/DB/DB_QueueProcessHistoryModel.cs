//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 12/12/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.DB;

namespace GS.Web.Models.DB
{
    [Validator(typeof(DB_QueueProcessHistoryValidator))]
	public class DB_QueueProcessHistoryModel :BaseGSEntityModel
	{
        public DB_QueueProcessHistoryModel(){
        
        }
		public DateTime? TIME_REQUEST {get;set;}
		public string RESPONSE {get;set;}
		public Decimal? TRANG_THAI_ID {get;set;}
		public String GUID_RESPONSE {get;set;}
		public Decimal? QUEUE_PROCESS_ID {get;set;}
	}
    public partial class DB_QueueProcessHistorySearchModel :BaseSearchModel {
        public DB_QueueProcessHistorySearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class DB_QueueProcessHistoryListModel : BasePagedListModel<DB_QueueProcessHistoryModel>
    {
       
    }
}

