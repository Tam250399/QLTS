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
	public partial class DMDC_ToChucNganSach : BaseEntity
	{
		public String MA {get;set;}
		public String TEN {get;set;}
		public String MA_CQT {get;set;}
		public String LOAIDV_MA {get;set;}
		public String LOAIDV_TEN {get;set;}
		public String CAPDT {get;set;}
		public String CHUONG {get;set;}
		public String LHDV_MA {get;set;}
		public String LHDV_TEN {get;set;}
		public String SOQD_TL {get;set;}
		public DateTime? NGAY_TL {get;set;}
		public String COQUAN_TL {get;set;}
		public String DVCT_MA {get;set;}
		public String DVCT_TEN {get;set;}
		public String DVQLTT_MA {get;set;}
		public String DVQLTT_TEN {get;set;}
		public String DBN_MA {get;set;}
		public String DBN_TEN {get;set;}
		
	}
}



