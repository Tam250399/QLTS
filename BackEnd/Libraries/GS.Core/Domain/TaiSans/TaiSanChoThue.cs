//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 5/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;

namespace GS.Core.Domain.TaiSans
{
	public partial class TaiSanChoThue : BaseEntity
	{
		public Decimal? DOI_TAC_ID {get;set;}
		public string DOI_TAC_TEN { get; set; }
		public decimal? LOAI_DOI_TAC_ID { get; set; }
		public Decimal TAI_SAN_ID {get;set;}
		public String HOP_DONG_SO { get;set;}
		public DateTime? HOP_DONG_NGAY { get;set;}
		public String HOA_DON_SO {get;set;}
		public DateTime? HOA_DON_NGAY {get;set;}
		public Decimal? DON_GIA_CHO_THUE {get;set;}
		public Decimal? THU_TU_CHO_THUE {get;set;}
		public Decimal? NOP_NGAN_SACH {get;set;}
		public Decimal? GIU_LAI_DON_VI {get;set;}
		public String GHI_CHU {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public DateTime? NGAY_UPDATE {get;set;}
		public Decimal? NGUOI_TAO_ID {get;set;}
		public DateTime? NGAY_CHO_THUE_FROM {get;set;}
		public DateTime? NGAY_CHO_THUE_TO {get;set;}
		public virtual TaiSan taisan { get; set; }
		public virtual DoiTac doitac { get; set; }
		public virtual NguoiDung nguoidung { get; set; }
	}
}



