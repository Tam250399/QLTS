using FluentValidation.Attributes;
using GS.Core.Domain.TaiSans;
using GS.Web.Framework.Models;
using GS.Web.Validators.TaiSans;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.TaiSans
{
	[Validator(typeof(KhaiThacChiTietValidator))]
	public class KhaiThacChiTietModel : BaseGSEntityModel
	{
		public KhaiThacChiTietModel()
		{
			DoiTacAvailable = new List<SelectListItem>();
			HinhThucLDLKAvaliable = new List<SelectListItem>();
			PhuongThucChoThueAvailable = new List<SelectListItem>();
			KhaiThac = new KhaiThac();
		}
		[UIHint("DateNullable")]
		public DateTime? NGAY_KHAI_THAC { get; set; }
        public string NgayKhaiThacString { get; set; }
        [UIHint("InputAddon")]
		public decimal SO_TIEN_THU_DUOC { get; set; }
		public string GHI_CHU { get; set; }
		public string strVNSoTienThuDuoc { get; set; }
		public decimal KHAI_THAC_ID { get; set; }
		public KhaiThacModel khaiThac { get; set; }

		public decimal? KhaiThacChiTietId { get; set; }
		public decimal CHO_THUE_PHUONG_THUC_ID { get; set; }
		public string HOP_DONG_SO { get; set; }
		[UIHint("DateNullable")]
		public DateTime? HOP_DONG_NGAY { get; set; }
		[UIHint("DateNullable")]
		public DateTime? NGAY_SU_DUNG { get; set; }
		[UIHint("DateNullable")]
		public DateTime? NGAY_KHAI_THAC_DEN { get; set; }
		public decimal? DOI_TAC_ID { get; set; }
		[UIHint("InputAddon")]
		public decimal? CHO_THUE_GIA { get; set; }
		public decimal? TAI_SAN_ID { get; set; }
		public decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
		public string TAI_SAN_MA { get; set; }
		public string TAI_SAN_TEN { get; set; }
		public decimal? DIEN_TICH { get; set; }
		public decimal? DON_VI_ID { get; set; }
        public string TEN_PHAP_NHAN_MOI { get; set; }
        public IList<SelectListItem> DoiTacAvailable { get; set; }
		public IList<SelectListItem> PhuongThucChoThueAvailable { get; set; }
		//public enumPhuongThucChoThue enumPhuongThucChoThue { get; set; }
		public enumPhuongThucChoThue enumPhuongThucChoThue
		{
			get => (enumPhuongThucChoThue)(CHO_THUE_PHUONG_THUC_ID);
			set => CHO_THUE_PHUONG_THUC_ID = (int)value;
		}
		public enumHinhThucLDLK enumHinhThucLDLK
		{
			get => (enumHinhThucLDLK)(LDLK_HINH_THUC_ID??0);
			set => LDLK_HINH_THUC_ID = (int)value;
		}
		public enumHinhThucKhaiThac enumHinhThucKhaiThac
		{
			get => (enumHinhThucKhaiThac)(LOAI_HINH_KHAI_THAC_ID);
			set => LOAI_HINH_KHAI_THAC_ID = (int)value;
		}
		public string phuongthucchothueten { get; set; }
		public string HinhThucLDLKTen { get; set; }
		public string HinhThucKhaiThac { get; set; }
		public string LoaiTaiSanTen { get; set; }
		public decimal? DoiTacId { get; set; }
		public string DoiTacTen { get; set; }
		[UIHint("InputAddon")]
		public decimal? DON_GIA_CHO_THUE { get; set; }
		public decimal? DIEN_TICH_KHAI_THAC { get; set; }
		public decimal? LOAI_HINH_KHAI_THAC_ID { get; set; }
		public String StrVNDienTich { get; set; }
		public String StrVNDienTichKhaiThac { get; set; }
		public IList<SelectListItem> HinhThucLDLKAvaliable { get; set; }
		public Decimal? LDLK_HINH_THUC_ID { get; set; }
		#region Thông tin cho cập nhật số tiên khai thác
		public bool IsShowInfo { get; set; }
		public KhaiThac KhaiThac { get; set; }
		[UIHint("InputAddon")]
		public Decimal? SO_TIEN_KHAI_THAC_LUY_KE { get; set; }
		#endregion

	}

	public partial class KhaiThacChiTietSearchModel : BaseSearchModel
	{
		public KhaiThacChiTietSearchModel()
		{
		}

		public string KeySearch { get; set; }
		public decimal KHAI_THAC_ID { get; set; }
		public Decimal? LOAI_HINH_KHAI_THAC_ID { get; set; }
	}

	public partial class KhaiThacChiTietListModel : BasePagedListModel<KhaiThacChiTietModel>
	{
	}
}