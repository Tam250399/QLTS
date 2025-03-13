//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.ThuocTinh;

namespace GS.Web.Models.ThuocTinh
{
    [Validator(typeof(ThuocTinhDataValidator))]
	public class ThuocTinhDataModel :BaseGSEntityModel
	{
        public ThuocTinhDataModel(){
        
        }
		public Decimal? THUOC_TINH_ID {get;set;}
		public String DATA {get;set;}
		public Decimal? DON_VI_ID {get;set;}
		public Decimal? DON_VI_BO_PHAN_ID {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public DateTime? NGAY_CAP_NHAT {get;set;}
		public Decimal NGUOI_TAO_ID {get;set;}
        public Decimal? TAI_SAN_ID { get; set; }
        public Decimal? TAI_SAN_TD_XU_LY_ID { get; set; }
    }
    public partial class ThuocTinhDataSearchModel :BaseSearchModel {
        public ThuocTinhDataSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class ThuocTinhDataListModel : BasePagedListModel<ThuocTinhDataModel>
    {
       
    }
}

