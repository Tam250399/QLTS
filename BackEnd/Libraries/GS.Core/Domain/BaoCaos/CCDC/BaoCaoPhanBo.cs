using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.CCDC
{
    public class BaoCaoPhanBo : BaseViewEntity
    {
        public String DONVIBOPHAN { get; set; }
        public String TENCONGCU { get; set; }
        public String DONVITINH { get; set; }
        public String CHUNGTUSO { get; set; }
        public DateTime? NGAYCHUNGTU { get; set; }
        public Decimal? SOKYPHANBO { get; set; }
        public Decimal? GIATRI { get; set; }
        public Decimal? GIATRICONPHANBO { get; set; }
        public Decimal? PHANBOTRONGKY { get; set; }
        public Decimal? SOKYCONLAI { get; set; }
        public Decimal? GIATRICONLAI { get; set; }
    }
}
