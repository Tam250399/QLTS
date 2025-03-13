//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 25/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.DanhMuc
{
	public partial class DonViChuyenDoi : BaseEntity
	{
		public Decimal DON_VI_ID {get;set;}
		public String TEN {get;set;}
		public Decimal LOAI_DON_VI_ID {get;set;}
		public String QUYET_DINH_SO {get;set;}
		public DateTime? QUYET_DINH_NGAY {get;set;}
		public String QUYET_DINH_GIAO_VON_SO {get;set;}
		public DateTime? QUYET_DINH_GIAO_VON_NGAY {get;set;}
		public DateTime NGAY_TAO {get;set;}
		public Decimal NGUOI_TAO_ID {get;set;}
		public String GHI_CHU {get;set; }
		public Decimal? LOAI_CAP_DON_VI_ID { get; set; }
		public Decimal? CAP_DON_VI_ID { get; set; }
		public String MA_DVQHNS { get; set; }
	}
}



