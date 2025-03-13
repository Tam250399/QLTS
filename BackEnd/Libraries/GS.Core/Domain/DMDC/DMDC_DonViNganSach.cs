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
	public partial class DMDC_DonViNganSach : BaseEntity
	{
		public String MA {get;set;}
		public String MA_DB {get;set;}
		public String MA_V { get; set; }
		public String MA_T { get; set; }
		public String MA_H { get; set; }
		public String MA_X { get; set; }
		public String MA_CU { get; set; }
		public String TEN {get;set;}
		public String MA_CQT {get;set;}
		public String MA_THAMCHIEU {get;set;}
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
		public String DIACHI {get;set;}
		public String DVDTCT {get;set;}
		public String DVDTCD {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public String NGUOI_TAO {get;set;}
		public DateTime? NGAY_SUA {get;set;}
		public String NGUOI_SUA {get;set;}
		public String CCH_TEN {get;set;}
		public String TRANGTHAI_MA {get;set;}
		public String TRANGTHAI_CU {get;set;}
		public Decimal? TRANGTHAI_DM {get;set;}
		public DateTime? NGAY_DMO {get;set;}
		public Decimal? LOAI { get; set; }
		public Boolean? HIEU_LUC { get; set; }
		public DateTime? NGAY_SD { get; set; }
		public DateTime? NGAY_HL { get; set; }
		public DateTime? NGAY_KT { get; set; }
		public String VAN_BAN_BH { get; set; }
		public DateTime? NGAY_VB { get; set; }
	}
}



