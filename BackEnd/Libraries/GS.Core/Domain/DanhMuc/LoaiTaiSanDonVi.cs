//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.DanhMuc
{
	public partial class LoaiTaiSanDonVi : BaseEntity
	{
		public String MA {get;set;}
		public String TEN {get;set;}
		public Decimal? LOAI_HINH_TAI_SAN_ID {get;set;}
		public Decimal? HM_THOI_HAN_SU_DUNG {get;set;}
		public Decimal? HM_TY_LE {get;set;}
		public Decimal? KH_THOI_HAN_SU_DUNG {get;set;}
		public Decimal? KH_TY_LE {get;set;}
		public String MO_TA {get;set;}
		public Decimal? CHE_DO_HAO_MON_ID {get;set;}
		public Decimal? PARENT_ID {get;set;}
		public String TREE_NODE {get;set;}
		public Decimal? TREE_LEVEL {get;set;}
		public String DON_VI_TINH {get;set;}
		public Decimal? DON_VI_ID {get;set;}
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public virtual LoaiTaiSan LoaiTaiSan { get; set; }
        public virtual DonVi DonVi { get; set; }
        public virtual CheDoHaoMon CheDoHaoMon { get; set; }
        public String DB_ID { get; set; }
    }
}



