//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 12/12/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.DB
{
	public partial class DB_QueueProcessHistory : BaseEntity
	{
		public DateTime? TIME_REQUEST {get;set;}
		public string RESPONSE {get;set;}
		public Decimal? TRANG_THAI_ID {get;set;}
		public String GUID_RESPONSE {get;set;}
		public Decimal? QUEUE_PROCESS_ID {get;set;}
		
	}
}



