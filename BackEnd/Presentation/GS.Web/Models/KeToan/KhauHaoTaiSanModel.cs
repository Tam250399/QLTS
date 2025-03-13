//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 5/6/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.KeToan
{

	public class KhauHaoTaiSanModel :BaseGSEntityModel
	{
        public KhauHaoTaiSanModel(){
        
        }
		public Decimal TAI_SAN_ID {get;set;}
		public String MA_TAI_SAN {get;set;}
		[UIHint("InputYear")]
		public Decimal NAM_KHAU_HAO {get;set;}
		[UIHint("InputAddon")]
		public Decimal? GIA_TRI_KHAU_HAO {get;set;}
		[UIHint("InputAddon")]
		public Decimal? TONG_KHAU_HAO_LUY_KE {get;set;}
		[UIHint("InputAddon")]
		public Decimal? TONG_GIA_TRI_CON_LAI {get;set;}
		public Decimal? TY_LE_KHAU_HAO {get;set;}
		[UIHint("InputAddon")]
		public Decimal? TONG_NGUYEN_GIA {get;set; }
		[UIHint("InputAddon")]
		public Decimal? THANG_KHAU_HAO {get;set;}
	}
    public partial class KhauHaoTaiSanSearchModel :BaseSearchModel {
        public KhauHaoTaiSanSearchModel()
        {
			ddlLoaiHinhTaiSan = new List<SelectListItem>();
		}
        public string KeySearch { get; set; }
		[UIHint("InputAddon")]
		public decimal ThangKhauHao { get; set; }
		[UIHint("InputYear")]
		public decimal NamKhauHao { get; set; }
		public decimal LoaiHinhTaiSan { get; set; }
		public List<SelectListItem> ddlLoaiHinhTaiSan { get; set; }
	}
   public partial class KhauHaoTaiSanListModel : BasePagedListModel<KhauHaoTaiSanModel>
    {
       
    }

	public partial class KhauHaoExport
	{
		[DisplayName("MA")]
		public string MA { get; set; }
		[DisplayName("THANG_KHAU_HAO")]
		public Decimal? THANG_KHAU_HAO { get; set; }
		[DisplayName("NAM_KHAU_HAO")]
		public Decimal? NAM_KHAU_HAO { get; set; }
		[DisplayName("GIA_TRI_KHAU_HAO")]
		public Decimal? GIA_TRI_KHAU_HAO { get; set; }
		[DisplayName("TONG_KHAU_HAO_LUY_KE")]
		public Decimal? TONG_KHAU_HAO_LUY_KE { get; set; }
		[DisplayName("TONG_GIA_TRI_CON_LAI")]
		public Decimal? TONG_GIA_TRI_CON_LAI { get; set; }
		[DisplayName("TI_LE_KHAU_HAO")]
		public Decimal? TI_LE_KHAU_HAO { get; set; }
		[DisplayName("TONG_NGUYEN_GIA")]
		public Decimal? TONG_NGUYEN_GIA { get; set; }
		[DisplayName("MA_FAST")]
		public string MA_FAST { get; set; }
	}
}

