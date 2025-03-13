using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCQH
{
	public class TS_BCQH_PL03_TANG_GIAM_TSNN : BaseViewEntity
	{
		public int? LOAI_TAI_SAN_ID { get; set; }
		public String LOAI_TAI_SAN_TEN { get; set; }		
		public Decimal? DAU_KY_SO_LUONG { get; set; }
		public Decimal? DAU_KY_NGUYEN_GIA { get; set; }
		public Decimal? DAU_KY_DIEN_TICH { get; set; }
		public Decimal? TANG_TRONG_KY_SO_LUONG { get; set; }
		public Decimal? TANG_TRONG_KY_NGUYEN_GIA { get; set; }
		public Decimal? TANG_TRONG_KY_DIEN_TICH { get; set; }
		public Decimal? GIAM_TRONG_KY_SO_LUONG { get; set; }
		public Decimal? GIAM_TRONG_KY_NGUYEN_GIA { get; set; }
		public Decimal? GIAM_TRONG_KY_DIEN_TICH { get; set; }
		public Decimal? CUOI_KY_SO_LUONG { get; set; }
		public Decimal? CUOI_KY_NGUYEN_GIA { get; set; }
		public Decimal? CUOI_KY_DIEN_TICH { get; set; }
	}
}
