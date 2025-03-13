using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCQH
{
    public class TS_BCQH_MAU06_TS_CAPQL:BaseViewEntity
    {
        public string TEN_LOAI_HINH_TAI_SAN { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? CAP_DON_VI_ID { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public Decimal? LOAI_CAP_DON_VI_ID { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? DIEN_TICH { get; set; }
        public Decimal? TONG_NGUYEN_GIA { get; set; }
        public Decimal? TONG_DIEN_TICH { get; set; }
        public Decimal? TONG_SO_LUONG { get; set; }
        // more
        public Decimal? LOAI_HINH_TAI_SAN_DB_ID { get; set; }
    }
}
