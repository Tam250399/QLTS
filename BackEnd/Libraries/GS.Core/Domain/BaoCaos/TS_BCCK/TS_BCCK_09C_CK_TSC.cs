using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCCK
{
    public class TS_BCCK_09C_CK_TSC : BaseViewEntity
    {
        public Decimal TAI_SAN_ID { get; set; }
        public String TAI_SAN_TEN { get; set; }
        public String MA_TAI_SAN_DB { get; set; }
		public string LOAI_TAI_SAN_TEN { get; set; }
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
        public String DON_VI_BO_PHAN_TEN { get; set; }
        public int? SO_LUONG { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? NGUON_NGAN_SACH { get; set; }
        public Decimal? NGUON_KHAC { get; set; }
        public Decimal? GIA_TRI_CON_LAI { get; set; }
        //public bool IsPhucVuChucDanh { get; set; }
        //public bool IsPhucVuCongTacChung { get; set; }
        //public bool IsPhucVuHoatDongDacThu { get; set; }
        public decimal? IsKinhDoanh { get; set; }
        public decimal? IsChoThue { get; set; }
        public decimal? IsLienDoanhLienKet { get; set; }
        public decimal? IsSuDungKhac { get; set; }
        public decimal? IsPhucVuChucDanh { get; set; }
        public decimal? IsPhucVuChung { get; set; }
        public decimal? IsPhucVuDacThu { get; set; }
        //
        public decimal? LOAI_TAI_SAN_DB_ID { get; set; }
    }
}
