using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCDA
{
    public class TS_BCDA_02I_KT_TSDA:BaseViewEntity 
    {
        public decimal? loai_hinh_tai_san_id { get; set; }
        public decimal? loai_tai_san_du_an { get; set; }
        public decimal? phan_loai_tai_san { get; set; }
        public string tai_san_ten { get; set; }
        public string tree_node { get; set; }
        public decimal? tree_level { get; set; }
        public decimal? so_luong_dau_ky { get; set; }
        public decimal? nguyen_gia_dau_ky { get; set; }
        public decimal? gia_tri_con_lai_dau_ky { get; set; }
        public decimal? so_luong_tang_trong_ky { get; set; }
        public decimal? nguyen_gia_tang_trong_ky { get; set; }
        public decimal? gia_tri_con_lai_tang_trong_ky { get; set; }
        public decimal? so_luong_giam_trong_ky { get; set; }
        public decimal? nguyen_gia_giam_trong_ky { get; set; }
        public decimal? gia_tri_con_lai_giam_trong_ky { get; set; }
        public decimal? du_an_id { get; set; }
        public string ten_du_an { get; set; }
        public string don_vi_ma { get; set; }
        public decimal? CAP_1 { get; set; }
        public string CAP_2 { get; set; }
        public string CAP_3 { get; set; }
        public string CAP_4 { get; set; }
        public string CAP_5 { get; set; }
        public string TEN_1 { get; set; }
        public string TEN_2 { get; set; }
        public string TEN_3 { get; set; }
        public string TEN_4 { get; set; }
        public string TEN_5 { get; set; }
        public decimal? DON_VI_ID { get; set; }
        public string TEN_DON_VI { get; set; }
		public string MA_1 { get; set; }
		public string MA_2 { get; set; }
		public string MA_3 { get; set; }
		public string MA_4 { get; set; }
		public string MA_5 { get; set; }
	}
}
