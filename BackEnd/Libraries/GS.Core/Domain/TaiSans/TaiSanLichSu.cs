using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.TaiSans
{
    public partial class TaiSanLichSu : BaseEntity
    {
        Decimal TAI_SAN_ID { get; set; }
        decimal? NGUOI_TAO_ID { get; set; }
        string HOAT_DONG { get; set; }
        public DateTime? NGAY_TAO { get; set; }
    }
}
