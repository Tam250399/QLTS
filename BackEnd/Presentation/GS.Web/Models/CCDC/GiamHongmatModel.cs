//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.CCDC;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.CCDC
{
    [Validator(typeof(GiamHongmatValidator))]
	public class GiamHongmatModel :BaseGSEntityModel
	{
        public GiamHongmatModel(){
        
        }
		public Decimal CONG_CU_ID {get;set;}
		public Decimal NHAP_XUAT_ID {get;set;}
		public Decimal? DON_VI_ID {get;set;}
		public Decimal? DON_VI_BO_PHAN_ID {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public Decimal? NGUOI_TAO {get;set;}
		public String LY_DO {get;set;}
		public String GHI_CHU {get;set; }
        public Decimal SO_LUONG { get; set; }
        [UIHint("Date")]
        public DateTime NGAY_LAP { get; set; }

        // more
        public String stringGuid { get; set; }
        public String BoPhanSuDungText { get; set; }
        public String MaCongCuText { get; set; }
        public String TenCongCuText { get; set; }
        public String NhomCongCuText { get; set; }
        public String DonGiaText { get; set; }
        public String SoLuongText { get; set; }
        public String NgayPhanBoText { get; set; }
        public Decimal MapId { get; set; }
        public DateTime? NgayPhanBo { get; set; }
    }
    public partial class GiamHongmatSearchModel :BaseSearchModel {
        public GiamHongmatSearchModel()
        {
            DDLDonViBoPhan = new List<SelectListItem>();
        }
        public string KeySearch {get;set;}
        public String KeySearchCongCu { get; set; }
        public Decimal DonViBoPhanId { get; set; }
        public List<SelectListItem> DDLDonViBoPhan { get; set; }
    }
   public partial class GiamHongmatListModel : BasePagedListModel<GiamHongmatModel>
    {
       
    }
}

