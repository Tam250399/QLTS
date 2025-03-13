//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Core.Domain.DanhMuc;
using GS.Web.Framework.Models;
using GS.Web.Validators.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(LoaiTaiSanVoHinhValidator))]
	public class LoaiTaiSanVoHinhModel :BaseGSEntityModel
	{
        public LoaiTaiSanVoHinhModel(){
        
        }
		public String MA {get;set;}
		public String TEN {get;set;}
		public Decimal? LOAI_HINH_TAI_SAN_ID {get;set;}
        [UIHint("InputAddon")]
        public Decimal? HM_THOI_HAN_SU_DUNG {get;set;}
        [UIHint("InputAddon")]
        public Decimal? HM_TY_LE {get;set;}
        [UIHint("InputAddon")]
        public Decimal? KH_THOI_HAN_SU_DUNG {get;set;}
        [UIHint("InputAddon")]
        public Decimal? KH_TY_LE {get;set;}
		public String MO_TA {get;set;}
		public Decimal? CHE_DO_HAO_MON_ID {get;set;}
		public Decimal? PARENT_ID {get;set;}
		public String TREE_NODE {get;set;}
		public Decimal? TREE_LEVEL {get;set;}
		public String DON_VI_TINH {get;set;}
		public Decimal? DON_VI_ID {get;set;}
        //
        public int CountSub { get; set; }
        public string TenLoaiTaiSanVoHinhCha { get; set; }
        public string TenDonViApDung { get; set; }
        public IList<SelectListItem> DanhSachLoaiHinhTaiSan { get; set; }
    }
    public partial class LoaiTaiSanVoHinhSearchModel :BaseSearchModel {
        public LoaiTaiSanVoHinhSearchModel()
        {
        }
        public string KeySearch { get; set; }
        public decimal forDonVi { get; set; }
        public decimal PARENTID { get; set; }
        public decimal TREELEVEL { get; set; }
    }
   public partial class LoaiTaiSanVoHinhListModel : BasePagedListModel<LoaiTaiSanVoHinhModel>
    {
       
    }
}

