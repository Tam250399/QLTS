using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.TaiSanDBApi
{
   public class BienDongApi
    {
        public BienDongApi()
        {
            this.HIEN_TRANG_BD = new List<TaiSanHienTrangSuDungApi>();
            this.NGUON_VON_BD = new List<TaiSanNguonVonApi>();
        }
        public decimal? ID { get; set; }
        public string ID_DB { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
        public String LOAI_TAI_SAN_DON_VI_MA { get; set; }
        public string TEN_TAI_SAN { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        public string DON_VI_BO_PHAN_MA { get; set; }
        public String CHUNG_TU_SO { get; set; }
        public DateTime? CHUNG_TU_NGAY { get; set; }
        public DateTime? NGAY_BIEN_DONG { get; set; }
        public DateTime? NGAY_SU_DUNG { get; set; }
        public Decimal? LOAI_BIEN_DONG_ID { get; set; }
        public String LY_DO_BIEN_DONG_MA { get; set; }
        public decimal? LY_DO_BIEN_DONG_ID { get; set; }
        public String LY_DO_BIEN_DONG_TEN { get; set; }
        public string LOAI_TAI_SAN_MA { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Guid GUID { get; set; }
        public String GHI_CHU { get; set; }
        public DateTime? QUYET_DINH_NGAY { get; set; }
        public DateTime? NGAY_DUYET { get; set; }
        public String QUYET_DINH_SO { get; set; }
        public bool? IS_BIENDONG_CUOI { get; set; }
        public Decimal? TRANG_THAI { get; set; }//1: CHO DUYET; 2: DA DUYET DANG KY; 3: TU CHOI
        public string DON_VI_MA { get; set; }
        public string DATA_JSON { get; set; }
        public string HTSD_JSON { get; set; }
        public Decimal? HINH_THUC_XU_LY_ID { get; set; }
        public List<TaiSanNguonVonApi> NGUON_VON_BD { get; set; }
        public List<TaiSanHienTrangSuDungApi> HIEN_TRANG_BD { get; set; }
        public decimal? THU_TU_BIEN_DONG_ID{ get; set; }
        #region BienDongChiTiet
        public String HINH_THUC_MUA_SAM_MA { get; set; }
        public String MUC_DICH_SU_DUNG_MA { get; set; }
        public decimal? MUC_DICH_SU_DUNG_ID { get; set; }
        public String NHAN_HIEU { get; set; }
        public String SO_HIEU { get; set; }
        public String DIA_BAN_MA { get; set; }
        public Decimal? DIA_BAN_ID { get; set; }
        public Decimal? DAT_TONG_DIEN_TICH { get; set; }
        public Decimal? HTSD_QUAN_LY_NHA_NUOC { get; set; }
        public Decimal? HTSD_HDSN_KINH_DOANH_KHONG { get; set; }
        public Decimal? HTSD_HDSN_KINH_DOANH { get; set; }
        public Decimal? HTSD_CHO_THUE { get; set; }
        public Decimal? HTSD_LIEN_DOANH { get; set; }
        public Decimal? HTSD_SU_DUNG_HON_HOP { get; set; }
        public Decimal? HTSD_SU_DUNG_KHAC { get; set; }
        public Decimal? HM_SO_NAM_CON_LAI { get; set; }
        public Decimal? HM_TY_LE_HAO_MON { get; set; }
        public Decimal? HM_LUY_KE { get; set; }
        public Decimal? GIA_TRI_CON_LAI { get; set; }
        public DateTime? KH_NGAY_BAT_DAU { get; set; }
        public Decimal? KH_THANG_CON_LAI { get; set; }
        public Decimal? KH_GIA_TINH_KHAU_HAO { get; set; }
        public Decimal? KH_GIA_TRI_TRICH_THANG { get; set; }
        public Decimal? KH_LUY_KE { get; set; }
        public Decimal? KH_CON_LAI { get; set; }
        public Decimal? NHA_SO_TANG { get; set; }
        public Decimal? NHA_NAM_XAY_DUNG { get; set; }
        public Decimal? NHA_DIEN_TICH_XD { get; set; }
        public Decimal? NHA_TONG_DIEN_TICH_XD { get; set; }
        public Decimal? VKT_DIEN_TICH { get; set; }
        public Decimal? VKT_THE_TICH { get; set; }
        public Decimal? VKT_CHIEU_DAI { get; set; }
        public String OTO_BIEN_KIEM_SOAT { get; set; }
        public Decimal? OTO_SO_CHO_NGOI { get; set; }
        public String OTO_CHUC_DANH_MA { get; set; }
        public String OTO_NHAN_XE_MA { get; set; }
        public Decimal? OTO_TAI_TRONG { get; set; }
        public Decimal? OTO_CONG_XUAT { get; set; }
        public Decimal? OTO_XI_LANH { get; set; }
        public String OTO_SO_KHUNG { get; set; }
        public String OTO_SO_MAY { get; set; }
        public String THONG_SO_KY_THUAT { get; set; }
        public Decimal? CLN_SO_NAM { get; set; }
        public Decimal? PHI_THU { get; set; }
        public Decimal? PHI_BU_DAP { get; set; }
        public Decimal? PHI_NOP_NGAN_SACH { get; set; }
        public Decimal? PHI_KHAC { get; set; }
        public String DON_VI_NHAN_DIEU_CHUYEN_MA { get; set; }
        public String HINH_THUC_XU_LY_MA { get; set; }
        public Boolean? IS_BAN_THANH_LY { get; set; }
        public Boolean? DIEU_CHUYEN_KEM_THEO { get; set; }
        public String HS_CNQSD_SO { get; set; }
        public DateTime? HS_CNQSD_NGAY { get; set; }
        public String HS_QUYET_DINH_GIAO_SO { get; set; }
        public DateTime? HS_QUYET_DINH_GIAO_NGAY { get; set; }
        public String HS_CHUYEN_NHUONG_SD_SO { get; set; }
        public DateTime? HS_CHUYEN_NHUONG_SD_NGAY { get; set; }
        public String HS_QUYET_DINH_CHO_THUE_SO { get; set; }
        public DateTime? HS_QUYET_DINH_CHO_THUE_NGAY { get; set; }
        public String HS_KHAC { get; set; }
        public String HS_QUYET_DINH_BAN_GIAO { get; set; }
        public DateTime? HS_QUYET_DINH_BAN_GIAO_NGAY { get; set; }
        public String HS_BIEN_BAN_NGHIEM_THU { get; set; }
        public DateTime? HS_BIEN_BAN_NGHIEM_THU_NGAY { get; set; }
        public String HS_HOP_DONG_CHO_THUE_SO { get; set; }
        public DateTime? HS_HOP_DONG_CHO_THUE_NGAY { get; set; }
        public String HS_PHAP_LY_KHAC { get; set; }
        public DateTime? HS_PHAP_LY_KHAC_NGAY { get; set; }
        public String DIA_CHI { get; set; }
        public decimal? DAT_GIA_TRI_QUYEN_SD_DAT { get; set; }
        #endregion
    }
    public class NguonVonJson
    {
        public decimal ID { get; set; }
        public String TEN { get; set; }
        public int? AP_DUNG_ID { get; set; }
        public String GHI_CHU { get; set; }      
        public decimal? GiaTri { get; set; }
        public decimal? LoaiHinhTaiSanId { get; set; }
    }
}
