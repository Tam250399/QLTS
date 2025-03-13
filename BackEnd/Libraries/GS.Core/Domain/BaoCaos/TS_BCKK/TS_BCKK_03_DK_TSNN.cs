using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCKK
{
	public class TS_BCKK_03_DK_TSNN : BaseViewEntity
	{

		public Decimal? TAI_SAN_ID { get; set; }
		public String TAI_SAN_TEN { get; set; }
		public string LOAI_TAI_SAN_TEN { get; set; }
		public int? TREE_LEVEL { get; set; }
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
		//More 
		public String KY_HIEU { get; set; }
		public String NUOC_SAN_XUAT { get; set; }
		public Decimal? NAM_SAN_XUAT { get; set; }
		public DateTime? NGAY_SU_DUNG { get; set; }
		public Decimal? NGUON_NGAN_SACH { get; set; }
		public Decimal? NGUON_KHAC { get; set; }
		public Decimal? GIA_TRI_CON_LAI { get; set; }

		public String IsQuanLyNhaNuoc { get; set; }
		public String IsKinhDoanh { get; set; }
		public String IsKhongKinhDoanh { get; set; }
		public String IsSuDungKhac { get; set; }

	}
}
