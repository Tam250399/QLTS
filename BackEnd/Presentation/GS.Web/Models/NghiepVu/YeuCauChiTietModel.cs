//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Core.Domain.DanhMuc;
using GS.Web.Framework.Models;
using GS.Web.Validators.NghiepVu;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.NghiepVu
{
    [Validator(typeof(YeuCauChiTietValidator))]
    public class YeuCauChiTietModel : BaseGSEntityModel
    {
        public YeuCauChiTietModel()
        {
            HinhThucMuaSamAvailable = new List<SelectListItem>();
            MucDichDuocGiaoAvailable = new List<SelectListItem>();
            BoPhanSuDungAvailable = new List<SelectListItem>();
            lstHienTrang = new List<ObjHienTrang>();
        }
        public Decimal YEU_CAU_ID { get; set; }
       
        public decimal? DonViSaiHienTrangId { get; set; }
        public Decimal? HINH_THUC_MUA_SAM_ID { get; set; }
        public Decimal? MUC_DICH_SU_DUNG_ID { get; set; }
        public String NHAN_HIEU { get; set; }
        public String SO_HIEU { get; set; }
        public Decimal? DIA_BAN_ID { get; set; }
        public String DATA_JSON { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NGUYEN_GIA { get; set; }
        [UIHint("InputAddon")]
        public Decimal? DAT_TONG_DIEN_TICH { get; set; }
        public Decimal? HTSD_QUAN_LY_NHA_NUOC { get; set; }
        public Decimal? HTSD_HDSN_KINH_DOANH_KHONG { get; set; }
        public Decimal? HTSD_HDSN_KINH_DOANH { get; set; }
        public Decimal? HTSD_CHO_THUE { get; set; }
        public Decimal? HTSD_LIEN_DOANH { get; set; }
        public Decimal? HTSD_SU_DUNG_HON_HOP { get; set; }
        public Decimal? HTSD_SU_DUNG_KHAC { get; set; }
        [UIHint("InputAddon")]
        public Decimal? HM_SO_NAM_CON_LAI { get; set; }
        [UIHint("InputAddon")]
        public Decimal? HM_TY_LE_HAO_MON { get; set; }
        [UIHint("InputAddon")]
        public Decimal? HM_LUY_KE { get; set; }
        [UIHint("InputAddon")]
        public Decimal? HM_GIA_TRI_CON_LAI { get; set; }
        [UIHint("DateNullable")]
        public DateTime? KH_NGAY_BAT_DAU { get; set; }
        [UIHint("InputAddon")]
        public Decimal KH_TY_LE { get; set; } 
        [UIHint("InputAddon")]
        public Decimal KH_THANG_THEO_QD { get; set; }
        [UIHint("InputAddon")]
        public Decimal? KH_THANG_CON_LAI { get; set; }
        [UIHint("InputAddon")]
        public Decimal? KH_GIA_TINH_KHAU_HAO { get; set; }
        [UIHint("InputAddon")]
        public Decimal? KH_TY_LE_NGUYEN_GIA_KHAU_HAO { get; set; }
        [UIHint("InputAddon")]
        public Decimal? KH_GIA_TRI_TRICH_THANG { get; set; }
        [UIHint("InputAddon")]
        public Decimal? KH_LUY_KE { get; set; }
        [UIHint("InputAddon")]
        public Decimal? KH_CON_LAI { get; set; }
        [UIHint("InputAddon")]
        public Decimal? HMKH_GIA_TRI_CON_LAI { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NHA_SO_TANG { get; set; }
        [UIHint("InputYear")]
        public Decimal? NHA_NAM_XAY_DUNG { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NHA_DIEN_TICH_XD { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NHA_TONG_DIEN_TICH_XD { get; set; }
        [UIHint("InputAddon")]
        public Decimal? VKT_DIEN_TICH { get; set; }
        [UIHint("InputAddon")]
        public Decimal? VKT_THE_TICH { get; set; }
        [UIHint("InputAddon")]
        public Decimal? VKT_CHIEU_DAI { get; set; }
        public String OTO_BIEN_KIEM_SOAT { get; set; }
        public Decimal? OTO_SO_CHO_NGOI { get; set; }
        public Decimal? OTO_SO_CAU_XE { get; set; }
        public decimal? OTO_CHUC_DANH_ID { get; set; }
        #region Thêm vào để xử lý yêu cầu chọn nhiều chức danh 1 lúc -- hungnt
        public string OTO_LIST_CHUC_DANH_ID { get; set; }
        #endregion
        public Decimal? OTO_NHAN_XE_ID { get; set; }
        public Decimal? OTO_TAI_TRONG { get; set; }
        public Decimal? OTO_CONG_XUAT { get; set; }
        public Decimal? OTO_XI_LANH { get; set; }
        public String OTO_SO_KHUNG { get; set; }
        public String OTO_SO_MAY { get; set; }
        public String THONG_SO_KY_THUAT { get; set; }
        public Decimal? CLN_SO_NAM { get; set; }
        public String HTSD_JSON { get; set; }
        [UIHint("InputAddon")]
        public Decimal? PHI_THU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? PHI_BU_DAP { get; set; }
        [UIHint("InputAddon")]
        public Decimal? PHI_NOP_NGAN_SACH { get; set; }
        [UIHint("InputAddon")]
        public Decimal? PHI_KHAC { get; set; }
        public Decimal? DON_VI_NHAN_DIEU_CHUYEN_ID { get; set; }
        public Decimal? HINH_THUC_XU_LY_ID { get; set; }
        public Boolean? IS_BAN_THANH_LY { get; set; }
        public Boolean DIEU_CHUYEN_KEM_THEO { get; set; }
        public DateTime? NGAY_BAN_THANH_LY { get; set; }
        //add more 
        public IList<SelectListItem> HinhThucMuaSamAvailable { get; set; }
        public IList<SelectListItem> MucDichDuocGiaoAvailable { get; set; }
        public IList<SelectListItem> BoPhanSuDungAvailable { get; set; }
        public Decimal? HM_GIA_TRI_TINH { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        [UIHint("InputAddon")]
        public Decimal? TS_GIA_TRI_HIEN_TAI { get; set; }
        public enumPHUONG_PHAP_TINH_KHAU_HAO enumPhuongPhapTinhKhauHao { get; set; }
        public SelectList PhuongPhapTinhKhauHaoAvailable { get; set; }

        public Decimal? PHUONG_PHAP_TINH_KHAU_HAO_ID { get; set; }
        public IList<ObjHienTrang> lstHienTrang { get; set; }
        public Decimal? HM_THOI_HAN_SU_DUNG { get; set; }
        public Decimal? KH_THOI_HAN_SU_DUNG { get; set; }
        public SelectList ddlPhuongAnXuLy { get; set; }
        public enumHINH_THUC_XU_LY_TSC enumPhuongAnXuLy { get; set; }
        //properties phuc vu bien dong
        [UIHint("InputAddon")]
        public decimal  NGUYEN_GIA_SAU_BD { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NHA_DIEN_TICH_XD_SAU_BD { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NHA_TONG_DIEN_TICH_XD_SAU_BD { get; set; }
        [UIHint("InputAddon")]
        public Decimal? DAT_TONG_DIEN_TICH_SAU_BD { get; set; }

        [UIHint("InputAddon")]
        public Decimal? NGUYEN_GIA_CU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? HM_GIA_TRI_CON_LAI_CU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NHA_DIEN_TICH_XD_CU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NHA_TONG_DIEN_TICH_XD_CU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? DAT_TONG_DIEN_TICH_CU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? VKT_CHIEU_DAI_CU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? VKT_DIEN_TICH_CU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? VKT_THE_TICH_CU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? VKT_CHIEU_DAI_SAU_BD { get; set; }
        [UIHint("InputAddon")]
        public Decimal? VKT_DIEN_TICH_SAU_BD { get; set; }
        [UIHint("InputAddon")]
        public Decimal? VKT_THE_TICH_SAU_BD { get; set; }
		//
		//Ho so, giay to
		public String HS_QUYET_DINH_BAN_GIAO { get; set; }
		[UIHint("DateNullable")]
		public DateTime? HS_QUYET_DINH_BAN_GIAO_NGAY { get; set; }
		public String HS_BIEN_BAN_NGHIEM_THU { get; set; }
		[UIHint("DateNullable")]
		public DateTime? HS_BIEN_BAN_NGHIEM_THU_NGAY { get; set; }
		public String HS_PHAP_LY_KHAC { get; set; }
		[UIHint("DateNullable")]
		public DateTime? HS_PHAP_LY_KHAC_NGAY { get; set; }
        public String HS_CNQSD_SO { get; set; }
        [UIHint("DateNullable")]
        public DateTime? HS_CNQSD_NGAY { get; set; }
        public String HS_QUYET_DINH_GIAO_SO { get; set; }
        [UIHint("DateNullable")]
        public DateTime? HS_QUYET_DINH_GIAO_NGAY { get; set; }
        public String HS_CHUYEN_NHUONG_SD_SO { get; set; }
        [UIHint("DateNullable")]
        public DateTime? HS_CHUYEN_NHUONG_SD_NGAY { get; set; }
        public String HS_QUYET_DINH_CHO_THUE_SO { get; set; }
        [UIHint("DateNullable")]
        public DateTime? HS_QUYET_DINH_CHO_THUE_NGAY { get; set; }
        public String HS_HOP_DONG_CHO_THUE_SO { get; set; }
        [UIHint("DateNullable")]
        public DateTime? HS_HOP_DONG_CHO_THUE_NGAY { get; set; }
        public String HS_KHAC { get; set; }

        public String DIA_CHI { get; set; }
		public string DonViNhanDieuChuyenTen { get; set; }
        public Decimal? TAI_SAN_TRUOC_DIEU_CHUYEN_ID { get; set; }
        [UIHint("InputAddon")]
        public Decimal? DAT_GIA_TRI_QUYEN_SD_DAT { get; set; }

        //Trường lưu thông tin địa bàn để tạo tên tài sản đất (thay đổi thông tin)
		public Decimal? DatTinhId { get; set; }
		public Decimal? DatHuyenId { get; set; }
		public Decimal? DatXaId { get; set; }

        //Trường lưu thông tin địa bàn tài sản nhà không có đất (thay đổi thông tin)
        public Decimal? NHA_TinhId { get; set; }
        public Decimal? NHA_HuyenId { get; set; }
        public Decimal? NHA_XaId { get; set; }
        public string NHA_DIA_CHI { get; set; }

        #region Thông tin cần thiết để thay đổi hiện trạng
        public decimal BienDongSaiHienTrangId { get; set; }
        public decimal LoaiHinhTaiSanSaiHienTrangId { get; set; }
        public decimal TaiSanSaiHienTrangId { get; set; }
        public bool? IsCauHinhHienTrang { get; set; }
        public int TongHienTrangCheckbox { get; set; }
        #endregion

    }
    public partial class YeuCauChiTietSearchModel : BaseSearchModel
    {
        public YeuCauChiTietSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class YeuCauChiTietListModel : BasePagedListModel<YeuCauChiTietModel>
    {

    }
    public partial class ObjHienTrang
    {
        public decimal? HienTrangId { get; set; }
        public String GiaTriText { get; set; }
        [UIHint("InputAddon")]
        public Decimal? GiaTriNumber { get; set; }
        public Boolean? GiaTriCheckbox { get; set; }
        public string TenHienTrang { get; set; }
        public Decimal? KieuDuLieuId { get; set; }
		public Decimal? NhomHienTrangId { get; set; }
        public decimal? DonViId { get; set; }
        public bool IsOpenAll { get; set; }
    }
    public partial class HienTrangList
    {
        public decimal? TaiSanId { get; set; }
        public IList<ObjHienTrang> lstObjHienTrang { get; set; }
    }
}

