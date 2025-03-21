using System.ComponentModel.DataAnnotations;
using System;
using GS.Web.Framework.Models;
using GS.Core.Domain.TaiSans;
using GS.NewAPI.Validators.TaiSanValidator;
using FluentValidation.Attributes;

namespace GS.NewAPI.Models
{
    //public class TaiSanModel : BaseGSEntityModel
    //{
    //    public String MA { get; set; }
    //    public String TEN { get; set; }
    //    public Decimal? LOAI_TAI_SAN_ID { get; set; }
    //    public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
    //    public Decimal? DU_AN_ID { get; set; }
    //    public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
    //    public Decimal? TRANG_THAI_ID { get; set; }
    //    public String QUYET_DINH_SO { get; set; }
    //    [UIHint("DateNullable")]
    //    public DateTime? QUYET_DINH_NGAY { get; set; }
    //    public Decimal? QUYET_DINH_NGUOI_ID { get; set; }
    //    public Decimal? NUOC_SAN_XUAT_ID { get; set; }
    //    public Decimal? LY_DO_BIEN_DONG_ID { get; set; }
    //    public Decimal? DOI_TAC_ID { get; set; }
    //    [UIHint("DateNullable")]
    //    public DateTime? NGAY_DUYET { get; set; }
    //    [UIHint("InputYear")]
    //    public Decimal? NAM_SAN_XUAT { get; set; }
    //    [UIHint("DateNullable")]
    //    public DateTime? NGAY_NHAP { get; set; }
    //    public DateTime? NGAY_CAP_NHAT { get; set; }
    //    [UIHint("DateNullable")]
    //    public DateTime? NGAY_SU_DUNG { get; set; }
    //    public String GHI_CHU { get; set; }
    //    public Decimal? DON_VI_BO_PHAN_ID { get; set; }
    //    public Decimal? DON_VI_ID { get; set; }
    //    public DateTime? NGAY_TAO { get; set; }
      // public Decimal? NGUOI_TAO_ID { get; set; }
    //    public Guid GUID { get; set; }
    //    public String CHUNG_TU_SO { get; set; }
    //    [UIHint("DateNullable")]
    //    public DateTime? CHUNG_TU_NGAY { get; set; }
    //    public decimal? NGUYEN_GIA_BAN_DAU { get; set; }
    //    [UIHint("InputAddon")]
    //    public Decimal? GIA_MUA_TIEP_NHAN { get; set; }
    //    public bool? IS_XAC_NHAN { get; set; }
    //    public DateTime? NGAY_XAC_NHAN { get; set; }
    //    public bool? IS_MIEN_THUE { get; set; }
    //    [UIHint("InputAddon")]
    //    public decimal? GIA_HOA_DON { get; set; }
    //    [UIHint("InputAddon")]
    //    public decimal? MIEN_THUE_SO_TIEN { get; set; }
    //    public String MA_QLDKTS40 { get; set; }
    //    public bool? IS_DUYET { get; set; }
    //    public String MA_DB { get; set; }//mã đồng bộ
    //    public decimal? PHAN_LOAI_TAI_SAN { get; set; }
    //}

