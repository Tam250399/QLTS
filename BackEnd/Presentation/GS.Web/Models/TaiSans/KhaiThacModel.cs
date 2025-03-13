//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/7/2020
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
	[Validator(typeof(KhaiThacValidator))]
	public class KhaiThacModel : BaseGSEntityModel
	{
		public KhaiThacModel()
		{

			ListKhaiThacTaiSanModel = new List<KhaiThacTaiSanModel>();
			ListTaiSanModel = new List<TaiSanModel>();
		}
		public Decimal? DON_VI_ID { get; set; }
		public String TEN_DON_VI { get; set; }
		public Decimal? LOAI_HINH_KHAI_THAC_ID { get; set; }
		[UIHint("DateNullable")]
		public DateTime? NGAY_KHAI_THAC { get; set; }
		public String QUYET_DINH_SO { get; set; }
		[UIHint("DateNullable")]
		public DateTime? QUYET_DINH_NGAY { get; set; }
		public String NGUOI_DUYET { get; set; }
		public String NOI_DUNG { get; set; }
		public String GHI_CHU { get; set; }
		public Decimal? TRANG_THAI_ID { get; set; }
		[UIHint("DateNullable")]
		public DateTime? KHAI_THAC_NGAY_TU { get; set; }
		[UIHint("DateNullable")]
		public DateTime? KHAI_THAC_NGAY_DEN { get; set; }
		public String HOP_DONG_SO { get; set; }
		[UIHint("DateNullable")]
		public DateTime? HOP_DONG_NGAY { get; set; }
		public Decimal? DOI_TAC_ID { get; set; }
		public String DOI_TAC_TEN { get; set; }
		[UIHint("InputAddon")]
		public Decimal? SO_TIEN_TAM_TINH { get; set; }
		[UIHint("InputAddon")]
		public Decimal? CHO_THUE_GIA { get; set; }
		public Decimal? CHO_THUE_PHUONG_THUC_ID { get; set; }
		public Decimal? LDLK_HINH_THUC_ID { get; set; }
		[UIHint("DateNullable")]
		public DateTime? NGAY_TAO { get; set; }
		public decimal? NGUOI_TAO_ID { get; set; }
		public IList<KhaiThacTaiSanModel> ListKhaiThacTaiSanModel { get; set; }
		public IList<int> lstKhaiThacTaiSan { get; set; }
		public IList<TaiSanModel> ListTaiSanModel { get; set; }
		public enumPhuongThucChoThue enumPhuongThucChoThue { get; set; }
		public SelectList PhuongThucChoThueAvaliable { get; set; }
		public IList<SelectListItem> DoiTacAvaliable { get; set; }
		public enumHinhThucLDLK enumHinhThucLDLK { get; set; }
		public SelectList HinhThucLDLKAvaliable { get; set; }
		public String TEN_PHAP_NHAN_MOI { get; set; }
		public decimal? DIEN_TICH_KT { get; set; }
		public String TenLoaiTaiSan { get; set; }
		public String strVNSoTienTamTinh { get; set; }
		public String DON_VI_MA { get; set; }
		[UIHint("InputAddon")]
		public Decimal? SO_TIEN_KHAI_THAC_LUY_KE { get; set; }
		public decimal? DoiTacId { get; set; }
	}
	public partial class KhaiThacSearchModel : BaseSearchModel
	{
		public KhaiThacSearchModel()
		{
		}
		public string KeySearch { get; set; }
		public decimal? donviId { get; set; }
		public decimal? idKhaiThac { get; set; }
		public string tendonvi { get; set; }
		public string QUYET_DINH_SO { get; set; }
		[UIHint("DateNullable")]
		public DateTime? QUYET_DINH_NGAY { get; set; }
		public string HOP_DONG_SO { get; set; }
		[UIHint("DateNullable")]
		public DateTime? HOP_DONG_NGAY { get; set; }
		[UIHint("DateNullable")]
		public DateTime? KHAI_THAC_NGAY_TU { get; set; }
		[UIHint("DateNullable")]
		public DateTime? KHAI_THAC_NGAY_DEN { get; set; }
		public decimal LOAI_HINH_KHAI_THAC_ID { get; set; }
		public decimal? isdanhsach { get; set; }
	}
	public partial class KhaiThacListModel : BasePagedListModel<KhaiThacModel>
	{

	}
	public partial class KhaiThacTaiSanChiTietModel
	{
		public KhaiThacTaiSanChiTietModel()
		{
			DoiTacAvailable = new List<SelectListItem>();
			//ListKhaiThacTaiSanModel = new List<KhaiThacTaiSanModel>();
			//ListTaiSanModel = new List<TaiSanModel>();
		}
		public decimal ID { get; set; }
		public decimal? KhaiThacChiTietId { get; set; }
		public decimal KhaiThacID { get; set; }
		public decimal TaiSanID { get; set; }
		public String TaiSanMa { get; set; }
		public String TaiSanTen { get; set; }
		public decimal? LoaiTaiSanTen { get; set; }
		public decimal? PhuongThucChoThueId { get; set; }
		public SelectList PhuongThucChoThueAvailable { get; set; }
		public enumPhuongThucChoThue enumPhuongThucChoThue { get; set; }
		public string HopDongSo { get; set; }
		[UIHint("DateNullable")]
		public DateTime? NgayHopDong { get; set; }
		public decimal? DoiTacId { get; set; }
		public IList<SelectListItem> DoiTacAvailable { get; set; }
		public DateTime? NgayKhaiThac_Tu { get; set; }
		public DateTime? NgayKhaiThac_Den { get; set; }
		public DateTime? NgaySuDung { get; set; }
		public decimal? DonGiaChoThue { get; set; }
		public decimal? LoaiHinhTaiSanId { get; set; }
		public decimal? DienTich { get; set; }
		public decimal? DienTichChoThue { get; set; }
		public decimal? phongBanId { get; set; }
		public decimal? DonViID { get; set; }

	}
}

