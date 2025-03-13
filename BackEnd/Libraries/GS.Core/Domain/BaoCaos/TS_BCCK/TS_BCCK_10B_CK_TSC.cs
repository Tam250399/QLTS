using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCCK
{
	public class TS_BCCK_10B_CK_TSC : BaseViewEntity
	{

		public int TAI_SAN_ID { get; set; }
		public string TAI_SAN_MA { get; set; }
		public string TAI_SAN_TEN { get; set; }
		//phân cấp loại tài sản
		public int? LOAI_HINH_TAI_SAN_ID { get; set; }
		public decimal? LOAI_HINH_TAI_SAN_NHOM { get; set; }
		public string LOAI_TAI_SAN_TEN { get; set; }
		public int? LOAI_TAI_SAN_DB_ID { get; set; }
		public int? COUNT_SO_HIEN_TRANG { get; set; }
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
		//phân cấp đơn vị
		public int? TREE_LEVEL_DV { get; set; }
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
		//dữ liệu báo cáo
		public Decimal? SO_LUONG { get; set; }
		public Decimal? DIEN_TICH { get; set; }
		public Decimal? HTSD_QLNN { get; set; }
		public Decimal? HTSD_KHONG_KINH_DOANH { get; set; }
		public Decimal? HTSD_KINH_DOANH { get; set; }
		public Decimal? HTSD_CHO_THUE { get; set; }
		public Decimal? HTSD_LD_LK { get; set; }
		public Decimal? HTSD_SU_DUNG_HON_HOP { get; set; }
		public Decimal? HTSD_KHAC { get; set; }
		// more
		//public decimal? LOAI_HINH_TAI_SAN_DB_ID { get; set; }
	}
}
