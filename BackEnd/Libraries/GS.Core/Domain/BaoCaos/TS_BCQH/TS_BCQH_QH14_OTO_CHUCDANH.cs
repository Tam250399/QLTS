using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCQH
{
	public class TS_BCQH_QH14_OTO_CHUCDANH : BaseViewEntity
	{
		public Decimal TAI_SAN_ID { get; set; }
		public String TAI_SAN_TEN { get; set; }
		public String LOAI_TAI_SAN_TEN { get; set; }
		public Decimal? DON_VI_ID { get; set; }
		public String DON_VI_TEN { get; set; }
		public String CAP_2 { get; set; }
		public string TEN_2 { get; set; }
		//More 
		public String BIEN_KIEM_SOAT { get; set; }
		public String CHUCDANHSUDUNG{ get; set; }
		public Decimal? NGUYEN_GIA { get; set; }
		public Decimal? GIA_TRI_CON_LAI { get; set; }
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
		public int? tree_level_dv { get; set; }
	}
}
