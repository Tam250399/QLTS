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
    
    public class DMDC_DonViNganSachModel : BaseGSApiModel
    {     
        public DMDC_DonViNganSachModel()
        {

        }
        [StringLength(7, ErrorMessage = "MA tối đa 7 ký tự")]
        [Required(ErrorMessage = "MA null")]
        public String MA { get; set; }
        [StringLength(22, ErrorMessage = "MA tối đa 22 ký tự")]
        public String ID_CHA { get; set; }
        [StringLength(5, ErrorMessage = "MA_V tối đa 5 ký tự")]
        public String MA_CHA { get; set; }
        [StringLength(10, ErrorMessage = "MA tối đa 10 ký tự")]
        [Required(ErrorMessage = "MA_DB null")]
        public String MA_DB { get; set; }
        [StringLength(5, ErrorMessage = "MA_V tối đa 5 ký tự")]
        public String MA_V { get; set; }
        [StringLength(2, ErrorMessage = "MA_T tối đa 2 ký tự")]
        [Required(ErrorMessage = "MA_T null")]
        public String MA_T { get; set; }
        [StringLength(3, ErrorMessage = "MA_H tối đa 3 ký tự")]
        public String MA_H { get; set; }
        [StringLength(5, ErrorMessage = "MA_X tối đa 5 ký tự")]
        public String MA_X { get; set; }
        [StringLength(50, ErrorMessage = "MA_CU tối đa 50 ký tự")]
        public String MA_CU { get; set; }
        [StringLength(170, ErrorMessage = "TEN tối đa 170 ký tự")]
        [Required(ErrorMessage = "TEN null")]
        public String TEN { get; set; }
        /// <summary>
        /// 
        /// </summary>

        [StringLength(1, ErrorMessage = "MA_CQT tối đa 1 ký tự")]
        public String MA_CQT { get; set; }
        [StringLength(30, ErrorMessage = "MA_THAMCHIEU tối đa 30 ký tự")]
        public String MA_THAMCHIEU { get; set; }
        [StringLength(200, ErrorMessage = "LOAIDV_MA tối đa 200 ký tự")]
        public String LOAIDV_MA { get; set; }
        [StringLength(200, ErrorMessage = "LOAIDV_TEN tối đa 200 ký tự")]
        public String LOAIDV_TEN { get; set; }
        [StringLength(1, ErrorMessage = "CAPDT tối đa 1 ký tự")]
        public String CAPDT { get; set; }
        [StringLength(3, ErrorMessage = "CHUONG tối đa 3 ký tự")]
        public String CHUONG { get; set; }
        [StringLength(200, ErrorMessage = "LHDV_MA tối đa 200 ký tự")]
        public String LHDV_MA { get; set; }
        [StringLength(200, ErrorMessage = "LHDV_TEN tối đa 200 ký tự")]
        public String LHDV_TEN { get; set; }
        [StringLength(25, ErrorMessage = "LHDV_TEN tối đa 25 ký tự")]
        public String SOQD_TL { get; set; }
        public string NGAY_TL { get; set; }
        [StringLength(254, ErrorMessage = "COQUAN_TL tối đa 254 ký tự")]
        public String COQUAN_TL { get; set; }
        [StringLength(200, ErrorMessage = "DVCT_MA tối đa 200 ký tự")]
        public String DVCT_MA { get; set; }
        [StringLength(200, ErrorMessage = "DVCT_TEN tối đa 200 ký tự")]
        public String DVCT_TEN { get; set; }
        [StringLength(200, ErrorMessage = "DVQLTT_MA tối đa 200 ký tự")]
        public String DVQLTT_MA { get; set; }
        [StringLength(200, ErrorMessage = "DVQLTT_TEN tối đa 200 ký tự")]
        public String DVQLTT_TEN { get; set; }
        [StringLength(200, ErrorMessage = "DBN_MA tối đa 200 ký tự")]
        public String DBN_MA { get; set; }
        [StringLength(200, ErrorMessage = "DBN_TEN tối đa 200 ký tự")]
        public String DBN_TEN { get; set; }
        [StringLength(254, ErrorMessage = "DIACHI tối đa 254 ký tự")]
        public String DIACHI { get; set; }
        [StringLength(1, ErrorMessage = "DVDTCT tối đa 1 ký tự")]
        public String DVDTCT { get; set; }
        [StringLength(1, ErrorMessage = "DVDTCD tối đa 1 ký tự")]
        public String DVDTCD { get; set; }
        [StringLength(200, ErrorMessage = "NGUOI_SUA tối đa 200 ký tự")]
        public String NGUOI_SUA { get; set; }
        [StringLength(254, ErrorMessage = "CCH_TEN tối đa 254 ký tự")]
        public String CCH_TEN { get; set; }
        [StringLength(2, ErrorMessage = "TRANGTHAI_MA tối đa 2 ký tự")]
        public String TRANGTHAI_MA { get; set; }
        [StringLength(2, ErrorMessage = "TRANGTHAI_CU tối đa 2 ký tự")]
        public String TRANGTHAI_CU { get; set; }
        public Decimal? TRANGTHAI_DM { get; set; }
        public string NGAY_DMO { get; set; }
        [StringLength(200, ErrorMessage = "NGUOI_TAO tối đa 200 ký tự")]
        public String NGUOI_TAO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NGAY_TAO { get; set; }
        public string NGAY_SUA { get; set; }
        public string Error { get; set; }
        [Required(ErrorMessage = "LOAI null")]
        public decimal LOAI { get; set; }
        [Required(ErrorMessage = "HIEU_LUC null")]
        public Boolean HIEU_LUC { get; set; }
        public string NGAY_SD { get; set; }
        [Required(ErrorMessage = "NGAY_HL null")]
        public string NGAY_HL { get; set; }
        public string NGAY_KT { get; set; }
        [StringLength(254, ErrorMessage = "VAN_BAN_BH tối đa 254 ký tự")]
        [Required(ErrorMessage = "VAN_BAN_BH null")]
        public String VAN_BAN_BH { get; set; }
        public string NGAY_VB { get; set; }
        public List<string> ListSuccess { get; set; }
        public List<string> ListError { get; set; }
    }

    public class SoapDMDC_DonViNganSachModel
    {
        public SoapDMDC_DonViNganSachModel()
        {

        }
        [StringLength(7, ErrorMessage = "MA tối đa 7 ký tự")]
        [Required(ErrorMessage = "MA null")]
        public String MA { get; set; }
        [StringLength(10, ErrorMessage = "MA tối đa 10 ký tự")]
        [Required(ErrorMessage = "MA_DB null")]
        public String MA_DB { get; set; }
        [StringLength(10, ErrorMessage = "MA_V tối đa 10 ký tự")]

        public String MA_V { get; set; }
        [StringLength(10, ErrorMessage = "MA_T tối đa 10 ký tự")]
        [Required(ErrorMessage = "MA_T null")]
        public String MA_T { get; set; }
        [StringLength(10, ErrorMessage = "MA_H tối đa 10 ký tự")]
        public String MA_H { get; set; }
        [StringLength(10, ErrorMessage = "MA_X tối đa 10 ký tự")]
        public String MA_X { get; set; }
        [StringLength(50, ErrorMessage = "MA_CU tối đa 50 ký tự")]
        public String MA_CU { get; set; }
        [StringLength(254, ErrorMessage = "TEN tối đa 254 ký tự")]
        [Required(ErrorMessage = "TEN null")]
        public String TEN { get; set; }
        [StringLength(1, ErrorMessage = "MA_CQT tối đa 1 ký tự")]
        public String MA_CQT { get; set; }
        [StringLength(30, ErrorMessage = "MA_THAMCHIEU tối đa 30 ký tự")]
        public String MA_THAMCHIEU { get; set; }
        [StringLength(200, ErrorMessage = "LOAIDV_MA tối đa 200 ký tự")]
        public String LOAIDV_MA { get; set; }
        [StringLength(200, ErrorMessage = "LOAIDV_TEN tối đa 200 ký tự")]
        public String LOAIDV_TEN { get; set; }
        [StringLength(1, ErrorMessage = "CAPDT tối đa 1 ký tự")]
        public String CAPDT { get; set; }
        [StringLength(3, ErrorMessage = "CHUONG tối đa 3 ký tự")]
        public String CHUONG { get; set; }
        [StringLength(200, ErrorMessage = "LHDV_MA tối đa 200 ký tự")]
        public String LHDV_MA { get; set; }
        [StringLength(200, ErrorMessage = "LHDV_TEN tối đa 200 ký tự")]
        public String LHDV_TEN { get; set; }
        [StringLength(25, ErrorMessage = "LHDV_TEN tối đa 25 ký tự")]
        public String SOQD_TL { get; set; }
        public string NGAY_TL { get; set; }
        [StringLength(254, ErrorMessage = "COQUAN_TL tối đa 254 ký tự")]
        public String COQUAN_TL { get; set; }
        [StringLength(200, ErrorMessage = "DVCT_MA tối đa 200 ký tự")]
        public String DVCT_MA { get; set; }
        [StringLength(200, ErrorMessage = "DVCT_TEN tối đa 200 ký tự")]
        public String DVCT_TEN { get; set; }
        [StringLength(200, ErrorMessage = "DVQLTT_MA tối đa 200 ký tự")]
        public String DVQLTT_MA { get; set; }
        [StringLength(200, ErrorMessage = "DVQLTT_TEN tối đa 200 ký tự")]
        public String DVQLTT_TEN { get; set; }
        [StringLength(200, ErrorMessage = "DBN_MA tối đa 200 ký tự")]
        public String DBN_MA { get; set; }
        [StringLength(200, ErrorMessage = "DBN_TEN tối đa 200 ký tự")]
        public String DBN_TEN { get; set; }
        [StringLength(254, ErrorMessage = "DIACHI tối đa 254 ký tự")]
        public String DIACHI { get; set; }
        [StringLength(1, ErrorMessage = "DVDTCT tối đa 1 ký tự")]
        public String DVDTCT { get; set; }
        [StringLength(1, ErrorMessage = "DVDTCD tối đa 1 ký tự")]
        public String DVDTCD { get; set; }
        public string NGAY_TAO { get; set; }
        [StringLength(200, ErrorMessage = "NGUOI_TAO tối đa 200 ký tự")]
        public String NGUOI_TAO { get; set; }
        public string NGAY_SUA { get; set; }
        [StringLength(200, ErrorMessage = "NGUOI_SUA tối đa 200 ký tự")]
        public String NGUOI_SUA { get; set; }
        [StringLength(254, ErrorMessage = "CCH_TEN tối đa 254 ký tự")]
        public String CCH_TEN { get; set; }
        [StringLength(2, ErrorMessage = "TRANGTHAI_MA tối đa 2 ký tự")]
        public String TRANGTHAI_MA { get; set; }
        [StringLength(2, ErrorMessage = "TRANGTHAI_CU tối đa 2 ký tự")]
        public String TRANGTHAI_CU { get; set; }
        public Decimal? TRANGTHAI_DM { get; set; }
        public string NGAY_DMO { get; set; }
        public string Error { get; set; }
        [Required(ErrorMessage = "LOAI null")]
        public Decimal LOAI { get; set; }
        [StringLength(10, ErrorMessage = "MA_THAMCHIEU tối đa 10 ký tự")]
        [Required(ErrorMessage = "HIEU_LUC null")]
        public Boolean HIEU_LUC { get; set; }
        public string NGAY_SD { get; set; }
        public string NGAY_HL { get; set; }
        public string NGAY_KT { get; set; }
        [StringLength(254, ErrorMessage = "VAN_BAN_BH tối đa 254 ký tự")]
        public String VAN_BAN_BH { get; set; }
        public string NGAY_VB { get; set; }
    }

}

