using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.CCDC
{
    public class LietKeCCDC : BaseViewEntity
    {
        public String TENCONGCU { get; set; }
        public String MACONGCU { get; set; }
        public String NHOMCONGCU { get; set; }
        public String DONVIBOPHAN { get; set; }
        public Decimal? DONGIA { get; set; }
        public Decimal? SOLUONG { get; set; }
        public Decimal? THANHTIEN { get; set; }
        public Decimal? DONVIBOPHANID { get; set; }
    }
}
