using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCTH
{
	public class TS_BCTH_02F_DK_TSNN : BaseViewEntity
	{
		public int? TAI_SAN_ID { get; set; }
		public string TAI_SAN_MA { get; set; }
		public string TAI_SAN_TEN { get; set; }
		public DateTime? NGAY_SU_DUNG { get; set; }
		public int? TREE_LEVEL { get; set; }
		public int? tree_level_dv { get; set; }
		public string DV_1 { get; set; }
		public string DV_2 { get; set; }
		public string DV_3 { get; set; }
		public string DV_4 { get; set; }
		public string DV_5 { get; set; }
		public string DV_TEN_1 { get; set; }
		public string DV_TEN_2 { get; set; }
		public string DV_TEN_3 { get; set; }
		public string DV_TEN_4 { get; set; }
		public string DV_TEN_5 { get; set; }
		public int? CAP_1 { get; set; }
		public String CAP_2 { get; set; }
		public String CAP_3 { get; set; }
		public String CAP_4 { get; set; }
		public String CAP_5 { get; set; }
		public string TEN_1 { get; set; }
		public string TEN_2 { get; set; }
		public string TEN_3 { get; set; }
		public string TEN_4 { get; set; }
		public string TEN_5 { get; set; }
		public Decimal? NGUYEN_GIA { get; set; }
		public Decimal? LUY_KE { get; set; }
		public Decimal? KH_HM_TRONG_NAM { get; set; }
		//public String GHI_CHU { get; set; }
		//
		public int? LOAI_HINH_TAI_SAN_ID { get; set; }
		public int? LOAI_TAI_SAN_ID { get; set; }
		public int? LOAI_TAI_SAN_DB_ID { get; set; }
		public int? LOAI_TAI_SAN_PARENT_ID { get; set; }
		public int? LOAI_DON_VI { get; set; }
		public int? DON_VI_ID { get; set; }
		public int? DON_VI_ID_COMPARE { get; set; }
		public string TEN_DON_VI { get; set; }
		public string TEN_LOAI_DON_VI { get; set; }
		public string DON_VI_MA { get; set; }
	}
}
