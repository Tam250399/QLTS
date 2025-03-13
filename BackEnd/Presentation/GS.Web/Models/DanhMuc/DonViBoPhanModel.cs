//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(DonViBoPhanValidator))]
	public class DonViBoPhanModel :BaseGSEntityModel
	{
        public DonViBoPhanModel(){
            dllDonViBoPhan = new List<SelectListItem>();
        }
		public String MA {get;set;}
		public String TEN {get;set;}
		public String DIA_CHI {get;set;}
		public String DIEN_THOAI {get;set;}
		public String FAX {get;set;}
		public Decimal? DON_VI_ID {get;set;}
		public Decimal? PARENT_ID {get;set;}
        public String TREE_NODE { get; set; }
        public Decimal? TREE_LEVEL { get; set; }
        //add more
        public string TEN_DON_VI_BO_PHAN_CHA { get; set; }
        public string TEN_DON_VI { get; set; }
        public IList<SelectListItem> dllDonViBoPhan { get; set; }
        public int CountSub { get; set; }
        public int pageIndex { get; set; }
        public int? DB_ID { get; set; }
    }
    public partial class DonViBoPhanSearchModel :BaseSearchModel {
        public DonViBoPhanSearchModel()
        {
        }
        public string KeySearch {get;set;}
        public decimal? NguoiDungId { get; set; }
        public decimal? DonViId { get; set; }
        public decimal? ParentID { get; set; }
        public int? pageIndex { get; set; }
    }
    public partial class DonViBoPhanListModel : BasePagedListModel<DonViBoPhanModel>
    {

    }

    public class IMP_DonViBoPhanModel
    {
        public IMP_DonViBoPhanModel()
        {

        }
        public Decimal? DB_ID { get; set; }
        public String MA { get; set; }
        public String TEN { get; set; }
        public String DIA_CHI { get; set; }
        public String DIEN_THOAI { get; set; }
        public String FAX { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? PARENT_ID { get; set; }
        public String TREE_NODE { get; set; }
        public Decimal? TREE_LEVEL { get; set; }
        public string Error { get; set; }
		public String MA_DON_VI { get; set; }
		public String MA_CHA { get; set; }
		//add more
		public String MESSAGE { get; set; }
    }
}

