using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCTH
{
	public class TS_BCTH_02D_DK_TSNN : BaseViewEntity
	{
		public int TAI_SAN_ID { get; set; }
		public string TAI_SAN_MA { get; set; }
		public string TAI_SAN_TEN { get; set; }
		public DateTime? NGAY_SU_DUNG { get; set; }
		public string TEN_LOAI_TAI_SAN { get; set; }
		public int? LOAI_TAI_SAN_DB_ID { get; set; }
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
		public string CAP_2 { get; set; }
		public string CAP_3 { get; set; }
		public string CAP_4 { get; set; }
		public string CAP_5 { get; set; }
		public string TEN_1 { get; set; }
		public string TEN_2 { get; set; }
		public string TEN_3 { get; set; }
		public string TEN_4 { get; set; }
		public string TEN_5 { get; set; }
		public int? SO_LUONG { get; set; }
		public decimal? DIEN_TICH { get; set; }
		public decimal? NGUYEN_GIA { get; set; }
		public decimal? NGUYEN_GIA_NGAN_SACH { get; set; }
		public decimal? NGUYEN_GIA_KHAC { get; set; }
		public decimal? GIA_TRI_CON_LAI { get; set; }
		//
		public int LOAI_HINH_TAI_SAN_ID { get; set; }
		public int LOAI_DON_VI { get; set; }
		public int DON_VI_ID { get; set; }
		public string TEN_DON_VI { get; set; }
		public string TEN_LOAI_DON_VI { get; set; }
		public string DON_VI_MA { get; set; }
	}
}
