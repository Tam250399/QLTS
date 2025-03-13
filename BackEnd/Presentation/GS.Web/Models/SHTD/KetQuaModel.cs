//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.SHTD;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.SHTD
{
    [Validator(typeof(KetQuaValidator))]
	public class KetQuaModel :BaseGSEntityModel
	{
        public KetQuaModel(){
            this.GUID = Guid.NewGuid();
            this.DDLDonVi = new List<SelectListItem>();
            this.ListModel = new List<KetQuaModel>();
            this.is_Delete = true;

        }
		public Decimal TAI_SAN_TD_XU_LY_ID {get;set;}
		public Decimal? DON_VI_CHUYEN_ID {get;set;}
        [UIHint("InputAddon")]
        public Decimal? SO_LUONG { get; set; }
        public Decimal? GIA_TRI_NSNN {get;set;}
		public Decimal? GIA_TRI_TKTG {get;set;}
		public Decimal? CHI_PHI_XU_LY {get;set;}
		public String HOP_DONG_SO {get;set;}
		public DateTime? HOP_DONG_NGAY {get;set;}
		public Guid? GUID {get;set;}
        [UIHint("InputAddon")]
        public Decimal? GIA_TRI_BAN {get;set;}
		public String HO_SO_GIAY_TO_KHAC {get;set;}
        [UIHint("DateNullable")]
        public DateTime? NGAY_XU_LY {get;set; }
        public String DB_ID { get; set; }
        public String DB_TAI_SAN_XU_LY_ID { get; set; }

        //more 
        public bool is_Delete { get; set; }
        public Decimal SoLuongDC { get; set; }
        public Decimal SoLuongXuLy { get; set; }
        public List<SelectListItem> DDLDonVi { get; set; }
        public List<KetQuaModel> ListModel { get; set; }
    }
    public partial class KetQuaSearchModel :BaseSearchModel {
        public KetQuaSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class KetQuaListModel : BasePagedListModel<KetQuaModel>
    {
       
    }
}

