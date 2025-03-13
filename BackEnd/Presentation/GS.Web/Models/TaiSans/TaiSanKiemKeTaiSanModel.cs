//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
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
    [Validator(typeof(TaiSanKiemKeTaiSanValidator))]
	public class TaiSanKiemKeTaiSanModel :BaseGSEntityModel
	{
        public TaiSanKiemKeTaiSanModel()
        {
            DDLNhomTaiSan = new List<SelectListItem>();
        }
		public Decimal KIEM_KE_ID {get;set;}
		public Decimal? TAI_SAN_ID {get;set; }
        [UIHint("InputAddon")]
        public Decimal? SO_LUONG_KIEM_KE {get;set; }
        [UIHint("InputAddon")]
        public Decimal? SO_LUONG_SO_SACH {get;set;}
		public bool IS_PHAT_HIEN_THUA {get;set; }
        [UIHint("InputAddon")]
        public Decimal? NGUYEN_GIA_SO_SACH { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NGUYEN_GIA_KIEM_KE { get; set; }
        [UIHint("InputAddon")]
        public Decimal? GIA_TRI_CON_LAI_SO_SACH { get; set; }
        [UIHint("InputAddon")]
        public Decimal? GIA_TRI_CON_LAI_KIEM_KE { get; set; }
        public Decimal? TINH_TRANG_ID { get; set; }
        public Decimal? DE_NGHI_XU_LY_ID { get; set; }
        //thêm trường
        public String KIEN_NGHI_GQTST { get; set; }
        public String GHI_CHU { get; set; }
        public String TEN_TAI_SAN_THUA { get; set; }
        public Decimal NHOM_TAI_SAN_ID { get; set; }

     

        // more
        public String MaTaiSan { get; set; }
        public String TenTaiSan { get; set; }
        public String SoLuongText { get; set; }
        public String NguyenGiaText { get; set; }
        public String GiaTriConLaiText { get; set; }
        public String TinhTrangText { get; set; }
        public SelectList DDLTinhTrangEnum { get; set; }
        public SelectList DDLDeNghiXuLyEnum { get; set; }
        public enumTinhTrang enumTinhTrang { get; set; }
        public enumDeNghiXuLy enumDeNghiXuLy { get; set; }
        public List<SelectListItem> DDLNhomTaiSan { get; set; }
        public Decimal TaiSanKeKhaiId { get; set; }
        public Decimal? SoLuongKiemKe { get; set; }
    }
    public partial class TaiSanKiemKeTaiSanSearchModel :BaseSearchModel {
        public TaiSanKiemKeTaiSanSearchModel()
        {
        }
        public string KeySearch {get;set;}
        public string KeySearchTSThua { get;set;}
        public Decimal KiemKeId { get; set; }
        public bool isTaiSanThua { get; set; }
    }
   public partial class TaiSanKiemKeTaiSanListModel : BasePagedListModel<TaiSanKiemKeTaiSanModel>
    {
       
    }
}

