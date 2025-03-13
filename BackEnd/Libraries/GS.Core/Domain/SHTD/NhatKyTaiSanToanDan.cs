//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.SHTD
{
	public partial class NhatKyTaiSanToanDan : BaseEntity
	{
		public Decimal? QUYET_DINH_ID {get;set;}
		public Decimal? NGUOI_DUNG_ID {get;set;}
		public Decimal? TRANG_THAI_ID {get;set;}
		public String DATA_JSON {get;set;}
		
	}
}



