using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCQH
{
	public class TS_BCQH_TH_QUAN_LY_SD_KHUONVIEN_DAT_TSC : BaseViewEntity
	{
		public int? TAI_SAN_ID { get; set; }
		public String TAI_SAN_MA { get; set; }
		public String TAI_SAN_TEN { get; set; }
		//
		public int? LOAI_CAP_DON_VI_ID { get; set; }
		public String DV_1 { get; set; }
		public String DV_TEN_1 { get; set; }
		public int? LOAI_DON_VI_ID { get; set; }
		public String TEN_LOAI_DON_VI { get; set; }
		//public Decimal? NGUYEN_GIA { get; set; }
		//

		public Decimal? SO_LUONG { get; set; }
		public Decimal? TONG_DIEN_TICH { get; set; }
		//giá trị hiện trạng sử dụng
		public Decimal? HTSD_TSLV_GIA_TRI { get; set; }
		public Decimal? HTSD_HDSN_GIA_TRI { get; set; }
		public Decimal? HTSD_LDLK_GIA_TRI { get; set; }
		public Decimal? HTSD_CHO_THUE_GIA_TRI { get; set; }
		public Decimal? HTSD_LAM_NHA_O_GIA_TRI { get; set; }
		public Decimal? HTSD_BO_TRONG_GIA_TRI { get; set; }
		public Decimal? HTSD_BI_LAN_CHIEM_GIA_TRI { get; set; }
		public Decimal? HTSD_KHAC_GIA_TRI { get; set; }
		//số cơ sở có hiện trạng sử dụng
		public Decimal? HTSD_TSLV_SO_LUONG { get; set; }
		public Decimal? HTSD_HDSN_SO_LUONG { get; set; }
		public Decimal? HTSD_LDLK_SO_LUONG { get; set; }
		public Decimal? HTSD_CHO_THUE_SO_LUONG { get; set; }
		public Decimal? HTSD_LAM_NHA_O_SO_LUONG { get; set; }
		public Decimal? HTSD_BO_TRONG_SO_LUONG { get; set; }
		public Decimal? HTSD_BI_LAN_CHIEM_SO_LUONG { get; set; }
		public Decimal? HTSD_KHAC_SO_LUONG { get; set; }

		//
		public String GHI_CHU { get; set; }

	}							  
}
