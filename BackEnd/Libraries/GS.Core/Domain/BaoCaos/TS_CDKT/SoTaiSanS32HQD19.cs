using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_CDKT
{
    //Sử dụng chung cho cả sổ S32H và S26H
    public class SoTaiSanS32HQD19 : BaseViewEntity
    {
        public Decimal? LOAI_HINH_TAI_SAN { get; set; }
        public String LOAI_HINH_TAI_SAN_CCDC { get; set; }
        public String TEN_LOAI_HINH_TAI_SAN { get; set; }
        public String MA_NHOM_TAI_SAN { get; set; }
        public String TEN_NHOM_TAI_SAN { get; set; }
        public DateTime? NGAY_GHI_SO { get; set; }
        public String SO_CHUNG_TU_TANG { get; set; }
        public DateTime? NGAY_CHUNG_TU_TANG { get; set; }
        public String MA_TAI_SAN { get; set; }
        public String TEN_TAI_SAN { get; set; }
        public String DON_VI_TINH { get; set; }
        public Decimal? DON_GIA_DAU_KY { get; set; }
        public Int32? SO_LUONG_DAU_KY { get; set; }
        public Decimal? THANH_TIEN_DAU_KY { get; set; }
        public Decimal? DON_GIA_TANG { get; set; }
        public Int32? SO_LUONG_TANG { get; set; }
        public Decimal? THANH_TIEN_TANG { get; set; }
        public String SO_CHUNG_TU_GIAM { get; set; }
        public DateTime? NGAY_CHUNG_TU_GIAM { get; set; }
        public String TEN_LY_DO_GIAM { get; set; }
        public Decimal? DON_GIA_GIAM { get; set; }
        public Int32? SO_LUONG_GIAM { get; set; }
        public Decimal? THANH_TIEN_GIAM { get; set; }
    }
}
