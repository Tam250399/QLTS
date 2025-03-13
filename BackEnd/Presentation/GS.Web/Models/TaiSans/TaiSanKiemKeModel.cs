//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
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
    [Validator(typeof(TaiSanKiemKeValidator))]
	public class TaiSanKiemKeModel :BaseGSEntityModel
	{
        public TaiSanKiemKeModel()
        {
            DDLDonViBoPhan = new List<SelectListItem>();
            ListKiemKeHoiDongModel = new List<TaiSanKiemKeHoiDongModel>();
            ListHoiDongKiemKe = new List<HoiDongKiemKeModel>();
            NGAY_KIEM_KE = DateTime.Now;
        }
		public Decimal DON_VI_ID {get;set;}
		public String SO_BIEN_BAN {get;set; }
        [UIHint("DateNullable")]
        public DateTime NGAY_KIEM_KE {get;set;}
		public Decimal? DON_VI_BO_PHAN_ID {get;set;}
		public String NGUOI_LAP_BIEU {get;set;}
		public String DAI_DIEN_BPSD {get;set;}
		public String KE_TOAN {get;set;}
		public String TRUONG_BAN {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public Decimal? NGUOI_TAO_ID {get;set;}
        // more
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public List<SelectListItem> DDLDonViBoPhan { get; set; }
        public String DonViBoPhanText { get; set; }
        public String NgayKiemKeText { get; set; }
        public List<TaiSanKiemKeHoiDongModel> ListKiemKeHoiDongModel { get; set; }
        public List<HoiDongKiemKeModel> ListHoiDongKiemKe { get; set; }
    }
    public partial class HoiDongKiemKeModel
    {
        public Decimal ID { get; set; }
        public String HO_TEN { get; set; }
        public String CHUC_VU { get; set; }
        public String DAI_DIEN { get; set; }
        public Decimal VI_TRI_ID { get; set; }
    }
    public partial class TaiSanKiemKeSearchModel :BaseSearchModel {
        public TaiSanKiemKeSearchModel()
        {
        }
        public string KeySearch {get;set;}
        public decimal DonViId { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayKiemKeTu { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayKiemKeDen { get; set; }
    }
   public partial class TaiSanKiemKeListModel : BasePagedListModel<TaiSanKiemKeModel>
    {
       
    }
}

