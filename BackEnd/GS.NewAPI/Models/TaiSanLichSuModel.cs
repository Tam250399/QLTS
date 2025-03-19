using GS.Web.Framework.Models;
using System;

namespace GS.NewAPI.Models
{
    public class TaiSanLichSuModel : BaseGSEntityModel
    {
        decimal TAI_SAN_ID  {get; set; }
        decimal? NGUOI_TAO_ID { get; set; }
        string HOAT_DONG { get; set; }
        public DateTime? NGAY_TAO { get; set; }

    }
}
