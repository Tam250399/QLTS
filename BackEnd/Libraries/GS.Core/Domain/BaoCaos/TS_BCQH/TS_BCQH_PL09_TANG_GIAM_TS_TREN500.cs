using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCQH
{
	public class TS_BCQH_PL09_TANG_GIAM_TS_TREN500 : BaseViewEntity
	{
		public Decimal TAI_SAN_ID { get; set; }
		public String TAI_SAN_TEN { get; set; }
		public int? CAP_1 { get; set; }
		public String CAP_2 { get; set; }
		public string TEN_1 { get; set; }
		public string TEN_2 { get; set; }
		//More 
		public Decimal? SO_LUONG { get; set; }
		public Decimal? NGUYEN_GIA { get; set; }
		public Decimal? GIA_TRI_CON_LAI { get; set; }
	}
}
