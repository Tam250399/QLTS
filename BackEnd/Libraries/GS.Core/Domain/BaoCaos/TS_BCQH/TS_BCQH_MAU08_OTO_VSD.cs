using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCQH
{
    public class TS_BCQH_MAU08_OTO_VSD:BaseViewEntity
    {
        public Decimal CAP_1 { get; set; }
        public Decimal? CAP_2 { get; set; }
        public string TEN_1 { get; set; }
        public string TEN_2 { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public Decimal? SO_LUONG_QUA_HAN { get; set; }
        public Decimal? TONG_SO_LUONG { get; set; }
        public Decimal? TONG_SO_LUONG_QUA_HAN { get; set; }
        public int LOAI_TAI_SAN_ID { get; set; }
        // more
        public Decimal? LOAI_TAI_SAN_DB_ID { get; set; }
    }
}
