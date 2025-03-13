//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.SHTD;

namespace GS.Web.Models.SHTD
{
    [Validator(typeof(NhatKyTaiSanToanDanValidator))]
	public class NhatKyTaiSanToanDanModel :BaseGSEntityModel
	{
        public NhatKyTaiSanToanDanModel(){
        
        }
		public Decimal? QUYET_DINH_ID {get;set;}
		public Decimal? NGUOI_DUNG_ID {get;set;}
		public Decimal? TRANG_THAI_ID {get;set;}
		public String DATA_JSON {get;set;}
	}
    public partial class NhatKyTaiSanToanDanSearchModel :BaseSearchModel {
        public NhatKyTaiSanToanDanSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class NhatKyTaiSanToanDanListModel : BasePagedListModel<NhatKyTaiSanToanDanModel>
    {
       
    }
}

