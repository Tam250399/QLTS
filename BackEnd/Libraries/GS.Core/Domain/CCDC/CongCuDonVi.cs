//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.CCDC
{
	public partial class CongCuDonVi : BaseEntity
	{
		public Decimal? CONG_CU_ID {get;set;}
		public Decimal? DON_VI_BO_PHAN_ID {get;set;}
		public Decimal? DON_VI_ID {get;set;}
		public Decimal? SO_LUONG_ID {get;set;}
		
	}
}



