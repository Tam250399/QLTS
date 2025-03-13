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
    [Validator(typeof(KiemKeValidator))]
	public class KiemKeModel :BaseGSEntityModel
	{
        public KiemKeModel()
        {
            DDLDonViBoPhan = new List<SelectListItem>();
            ListHoiDongKiemKe = new List<HoiDongKiemKeModel>();
            ListKiemKeHoiDongModel = new List<KiemKeHoiDongModel>();
            ListKiemKeCongCuThua = new List<KiemKeCongCuModel>();
        }
		public String SO_KIEM_KE {get;set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_KIEM_KE {get;set;}
		public Decimal? DON_VI_BO_PHAN_ID {get;set;}
		public String NGUOI_LAP_BIEU {get;set;}
		public String DAI_DIEN_BO_PHAN {get;set;}
		public String KE_TOAN {get;set;}
		public String TRUONG_BAN {get;set;}
		public Decimal? NGUOI_TAO_ID {get;set;}
		public DateTime? NGAY_TAO {get;set; }
        public Decimal? DON_VI_ID { get; set; }

        // more
        public String MaDonVi { get; set; }
        public String TenDonVi { get; set; }
        public IList<SelectListItem> DDLDonViBoPhan { get; set; }
        public IList<String> ListGuid { get; set; }
        public String DonViBoPhanText { get; set; }
        public String NgayKiemKeText { get; set; }
        public List<HoiDongKiemKeModel> ListHoiDongKiemKe { get; set; }
        public List<KiemKeHoiDongModel> ListKiemKeHoiDongModel { get; set; }
        public List<KiemKeCongCuModel> ListKiemKeCongCuThua { get; set; }
        public int CurrentPage { get; set; }
    }
    public partial class HoiDongKiemKeModel
    {
        public Decimal ID { get; set; }
        public String HO_TEN { get; set; }
        public String CHUC_VU { get; set; }
        public String DAI_DIEN { get; set; }
        public Decimal VI_TRI_ID { get; set; }
    }
    public partial class KiemKeSearchModel :BaseSearchModel {
        public KiemKeSearchModel()
        {
            ddlBoPhan = new List<SelectListItem>();
        }
        public string KeySearch {get;set;}
        public Decimal BoPhanId { get; set; }
        public List<SelectListItem> ddlBoPhan { get; set; }
        [UIHint("DateNullable")]
        public DateTime? TuNgay { get; set; }
        [UIHint("DateNullable")]
        public DateTime? DenNgay { get; set; }
    }
   public partial class KiemKeListModel : BasePagedListModel<KiemKeModel>
    {
       
    }
}

