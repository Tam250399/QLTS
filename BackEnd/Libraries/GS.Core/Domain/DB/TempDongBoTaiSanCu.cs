using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.DB
{
   public class TempDongBoTaiSanCu: BaseEntity
    {
        public String DATA { get; set; }
        public String RESPONSE { get; set; }
        public Decimal? LOAI_BIEN_DONG_ID { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public String TAI_SAN_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public DateTime? NGAY_DONG_BO { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
    }
}
