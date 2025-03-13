//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 25/6/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.HeThong
{
	public partial class ScheduleTask : BaseEntity
	{
		public String NAME {get;set;}
		public int SECONDS {get;set;}
		public Boolean? ENABLED {get;set;}
		public Boolean? STOP_OR_ERROR {get;set;}
		public DateTime? LAST_START {get;set;}
		public DateTime? LAST_END {get;set;}
		public DateTime? LAST_SUCCESS {get;set;}
		public String TYPE {get;set;}
		
	}
}



