using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCDA
{
    public class TS_BCDA_02F_KT_TSDA:BaseViewEntity
    {
        public decimal? loai_hinh_tai_san_id { get; set; }
        public decimal? loai_tai_san_du_an { get; set; }
        public decimal? phan_loai_tai_san { get; set; }
        public string tai_san_ten { get; set; }
        public string tree_node { get; set; }
        public decimal? tree_level { get; set; }
        public decimal? so_luong_truoc_ky { get; set; }
        public decimal? nguyen_gia_truoc_ky { get; set; }
        public decimal? so_luong_de_xuat { get; set; }
        public decimal? nguyen_gia_de_xuat { get; set; }
        public decimal? so_luong_ket_qua { get; set; }
        public decimal? nguyen_gia_ket_qua { get; set; }
        public decimal? so_luong_cuoi_ky { get; set; }
        public decimal? nguyen_gia_cuoi_ky { get; set; }
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
