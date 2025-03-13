//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 28/5/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.TaiSans
{
	public partial class TaiSanKhauHao : BaseEntity
	{
		public DateTime? NGAY_BAT_DAU {get;set;}
		public DateTime? NGAY_KET_THUC {get;set;}
		public Decimal? SO_THANG_KHAU_HAO {get;set;}
		public Decimal? TAI_SAN_ID {get;set;}
		public Decimal? TY_LE_KHAU_HAO {get;set;}
		public Decimal? TY_LE_NGUYEN_GIA_KHAU_HAO {get;set;}
		
	}
}



