using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCDA
{
	public class TS_BCDA_01C_DK_TSDA_KEKHAI_TAISANKHAC : BaseViewEntity
	{
		public decimal? DU_AN_ID { get; set; }
		public String DU_AN_TEN { get; set; }
		public decimal? TAI_SAN_ID { get; set; }
		public decimal? NGUYEN_GIA_NS { get; set; }
		public decimal? NGUYEN_GIA_KHAC { get; set; }
		public string ISQUANLYNHANUOC { get; set; }
		public string ISKHONGKINHDOANH { get; set; }
		public string ISKINHDOANH { get; set; }
		public string ISCHOTHUE { get; set; }
		public string ISLIENDOANHLIENKET { get; set; }
		public string ISSUDUNGKHAC { get; set; }
		public string TAI_SAN_MA { get; set; }
		public string TAI_SAN_TEN { get; set; }
		public DateTime? NGAY_SU_DUNG { get; set; }
		public string NUOC_SAN_XUAT { get; set; }
		public int? NAM_SAN_XUAT { get; set; }
		public decimal? GTCL { get; set; }
		public decimal? NGUYEN_GIA { get; set; }
		public string KY_HIEU { get; set; }
	}
}
