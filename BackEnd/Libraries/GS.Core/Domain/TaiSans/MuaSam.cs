//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 21/7/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.TaiSans
{
	public partial class MuaSam : BaseEntity
	{
		public MuaSam()
		{
			this.GUID = Guid.NewGuid();
		}
		public Guid GUID {get;set;}
		public Decimal? DVLQTS_ID {get;set;}
		public Decimal DVSDTS_ID {get;set;}
		public DateTime NGAY_DANG_KY {get;set;}
		public Decimal? NAM {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public Decimal? NGUOI_TAO_ID {get;set;}
		public Decimal TRANG_THAI_ID { get;set;}
		public Decimal? NGUOI_DUYET_ID { get; set; }
		public DateTime? NGAY_DUYET { get; set; }
		public String GHI_CHU { get; set; }


	}
}



