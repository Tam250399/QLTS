using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCCK
{
    public class TS_BCCK_09DD_CK_TSC : BaseViewEntity
    {
        public Decimal? TAI_SAN_ID { get; set; }
        public String TAI_SAN_TEN { get; set; }
        public string LOAI_TAI_SAN_TEN { get; set; }
        public int? LOAI_TAI_SAN_DB_ID { get; set; }
        public Decimal? TREE_LEVEL { get; set; }
        public Decimal? CAP_1 { get; set; }
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
        public Decimal? KD_SO_LUONG_DIEN_TICH { get; set; }
        public String KD_HINH_THUC_TEN { get; set; }
        public Decimal? KD_SO_TIEN_THU_TRONG_NAM { get; set; }
        public Decimal? CT_DON_GIA_THUE { get; set; }
        public Decimal? CT_SO_LUONG_DIEN_TICH { get; set; }
        public String CT_DON_VI_THUE_TEN { get; set; }
        public String CT_HOP_DONG { get; set; }
        public String CT_THOI_HAN_THUE { get; set; }
        public Decimal? CT_SO_TIEN_THU_TRONG_NAM { get; set; }
        public String CT_PHUONG_THUC_TEN { get; set; }
        public String LDLK_DOI_TAC_TEN { get; set; }
        public Decimal? LDLK_SO_LUONG_DIEN_TICH { get; set; }
        public String LDLK_HINH_THUC_TEN { get; set; }
        public String LDLK_HOP_DONG { get; set; }
        public String LDLK_THOI_HAN { get; set; }
        public Decimal? LDLK_SO_TIEN_THU_TRONG_NAM { get; set; }

    }
}
