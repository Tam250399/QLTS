using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCQH
{
	public class TS_BCQH_MAU11A_THTSNN:BaseViewEntity
	{
		public decimal? TAI_SAN_ID { get; set; }

		public decimal? LOAI_HINH_TAI_SAN_ID { get; set; }

		public decimal? BIEN_DONG_ID { get; set; }
		public decimal? SO_LUONG { get; set; }
		public string DV_TREE_NODE { get; set; }
		public string MA_DON_VI_CHA { get; set; }
		public string TEN_DON_VI_CHA { get; set; }
		public decimal? DIEN_TICH { get; set; }
		public decimal? NGUYEN_GIA_NGAN_SACH { get; set; }
		public decimal? NGUYEN_GIA_KHAC { get; set; }
		public string TEN_LOAI_HINH_TAI_SAN { get; set; }
		public decimal? GIA_TRI_CON_LAI { get; set; }
		public decimal? NGUYEN_GIA { get; set; }
		// more
		public Decimal? LOAI_HINH_TAI_SAN_DB_ID { get; set; }
	}
}
