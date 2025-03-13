using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCCK
{
    public class TS_BCCK_11C_CK_TSC: BaseViewEntity
    {
        // thu hồi
        public Decimal? THU_HOI_SO_LUONG { get; set; }
        public Decimal? THU_HOI_DIEN_TICH { get; set; }
        public Decimal? THU_HOI_NGUYEN_GIA { get; set; }
        public Decimal? THU_HOI_GIA_TRI_CON_LAI { get; set; }
        // điều chuyển
        public Decimal? DIEU_CHUYEN_SO_LUONG { get; set; }
        public Decimal? DIEU_CHUYEN_DIEN_TICH { get; set; }
        public Decimal? DIEU_CHUYEN_NGUYEN_GIA { get; set; }
        public Decimal? DIEU_CHUYEN_GIA_TRI_CON_LAI { get; set; }
        // Chuyển giao về địa phương
        public Decimal? CHUYEN_GIAO_VE_DIA_PHUONG_SO_LUONG { get; set; }
        public Decimal? CHUYEN_GIAO_VE_DIA_PHUONG_DIEN_TICH { get; set; }
        public Decimal? CHUYEN_GIAO_VE_DIA_PHUONG_NGUYEN_GIA { get; set; }
        public Decimal? CHUYEN_GIAO_VE_DIA_PHUONG_GIA_TRI_CON_LAI { get; set; }
        //bán
        public Decimal? BAN_SO_LUONG { get; set; }
        public Decimal? BAN_DIEN_TICH { get; set; }
        public Decimal? BAN_NGUYEN_GIA { get; set; }
        public Decimal? BAN_GIA_TRI_CON_LAI { get; set; }
        //thanh lý
        public Decimal? THANH_LY_SO_LUONG { get; set; }
        public Decimal? THANH_LY_DIEN_TICH { get; set; }
        public Decimal? THANH_LY_NGUYEN_GIA { get; set; }
        public Decimal? THANH_LY_GIA_TRI_CON_LAI { get; set; }
        // tiêu hủy
        public Decimal? TIEU_HUY_SO_LUONG { get; set; }
        public Decimal? TIEU_HUY_DIEN_TICH { get; set; }
        public Decimal? TIEU_HUY_NGUYEN_GIA { get; set; }
        public Decimal? TIEU_HUY_GIA_TRI_CON_LAI { get; set; }
        //mất, hủy hoại
        public Decimal? MAT_HUY_HOAI_SO_LUONG { get; set; }
        public Decimal? MAT_HUY_HOAI_DIEN_TICH { get; set; }
        public Decimal? MAT_HUY_HOAI_NGUYEN_GIA { get; set; }
        public Decimal? MAT_HUY_HOAI_GIA_TRI_CON_LAI { get; set; }
        //khác
        public Decimal? KHAC_SO_LUONG { get; set; }
        public Decimal? KHAC_DIEN_TICH { get; set; }
        public Decimal? KHAC_NGUYEN_GIA { get; set; }
        public Decimal? KHAC_GIA_TRI_CON_LAI { get; set; }
        // tài sản
        public Decimal TAI_SAN_ID { get; set; }
        public String TAI_SAN_TEN { get; set; }
        public int? TREE_LEVEL { get; set; }
        public String TREE_NODE { get; set; }
		//public decimal? PHAN_LOAI_TAI_SAN { get; set; }
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
        public string GHI_CHU { get; set; }
        public int? LOAI_HINH_TAI_SAN_ID { get; set; }
        public decimal? LOAI_HINH_TAI_SAN_NHOM { get; set; }
        public string LOAI_TAI_SAN_TEN { get; set; }
        public int? LOAI_TAI_SAN_DB_ID { get; set; }
    }
}
