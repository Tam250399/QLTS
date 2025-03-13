using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GS.WebTool.Models
{
    [XmlRoot(DataType = "TaiSanDongBoApi")]
    public class TaiSanDongBoModel
    {
        public String TEN_TAI_SAN { get; set; }
        public String MA_TAI_SAN { get; set; }
        public String LOAI_TAI_SAN_TEN { get; set; }
        public DateTime? NGAY_NHAP { get; set; }
        public DateTime? NGAY_DUYET { get; set; }
        public DateTime? NGAY_SU_DUNG { get; set; }
        public String LY_DO_BIEN_DONG_MA { get; set; }
        public String LY_DO_BIEN_DONG_TEN { get; set; }
        public String MA_LOAI_TAI_SAN { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? NGUYEN_GIA_BAN_DAU { get; set; }
        public String MA_DON_VI { get; set; }
        public Decimal? TRANG_THAI { get; set; }
        public String GHI_CHU { get; set; }
        public String DON_VI_BO_PHAN_MA { get; set; }
        public String NUOC_SAN_XUAT_MA { get; set; }
        public Decimal? NAM_SAN_XUAT { get; set; }
        public Decimal? GIA_MUA_TIEP_NHAN { get; set; }
        public String QUYET_DINH_SO { get; set; }
        public DateTime? QUYET_DINH_NGAY { get; set; }
        public String CHUNG_TU_SO { get; set; }
        public DateTime? CHUNG_TU_NGAY { get; set; }
        public String MA_DU_AN { get; set; }
        public String LST_BIEN_DONG_JSON { get; set; }
        public String LST_HIEN_TRANG_JSON { get; set; }
        public String LST_NGUON_VON_JSON { get; set; }
        public String KT_HAO_MON_JSON { get; set; }
        public String TAI_SAN_JSON { get; set; }
        public Decimal? CHE_DO_HAO_MON_ID { get; set; }
        public String MA_NHOM_DON_VI { get; set; }
        public Decimal? NAM { get; set; }
        public Decimal? CHE_DO_HAO_MON_ID_OLD { get; set; }
        public Decimal? HM_TY_LE { get; set; }


        public WT_TSClnDBModel TS_CLN { get; set; }//map bảng TS_TAI_SAN_CLN
        public WT_TSDatDBModel TS_DAT { get; set; }//map bảng TS_TAI_SAN_DAT
        public WT_TSNhaDBModel TS_NHA { get; set; }//map bảng TS_TAI_SAN_NHA
        public WT_TSVktDBModel TS_VKT { get; set; }//map bảng TS_TAI_SAN_VKT
        public WT_TSOtoDBModel TS_OTO { get; set; }//map bảng TS_TAI_SAN_OTO
        public WT_TSOtoDBModel TS_PTK { get; set; }//map bảng TS_TAI_SAN_OTO
        public WT_TSMayMocDBModel TS_MAY_MOC { get; set; }//map bảng TS_TAI_SAN_MAY_MOC
        public WT_TSMayMocDBModel TS_DAC_THU { get; set; }//map bảng TS_TAI_SAN_MAY_MOC
        public WT_TSMayMocDBModel TS_HUU_HINH_KHAC { get; set; }//map bảng TS_TAI_SAN_MAY_MOC
        public WT_TSVoHinhDBModel TS_VO_HINH { get; set; }//map bảng TS_TAI_SAN_VO_HINH    

        public List<DB_BienDongModel> LST_BIEN_DONG { get; set; }//danh sách biến động - map bảng BD_BIEN_DONG + BD_BIEN_DONG_CHI_TIET
        public List<DB_TaiSanNguonVonModel> LST_NGUON_VON { get; set; }//danh sách nguồn vốn - map bảng TS_TAI_SAN_NGUON_VON
        public List<DB_TaiSanHienTrangSuDungModel> LST_HIEN_TRANG { get; set; }//danh sách Hiện trạng sử dụng - map bảng TS_TAI_SAN_HIEN_TRANG_SU_DUNG
        public List<DB_KT_HaoMonModel> LST_HAO_MON { get; set; }
    }

    #region WT_TS
    public class WT_TSClnDBModel
    {
        public Decimal? NAM_SINH { get; set; }
    }

    public class WT_TSDatDBModel
    {
        public String DIA_CHI { get; set; }
        public String DIA_BAN_MA { get; set; }
        public Decimal? DIEN_TICH { get; set; }
        public String HS_CNQSD_SO { get; set; }
        public String HS_CNQSD_NGAY { get; set; }
        public String HS_QUYET_DINH_GIAO_SO { get; set; }
        public String HS_QUYET_DINH_GIAO_NGAY { get; set; }
        public String HS_CHUYEN_NHUONG_SD_SO { get; set; }
        public String HS_CHUYEN_NHUONG_SD_NGAY { get; set; }
        public String HS_QUYET_DINH_CHO_THUE_SO { get; set; }
        public String HS_QUYET_DINH_CHO_THUE_NGAY { get; set; }

        //public String HS_KHAC { get; set; }
        //public Decimal? DIEN_TICH_XAY_NHA { get; set; }
    }
    public class WT_TSNhaDBModel
    {
        public String TAI_SAN_DAT_MA { get; set; }
        public String DIA_CHI { get; set; }
        public Decimal? NHA_SO_TANG { get; set; }
        public Decimal? NAM_XAY_DUNG { get; set; }
        public Decimal? DIEN_TICH_XAY_DUNG { get; set; }
        public Decimal? DIEN_TICH_SAN_XAY_DUNG { get; set; }
        public String NGAY_SU_DUNG { get; set; }
        //Ho so, giay to
        public String HS_QUYET_DINH_BAN_GIAO { get; set; }
        public String HS_QUYET_DINH_BAN_GIAO_NGAY { get; set; }
        public String HS_BIEN_BAN_NGHIEM_THU { get; set; }
        public String HS_BIEN_BAN_NGHIEM_THU_NGAY { get; set; }
        public String HS_PHAP_LY_KHAC { get; set; }
        public String HS_PHAP_LY_KHAC_NGAY { get; set; }
    }
    public class WT_TSVktDBModel
    {
        public Decimal? DIEN_TICH { get; set; }
        public Decimal? THE_TICH { get; set; }
        public Decimal? CHIEU_DAI { get; set; }
    }
    public class WT_TSOtoDBModel
    {
        public String BIEN_KIEM_SOAT { get; set; }
        public Decimal? SO_CHO_NGOI { get; set; }
        public Decimal? DUNG_TICH { get; set; }
        public String CHUC_DANH_MA { get; set; }
        public Decimal? TAI_TRONG { get; set; }
        public String SO_KHUNG { get; set; }
        public String NHAN_XE_MA { get; set; }
        public Decimal? CONG_XUAT { get; set; }
        public String SO_MAY { get; set; }
        public bool? TRANG_THAI_KH { get; set; }

    }
    public class WT_TSMayMocDBModel
    {
        public String PHU_KIEN_JSON { get; set; }
        public String THONG_SO_KY_THUAT { get; set; }
        public String THONG_SO_KY_HIEU { get; set; }
    }
    public class WT_TSVoHinhDBModel
    {
        public String THONG_SO_KY_THUAT { get; set; }
    }
    #endregion
    [XmlRoot(DataType = "DB_BienDongModel")]
    public class DB_BienDongModel
    {
        public DB_BienDongModel()
        {
            this.IS_BIENDONG_CUOI = false;
        }
        public string ID_DB { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public String DON_VI_BO_PHAN_MA { get; set; }
        public String CHUNG_TU_SO { get; set; }
        public String CHUNG_TU_NGAY { get; set; }
        public String NGAY_BIEN_DONG { get; set; }
        public String NGAY_SU_DUNG { get; set; }
        public Decimal? LOAI_BIEN_DONG_ID { get; set; }
        public Decimal? HINH_THUC_XU_LY_ID { get; set; }
        public String LY_DO_BIEN_DONG_MA { get; set; }
        public String LY_DO_BIEN_DONG_TEN { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public String NGAY_TAO { get; set; }
        public Guid GUID { get; set; }
        public String GHI_CHU { get; set; }
        public String QUYET_DINH_NGAY { get; set; }
        public String NGAY_DUYET { get; set; }
        public String QUYET_DINH_SO { get; set; }
        public bool? IS_BIENDONG_CUOI { get; set; }
        public Decimal? TRANG_THAI { get; set; }//1: CHO DUYET; 2: DA DUYET DANG KY; 3: TU CHOI
        public string DON_VI_MA { get; set; }
        public string LOAI_TAI_SAN_MA { get; set; }
        public List<DB_TaiSanNguonVonModel> NGUON_VON_BD { get; set; }
        public List<DB_TaiSanHienTrangSuDungModel> HIEN_TRANG_BD { get; set; }

        #region BienDongChiTiet
        public String DATA_JSON { get; set; }
        public Decimal? GIA_TRI_CON_LAI { get; set; }
        public String HINH_THUC_MUA_SAM_MA { get; set; }
        public String MUC_DICH_SU_DUNG_MA { get; set; }
        public String NHAN_HIEU { get; set; }
        public String SO_HIEU { get; set; }
        public String DIA_BAN_MA { get; set; }
        public Decimal? DAT_TONG_DIEN_TICH { get; set; }
        public Decimal? HM_SO_NAM_CON_LAI { get; set; }
        public Decimal? HM_TY_LE_HAO_MON { get; set; }
        public Decimal? HM_LUY_KE { get; set; }
        public Decimal? HM_GIA_TRI_CON_LAI { get; set; }
        public String KH_NGAY_BAT_DAU { get; set; }
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
        public String DON_VI_NHAN_DIEU_CHUYEN_MA { get; set; }
        public String HINH_THUC_XU_LY_MA { get; set; }
        public Boolean? IS_BAN_THANH_LY { get; set; }
        public Boolean? DIEU_CHUYEN_KEM_THEO { get; set; }
        public String HS_CNQSD_SO { get; set; }
        public String HS_CNQSD_NGAY { get; set; }
        public String HS_QUYET_DINH_GIAO_SO { get; set; }
        public String HS_QUYET_DINH_GIAO_NGAY { get; set; }
        public String HS_CHUYEN_NHUONG_SD_SO { get; set; }
        public String HS_CHUYEN_NHUONG_SD_NGAY { get; set; }
        public String HS_QUYET_DINH_CHO_THUE_SO { get; set; }
        public String HS_QUYET_DINH_CHO_THUE_NGAY { get; set; }
        public String HS_KHAC { get; set; }
        public String HS_QUYET_DINH_BAN_GIAO { get; set; }
        public String HS_QUYET_DINH_BAN_GIAO_NGAY { get; set; }
        public String HS_BIEN_BAN_NGHIEM_THU { get; set; }
        public String HS_BIEN_BAN_NGHIEM_THU_NGAY { get; set; }
        public String HS_PHAP_LY_KHAC { get; set; }
        public String HS_PHAP_LY_KHAC_NGAY { get; set; }
        public String DIA_CHI { get; set; }
        #endregion
    }
    [XmlRoot(DataType = "DB_TaiSanHienTrangSuDungModel")]
    public class DB_TaiSanHienTrangSuDungModel
    {
        public Decimal? HIEN_TRANG_ID { get; set; }
        public Guid BIEN_DONG_GUID { get; set; }
        public Decimal? NHOM_HIEN_TRANG_ID { get; set; }
        public Decimal? KIEU_DU_LIEU_ID { get; set; }
        public string TEN_HIEN_TRANG { get; set; }
        public string GIA_TRI_TEXT { get; set; }
        public Decimal? GIA_TRI_NUMBER { get; set; }
        public bool? GIA_TRI_CHECKBOX { get; set; }
    }
    [XmlRoot(DataType = "DB_TaiSanNguonVonModel")]
    public class DB_TaiSanNguonVonModel
    {
        public String TEN_NGUON_VON { get; set; }
        public Decimal? NGUON_VON_ID { get; set; }
        public Decimal? GIA_TRI { get; set; }
        public Guid BIEN_DONG_GUID { get; set; }
    }
    public class DB_KT_HaoMonModel
    {
        public String MA_TAI_SAN { get; set; }
        public decimal? NAM_HAO_MON { get; set; }
        public decimal? GIA_TRI_HAO_MON { get; set; }
        public decimal? TONG_HAO_MON_LUY_KE { get; set; }
        public decimal? TONG_GIA_TRI_CON_LAI { get; set; }
        public decimal? TY_LE_HAO_MON { get; set; }
        public decimal? TONG_NGUYEN_GIA { get; set; }
    }
}
