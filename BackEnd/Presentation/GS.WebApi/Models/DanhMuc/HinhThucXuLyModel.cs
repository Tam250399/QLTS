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
    [Validator(typeof(HinhThucXuLyValidator))]
    public class HinhThucXuLyModel : BaseGSApiModel
    {
        [Required(ErrorMessage = "MA null")]
        public String MA { get; set; }
        [Required(ErrorMessage = "TEN null")]
        public String TEN { get; set; }
        public Decimal? PHUONG_AN_XU_LY_ID { get; set; }
        public Decimal? SAP_XEP { get; set; }

    }
}
