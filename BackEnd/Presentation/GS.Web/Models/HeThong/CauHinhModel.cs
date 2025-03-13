//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 27/9/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Framework.Mvc.ModelBinding;
using GS.Web.Models.DanhMuc;
using GS.Web.Validators.CauHinh;
using GS.Web.Validators.HeThong;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.HeThong
{
	[Validator(typeof(CauHinhValidator))]
	public class CauHinhModel : BaseGSEntityModel
	{
		public CauHinhModel()
		{
		}

		public String TEN { get; set; }
		public String GIA_TRI { get; set; }
		public decimal DON_VI_ID { get; set; }
	}

	public partial class CauHinhSearchModel : BaseSearchModel
	{
		public CauHinhSearchModel()
		{
		}

		public String TEN { get; set; }
		public String GIA_TRI { get; set; }
	}

	public partial class CauHinhListModel : BasePagedListModel<CauHinhModel>
	{
	}

	public class CauHinhChungModel : BaseGSModel, ICauHinhModel
	{
		public string IPTruyCapHeThong { get; set; }

		public string IPTruyCapQuanTri { get; set; }

		public string SMTPServerIP { get; set; }

		public int SMTPPort { get; set; }

		public bool LDAPEnable { get; set; }
		public string LDAPServerIP { get; set; }
		public int LDAPPort { get; set; }
		public string LDAPAdminUser { get; set; }

		[DataType(DataType.Password)]
		[NoTrim]
		public string LDAPAdminPassword { get; set; }

		public bool Log404Errors { get; set; }
		public decimal ValueDKTSKhac { get; set; }
		public int NhacNgayKetThucDuAn { get; set; }

		// Thiet lap lich ket xuat du lieu
		public string LichKetXuat { get; set; }
	}

	public class XacThucChungThuSomodel : BaseGSModel, ICauHinhModel
	{
		public bool XacThucChuKySo { get; set; }
		public bool XacThucThaoTacNghiepVu { get; set; }
		public bool CDHTKhauHaoNhapDuDauKy { get; set; }
		public int DinhKyDoiMatKhau { get; set; }
		public bool DonViKhongCanXacThuc { get; set; }
	}

	public class KetXuatDuLieuModel : BaseGSModel, ICauHinhModel
	{
		// Thiet lap lich ket xuat du lieu
		public string LichKetXuat { get; set; }
	}

	public class ThongTinKetXuatDuLieuModel
	{
		/// <summary>
		/// Ket xuat theo hang thang, hang tuan hoac hang ngay
		/// </summary>
		public int LoaiKetXuat { get; set; }

		public int ValueNgay { get; set; }
		public int ValueGio { get; set; }
		public int ValuePhut { get; set; }
		public string Note { get; set; }
	}

	#region Sổ tài sản

	public class CauHinhSoTaiSanModel
	{
		public CauHinhSoTaiSanModel()
		{
			list_TrangThaiNam = new List<TrangThaiNamModel>();
		}

		public List<TrangThaiNamModel> list_TrangThaiNam { get; set; }
	}
	[Validator(typeof(TrangThaiNamValidator))]
	public class TrangThaiNamModel: BaseGSModel, ICauHinhModel
	{
		[UIHint("InputYear")]
		public decimal Nam { get; set; }
		public decimal TrangThai { get; set; }
		public string TenTrangThai { get; set; }
		[UIHint("DateNullable")]
		public DateTime? NgayKhoaSo { get; set; }
		public IList<SelectListItem> ddlTrangThai { get; set; }
	}

	#endregion Sổ tài sản

	#region Định mức chức danh

	public class DinhMucChucDanhModel : BaseGSModel, ICauHinhModel
	{
		public String MA { get; set; }

		[UIHint("DateNullable")]
		public DateTime? TU_NGAY { get; set; }

		[UIHint("DateNullable")]
		public DateTime? DEN_NGAY { get; set; }

		public String SO_QUYET_DINH { get; set; }

		[UIHint("DateNullable")]
		public DateTime? NGAY_QUYET_DINH { get; set; }
		public Boolean IS_HIEU_LUC { get; set; }

		public String THONG_TU_NGHI_DINH { get; set; }
		public IList<ChiTietDinhMucChucDanhModel> ChiTietDinhMuc { get; set; }
	}

	public class ChiTietDinhMucChucDanhModel
	{
		//public
		public Decimal? CHUC_DANH_ID { get; set; }

		public String TEN_CHUC_DANH { get; set; }
		public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
		public Decimal? NHOM_TAI_SAN_ID { get; set; }
		public String TEN_NHOM_TAI_SAN { get; set; }
		public Decimal? DINH_MUC { get; set; }
		public Decimal? SO_LUONG { get; set; }
		public IList<SelectListItem> DDLChucDanh { get; set; }
		public IList<SelectListItem> DDLNhomTaiSan { get; set; }
		public IList<SelectListItem> DDLloaiHinhTaiSan { get; set; }
	}

	public class DinhMucChucDanhSearchModel : BaseSearchModel
	{
		public DinhMucChucDanhSearchModel()
		{
		}

		[UIHint("DateNullable")]
		public DateTime? TuNgay { get; set; }

		[UIHint("DateNullable")]
		public DateTime? DenNgay { get; set; }

		public string SoQuyetDinh { get; set; }

		[UIHint("DateNullable")]
		public DateTime? NgayQuyetDinh { get; set; }
	}

	public partial class DinhMucChucDanhListModel : BasePagedListModel<DinhMucChucDanhModel>
	{
	}

	#endregion Định mức chức danh

	#region Cấu hình đồng bộ

	public class CauHinhDongBoTaiSan
	{
		public bool DongBoTucThoi { get; set; }
		public bool ChayJobDongBo { get; set; }
	}

	#endregion Cấu hình đồng bộ
}