//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/6/2020
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
    [Validator(typeof(LoaiTaiSanKhauHaoValidator))]
	public class LoaiTaiSanKhauHaoModel :BaseGSEntityModel
	{
        public LoaiTaiSanKhauHaoModel(){
        
        }
		public Decimal LOAI_TAI_SAN_ID {get;set;}
		public Decimal DON_VI_ID {get;set;}
        [UIHint("InputAddon")]
        public Decimal? TY_LE_KHAU_HAO {get;set;}
        [UIHint("InputAddon")]
        public Decimal THOI_HAN_SU_DUNG {get;set;}
		public String MA_LOAI_TAI_SAN {get;set;}
		public String MA_DON_VI {get;set; }
        public String TEN_LOAI_TAI_SAN { get; set; }
        public String TEN_DON_VI { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
    }
    public partial class LoaiTaiSanKhauHaoSearchModel :BaseSearchModel {
        public LoaiTaiSanKhauHaoSearchModel()
        {
        }
        public string KeySearch {get;set; }
        public decimal? CheDoId { get; set; }
        public IList<SelectListItem> CheDoAvaliable { get; set; }
        public decimal? ParentId { get; set; }
        public decimal DonViId { get; set; }
    }
   public partial class LoaiTaiSanKhauHaoListModel : BasePagedListModel<LoaiTaiSanKhauHaoModel>
    {
       
    }
}

