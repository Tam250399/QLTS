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
    [Validator(typeof(ThuocTinhTaiSanValidator))]
	public class ThuocTinhTaiSanModel :BaseGSEntityModel
	{
        public ThuocTinhTaiSanModel(){
        
        }
		public Decimal THUOC_TINH_ID {get;set;}
		public Decimal LOAI_HINH_TAI_SAN_ID {get;set;}
		public Decimal? SAP_XEP {get;set;}
		public String TREE_NOTE {get;set;}
		public Decimal LOAI_TAI_SAN_ID {get;set;}
	}
    public partial class ThuocTinhTaiSanSearchModel :BaseSearchModel {
        public ThuocTinhTaiSanSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class ThuocTinhTaiSanListModel : BasePagedListModel<ThuocTinhTaiSanModel>
    {
       
    }
}

