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
    [Validator(typeof(LoaiTaiSanDonViValidator))]
    public class LoaiTaiSanDonViModel:BaseGSApiModel
    {
        [Required(ErrorMessage ="MA null")]
        public String MA { get; set; }
        [Required(ErrorMessage = "TEN null")]
        public String TEN { get; set; }
        public Decimal? HM_THOI_HAN_SU_DUNG { get; set; }
        [Required(ErrorMessage = "HM_TY_LE null")]
        public Decimal? HM_TY_LE { get; set; }
        public Decimal? KH_THOI_HAN_SU_DUNG { get; set; }
        public Decimal? KH_TY_LE { get; set; }
        public String MO_TA { get; set; }
        [Required(ErrorMessage = "CHE_DO_HAO_MON_ID null")]
        public Decimal? CHE_DO_HAO_MON_ID { get; set; }
        [Required(ErrorMessage = "PARENT_ID null")]
        public Decimal? PARENT_ID { get; set; }
        public String DON_VI_TINH { get; set; }
        [Required(ErrorMessage = "DON_VI_ID null")]
        public Decimal? DON_VI_ID { get; set; }
        [Required(ErrorMessage ="DB_ID null")]
        public String DB_ID { get; set; }
    }
}
