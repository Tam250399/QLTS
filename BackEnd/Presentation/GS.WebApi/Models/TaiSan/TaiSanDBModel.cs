using GS.Web.Framework.Models;
using GS.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GS.Core.Domain.TaiSans;
using GS.Core.Domain.BienDongs;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.WebApi.Validator.TaiSan;
using System.Runtime.CompilerServices;

namespace GS.WebApi.Models.TaiSan
{
    [Validator(typeof(TaiSanValidator))]
    public class TaiSanDBModel : BaseGSApiModel
    {
        public TaiSanDBModel()
        {
            HoSoGiayTo = new HoSoGiayTo();
        }
        public decimal NGUON_TAI_SAN_ID { get; set; }
        public String GUID { get; set; }
        //[Required(ErrorMessage = ("TEN null"))]
        public String TEN { get; set; }//tên tài san
        public String MA { get; set; }//mã tài sản
        [Required(ErrorMessage = ("DB_MA null"))]
        public string DB_MA { get; set; }
        public String NGAY_NHAP { get; set; }//Ngày kê khai
                                             // [Required(ErrorMessage = ("NGAY_DANG_KY null"))]
        public String NGAY_DANG_KY { get; set; }
        // [Required(ErrorMessage = ("NGAY_SU_DUNG null"))]
        public String NGAY_SU_DUNG { get; set; }//Ngày sử dụng
        public String LY_DO_BIEN_DONG_MA { get; set; }//Lý do tăng 
        public int? DB_LOAI_TAI_SAN_ID { get; set; }
        //[Required(ErrorMessage = ("LOAI_TAI_SAN_ID null"))]
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
        public String LOAI_TAI_SAN_MA { get; set; }//Loại tài sản
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }//đất, nhà,...
                                                          // [Required(ErrorMessage = ("MA_DON_VI null"))]
        public String MA_DON_VI { get; set; }
        public decimal? DON_VI_ID { get; set; }
        public decimal? DB_DON_VI_ID { get; set; }
        public decimal? DU_AN_ID { get; set; }
        public decimal? DB_DU_AN_ID { get; set; }
        public string TEN_DON_VI { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public String GHI_CHU { get; set; }
        public decimal? DON_VI_BO_PHAN_ID { get; set; }
        public decimal? DB_DON_VI_BO_PHAN_ID { get; set; }
        public Decimal? NUOC_SAN_XUAT_ID { get; set; }//Nước sản xuất
        //public decimal? QUOC_GIA_ID { get; set; }
        public Decimal? DB_QUOC_GIA_ID { get; set; }
        public String NAM_SAN_XUAT { get; set; }//Năm sản xuất
        public String QUYET_DINH_SO { get; set; }//Quyết định số
        public String QUYET_DINH_NGAY { get; set; }//Quyết định ngày
        public String CHUNG_TU_SO { get; set; }//Chứng từ/hồ sơ số
        public String CHUNG_TU_NGAY { get; set; }//Chứng từ/hồ sơ ngày
        public String LOAI_TAI_SAN_VO_HINH_MA { get; set; }
        public decimal? NGUYEN_GIA_BAN_DAU { get; set; }
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
        public List<BienDongDBModel> LST_BIEN_DONG { get; set; }//danh sách biến động - map bảng BD_BIEN_DONG + BD_BIEN_DONG_CHI_TIET
        public HoSoGiayTo HoSoGiayTo { get; set; }
        public string Error { get; set; }
        public string NGUOI_DUYET { get; set; }
        public decimal? GIA_MUA_TIEP_NHAN { get; set; }
        public decimal? GIA_HOA_DON { get; set; }
        public decimal? MIEN_THUE_SO_TIEN { get; set; }
        public decimal? HM_TY_LE { get; set; }
        public List<HaoMonInTaiSanDBModel> LST_HAO_MON { get; set; }
        public List<KhauHaoInTaiSanDBModel> LST_KHAU_HAO { get; set; }

    }

    #region TaiSan
    public class TaiSanClnDBModel : BaseGSApiModel
    {
        public Decimal? NAM_SINH { get; set; }
    }
    public class TaiSanDatDBModel : BaseGSApiModel
    {
        public String DIA_CHI { get; set; }
        public String DIA_BAN_MA { get; set; }
        public Decimal? DIEN_TICH { get; set; }
        public Decimal? DIA_BAN_ID { get; set; }
        public long? DB_DIA_BAN_ID { get; set; }

