using System;

namespace GS.Core.Domain.TaiSans
{
    public partial class TaiSanLichSu : BaseEntity
    {
        public decimal TAI_SAN_ID { get; set; }
        public decimal? NGUOI_TAO_ID { get; set; }
        public string HOAT_DONG { get; set; }
        public DateTime? NGAY_TAO { get; set; }
    }
}
