using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.WebApi.Validator.TaiSan;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.TaiSan
{
    [Validator(typeof(KhauHaoValidator))]
    public class KhauHaoDBModel : BaseGSApiModel
    {
        [Required(ErrorMessage = "GIA_TRI_KHAU_HAO null")]
        public Decimal? GIA_TRI_KHAU_HAO { get; set; }
        //[Required(ErrorMessage = "MA_TAI_SAN null")]
        public string MA_TAI_SAN { get; set; }
        //[Required(ErrorMessage = "MA_TAI_SAN null")]
        public string MA_TAI_SAN_DB { get; set; }
        [Required(ErrorMessage = "NAM_KHAU_HAO null")]
        public decimal? NAM_KHAU_HAO { get; set; }
        [Required(ErrorMessage = "THANG_KHAU_HAO null")]
        public decimal? THANG_KHAU_HAO { get; set; }
        [Required(ErrorMessage = "TONG_GIA_TRI_CON_LAI null")]
        public decimal? TONG_GIA_TRI_CON_LAI { get; set; }
        [Required(ErrorMessage = "TONG_KHAU_HAO_LUY_KE null")]
        public decimal? TONG_KHAU_HAO_LUY_KE { get; set; }
        [Required(ErrorMessage = "TONG_NGUYEN_GIA null")]
        public decimal? TONG_NGUYEN_GIA { get; set; }
        [Required(ErrorMessage = "TY_LE_KHAU_HAO null")]
        public decimal? TY_LE_KHAU_HAO { get; set; }
        public String MESSAGE { get; set; }
    }
    [Validator(typeof(HaoMonValidator))]
    public class HaoMonDBModel : BaseGSApiModel
    {
        public decimal? TAI_SAN_ID { get; set; }
        //[Required(ErrorMessage ="MA_TAI_SAN null")]
        //[StringLength(255, ErrorMessage = "MA_TAI_SAN tối đa 255 ký tự")]
        public string MA_TAI_SAN { get; set; }
        //[Required(ErrorMessage = "MA_TAI_SAN_DB null")]
        public string MA_TAI_SAN_DB { get; set; }
        [Required(ErrorMessage = "NAM_HAO_MON null")]
        public decimal? NAM_HAO_MON { get; set; }
        [Required(ErrorMessage = "GIA_TRI_HAO_MON null")]
        public decimal? GIA_TRI_HAO_MON { get; set; }
        [Required(ErrorMessage = "TONG_HAO_MON_LUY_KE null")]
        public decimal? TONG_HAO_MON_LUY_KE { get; set; }
        [Required(ErrorMessage = "TONG_GIA_TRI_CON_LAI null")]
        public decimal? TONG_GIA_TRI_CON_LAI { get; set; }
        [Required(ErrorMessage = "TY_LE_HAO_MON null")]
        public decimal? TY_LE_HAO_MON { get; set; }
        [Required(ErrorMessage = "TONG_NGUYEN_GIA null")]
        public decimal? TONG_NGUYEN_GIA { get; set; }
        public String MESSAGE { get; set; }
    }
}
