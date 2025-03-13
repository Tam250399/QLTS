//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
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
    [Validator(typeof(XuatNhapValidator))]
	public class XuatNhapModel :BaseGSEntityModel
	{
        public XuatNhapModel()
        {
            ListCongCuModel = new List<CongCuModel>();
            DDLDonViBoPhan = new List<SelectListItem>();
            this.GUID = Guid.NewGuid();
            ListMap = new List<NhapXuatCongCuModel>();
            IsEdit = true;
        }
		public Decimal? TRANG_THAI_ID {get;set;}
		public Decimal? FROM_XUAT_NHAP_ID {get;set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_XUAT_NHAP {get;set;}
		public Decimal? DON_VI_ID {get;set;}
		public Decimal? NGUOI_DUNG_ID {get;set;}
		public Decimal? DON_VI_BO_PHAN_ID {get;set;}
		public String MA {get;set;}
		public String MA_LIEN_QUAN { get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public DateTime? NGAY_CAP_NHAP {get;set;}
		public String GHI_CHU {get;set;}
		public Boolean? IS_XUAT {get;set;}
		public Guid GUID {get;set; }
        public String TEN { get; set; }
        public String QUYET_DINH_SO { get; set; }
        [UIHint("DateNullable")]
        public DateTime? QUYET_DINH_NGAY { get; set; }
        public Boolean? IS_PHAN_BO_FIRST { get; set; }

        //more
        public List<CongCuModel> ListCongCuModel { get; set; }
        [UIHint("InputAddon")]
        public Decimal SoLuongPhanBo { get; set; }
        public List<SelectListItem> DDLDonViBoPhan { get; set; }
        public SelectList DDLTrangThai { get; set; }
        public enumTrangThaiCongCu TrangThaiCongCuEnum { get; set; }
        public String StringMapId { get; set; }
        public String StringGuid { get; set; }
        public Decimal TongSoLuong { get; set; }
        public List<NhapXuatCongCuModel> ListMap { get; set; }
        public Decimal MapId { get; set; }
        public Decimal BoPhanId { get; set; }
        public DateTime DateBefore { get; set; }
        public DateTime NgayNhapKhoMin { get; set; }

        // view list
        public String TenCongCuText { get; set; }
        public String MaLoCongCuText { get; set; }
        public String MaCongCuText { get; set; }
        public String NhomCongCuText { get; set; }
        public String BoPhanText { get; set; }
        public String DonGiaText { get; set; }
        public String SoLuongText { get; set; }
        public String SoLuongDaDungText { get; set; }
        public Guid? CongCuGuid { get; set; }
        public String NgayXuatNhapText { get; set; }
        public int SoCongCuText { get; set; }
        public String BoPhanSuDungText { get; set; }
        public String BoPhanSuDungDenText { get; set; }
        public String DonViText { get; set; }
        public String ChungTuSoText { get; set; }
        public String ChungTuNgayText { get; set; }
        public bool IsEdit { get; set; }
    }
    public partial class XuatNhapSearchModel :BaseSearchModel
    {
        public XuatNhapSearchModel()
        {
            IsPhanBo = false;
            DDLDonViBoPhan = new List<SelectListItem>();
            DDLNhomCongCu = new List<SelectListItem>();
            DenNgay = DateTime.Now;
        }
        public string KeySearch {get;set; }
        public bool IsPhanBo { get; set; }
        public string KeySearchCongCu { get; set; }
        public Decimal DonViBoPhanId { get; set; }
        public List<SelectListItem> DDLDonViBoPhan { get; set; }
        public List<SelectListItem> DDLNhomCongCu { get; set; }
        [UIHint("DateNullable")]
        public DateTime? TuNgay { get; set; }
        [UIHint("DateNullable")]
        public DateTime? DenNgay { get; set; }
        public Decimal LoaiCongCu { get; set; }

    }
    public partial class LuanChuyenSearchModel : BaseSearchModel
    {
        public LuanChuyenSearchModel()
        {
            IsPhanBo = false;
            DDLDonViBoPhan = new List<SelectListItem>();
            DDLNhomCongCu = new List<SelectListItem>();
            DenNgay = DateTime.Now;
        }
        public string KeySearch { get; set; }
        public bool IsPhanBo { get; set; }
        public string KeySearchCongCu { get; set; }
        public Decimal DonViBoPhanId { get; set; }
        public List<SelectListItem> DDLDonViBoPhan { get; set; }
        public List<SelectListItem> DDLNhomCongCu { get; set; }
        [UIHint("DateNullable")]
        public DateTime? TuNgay { get; set; }
        [UIHint("DateNullable")]
        public DateTime? DenNgay { get; set; }
        public Decimal LoaiCongCu { get; set; }

    }
    public partial class GhiTangModel
    {
        public GhiTangModel()
        {
            guids = new List<Guid>();
        }
        public List<Guid> guids { get; set; }
    }
    public partial class XuatNhapListModel : BasePagedListModel<XuatNhapModel>
    {
       
    }

}

