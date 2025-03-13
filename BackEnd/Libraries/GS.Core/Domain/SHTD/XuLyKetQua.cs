using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.SHTD
{
	public enum enumTrangThaiXuLyKetQua
	{
		XOA = 0,
		TONTAI = 1,
		NHAP = 2
	}
    public partial class XuLyKetQua:BaseEntity
    {
		public Decimal? XU_LY_ID { get; set; }
		public String CHUNG_TU_NOP_TIEN_SO { get; set; }
		public DateTime? CHUNG_TU_NOP_TIEN_NGAY { get; set; }
		public Decimal? SO_TIEN { get; set; }
		public DateTime? NGAY_NOP_TIEN { get; set; }
		public String GHI_CHU { get; set; }
		public DateTime? NGAY_TAO { get; set; }
		public Decimal? NGUOI_TAO_ID { get; set; }
		public Decimal? TRANG_THAI_ID { get; set; }
		//public virtual XuLy xuly { get; set; }
		public virtual XuLy xulyTD { get; set; }

	}
}
