using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCKK
{
	public class TS_BCKK_03_DMTSNN_GIAM_KHAC:BaseViewEntity
	{
		public decimal? TAI_SAN_ID { get; set; }
		public decimal? NGUYEN_GIA_NS { get; set; }
		public decimal? NGUYEN_GIA_KHAC { get; set; }
		public string ISQUANLYNHANUOC { get; set; }
		public string ISKHONGKINHDOANH { get; set; }
		public string ISKINHDOANH { get; set; }
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
