using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCQH
{
    public class TS_BCQH_MAU09_DAT_SD: BaseViewEntity
    {
        public Decimal CAP_1 { get; set; }
        public string TEN_1 { get; set; }
        public Decimal CAP_2 { get; set; }
        public string TEN_2 { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? DIEN_TICH { get; set; }
        public Decimal? TONG_SO_LUONG { get; set; }
        public Decimal? TONG_DIEN_TICH { get; set; }
        public Decimal? TONG_NGUYEN_GIA { get; set; }
    }
}
