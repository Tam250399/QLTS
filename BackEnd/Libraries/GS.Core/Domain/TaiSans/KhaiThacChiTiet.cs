using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.TaiSans
{
	public class KhaiThacChiTiet : BaseEntity
	{

		public DateTime? NGAY_KHAI_THAC { get; set; }
		public decimal SO_TIEN_THU_DUOC { get; set; }
		public string GHI_CHU { get; set; }
		public decimal KHAI_THAC_ID { get; set; }
		public decimal TAI_SAN_ID { get; set; }
		//more
		public decimal? CHO_THUE_PHUONG_THUC_ID { get; set; }
		public string HOP_DONG_SO { get; set; }
		public DateTime? HOP_DONG_NGAY { get; set; }		
		public DateTime? NGAY_KHAI_THAC_DEN { get; set; }
		public decimal? DOI_TAC_ID { get; set; }
		public decimal? CHO_THUE_GIA { get; set; }
		public decimal? DIEN_TICH_KHAI_THAC { get; set; }
		public Decimal? LDLK_HINH_THUC_ID { get; set; }
		public String TEN_PHAP_NHAN_MOI { get; set; }
	}
}
