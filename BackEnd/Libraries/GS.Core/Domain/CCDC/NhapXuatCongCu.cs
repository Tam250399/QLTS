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
	public partial class NhapXuatCongCu : BaseEntity
	{
		public Decimal NHAP_XUAT_ID {get;set;}
		public Decimal CONG_CU_ID {get;set;}
		public Decimal? SO_LUONG {get;set;}
        public Decimal? DON_GIA { get; set; }
        public Decimal? THANH_TIEN { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public String CHUNG_TU_SO { get; set; }
        public DateTime? CHUNG_TU_NGAY { get; set; }
        public Decimal SoLuongCoThePhanBo { get; set; }
        public Decimal? NHAP_KHO_ID { get; set; }
        public virtual XuatNhap XuatNhap { get; set; }
        public virtual CongCu CongCu { get; set; }
    }
}



