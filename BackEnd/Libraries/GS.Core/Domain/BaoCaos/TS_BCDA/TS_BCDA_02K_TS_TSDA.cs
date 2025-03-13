using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCDA
{
    public class TS_BCDA_02K_TS_TSDA:BaseViewEntity
    {
        public decimal? loai_hinh_tai_san_id {get;set;}
        public decimal? so_luong {get;set;}
        public decimal? dien_tich {get;set;}
        public decimal? so_luong_giao_moi { get; set; }
        public decimal? nguyen_gia_giao_moi { get; set; }
        public decimal? dien_tich_giao_moi { get; set; }
        public decimal? so_luong_tiep_nhan { get; set; }
        public decimal? nguyen_gia_tiep_nhan { get; set; }
        public decimal? dien_tich_tiep_nhan { get; set; }
        public decimal? so_luong_mua_moi { get; set; }
        public decimal? nguyen_gia_mua_moi { get; set; }
        public decimal? dien_tich_mua_moi { get; set; }
        public decimal? so_luong_khac { get; set; }
        public decimal? nguyen_gia_khac { get; set; }
        public decimal? dien_tich_khac { get; set; }
        public decimal? nguon_ngan_sach {get;set;}
        public decimal? nguon_gia_khac {get;set;}
        public decimal? nguon_oda { get; set; }
        public decimal? nguon_vtpcp { get; set; }
        public decimal? phan_loai_tai_san {get;set;}
        public string tai_san_ten {get;set;}
        public decimal? nguyen_gia {get;set;}
        public decimal? gia_tri_con_lai {get;set;}
        public decimal? nam_dua_vao_sd {get;set;}
        public decimal? du_an_id {get;set;}
        public string ten_du_an {get;set;}
        public string ten_don_vi {get;set;}
        public decimal? don_vi_id {get;set;}
        public string tree_node { get; set; }
        public decimal? tree_level { get; set; }
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
    }
}
