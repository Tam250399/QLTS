using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.DanhMuc
{
	public class DonViLichSu:BaseEntity
	{
		public string MA_DVQHNS { get; set; }
		public string TRUOC_MA_DVQHNS { get; set; }
		public string TEN { get; set; }
		public string TRUOC_TEN { get; set; }
		public decimal? LOAI_DON_VI_ID { get; set; }
		public decimal? TRUOC_LOAI_DON_VI_ID { get; set; }
		public bool? LA_DON_VI_NHAP_LIEU { get; set; }
		public bool? TRUOC_LA_DON_VI_NHAP_LIEU { get; set; }
		public decimal? CAP_DON_VI_ID { get; set; }
		public decimal? TRUOC_CAP_DON_VI_ID { get; set; }
		public decimal? DON_VI_CHA_ID { get; set; }
		public decimal? TRUOC_DON_VI_CHA_ID { get; set; }
		public DateTime? NGAY_CAP_NHAT { get; set; }
		public string LY_DO_THAY_DOI { get; set; }
		public decimal? NGUOI_THAY_DOI_ID { get; set; }
		public decimal? DON_VI_ID { get; set; }
	}
}
