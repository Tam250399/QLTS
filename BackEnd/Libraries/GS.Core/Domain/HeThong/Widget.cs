//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 20/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.HeThong
{
	public partial class Widget : BaseEntity
	{
		public String TEN {get;set;}
		public String CAU_HINH {get;set;}
		public String MO_TA {get;set;}
		public String PARTIAL_VIEW_NAME { get; set; }
	}
}



