//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 5/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.TaiSans;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.TaiSans
{
    [Validator(typeof(TaiSanChoThueValidator))]
	public class TaiSanChoThueModel :BaseGSEntityModel
	{
        public TaiSanChoThueModel(){
			LoaiDoiTacAvaiable = new List<SelectListItem>();
		}
		public Decimal? DOI_TAC_ID {get;set;}
		public string DOI_TAC_TEN { get; set; }
		public decimal? LOAI_DOI_TAC_ID { get; set; }
        public IList<SelectListItem> LoaiDoiTacAvaiable { get; set; }
        public Decimal TAI_SAN_ID {get;set;}
		public String HOP_DONG_SO { get;set;}
		[UIHint("DateNullable")]
		public DateTime? HOP_DONG_NGAY { get;set;}
		public String HOA_DON_SO {get;set;}
		[UIHint("DateNullable")]
		public DateTime? HOA_DON_NGAY {get;set;}
		[UIHint("InputAddon")]
		public Decimal? DON_GIA_CHO_THUE {get;set;}
		[UIHint("InputAddon")]
		public Decimal? THU_TU_CHO_THUE {get;set;}
		[UIHint("InputAddon")]
		public Decimal? NOP_NGAN_SACH {get;set;}
		[UIHint("InputAddon")]
		public Decimal? GIU_LAI_DON_VI {get;set;}
		public String GHI_CHU {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public DateTime? NGAY_UPDATE {get;set;}
		public Decimal? NGUOI_TAO_ID {get;set;}
		[UIHint("DateNullable")]
		public DateTime? NGAY_CHO_THUE_FROM {get;set;}
		[UIHint("DateNullable")]
		public DateTime? NGAY_CHO_THUE_TO {get;set;}
		//add more
		public string DonViSuDungTaiSan { get; set; }
		public string MaTaiSan { get; set; }
		public string TenTaiSan { get; set; }
		public string LoaiHinhTaiSan { get; set; }
		public string BoPhanSuDung { get; set; }
		public string TenDoiTac { get; set; }
		public string DonGiaChoThueText { get; set; }
		public string ChiPhiSuDung { get; set; }
		public DateTime? ngaykekhaits { get; set; }
	}
    public partial class TaiSanChoThueSearchModel :BaseSearchModel {
        public TaiSanChoThueSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class TaiSanChoThueListModel : BasePagedListModel<TaiSanChoThueModel>
    {
       
    }
}

