using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.WebApi.Validator.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.DanhMuc
{
    [Validator(typeof(DanhMucLoaiLyDoBienDongValidator))]
    public class LoaiLyDoBienDongModel: BaseGSApiModel
    {
        [Required(ErrorMessage ="MA null")]
        [StringLength(255,ErrorMessage ="MA tối đa 255 ký tự")]
        public string MA { get; set; }
        [Required(ErrorMessage = "TEN null")]
        public string TEN { get; set; }
        public Decimal? PARENT_ID { get; set; }
        [Required(ErrorMessage = "DB_ID null")]
        public Decimal? DB_ID { get; set; }
    }
}
