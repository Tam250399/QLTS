//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.CCDC;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.CCDC
{
    [Validator(typeof(ChoThueValidator))]
	public class ChoThueModel :BaseGSEntityModel
	{
        public ChoThueModel()
        {
            DDLDoiTac = new List<SelectListItem>();
        }
		public Decimal CONG_CU_ID {get;set; }
        public Decimal NHAP_XUAT_ID { get; set; }
        public Decimal? NGUOI_TAO_ID {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public String QUYET_DINH_SO {get;set; }
        [UIHint("DateNullable")]
        public DateTime? QUYET_DINH_NGAY {get;set;}
		public String CAP_QUYET_DINH {get;set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_CHO_THUE_FROM {get;set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_CHO_THUE_TO {get;set;}
		public Decimal? KHACH_HANG_ID {get;set;}
		public String HOP_DONG_SO {get;set; }
        [UIHint("DateNullable")]
        public DateTime? HOP_DONG_NGAY {get;set; }
        [UIHint("InputAddon")]
        public Decimal? SO_LUONG {get;set; }
        [UIHint("InputAddon")]
        public Decimal? SO_TIEN_THU_DUOC {get;set;}
		public String GHI_CHU {get;set;}
        //more
        public Decimal MapId { get; set; }
        public Decimal DON_VI_BO_PHAN_ID { get; set; }
        public DateTime? NgayXuatNhap { get; set; }
        public Decimal SoLuongCon { get; set; }
        public List<SelectListItem> DDLDoiTac { get; set; }
        //thong tin cong cu
        public String DonViText { get; set; }
        public String MaCongCuText { get; set; }
        public String TenCongCuText { get; set; }
        public String NhomCongCuText { get; set; }
        public String DonGiaText { get; set; }
        public String SoLuongText { get; set; }
        public String BoPhanSuDungText { get; set; }
        public String stringGuid { get; set; }
        public String NgaySuDungText { get; set; }
        public String MaLoCongCuText { get; set; }
        public decimal? DoiTacId { get; set; }
        public decimal? DON_VI_ID { get; set; }

    }
    public partial class ChoThueSearchModel :BaseSearchModel {
        public ChoThueSearchModel()
        {
            DDLDonViBoPhan = new List<SelectListItem>();
            DDLCongCu = new List<SelectListItem>();
        }
        public string KeySearch {get;set;}
        public string KeySearchCongCu { get; set; }
        public Decimal DonViBoPhanId { get; set; }
        public List<SelectListItem> DDLDonViBoPhan { get; set; }
        public Decimal CongCuId { get; set; }
        public List<SelectListItem> DDLCongCu { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayChoThueTu { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayChoThueDen { get; set; }
    }
   public partial class ChoThueListModel : BasePagedListModel<ChoThueModel>
    {
       
    }
}

