using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCQH
{
    public class TS_BCQH_MAU05_TS_CQ_TC_DV:BaseViewEntity
    {
        public string TEN_LOAI_HINH_DON_VI { get; set; }
        public Decimal? LOAI_HINH_DON_VI { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? GIA_TRI_CON_LAI { get; set; }
        public Decimal? TONG_NGUYEN_GIA { get; set; }
        public Decimal? TONG_GIA_TRI_CON_LAI { get; set; }
    }
}
