using System;
using DevExpress.XtraReports.UI;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanTongHop;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCQH;
using DevExpress.XtraPrinting;
using System.Drawing;
using DevExpress.DataAccess.Sql;

namespace GS.Web.Reports.TS_BCQH
{
	public partial class rptTS_BCQH_MAU02_CCTSNN
	{
		private readonly BaoCaoTaiSanTongHopSearchModel _baoCaoTaiSanTongHopSearchModel;
		private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
		private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
		public rptTS_BCQH_MAU02_CCTSNN(
			BaoCaoTaiSanTongHopSearchModel baoCaoTaiSanTongHopSearchModel,
			CauHinhBaoCaoModel cauHinhBaoCaoModel,
			CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel
			)
		{
			InitializeComponent();
			this._baoCaoTaiSanTongHopSearchModel = baoCaoTaiSanTongHopSearchModel;
			this._cauHinhBaoCaoModel = cauHinhBaoCaoModel;
			this._cauHinhBaoCaoChungModel = cauHinhBaoCaoChungModel;
			this.ExportOptions.Xls.FitToPrintedPageWidth = true;
			this.ExportOptions.Xlsx.FitToPrintedPageWidth = true;
			this.ExportOptions.Xls.ExportMode = _baoCaoTaiSanTongHopSearchModel.enumDinhDanhXlsExcel;
			this.ExportOptions.Xlsx.ExportMode = _baoCaoTaiSanTongHopSearchModel.enumDinhDanhXlsxExcel;
		}

		private void tableCell166_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCQH_MAU02_CCTSNN _row = GetCurrentRow() as TS_BCQH_MAU02_CCTSNN;
			if (_row != null)
			{
				var _thisRow = (sender as XRTableCell);
				switch (_row.LOAI_HINH_TAI_SAN_ID)
				{
					case 1:
						_thisRow.Text = "1. Đất";
						break;
					case 2:
						_thisRow.Text = "2. Nhà";
						break;
					case 4:
						_thisRow.Text = "3. Ô tô";
						break;
					case 499:
						_thisRow.Text = "4. Tài sản cố định khác";
						break;
						//case 500:
						//	_thisRow.Text = "4- Tài sản khác có nguyên giá từ 500 triệu đồng trở lên";
						//	break;
						//case 501:
						//	_thisRow.Text = "5- Tài sản khác có nguyên giá dưới 500 triệu đồng";
						//break;
				}
			}
		}

		private void rptTS_BCQH_MAU02_CCTSNN_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
		}

		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			label4.Text = "Đơn vị báo cáo: " + _baoCaoTaiSanTongHopSearchModel.TEN_DON_VI;
			tieude.Text = "Phụ lục II" + "\r\n" + "CƠ CẤU TÀI SẢN CÔNG TẠI CÁC CƠ QUAN, TỔ CHỨC, ĐƠN VỊ," + "\r\n" + "BAN QUẢN LÝ DỰ ÁN" + "\r\n" + "(Thời điểm báo cáo: " + _baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString() + ")";
		}

		private void tableCell117_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCQH_MAU02_CCTSNN _row = GetCurrentRow() as TS_BCQH_MAU02_CCTSNN;
			if (_row != null)
			{
				var _thisRow = (sender as XRTableCell);
				switch (_row.LOAI_HINH_DON_VI)
				{
					case (int)enumLoaiDonVi.CO_QUAN_NN:
						_thisRow.Text = "a- " + "Cơ quan nhà nước";
						break;
					case (int)enumLoaiDonVi.DOANH_NGHIEP:
					case (int)enumLoaiDonVi.DON_VI_SU_NGHIEP:
						_thisRow.Text = "b- " + "Đơn vị sự nghiệp";
						break;
					case (int)enumLoaiDonVi.TO_CHUC:
						_thisRow.Text = "c- " + "Các tổ chức";
						break;
					case (int)enumLoaiDonVi.BAN_QUAN_LY_DU_AN:
						_thisRow.Text = "d- " + "Ban quản lý dự án";
						break;
					default:
						_thisRow.Text = "Đơn vị lực lượng vũ trang nhân dân";
						break;
				}

			}
		}

		private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			donviTien.Text = _baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien();
			donviDienTich.Text = _baoCaoTaiSanTongHopSearchModel.DonViDienTich.ToStringDonViDienTich();
		}


		private void SubBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{

			TS_BCQH_MAU02_CCTSNN _row = GetCurrentRow() as TS_BCQH_MAU02_CCTSNN;
			if (_row != null)
			{
				var _thisRow = (sender as SubBand);
				if ((_row.SO_LUONG_TRUNG_UONG ?? 0) <= 0)
				{
					_thisRow.Visible = false;

				}
				else
				{
					_thisRow.Visible = true;
				}

			}
		}

		private void SubBand2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCQH_MAU02_CCTSNN _row = GetCurrentRow() as TS_BCQH_MAU02_CCTSNN;
			if (_row != null)
			{
				var _thisRow = (sender as SubBand);
				if ((_row.SO_LUONG_DIA_PHUONG ?? 0) <= 0)
				{
					_thisRow.Visible = false;

				}
				else
				{
					_thisRow.Visible = true;
				}

			}
		}

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}
	}
}