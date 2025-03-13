//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/6/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.DanhMuc
{
	public partial class LoaiTaiSanKhauHao : BaseEntity
	{
		public Decimal LOAI_TAI_SAN_ID {get;set;}
		public Decimal DON_VI_ID {get;set;}
		public Decimal? TY_LE_KHAU_HAO {get;set;}
		public Decimal? THOI_HAN_SU_DUNG {get;set;}
		public String MA_LOAI_TAI_SAN {get;set;}
		public String MA_DON_VI {get;set; }
		//public virtual LoaiTaiSan LoaiTaiSan { get; set; }
		//public virtual DonVi DonVi { get; set; }
		//public virtual CheDoHaoMon CheDoHaoMon { get; set; }

	}
}



