//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.ThuocTinhs
{
	public partial class ThuocTinhTaiSan : BaseEntity
	{
		public Decimal THUOC_TINH_ID {get;set;}
		public Decimal LOAI_HINH_TAI_SAN_ID {get;set;}
		public Decimal? SAP_XEP {get;set;}
		public String TREE_NOTE {get;set;}
		public Decimal LOAI_TAI_SAN_ID {get;set;}
		public virtual ThuocTinh ThuocTinh { get; set; }
		
	}
}



