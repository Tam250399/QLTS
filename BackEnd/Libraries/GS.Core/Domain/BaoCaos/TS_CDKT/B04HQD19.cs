using System;

namespace GS.Core.Domain.BaoCaos.TS_CDKT
{
	public class B04HQD19 : BaseViewEntity
	{
		public String TAI_SAN_MA { get; set; }
		public String TAI_SAN_TEN { get; set; }
		//public String MA_NHOM_TAI_SAN { get; set; }
		//public String TEN_NHOM_TAI_SAN { get; set; }
		//public String DON_VI_TINH { get; set; }
		public Int32? SO_LUONG_DK { get; set; }
		public Decimal? NGUYEN_GIA_DK { get; set; }
		//public Decimal? GIA_TRI_CON_LAI_DK { get; set; }
		public Int32? SO_LUONG_TK { get; set; }
		public Decimal? NGUYEN_GIA_TK { get; set; }
		//public Decimal? GIA_TRI_CON_LAI_TK { get; set; }
		public Int32? SO_LUONG_GK { get; set; }
		public Decimal? NGUYEN_GIA_GK { get; set; }
		//public Decimal? GIA_TRI_CON_LAI_GK { get; set; }
		public Int32? SO_LUONG_CK { get; set; }
		public Decimal? NGUYEN_GIA_CK { get; set; }
		//public Decimal? GIA_TRI_CON_LAI_CK { get; set; }
		public string DON_VI_TINH_SO_LUONG { get; set; }
		//add more
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
		public string MA_1 { get; set; }
		public string MA_2 { get; set; }
		public string MA_3 { get; set; }
		public string MA_4 { get; set; }
		public string MA_5 { get; set; }
		public int? TREE_LEVEL { get; set; }
	}
}