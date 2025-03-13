//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.DanhMuc
{
	public partial class HinhThucXuLy : BaseEntity
	{
		public String MA {get;set;}
		public String TEN {get;set;}
		public Decimal? PHUONG_AN_XU_LY_ID {get;set;}
		public Decimal? SAP_XEP {get;set;}
		public Decimal? DB_ID { get; set; }

	}
}



