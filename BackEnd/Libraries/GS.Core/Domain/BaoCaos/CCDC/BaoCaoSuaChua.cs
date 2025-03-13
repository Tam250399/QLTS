using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.CCDC
{
    public class BaoCaoSuaChua : BaseViewEntity
    {
        public String MACONGCU { get; set; }
        public String TENCONGCU { get; set; }
        public DateTime? NGAYSUACHUA { get; set; }
        public String BOPHANSUDUNG { get; set; }
        public Decimal? SOLUONG { get; set; }
        public Decimal? CHIPHI { get; set; }
    }
}
