using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCCK
{
    public class TS_BCCK_THMS:BaseViewEntity
    {
		public string TEN_DON_VI_SD { get; set; }
		public int? DON_VI_ID_SD { get; set; }
		public decimal? PHAN_LOAI_ID { get; set; }
		public string TEN_TAI_SAN { get; set; }
		public decimal? DON_GIA { get; set; }
		public string TEN_HINH_THUC_MUA_SAM { get; set; }
		public DateTime? NGAY_DANG_KY { get; set; }
		public decimal? SO_LUONG { get; set; }
		public string NHAN_HIEU { get; set; }
		public string NUOC_SAN_XUAT { get; set; }
		public string GHI_CHU { get; set; }
		public int? TREE_LEVEL { get; set; }
		public int? TREE_LEVEL_DV { get; set; }
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
	}
}
