//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.CCDC
{
	public partial class SuaChuaBaoDuong : BaseEntity
    {
        public Decimal NHAP_XUAT_ID { get; set; }
        public Decimal CONG_CU_ID {get;set;}
		public Decimal? NGUOI_TAO_ID {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public String SO_QUYET_DINH {get;set;}
		public DateTime? NGAY_QUYET_DINH {get;set;}
		public String CAP_QUYET_DINH {get;set;}
		public DateTime NGAY_SUA_CHUA_FROM {get;set;}
		public DateTime NGAY_SUA_CHUA_TO {get;set;}
		public Decimal SO_LUONG_SUA_CHUA {get;set;}
		public Decimal GIA_TRI_SUA_CHUA {get;set;}
		public Decimal? KHACH_HANG_ID {get;set;}
		public String CHUNG_TU_SO {get;set;}
		public DateTime? CHUNG_TU_NGAY {get;set;}
		public String GHI_CHU {get;set;}
        public virtual XuatNhap XuatNhap { get; set; }
        public virtual CongCu CongCu { get; set; }
		
	}
}



