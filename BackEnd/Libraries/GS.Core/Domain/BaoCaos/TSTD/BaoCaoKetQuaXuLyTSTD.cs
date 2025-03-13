using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TSTD
{
    public class BaoCaoKetQuaXuLyTSTD : BaseViewEntity
    {
        public string XL_QUYET_DINH_SO { get; set; }
        public string LIST_XU_LY_ID { get; set; }
        public DateTime? XL_QUYET_DINH_NGAY { get; set; }
        public string XL_NGUOI_QUYET_DINH { get; set; }
        public Decimal? CHI_PHI { get; set; }
        public Decimal? SO_TIEN_PHAI_THU { get; set; }
        public Decimal? SO_TIEN_THU { get; set; }
        public Decimal? SO_TIEN_TKTG { get; set; }
        public Decimal? SO_TIEN_CON_PHAI_THU { get; set; }
        public Decimal? SO_TIEN_NOP_NSNN { get; set; }
        

    }
}



