using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.CongCuKiemTra
{
	public class DM_TS_KIEM_TRA_LAI_SO_LIEU : BaseViewEntity
	{
		public decimal? TAI_SAN_ID { get; set; }
		public string TAI_SAN_MA { get; set; }
		public string TAI_SAN_TEN { get; set; }
		public string THONG_SO { get; set; }
		public Decimal? DIEN_TICH { get; set; }
		public Decimal? CHO_NGOI_TAI_TRONG { get; set; }
		public Decimal? NGUYEN_GIA { get; set; }
		public string TEN_DON_VI { get; set; }
		public string NGUOI_NHAP { get; set; }
		public int? SO_CHO { get; set; }
		public int? TAI_TRONG { get; set; }
		public int? SO_TANG { get; set; }
		public int LOAI_HINH_TAI_SAN_ID { get; set; }

	}
}
