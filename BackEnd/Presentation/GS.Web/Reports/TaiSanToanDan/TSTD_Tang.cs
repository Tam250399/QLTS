using System;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TSTD;
using GS.Core.Domain.TaiSans;
using GS.Core.Domain.SHTD;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;

namespace GS.Web.Reports.TaiSanToanDan
{
    public partial class TSTD_Tang
    {
        private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;

		private int stt1 = 0;
		private int stt2 = 0;
		private int stt = 0;
		private int stt_1 = 0;
		private int stt_2 = 0;
		private int stt_3 = 0;
		public TSTD_Tang(BaoCaoTaiSanChiTietSearchModel baoCaoTaiSanChiTietSearchModel,
            CauHinhBaoCaoModel cauHinhBaoCaoModel,
            CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel)
        {
            InitializeComponent();
            this._baoCaoTaiSanChiTietSearchModel = baoCaoTaiSanChiTietSearchModel;
            this._cauHinhBaoCaoModel = cauHinhBaoCaoModel;
            this._cauHinhBaoCaoChungModel = cauHinhBaoCaoChungModel;
            this.ExportOptions.Xls.FitToPrintedPageWidth = true;
            this.ExportOptions.Xlsx.FitToPrintedPageWidth = true;
            this.ExportOptions.Xls.ExportMode = _baoCaoTaiSanChiTietSearchModel.enumDinhDanhXlsExcel;
            this.ExportOptions.Xlsx.ExportMode = _baoCaoTaiSanChiTietSearchModel.enumDinhDanhXlsxExcel;
        }
		private void TSTD_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			//cấu hình chung
			if (String.IsNullOrEmpty(_cauHinhBaoCaoChungModel.Color))
			{
				_cauHinhBaoCaoChungModel.Color = "#FFF";
			}
			if (String.IsNullOrEmpty(_cauHinhBaoCaoChungModel.ColorBorder))
			{
				_cauHinhBaoCaoChungModel.ColorBorder = "#000";
			}
			if (String.IsNullOrEmpty(_cauHinhBaoCaoChungModel.Font))
			{
				_cauHinhBaoCaoChungModel.Font = "Times New Roman";
			}
			if (_cauHinhBaoCaoChungModel.WidthBorder == 0)
			{
				_cauHinhBaoCaoChungModel.WidthBorder = 1;
			}
			var report = (sender as XtraReport);
			report.BackColor = ColorTranslator.FromHtml(_cauHinhBaoCaoChungModel.Color);
			foreach (var item in this.AllControls<XRControl>())
			{
				item.Font = new System.Drawing.Font(_cauHinhBaoCaoChungModel.Font, item.Font.Size, item.Font.Style);
				//item.BorderColor = ColorTranslator.FromHtml(_cauHinhBaoCaoChungModel.ColorBorder);
				//item.BorderWidth = _cauHinhBaoCaoChungModel.WidthBorder;
				//item.BorderDashStyle = (BorderDashStyle)_cauHinhBaoCaoChungModel.StyleBorder;
			}
			if (_cauHinhBaoCaoChungModel.MarginTop > 0)
			{
				Margins.Top = Convert.ToInt32(_cauHinhBaoCaoChungModel.MarginTop * 0.3937 * 100);
			}
			if (_cauHinhBaoCaoChungModel.MarginBottom > 0)
			{
				Margins.Bottom = Convert.ToInt32(_cauHinhBaoCaoChungModel.MarginBottom * 0.3937 * 100);
			}
			//cấu hình riêng
			{
				Margins.Top = Convert.ToInt32(_cauHinhBaoCaoModel.MarginTop * 0.3937 * 100);
			}
			if (_cauHinhBaoCaoModel.MarginBottom > 0)
			{
				Margins.Bottom = Convert.ToInt32(_cauHinhBaoCaoModel.MarginBottom * 0.3937 * 100);
			}
			//if (_cauHinhBaoCaoModel.MarginLeft > 0)
			//{
			//	Margins.Left = Convert.ToInt32(_cauHinhBaoCaoModel.MarginLeft * 0.3937 * 100);
			//}
			//if (_cauHinhBaoCaoModel.MarginRight > 0)
			//{
			//	Margins.Right = Convert.ToInt32(_cauHinhBaoCaoModel.MarginRight * 0.3937 * 100);
			//}			
		}
		//private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Khối lượng là: " + ((enumDonViKhoiLuong)_baoCaoTaiSanChiTietSearchModel.DonViKhoiLuong).ToString() + "; Diện tích là: " + _baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Giá trị là: " + _baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien() + ".";
		//}
		#region Cấu hình
		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			label3.Text = "Kỳ báo cáo: Từ ngày "+_baoCaoTaiSanChiTietSearchModel.NgayBatDau.toDateVNString()+ " đến ngày " + _baoCaoTaiSanChiTietSearchModel.NgayKetThuc.toDateVNString() + " \r\n " + "Nguồn gốc tài sản: " + _baoCaoTaiSanChiTietSearchModel.TenTaiSan + "; Chi tiết bậc báo cáo: " + _baoCaoTaiSanChiTietSearchModel.BacNguonGocTSTD;
			lblDonVi.Text = "Cơ quan quản lý cấp trên: " + _baoCaoTaiSanChiTietSearchModel.TEN_BO_NGANH + "\r\n" + "Cơ quan chủ trì xử lý tài sản: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI;
		}
		#endregion
		//private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TSTD_MAU_03_BC_XLSHTD _rowC = GetCurrentRow() as TSTD_MAU_03_BC_XLSHTD;
		//	if (_rowC == null && GetCurrentRow() != null)
		//		_rowC = GetCurrentRow().toStringJson().toEntity<TSTD_MAU_03_BC_XLSHTD>();
		//	TSTD_MAU_03_BC_XLSHTD _rowN = GetNextRow() as TSTD_MAU_03_BC_XLSHTD;
		//	if (_rowN == null && GetCurrentRow() != null)
		//		_rowN = GetCurrentRow().toStringJson().toEntity<TSTD_MAU_03_BC_XLSHTD>();
		//	var _thisRow = (sender as GroupFooterBand);
		//	_thisRow.Visible = false;
		//	if (_rowN == _rowC)
		//	{
		//		_thisRow.Visible = true;
		//	}
		//	if (_cauHinhBaoCaoModel.ChucDanhNguoiLapBieu != null)
		//	{
		//		ChucDanhNguoiLapBieu.Text = _cauHinhBaoCaoModel.ChucDanhNguoiLapBieu;
		//	}
		//	if (_cauHinhBaoCaoModel.ChucDanhThuTruongDonVi != null)
		//	{
		//		ChucDanhThuTruongDonVi.Text = _cauHinhBaoCaoModel.ChucDanhThuTruongDonVi;
		//	}
		//	if (_cauHinhBaoCaoModel.TenNguoiLapBieu != null)
		//	{
		//		TenNguoiLapBieu.Text = _cauHinhBaoCaoModel.TenNguoiLapBieu;
		//	}
		//	if (_cauHinhBaoCaoModel.TenThuTruongDonVi != null)
		//	{
		//		TenThuTruongDonVi.Text = _cauHinhBaoCaoModel.TenThuTruongDonVi;
		//	}
		//	if (_cauHinhBaoCaoModel.DiaDanhBaoCao != null)
		//	{
		//		DiaDanhBaoCao.Text = _cauHinhBaoCaoModel.DiaDanhBaoCao;
		//	}
		//}
		private void taisanTen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_03_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_03_BC_XLSHTD;
			if (_row != null)
			{
				var _thisCell = (sender as XRTableCell);
				StringBuilder @string = new StringBuilder("   ");
				for (int level = 0; level < _row.TREE_LEVEL_NGUON_GOC; level++)
					@string.Append(" ");
				_thisCell.Text = @string.ToString() + _row.TEN_TAI_SAN;
			}

		}

		private void NguonCap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_03_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_03_BC_XLSHTD;
			if (_row == null || _row.NG_CAP_2 == null || _baoCaoTaiSanChiTietSearchModel.BacNguonGocTSTD < 2)
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = false;
			}
			else
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = true;
			}
		}
		private void NguonCap3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_03_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_03_BC_XLSHTD;
			if (_row == null || _row.NG_CAP_3 == null || _baoCaoTaiSanChiTietSearchModel.BacNguonGocTSTD < 3)
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = false;
			}
			else
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = true;
			}
		}
		private void NguonCap4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_03_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_03_BC_XLSHTD;
			if (_row == null || _row.NG_CAP_4 == null || _baoCaoTaiSanChiTietSearchModel.BacNguonGocTSTD < 4)
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = false;
			}
			else
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = true;
			}
		}
		private void NguonCap5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_03_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_03_BC_XLSHTD;
			if (_row == null || _row.NG_CAP_5 == null || _baoCaoTaiSanChiTietSearchModel.BacNguonGocTSTD < 5)
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = false;
			}
			else
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = true;
			}
		}
		
        private void stt_ng_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TSTD_MAU_03_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_03_BC_XLSHTD;
            var _this = sender as XRTableCell;
            if (_row != null)
            {
                stt_1 += 1;
                _this.Text = $"{stt_1.ConvertIntToRomanNumber()}";
            }
			stt1 = 0;
            stt_2 = 0;
            stt_3 = 0;
        }
		private void tableCell46_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            TSTD_MAU_03_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_03_BC_XLSHTD;
            var _this = sender as XRTableCell;
            if (_row != null)
            {
                stt_2 += 1;
                _this.Text = $"{ stt_1.ConvertIntToRomanNumber()}.{stt_2}";
            }
			stt1 = 0;
			stt_3 = 0;
        }
		private void tableCell51_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            TSTD_MAU_03_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_03_BC_XLSHTD;
            var _this = sender as XRTableCell;
            if (_row != null)
            {			
				stt_3 += 1;
                _this.Text = $"{ stt_1.ConvertIntToRomanNumber()}.{stt_2}.{stt_3}";
            }
			stt1 = 0;
		}
		private void Detail_BeforePrint_2(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_03_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_03_BC_XLSHTD;
			if (_row != null)
			{
				var _thisRow = (sender as DetailBand);
				if (_baoCaoTaiSanChiTietSearchModel.BacTaiSan < 2)
				{

					_thisRow.Visible = false;
				}
				else
				{
					_thisRow.Visible = true;
				}
			}
		}

		private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{

			chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Khối lượng là: " + ((enumDonViKhoiLuong)_baoCaoTaiSanChiTietSearchModel.DonViKhoiLuong).ToString() + "; Diện tích là: " + _baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Giá trị là: " + _baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien() + ".";
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

       

        private void tableCell8_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TSTD_MAU_03_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_03_BC_XLSHTD;
			if (_row != null)
			{
				var _this = sender as XRTableCell;
				var TenLoaiHinh = _baoCaoTaiSanChiTietSearchModel.lstNhanHienThi.Where(c => c.Key == (new enumNHAN_HIEN_THI_LOAI_HINH_TSTD().GetType().FullName) + "." + ((enumNHAN_HIEN_THI_LOAI_HINH_TSTD)_row.LOAI_HINH_TAI_SAN_ID).ToString()).Select(c => c.Value).FirstOrDefault();
				_this.Text = $"{TenLoaiHinh}";
			}
		}
		private void tableCell3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_03_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_03_BC_XLSHTD;
			var _this = sender as XRTableCell;
			if (_row != null)
			{
				stt1 += 1;
				_this.Text = $"{stt1}";
			}
			stt2 = 0;
		}

        private void tableCell11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TSTD_MAU_03_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_03_BC_XLSHTD;
			var _this = sender as XRTableCell;
			if (_row != null)
			{
				stt2 += 1;
				_this.Text = $"{stt1}. {stt2}";
			}			
		}

        private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			
		}

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}
	}
}
