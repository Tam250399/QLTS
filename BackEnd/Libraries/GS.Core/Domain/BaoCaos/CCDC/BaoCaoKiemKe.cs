using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.CCDC
{
    public class BaoCaoKiemKe : BaseViewEntity
    {
        public String TENDONVI { get; set; }
        public String DONVIBOPHAN { get; set; }
        public String MACONGCU { get; set; }
        public String TENCONGCU { get; set; }
        public String NHOMCONGCU { get; set; }
        public Decimal? SLKK_SOLUONG { get; set; }
        public Decimal? SLKK_THANHTIEN { get; set; }
        public Decimal? SLSS_SOLUONG { get; set; }
        public Decimal? SLSS_THANHTIEN { get; set; }
        public Decimal? CL_THUA_SOLUONG { get; set; }
        public Decimal? CL_THUA_THANHTIEN { get; set; }
        public Decimal? CL_THIEU_SOLUONG { get; set; }
        public Decimal? CL_THIEU_THANHTIEN { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
    }
}
