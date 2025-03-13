using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCDA
{
    public class TS_BCDA_05BC_TANG_GIAM_TSDA:BaseViewEntity
    {
		public string TAI_SAN_MA { get; set; }
		public String TAI_SAN_TEN { get; set; }
		public int? LOAI_TAI_SAN_ID { get; set; }
		public int? CAP_1 { get; set; }
		public string TEN_1 { get; set; }
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
