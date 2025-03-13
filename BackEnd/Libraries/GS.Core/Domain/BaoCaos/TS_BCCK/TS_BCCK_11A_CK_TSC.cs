using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCCK
{
    public class TS_BCCK_11A_CK_TSC : BaseViewEntity
    {
        // mua sắm
        public Decimal? MUA_SAM_SO_LUONG { get; set; }
        public Decimal? MUA_SAM_DIEN_TICH { get; set; }
        public Decimal? MUA_SAM_NGUYEN_GIA { get; set; }
        // tiếp nhận
        public Decimal? TIEP_NHAN_SO_LUONG { get; set; }
        public Decimal? TIEP_NHAN_DIEN_TICH { get; set; }
        public Decimal? TIEP_NHAN_NGUYEN_GIA { get; set; }
        //cho thuê
        public Decimal? CHO_THUE_SO_LUONG { get; set; }
        public Decimal? CHO_THUE_DIEN_TICH { get; set; }
        public Decimal? CHO_THUE_NGUYEN_GIA { get; set; }        
        // tài sản
        public Decimal TAI_SAN_ID { get; set; }
        public String TAI_SAN_TEN { get; set; }     
        public int? TREE_LEVEL { get; set; }
        public String TREE_NODE { get; set; }
        // cấp tài sản
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
        public int? LOAI_HINH_TAI_SAN_ID { get; set; }
        public decimal? LOAI_HINH_TAI_SAN_NHOM { get; set; }
        public string LOAI_TAI_SAN_TEN { get; set; }
        public int? LOAI_TAI_SAN_DB_ID { get; set; }
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
