using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCDA
{
    public class TS_BCDA_01BC_TSDA:BaseViewEntity
    {
        public string TEN_TAI_SAN { get; set; }
        public decimal? SO_LUONG { get; set; }
        public decimal? NAM_SU_DUNG { get; set; }
        public decimal? DIEN_TICH { get; set; }
        public decimal? TRU_SO_LAM_VIEC { get; set; }
        public decimal? KHAC { get; set; }
        public decimal? NGUYEN_GIA { get; set; }
        public decimal? CAP_1 { get; set; }
        public string TEN_1 { get; set; }
    }
}
