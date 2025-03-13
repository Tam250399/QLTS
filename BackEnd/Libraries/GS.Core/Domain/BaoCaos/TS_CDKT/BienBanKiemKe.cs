using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_CDKT
{
    public class BienBanKiemKe:BaseViewEntity
    {
        public string TEN_TAI_SAN { get; set; }
        public string MA_TAI_SAN { get; set; }
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

        public string TEN_HIEN_TRANG { get; set; }

        public string KIEN_NGHI_GIAI_QUYET_CHENH_LECH { get; set; }
    }
}
