using System.ComponentModel.DataAnnotations;
using System;
using GS.Web.Framework.Models;

namespace GS.NewAPI.Models
{
    public class TaiSanModel : BaseGSEntityModel
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
        public Decimal? DU_AN_ID { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public String QUYET_DINH_SO { get; set; }
        [UIHint("DateNullable")]
        public DateTime? QUYET_DINH_NGAY { get; set; }
        public Decimal? QUYET_DINH_NGUOI_ID { get; set; }
        public Decimal? NUOC_SAN_XUAT_ID { get; set; }
        public Decimal? LY_DO_BIEN_DONG_ID { get; set; }
        public Decimal? DOI_TAC_ID { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_DUYET { get; set; }
        [UIHint("InputYear")]
        public Decimal? NAM_SAN_XUAT { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_NHAP { get; set; }
        public DateTime? NGAY_CAP_NHAT { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_SU_DUNG { get; set; }
        public String GHI_CHU { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Decimal? NGUOI_TAO_ID { get; set; }
        public Guid GUID { get; set; }
        public String CHUNG_TU_SO { get; set; }
        [UIHint("DateNullable")]
        public DateTime? CHUNG_TU_NGAY { get; set; }
        public decimal? NGUYEN_GIA_BAN_DAU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? GIA_MUA_TIEP_NHAN { get; set; }
        public bool? IS_XAC_NHAN { get; set; }
        public DateTime? NGAY_XAC_NHAN { get; set; }
        public bool? IS_MIEN_THUE { get; set; }
        [UIHint("InputAddon")]
        public decimal? GIA_HOA_DON { get; set; }
        [UIHint("InputAddon")]
        public decimal? MIEN_THUE_SO_TIEN { get; set; }
        public String MA_QLDKTS40 { get; set; }
        public bool? IS_DUYET { get; set; }
        public String MA_DB { get; set; }//mã đồng bộ
        public decimal? PHAN_LOAI_TAI_SAN { get; set; }
    }
}
