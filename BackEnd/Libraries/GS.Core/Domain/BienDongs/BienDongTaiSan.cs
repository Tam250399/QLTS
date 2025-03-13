using GS.Core.Domain.TaiSans;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GS.Core.Domain.BienDongs
{

    public class DongBoApi_BienDongTaiSan : BaseViewEntity
    {
        #region tài sản
        [NotMapped]
        public Decimal? ID { get; set; }
        public Decimal? HM_TY_LE_TAI_SAN { get; set; }
        public Decimal? NUOC_SAN_XUAT_ID { get; set; }
        public Decimal? NAM_SAN_XUAT { get; set; }
        public Decimal? MIEN_THUE_SO_TIEN { get; set; }
        public Decimal? GIA_MUA_TIEP_NHAN { get; set; }
        public string NUOC_SAN_XUAT { get; set; }
        public string MA_TAI_SAN_DB { get; set; }
        public string NGAY_NHAP_TAI_SAN { get; set; }
        public Decimal? NGUYEN_GIA_BAN_DAU { get; set; }
        #region TS_NHA
        public Decimal? TS_NHA_DIEN_TICH_XAY_DUNG { get; set; }
        public string TS_NHA_TAI_SAN_DAT_MA { get; set; }
        #endregion
        #region TS_OTO
        public Decimal? TS_OTO_DUNG_TICH { get; set; }
        public string TS_OTO_SO_KHUNG { get; set; }
        public string TS_OTO_SO_MAY { get; set; }
        public string TS_OTO_CHUC_DANH_TEN { get; set; }
        public Decimal? TS_OTO_SO_CAU { get; set; }
        public string TS_OTO_GCN_DANG_KY { get; set; }
        public string TS_OTO_CO_QUAN_CAP_DANG_KY { get; set; }
        #endregion
        #region TS_VKT
        public Decimal? TS_VKT_DIEN_TICH { get; set; }
        public Decimal? TS_VKT_THE_TICH { get; set; }
        public Decimal? TS_VKT_CHIEU_DAI { get; set; }
        #endregion
        #region TS_MAY_MOC
        public string TS_THONG_SO_KY_THUAT { get; set; }
        #endregion
        #region TS_CLN
        public Decimal? TS_CLN_NAM_SINH { get; set; }
        #endregion
        #endregion
        #region biến động
        public string ID_DB { get; set; }
        public Decimal? TAI_SAN_ID { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? DB_DON_VI_ID { get; set; }
        public string TEN_DON_VI { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? DB_LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public string TEN_TAI_SAN { get; set; }
        public string TEN_TAI_SAN_BD { get; set; }
        public string MA_TAI_SAN { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public string CHUNG_TU_SO { get; set; }
        public string CHUNG_TU_NGAY { get; set; }
        public string NGAY_BIEN_DONG { get; set; }
        public string NGAY_SU_DUNG { get; set; }
        public Decimal? DU_AN_ID { get; set; }
        public Decimal? DB_DU_AN_ID { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        public Decimal? DB_DON_VI_BO_PHAN_ID { get; set; }
        public Decimal? LOAI_BIEN_DONG_ID { get; set; }
        public string LY_DO_BIEN_DONG_MA { get; set; }
        public Decimal? DB_LY_DO_BIEN_DONG_ID { get; set; }
        public Decimal? LY_DO_BIEN_DONG_ID { get; set; }
        public string TEN_LY_DO { get; set; }
        public string NGAY_TAO { get; set; }
        
        public Guid GUID { get; set; }
        public string GHI_CHU { get; set; }
        public string QUYET_DINH_NGAY { get; set; }
        public string NGAY_DUYET { get; set; }
        public string NGUOI_DUYET { get; set; }
        public string QUYET_DINH_SO { get; set; }
        #region BienDongChiTiet
        public string HINH_THUC_MUA_SAM_MA { get; set; }
        public Decimal? HINH_THUC_MUA_SAM_DB_ID { get; set; }
        public string HINH_THUC_MUA_SAM_TEN { get; set; }

        public string MUC_DICH_SU_DUNG_MA { get; set; }
        public Decimal? DB_MUC_DICH_SU_DUNG_ID { get; set; }
        public Decimal? MUC_DICH_SU_DUNG_ID { get; set; }
        public string NHAN_HIEU { get; set; }
        public string SO_HIEU { get; set; }
        public string DIA_BAN_MA { get; set; }
        public string DIA_BAN_TEN { get; set; }
        public Decimal? DIA_BAN_ID { get; set; }
        public Decimal? DB_QUOC_GIA_ID { get; set; }
        public string DIA_CHI { get; set; }
        public Decimal? DB_DIA_BAN_ID { get; set; }
        public Decimal? DAT_TONG_DIEN_TICH { get; set; }
        public Decimal? DAT_GIA_TRI_QUYEN_SD_DAT { get; set; }
        public Decimal? HM_SO_NAM_CON_LAI { get; set; }
        public Decimal? HM_TY_LE_HAO_MON { get; set; }
        public Decimal? HM_LUY_KE { get; set; }
        public Decimal? HM_GIA_TRI_CON_LAI { get; set; }
        public string KH_NGAY_BAT_DAU { get; set; }
        public Decimal? KH_THANG_CON_LAI { get; set; }
        public Decimal? KH_GIA_TINH_KHAU_HAO { get; set; }
        public Decimal? KH_GIA_TRI_TRICH_THANG { get; set; }
        public Decimal? KH_CON_LAI { get; set; }
       
        public Decimal? KH_LUY_KE { get; set; }
        
        public Decimal? NHA_SO_TANG { get; set; }
        public Decimal? NHA_NAM_XAY_DUNG { get; set; }
        public Decimal? NHA_DIEN_TICH_XD { get; set; }
        public Decimal? NHA_TONG_DIEN_TICH_XD { get; set; }
        public Decimal? VKT_DIEN_TICH { get; set; }
        public Decimal? VKT_THE_TICH { get; set; }
        public Decimal? VKT_CHIEU_DAI { get; set; }
        public string OTO_BIEN_KIEM_SOAT { get; set; }
        public Decimal? OTO_SO_CHO_NGOI { get; set; }
        public Decimal? OTO_CHUC_DANH_ID { get; set; }
        public Decimal? OTO_CHUC_DANH_DB_ID { get; set; }
        public string OTO_CHUC_DANH_TEN { get; set; }
        public string OTO_NHAN_XE_MA { get; set; }
        public string OTO_NHAN_XE_TEN { get; set; }
        public Decimal? OTO_NHAN_XE_ID { get; set; }
        public Decimal? OTO_NHAN_XE_DB_ID { get; set; }
        public Decimal? OTO_TAI_TRONG { get; set; }
        public Decimal? OTO_CONG_XUAT { get; set; }
        public Decimal? OTO_XI_LANH { get; set; }
        public string OTO_SO_KHUNG { get; set; }
        public string OTO_SO_MAY { get; set; }
        public string THONG_SO_KY_THUAT { get; set; }
        public Decimal? CLN_SO_NAM { get; set; }
        public string DON_VI_NHAN_DIEU_CHUYEN_MA { get; set; }
        public Decimal? DON_VI_NHAN_DIEU_CHUYEN_DB_ID { get; set; }
        public string DON_VI_NHAN_DIEU_CHUYEN_TEN { get; set; }
        public string OTO_CHUC_DANH_MA { get; set; }
        public string HINH_THUC_XU_LY_MA { get; set; }
        public Decimal? IS_BAN_THANH_LY { get; set; }
        public Decimal? HINH_THUC_XU_LY_ID { get; set; }
        public Decimal? PHI_THU { get; set; }
        public Decimal? PHI_BU_DAP { get; set; }
        public Decimal? PHI_NOP_NGAN_SACH { get; set; }
        #endregion
        #region Hiện trạng sử dụng
        [NotMapped]
        public List<DongBoApi_TaiSanHienTrangSuDung> LST_HIEN_TRANG
        {
            get => LST_HIEN_TRANG_JSON.toEntities<DongBoApi_TaiSanHienTrangSuDung>();
            set => LST_HIEN_TRANG_JSON = value.toStringJson();
        }
        public string LST_HIEN_TRANG_JSON { get; set; }
        #endregion
        #region Nguồn vốn
        public Decimal? NV_NGAN_SACH { get; set; }
        public Decimal? NV_HDSN { get; set; }
        public Decimal? NV_NGUON_KHAC { get; set; }
        public Decimal? NV_VIEN_TRO { get; set; }
        #endregion
        #region Hồ sơ giấy tờ
        public string HS_CNQSD_SO { get; set; }
        public string HS_CNQSD_NGAY { get; set; }
        public string HS_QUYET_DINH_GIAO_SO { get; set; }
        public string HS_QUYET_DINH_GIAO_NGAY { get; set; }
        [NotMapped]
        public string HS_CHUYEN_NHUONG_SD_SO { get; set; }
        [NotMapped]
        public string HS_CHUYEN_NHUONG_SD_NGAY { get; set; }
        public string HS_QUYET_DINH_CHO_THUE_SO { get; set; }
        public string HS_QUYET_DINH_CHO_THUE_NGAY { get; set; }
        [NotMapped]
        public string HS_KHAC { get; set; }
        public string HS_QUYET_DINH_BAN_GIAO { get; set; }
        public string HS_QUYET_DINH_BAN_GIAO_NGAY { get; set; }
        public string HS_BIEN_BAN_NGHIEM_THU { get; set; }
        public string HS_BIEN_BAN_NGHIEM_THU_NGAY { get; set; }
        public string HS_PHAP_LY_KHAC { get; set; }
        public string HS_PHAP_LY_KHAC_NGAY { get; set; }
        public string HS_HOP_DONG_CHO_THUE_SO { get; set; }
        public string HS_HOP_DONG_CHO_THUE_NGAY { get; set; }
        [NotMapped]
        public Decimal? HS_DAT_GIA_TRI_QUYEN_SD_DAT { get; set; }
        #endregion
        #endregion
    }
    public class DongBoApi_TaiSanHienTrangSuDung : BaseViewEntity
    {
        public Decimal? TAI_SAN_ID { get; set; }
        public Decimal? BIEN_DONG_ID { get; set; }
        public Decimal? NHOM_HIEN_TRANG_ID { get; set; }
        public Decimal? KIEU_DU_LIEU_ID { get; set; }
        public string TEN_HIEN_TRANG { get; set; }
        public string GIA_TRI_TEXT { get; set; }
        public Decimal? GIA_TRI_NUMBER { get; set; }
        public Decimal? GIA_TRI_CHECKBOX { get; set; }
        public Decimal? HIEN_TRANG_ID { get; set; }
        public Decimal? HIEN_TRANG_DB_ID { get; set; }
    }    
    public class DongBoApi_TaiSanNguonVon : BaseViewEntity
    {
        public Decimal? TAI_SAN_ID { get; set; }
        public Decimal? NGUON_VON_ID { get; set; }
        public Decimal? GIA_TRI { get; set; }
        public Decimal? BIEN_DONG_ID { get; set; }
    }    
}
