using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCCK
{
    public class TS_BCCK_11B_CK_TSC: BaseViewEntity
    {
        
        public int? TAI_SAN_ID { get; set; }
        public string TAI_SAN_MA { get; set; }
        public string TAI_SAN_TEN { get; set; }
        public decimal? SO_LUONG { get; set; }
        public decimal? DIEN_TICH { get; set; }
        public int? LOAI_HINH_TAI_SAN_ID { get; set; }
        public string LOAI_TAI_SAN_TEN { get; set; }
        public int? LOAI_TAI_SAN_DB_ID { get; set; }
        public int? COUNT_SO_HIEN_TRANG { get; set; }
        public decimal? TSLV { get; set; }
        public decimal? KHONG_KD { get; set; }
        public decimal? KD { get; set; }
        public decimal? CHO_THUE { get; set; }
        public decimal? LDLK { get; set; }
        public decimal? SDHH { get; set; }
        public decimal? KHAC { get; set; }
        public int? TREE_LEVEL { get; set; }
        
        //cấp tài sản
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
        // đơn vị
        public int? LOAI_CAP_DON_VI_ID { get; set; }
        public string TEN_LOAI_CAP_DON_VI { get; set; }
        public decimal? LOAI_HINH_TAI_SAN_NHOM { get; set; }
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
        //
        public string TEN_DON_VI { get; set; }
        public string DON_VI_MA { get; set; }
        public int? DON_VI_DB_ID { get; set; }
        //public decimal? LOAI_HINH_TAI_SAN_DB_ID { get; set; }
    }
}
