using System;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCCT;
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;

namespace GS.Web.Reports.TS_BCCT
{
	public partial class rptTS_BCCT_01E_DK_TSNN
	{
		private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
		private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
		private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;

		public rptTS_BCCT_01E_DK_TSNN(
			BaoCaoTaiSanChiTietSearchModel baoCaoTaiSanChiTietSearchModel,
			CauHinhBaoCaoModel cauHinhBaoCaoModel,
			CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel
			)
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

		private void tableCell81_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCT_01E_DK_TSNN _row = GetCurrentRow() as TS_BCCT_01E_DK_TSNN;
			var _thisCell = (sender as XRTableCell);
			if (_row != null)
			{
				switch (_row.LOAI_HINH_TAI_SAN_ID)
				{
					case (int)enumLOAI_HINH_TAI_SAN.NHA:
						_thisCell.Text = _row.TEN_LOAI_TAI_SAN;
						break;

					case (int)enumLOAI_HINH_TAI_SAN.OTO:
						if (_row.SO_CHO > 0)
							_thisCell.Text = _row.SO_CHO.ToString() + " chỗ";
						else if (_row.TAI_TRONG > 0)
							_thisCell.Text = _row.TAI_TRONG.ToString();
						break;

					case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
					case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
					case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
					case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
						_thisCell.Text = _row.THONG_SO_KY_THUAT;
						break;

					default:
						_thisCell.Text = "";
						break;
				}
			}
		}

