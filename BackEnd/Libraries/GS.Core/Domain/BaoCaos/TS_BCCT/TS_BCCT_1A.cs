using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCCT
{
	public class TS_BCCT_1A : BaseViewEntity
	{
		public int TAI_SAN_ID { get; set; }
		public string TAI_SAN_MA { get; set; }
		//public string MA_DONG_BO { get; set; }
		public string TAI_SAN_TEN { get; set; }
		public string TEN_LOAI_TAI_SAN { get; set; }
		public int? TREE_LEVEL { get; set; }
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
		public int? NAM_DUA_VAO_SD { get; set; }
		public int? SO_CHO { get; set; }
		public int? TAI_TRONG { get; set; }
		public string THONG_SO_KY_THUAT { get; set; }
		public int? SO_TANG { get; set; }
		public int? SO_LUONG { get; set; }
		public decimal? DIEN_TICH { get; set; }
		public decimal? NGUYEN_GIA { get; set; }
		public decimal? NGUYEN_GIA_NGAN_SACH { get; set; }
		public decimal? NGUYEN_GIA_KHAC { get; set; }
		public decimal? GIA_TRI_CON_LAI { get; set; }
		//
		public int LOAI_HINH_TAI_SAN_ID { get; set; }
		//public int BIEN_DONG_ID { get; set; }
	}
}
