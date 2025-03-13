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
    
    public class DMDC_QuocGiaModel :BaseGSApiModel
	{
        public DMDC_QuocGiaModel(){
        
        }
        [StringLength(10, ErrorMessage ="MA tối đa 10 ký tự")]
        [Required(ErrorMessage = "MA null")]
        public String MA {get;set;}
        [StringLength(500, ErrorMessage = "TEN tối đa 500 ký tự")]
        [Required(ErrorMessage = "TEN null")]
        public String TEN {get;set;}
        [Required(ErrorMessage = "TEN_TA null")]
        [StringLength(500, ErrorMessage = "TEN_TA tối đa 500 ký tự")]
        public String TEN_TA {get;set;}
        [StringLength(500, ErrorMessage = "GHICHU tối đa 500 ký tự")]
        public String GHICHU {get;set;}
        [StringLength(10, ErrorMessage = "MA_HQ tối đa 10 ký tự")]
        public String MA_HQ {get;set;}
        [Required(ErrorMessage = "HIEU_LUC null")]
        public Boolean HIEU_LUC {get;set;}
		public string NGAY_HL {get;set;}
		public string NGAY_SD {get;set;}
		public string NGAY_KT {get;set;}
        [StringLength(100, ErrorMessage = "VAN_BAN_BH tối đa 100 ký tự")]
        public String VAN_BAN_BH {get;set;}
		public string NGAY_VB {get;set;}
        [Required(ErrorMessage = "NGAY_TAO null")]
        public string NGAY_TAO {get;set;}
        [StringLength(20, ErrorMessage = "VAN_BAN_BH tối đa 20 ký tự")]
        public String NGUOI_TAO {get;set;}
        public string Error { get; set; }
	}
    public class SoapDMDC_QuocGiaModel 
    {
        
        [StringLength(10, ErrorMessage = "MA tối đa 10 ký tự")]
        [Required(ErrorMessage = "MA null")]
        public String MA { get; set; }
        [StringLength(500, ErrorMessage = "TEN tối đa 500 ký tự")]
        [Required(ErrorMessage = "TEN null")]
        public String TEN { get; set; }
        [Required(ErrorMessage = "TEN_TA null")]
        [StringLength(500, ErrorMessage = "TEN_TA tối đa 500 ký tự")]
        public String TEN_TA { get; set; }
        [StringLength(500, ErrorMessage = "GHICHU tối đa 500 ký tự")]
        public String GHICHU { get; set; }
        [StringLength(10, ErrorMessage = "MA_HQ tối đa 10 ký tự")]
        public String MA_HQ { get; set; }
        [Required(ErrorMessage = "HIEU_LUC null")]
        public Boolean HIEU_LUC { get; set; }
        public string NGAY_HL { get; set; }
        public string NGAY_SD { get; set; }
        public string NGAY_KT { get; set; }
        [StringLength(100, ErrorMessage = "VAN_BAN_BH tối đa 100 ký tự")]
        public String VAN_BAN_BH { get; set; }
        public string NGAY_VB { get; set; }
        [Required(ErrorMessage = "NGAY_TAO null")]
        public string NGAY_TAO { get; set; }
        [StringLength(20, ErrorMessage = "VAN_BAN_BH tối đa 20 ký tự")]
        public String NGUOI_TAO { get; set; }
        public string Error { get; set; }
    }

}

