using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCCK
{
    public class TS_BCCK_11D_CK_TSC: BaseViewEntity
    {
        // kinh doanh
        public Decimal? KD_SO_LUONG { get; set; }
        public Decimal? KD_DIEN_TICH { get; set; }
        public Decimal? KD_GIA_TRI_CON_LAI { get; set; }
        public Decimal? KD_NGUYEN_GIA { get; set; }
        public Decimal? KD_TIEN_THU_DUOC { get; set; }
        // cho thuê
        public Decimal? CT_SO_LUONG { get; set; }
        public Decimal? CT_DIEN_TICH { get; set; }
        public Decimal? CT_GIA_TRI_CON_LAI { get; set; }
        public Decimal? CT_NGUYEN_GIA { get; set; }
        public Decimal? CT_TIEN_THU_DUOC { get; set; }
        // liên loanh, lIÊN KẾT
        public Decimal? LDLK_SO_LUONG { get; set; }
        public Decimal? LDLK_DIEN_TICH { get; set; }
        public Decimal? LDLK_GIA_TRI_CON_LAI { get; set; }
        public Decimal? LDLK_NGUYEN_GIA { get; set; }
        public Decimal? LDLK_TIEN_THU_DUOC { get; set; }
        // Khai thác khác
        public Decimal? KHAC_SO_LUONG { get; set; }
        public Decimal? KHAC_DIEN_TICH { get; set; }
        public Decimal? KHAC_GIA_TRI_CON_LAI { get; set; }
        public Decimal? KHAC_NGUYEN_GIA { get; set; }
        public Decimal? KHAC_TIEN_THU_DUOC { get; set; }
       
        // tài sản
        public Decimal TAI_SAN_ID { get; set; }
        public String TAI_SAN_TEN { get; set; }
		//public decimal? PHAN_LOAI_TAI_SAN { get; set; }
		public int? TREE_LEVEL { get; set; }
        public String TREE_NODE { get; set; }
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
        public int? LOAI_HINH_TAI_SAN_ID { get; set; }
        public decimal? LOAI_HINH_TAI_SAN_NHOM { get; set; }
        public string LOAI_TAI_SAN_TEN { get; set; }
        public int? LOAI_TAI_SAN_DB_ID { get; set; }
    }
}
