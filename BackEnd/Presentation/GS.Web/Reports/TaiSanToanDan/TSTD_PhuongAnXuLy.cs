using System;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TSTD;
using GS.Core.Domain.SHTD;
using GS.Services.DanhMuc;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;

namespace GS.Web.Reports.TaiSanToanDan
{
	public partial class TSTD_HinhThucXuLy
	{
		private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
		private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
		private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
		private int Cap1;
		private int Cap2;
		public TSTD_HinhThucXuLy(
			BaoCaoTaiSanChiTietSearchModel baoCaoTaiSanChiTietSearchModel,
			CauHinhBaoCaoModel cauHinhBaoCaoModel,
			CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel
			)
		{
			InitializeComponent();
			this.Cap1 = 0;
			this.Cap2 = 0;
			this._baoCaoTaiSanChiTietSearchModel = baoCaoTaiSanChiTietSearchModel;
			this._cauHinhBaoCaoModel = cauHinhBaoCaoModel;
			this._cauHinhBaoCaoChungModel = cauHinhBaoCaoChungModel;
			this.ExportOptions.Xls.FitToPrintedPageWidth = true;
			this.ExportOptions.Xlsx.FitToPrintedPageWidth = true;
			this.ExportOptions.Xls.ExportMode = _baoCaoTaiSanChiTietSearchModel.enumDinhDanhXlsExcel;
			this.ExportOptions.Xlsx.ExportMode = _baoCaoTaiSanChiTietSearchModel.enumDinhDanhXlsxExcel;
		}
		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDonVi.Text = "Cơ quan quản lý cấp trên: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI_CAP_TREN + "\r\n" + "Cơ quan chủ trì xử lý tài sản: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI;
			lblDieuKien.Text = "Nguồn gốc tài sản: " + _baoCaoTaiSanChiTietSearchModel.TenTaiSan + "\r\n" + "Hình thức xử lý tài sản: " + _baoCaoTaiSanChiTietSearchModel.TenHinhThucXuLy;
		}

		private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			if (_cauHinhBaoCaoModel.ChucDanhNguoiLapBieu != null)
			{
				ChucDanhNguoiLapBieu.Text = _cauHinhBaoCaoModel.ChucDanhNguoiLapBieu;
			}
			if (_cauHinhBaoCaoModel.ChucDanhThuTruongDonVi != null)
			{
				ChucDanhThuTruongDonVi.Text = _cauHinhBaoCaoModel.ChucDanhThuTruongDonVi;
			}
			if (_cauHinhBaoCaoModel.TenNguoiLapBieu != null)
			{
				TenNguoiLapBieu.Text = _cauHinhBaoCaoModel.TenNguoiLapBieu;
			}
			if (_cauHinhBaoCaoModel.TenThuTruongDonVi != null)
			{
				TenThuTruongDonVi.Text = _cauHinhBaoCaoModel.TenThuTruongDonVi;
			}
			if (_cauHinhBaoCaoModel.DiaDanhBaoCao != null)
			{
				DiaDanhBaoCao.Text = _cauHinhBaoCaoModel.DiaDanhBaoCao;
			}
			if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhNguoiLapBieu) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenNguoiLapBieu))
				lb_GhiChuNLB.Visible = false;
			if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhThuTruongDonVi) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenThuTruongDonVi))
				lb_GhiChuThuTruong.Visible = false;
		}

		private void cap1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{

		}

		private void cap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			BaoCaoPhuongAnXuLyTSTD _row = GetCurrentRow() as BaoCaoPhuongAnXuLyTSTD;
			var _this = sender as GroupHeaderBand;
			if (_row != null && _row.CAP_1 == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT)
			{
				_this.Visible = true;
			}
			else
			{
				_this.Visible = false;
			}
		}

		private void stt1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			BaoCaoPhuongAnXuLyTSTD _row = GetCurrentRow() as BaoCaoPhuongAnXuLyTSTD;
			var _this = sender as XRTableCell;
			if (_row != null && _row.CAP_1 >= 0)
			{
				Cap2 = 0;
			}
			if (_row != null)
				Cap1 += 1;
			_this.Text = Cap1.ToString();
		}

		private void stt2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			BaoCaoPhuongAnXuLyTSTD _row = GetCurrentRow() as BaoCaoPhuongAnXuLyTSTD;
			var _this = sender as XRTableCell;
			if (_row != null && _row.CAP_1 == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT)
				Cap2 += 1;
			if (_row != null && _row.CAP_1 == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT)
				_this.Text = Cap1.ToString() + "." + Cap2.ToString();
		}

		private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			chuthich.Text = /*"ĐVT giá trị là: " + _baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien() + "."*/"";
		}

		private void sttDetail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			BaoCaoPhuongAnXuLyTSTD _row = GetCurrentRow() as BaoCaoPhuongAnXuLyTSTD;
			var _this = sender as XRTableCell;
			if (_row != null && _row.CAP_1 != (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT)
				Cap2 += 1;
			if (_row != null && _row.CAP_1 != (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT)
				_this.Text = Cap1.ToString() + "." + Cap2.ToString();
		}

		private void tencap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			BaoCaoPhuongAnXuLyTSTD _row = GetCurrentRow() as BaoCaoPhuongAnXuLyTSTD;
			var _this = sender as XRTableCell;
			if (_row != null)
				_this.Text = "Đất và tài sản gắn liền với đất " + Cap2.ToString();
		}

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}
	}
}
