using GS.Web.Models.DongBoTaiSan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.ImportTaiSan
{
    public class ImportExcelTaiSanDatNhaModel
    {
        public String TEN { get; set; }
        public String LOAI_TAI_SAN_MA { get; set; }
        public decimal? DAT_GIA_TRI_QUYEN_SD_DAT { get; set; }
        public decimal? NV_NGAN_SACH { get; set; }
        public decimal? NV_NGUON_KHAC { get; set; }
        public decimal? GIA_TRI_CON_LAI { get; set; }
        public String LY_DO_BIEN_DONG_MA { get; set; }//Lý do tăng
        public String NGAY_BIEN_DONG { get; set; }//Ngày kê khai
        public String NGAY_SU_DUNG { get; set; }//Ngày sử dụng  
        //public String DIA_CHI { get; set; }
        public String MA_TINH { get; set; }
        public string MA_HUYEN { get; set; }
        public string MA_XA { get; set; }
        public decimal? DIEN_TICH { get; set; }
        public String TAI_SAN_DAT_MA { get; set; }
        public decimal? NHA_SO_TANG { get; set; }
        public decimal? NAM_XAY_DUNG { get; set; }
        public decimal? DIEN_TICH_XAY_DUNG { get; set; }
        public decimal? DIEN_TICH_SAN_XAY_DUNG { get; set; }
        public decimal? HTSD_HDSN_KINH_DOANH_KHONG { get; set; }
        public decimal? HTSD_HDSN_KINH_DOANH { get; set; }
        public decimal? HTSD_HDSN_LIEN_DOANH { get; set; }
        public decimal? HTSD_HDSN_CHO_THUE { get; set; }
        public decimal? HTSD_QUAN_LY_NHA_NUOC { get; set; }
        public decimal? HTSD_TRU_SO_LAM_VIEC { get; set; }
        public decimal? HTSD_SU_DUNG_HON_HOP { get; set; }
        public decimal? HTSD_SU_DUNG_KHAC { get; set; }
        public decimal? HTSD_DE_O { get; set; }
        public decimal? HTSD_BO_TRONG { get; set; }
        public decimal? HTSD_BI_LAN_CHIEM { get; set; }
        public string DAT_CHUNGNHAN_QUYENSUDUNG { get; set; }
        public string DAT_QUYETDINH_GIAODAT { get; set; }
        public string DAT_QUYETDINH_CHOTHUE { get; set; }
        public string DAT_HOPDONG_THUE { get; set; }
        public string DAT_GIAY_TO_KHAC { get; set; }
        public string MA_DU_AN { get; set; }
        public string Error { get; set; }
    }
    public class ImportExcelTaiSanKhacModel
    {
        public String TEN { get; set; }
        public String LOAI_TAI_SAN_MA { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public decimal? NV_NGAN_SACH { get; set; }
        public decimal? NV_NGUON_KHAC { get; set; }
        public decimal? GIA_TRI_CON_LAI { get; set; }
        public String LY_DO_BIEN_DONG_MA { get; set; }//Lý do tăng
        public String NGAY_BIEN_DONG { get; set; }//Ngày kê khai
        public String NGAY_SU_DUNG { get; set; }//Ngày sử dụng  
        public decimal? GIA_HOA_DON { get; set; }
        public decimal? SO_THUE_DUOC_MIEN { get; set; }
        public string BIEN_KIEM_SOAT { get; set; }
        public string NHAN_HIEU { get; set; }
        public string LOAI_XE { get; set; }
        public decimal? CHO_NGOI_TAI_TRONG { get; set; }
        public decimal? SO_CAU { get; set; }
        public String CHUC_DANH { get; set; }
        public string SO_KHUNG { get; set; }
        public string SO_MAY { get; set; }
        public decimal? XI_LANH { get; set; }
        public string SO_DANG_KY_XE { get; set; }
        public string CO_QUAN_CAP_DANG_KY { get; set; }
        public DateTime? NGAY_DANG_KY { get; set; }
        public string NUOC_SAN_XUAT { get; set; }
        public decimal? NAM_SAN_XUAT { get; set; }
        public decimal? HTSD_HDSN_LIEN_DOANH { get; set; }
        public decimal? HTSD_HDSN_CHO_THUE { get; set; }
        public decimal? HTSD_QUAN_LY_NHA_NUOC { get; set; }
        public decimal? HTSD_SU_DUNG_KHAC { get; set; }
        public decimal? HTSD_HDSN_KINH_DOANH_KHONG { get; set; }
        public decimal? HTSD_HDSN_KINH_DOANH { get; set; }
        public string BO_PHAN_SU_DUNG { get; set; }
        public string MA_DU_AN { get; set; }
        public string Error { get; set; }
    }
}
