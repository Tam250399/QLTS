using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.CCDC
{
    public class TangGiamCongCu : BaseViewEntity
    {
        public String XUATORNHAP { get; set; }
        public String TENCONGCU { get; set; }
        public String MACONGCU { get; set; }
        public DateTime? NGAYXUATNHAP { get; set; }
        public String BOPHANSUDUNG { get; set; }
        public Decimal? SOLUONG { get; set; }
        public Decimal? DONGIA { get; set; }
        public Decimal? THANHTIEN { get; set; }
    }
}
