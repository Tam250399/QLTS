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
    public enum enumViTriKiemKeHoiDong
    {
		ALL = 0,
        TRUONG_BAN = 1,
        UY_VIEN = 2
    }
	
	public partial class KiemKeHoiDong : BaseEntity
	{
		public Decimal KIEM_KE_ID {get;set;}
		public String HO_TEN {get;set;}
		public String CHUC_VU {get;set;}
		public String DAI_DIEN {get;set;}
		public Decimal VI_TRI_ID {get;set;}
	}
}



