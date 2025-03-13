//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.CCDC
{
	public partial class GiamHongmat : BaseEntity
	{
		public Decimal CONG_CU_ID {get;set;}
		public Decimal NHAP_XUAT_ID {get;set;}
		public Decimal? DON_VI_ID {get;set;}
		public Decimal? DON_VI_BO_PHAN_ID {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public Decimal? NGUOI_TAO {get;set;}
		public String LY_DO {get;set;}
		public String GHI_CHU {get;set;}
		public Decimal SO_LUONG { get; set; }
        public DateTime NGAY_LAP { get; set; }
        public virtual CongCu CongCu { get; set; }
        public virtual XuatNhap XuatNhap { get; set; }
    }
}



