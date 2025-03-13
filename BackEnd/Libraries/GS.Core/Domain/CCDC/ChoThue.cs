//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.CCDC
{
	public partial class ChoThue : BaseEntity
	{
		public Decimal CONG_CU_ID {get;set; }
        public Decimal NHAP_XUAT_ID { get; set; }
        public Decimal? NGUOI_TAO_ID {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public String QUYET_DINH_SO {get;set;}
		public DateTime? QUYET_DINH_NGAY {get;set;}
		public String CAP_QUYET_DINH {get;set;}
		public DateTime? NGAY_CHO_THUE_FROM {get;set;}
		public DateTime? NGAY_CHO_THUE_TO {get;set;}
		public Decimal? KHACH_HANG_ID {get;set;}
		public String HOP_DONG_SO {get;set;}
		public DateTime? HOP_DONG_NGAY {get;set;}
		public Decimal? SO_LUONG {get;set;}
		public Decimal? SO_TIEN_THU_DUOC {get;set;}
		public String GHI_CHU {get;set;}
		public virtual CongCu CongCu { get; set; }
	}
}



