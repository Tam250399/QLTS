//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 28/5/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Core.Domain.TaiSans;
using GS.Web.Framework.Models;
using GS.Web.Validators.TaiSans;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.TaiSans
{
    [Validator(typeof(TaiSanKhauHaoValidator))]
	public class TaiSanKhauHaoModel :BaseGSEntityModel
	{
        public TaiSanKhauHaoModel(){
        
        }
        [UIHint("DateNullable")]
        public DateTime? NGAY_BAT_DAU {get;set;}
        [UIHint("DateNullable")]
        public DateTime? NGAY_KET_THUC {get;set;}
        [UIHint("InputAddon")]
        public Decimal? SO_THANG_KHAU_HAO {get;set;}
		public Decimal? TAI_SAN_ID {get;set;}
		public Decimal? taisankhauhaoID { get;set;}
		public Decimal? pageIndex { get;set;}
        [UIHint("InputAddon")]
        public Decimal? TY_LE_KHAU_HAO {get;set;}
        [UIHint("InputAddon")]
        public Decimal? TY_LE_NGUYEN_GIA_KHAU_HAO {get;set;}
        public DateTime? NgayBatDauMin { get; set; }
        public TaiSanModel taiSanModel { get; set; }
        public bool IsEnableNgayKH { get; set; }
        public bool IsLastTSKH { get; set; }
    }
    public partial class TaiSanKhauHaoSearchModel :BaseSearchModel {
        public TaiSanKhauHaoSearchModel()
        {
        }
        public string KeySearch {get;set;}
        public decimal? TaiSanId { get; set; }
    }
   public partial class TaiSanKhauHaoListModel : BasePagedListModel<TaiSanKhauHaoModel>
    {
       
    }
}

