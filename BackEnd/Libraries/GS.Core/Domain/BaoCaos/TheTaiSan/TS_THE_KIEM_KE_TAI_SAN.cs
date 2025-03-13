using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TheTaiSan
{
	public class TS_THE_KIEM_KE_TAI_SAN : BaseViewEntity
	{
		public Decimal? TAI_SAN_ID { get; set; }
		public String TAI_SAN_MA { get; set; }
		public String TAI_SAN_TEN { get; set; }
		public String TEN_BO_PHAN { get; set; }
	}
}
