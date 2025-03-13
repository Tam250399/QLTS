//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 3/10/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.DM
{
	public partial class LoaiLyDoBienDong : BaseEntity
	{
		public String MA {get;set;}
		public String TEN {get;set;}
		public Decimal? PARENT_ID {get;set;}
		public String TREE_NODE {get;set;}
		public Decimal? TREE_LEVEL {get;set;}
        public Decimal? DB_ID { get; set; }
		
	}
}



