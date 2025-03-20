using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using GS.Web.Framework.Models;

namespace GS.NewAPI.Models
{
    public class TaiSanDatModel : BaseGSApiModel
    {
        public new decimal? ID { get; set; }
        public Decimal TAI_SAN_ID { get; set; }
        public String DIA_CHI { get; set; }
        public Decimal? DIA_BAN_ID { get; set; }
        public Decimal DIEN_TICH { get; set; }
        public Decimal? DIEN_TICH_XAY_NHA { get; set; }
        public int QuocGiaId { get; set; }
        public int TinhId { get; set; }
        public int HuyenId { get; set; }
        public int XaId { get; set; }
    }
}