    public class TaiSanModel : BaseGSApiModel
    {
        public String MA { get; set; }
        public new decimal? ID { get; set; } 
        public String TEN { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? DIEN_TICH { get; set; } = 0;
        public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? DU_AN_ID { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; } = (decimal?)enumTRANG_THAI_TAI_SAN.CHO_DUYET;
        public DateTime? NGAY_TAO { get; set; }
        public DateTime? NGAY_TANG { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        public DateTime? NGAY_SU_DUNG { get; set; }
        public Decimal? LY_DO_TANG_ID { get; set; }
        public Decimal? MUC_DICH_ID { get; set; }
        public string DIA_CHI { get; set; } = null;
        public Decimal? QUOC_GIA_ID { get; set; }
        public Decimal? TINH_THANH_PHO_ID { get; set; }
        public Decimal? QUAN_HUYEN_ID { get; set; }
        public Decimal? XA_PHUONG_ID { get; set; }
        public decimal? NGUYEN_GIA { get; set; }
        public GiaTriSuDungDatModel GIA_TRI_SU_DUNG_DAT { get; set; }

        public HienTrangSuDungModel HIEN_TRANG_SU_DUNG { get; set; }
        public HoSoGiayToModel HO_SO_GIAY_TO { get; set; }
        public TaiSanDatModel taisandatModel { get; set; }
        #region chu dung den
        //public String QUYET_DINH_SO { get; set; }
        //[UIHint("DateNullable")]
        //public DateTime? QUYET_DINH_NGAY { get; set; }
        public Decimal? QUYET_DINH_NGUOI_ID { get; set; }
        //public Decimal? NUOC_SAN_XUAT_ID { get; set; }
        //public Decimal? LY_DO_BIEN_DONG_ID { get; set; }
        //public Decimal? DOI_TAC_ID { get; set; }
        //[UIHint("DateNullable")]
        //public DateTime? NGAY_DUYET { get; set; }
        //[UIHint("InputYear")]
        //public Decimal? NAM_SAN_XUAT { get; set; }
        //[UIHint("DateNullable")]
        //public DateTime? NGAY_NHAP { get; set; }
        //public DateTime? NGAY_CAP_NHAT { get; set; }

        //public String GHI_CHU { get; set; }

        public Decimal? NGUOI_TAO_ID { get; set; }
        //public Guid GUID { get; set; }
        //public String CHUNG_TU_SO { get; set; }
        //[UIHint("DateNullable")]
        //public DateTime? CHUNG_TU_NGAY { get; set; }
        //public decimal? NGUYEN_GIA_BAN_DAU { get; set; }
        //[UIHint("InputAddon")]
        //public Decimal? GIA_MUA_TIEP_NHAN { get; set; }
        //public bool? IS_XAC_NHAN { get; set; }
        //public DateTime? NGAY_XAC_NHAN { get; set; }
        //public bool? IS_MIEN_THUE { get; set; }
        //[UIHint("InputAddon")]
        //public decimal? GIA_HOA_DON { get; set; }
        //[UIHint("InputAddon")]
        //public decimal? MIEN_THUE_SO_TIEN { get; set; }
        //public String MA_QLDKTS40 { get; set; }
        //public bool? IS_DUYET { get; set; }
        //public String MA_DB { get; set; }//mã đồng bộ
        //public decimal? PHAN_LOAI_TAI_SAN { get; set; }
        #endregion
    }



    public class GiaTriSuDungDatModel
    {
        public decimal? GIA_TRI_QUYEN_SD_DAT { get; set; }
        public decimal? NGUON_KHAC { get; set; }
        public decimal? NGUON_NGAN_SACH { get; set; }

    }

    public class HienTrangSuDungModel
    {
        public decimal? BI_LAN_CHIEM { get; set; }
        public decimal? BO_TRONG { get; set; }
        public decimal? DE_O { get; set; }
        public decimal? HD_SN_CHO_THUE { get; set; }
        public decimal? HD_SN_KHONG_KINH_DOANH { get; set; }
        public decimal? HD_SN_KINH_DOANH { get; set; }
        public decimal? HD_SN_LIEN_DOANH_LK { get; set; }
        public decimal? SU_DUNG_HON_HOP { get; set; }
        public decimal? SU_DUNG_KHAC { get; set; }
        public decimal? TRU_SO_LAM_VIEC { get; set; }
    }

    public class HoSoGiayToModel
    {
        public string CHUNG_NHAN_QUYEN_SD_DAT { get; set; }
        public string GIAY_TO_KHAC { get; set; }
        public string HOP_DONG_CHO_THUE_DAT { get; set; }
        public string QD_CHO_THUE_DAT { get; set; }
        public string QD_GIAO_DAT { get; set; }
        public NgayCaoHoSoGiayToModel NGAY_CAP { get; set; }
    }

    public class NgayCaoHoSoGiayToModel
    {
        public DateTime? CHUNG_NHAN_QUYEN_SD_DAT { get; set; }
        public DateTime? HOP_DONG_CHO_THUE_DAT { get; set; }
        public DateTime? QD_CHO_THUE_DAT { get; set; }
        public DateTime? QD_GIAO_DAT { get; set; }
    }
}
