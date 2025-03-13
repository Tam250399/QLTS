//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 5/6/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core.Domain.TaiSans;

namespace GS.Core.Domain.KT
{
	public partial class KhauHaoTaiSan : BaseEntity
	{
		public Decimal TAI_SAN_ID {get;set;}
		public String MA_TAI_SAN {get;set;}
		public Decimal NAM_KHAU_HAO {get;set;}
		public Decimal? GIA_TRI_KHAU_HAO {get;set;}
		public Decimal? TONG_KHAU_HAO_LUY_KE {get;set;}
		public Decimal? TONG_GIA_TRI_CON_LAI {get;set;}
		public Decimal? TY_LE_KHAU_HAO {get;set;}
		public Decimal? TONG_NGUYEN_GIA {get;set;}
		public Decimal? THANG_KHAU_HAO {get;set;}
		public Decimal? SO_NAM_SD_CON_LAI {get;set;}
		public Decimal? TY_LE_KH_NEW {get;set;}

		public virtual TaiSan TaiSan { get; set; }

	}
}



