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
	public partial class DMDC_DiaBan : BaseEntity
	{
		public String MA_V {get;set;}
		public String MA_T {get;set;}
		public String MA_H {get;set;}
		public String MA_X {get;set;}
		public String MA_CU {get;set;}
		public String TEN {get;set;}
		public String MA_DB {get;set;}
		public Decimal? ID_CHA {get;set;}
		public String MA_CHA {get;set;}
		public Decimal LOAI {get;set;}
		public String MA_THAMCHIEU {get;set;}
		public Boolean HIEU_LUC {get;set;}
		public DateTime? NGAY_SD {get;set;}
		public DateTime? NGAY_HL {get;set;}
		public DateTime? NGAY_KT {get;set;}
		public String VAN_BAN_BH {get;set;}
		public DateTime? NGAY_VB {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public String NGUOI_TAO {get;set;}
		public decimal? TREE_LEVEL {get;set;}
		
		public String TREE_NODE {get;set;}
		public Decimal? QUOC_GIA_ID { get; set; }

	}
    public enum enumLoaiDiaBanHanhChinh
    {
        Vung=1,
        Tinh=2,
        Huyen=3,
        Xa=4
    }
}



