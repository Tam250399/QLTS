using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCCK
{
    public class TS_BCCK_09B_CK_TSC:BaseViewEntity
    {
        public Decimal? TAI_SAN_ID { get; set; }
        public Decimal? TAI_SAN_DAT_ID { get; set; }
        public String DIA_CHI { get; set; }
        public String GHI_CHU { get; set; }
        //Property ts đất
        //public Decimal? DAT_TAI_SAN_ID { get; set; }
        public Decimal? DAT_NGUYEN_GIA { get; set; }
        public Decimal? DIEN_TICH_DAT { get; set; }
        //hiện trạng đất
        public Decimal? DAT_TRU_SO_LAM_VIEC { get; set; }
        public Decimal? DAT_KHONG_KINH_DOANH { get; set; }
        public Decimal? DAT_KINH_DOANH { get; set; }
        public Decimal? DAT_CHO_THUE { get; set; }
        public Decimal? DAT_LD_LK { get; set; }
        public Decimal? DAT_SU_DUNG_KHAC { get; set; }
		//tổng đất
        public Decimal? TONG_DAT_NGUYEN_GIA { get; set; }
        public Decimal? TONG_DIEN_TICH_DAT { get; set; }
        //hiện trạng đất
        public Decimal? TONG_DAT_TRU_SO_LAM_VIEC { get; set; }
        public Decimal? TONG_DAT_KHONG_KINH_DOANH { get; set; }
        public Decimal? TONG_DAT_KINH_DOANH { get; set; }
        public Decimal? TONG_DAT_CHO_THUE { get; set; }
        public Decimal? TONG_DAT_LD_LK { get; set; }
        public Decimal? TONG_DAT_SU_DUNG_KHAC { get; set; }

        //Property ts nhà
        //public Decimal? NHA_TAI_SAN_ID { get; set; }
        public Decimal? NHA_TAI_SAN_DAT_ID { get; set; }
        public Decimal? NHA_NAM_SU_DUNG { get; set; }
        public Decimal? NHA_TONG_DIEN_TICH_SAN { get; set; }
        public Decimal? NHA_NGUYEN_GIA { get; set; }
        public Decimal? NHA_GIA_TRI_CON_LAI { get; set; }
        public string NHA_DIA_CHI { get; set; }
        //hiện trạng nhà
        public Decimal? NHA_TRU_SO_LAM_VIEC { get; set; }
        public Decimal? NHA_KHONG_KINH_DOANH { get; set; }
        public Decimal? NHA_KINH_DOANH { get; set; }
        public Decimal? NHA_CHO_THUE { get; set; }
        public Decimal? NHA_LD_LK { get; set; }
        public Decimal? NHA_SU_DUNG_HON_HOP { get; set; }
        public Decimal? NHA_SU_DUNG_KHAC { get; set; }
    }
}
