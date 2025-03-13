//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.ThuocTinhs
{
	public partial class ThuocTinhData : BaseEntity
	{
		public Decimal? THUOC_TINH_ID {get;set;}
		public String DATA {get;set;}
		public Decimal? DON_VI_ID {get;set;}
		public Decimal? DON_VI_BO_PHAN_ID {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public DateTime? NGAY_CAP_NHAT {get;set;}
		public Decimal NGUOI_TAO_ID {get;set;}
		public Decimal? TAI_SAN_ID { get; set; }
		public Decimal? TAI_SAN_TD_XU_LY_ID { get; set; }
		
	}
}



