using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace GS.NewAPI.Models
{
    public class NguonVonModel : BaseGSEntityModel
    {
        public String TEN { get; set; }
        public int? AP_DUNG_ID { get; set; }
        public String GHI_CHU { get; set; }
        public decimal? TRANG_THAI_ID { get; set; }
        public decimal? THU_TU { get; set; }
        [UIHint("InputAddon")]
        public decimal? GiaTri { get; set; }
        public decimal? LoaiHinhTaiSanId { get; set; }
    }
}
