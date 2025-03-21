using GS.Web.Framework.Models;
using System;

namespace GS.NewAPI.Models
{
    public class TaiSanLichSuModel : BaseGSApiModel
    {
        public new decimal? ID { get; set; }
        public decimal TAI_SAN_ID { get; set; }
        public decimal? NGUOI_TAO_ID { get; set; }
        public string HOAT_DONG { get; set; }
        public DateTime? NGAY_TAO { get; set; }

    }
}
