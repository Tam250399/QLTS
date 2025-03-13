//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using Newtonsoft.Json;
using System;


namespace GS.Core.Domain.DanhMuc
{
    public enum enumDonVi
    {
        TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG = 99284,
        DPAC = 189988
    }
    public enum enumMaDPAC
    {
        TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG = 018999
       
    }
    public enum enumLoaiDonVi_TheoId
    {
        BAN_QUAN_LY_DU_AN = 15,
        DON_VI_SU_NGHIEP = 12
    }
    public static class enumLoaiDonVi
    {
        public const string LDV_CO_QUAN_NHA_NUOC = "0";
        public const string LDV_SU_NGHIEP = "1";
        public const string LDV_TO_CHUC = "2";
        public const string LDV_DOANH_NGHIEP = "3";
        public const string LDV_BAN_QUAN_LY_DU_AN = "4";
        public const string LDV_DON_VI_KHAC = "103";
    }
    public static class enumDonViTongHop
    {
        public const string TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG = "018999";
    }
    public partial class DonVi : BaseEntity
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public String MA_BO { get; set; }
        public String MA_DIA_BAN { get; set; }
        public String MA_DVQHNS { get; set; }
        public String DIA_CHI { get; set; }
        public String DIEN_THOAI { get; set; }
        public String FAX { get; set; }
        public String MA_TINH { get; set; }
        public Decimal? NHOM_DON_VI_ID { get; set; }
        public Decimal? CAP_DON_VI_ID { get; set; }
        public Decimal? LOAI_CAP_DON_VI_ID { get; set; }
        public Decimal? OLD_CAP_DON_VI_ID { get; set; }
        public String MA_HUYEN { get; set; }
        public String CQTC_MA { get; set; }
        public Decimal? CHE_DO_HACH_TOAN_ID { get; set; }
        public String SO_QUYET_DINH { get; set; }
        public DateTime? NGAY_QUYET_DINH { get; set; }
        public String SO_QUYET_DINH_GIAO_VON { get; set; }
        public DateTime? NGAY_QUYET_DINH_GIAO_VON { get; set; }
        public Decimal? PARENT_ID { get; set; }
        public Decimal? LOAI_DON_VI_ID { get; set; }
        public Decimal? TREE_LEVEL { get; set; }
        public String TREE_NODE { get; set; }
        public Decimal? DIA_BAN_ID { get; set; }
        public Boolean? TRANG_THAI_ID { get; set; }
        public Boolean? DON_VI_HIEN_THI { get; set; }
        public Boolean? TRANG_THAI_DONG_BO_ID { get; set; }
        public Boolean? LA_DON_VI_NHAP_LIEU { get; set; }
        public Boolean? TRANG_THAI_THAY_DOI_ID { get; set; }
        public Boolean? CO_TAI_SAN { get; set; }
        public Boolean? KHONG_CHUYEN_MA { get; set; }
        public Boolean? LA_BAN_QL_DU_AN { get; set; }
        public Boolean? LA_DON_VI_TU_CHU_TAI_CHINH { get; set; }
        public Boolean? DA_CO_QUYET_DINH_GIAO_VON { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Decimal? NGUOI_TAO_ID { get; set; }
        public DateTime? NGAY_CAP_NHAT { get; set; }
        public Decimal? NGUOI_CAP_NHAT_ID { get; set; }
        public int? DB_ID { get; set; }
        //add more
        public virtual DonVi DonViCha { get; set; }
        public virtual LoaiDonVi LoaiDonVi { get; set; }

        public Decimal? TINH_ID { get; set; }
        public Decimal? HUYEN_ID { get; set; }
        public Decimal? XA_ID { get; set; }

        public Boolean? IS_TSQUAN_LY_NHU_TSCD { get;set; }
        public Boolean? IS_XAC_NHAN_DU_LIEU { get;set; }
        public DateTime? NGAY_XAC_NHAN { get; set; }
        public String  LIST_FILE_XAC_NHAN { get; set; }
        public string MA_CHUONG { get; set; }
        public Boolean? IS_TSQUAN_LY_CHU_KY_SO { get; set; }
    }
    public partial class InfoCacheDonvi : BaseCacheEntity
    {
        public String MA_DON_VI { get; set; }
        public String TEN_DON_VI { get; set; }
        public String TEN_DON_VI_CHA { get; set; }
        public bool? IS_LA_DON_VI_NHAP_LIEU { get; set; }
        public bool? IS_LA_DON_VI_TU_CHU_TAI_CHINH { get; set; }
        public Boolean? LA_BAN_QL_DU_AN { get; set; }
        public decimal? TREE_LEVEL { get; set; }
    }
    public enum EnumLoaiCapDonVi
    {
        CapTrungUong = 1,
        CapDiaPhuong = 2,
    }
    public enum CapEnum
    {
        /// <summary>
        /// Bộ cơ quan trung ương
        /// </summary>
        BoCoQuanTrungUong = 0,
        /// <summary>
        /// Tỉnh
        /// </summary>
        Tinh = 1,
        /// <summary>
        /// Huyện
        /// </summary>
        Huyen = 2,
        /// <summary>
        /// Xã
        /// </summary>
        Xa = 3,
        /// <summary>
        /// Tổng cục
        /// </summary>
        TongCuc = 4,
        /// <summary>
        /// Cục
        /// </summary>
        Cuc = 5,
        /// <summary>
        /// Chi cục
        /// </summary>
        ChiCuc = 6
    }
    public enum EnumCapDonViTrungUong
    {
        BoCoQuanTrungUong = 0,
        TongCuc = 4,
        Cuc = 5,
        ChiCuc = 6
    }
    public enum EnumMaLoaiDonViToChuc
    {
        ChinhTri =36,
        ChinhTriXaHoi=37,
        ChinhTriXaHoiNgheNghiep=38,
        XaHoi=39,
        XaHoiNgheNghiep =40,
        ToChucKhac = 41    
    }

    public enum EnumCapDonViDiaPhuong
    {

        Tinh = 1,
        Huyen = 2,
        Xa = 3,
    }
    public enum enumQuyetDinhGiaoVon
    {
        Chua = 0,
        Co = 1
    }
}



