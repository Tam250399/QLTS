using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCCT
{
	public class TS_BCCT_01F_DK_TSNN:BaseViewEntity
	{
		public int? TAI_SAN_ID { get; set; }
		public string TAI_SAN_MA { get; set; }
		public string TAI_SAN_TEN { get; set; }
		public int? TREE_LEVEL { get; set; }
		public int? CAP_1 { get; set; }
		public String CAP_2 { get; set; }
		public String CAP_3 { get; set; }
		public String CAP_4 { get; set; }
		public String CAP_5 { get; set; }
		//public String MA_1 { get; set; }
		//public String MA_2 { get; set; }
		//public String MA_3 { get; set; }
		//public String MA_4 { get; set; }
		//public String MA_5 { get; set; }
		public string TEN_1 { get; set; }
		public string TEN_2 { get; set; }
		public string TEN_3 { get; set; }
		public string TEN_4 { get; set; }
		public string TEN_5 { get; set; }
		public Decimal? LUY_KE { get; set; }
		public Decimal? KH_HM_TRONG_NAM { get; set; }
		public Decimal? TONG_NGUYEN_GIA { get; set; }
		//public String GHI_CHU { get; set; }

		//
		public int? DON_VI_ID_COMPARE { get; set; }
		public int? LOAI_HINH_TAI_SAN_ID { get; set; }
		public int? LOAI_TAI_SAN_ID { get; set; }
		public int? LOAI_TAI_SAN_PARENT_ID { get; set; }


	}
}
