//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core.Domain.DanhMuc;

namespace GS.Core.Domain.SHTD
{
	public partial class KetQua : BaseEntity
	{
		public Decimal TAI_SAN_TD_XU_LY_ID {get;set;}
		public Decimal? DON_VI_CHUYEN_ID {get;set;}
		public Decimal? GIA_TRI_NSNN {get;set;}
		public Decimal? GIA_TRI_TKTG {get;set;}
		public Decimal? CHI_PHI_XU_LY {get;set;}
		public String HOP_DONG_SO {get;set;}
		public DateTime? HOP_DONG_NGAY {get;set;}
		public Guid? GUID {get;set;}
		public Decimal? GIA_TRI_BAN {get;set;}
		public String HO_SO_GIAY_TO_KHAC {get;set;}
		public DateTime? NGAY_XU_LY {get;set;}
		public Decimal? SO_LUONG { get; set; }
		public String DB_ID { get; set; }
		public String DB_TAI_SAN_XU_LY_ID { get; set; }
		public virtual TaiSanTdXuLy taiSanTdXuLy { get; set; }
		public virtual DonVi donVi { get; set; }
	}
}



