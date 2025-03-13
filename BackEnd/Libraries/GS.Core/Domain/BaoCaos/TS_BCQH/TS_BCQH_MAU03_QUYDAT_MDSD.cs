using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCQH
{
    public class TS_BCQH_MAU03_QUYDAT_MDSD:BaseViewEntity
    {
        public Decimal? SO_LUONG { get; set; }
        public Decimal? DIEN_TICH { get; set; }
        public String TEN_LOAI_TAI_SAN { get; set; }
        public Decimal? TONG_DIEN_TICH { get; set; }
        public int LOAI_TAI_SAN_ID { get; set; }
        // more
        public Decimal? LOAI_TAI_SAN_DB_ID { get; set; }
    }
}
