//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.TaiSans
{
	public partial class TaiSanKiemKe : BaseEntity
	{
		public Decimal DON_VI_ID {get;set;}
		public String SO_BIEN_BAN {get;set;}
		public DateTime NGAY_KIEM_KE {get;set;}
		public Decimal? DON_VI_BO_PHAN_ID {get;set;}
		public String NGUOI_LAP_BIEU {get;set;}
		public String DAI_DIEN_BPSD {get;set;}
		public String KE_TOAN {get;set;}
		public String TRUONG_BAN {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public Decimal? NGUOI_TAO_ID {get;set;}
		
	}
}



