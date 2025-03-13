//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.TaiSans;
using System;


namespace GS.Core.Domain.NghiepVu
{
    public enum enumTRANG_THAI_YEU_CAU
    {
        TAT_CA=0,
        NHAP = 1,
        CHO_DUYET = 2,
        DA_DUYET = 3,
        TU_CHOI = 4,
        NHAP_LIEU = 5,
        XOA = 6
    }
    public partial class YeuCau : BaseEntity
    {
        public YeuCau()
        {

        }
        public YeuCau(YeuCau obj)
        {
            this.ID = obj.ID;
            this.GUID = obj.GUID;
            this.TAI_SAN_ID = obj.TAI_SAN_ID;
            this.TAI_SAN_MA = obj.TAI_SAN_MA;
            this.TAI_SAN_TEN = obj.TAI_SAN_TEN;
            this.LOAI_TAI_SAN_ID = obj.LOAI_TAI_SAN_ID;
            this.LOAI_HINH_TAI_SAN_ID = obj.LOAI_HINH_TAI_SAN_ID;
            this.NGUYEN_GIA = obj.NGUYEN_GIA;
            this.DON_VI_BO_PHAN_ID = obj.DON_VI_BO_PHAN_ID;
            this.CHUNG_TU_SO = obj.CHUNG_TU_SO;
            this.CHUNG_TU_NGAY = obj.CHUNG_TU_NGAY;
            this.NGAY_BIEN_DONG = obj.NGAY_BIEN_DONG;
            this.NGAY_SU_DUNG = obj.NGAY_SU_DUNG;
            this.LOAI_BIEN_DONG_ID = obj.LOAI_BIEN_DONG_ID;
            this.LY_DO_BIEN_DONG_ID = obj.LY_DO_BIEN_DONG_ID;
            this.TRANG_THAI_ID = obj.TRANG_THAI_ID;
            this.DON_VI_ID = obj.DON_VI_ID;
            this.NGUOI_TAO_ID = obj.NGUOI_TAO_ID;
            this.NGAY_TAO = obj.NGAY_TAO;
            this.GHI_CHU = obj.GHI_CHU;
            this.QUYET_DINH_NGAY = obj.QUYET_DINH_NGAY;
            this.QUYET_DINH_SO = obj.QUYET_DINH_SO;
            this.NGUON_VON_JSON = obj.NGUON_VON_JSON;
            this.IS_BIENDONG_CUOI = obj.IS_BIENDONG_CUOI;
            this.LY_DO_KHONG_DUYET = obj.LY_DO_KHONG_DUYET;
            this.LOAI_TAI_SAN_DON_VI_ID = obj.LOAI_TAI_SAN_DON_VI_ID;
            this.HOA_HONG_NOP_NSNN = obj.HOA_HONG_NOP_NSNN;
            this.HOA_HONG_DE_LAI_DON_VI = obj.HOA_HONG_DE_LAI_DON_VI;
        }
        public YeuCau(BienDong obj)
        {
            this.ID = obj.ID;
            this.GUID = obj.GUID;
            this.TAI_SAN_ID = obj.TAI_SAN_ID;
            this.TAI_SAN_MA = obj.TAI_SAN_MA;
            this.TAI_SAN_TEN = obj.TAI_SAN_TEN;
            this.LOAI_TAI_SAN_ID = obj.LOAI_TAI_SAN_ID;
            this.LOAI_HINH_TAI_SAN_ID = obj.LOAI_HINH_TAI_SAN_ID;
            this.NGUYEN_GIA = obj.NGUYEN_GIA;
            this.DON_VI_BO_PHAN_ID = obj.DON_VI_BO_PHAN_ID;
            this.CHUNG_TU_SO = obj.CHUNG_TU_SO;
            this.CHUNG_TU_NGAY = obj.CHUNG_TU_NGAY;
            this.NGAY_BIEN_DONG = obj.NGAY_BIEN_DONG;
            this.NGAY_SU_DUNG = obj.NGAY_SU_DUNG;
            this.LOAI_BIEN_DONG_ID = obj.LOAI_BIEN_DONG_ID;
            this.LY_DO_BIEN_DONG_ID = obj.LY_DO_BIEN_DONG_ID;
            this.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.DA_DUYET;
            this.DON_VI_ID = obj.DON_VI_ID;
            this.NGUOI_TAO_ID = obj.NGUOI_TAO_ID;
            this.NGAY_TAO = obj.NGAY_TAO;
            this.GHI_CHU = obj.GHI_CHU;
            this.QUYET_DINH_NGAY = obj.QUYET_DINH_NGAY;
            this.QUYET_DINH_SO = obj.QUYET_DINH_SO;
            this.IS_BIENDONG_CUOI = obj.IS_BIENDONG_CUOI;
            this.LOAI_TAI_SAN_DON_VI_ID = obj.LOAI_TAI_SAN_DON_VI_ID;
        }
        public Decimal TAI_SAN_ID { get; set; }
        public String TAI_SAN_MA { get; set; }
        public String TAI_SAN_TEN { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        public String CHUNG_TU_SO { get; set; }
        public DateTime? CHUNG_TU_NGAY { get; set; }
        public DateTime? NGAY_BIEN_DONG { get; set; }
        public DateTime? NGAY_SU_DUNG { get; set; }
        public Decimal? LOAI_BIEN_DONG_ID { get; set; }
        public Decimal? LY_DO_BIEN_DONG_ID { get; set; }
        public Decimal TRANG_THAI_ID { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? NGUOI_TAO_ID { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public Guid GUID { get; set; }
        public String GHI_CHU { get; set; }
        public DateTime? QUYET_DINH_NGAY { get; set; }
        public String QUYET_DINH_SO { get; set; }
        public String NGUON_VON_JSON { get; set; }
        public bool? IS_BIENDONG_CUOI { get; set; }
        public string LY_DO_KHONG_DUYET { get; set; }
        public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
        public virtual LoaiTaiSan loaitaisan { get; set; }
        public virtual TaiSan taisan { get; set; }
        public virtual NguoiDung nguoidung { get; set; }
        public virtual DonVi donvi { get; set; }
        public virtual LyDoBienDong lydobiendong { get; set; }
        public virtual DonViBoPhan donvibophan { get; set; }
        public Guid? DB_GUID { get; set; }
        public Decimal? HOA_HONG_NOP_NSNN { get; set; }
        public Decimal? HOA_HONG_DE_LAI_DON_VI { get; set; }
        public enumTRANG_THAI_YEU_CAU TrangThaiYeuCau
        {
            get => (enumTRANG_THAI_YEU_CAU)TRANG_THAI_ID;
            set => TRANG_THAI_ID = (int)value;
        }
    }
}



