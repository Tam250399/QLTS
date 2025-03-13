using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.TaiSanDBApi
{
    public class TaiSanNhaApi 
    {
        public String TAI_SAN_DAT_MA { get; set; }
        public String DIA_CHI { get; set; }
        public Decimal? NHA_SO_TANG { get; set; }
        public Decimal? NAM_XAY_DUNG { get; set; }
        public Decimal? DIEN_TICH_XAY_DUNG { get; set; }
        public Decimal? DIEN_TICH_SAN_XAY_DUNG { get; set; }
        public DateTime? NGAY_SU_DUNG { get; set; }
        //Ho so, giay to
        public String HS_QUYET_DINH_BAN_GIAO { get; set; }
        public DateTime? HS_QUYET_DINH_BAN_GIAO_NGAY { get; set; }
        public String HS_BIEN_BAN_NGHIEM_THU { get; set; }
        public DateTime? HS_BIEN_BAN_NGHIEM_THU_NGAY { get; set; }
        public String HS_PHAP_LY_KHAC { get; set; }
        public DateTime? HS_PHAP_LY_KHAC_NGAY { get; set; }
        public decimal TAI_SAN_DAT_ID { get; set; }
    }
}
