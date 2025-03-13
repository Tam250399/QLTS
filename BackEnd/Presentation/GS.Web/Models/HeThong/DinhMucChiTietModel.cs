//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 18/6/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.HeThong;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.HeThong
{
    [Validator(typeof(DinhMucChiTietValidator))]
	public class DinhMucChiTietModel :BaseGSEntityModel
	{
        public DinhMucChiTietModel(){
        
        }
		public Decimal DINH_MUC_ID {get;set;}
		public Decimal LOAI_TAI_SAN_ID {get;set;}
		public Decimal CHUC_DANH_ID {get;set;}
		public Decimal? LOAI_HINH_TAI_SAN_ID {get;set;}
        [UIHint("InputAddon")]
        public Decimal? DINH_MUC {get;set;}
        [UIHint("InputAddon")]
        public Decimal? SO_LUONG {get;set; }
        public String TEN_CHUC_DANH { get; set; }
        public String TEN_NHOM_TAI_SAN { get; set; }
        public IList<SelectListItem> DDLChucDanh { get; set; }
        public IList<SelectListItem> DDLNhomTaiSan { get; set; }
        public IList<SelectListItem> DDLloaiHinhTaiSan { get; set; }
        public string _arr { get; set; }
    }
    public partial class DinhMucChiTietSearchModel :BaseSearchModel {
        public DinhMucChiTietSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class DinhMucChiTietListModel : BasePagedListModel<DinhMucChiTietModel>
    {
       
    }
}

