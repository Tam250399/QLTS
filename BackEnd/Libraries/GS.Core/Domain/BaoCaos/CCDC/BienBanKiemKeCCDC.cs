using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.CCDC
{
    public class BienBanKiemKeCCDC: BaseViewEntity
    {
        public string TEN_CCDC { get; set; }
        public string MA_CCDC { get; set; }
        public decimal? NAM_SU_DUNG { get; set; }
        // kiểm kê
        public decimal? SO_LUONG_KK { get; set; }
        public decimal? NGUYEN_GIA_KK { get; set; }
        public decimal? GIA_TRI_CON_LAI_KK { get; set; }
        // số sách
        public decimal? SO_LUONG_SS { get; set; }
        public decimal? NGUYEN_GIA_SS { get; set; }
        public decimal? GIA_TRI_CON_LAI_SS { get; set; }
        //chênh lệch
        public decimal? SO_LUONG_CL { get; set; }
        public decimal? NGUYEN_GIA_CL { get; set; }
        public decimal? GIA_TRI_CON_LAI_CL { get; set; }
    }
}
