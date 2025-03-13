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
    [Validator(typeof(DMDC_ToChucNganSachValidator))]
	public class DMDC_ToChucNganSachModel :BaseGSApiModel
	{
        public DMDC_ToChucNganSachModel(){
        
        }
        [StringLength(7,ErrorMessage ="MA tối đa 7 ký tự")]
        [Required(ErrorMessage = "MA null")]
        public String MA {get;set;}
        [StringLength(240, ErrorMessage = "MA tối đa 240 ký tự")]
        [Required(ErrorMessage = "TEN null")]
        public String TEN {get;set;}
        [StringLength(1, ErrorMessage = "MA_CQT tối đa 1 ký tự")]
        public String MA_CQT {get;set;}
        [StringLength(200, ErrorMessage = "LOAIDV_MA tối đa 200 ký tự")]
        public String LOAIDV_MA {get;set;}
        [StringLength(200, ErrorMessage = "LOAIDV_TEN tối đa 200 ký tự")]
        public String LOAIDV_TEN {get;set;}
        [StringLength(1, ErrorMessage = "CAPDT tối đa 1 ký tự")]
        public String CAPDT {get;set;}
        [StringLength(3, ErrorMessage = "CHUONG tối đa 3 ký tự")]
        public String CHUONG {get;set;}
        [StringLength(200, ErrorMessage = "LHDV_MA tối đa 200 ký tự")]
        public String LHDV_MA {get;set;}
        [StringLength(200, ErrorMessage = "LHDV_TEN tối đa 200 ký tự")]
        public String LHDV_TEN {get;set;}
        [StringLength(25, ErrorMessage = "LHDV_TEN tối đa 25 ký tự")]
        public String SOQD_TL {get;set;}
		public string NGAY_TL {get;set;}
        [StringLength(254, ErrorMessage = "COQUAN_TL tối đa 254 ký tự")]
        public String COQUAN_TL {get;set;}
        [StringLength(200, ErrorMessage = "DVCT_MA tối đa 200 ký tự")]
        public String DVCT_MA {get;set;}
        [StringLength(200, ErrorMessage = "DVCT_TEN tối đa 200 ký tự")]
        public String DVCT_TEN {get;set;}
        [StringLength(200, ErrorMessage = "DVQLTT_MA tối đa 200 ký tự")]
        public String DVQLTT_MA {get;set;}
        [StringLength(200, ErrorMessage = "DVQLTT_TEN tối đa 200 ký tự")]
        public String DVQLTT_TEN {get;set;}
        [StringLength(200, ErrorMessage = "DBN_MA tối đa 200 ký tự")]
        public String DBN_MA {get;set;}
        [StringLength(200, ErrorMessage = "DBN_TEN tối đa 200 ký tự")]
        public String DBN_TEN {get;set;}
        public string Error { get; set; }
	}

}

