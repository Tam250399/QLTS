using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCQH
{
    public class TS_BCQH_MAU02_CCTSNN : BaseViewEntity
    {
        public Decimal? SO_LUONG_TRUNG_UONG { get; set; }
        public Decimal? DIEN_TICH_TRUNG_UONG { get; set; }
        public Decimal? NGUYEN_GIA_TRUNG_UONG { get; set; }
        public String TEN_LOAI_HINH_DON_VI { get; set; }
        public Decimal? LOAI_HINH_DON_VI { get; set; }
        public String TEN_LOAI_HINH_TAI_SAN { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? SO_LUONG_DIA_PHUONG { get; set; }
        public Decimal? DIEN_TICH_DIA_PHUONG { get; set; }
        public Decimal? NGUYEN_GIA_DIA_PHUONG { get; set; }
        public Decimal? TONG_SO_LUONG { get; set; }
        public Decimal? TONG_DIEN_TICH { get; set; }
        public Decimal? TONG_NGUYEN_GIA { get; set; }
        public Decimal? TONG_SO_LUONG_THEO_LTS { get; set; }
        public Decimal? TONG_DIEN_TICH_THEO_LTS { get; set; }
        public Decimal? TONG_NGUYEN_GIA_THEO_LTS { get; set; }
        // more
        public Decimal? LOAI_HINH_TAI_SAN_DB_ID { get; set; }
    }
}
