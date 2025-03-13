using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCQH
{
    public class TS_BCQH_MAU10_TS_TREN500_MDSD: BaseViewEntity
    {
        public Decimal CAP_1 { get; set; }
        public string TEN_1 { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? GIA_TRI_CON_LAI { get; set; }
        public Decimal? TONG_NGUYEN_GIA { get; set; }
        public Decimal? TONG_GIA_TRI_CON_LAI { get; set; }
        public Decimal? TONG_SO_LUONG { get; set; }
        public Decimal? LOAI_TAI_SAN_DB_ID { get; set; }
    }
}
