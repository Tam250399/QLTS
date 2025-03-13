using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_CDKT
{
    //Sử dụng chung cho cả sổ S31H và S24H
    public class SoTaiSanS31HQD19 : BaseViewEntity
    {
        public Decimal? LOAI_HINH_TAI_SAN { get; set; }
        public String TEN_LOAI_HINH_TAI_SAN { get; set; }
        public String MA_NHOM_TAI_SAN { get; set; }
        public String TEN_NHOM_TAI_SAN { get; set; }
        public String SO_CHUNG_TU_TANG { get; set; }
        public DateTime? NGAY_CHUNG_TU_TANG { get; set; }
        public String MA_TAI_SAN { get; set; }
        public String TEN_TAI_SAN { get; set; }
        public String DON_VI_TINH { get; set; }
        public String TEN_NUOC_SAN_XUAT { get; set; }
        public Int32? NAM_SAN_XUAT { get; set; }
        public Int32? NAM_SU_DUNG { get; set; }
        public string THANG_NAM_SU_DUNG { get; set; }
        public String SO_HIEU_TAI_SAN { get; set; }
        public DateTime? NGAY_GHI_SO { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? TY_LE_HAO_MON { get; set; }
        public Decimal? HAO_MON_1_NAM { get; set; }
        public Decimal? HAO_MON_NAM_TRUOC_CHUYEN_SANG { get; set; }
        public Decimal? HAO_MON_NAM { get; set; }
        public Decimal? TY_LE_KHAU_HAO { get; set; }
        public Decimal? KHAU_HAO_1_NAM { get; set; }
        public Decimal? KHAU_HAO_NAM { get; set; }
        public Decimal? TONG_KHAU_HAO_HAO_MON_NAM { get; set; }
        public Decimal? LUY_KE_HAO_MON { get; set; }
        public String SO_CHUNG_TU_GIAM { get; set; }
        public DateTime? NGAY_CHUNG_TU_GIAM { get; set; }
        public String TEN_LY_DO_GIAM { get; set; }
        public Decimal? GIA_TRI_CON_LAI_GIAM { get; set; }
    }
}
