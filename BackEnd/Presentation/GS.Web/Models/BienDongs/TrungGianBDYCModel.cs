using FluentValidation.Attributes;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using GS.Web.Framework.Models;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using GS.Web.Validators.BienDongs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.BienDongs
{
	public class ThongTinChungTaiSanModel : BaseGSEntityModel
	{
		public String TAI_SAN_MA { get; set; }
		public String TAI_SAN_TEN { get; set; }
		public string TenDonViBoPhan { get; set; }
		public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
		public string TenDonVi { get; set; }
		public string TenLoaiTaiSan { get; set; }
		[UIHint("DateNullable")]
		public DateTime? NGAY_SU_DUNG { get; set; }
		public string TenLoaiTaiSanParent { get; set; }
	}
	[Validator(typeof(TrungGianBDYCValidator))]
	public class TrungGianBDYCModel : BaseGSEntityModel
	{
		public TrungGianBDYCModel()
		{
			LyDoTangAvailable = new List<SelectListItem>();
		}
		public enumBDorYC EnumBDorYC
		{
			get => (enumBDorYC)BDorYC;
		}
		public int BDorYC { get; set; }
		public Decimal TAI_SAN_ID { get; set; }
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
		public DateTime? NGAY_DUYET { get; set; }

		[UIHint("DateNullable")]
		public DateTime? NGAY_SU_DUNG { get; set; }
		public Decimal? LOAI_BIEN_DONG_ID { get; set; }
		public Decimal? LY_DO_BIEN_DONG_ID { get; set; }
		public Decimal? TRANG_THAI_ID { get; set; }
		public Decimal? DON_VI_ID { get; set; }
		public Decimal NGUOI_TAO_ID { get; set; }
		public Decimal? NGUOI_DUYET_ID { get; set; }
		public DateTime NGAY_TAO { get; set; }
		public String GHI_CHU { get; set; }
		public Guid GUID { get; set; }
		public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
		[UIHint("DateNullable")]
		public DateTime? QUYET_DINH_NGAY { get; set; }
		public Decimal? HINH_THUC_XU_LY_ID { get; set; }
		public enumHINH_THUC_XU_LY_TSC enumPhuongAnXuLy { get; set; }
		public String QUYET_DINH_SO { get; set; }
		public String NGUON_VON_JSON { get; set; }
		public string LY_DO_KHONG_DUYET { get; set; }
		public bool? IS_BIENDONG_CUOI { get; set; }
		public bool? HasThaoTac { get; set; }
		//add more
		public decimal? TAI_SAN_TRANG_THAI_ID { get; set; }

		public IList<SelectListItem> LyDoTangAvailable { get; set; }
		#region trường show thông tin tài sản
		public string TenDonViBoPhan { get; set; }
		public string TenDonVi { get; set; }
		public string TenNhanXe { get; set; }
		public string MaDonVi { get; set; }
		public string TenLyDoBienDong { get; set; }
		public decimal? GIA_MUA_TIEP_NHAN { get; set; }
		public bool? IS_MIEN_THUE { get; set; }
		public decimal? MIEN_THUE_SO_TIEN { get; set; }
		public string TenLoaiLyDoBienDong { get; set; }
		public string MaLoaiLyDoBienDong { get; set; }
		public string NguyenGiaVNStringNumber { get; set; }
		public string TenNguoiTao { get; set; }
		public string TenTrangThai { get; set; }
		public string TenLoaiTaiSan { get; set; }
		public string TenLoaiTaiSanParent { get; set; }
		public string TenMucDichSuDung { get; set; }
		public string TenTinh { get; set; }
		public string TenHuyen { get; set; }
		public string TenXa { get; set; }
		public string TenQuocGia { get; set; }
		public string TenHinhThucMuaSam { get; set; }
		public string TenNuocSanXuat { get; set; }
		public decimal? NamSanXuat { get; set; }
		public string MaTaiSan { get; set; }
		public string TenTaiSan { get; set; }
		public string LyDoHuy { get; set; }
        public string TenDonViMuaSam { get; set; }
        public string TenPhuongThucMuaSam { get; set; }
        public decimal? PHUONG_THUC_MUA_SAM_ID { get; set; }
        public string MaLyDoBienDong { get; set; }
        #endregion
        //Phuc vu bien dong

        public IList<NguonVonModel> lstNguonVonModel { get; set; }
		public Guid TaiSanGuid { get; set; }
		public TrungGianBDYCChiTietModel TrungGianBDYCChiTietModel { get; set; }

		public TaiSanDatModel taisanDatModel { get; set; }
		public TaiSanNhaModel taisanNhaModel { get; set; }
		public TaiSanOtoModel taiSanOtoModel { get; set; }
		public TaiSanMayMocModel taiSanMayMocModel { get; set; }
		public TaiSanVktModel taisanVktModel { get; set; }
		public TaiSanVoHinhModel taiSanVoHinhModel { get; set; }
		public TaiSanClnModel taiSanClnModel { get; set; }
		#region Biến động giảm tài sản
		public decimal? DonViNhanDieuChuyenId { get; set; }
		public string DonViNhanDieuChuyenTen { get; set; }
		public string TenPhuongAnXuLy { get; set; }
		#endregion
		#region biến động thay đổi thông tin tài sản
		public ThongTinChungTaiSanModel thongTinChungTaiSanModel { get; set; }

		//nhà
		//đất

		//oto

		//cây lâu năm
		//vật kiến trúc
		//máy móc thiết bị

		#endregion biến động thay đổi thông tin tài sản

		public int SoYCTruocChuaDuyet { get; set; }
		public int SoYCTruocChuaHuy { get; set; }
		public int? pageIndex { get; set; }
		public SelectList ddlPhuongAnXuLy { get; set; }
		public Decimal? TAI_SAN_TRUOC_DIEU_CHUYEN_ID { get; set; }
		public String TenDonViDieuChuyen { get; set; }
		public String MaTaiSanDieuChuyen { get; set; }
		public Decimal? DU_AN_ID { get; set; }
		public String TenDuAn { get; set; }

		//Cho phép duyệt thay đổi thông tin tài sản dkts
		public bool IsDisableTSDKTS { get; set; }
	}

	public partial class TrungGianBDYCSearchModel : BaseSearchModel
	{
		public TrungGianBDYCSearchModel()
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
		public bool isDuyet { get; set; }
		public bool IsTaiSanDuAn { get; set; }
	}

	public partial class TrungGianBDYCListModel : BasePagedListModel<TrungGianBDYCModel>
	{
	}
	public partial class ResultAction
	{
		public ResultAction(bool Result, string Message)
		{
			this.Result = Result;
			this.Message = Message;
		}
		public bool Result { get; set; }
		public string Message { get; set; }
	}
}