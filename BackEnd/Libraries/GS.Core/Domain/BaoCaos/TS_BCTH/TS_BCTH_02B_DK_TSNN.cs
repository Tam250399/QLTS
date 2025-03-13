using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCTH
{
	public class TS_BCTH_02B_DK_TSNN:BaseViewEntity
	{
		public int? TAI_SAN_ID { get; set; }
		public int? LOAI_TAI_SAN_ID { get; set; }
		public int? LOAI_TAI_SAN_DB_ID { get; set; }
		public int? BIEN_DONG_ID { get; set; }
		public string TAI_SAN_TEN { get; set; }
		public string TAI_SAN_MA { get; set; }
		public DateTime? NGAY_SU_DUNG { get; set; }
		public decimal? DIEN_TICH { get; set; }
		public decimal? QUAN_LY_NHA_NUOC { get; set; }
		public decimal? KHONG_KINH_DOANH { get; set; }
		public decimal? KINH_DOANH { get; set; }
		public decimal? CHO_THUE { get; set; }
		public decimal? LDLK { get; set; }
		public decimal? SU_DUNG_HON_HOP { get; set; }
		public decimal? KHAC { get; set; }
		//public decimal? HTSD { get; set; }
		//public decimal? KHAC { get; set; }
		//public decimal? LAM_NHA_O { get; set; }
		//public decimal? CHO_THUE { get; set; }
		//public decimal? BO_TRONG { get; set; }
		//public decimal? BI_LAN_CHIEM { get; set; }
		public int? LOAI_HINH_TAI_SAN_ID { get; set; }
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
		public int? CAP_2 { get; set; }
		public int? CAP_3 { get; set; }
		public int? CAP_4 { get; set; }
		public int? CAP_5 { get; set; }
		public int? NAM_SU_DUNG { get; set; }
		public string CAP_NHA { get; set; }
		public int? SO_TANG { get; set; }
		public string TEN_1 { get; set; }
		public string TEN_2 { get; set; }
		public string TEN_3 { get; set; }
		public string TEN_4 { get; set; }
		public string TEN_5 { get; set; }
		//
		public int? LOAI_DON_VI { get; set; }
		public int? DON_VI_ID { get; set; }
		public string TEN_DON_VI { get; set; }
		public string TEN_LOAI_DON_VI { get; set; }
		public int? SO_LUONG { get; set; }
		public string DON_VI_MA { get; set; }
		//
		public decimal? DIEN_TICH_KHAI_THAC { get; set; }
		public decimal? DIEN_TICH_CHUA_KHAI_THAC { get; set; }
	}
}
