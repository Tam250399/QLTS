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
    [Validator(typeof(DMDC_DuAnValidator))]
    public class DMDC_DuAnModel :BaseGSApiModel
	{
        public DMDC_DuAnModel(){
        
        }
        [StringLength(20,ErrorMessage = "MA_CHA tối đa 20 ký tự")]
		public String MA_CHA {get;set;}
        [StringLength(20, ErrorMessage = "MA tối đa 20 ký tự")]
        [Required(ErrorMessage = "MA null")]
        public String MA {get;set;}
        [StringLength(254, ErrorMessage = "TEN tối đa 254 ký tự")]
        [Required(ErrorMessage = "TEN null")]
        public String TEN {get;set;}
        [Required(ErrorMessage = "CQTC_MA null")]
        [StringLength(7, ErrorMessage = "CQTC_MA tối đa 7 ký tự")]
        public String CQTC_MA {get;set;}
        [StringLength(30, ErrorMessage = "MA_THAMCHIEU tối đa 30 ký tự")]
        public String MA_THAMCHIEU {get;set;}
        [StringLength(200, ErrorMessage = "DVQLTT_MA tối đa 200 ký tự")]
        public String DVQLTT_MA {get;set;}
        [StringLength(200, ErrorMessage = "DVQLTT_TEN tối đa 200 ký tự")]
        public String DVQLTT_TEN {get;set;}
        [StringLength(200, ErrorMessage = "CDT_MA tối đa 200 ký tự")]
        public String CDT_MA {get;set;}
        [StringLength(200, ErrorMessage = "CDT_TEN tối đa 200 ký tự")]
        public String CDT_TEN {get;set;}
        [StringLength(200, ErrorMessage = "BQL_MA tối đa 200 ký tự")]
        public String BQL_MA {get;set;}
        [StringLength(200, ErrorMessage = "BQL_TEN tối đa 200 ký tự")]
        public String BQL_TEN {get;set;}
        [StringLength(200, ErrorMessage = "LOAIDA_MA tối đa 200 ký tự")]
        public String LOAIDA_MA {get;set;}
        [StringLength(200, ErrorMessage = "LOAIDA_TEN tối đa 200 ký tự")]
        public String LOAIDA_TEN {get;set;}
        [StringLength(200, ErrorMessage = "NHOMDA_MA tối đa 200 ký tự")]
        public String NHOMDA_MA {get;set;}
        [StringLength(200, ErrorMessage = "NHOMDA_TEN tối đa 200 ký tự")]
        public String NHOMDA_TEN {get;set;}
        [StringLength(200, ErrorMessage = "CTMT_MA tối đa 200 ký tự")]
        public String CTMT_MA {get;set;}
        [StringLength(200, ErrorMessage = "CTMT_TEN tối đa 200 ký tự")]
        public String CTMT_TEN {get;set;}
        [StringLength(200, ErrorMessage = "HTDA_MA tối đa 200 ký tự")]
        public String HTDA_MA {get;set;}
        [StringLength(200, ErrorMessage = "HTDA_TEN tối đa 200 ký tự")]
        public String HTDA_TEN {get;set;}
        [StringLength(200, ErrorMessage = "HTQL_MA tối đa 200 ký tự")]
        public String HTQL_MA {get;set;}
        [StringLength(200, ErrorMessage = "HTQL_TEN tối đa 200 ký tự")]
        public String HTQL_TEN {get;set;}        
        public string NGAY_TAO {get;set;}
        [StringLength(200, ErrorMessage = "NGUOI_TAO tối đa 200 ký tự")]
        public String NGUOI_TAO {get;set;}      
        public string NGAY_SUA {get;set;}
        [StringLength(200, ErrorMessage = "NGUOI_SUA tối đa 200 ký tự")]
        public String NGUOI_SUA {get;set;}
        [StringLength(200, ErrorMessage = "LY_DO_SUA tối đa 200 ký tự")]
        public String LY_DO_SUA {get;set;}
        [StringLength(500, ErrorMessage = "SOQD_TL tối đa 500 ký tự")]
        public String SOQD_TL {get;set;}
		public string NGAY_TL {get;set;}
        [StringLength(254, ErrorMessage = "COQUAN_TL tối đa 254 ký tự")]
        public String COQUAN_TL {get;set;}
        [StringLength(2, ErrorMessage = "TRANGTHAI_MA tối đa 2 ký tự")]
        public String TRANGTHAI_MA {get;set;}
        [StringLength(2, ErrorMessage = "TRANGTHAI_CU tối đa 2 ký tự")]
        public String TRANGTHAI_CU {get;set;}
		public Decimal? TRANGTHAI_DM {get;set;}
		public string NGAY_DMO {get;set;}
        public string Error { get; set; }
	}
}

