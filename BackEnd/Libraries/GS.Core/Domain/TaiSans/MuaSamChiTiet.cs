//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 21/7/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.TaiSans
{
	public partial class MuaSamChiTiet : BaseEntity
	{
		public Decimal MUA_SAM_ID {get;set;}
		public Decimal? LOAI_TAI_SAN_ID {get;set;}
		public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
		public String TEN_TAI_SAN {get;set;}
		public Decimal? HINH_THUC_MUA_SAM_ID {get;set;}
		public String DAC_DIEM {get;set;}
		public Decimal? SO_LUONG {get;set;}
		public Decimal? DON_GIA {get;set;}
		public DateTime? THOI_GIAN_DU_KIEN {get;set;}
		public Decimal? DU_TOAN_DUOC_DUYET {get;set;}
		public Boolean? IS_TAI_SAN_NGUON_VIEN_TRO {get;set;}
		public String DE_XUAT {get;set;}
		public String GHI_CHU {get;set; }
		public virtual MuaSam muaSam { get; set; }

	}
}



