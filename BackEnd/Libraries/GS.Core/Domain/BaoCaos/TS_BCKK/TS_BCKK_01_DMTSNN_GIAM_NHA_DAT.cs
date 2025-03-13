using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCKK
{
	public class TS_BCKK_01_DMTSNN_GIAM_NHA_DAT:BaseViewEntity
	{
		public int? TAI_SAN_DAT_ID { get; set; }
		public decimal? DAT_DIEN_TICH { get; set; }
		public decimal? DAT_NGUYEN_GIA_NS { get; set; }
		public decimal? DAT_NGUYEN_GIA_KHAC { get; set; }
		public decimal? DAT_TRU_SO_LAM_VIEC { get; set; }
		public decimal? DAT_HDSN { get; set; }
		public decimal? DAT_HDSN_KD { get; set; }
		public decimal? DAT_HDSN_LAN_CHIEM { get; set; }
		public decimal? DAT_HDSN_BO_TRONG { get; set; }
		public decimal? DAT_KHAC { get; set; }
		public String DAT_DIA_CHI { get; set; }
		public decimal? DAT_NGUYEN_GIA { get; set; }
		public decimal? TAI_SAN_NHA_ID { get; set; }
		public decimal? NHA_DIEN_TICH_XD { get; set; }
		public decimal? NHA_TONG_DIEN_TICH_XD { get; set; }
		public decimal? NHA_NGUYEN_GIA_NS { get; set; }
		public decimal? NHA_NGUYEN_GIA_KHAC { get; set; }
		public decimal? NHA_DT_LAM_VIEC { get; set; }
		public decimal? NHA_HDSN { get; set; }
		public decimal? NHA_HDSN_KD { get; set; }
		public decimal? NHA_HDSN_CHO_THUE { get; set; }
		public decimal? NHA_BO_TRONG { get; set; }
		public decimal? NHA_BI_LAN_CHIEM { get; set; }
		public decimal? NHA_KHAC { get; set; }
		public String TAI_SAN_NHA_MA { get; set; }
		public String TAI_SAN_NHA_TEN { get; set; }
		public decimal? SO_TANG { get; set; }
		public int? NHA_NAM_XAY_DUNG { get; set; }
		public DateTime? NGAY_SU_DUNG { get; set; }
		public String TEN_LOAI_TAI_SAN_NHA { get; set; }
		public String DIA_CHI_NHA { get; set; }
		public decimal? NHA_GTCL { get; set; }
		public decimal? NHA_NGUYEN_GIA { get; set; }

		public String HS_CNQSD_SO { get; set; }
		public DateTime? HS_CNQSD_NGAY { get; set; }
		public String HS_QUYET_DINH_GIAO_SO { get; set; }
		public DateTime? HS_QUYET_DINH_GIAO_NGAY { get; set; }
		public String HS_QUYET_DINH_CHO_THUE_SO { get; set; }
		public DateTime? HS_QUYET_DINH_CHO_THUE_NGAY { get; set; }
		public String HS_HOP_DONG_CHO_THUE_SO { get; set; }
		public DateTime? HS_HOP_DONG_CHO_THUE_NGAY { get; set; }
		public String HS_KHAC { get; set; }
	}
}
