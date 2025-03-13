using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCCK
{
    public class TS_BCCK_09D_CK_TSC : BaseViewEntity
	{
		public Decimal TAI_SAN_ID { get; set; }
		public String TAI_SAN_TEN { get; set; }
		public string LOAI_TAI_SAN_TEN { get; set; }
		public int? LOAI_TAI_SAN_DB_ID { get; set; }
		public int? TREE_LEVEL { get; set; }
		public int? CAP_1 { get; set; }
		public String CAP_2 { get; set; }
		public String CAP_3 { get; set; }
		public String CAP_4 { get; set; }
		public String CAP_5 { get; set; }
		public string TEN_1 { get; set; }
		public string TEN_2 { get; set; }
		public string TEN_3 { get; set; }
		public string TEN_4 { get; set; }
		public string TEN_5 { get; set; }
        //More
        public Decimal? NGUON_NGAN_SACH { get; set; }
        public Decimal? NGUON_KHAC { get; set; }
        public Decimal? GIA_TRI_CON_LAI { get; set; }
        //public String LY_DO_BIEN_DONG_TEN { get; set; }
        public String HINH_THUC_XU_LY_TEN { get; set; }
		public Decimal? PHI_THU { get; set; }
		public Decimal? PHI_NOP_NGAN_SACH { get; set; } // phí nộp tài khoản tạm giữ
		public Decimal? PHI_BU_DAP { get; set; } //chi phí xử lý
		public Decimal? IsDieuChuyen { get; set; }
        public Decimal? IsBan { get; set; }
        public Decimal? IsThanhLy { get; set; }
        public Decimal? IsTieuHuy { get; set; }
        public Decimal? IsXuLyMatHuyHoai { get; set; }
        public Decimal? IsXuLyKhac { get; set; }
        public Decimal? IsChuyenGiaoVeDiaPhuong { get; set; }
        public Decimal? IsBiThuHoi { get; set; }
        public String GHI_CHU { get; set; }
	}
}
