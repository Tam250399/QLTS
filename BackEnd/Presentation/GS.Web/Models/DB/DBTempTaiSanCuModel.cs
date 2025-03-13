//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.DB;

namespace GS.Web.Models.DB
{
    [Validator(typeof(DBTempTaiSanCuValidator))]
	public class DBTempTaiSanCuModel :BaseGSEntityModel
	{
        public DBTempTaiSanCuModel(){
        
        }
		public String MA_TAI_SAN {get;set;}
		public String TEN_TAI_SAN {get;set;}
		public Decimal NGUON_ID {get;set;}
		public String LOAI_TAI_SAN_MA {get;set;}
		public DateTime? NGAY_NHAP {get;set;}
		public DateTime? NGAY_DUYET {get;set;}
		public DateTime? NGAY_SU_DUNG {get;set;}
		public Decimal? LOAI_HINH_TAI_SAN_ID {get;set;}
		public String DON_VI_MA {get;set;}
		public Decimal? TRANG_THAI {get;set;}
		public String DON_VI_BO_PHAN_MA {get;set;}
		public String NUOC_SAN_XUAT_MA {get;set;}
		public Decimal? NAM_SAN_XUAT {get;set;}
		public Decimal? CHE_DO_HAO_MON_ID {get;set;}
		public String NHOM_DON_VI_MA {get;set;}
		public Decimal? NAM {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public DateTime? NGAY_SUA {get;set;}
		public String DATA_JSON {get;set;}
		public String RESPONSE {get;set;}
		public Decimal? TRANG_THAI_DB {get;set;}
	}
    public partial class DBTempTaiSanCuSearchModel :BaseSearchModel {
        public DBTempTaiSanCuSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class DBTempTaiSanCuListModel : BasePagedListModel<DBTempTaiSanCuModel>
    {
       
    }
}

