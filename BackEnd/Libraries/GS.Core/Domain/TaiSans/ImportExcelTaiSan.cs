using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.TaiSans
{
    public class ImportExcelTaiSan: BaseEntity
    {

        public String DON_VI_MA { get; set; }
        public String TEN { get; set; }
        public String LOAI_TAI_SAN_MA { get; set; }
        public decimal? NGUYEN_GIA { get; set; }
        public decimal? NV_NGAN_SACH { get; set; }
        public decimal? NV_KHAC { get; set; }
        public decimal? NV_QUY_HDSN { get; set; }
        public decimal? NV_VIEN_TRO { get; set; }
        public decimal? SO_LUONG { get; set; }
        public decimal? HAO_MON_LUY_KE { get; set; }
        public decimal? GIA_TRI_CON_LAI { get; set; }
        public String LY_DO_TANG_MA { get; set; }//Lý do tăng
        public DateTime NGAY_TANG { get; set; }//Ngày kê khai
        public DateTime NGAY_SU_DUNG { get; set; }//Ngày sử dụng  
        public String DIA_CHI { get; set; }
        //public String MA_TINH { get; set; }
        public String MA_HUYEN { get; set; }
        public String MA_XA { get; set; }
        //public decimal? DIEN_TICH { get; set; }
        public String TAI_SAN_DAT_MA { get; set; }
        public decimal? SO_TANG { get; set; }
        public decimal? NAM_XAY_DUNG { get; set; }
        public decimal? DIEN_TICH_DATNHA { get; set; }
        public decimal? TONG_DT_SAN_XD { get; set; }
        public decimal? HTSD_TRU_SO_LAM_VIEC { get; set; }
        public decimal? HTSD_HDSN_KINH_DOANH { get; set; }
        public decimal? HTSD_HDSN_KHONG_KINH_DOANH { get; set; }
        public decimal? HTSD_HDSN_LDLK { get; set; }
        public decimal? HTSD_HDSN_CHO_THUE { get; set; }
        //public decimal? HTSD_QUAN_LY_NHA_NUOC { get; set; }
        public decimal? HTSD_SDHH { get; set; }
        public decimal? HTSD_KHAC { get; set; }
        public decimal? HTSD_DE_O { get; set; }
        public decimal? HTSD_BO_TRONG { get; set; }
        public decimal? HTSD_BI_LAN_CHIEM { get; set; }
        public String HTSD_TAI_SAN_KHAC { get; set; }
        public String PHUONG_THUC_MUA_SAM { get; set; }
        public String HINH_THUC_MUA_SAM { get; set; }
        public String DON_VI_MUA_SAM { get; set; }
        public String BIEN_KIEM_SOAT { get; set; }
        public String NHAN_XE { get; set; }
        public String DONG_XE { get; set; }
        public decimal? SO_CHO_NGOI { get; set; }
        public decimal? TAI_TRONG { get; set; }
        public decimal? SO_CAU_XE { get; set; }
        public String CHUC_DANH_SU_DUNG { get; set; }
        //public DateTime? NGAY_DANG_KY { get; set; }
        public String NUOC_SX { get; set; }
        public decimal? NAM_SX { get; set; }
        public String BO_PHAN_SU_DUNG { get; set; }
        public decimal? TRANG_THAI_ID { get; set; }
        public String MA_DB { get; set; }
        public DateTime? NGAY_TINH_KHAU_HAO { get; set; }
        public Decimal? NGUYEN_GIA_TINH_KHAU_HAO { get; set; }
        public Decimal? SO_THANG_KHAU_HAO { get; set; }
        public Decimal? TY_LE_KHAU_HAO_THANG { get; set; } 
        public Decimal? GIA_TRI_KHAU_HAO { get; set; }
        public Decimal? GIA_TRI_KHAU_HAO_CON_LAI { get; set; }
    }
}
