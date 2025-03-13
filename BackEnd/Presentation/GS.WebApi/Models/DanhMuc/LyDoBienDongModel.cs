using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.WebApi.Validator.DanhMuc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.DanhMuc
{
    [Validator(typeof (LyDoBienDongValidator))]
    public class LyDoBienDongModel : BaseGSApiModel
    {
        public LyDoBienDongModel()
        {
            this.LST_LOAI_HINH_TAI_SAN_ID = new List<decimal>();
        }
        [Required(ErrorMessage = "MA null")]
        public String MA { get; set; }
        [Required(ErrorMessage = "TEN null")]
        public String TEN { get; set; }
        public Decimal? DB_LoaiLyDoBienDongId { get; set; }
        public Decimal? LOAI_LY_DO_BIEN_DONG_ID { get; set; }
        [Required(ErrorMessage = "DB_ID null")]
        public int? DB_ID { get; set; }
        [JsonIgnore]
        public string LOAI_HINH_TAI_SAN_AP_DUNG_ID { get; set; }
        public decimal? TRUONG_SAP_XEP { get; set; }
        public List<decimal> LST_LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_LY_DO_ID { get; set; }
    }
}
