using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TSTD
{
    public class BaoCaoThongTinTSTD : BaseViewEntity
    {
        public string LOAI_TAI_SAN { get; set; }
        public string SO_QUYET_DINH_TT { get; set; }
        public string NGAY_QUYET_DINH_TT { get; set; }
        public string DON_VI_TT { get; set; }
        public string SO_QUYET_DINH_XL { get; set; }
        public string NGAY_QUYET_DINH_XL { get; set; }
        public string DON_VI_XL { get; set; }
        public int? SO_LUONG { get; set; }
        public int? NGUYEN_GIA { get; set; }
        public int? GIA_TRI_CON_LAI { get; set; }
        public string PHUONG_AN_XU_LY { get; set; }
        public string GHI_CHU { get; set; }
    }
}
