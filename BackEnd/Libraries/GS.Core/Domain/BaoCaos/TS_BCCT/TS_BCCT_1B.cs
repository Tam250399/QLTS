using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCCT
{
    public class TS_BCCT_1B : BaseViewEntity
    {
        public int? TAI_SAN_ID { get; set; }
        public string TAI_SAN_MA { get; set; }
        public int? LOAI_TAI_SAN_ID { get; set; }
        public int? BIEN_DONG_ID { get; set; }
        public string TAI_SAN_TEN { get; set; }
        public decimal? DIEN_TICH { get; set; }
        public decimal? TSLV { get; set; }
        public decimal? KHONG_KINH_DOANH { get; set; }
        public decimal? KINH_DOANH { get; set; }
        public decimal? CHO_THUE { get; set; }
        public decimal? LDLK { get; set; }
        public decimal? HON_HOP { get; set; }
        public decimal? KHAC { get; set; }
        public int? LOAI_HINH_TAI_SAN_ID { get; set; }
        public int? TREE_LEVEL { get; set; }
        public int? CAP_1 { get; set; }
        public int? CAP_2 { get; set; }
        public int? CAP_3 { get; set; }
        public int? CAP_4 { get; set; }
        public int? CAP_5 { get; set; }
        public int? NAM_SU_DUNG { get; set; }
        public string CAP_NHA { get; set; }
        public int? SO_TANG { get; set; }
        public string TEN_1 { get; set; }
        public string TEN_2 { get; set; }
        public string TEN_3 { get; set; }
        public string TEN_4 { get; set; }
        public string TEN_5 { get; set; }
    }
}
