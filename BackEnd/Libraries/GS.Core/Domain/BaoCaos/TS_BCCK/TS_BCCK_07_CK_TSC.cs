using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCCK
{
	public class TS_BCCK_07_CK_TSC:BaseViewEntity
	{
		public string TAI_SAN_TEN { get; set; }
		public string TAI_SAN_MA { get; set; }
		public DateTime? THOI_GIAN_MUA { get; set; }
		public Decimal? SO_LUONG { get; set; }
		public Decimal? NGUYEN_GIA { get; set; }
		public DateTime? THOI_GIAN_DU_KIEN_MUA { get; set; }
		public Decimal? SO_LUONG_DU_KIEN { get; set; }
		public Decimal? DON_GIA_DU_KIEN { get; set; }
		public Decimal? DU_AN_CO_THAM_QUYEN_PHE { get; set; }
		public Decimal? TY_LE_HOAN_THANH { get; set; }
	}
}
