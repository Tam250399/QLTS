using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TheTaiSan
{
	public class THE_TSCD_TT133 : BaseViewEntity
	{
		public String TAI_SAN_MA { get; set; }
		public Decimal? TAI_SAN_ID { get; set; }
		public String TAI_SAN_TEN { get; set; }
		public int? NAM_SAN_XUAT { get; set; }
		public int? NAM_HAO_MON { get; set; }
		public DateTime? NGAY_SU_DUNG { get; set; }
		public String NUOC_SAN_XUAT_TEN { get; set; }
		public String BO_PHAN_SU_DUNG_TEN { get; set; }
		public Decimal? HM_TONG_NGUYEN_GIA { get; set; }
		public Decimal? HM_GIA_TRI_HAO_MON { get; set; }
		public Decimal? HM_LUY_KE_HAO_MON { get; set; }
		public Decimal? KH_TONG_NGUYEN_GIA { get; set; }
		public Decimal? KH_GIA_TRI_KHAU_HAO_NAM { get; set; }
		public Decimal? KH_LUY_KE_KHAU_HAO { get; set; }
		public Decimal? CONG_SUAT_DIEN_TICH { get; set; }
		public String QUYET_DINH_SO { get; set; }
		public DateTime? QUYET_DINH_NGAY { get; set; }
		public String LY_DO_BIEN_DONG_TEN { get; set; }
		public String CHUNG_TU_SO { get; set; }
		public String DIEN_GIAI { get; set; }
		public string LY_DO_GIAM { get; set; }
		public string QD_GIAM { get; set; }
	}
}
