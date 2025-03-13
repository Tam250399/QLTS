using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCQH
{
	public class TS_BCQH_PL05_TANG_GIAM_TSNN_NHOM_DV : BaseViewEntity
	{
		public int? LOAI_HINH_DON_VI_ID { get; set; }
		public String LOAI_HINH_DON_VI_TEN { get; set; }
		public Decimal? DAU_KY_NGUYEN_GIA { get; set; }
		//public Decimal? DAU_KY_GIA_TRI_CON_LAI { get; set; }
		public Decimal? TANG_TRONG_KY_NGUYEN_GIA { get; set; }
		public Decimal? GIAM_TRONG_KY_NGUYEN_GIA { get; set; }
		public Decimal? CUOI_KY_NGUYEN_GIA { get; set; }
		//public Decimal? CUOI_KY_GIA_TRI_CON_LAI { get; set; }

		//tổng
		public Decimal? DAU_KY_TONG_NGUYEN_GIA { get; set; }
		//public Decimal? DAU_KY_TONG_GIA_TRI_CON_LAI { get; set; }
		public Decimal? CUOI_KY_TONG_NGUYEN_GIA { get; set; }
		//public Decimal? CUOI_KY_TONG_GIA_TRI_CON_LAI { get; set; }
	}
}
