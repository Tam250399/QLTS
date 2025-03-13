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
    [Validator(typeof(DMDC_DonViDuAnValidator))]
    public class DMDC_DonViDuAnModel :BaseGSApiModel
	{
        public DMDC_DonViDuAnModel(){
        
        }
        [StringLength(7,ErrorMessage =( "MA tối đa 7 ký tự"))]
        [Required(ErrorMessage = "MA null")]
		public String MA {get;set;}
        [StringLength(240, ErrorMessage = ("TEN tối đa 240 ký tự"))]
        [Required(ErrorMessage = "TEN null")]
        public String TEN {get;set;}
        [StringLength(1, ErrorMessage = ("MA_CQT tối đa 1 ký tự"))]
        public String MA_CQT {get;set;}
        [StringLength(200, ErrorMessage = ("LOAIDV_MA tối đa 200 ký tự"))]
        public String LOAIDV_MA {get;set;}
        [StringLength(200, ErrorMessage = ("LOAIDV_TEN tối đa 200 ký tự"))]
        public String LOAIDV_TEN {get;set;}
        [StringLength(1, ErrorMessage = ("CAPDT tối đa 1 ký tự"))]
        public String CAPDT {get;set;}
        [StringLength(3, ErrorMessage = ("CHUONG tối đa 3 ký tự"))]
        public String CHUONG {get;set;}
        [StringLength(200, ErrorMessage = ("LHDV_MA tối đa 200 ký tự"))]
        public String LHDV_MA {get;set;}
        [StringLength(200, ErrorMessage = ("LHDV_TEN tối đa 200 ký tự"))]
        public String LHDV_TEN {get;set;}
        [StringLength(25, ErrorMessage = ("SOQD_TL tối đa 25 ký tự"))]
        public String SOQD_TL {get;set;}
		public string NGAY_TL {get;set;}
        [StringLength(254, ErrorMessage = ("COQUAN_TL tối đa 254 ký tự"))]
        public String COQUAN_TL {get;set;}
        [StringLength(200, ErrorMessage = ("DVCT_MA tối đa 200 ký tự"))]
        public String DVCT_MA {get;set;}
        [StringLength(200, ErrorMessage = ("DVCT_TEN tối đa 200 ký tự"))]
        public String DVCT_TEN {get;set;}
        [StringLength(200, ErrorMessage = ("DVQLTT_MA tối đa 200 ký tự"))]
        public String DVQLTT_MA {get;set;}
        [StringLength(200, ErrorMessage = ("DVQLTT_TEN tối đa 200 ký tự"))]
        public String DVQLTT_TEN {get;set;}
        [StringLength(200, ErrorMessage = ("DBN_MA tối đa 200 ký tự"))]
        public String DBN_MA {get;set;}
        [StringLength(200, ErrorMessage = ("DBN_TEN tối đa 200 ký tự"))]
        public String DBN_TEN {get;set;}
        [StringLength(254, ErrorMessage = ("DBN_TEN tối đa 254 ký tự"))]
        public String DIACHI {get;set;}
        [StringLength(1, ErrorMessage = ("DVDTCT tối đa 1 ký tự"))]
        public String DVDTCT {get;set;}
        [StringLength(1, ErrorMessage = ("DVDTCD tối đa 1 ký tự"))]
        public String DVDTCD {get;set;}
		public string NGAY_TAO {get;set;}
        [StringLength(20, ErrorMessage = ("DVDTCD tối đa 20 ký tự"))]
        public String NGUOI_TAO {get;set;}
		public string NGAY_SUA {get;set;}
        [StringLength(200, ErrorMessage = ("NGUOI_SUA tối đa 200 ký tự"))]
        public String NGUOI_SUA {get;set;}
        [StringLength(254, ErrorMessage = ("CCH_TEN tối đa 254 ký tự"))]
        public String CCH_TEN {get;set;}
        [StringLength(2, ErrorMessage = ("TRANGTHAI_MA tối đa 2 ký tự"))]
        public String TRANGTHAI_MA {get;set;}
        [StringLength(2, ErrorMessage = ("TRANGTHAI_CU tối đa 2 ký tự"))]
        public String TRANGTHAI_CU {get;set;}
        [StringLength(30, ErrorMessage = ("MA_THAMCHIEU tối đa 30 ký tự"))]
        public String MA_THAMCHIEU {get;set;}
        public string Error { get; set; }
	}
    
}

