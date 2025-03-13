using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCKK
{
	public class TS_BCKK_05B_DK_TSDA : BaseViewEntity
	{
		public Decimal? DU_AN_ID { get; set; }
		public String DU_AN_TEN { get; set; }
		public decimal? TAI_SAN_ID { get; set; }
		public decimal? NGUYEN_GIA_NS { get; set; }
		public decimal? NGUYEN_GIA_KHAC { get; set; }
		public string CAP_2 { get; set; }
		public string TEN_2 { get; set; }
		//public string ISQUANLYNHANUOC { get; set; }
		//public string ISKHONGKINHDOANH { get; set; }
		//public string ISKINHDOANH { get; set; }
		//public string ISCHOTHUE { get; set; }
		//public string ISLIENDOANHLIENKET { get; set; }
		//public string ISSUDUNGKHAC { get; set; }
		public string IsQuanLyDuAn { get; set; }
		public string IsSuDungKhacQLDA { get; set; }
		public string TAI_SAN_MA { get; set; }
		public string TAI_SAN_TEN { get; set; }
		public DateTime? NGAY_SU_DUNG { get; set; }
		public string NUOC_SAN_XUAT { get; set; }
		public int? NAM_SAN_XUAT { get; set; }
		public string BIEN_KIEM_SOAT { get; set; }
		public decimal? CHO_NGOI_TAI_TRONG { get; set; }
		public string NHAN_HIEU { get; set; }
		public Decimal? CONG_SUAT { get; set; }
		public string CHUCDANHSUDUNG { get; set; }
		public string NGUON_GOC_XE { get; set; }
		public decimal? GTCL { get; set; }
		public decimal? NGUYEN_GIA { get; set; }
	}
}
