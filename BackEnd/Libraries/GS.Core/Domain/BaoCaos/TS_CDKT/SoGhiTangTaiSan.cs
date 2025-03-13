using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_CDKT
{
    public class SoGhiTangTaiSan : BaseViewEntity
    {
        public String MA_NHOM_TAI_SAN { get; set; }
        public String TEN_NHOM_TAI_SAN { get; set; }
        public String SO_CHUNG_TU { get; set; }
        public DateTime? NGAY_CHUNG_TU { get; set; }
        public String MA_TAI_SAN { get; set; }
        public String TEN_TAI_SAN { get; set; }
        public String TEN_NUOC_SAN_XUAT { get; set; }
        public Int32? NAM_SAN_XUAT { get; set; }
        public Int32? NAM_SU_DUNG { get; set; }
        public String SO_HIEU_TAI_SAN { get; set; }
        public Decimal? NGUYEN_GIA_TANG { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }

    }
}
