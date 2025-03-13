//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(NhomCongCuValidator))]
	public class NhomCongCuModel :BaseGSEntityModel
	{
        public NhomCongCuModel(){
			//DDL_DON_VI_ID = new List<SelectListItem>();
			selectListsParent = new List<SelectListItem>();
		}
		public String MA {get;set;}
		public String TEN {get;set;}
		[UIHint("InputAddon")]
		public Decimal? THOI_HAN_PHAN_BO {get;set;}
		public String DON_VI_TINH_ID {get;set;}
		public Decimal? PARENT_ID {get;set;}
		public Decimal? DON_VI_ID {get;set;}
		public Decimal? NGUOI_DUNG_ID {get;set;}
		//public List<SelectListItem> DDL_DON_VI_ID {get;set;}
		public List<SelectListItem> selectListsParent { get; set; }
		public string TEN_DON_VI { get; set; }
		public string TEN_CHA { get; set; }
		public int CountSub { get; set; }
		public string stringGuid { get; set; }

	}
    public partial class NhomCongCuSearchModel :BaseSearchModel {
        public NhomCongCuSearchModel()
        {
        }
        public string KeySearch {get;set;}
		public decimal? ParentId { get; set; }
	}
   public partial class NhomCongCuListModel : BasePagedListModel<NhomCongCuModel>
    {
       
    }

	public partial class IMP_NhomCongCuModel
	{
		public String MA { get; set; }
		public String MA_DON_VI { get; set; }
		public String TEN { get; set; }
		public String DON_VI_TINH { get; set; }
		public String MA_CHA { get; set; }
		public decimal THOI_HAN_PHAN_BO { get; set; }
		//add more
		public String MESSAGE { get; set; }

	}
}

