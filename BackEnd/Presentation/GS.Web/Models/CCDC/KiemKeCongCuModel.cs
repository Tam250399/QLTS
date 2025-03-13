//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Core.Domain.CCDC;
using GS.Web.Framework.Models;
using GS.Web.Validators.CCDC;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.CCDC
{
    [Validator(typeof(KiemKeCongCuValidator))]
	public class KiemKeCongCuModel :BaseGSEntityModel
	{
        public KiemKeCongCuModel(){
            ListCongCuModel = new List<CongCuKiemKeModel>();
        }
		public Decimal KIEM_KE_ID {get;set;}
		public Decimal? CONG_CU_ID {get;set;}
		public bool IS_PHAT_HIEN_THUA {get;set;}
		public Decimal? SO_LUONG_THUA {get;set; }
        public Decimal? TINH_TRANG_ID { get; set; }
        public Decimal? DE_NGHI_XU_LY { get; set; }
        public String GHI_CHU { get; set; }
        public String TEN_CONG_CU_THUA { get; set; }
        public Decimal? XUAT_NHAP_ID { get; set; }
        public Decimal? NHOM_CONG_CU_ID { get; set; }
        [UIHint("InputAddon")]
        public Decimal? SO_LUONG_KIEM_KE { get; set; }
        [UIHint("InputAddon")]
        public Decimal? SO_LUONG_SO_SACH { get; set; }
        [UIHint("InputAddon")]
        public Decimal? DON_GIA_THUA { get; set; }
        public Decimal? DON_GIA { get; set; }

        // more
        public Decimal MapId { get; set; }
        public List<CongCuKiemKeModel> ListCongCuModel { get; set; }
        public String MaCongCu { get; set; }
        public String TenCongCu { get; set; }
        public String DonGiaText { get; set; }
        public String NhomCongCuText { get; set; }
        public String TrangThaiText { get; set; }
        public Guid CongCuGuid { get; set; }
        public String SoLuongText { get; set; }
        public Decimal DON_VI_BO_PHAN_ID { get; set; }
        public String BoPhanSuDungText { get; set; }
        public Decimal SoLuongKiemKe { get; set; }
        public Decimal? SoLuongSoSach { get; set; }
        public SelectList DDLTinhTrangEnum { get; set; }
        public SelectList DDLDeNghiXuLyEnum { get; set; }
        public List<SelectListItem> DDLNhomCongCu { get; set; }
        public enumTinhTrang enumTinhTrang { get; set; }
        public enumDeNghiXuLy enumDeNghiXuLy { get; set; }
        public Decimal KiemKeCongCuId { get; set; }
    }
    public class CongCuKiemKeModel
    {
        public Decimal? ID { get; set; }
        public Decimal KIEM_KE_ID { get; set; }
        public String stringGuid { get; set; }
        public String MaCongCu { get; set; }
        public String TenCongCu { get; set; }
        public Decimal? SoLuong { get; set; }
        public String SoLuongText { get; set; }
        public Decimal? DonGia { get; set; }
        public String DonGiaText { get; set; }
        public String TinhTrang { get; set; }
        public Guid CongCuGuid { get; set; }
        public Decimal MapId { get; set; }
        public Decimal? SoLuongSoSach { get; set; }
    }
    public partial class KiemKeCongCuSearchModel :BaseSearchModel {
        public KiemKeCongCuSearchModel()
        {
        }
        public string KeySearch {get;set;}
        public Decimal BoPhanKiemKeId { get; set; }
        public Decimal KiemKeId { get; set; }
        public String ListCongCuThuaDaChon { get; set; }
        public bool isPhatHienThua { get; set; }
    }
    public partial class KiemKeCongCuListModel : BasePagedListModel<KiemKeCongCuModel>
    {
       
    }
}

