using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCQH
{
	public class TS_BCQH_TH_QUAN_LY_SD_OTO_TS_KHAC_TSC : BaseViewEntity
	{
		public String TAI_SAN_MA { get; set; }
		public int? LOAI_CAP_DON_VI_ID { get; set; }
		public String DV_1 { get; set; }
		public String DV_TEN_1 { get; set; }
		public Decimal? LOAI_DON_VI_ID { get; set; }
		public String LOAI_DON_VI_TEN { get; set; }
		//thông tin ô tô
		public Decimal? OTO_SO_LUONG { get; set; }
		public Decimal? OTO_NGUON_NS { get; set; }
		public Decimal? OTO_NGUON_KHAC { get; set; }
		//public Decimal? OTO_GIA_TRI_CON_LAI { get; set; }
		public Decimal? OTO_HTSD_QLNN { get; set; }//ô tô hiện trạng sử dụng - quản lý nhà nước
		public Decimal? OTO_HTSD_KINH_DOANH { get; set; }
		public Decimal? OTO_HTSD_KHONG_KINH_DOANH { get; set; }
		public Decimal? OTO_HTSD_KHAC { get; set; }
		//public String OTO_GHI_CHU { get; set; }

		//thông tin tài sản khác đất, nhà, ô tô có nguyên giá trên 500tr
		//thông tin ô tô
		public Decimal? TSK_SO_LUONG { get; set; }
		public Decimal? TSK_NGUON_NS { get; set; }
		public Decimal? TSK_NGUON_KHAC { get; set; }
		//public Decimal? TSK_GIA_TRI_CON_LAI { get; set; }
		public Decimal? TSK_HTSD_QLNN { get; set; }//ô tô hiện trạng sử dụng - quản lý nhà nước
		public Decimal? TSK_HTSD_KINH_DOANH { get; set; }
		public Decimal? TSK_HTSD_KHONG_KINH_DOANH { get; set; }
		public Decimal? TSK_HTSD_KHAC { get; set; }

		//public String TSK_GHI_CHU { get; set; }

	}
}
