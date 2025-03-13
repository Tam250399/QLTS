//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.DanhMuc
{
	public partial class NhomCongCu : BaseEntity
	{
		public String MA {get;set;}
		public String TEN {get;set;}
		public Decimal? THOI_HAN_PHAN_BO {get;set;}
		public String DON_VI_TINH_ID {get;set;}
		public Decimal? PARENT_ID {get;set;}
		public Decimal? DON_VI_ID {get;set;}
		public virtual DonVi DON_VI_ENTITY { get; set; }
		public String TREE_NODE { get; set; }
		public Decimal? TREE_LEVEL { get; set; }

	}
}



