using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCKK
{
	public class TS_BCKK_01_DK_TSNN : BaseViewEntity
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
		public Decimal? NAM_XAY_DUNG { get; set; }
		public DateTime? NGAY_SU_DUNG { get; set; }
		public Decimal? NGUON_NGAN_SACH { get; set; }
		public Decimal? NGUON_KHAC { get; set; }
		public Decimal? GIA_TRI_CON_LAI { get; set; }
		public Decimal? SO_TANG { get; set; }
		public Decimal? DIEN_TICH_XAY_DUNG { get; set; }
		public Decimal? TONG_DIEN_TICH_SAN_XAY_DUNG { get; set; }
		public String NHA_DIA_CHI { get; set; }
		//Thông tin đất
		public Decimal? DAT_ID { get; set; }
		//public String DAT_DIA_CHI { get; set; }
		//Hiện trạng sử dụng
		public Decimal? NHA_HT_TRU_SO_LAM_VIEC { get; set; }
		public Decimal? NHA_HT_HOAT_DONG_SU_NGHIEP { get; set; }
		public Decimal? NHA_HT_LAM_NHA_O { get; set; }
		public Decimal? NHA_HT_CHO_THUE { get; set; }
		public Decimal? NHA_HT_BO_TRONG { get; set; }
		public Decimal? NHA_HT_BI_LAN_CHIEM { get; set; }
		public Decimal? NHA_HT_KHAC { get; set; }
		// thông tin đất
		public decimal? DAT_DIEN_TICH { get; set; }
		public decimal? DAT_NGUYEN_GIA_NS { get; set; }
		public decimal? DAT_NGUYEN_GIA_KHAC { get; set; }
		public decimal? DAT_TRU_SO_LAM_VIEC { get; set; }
		public decimal? DAT_HDSN { get; set; }
		public decimal? DAT_HDSN_KD { get; set; }
		public decimal? DAT_HDSN_CHO_THUE { get; set; }
		public decimal? DAT_HDSN_LDLK { get; set; }
		public decimal? DAT_KHAC { get; set; }
	}
}
