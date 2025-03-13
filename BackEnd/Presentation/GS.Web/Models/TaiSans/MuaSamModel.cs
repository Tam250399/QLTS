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
    [Validator(typeof(MuaSamValidator))]
	public class MuaSamModel :BaseGSEntityModel
	{
        public MuaSamModel(){

            ListMuaSamChiTietModel = new List<MuaSamChiTietModel>();
        }
		public Guid GUID {get;set;}
		public Decimal? DVLQTS_ID {get;set;}
		public Decimal DVSDTS_ID {get;set;}
        [UIHint("DateNullable")]
		public DateTime? NGAY_DANG_KY {get;set;}
		public Decimal? NAM {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public Decimal? NGUOI_TAO_ID {get;set;}
		public Decimal TRANG_THAI_ID { get;set;}
        public IList<MuaSamChiTietModel> ListMuaSamChiTietModel { get; set; }
        public Decimal? NGUOI_DUYET_ID { get; set; }
        public DateTime? NGAY_DUYET { get; set; }
		public String GHI_CHU { get; set; }
		//more
		public String TenNguoiTao { get; set; }
        public String TenNguoiDuyet { get; set; }
        public String DVSDTS_Ten { get; set; }
        public String DVSDTS_Ma { get; set; }
        public String DVQLTS_Ten { get; set; }
        public String DVQLTS_Ma { get; set; }
        // ngày nhỏ nhất trong mua sắm chi tiết
        public DateTime NgayMuaSamChiTietMin { get; set; }
    }
    public partial class MuaSamSearchModel :BaseSearchModel {
        public MuaSamSearchModel()
        {
            ListLoaiTaiSan = new List<SelectListItem>();
        }
        public string KeySearch {get;set; }
        [UIHint("DateNullable")]
        public DateTime? Tu_ngay { get;set; }
        [UIHint("DateNullable")]
        public DateTime? Den_ngay { get;set;}
        public decimal? LOAI_TAI_SAN_ID { get; set; }
        public decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public decimal? TRANG_THAI_ID { get; set; }
        public decimal? DonviQL_ID { get; set; }
        public string TenDonViQL { get; set; }
        public decimal? DonviSD_ID { get; set; }
        public string TenDonViSD { get; set; }
        public string MaDonViSD { get; set; }
        public IList<SelectListItem> ListLoaiTaiSan { get; set; }
    }
   public partial class MuaSamListModel : BasePagedListModel<MuaSamModel>
    {
       
    }
}

