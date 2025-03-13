//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 6/10/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.WebApi.Validator.DanhMuc;

namespace GS.WebApi.Models.DMDC
{
    public class DMDC_DiaBanModel : BaseGSApiModel
    {
        public DMDC_DiaBanModel(){
        
        }
        [StringLength(10, ErrorMessage = "MA_V tối đa 10 ký tự")]
      
        public String MA_V {get;set;}
        [StringLength(10, ErrorMessage = "MA_T tối đa 10 ký tự")]
        [Required(ErrorMessage ="MA_T null")]
        public String MA_T {get;set;}
        [StringLength(10, ErrorMessage = "MA_H tối đa 10 ký tự")]
        public String MA_H {get;set;}
        [StringLength(10, ErrorMessage = "MA_X tối đa 10 ký tự")]
        public String MA_X {get;set;}
        [StringLength(50, ErrorMessage = "MA_CU tối đa 50 ký tự")]
        public String MA_CU {get;set;}
        [StringLength(500, ErrorMessage = "TEN tối đa 500 ký tự")]
        [Required(ErrorMessage = "TEN null")]
        public String TEN {get;set;}
        [StringLength(10, ErrorMessage = "MA_DB tối đa 10 ký tự")]
        [Required(ErrorMessage = "MA_DB null")]
        public String MA_DB {get;set;}
		public Decimal? ID_CHA {get;set;}
        [StringLength(20, ErrorMessage = "MA_CHA tối đa 20 ký tự")]
        public String MA_CHA {get;set;}
        [Required(ErrorMessage ="LOAI null")]
		public Decimal LOAI {get;set;}
        [StringLength(10, ErrorMessage = "MA_THAMCHIEU tối đa 10 ký tự")]
        [Required(ErrorMessage = "MA_THAMCHIEU null")]
        public String MA_THAMCHIEU {get;set;}
        [Required(ErrorMessage = "HIEU_LUC null")]
        public Boolean HIEU_LUC {get;set;}
		public string NGAY_SD {get;set;}
		public string NGAY_HL {get;set;}
		public string NGAY_KT {get;set;}
        [StringLength(100, ErrorMessage = "VAN_BAN_BH tối đa 100 ký tự")]
        public String VAN_BAN_BH {get;set;}
		public string NGAY_VB {get;set;}
		public string NGAY_TAO {get;set;}
        [StringLength(20, ErrorMessage = "NGUOI_TAO tối đa 20 ký tự")]
        public String NGUOI_TAO {get;set;}
        public string Error { get; set; }
        public Decimal? QUOC_GIA_ID { get; set; }
    }


    public class SoapDMDC_DiaBanModel
    {
        public decimal ID { get; set; }

        public String MA_V { get; set; }
        public String MA_T { get; set; }
        public String MA_H { get; set; }
        public String MA_X { get; set; }
        public String MA_CU { get; set; }
        public String TEN { get; set; }
        public String MA_DB { get; set; }
        public Decimal? ID_CHA { get; set; }
        public String MA_CHA { get; set; }
        public Decimal LOAI { get; set; }
        public String MA_THAMCHIEU { get; set; }
        public Boolean HIEU_LUC { get; set; }
        public string NGAY_SD { get; set; }
        public string NGAY_HL { get; set; }
        public string NGAY_KT { get; set; }
        public String VAN_BAN_BH { get; set; }
        public string NGAY_VB { get; set; }
        public string NGAY_TAO { get; set; }
        public String NGUOI_TAO { get; set; }
        public Decimal? QUOC_GIA_ID { get; set; }
        public string Error { get; set; }
    }
}

