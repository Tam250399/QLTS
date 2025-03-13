using System;

namespace GS.Core.Domain.BaoCaos.TS_BCCK
{
	public class TS_BCCK_DIEUCHUYEN_BAN_GIAO:BaseViewEntity
	{
		public String TAI_SAN_TEN { get; set; }
		public String NHAN_HIEU { get; set; }
		public String NUOC_SAN_XUAT { get; set; }
		public int? NAM_SAN_XUAT { get; set; }
		public decimal? SO_LUONG { get; set; }
		public String DON_VI_MUA_SAM_TAP_TRUNG { get; set; }
		public String HINH_THUC_MUA_SAM { get; set; }
		public DateTime? NGAY_DIEU_CHUYEN { get; set; }
		public String DON_VI_NHAN_DIEU_CHUYEN { get; set; }
		public String GHI_CHU { get; set; }
		public decimal? DON_GIA { get; set; }
	}
}