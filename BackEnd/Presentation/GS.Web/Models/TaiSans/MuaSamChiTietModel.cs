//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 21/7/2020
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
    [Validator(typeof(MuaSamChiTietValidator))]
	public class MuaSamChiTietModel :BaseGSEntityModel
	{
        public MuaSamChiTietModel(){
        
        }
		public Decimal MUA_SAM_ID {get;set;}
		public Decimal? LOAI_TAI_SAN_ID {get;set;}
		public String TEN_TAI_SAN {get;set;}
		public Decimal? HINH_THUC_MUA_SAM_ID {get;set;}
		public String DAC_DIEM {get;set;}
		[UIHint("InputAddon")]
		public Decimal? SO_LUONG {get;set;}
		[UIHint("InputAddon")]
		public Decimal? DON_GIA {get;set; }
		[UIHint("DateNullable")]
		public DateTime? THOI_GIAN_DU_KIEN {get;set;}
		[UIHint("InputAddon")]
		public Decimal? DU_TOAN_DUOC_DUYET {get;set;}
		public Boolean IS_TAI_SAN_NGUON_VIEN_TRO {get;set;}
		public String DE_XUAT {get;set;}
		public String GHI_CHU {get;set;}
		public String LoaiTaiSanTen { get;set;}
		public String TenHinhThucMuaSam { get;set; }
		public IList<SelectListItem> LoaiTaiSanAvailable { get; set; }
		public IList<SelectListItem> HinhThucMuaSamAvailable { get; set; }
		[UIHint("InputAddon")]
		public Decimal? THANH_TIEN { get; set; }
		public string strVNDonGia { get; set; }
		public string strVNSoLuong { get; set; }
		public string strVNDuToanDuocDuyet { get; set; }
		public string strVNThanhTien { get; set; }
	}
    public partial class MuaSamChiTietSearchModel :BaseSearchModel {
        public MuaSamChiTietSearchModel()
        {
        }
        public string KeySearch {get;set;}
        public decimal? MUA_SAM_ID { get;set;}
    }
   public partial class MuaSamChiTietListModel : BasePagedListModel<MuaSamChiTietModel>
    {
       
    }
}