        //public Decimal? DIEN_TICH_XAY_NHA { get; set; }
    }
    public class TaiSanNhaDBModel : BaseGSApiModel
    {
        public String TAI_SAN_DAT_MA { get; set; }
        public String DIA_CHI { get; set; }
        public Decimal? NHA_SO_TANG { get; set; }
        public Decimal? NAM_XAY_DUNG { get; set; }
        public Decimal? DIEN_TICH_XAY_DUNG { get; set; }
        public Decimal? DIEN_TICH_SAN_XAY_DUNG { get; set; }
        public String NGAY_SU_DUNG { get; set; }
        public string DB_TAI_SAN_DAT_MA { get; set; }

        //Ho so, giay to
    }
    public class TaiSanVktDBModel : BaseGSApiModel
    {
        public Decimal? DIEN_TICH { get; set; }
        public Decimal? THE_TICH { get; set; }
        public Decimal? CHIEU_DAI { get; set; }
    }
    public class TaiSanOtoDBModel : BaseGSApiModel
    {
        public String BIEN_KIEM_SOAT { get; set; }
        public Decimal? SO_CHO_NGOI { get; set; }
        public Decimal? DUNG_TICH { get; set; }
        public String CHUC_DANH_MA { get; set; }
        public int? CHUC_DANH_ID { get; set; }
        public Decimal? TAI_TRONG { get; set; }
        public String SO_KHUNG { get; set; }
        public String NHAN_XE_MA { get; set; }
        public int? NHAN_XE_ID { get; set; }
        public Decimal? CONG_XUAT { get; set; }
        public String SO_MAY { get; set; }
        public bool TRANG_THAI_KH { get; set; }
        public String CHUC_DANH_TEN { get; set; }
        public String NHAN_XE_TEN { get; set; }
        public Decimal? SO_CAU { get; set; }
        public string GCN_DANG_KY { get; set; }
        public string CO_QUAN_CAP_DANG_KY { get; set; }
    }
    public class TaiSanMayMocDBModel : BaseGSApiModel
    {
        public String PHU_KIEN_JSON { get; set; }
        public String THONG_SO_KY_THUAT { get; set; }
    }
    public class TaiSanVoHinhDBModel : BaseGSApiModel
    {
        public String THONG_SO_KY_THUAT { get; set; }
    }
    public class TaiSanKhac : BaseGSApiModel
    {
        public string NAM_SINH { get; set; }
        public string THONG_SO_KY_THUAT { get; set; }
        public Decimal? DIEN_TICH { get; set; }
        public Decimal? THE_TICH { get; set; }
        public Decimal? CHIEU_DAI { get; set; }

    }
    #endregion
    [Validator(typeof(BienDongValidator))]
    public class BienDongDBModel : BaseGSApiModel
    {
        public BienDongDBModel()
        {

        }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
        public string TEN_TAI_SAN { get; set; }
        public string MA_TAI_SAN { get; set; }
        public string MA_TAI_SAN_DB { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public String CHUNG_TU_SO { get; set; }
        public String CHUNG_TU_NGAY { get; set; }
        [Required(ErrorMessage = ("NGAY_BIEN_DONG null"))]
        public String NGAY_BIEN_DONG { get; set; }
        public String NGAY_SU_DUNG { get; set; }
        public decimal? DON_VI_BO_PHAN_ID { get; set; }
        public decimal? DB_DON_VI_BO_PHAN_ID { get; set; }
        [Required(ErrorMessage = ("LOAI_BIEN_DONG_ID null"))]
        public Decimal? LOAI_BIEN_DONG_ID { get; set; }
        public String LY_DO_BIEN_DONG_MA { get; set; }
        public Decimal? DB_LY_DO_BIEN_DONG_ID { get; set; }
        [Required(ErrorMessage = ("LY_DO_BIEN_DONG_ID null"))]
        public Decimal? LY_DO_BIEN_DONG_ID { get; set; }
        public string TEN_LY_DO { get; set; }
        public String NGAY_TAO { get; set; }
        [Required(ErrorMessage = ("ID_DB null"))]
        public string ID_DB { get; set; }
        public string GUID { get; set; }
        public String GHI_CHU { get; set; }
        public String QUYET_DINH_NGAY { get; set; }
        [Required(ErrorMessage = ("NGAY_DUYET null"))]
        public String NGAY_DUYET { get; set; }
        public String NGUOI_DUYET { get; set; }
        public String QUYET_DINH_SO { get; set; }
        public decimal? GIA_MUA_TIEP_NHAN { get; set; }
        #region BienDongChiTiet
        public String HINH_THUC_MUA_SAM_MA { get; set; }
        public Decimal? HINH_THUC_MUA_SAM_DB_ID { get; set; }
        public String HINH_THUC_MUA_SAM_TEN { get; set; }

        public String MUC_DICH_SU_DUNG_MA { get; set; }
        public Decimal? DB_MUC_DICH_SU_DUNG_ID { get; set; }
        public Decimal? MUC_DICH_SU_DUNG_ID { get; set; }
        public String NHAN_HIEU { get; set; }
        public String SO_HIEU { get; set; }
        public String DIA_BAN_MA { get; set; }
        public Decimal? DIA_BAN_ID { get; set; }
        public Decimal? DB_QUOC_GIA_ID { get; set; }
        public String DIA_CHI { get; set; }
        public Decimal? DB_DIA_BAN_ID { get; set; }
        public Decimal? DAT_TONG_DIEN_TICH { get; set; }
        public Decimal? DAT_GIA_TRI_QUYEN_SD_DAT { get; set; }
        public Decimal? HM_SO_NAM_CON_LAI { get; set; }
        public Decimal? HM_TY_LE_HAO_MON { get; set; }
        public Decimal? HM_LUY_KE { get; set; }
        public Decimal? HM_GIA_TRI_CON_LAI { get; set; }
        [Required(ErrorMessage = "GIA_TRI_CON_LAI null")]
        public Decimal? GIA_TRI_CON_LAI { get; set; }
        public String KH_NGAY_BAT_DAU { get; set; }
        public Decimal? KH_THANG_CON_LAI { get; set; }
        public Decimal? KH_GIA_TINH_KHAU_HAO { get; set; }
        public Decimal? KH_GIA_TRI_TRICH_THANG { get; set; }
        public Decimal? KH_LUY_KE { get; set; }
        public Decimal? KH_CON_LAI { get; set; }
        public Decimal? KH_TY_LE { get; set; }
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
        public decimal? OTO_CHUC_DANH_DB_ID { get; set; }
        public string OTO_CHUC_DANH_TEN { get; set; }
        public String OTO_NHAN_XE_MA { get; set; }
        public String OTO_NHAN_XE_TEN { get; set; }
        public decimal? OTO_NHAN_XE_ID { get; set; }
        public decimal? OTO_NHAN_XE_DB_ID { get; set; }
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
        public decimal? PHI_THU { get; set; }
        public decimal? PHI_BU_DAP { get; set; }
        public decimal? PHI_NOP_NGAN_SACH { get; set; }
        #endregion
        #region Hiện trạng sử dụng
        public List<TaiSanHienTrangSuDungDBModel> LST_HIEN_TRANG { get; set; }
        #endregion
        #region Nguồn vốn
        public decimal? NV_NGAN_SACH { get; set; }
        public decimal? NV_HDSN { get; set; }
        public decimal? NV_NGUON_KHAC { get; set; }
        public decimal? NV_VIEN_TRO { get; set; }
        #endregion
        public HoSoGiayTo HO_SO_GIAY_TO { get; set; }
        public string ERROR { get; set; }
        public decimal NGUON_TAI_SAN_ID { get; set; }
    }
    public class TaiSanHienTrangSuDungDBModel : BaseGSApiModel
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
    public class TaiSanNguonVonDBModel : BaseGSApiModel
    {
        public String TEN_NGUON_VON { get; set; }
        public Decimal? NGUON_VON_ID { get; set; }
        public Decimal? GIA_TRI { get; set; }
        public Guid BIEN_DONG_GUID { get; set; }
        public decimal? QLDKTS_BIEN_DONG_ID { get; set; }//id biến động dkts cũ
    }
    public class HienTrangSuDungCheckbox
    {
        public bool? HTSD_HDSN_KINH_DOANH_KHONG { get; set; }
        public bool? HTSD_HDSN_KINH_DOANH { get; set; }
        public bool? HTSD_HDSN_LIEN_DOANH { get; set; }
        public bool? HTSD_HDSN_CHO_THUE { get; set; }
        public bool? HTSD_SU_DUNG_KHAC { get; set; }
        public bool? HTSD_QUAN_LY_NHA_NUOC { get; set; }
        public bool? HTSD_SU_DUNG_HON_HOP { get; set; }
    }
    public class HienTrangSuDungNumber
    {
        public decimal? HTSD_HDSN_KINH_DOANH_KHONG { get; set; }
        public decimal? HTSD_HDSN_KINH_DOANH { get; set; }
        public decimal? HTSD_HDSN_LIEN_DOANH { get; set; }
        public decimal? HTSD_HDSN_CHO_THUE { get; set; }
        public decimal? HTSD_TRU_SO_LAM_VIEC { get; set; }
        public decimal? HTSD_SU_DUNG_HON_HOP { get; set; }
        public decimal? HTSD_DE_O { get; set; }
        public decimal? HTSD_BO_TRONG { get; set; }
        public decimal? HTSD_BI_LAN_CHIEM { get; set; }

    }
    public class HoSoGiayTo
    {
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
        public String HS_HOP_DONG_CHO_THUE_SO { get; set; }
        public string HS_HOP_DONG_CHO_THUE_NGAY { get; set; }
        public Decimal? DAT_GIA_TRI_QUYEN_SD_DAT { get; set; }
    }
    public class ResultTaiSan
    {
        public ResultTaiSan()
        {
            this.ListTaiSan = new List<TaiSanDBModel>();
        }
        public List<TaiSanDBModel> ListTaiSan { get; set; }
        public int Total { get; set; }
        public int TotalPage { get; set; }
    }
    public class TS_HTSD
    {
        public decimal ID { get; set; }
        public decimal GIA_TRI_NUMBER { get; set; }
        public bool GIA_TRI_CHECKBOX { get; set; }
    }
    public class TrangThaiNamModel
    {
        public int Nam { get; set; }
        public int TrangThai { get; set; }
    }
    #region Khấu hao/Hao mòn tài sản
    [Validator(typeof(KhauHaoInTaiSanValidator))]
    public class KhauHaoInTaiSanDBModel : BaseGSApiModel
    {
        [Required(ErrorMessage = "GIA_TRI_KHAU_HAO null")]
        public Decimal? GIA_TRI_KHAU_HAO { get; set; }
        public string MA_TAI_SAN { get; set; }
        [Required(ErrorMessage = "NAM_KHAU_HAO null")]
        public decimal? NAM_KHAU_HAO { get; set; }
        [Required(ErrorMessage = "THANG_KHAU_HAO null")]
        public decimal? THANG_KHAU_HAO { get; set; }
        [Required(ErrorMessage = "TONG_GIA_TRI_CON_LAI null")]
        public decimal? TONG_GIA_TRI_CON_LAI { get; set; }
        [Required(ErrorMessage = "TONG_KHAU_HAO_LUY_KE null")]
        public decimal? TONG_KHAU_HAO_LUY_KE { get; set; }
        [Required(ErrorMessage = "TONG_NGUYEN_GIA null")]
        public decimal? TONG_NGUYEN_GIA { get; set; }
        [Required(ErrorMessage = "TY_LE_KHAU_HAO null")]
        public decimal? TY_LE_KHAU_HAO { get; set; }

    }
    [Validator(typeof(HaoMonInTaiSanValidator))]
    public class HaoMonInTaiSanDBModel : BaseGSApiModel
    {
        public decimal? TAI_SAN_ID { get; set; }
        public string MA_TAI_SAN { get; set; }
        [Required(ErrorMessage = "NAM_HAO_MON null")]
        public decimal? NAM_HAO_MON { get; set; }
        [Required(ErrorMessage = "GIA_TRI_HAO_MON null")]
        public decimal? GIA_TRI_HAO_MON { get; set; }
        [Required(ErrorMessage = "TONG_HAO_MON_LUY_KE null")]
        public decimal? TONG_HAO_MON_LUY_KE { get; set; }
        [Required(ErrorMessage = "TONG_GIA_TRI_CON_LAI null")]
        public decimal? TONG_GIA_TRI_CON_LAI { get; set; }
        [Required(ErrorMessage = "TY_LE_HAO_MON null")]
        public decimal? TY_LE_HAO_MON { get; set; }
        [Required(ErrorMessage = "TONG_NGUYEN_GIA null")]
        public decimal? TONG_NGUYEN_GIA { get; set; }
    }
    #endregion

    #region anhnt

    #endregion
}


