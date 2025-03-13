using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.DongBoTaiSan
{
    public class ThongTinTaiSanDBModel : BaseGSEntityModel
    {
        public ThongTinTaiSanDBModel()
        {

        }
        public String GUID { get; set; }
        public decimal? NGUON_TAI_SAN_ID { get; set; }
        public String TEN { get; set; }//tên tài san
        public String MA { get; set; }//mã tài sản
        //public String MA_TAI_SAN { get; set; }
        public String NGAY_NHAP { get; set; }//Ngày kê khai
        public String NGAY_SU_DUNG { get; set; }//Ngày sử dụng
        public String NGAY_DANG_KY { get; set; }
        public String LY_DO_BIEN_DONG_MA { get; set; }//Lý do tăng
        public int? DB_LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
        public String LOAI_TAI_SAN_DON_VI_MA { get; set; }
        public String LOAI_TAI_SAN_MA { get; set; }//Loại tài sản
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }//đất, nhà,...
        public String MA_DON_VI { get; set; }
        public decimal? DON_VI_ID { get; set; }
        public decimal? DB_DON_VI_ID { get; set; }
        public string TEN_DON_VI { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public String GHI_CHU { get; set; }
        public String DON_VI_BO_PHAN_MA { get; set; }//Bộ phận sử dụng
        public decimal? DON_VI_BO_PHAN_ID { get; set; }
        public String DON_VI_BO_PHAN_TEN { get; set; }
        public String NUOC_SAN_XUAT_MA { get; set; }//Nước sản xuất
        public decimal? NUOC_SAN_XUAT_ID { get; set; }
        public decimal? QUOC_GIA_ID { get; set; }
        public Decimal? DB_QUOC_GIA_ID { get; set; }
        public String NAM_SAN_XUAT { get; set; }//Năm sản xuất
        public String QUYET_DINH_SO { get; set; }//Quyết định số
        public String QUYET_DINH_NGAY { get; set; }//Quyết định ngày
        public String CHUNG_TU_SO { get; set; }//Chứng từ/hồ sơ số
        public String CHUNG_TU_NGAY { get; set; }//Chứng từ/hồ sơ ngày
        public String LOAI_TAI_SAN_VO_HINH_MA { get; set; }
        public decimal? NGUYEN_GIA_BAN_DAU { get; set; }
        public decimal? MIEN_THUE_SO_TIEN { get; set; }
        public decimal? GIA_MUA_TIEP_NHAN { get; set; }
        public decimal? GIA_HOA_DON { get; set; }
        public TaiSanClnDBModel TS_CLN { get; set; }//map bảng TS_TAI_SAN_CLN
        public TaiSanDatDBModel TS_DAT { get; set; }//map bảng TS_TAI_SAN_DAT
        public TaiSanNhaDBModel TS_NHA { get; set; }//map bảng TS_TAI_SAN_NHA
        public TaiSanVktDBModel TS_VKT { get; set; }//map bảng TS_TAI_SAN_VKT
        public TaiSanOtoDBModel TS_OTO { get; set; }//map bảng TS_TAI_SAN_OTO
        public TaiSanOtoDBModel TS_PTK { get; set; }//map bảng TS_TAI_SAN_OTO
        public TaiSanMayMocDBModel TS_MAY_MOC { get; set; }//map bảng TS_TAI_SAN_MAY_MOC
        public TaiSanMayMocDBModel TS_DAC_THU { get; set; }//map bảng TS_TAI_SAN_MAY_MOC
        public TaiSanMayMocDBModel TS_HUU_HINH_KHAC { get; set; }//map bảng TS_TAI_SAN_MAY_MOC
        public TaiSanVoHinhDBModel TS_VO_HINH { get; set; }//map bảng TS_TAI_SAN_VO_HINH  
        public TaiSanKhac TS_KHAC { get; set; }
        public List<BDDBModel> LST_BIEN_DONG { get; set; }//danh sách biến động - map bảng BD_BIEN_DONG + BD_BIEN_DONG_CHI_TIET
        public HoSoGiayTo HoSoGiayTo { get; set; }
        public List<HaoMonDBModel> LST_HAO_MON { get; set; }
        public List<KhauHaoDBModel> LST_KHAU_HAO { get; set; }
        public string Error { get; set; }
        public string NGUOI_DUYET { get; set; }
    }
    #region TaiSan
    public class TSClnDBModel : BaseGSApiModel
    {
        public Decimal? NAM_SINH { get; set; }
    }
    public class TSDatDBModel : BaseGSApiModel
    {
        public String DIA_CHI { get; set; }
        public String DIA_BAN_MA { get; set; }
        public Decimal? DIEN_TICH { get; set; }
        public Decimal? DIA_BAN_ID { get; set; }
        public long? DB_DIA_BAN_ID { get; set; }

        //public Decimal? DIEN_TICH_XAY_NHA { get; set; }
    }
    public class TSNhaDBModel : BaseGSApiModel
    {
        public String TAI_SAN_DAT_MA { get; set; }
        public String DIA_CHI { get; set; }
        public Decimal? NHA_SO_TANG { get; set; }
        public Decimal? NAM_XAY_DUNG { get; set; }
        public Decimal? DIEN_TICH_XAY_DUNG { get; set; }
        public Decimal? DIEN_TICH_SAN_XAY_DUNG { get; set; }
        public String NGAY_SU_DUNG { get; set; }
        //Ho so, giay to
    }
    public class TSVktDBModel : BaseGSApiModel
    {
        public Decimal? DIEN_TICH { get; set; }
        public Decimal? THE_TICH { get; set; }
        public Decimal? CHIEU_DAI { get; set; }
    }
    public class TSOtoDBModel : BaseGSApiModel
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
        public bool TRANG_THAI_KH { get; set; }
        public String CHUC_DANH_TEN { get; set; }
        public String NHAN_XE_TEN { get; set; }

    }
    public class TaiSanMayMocThietBiDBModel : BaseGSModel
    {
        public String PHU_KIEN_JSON { get; set; }
        public String THONG_SO_KY_THUAT { get; set; }
    }
    public class TaiSanVHDBModel : BaseGSModel
    {
        public String THONG_SO_KY_THUAT { get; set; }
    }
    public class TaiSanDBKhac : BaseGSModel
    {
        public string NAM_SINH { get; set; }
        public string THONG_SO_KY_THUAT { get; set; }
        public Decimal? DIEN_TICH { get; set; }
        public Decimal? THE_TICH { get; set; }
        public Decimal? CHIEU_DAI { get; set; }

    }
    #endregion
    public class KhauHaoDBoModel
    {
        public decimal? ID { get; set; }
        public decimal? TAI_SAN_ID { get; set; }
        public Decimal? GIA_TRI_KHAU_HAO { get; set; }
        public string MA_TAI_SAN { get; set; }
        public string MA_TAI_SAN_DB { get; set; }
        public string MA_TSC { get; set; }
        public string MA_DKTS { get; set; }
        public decimal? NAM_KHAU_HAO { get; set; }
        public decimal? THANG_KHAU_HAO { get; set; }
        public decimal? TONG_GIA_TRI_CON_LAI { get; set; }
        public decimal? TONG_KHAU_HAO_LUY_KE { get; set; }
        public decimal? TONG_NGUYEN_GIA { get; set; }
        public decimal? TY_LE_KHAU_HAO { get; set; }
        public decimal? DB_ID { get; set; }


    }
    public class HaoMonDBoModel
    {
        public decimal? ID { get; set; }
        public decimal? TAI_SAN_ID { get; set; }
        public string MA_TAI_SAN { get; set; }
        public string MA_DKTS { get; set; }
        public string MA_TSC { get; set; }
        public string MA_TAI_SAN_DB { get; set; }
        public decimal? NAM_HAO_MON { get; set; }
        public decimal? GIA_TRI_HAO_MON { get; set; }
        public decimal? TONG_HAO_MON_LUY_KE { get; set; }
        public decimal? TONG_GIA_TRI_CON_LAI { get; set; }
        public decimal? TY_LE_HAO_MON { get; set; }
        public decimal? TONG_NGUYEN_GIA { get; set; }
        public decimal? DB_ID { get; set; }
        public bool IsUpdate { get; set; }
    }
    
    public class BDDBModel : BaseGSModel
    {
        public BDDBModel()
        {

        }
        public string MA_TAI_SAN { get; set; }
        public string MA_TAI_SAN_DB { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public string TEN_TAI_SAN { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public String LOAI_TAI_SAN_MA { get; set; }
        public decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
        public String LOAI_TAI_SAN_DON_VI_MA { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        public String DON_VI_BO_PHAN_MA { get; set; }
        public String CHUNG_TU_SO { get; set; }
        public String CHUNG_TU_NGAY { get; set; }
        public String NGAY_BIEN_DONG { get; set; }
        public String NGAY_SU_DUNG { get; set; }
        public Decimal? LOAI_BIEN_DONG_ID { get; set; }
        public Decimal? LY_DO_BIEN_DONG_ID { get; set; }
        public string LY_DO_BIEN_DONG_MA { get; set; }
        public String NGAY_TAO { get; set; }
        public string ID_DB { get; set; }
        public string GUID { get; set; }
        public String GHI_CHU { get; set; }
        public String QUYET_DINH_NGAY { get; set; }
        public String NGAY_DUYET { get; set; }
        public String NGUOI_DUYET { get; set; }
        public String QUYET_DINH_SO { get; set; }/// quyết định về việc sửa chữa nâng cấp tài sản .....
        #region BienDongChiTiet
        public String HINH_THUC_MUA_SAM_MA { get; set; }
        public String HINH_THUC_MUA_SAM_TEN { get; set; }
        public String MUC_DICH_SU_DUNG_MA { get; set; }
        public Decimal? DB_MUC_DICH_SU_DUNG_ID { get; set; }
        public Decimal? MUC_DICH_SU_DUNG_ID { get; set; }
        public String NHAN_HIEU { get; set; }
        public String SO_HIEU { get; set; }
        public Decimal? DIA_BAN_ID { get; set; }
        public string DIA_BAN_MA { get; set; }
        public String DIA_CHI { get; set; }
        public Decimal? DAT_TONG_DIEN_TICH { get; set; }
        public Decimal? HM_SO_NAM_CON_LAI { get; set; }
        public Decimal? HM_TY_LE_HAO_MON { get; set; }
        public Decimal? HM_LUY_KE { get; set; }
        public Decimal? HM_GIA_TRI_CON_LAI { get; set; }
        public Decimal? GIA_TRI_CON_LAI { get; set; }
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
        public decimal? OTO_CHUC_DANH_ID { get; set; }
        public String OTO_NHAN_XE_MA { get; set; }
        public String OTO_NHAN_XE_TEN { get; set; }
        public Decimal? OTO_TAI_TRONG { get; set; }
        public Decimal? OTO_CONG_XUAT { get; set; }
        public Decimal? OTO_XI_LANH { get; set; }
        public String OTO_SO_KHUNG { get; set; }
        public String OTO_SO_MAY { get; set; }
        public String THONG_SO_KY_THUAT { get; set; }
        public Decimal? CLN_SO_NAM { get; set; }
        public String DON_VI_NHAN_DIEU_CHUYEN_MA { get; set; }
        public String OTO_CHUC_DANH_MA { get; set; }
        public String HINH_THUC_XU_LY_MA { get; set; }
        public bool? IS_BAN_THANH_LY { get; set; }
        public decimal? HINH_THUC_XU_LY_ID { get; set; }
        #endregion
        #region Hiện trạng sử dụng
        public HienTrangSuDungNumber HTSD_DAT { get; set; }
        public HienTrangSuDungNumber HTSD_NHA { get; set; }
        public HienTrangSuDungCheckbox HTSD_OTO { get; set; }
        public HienTrangSuDungCheckbox HTSD_VTK { get; set; }
        public HienTrangSuDungCheckbox HTSD_MAY_MOC { get; set; }
        public HienTrangSuDungCheckbox HTSD_CLN { get; set; }
        public HienTrangSuDungCheckbox HTSD_VO_HINH { get; set; }
        public List<TaiSanHienTrangSuDungDBModel> LST_HIEN_TRANG { get; set; }
        #endregion
        #region Nguồn vốn
        public decimal? NV_NGAN_SACH { get; set; }
        public decimal? NV_HDSN { get; set; }
        public decimal? NV_NGUON_KHAC { get; set; }
        public decimal? NV_VIEN_TRO { get; set; }
        //public decimal? NV_NGAN_SACH_DIA_PHUONG { get; set; }
        //public decimal? NV_TRAI_PHIEU { get; set; }
        //public decimal? NV_Y_TE { get; set; }
        //public decimal? NV_DAU_TU { get; set; }
        //public decimal? NV_THUONG_XUYEN { get; set; }
        //public decimal? NV_CHI_THUONG_XUYEN_MOTPHAN { get; set; }
        //public decimal? NV_CHI_THUONG_XUYEN_TOANBO { get; set; }
        //public decimal? NV_NGOAI_NGAN_SACH { get; set; }
        #endregion
        public HoSoGiayTo HO_SO_GIAY_TO { get; set; }
        public Decimal? GIA_HOA_DON { get; set; }
        public decimal NGUON_TAI_SAN_ID { get; set; }
        public decimal? THU_TU_BIEN_DONG_ID { get; set; }
    }
    public class TaiSanHTSDDBModel : BaseGSModel
    {
        public decimal? QLDKTS_BIEN_DONG_ID { get; set; }//id biến động dkts cũ
        public decimal? HIEN_TRANG_ID { get; set; }
        public Guid BIEN_DONG_GUID { get; set; }
        public decimal? NHOM_HIEN_TRANG_ID { get; set; }
        public decimal? KIEU_DU_LIEU_ID { get; set; }
        public string TEN_HIEN_TRANG { get; set; }
        public string GIA_TRI_TEXT { get; set; }
        public decimal? GIA_TRI_NUMBER { get; set; }
        public bool? GIA_TRI_CHECKBOX { get; set; }
    }
    public class TaiSanNVDBModel : BaseGSModel
    {
        public String TEN_NGUON_VON { get; set; }
        public Decimal? NGUON_VON_ID { get; set; }
        public Decimal? GIA_TRI { get; set; }
        public Guid BIEN_DONG_GUID { get; set; }
        public decimal? QLDKTS_BIEN_DONG_ID { get; set; }//id biến động dkts cũ
    }
    public class HTSDCheckbox
    {
        public bool? HTSD_HDSN_KINH_DOANH_KHONG { get; set; }
        public bool? HTSD_HDSN_KINH_DOANH { get; set; }
        public bool? HTSD_HDSN_LIEN_DOANH { get; set; }
        public bool? HTSD_HDSN_CHO_THUE { get; set; }
        public bool? HTSD_SU_DUNG_KHAC { get; set; }
        public bool? HTSD_QUAN_LY_NHA_NUOC { get; set; }
        public bool? HTSD_SU_DUNG_HON_HOP { get; set; }
    }
    public class HTSDNumber
    {
        public decimal? HTSD_HDSN_KINH_DOANH_KHONG { get; set; }
        public decimal? HTSD_HDSN_KINH_DOANH { get; set; }
        public decimal? HTSD_HDSN_LIEN_DOANH { get; set; }
        public decimal? HTSD_HDSN_CHO_THUE { get; set; }
        public decimal? HTSD_QUAN_LY_NHA_NUOC { get; set; }
        public decimal? HTSD_TRU_SO_LAM_VIEC { get; set; }
        public decimal? HTSD_SU_DUNG_HON_HOP { get; set; }
        public decimal? HTSD_DE_O { get; set; }
        public decimal? HTSD_BO_TRONG { get; set; }
        public decimal? HTSD_BI_LAN_CHIEM { get; set; }


    }
    public class HSGiayTo
    {
        public String HS_CNQSD_SO { get; set; }
        public String HS_CNQSD_NGAY { get; set; }
        public String HS_QUYET_DINH_GIAO_SO { get; set; }
        public String HS_QUYET_DINH_GIAO_NGAY { get; set; }
        public String HS_QUYET_DINH_CHO_THUE_SO { get; set; }
        public String HS_QUYET_DINH_CHO_THUE_NGAY { get; set; }
        public String HS_KHAC { get; set; }
        public String HS_QUYET_DINH_BAN_GIAO { get; set; }
        public String HS_QUYET_DINH_BAN_GIAO_NGAY { get; set; }
        public String HS_BIEN_BAN_NGHIEM_THU { get; set; }
        public String HS_BIEN_BAN_NGHIEM_THU_NGAY { get; set; }
        public String HS_CHUYEN_NHUONG_SD_SO { get; set; }
        public String HS_CHUYEN_NHUONG_SD_NGAY { get; set; }
        public String HS_PHAP_LY_KHAC { get; set; }
        public String HS_PHAP_LY_KHAC_NGAY { get; set; }
        public String HS_HOP_DONG_CHO_THUE_SO { get; set; }
        public String HS_HOP_DONG_CHO_THUE_NGAY { get; set; }
    }
}
