//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 18/6/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.HeThong
{
	public partial class DinhMucChiTiet : BaseEntity
	{
		public Decimal DINH_MUC_ID {get;set;}
		public Decimal LOAI_TAI_SAN_ID {get;set;}
		public Decimal CHUC_DANH_ID {get;set;}
		public Decimal? LOAI_HINH_TAI_SAN_ID {get;set;}
		public Decimal? DINH_MUC {get;set;}
		public Decimal? SO_LUONG {get;set;}
		
	}
}



