//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 22/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core.Domain.TaiSans;

namespace GS.Core.Domain.KT
{
	public partial class HaoMonTaiSan : BaseEntity
	{
		
		public HaoMonTaiSan()
		{

		}
		public HaoMonTaiSan(HaoMonTaiSan obj)
		{
			this.TAI_SAN_ID = obj.TAI_SAN_ID;
			this.NAM_HAO_MON = obj.NAM_HAO_MON;
			this.MA_TAI_SAN = obj.MA_TAI_SAN;
			this.GIA_TRI_HAO_MON = obj.GIA_TRI_HAO_MON;
			this.TONG_HAO_MON_LUY_KE = obj.TONG_HAO_MON_LUY_KE;
			this.TONG_GIA_TRI_CON_LAI = obj.TONG_GIA_TRI_CON_LAI;
			this.TY_LE_HAO_MON = obj.TY_LE_HAO_MON;
			this.TONG_NGUYEN_GIA = obj.TONG_NGUYEN_GIA;
			
		}
		public Decimal TAI_SAN_ID {get;set;}
		public String MA_TAI_SAN {get;set;}
		public Decimal NAM_HAO_MON {get;set;}
		public Decimal? GIA_TRI_HAO_MON {get;set;}
		public Decimal? TONG_HAO_MON_LUY_KE {get;set;}
		public Decimal? TONG_GIA_TRI_CON_LAI {get;set;}
		public Decimal? TY_LE_HAO_MON {get;set;}
		public Decimal? TONG_NGUYEN_GIA {get;set;}

		public virtual TaiSan TaiSan { get; set; }
        public Decimal? DB_ID { get; set; }
        public Decimal TRANG_THAI_DONG_BO { get; set; }

	}
}



