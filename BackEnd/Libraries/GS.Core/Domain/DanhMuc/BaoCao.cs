//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;


namespace GS.Core.Domain.DanhMuc
{
	public partial class BaoCao : BaseEntity
	{
		public String MA_BAO_CAO {get;set;}
		public String NOI_DUNG {get;set;}
		public Decimal? HAS_ROW_ID {get;set;}
		public Decimal? HAS_COL_ID {get;set;}
	}
}



