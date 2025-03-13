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
	public partial class DMDC_DuAn : BaseEntity
	{
		public String MA_CHA {get;set;}
		public String MA {get;set;}
		public String TEN {get;set;}
		public String CQTC_MA {get;set;}
		public String MA_THAMCHIEU {get;set;}
		public String DVQLTT_MA {get;set;}
		public String DVQLTT_TEN {get;set;}
		public String CDT_MA {get;set;}
		public String CDT_TEN {get;set;}
		public String BQL_MA {get;set;}
		public String BQL_TEN {get;set;}
		public String LOAIDA_MA {get;set;}
		public String LOAIDA_TEN {get;set;}
		public String NHOMDA_MA {get;set;}
		public String NHOMDA_TEN {get;set;}
		public String CTMT_MA {get;set;}
		public String CTMT_TEN {get;set;}
		public String HTDA_MA {get;set;}
		public String HTDA_TEN {get;set;}
		public String HTQL_MA {get;set;}
		public String HTQL_TEN {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public String NGUOI_TAO {get;set;}
		public DateTime? NGAY_SUA {get;set;}
		public String NGUOI_SUA {get;set;}
		public String LY_DO_SUA {get;set;}
		public String SOQD_TL {get;set;}
		public DateTime? NGAY_TL {get;set;}
		public String COQUAN_TL {get;set;}
		public String TRANGTHAI_MA {get;set;}
		public String TRANGTHAI_CU {get;set;}
		public Decimal? TRANGTHAI_DM {get;set;}
		public DateTime? NGAY_DMO {get;set;}
		
	}
}



