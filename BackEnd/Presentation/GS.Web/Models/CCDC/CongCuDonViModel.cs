//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.CCDC;

namespace GS.Web.Models.CCDC
{
    [Validator(typeof(CongCuDonViValidator))]
	public class CongCuDonViModel :BaseGSEntityModel
	{
        public CongCuDonViModel(){
        
        }
		public Decimal? CONG_CU_ID {get;set;}
		public Decimal? DON_VI_BO_PHAN_ID {get;set;}
		public Decimal? DON_VI_ID {get;set;}
		public Decimal? SO_LUONG_ID {get;set;}
	}
    public partial class CongCuDonViSearchModel :BaseSearchModel {
        public CongCuDonViSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class CongCuDonViListModel : BasePagedListModel<CongCuDonViModel>
    {
       
    }
}

