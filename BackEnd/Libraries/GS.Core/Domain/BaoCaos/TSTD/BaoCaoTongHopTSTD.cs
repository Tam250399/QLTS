using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TSTD
{
    public class BaoCaoTongHopTSTD : BaseViewEntity
    {
        public string TEN_LOAI_TAI_SAN { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? SO_LUONG_DAU_KY { get; set; }
        public Decimal? DIEN_TICH_DAU_KY { get; set; }
        public Decimal? GIA_TRI_DAU_KY { get; set; }
        public Decimal? SO_LUONG_TANG { get; set; }
        public Decimal? DIEN_TICH_TANG { get; set; }
        public Decimal? GIA_TRI_TANG { get; set; }
        public Decimal? SO_LUONG_GIAM { get; set; }
        public Decimal? DIEN_TICH_GIAM { get; set; }
        public Decimal? GIA_TRI_GIAM { get; set; }
        public Decimal? SO_LUONG_CUOI_KY { get; set; }
        public Decimal? DIEN_TICH_CUOI_KY { get; set; }
        public Decimal? GIA_TRI_CUOI_KY { get; set; }
 
    }
}
