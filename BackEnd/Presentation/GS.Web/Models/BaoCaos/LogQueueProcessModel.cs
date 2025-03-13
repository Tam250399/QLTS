//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 28/6/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.BaoCaos;

namespace GS.Web.Models.BaoCaos
{
    [Validator(typeof(LogQueueProcessValidator))]
	public class LogQueueProcessModel :BaseGSEntityModel
	{
        public LogQueueProcessModel(){
        
        }
		public Decimal? STATUS {get;set;}
		public String ACTION {get;set;}
		public String OUTPUT {get;set;}
		public DateTime? TIME_START_SCAN {get;set;}
		public DateTime? TIME_STOP_SCAN {get;set;}
		public Decimal? QUEUE_ID {get;set;}
	}
    public partial class LogQueueProcessSearchModel :BaseSearchModel {
        public LogQueueProcessSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class LogQueueProcessListModel : BasePagedListModel<LogQueueProcessModel>
    {
       
    }
}

