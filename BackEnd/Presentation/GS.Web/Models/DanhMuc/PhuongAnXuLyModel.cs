//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(HinhThucXuLyValidator))]
	public class HinhThucXuLyModel :BaseGSEntityModel
	{
        public HinhThucXuLyModel(){
            this.DDLPhuongAnXuLy = new List<SelectListItem>();
            this.ListHinhXuLyId = new List<int>();

        }
		public String MA {get;set;}
		public String TEN {get;set;}
		public Decimal? PHUONG_AN_XU_LY_ID { get;set;}
        public string TEN_PHUONG_AN_XU_LY { get; set; }
        public Decimal? SAP_XEP {get;set;}
        public List<int> ListHinhXuLyId { get; set; }
        public List<SelectListItem> DDLPhuongAnXuLy { get; set; }
	}
    public partial class HinhThucXuLySearchModel :BaseSearchModel {
        public HinhThucXuLySearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class HinhThucXuLyListModel : BasePagedListModel<HinhThucXuLyModel>
    {
       
    }
}

