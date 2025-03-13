//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.SHTD
{
	public partial class ThuChi : BaseEntity
	{
		public Decimal? KET_QUA_ID {get;set;}
		public Decimal? SO_TIEN_PHAI_THU {get;set;}
		public Decimal? SO_TIEN_CON_PHAI_THU { get; set; }
		public Decimal? SO_TIEN_THU {get;set;}
		public DateTime? NGAY_THU {get;set;}
		public Decimal? TG_SO_TIEN {get;set;}
		public DateTime? TG_NGAY_NOP {get;set;}
		public Decimal? CHI_PHI {get;set;}
		public Decimal? XU_LY_ID { get; set; }
        public Decimal? SO_TIEN_NOP_NSNN { get; set; }

        // xử lý chọn nhiều xử lý
        public string LIST_XU_LY_ID { get; set; }
		public Decimal? DON_VI_ID { get; set; }
		public string DB_ID { get; set; }
		public string DB_XU_LY_ID { get; set; }
		public virtual XuLy xuLy { get; set; }
		public virtual KetQua ketQua { get; set; }
	}
}



