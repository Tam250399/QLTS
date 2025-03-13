using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.CCDC
{
    public class TongHopCCDC : BaseViewEntity
    {
        public String TENCONGCU { get; set; }
        public String MACONGCU { get; set; }
        public String NHOMCONGCU { get; set; }
        public String DONVIBOPHAN { get; set; }
        public Decimal? DONGIA { get; set; }
        public Decimal? SOLUONG { get; set; }
        public Decimal? THANHTIEN { get; set; }
        public Decimal? DONVIBOPHANID { get; set; }
        public string DV_1 { get; set; }
        public string DV_2 { get; set; }
        public string DV_3 { get; set; }
        public string DV_4 { get; set; }
        public string DV_5 { get; set; }
        public string DV_TEN_1 { get; set; }
        public string DV_TEN_2 { get; set; }
        public string DV_TEN_3 { get; set; }
        public string DV_TEN_4 { get; set; }
        public string DV_TEN_5 { get; set; }
    }
}
