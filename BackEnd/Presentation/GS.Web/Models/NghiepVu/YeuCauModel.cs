//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Web.Framework.Models;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.TaiSans;
using GS.Web.Validators.NghiepVu;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.NghiepVu
{
    [Validator(typeof(YeuCauValidator))]
    public class YeuCauModel : BaseGSEntityModel
    {
        public YeuCauModel()
        {
            LyDoTangAvailable = new List<SelectListItem>();
            lstNguonVonBD = new List<NguonVonBDModel>();
        }
        public Decimal TAI_SAN_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
        public String TAI_SAN_MA { get; set; }
        public String TAI_SAN_TEN { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        public String CHUNG_TU_SO { get; set; }
        [UIHint("DateNullable")]
        public DateTime? CHUNG_TU_NGAY { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_BIEN_DONG { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_SU_DUNG { get; set; }
        public Decimal? LOAI_BIEN_DONG_ID { get; set; }
        public Decimal? LY_DO_BIEN_DONG_ID { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? NGUOI_TAO_ID { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public String GHI_CHU { get; set; }
        public Guid GUID { get; set; }
        [UIHint("DateNullable")]
        public DateTime? QUYET_DINH_NGAY { get; set; }
        public String QUYET_DINH_SO { get; set; }
        public String NGUON_VON_JSON { get; set; }
        public string LY_DO_KHONG_DUYET { get; set; }
        [UIHint("InputAddon")]
        public Decimal? HOA_HONG_NOP_NSNN { get; set; }
        [UIHint("InputAddon")]
        public Decimal? HOA_HONG_DE_LAI_DON_VI { get; set; }
        [UIHint("InputAddon")]
        public Decimal? TONG_HOA_HONG_CHIET_KHAU { get; set; }
        //add more
        public decimal? TAI_SAN_TRANG_THAI_ID { get; set; }
        public IList<SelectListItem> LyDoTangAvailable { get; set; }
        public string TenDonViBoPhan { get; set; }
        public string TenDonVi { get; set; }
        public string TenLyDoBienDong { get; set; }
        public string TenLoaiLyDoBienDong { get; set; }
        public string NguyenGiaVNStringNumber { get; set; }
        public string TenNguoiTao { get; set; }
        public string TenTrangThai { get; set; }
        public string TenLoaiTaiSan { get; set; }
		public bool? IS_BIENDONG_CUOI { get; set; }
		//Phuc vu bien dong
		public IList<SelectListItem> NguonVonAvailable { get; set; }
        public IList<int> SelectedNguonVonIds { get; set; }
        public IList<NguonVonModel> lstNguonVonModel { get; set; }
        public IList<NguonVonBDModel> lstNguonVonBD { get; set; }
        public Guid TaiSanGuid { get; set; }
        public YeuCauChiTietModel YCCTModel { get; set; }

        public string strTaiSanIds { get; set; }
        public IList<SelectListItem> DonVisAvaliable{ get; set; }
        public TaiSanDatModel taisanDatModel { get; set; }
        public TaiSanNhaModel taisanNhaModel { get; set; }
        public TaiSanVktModel taisanVktModel { get; set; }
        public TaiSanClnModel taisanClnModel { get; set; }
        public TaiSanOtoModel taiSanOtoModel { get; set; }
        public decimal? DonViNhanDieuChuyenId { get; set; }
        public string DonViNhanDieuChuyenTen { get; set; }
        public decimal? HINH_THUC_XU_LY_ID { get; set; }
        public SelectList ddlPhuongAnXuLy { get; set; }
        public enumHINH_THUC_XU_LY_TSC enumPhuongAnXuLy { get; set; }
        #region biến động thay đổi thông tin tài sản
        //biến động thay đổi thông tin tài sản
        public IList<SelectListItem> LoaiTaiSanAvailable { get; set; }
        public IList<SelectListItem> BoPhanSuDungAvailable { get; set; }
        //nhà
        //đất
        public decimal? XaId { get; set; }
        //oto
        public bool IsHaveHS { get; set; }
        public string BIEN_KIEM_SOAT { get; set; }
        public decimal? CONG_XUAT { get; set; }
        public decimal? NHAN_XE_ID { get; set; }
        public decimal? SO_CHO_NGOI { get; set; }
        public decimal? SO_CAU_XE { get; set; }
        public decimal? TAI_TRONG { get; set; }
        //cây lâu năm
        //vật kiến trúc
        //máy móc thiết bị

        #endregion

        public int SoYCTruocChuaDuyet { get; set; }
        public int SoYCTruocChuaHuy{ get; set; }
        public int? pageIndex { get; set; }
        //more nhà
        //Thông tin tài sản đất (trụ sở) mà tài sản nhà được gắn
        public string NhaMaTruSo { get; set; }
        public string NhaTenTruSo { get; set; }
        public decimal? DU_AN_ID { get; set; }
        public string DU_AN_TEN { get; set; }
        public bool? IsTaiSanDuAn { get; set; }
        public bool  IsCapNhatThongTinDiaBan { get; set; }
        public bool  IsCapNhatSoChoNgoiOto { get; set; }
        public bool IsCapNhatNguyenGia { get; set; }
        // Cập nhật hiện trạng của tài sản nếu hiện trạng bị sai,
        // nếu không sai thì không hiển thị
        public bool  IsShowHienTrang { get; set; }
    }
    public partial class YeuCauSearchModel : BaseSearchModel
    {
        public YeuCauSearchModel()
        {
            BoPhanSuDungAvailable = new List<SelectListItem>();
        }
        public string KeySearch { get; set; }
        public Decimal? LOAI_LY_DO_BD_ID { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? LY_DO_BIEN_DONG_ID { get; set; }
        public string CHUNG_TU_SO { get; set; }
        public string QUYET_DINH_SO { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public Decimal NGUOI_TAO_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_BIEN_DONG { get; set; }
        [UIHint("DateNullable")]
        public DateTime? FromDate { get; set; }
        [UIHint("DateNullable")]
        public DateTime? ToDate { get; set; }

        public SelectList Trangthailist { get; set; }
        public enumTRANG_THAI_YEU_CAU enumtrangthaiyeucau { get; set; }
        public enumLOAI_HINH_TAI_SAN enumLoaiHinhTaiSan { get; set; }
        public SelectList LoaiHinhTaiSanAvailable { get; set; }
        public IList<SelectListItem> BoPhanSuDungAvailable { get; set; }
        public decimal? taisanId { get; set; }
        public bool isIgnoreTraLai { get; set; }
        public bool? isTraCuu { get; set; }
        public int? pageIndex { get; set; }
		public bool? IsDanhSachTaiSanDuAn { get; set; }
	}
    public partial class YeuCauListModel : BasePagedListModel<YeuCauModel>
    {

    }

    public partial class InsertEditDeniedModel
    {
        public InsertEditDeniedModel()
        {

        }
        public string RedirectUrl { get; set; }
        public string MA_TAI_SAN { get; set; }
        public string TEN_TAI_SAN { get; set; }
        public string TEN_BIEN_DONG { get; set; }
        public string TEN_BIEN_DONG_CAN_SUA { get; set; }
    }
}