		private void tableCell84_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCT_01E_DK_TSNN _row = GetCurrentRow() as TS_BCCT_01E_DK_TSNN;
			if (_row != null)
			{
				var _thisCell = (sender as XRTableCell);
				if (_row.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT && _row.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAC_THU && _row.GIA_TRI_CON_LAI >= 0 && _row.NGUYEN_GIA.Value>0)
				{
					var gtcl = _row.GIA_TRI_CON_LAI ?? 0;
					var ng = (_row.NGUYEN_GIA == 0 || _row.NGUYEN_GIA == null) ? 1 : _row.NGUYEN_GIA.Value;
					_thisCell.Text = Math.Ceiling((gtcl / ng) * 100).ToString();
				}
				else
				{
					_thisCell.Text = "";
				}
			}
		}

		private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			
		}

		private void rptTS_BCCT_01E_DK_TSNN_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
				item.BorderColor = ColorTranslator.FromHtml(_cauHinhBaoCaoChungModel.ColorBorder);
				item.BorderWidth = _cauHinhBaoCaoChungModel.WidthBorder;
				item.BorderDashStyle = (BorderDashStyle)_cauHinhBaoCaoChungModel.StyleBorder;
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
			if (_cauHinhBaoCaoModel.MarginTop > 0)
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

		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDonVi.Text = "Bộ, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanChiTietSearchModel.TEN_BO_NGANH) + "\r\n" + "Tên đơn vị: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI + "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanChiTietSearchModel.MA_DON_VI;
			lb_DieuKienBaoCao.Text = "Thời điểm báo cáo: Từ ngày " + _baoCaoTaiSanChiTietSearchModel.NgayBatDau.toDateVNString() + " đến ngày " + _baoCaoTaiSanChiTietSearchModel.NgayKetThuc.toDateVNString() + "\r\n" + _baoCaoTaiSanChiTietSearchModel.StringLoaiTaiSan.ToStringLoaiHinhTaiSanFromString() + "\r\n" + "Chi tiết bậc báo cáo: " + _baoCaoTaiSanChiTietSearchModel.BacTaiSan + "\r\n" + "Lý do giảm: " + _baoCaoTaiSanChiTietSearchModel.lstNhanHienThi.Where(c => c.Key == ("List." + new enumLyDoBienDongBC().GetType().FullName)).Select(c => c.Value).FirstOrDefault();
		}

		private void label13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{

		}

		private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			ChucDanhNguoiLapBieu.Text = _cauHinhBaoCaoModel.ChucDanhNguoiLapBieu;
			ChucDanhKeToanTruong.Text = _cauHinhBaoCaoModel.ChucDanhKeToanTruong;
			ChucDanhThuTruongDonVi.Text = _cauHinhBaoCaoModel.ChucDanhThuTruongDonVi;
			TenNguoiLapBieu.Text = _cauHinhBaoCaoModel.TenNguoiLapBieu;
			TenKeToanTruong.Text = _cauHinhBaoCaoModel.TenKeToanTruong;
			TenThuTruongDonVi.Text = _cauHinhBaoCaoModel.TenThuTruongDonVi;
			DiaDanhBaoCao.Text = _cauHinhBaoCaoModel.DiaDanhBaoCao;
			if (String.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhNguoiLapBieu))
				lb_GhiChuNLB.Visible = false;
			if (String.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhKeToanTruong))
				lb_GhiChuKeToan.Visible = false;
			if (String.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhThuTruongDonVi))
				lb_GhiChuThuTruong.Visible = false;
		}
		#region Cấp 1
		private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{

		}
		#endregion
		#region Cấp 2
		private void GroupHeader3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			string Cap_2 = GetCurrentColumnValue<string>("CAP_2");
			var _thisRow = (sender as GroupHeaderBand);
			if (Cap_2 == null || Cap_2.Length < 4 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 2)
			{
			
				_thisRow.Visible = false;
			}
			else
			{
				_thisRow.Visible = true;
			}
		}

		private void tableCell45_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			string TEN_2 = GetCurrentColumnValue<string>("TEN_2");
			var _thisCell = (sender as XRTableCell);
			_thisCell.Text = "  " + TEN_2;
		}
		#endregion

		#region Cấp 3
		private void GroupHeader4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			var _thisRow = (sender as GroupHeaderBand);
			string Cap_3 = GetCurrentColumnValue<string>("CAP_3");
			if (Cap_3 == null || Cap_3.Length < 4 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 3)
			{
				_thisRow.Visible = false;
			}
			else
			{
				_thisRow.Visible = true;
			}
		}
		private void tableCell56_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			string TEN_3 = GetCurrentColumnValue<string>("TEN_3");
			var _thisCell = (sender as XRTableCell);
			_thisCell.Text = "    " + TEN_3;
		}
		#endregion
		#region Cấp 4
		private void GroupHeader5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			var _thisRow = (sender as GroupHeaderBand);
			string Cap_4 = GetCurrentColumnValue<string>("CAP_4");
			if (Cap_4 == null || Cap_4.Length < 4 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 4)
			{
				_thisRow.Visible = false;
			}
			else
			{
				_thisRow.Visible = true;
			}
		}

		private void tableCell67_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			string TEN_4 = GetCurrentColumnValue<string>("TEN_4");
			var _thisCell = (sender as XRTableCell);
			_thisCell.Text = "      " + TEN_4;
		}
		#endregion
		#region Cấp 5
		private void GroupHeader6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			var _thisRow = (sender as GroupHeaderBand);
			string Cap_5 = GetCurrentColumnValue<string>("CAP_5");
			if (Cap_5 == null || Cap_5.Length < 4 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 5)
			{
				_thisRow.Visible = false;
			}
			else
			{
				_thisRow.Visible = true;
			}
		}

		private void tableCell100_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			string TEN_5 = GetCurrentColumnValue<string>("TEN_5");
			var _thisCell = (sender as XRTableCell);
			_thisCell.Text = "        " + TEN_5;
		}



		#endregion

		private void tableCell78_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCT_01E_DK_TSNN _row = GetCurrentRow() as TS_BCCT_01E_DK_TSNN;
			if (_row != null)
			{
				var _thisCell = (sender as XRTableCell);
				StringBuilder @string = new StringBuilder("   ");
				for (int level = 0; level < _row.TREE_LEVEL; level++)
					@string.Append(" ");
				_thisCell.Text = @string.ToString() + _row.TAI_SAN_TEN;
			}
		}

		private void detailBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCT_01E_DK_TSNN _row = GetCurrentRow() as TS_BCCT_01E_DK_TSNN;
			if (_row != null)
			{
				if (_row.TREE_LEVEL + 1  > _baoCaoTaiSanChiTietSearchModel.BacTaiSan)
				{
					var _thisRow = (sender as DetailBand);
					_thisRow.Visible = false;
				}
				else
				{
					var _thisRow = (sender as DetailBand);
					_thisRow.Visible = true;
				}
			}
		}

		private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			//TS_BCCT_01E_DK_TSNN _rowC = GetCurrentRow() as TS_BCCT_01E_DK_TSNN;
			//TS_BCCT_01E_DK_TSNN _rowN = GetNextRow() as TS_BCCT_01E_DK_TSNN;
			//var _thisRow = (sender as GroupFooterBand);
			//_thisRow.Visible = false;
			//if (_rowN == _rowC)
			//{
			//	_thisRow.Visible = true;
			//}
			//ChucDanhNguoiLapBieu.Text = _cauHinhBaoCaoModel.ChucDanhNguoiLapBieu;
			//ChucDanhKeToanTruong.Text = _cauHinhBaoCaoModel.ChucDanhKeToanTruong;
			//ChucDanhThuTruongDonVi.Text = _cauHinhBaoCaoModel.ChucDanhThuTruongDonVi;
			//TenNguoiLapBieu.Text = _cauHinhBaoCaoModel.TenNguoiLapBieu;
			//TenKeToanTruong.Text = _cauHinhBaoCaoModel.TenKeToanTruong;
			//TenThuTruongDonVi.Text = _cauHinhBaoCaoModel.TenThuTruongDonVi;
			//DiaDanhBaoCao.Text = _cauHinhBaoCaoModel.DiaDanhBaoCao;
			//if (String.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhNguoiLapBieu))
			//	lb_GhiChuNLB.Visible = false;
			//if (String.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhKeToanTruong))
			//	lb_GhiChuKeToan.Visible = false;
			//if (String.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhThuTruongDonVi))
			//	lb_GhiChuThuTruong.Visible = false;
		}

		private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lb_DonVi.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Diện tích đất - nhà là: " + _baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich() + ";Giá trị là: " + _baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien() + ".";
		}

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}
	}
}
