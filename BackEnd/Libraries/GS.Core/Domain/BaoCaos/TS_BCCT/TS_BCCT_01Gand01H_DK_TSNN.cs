using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCCT
{
	public enum BanOrThanhLy
	{
		TatCa = 0,
		Ban =  1,
		ThanhLy = 2
	}
	public class TS_BCCT_01Gand01H_DK_TSNN:BaseViewEntity
	{
		public int? TAI_SAN_ID { get; set; }
		public String TAI_SAN_TEN { get; set; }
		public String TAI_SAN_MA { get; set; }
		public String TEN_LOAI_TAI_SAN { get; set; }
		public int? LOAI_HINH_TAI_SAN_ID { get; set; }
		public int? TREE_LEVEL { get; set; }
		public int? CAP_1 { get; set; }
		public String CAP_2 { get; set; }
		public String CAP_3 { get; set; }
		public String CAP_4 { get; set; }
		public String CAP_5 { get; set; }
		public String TEN_1 { get; set; }
		public String TEN_2 { get; set; }
		public String TEN_3 { get; set; }
		public String TEN_4 { get; set; }
		public String TEN_5 { get; set; }
		public int? HINH_THUC_XU_LY_ID { get; set; }
		public String HINH_THUC_XU_LY_TEN { get; set; }
		public decimal? PHI_THU { get; set; }
		public String QUYET_DINH_SO { get; set; }
		public DateTime? QUYET_DINH_NGAY { get; set; }
		public int? SO_LUONG { get; set; }
		public decimal? NGUYEN_GIA { get; set; }
		public decimal? GIA_TRI_CON_LAI { get; set; }
	}
}
