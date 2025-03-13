//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 6/10/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.DMDC
{
	public partial class DMDC_QuocGia : BaseEntity
	{
		public String MA {get;set;}
		public String TEN {get;set;}
		public String TEN_TA {get;set;}
		public String GHICHU {get;set;}
		public String MA_HQ {get;set;}
		public Boolean HIEU_LUC {get;set;}
		public DateTime? NGAY_HL {get;set;}
		public DateTime? NGAY_SD {get;set;}
		public DateTime? NGAY_KT {get;set;}
		public String VAN_BAN_BH {get;set;}
		public DateTime? NGAY_VB {get;set;}
		public DateTime NGAY_TAO {get;set;}
		public String NGUOI_TAO {get;set;}
		
	}
}



