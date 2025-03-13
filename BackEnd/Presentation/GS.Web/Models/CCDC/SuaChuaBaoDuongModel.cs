//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/2/2020
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
    [Validator(typeof(SuaChuaBaoDuongValidator))]
	public class SuaChuaBaoDuongModel :BaseGSEntityModel
	{
        public SuaChuaBaoDuongModel()
        {
            DDLDoiTac = new List<SelectListItem>();
        }
        public Decimal NHAP_XUAT_ID { get; set; }
        public Decimal CONG_CU_ID {get;set;}
		public Decimal? NGUOI_TAO_ID {get;set; }
        public DateTime? NGAY_TAO {get;set;}
		public String SO_QUYET_DINH {get;set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_QUYET_DINH {get;set;}
		public String CAP_QUYET_DINH {get;set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_SUA_CHUA_FROM {get;set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_SUA_CHUA_TO {get;set; }
        [UIHint("InputAddon")]
        public Decimal SO_LUONG_SUA_CHUA {get;set; }
        [UIHint("InputAddon")]
        public Decimal GIA_TRI_SUA_CHUA {get;set;}
		public Decimal? KHACH_HANG_ID {get;set; }
		public decimal? DoiTacId { get; set; }
        public decimal? DON_VI_ID { get; set; }
        public List<SelectListItem> DDLDoiTac { get; set; }
        public String CHUNG_TU_SO {get;set; }
        [UIHint("DateNullable")]
        public DateTime? CHUNG_TU_NGAY {get;set;}
		public String GHI_CHU {get;set;}

        // more
        public String NgaySuaChuaText { get; set; }
        public Decimal MapId { get; set; }
        public Decimal DON_VI_BO_PHAN_ID { get; set; }
        public Decimal TongNguyenGia { get; set; }
        public DateTime? NgayXnBefore { get; set; }

        //thong tin cong cu
        public String DonViText { get; set; }
        public String MaCongCuText { get; set; }
        public String TenCongCuText { get; set; }
        public String NhomCongCuText { get; set; }
        public String DonGiaText { get; set; }
        public String SoLuongText { get; set; }
        public String BoPhanSuDungText { get; set; }
        public String stringGuid { get; set; }
        public String MaLoCongCuText { get; set; }
	}
    public partial class SuaChuaBaoDuongSearchModel :BaseSearchModel {
        public SuaChuaBaoDuongSearchModel()
        {
            DDLDonViBoPhan = new List<SelectListItem>();
            DDLCongCu = new List<SelectListItem>();
        }
        public string KeySearch { get; set; }
        public string KeySearchCongCu { get; set; }
        public Decimal BoPhanId { get; set; }
        public List<SelectListItem> DDLDonViBoPhan { get; set; }
        public Decimal CongCuId { get; set; }
        public List<SelectListItem> DDLCongCu { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayTu { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayDen { get; set; }

    }
   public partial class SuaChuaBaoDuongListModel : BasePagedListModel<SuaChuaBaoDuongModel>
    {
       
    }
}

