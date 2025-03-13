using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.TaiSanDBApi
{
    public class TaiSanDatApi 
    {
        public String DIA_CHI { get; set; }
         public String DIA_BAN_MA { get; set; }
        public decimal? DIA_BAN_ID { get; set; }
        public Decimal DIEN_TICH { get; set; }
        public String HS_CNQSD_SO { get; set; }
        public DateTime? HS_CNQSD_NGAY { get; set; }
        public String HS_QUYET_DINH_GIAO_SO { get; set; }
        public DateTime? HS_QUYET_DINH_GIAO_NGAY { get; set; }
        public String HS_CHUYEN_NHUONG_SD_SO { get; set; }
        public DateTime? HS_CHUYEN_NHUONG_SD_NGAY { get; set; }
        public String HS_QUYET_DINH_CHO_THUE_SO { get; set; }
        public DateTime? HS_QUYET_DINH_CHO_THUE_NGAY { get; set; }
    }
}
