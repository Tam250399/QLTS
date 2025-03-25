using GS.Web.Framework.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace GS.NewAPI.Models
{
    public class LyDoBienDongModel : BaseGSApiModel
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_LY_DO_ID { get; set; }
        public Decimal? LOAI_LY_DO_BIEN_DONG_ID { get; set; }
    }
}
