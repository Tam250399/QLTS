using System;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCTH;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;
using GS.Web.Models.BaoCaos.TaiSanTongHop;

namespace GS.Web.Reports.TS_BCTH
{
    public partial class rptTS_BCTH_02H_DK_TSNN
    {
		private readonly BaoCaoTaiSanTongHopSearchModel _baoCaoTaiSanTongHopSearchModel;
		private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
		private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
		private int STT_DV;
		private int STT_DV_1;
		private int STT_DV_2;
		private int STT_DV_3;
		private int STT_DV_4;
		private int STT_DV_5;
		private int STT_TS;
		private int STT_TS_1;
		private int STT_TS_2;
		private int STT_TS_3;
		private int STT_TS_4;
		private int STT_TS_5;

		public rptTS_BCTH_02H_DK_TSNN(
			BaoCaoTaiSanTongHopSearchModel baoCaoTaiSanChiTietSearchModel,
			CauHinhBaoCaoModel cauHinhBaoCaoModel,
			CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel
			)
		{
			InitializeComponent();
			this._baoCaoTaiSanTongHopSearchModel = baoCaoTaiSanChiTietSearchModel;
			this._cauHinhBaoCaoModel = cauHinhBaoCaoModel;
			this._cauHinhBaoCaoChungModel = cauHinhBaoCaoChungModel;
			STT_DV_1 = 0;
			STT_DV = 0;
			STT_DV_2 = 0;
			STT_DV_3 = 0;
			STT_DV_4 = 0;
			STT_DV_5 = 0;
			STT_TS = 0;
			STT_TS_1 = 0;
			STT_TS_2 = 0;
			STT_TS_3 = 0;
			STT_TS_4 = 0;
			STT_TS_5 = 0;
			this.ExportOptions.Xls.FitToPrintedPageWidth = true;
			this.ExportOptions.Xlsx.FitToPrintedPageWidth = true;
			this.ExportOptions.Xls.ExportMode = _baoCaoTaiSanTongHopSearchModel.enumDinhDanhXlsExcel;
			this.ExportOptions.Xlsx.ExportMode = _baoCaoTaiSanTongHopSearchModel.enumDinhDanhXlsxExcel;
		}

		private void LoaiDonVi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			if (_row != null && _baoCaoTaiSanTongHopSearchModel.MauSo != 2)
			{
				var _this = sender as GroupBand;
				_this.Visible = false;
			}
			else
			{
				var _thisRow = (sender as GroupHeaderBand);
				_thisRow.Visible = true;
			}
			STT_TS_1 = 0;
		}
		private void dv1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			if (_row == null || _baoCaoTaiSanTongHopSearchModel.MauSo != 3)
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = false;
			}
			else
			{
				var _thisRow = (sender as GroupHeaderBand);
				_thisRow.Visible = true;
			}
		}
		private void dv2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			if (_row == null || _row.DV_2 == null || _baoCaoTaiSanTongHopSearchModel.MauSo != 3 || _baoCaoTaiSanTongHopSearchModel.BacDonVi < 2)
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
		private void dv3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			if (_row == null || _row.DV_3 == null || _baoCaoTaiSanTongHopSearchModel.MauSo != 3 || _baoCaoTaiSanTongHopSearchModel.BacDonVi < 3)
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
		private void dv4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			if (_row == null || _row.DV_4 == null || _baoCaoTaiSanTongHopSearchModel.MauSo != 3 || _baoCaoTaiSanTongHopSearchModel.BacDonVi < 4)
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
		private void dv5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			if (_row == null || _row.DV_5 == null || _baoCaoTaiSanTongHopSearchModel.MauSo != 3 || _baoCaoTaiSanTongHopSearchModel.BacDonVi < 5)
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
		private void Cap1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			var _thisRow = (sender as GroupHeaderBand);
			if (_row == null)
			{
				_thisRow.Visible = false;
			}
			else
			{
				_thisRow.Visible = true;
			}
		}
		private void Cap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			TS_BCTH_02H_DK_TSNN _row = row ?? new TS_BCTH_02H_DK_TSNN();
			string Cap_2 = _row.CAP_2??"";
			if (Cap_2.Length < 4 || _baoCaoTaiSanTongHopSearchModel.BacTaiSan < 2)
			{
				var _thisRow = (sender as GroupHeaderBand);
				_thisRow.Visible = false;
			}
			else
			{
				var _thisRow = (sender as GroupHeaderBand);
				_thisRow.Visible = true;
			}
		}
		private void cap3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			TS_BCTH_02H_DK_TSNN _row = row ?? new TS_BCTH_02H_DK_TSNN();
			string Cap_3 = _row.CAP_3??"";
			if (Cap_3.Length < 4 || _baoCaoTaiSanTongHopSearchModel.BacTaiSan < 3)
			{
				var _thisRow = (sender as GroupHeaderBand);
				_thisRow.Visible = false;
			}
			else
			{
				var _thisRow = (sender as GroupHeaderBand);
				_thisRow.Visible = true;
			}
		}
		private void Cap4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			TS_BCTH_02H_DK_TSNN _row = row ?? new TS_BCTH_02H_DK_TSNN();
			string Cap_4 = _row.CAP_4??"";
			if (Cap_4.Length < 4 || _baoCaoTaiSanTongHopSearchModel.BacTaiSan < 4)
			{
				var _thisRow = (sender as GroupHeaderBand);
				_thisRow.Visible = false;
			}
			else
			{
				var _thisRow = (sender as GroupHeaderBand);
				_thisRow.Visible = true;
			}
		}
		private void Cap5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			TS_BCTH_02H_DK_TSNN _row = row ?? new TS_BCTH_02H_DK_TSNN();
			string Cap_5 = _row.CAP_5??"";
			if (Cap_5.Length < 4 || _baoCaoTaiSanTongHopSearchModel.BacTaiSan < 5)
			{
				var _thisRow = (sender as GroupHeaderBand);

				_thisRow.Visible = false;
			}
			else
			{
				var _thisRow = (sender as GroupHeaderBand);
				_thisRow.Visible = true;
			}
		}

		private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			if (_row != null)
			{
				if (_baoCaoTaiSanTongHopSearchModel.BacTaiSan < 5)
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
		private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			
		}

		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{

			lblDonVi.Text = "Bộ, ngành, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanTongHopSearchModel.TEN_BO_NGANH) + "\r\n" + "Tên đơn vị: " + _baoCaoTaiSanTongHopSearchModel.TEN_DON_VI + "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanTongHopSearchModel.MA_DON_VI;
			if (_baoCaoTaiSanTongHopSearchModel.MA_DON_VI == "018999")
			{
				ngayandbactaisan.Text = "Thời điểm báo cáo: từ " + _baoCaoTaiSanTongHopSearchModel.NgayBatDau.toDateVNString() + " đến " + _baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString() + "\r\n" + "Loại tài sản: " + ((int)_baoCaoTaiSanTongHopSearchModel.LoaiTaiSan).ToStringLoaiHinhTaiSan() + "; " + "\r\n" + " Chi tiết bậc báo cáo: " + _baoCaoTaiSanTongHopSearchModel.BacTaiSan + "; Lý do:" + _baoCaoTaiSanTongHopSearchModel.BanOrThanhLy.ToStringBanOrThanhLy() + "\r\n" + "Cấp hành chính: " + (_baoCaoTaiSanTongHopSearchModel.StringCapHanhChinh != null ? _baoCaoTaiSanTongHopSearchModel.lstNhanHienThi.Where(c => c.Key == ("List." + new enumTinhHuyenXaTrungUong().GetType().FullName)).Select(c => c.Value).FirstOrDefault() : "Tất cả");
			}
			else
			{
				ngayandbactaisan.Text = "Thời điểm báo cáo: từ " + _baoCaoTaiSanTongHopSearchModel.NgayBatDau.toDateVNString() + " đến " + _baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString() + "\r\n" + "Loại tài sản: " + ((int)_baoCaoTaiSanTongHopSearchModel.LoaiTaiSan).ToStringLoaiHinhTaiSan() + "; " + "\r\n" + " Chi tiết bậc báo cáo: " + _baoCaoTaiSanTongHopSearchModel.BacTaiSan + "; Lý do:" + _baoCaoTaiSanTongHopSearchModel.BanOrThanhLy.ToStringBanOrThanhLy() + "\r\n" + "Cấp hành chính: " + (_baoCaoTaiSanTongHopSearchModel.StringCapHanhChinh != null ? _baoCaoTaiSanTongHopSearchModel.lstNhanHienThi.Where(c => c.Key == ("List." + new enumTinhHuyenXa().GetType().FullName)).Select(c => c.Value).FirstOrDefault() : "Tất cả");
			}
			//ngayandbactaisan.Text = "Thời điểm báo cáo: từ " + _baoCaoTaiSanTongHopSearchModel.NgayBatDau.toDateVNString() + " đến " + _baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString() + "\r\n" + "Loại tài sản: " + ((int)_baoCaoTaiSanTongHopSearchModel.LoaiTaiSan).ToStringLoaiHinhTaiSan() + "; " + "\r\n" + " Chi tiết bậc báo cáo: " + _baoCaoTaiSanTongHopSearchModel.BacTaiSan + "; Lý do:" + _baoCaoTaiSanTongHopSearchModel.BanOrThanhLy.ToStringBanOrThanhLy();
			switch (_baoCaoTaiSanTongHopSearchModel.MauSo)
			{
				case 1:
					maubaocao.Text = "Phần 1: Tổng hợp chung";
					break;
				case 2:
					maubaocao.Text = "Phần 2: Chi tiết theo loại hình đơn vị";
					ngayandbactaisan.Text = ngayandbactaisan.Text + "\r\n" + "Loại hình đơn vị: " + _baoCaoTaiSanTongHopSearchModel.TenLoaiDonVi;
					break;
				case 3:
					maubaocao.Text = "Phần 3: Chi tiết theo từng đơn vị";
					break;
			}
		}

		private void TieuDe_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Giá trị là: " + _baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien() + ".";
		}
		private void taisanTen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			if (_row != null)
			{
				var _thisCell = (sender as XRTableCell);
				StringBuilder @string = new StringBuilder("   ");
				for (int level = 0; level < _row.TREE_LEVEL; level++)
					@string.Append(" ");
				_thisCell.Text = @string.ToString() + _row.TAI_SAN_TEN;
			}

		}
		private void ten1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			var _this = sender as XRTableCell;
			STT_TS_2 = 0;
			if (_row != null && _row.TEN_1 != null)
			{
				STT_TS_1 += 1;
				_this.Text = $"{STT_TS_1}. {_row.TEN_1}";
			}
		}
		private void ten2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			STT_TS_3 = 0;
			var _this = sender as XRTableCell;
			if (_row != null && _row.TEN_2 != null)
			{
				STT_TS_2 += 1;

				_this.Text = $"{STT_TS_1}.{STT_TS_2}. {_row.TEN_2}";
			}
		}
		private void ten3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			STT_TS_4 = 0;
			var _this = sender as XRTableCell;
			if (_row != null && _row.TEN_3 != null)
			{
				STT_TS_3 += 1;
				_this.Text = $"{STT_TS_1}.{STT_TS_2}.{STT_TS_3}. {_row.TEN_3}";
			}
		}
		private void ten4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			STT_TS_5 = 0;
			var _this = sender as XRTableCell;
			if (_row != null && _row.TEN_4 != null)
			{
				STT_TS_4 += 1;
				_this.Text = $"{STT_TS_1}.{STT_TS_2}.{STT_TS_3}.{STT_TS_4}. {_row.TEN_4}";

			}
		}
		private void ten5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			var _this = sender as XRTableCell;
			if (_row != null && _row.TEN_5 != null)
			{
				STT_TS_5 += 1;

				_this.Text = $"{STT_TS_1}.{STT_TS_2}.{STT_TS_3}.{STT_TS_4}.{STT_TS_5}. {_row.TEN_5}";


			}
		}
		private void BaoCao_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

		private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _rowC = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			TS_BCTH_02H_DK_TSNN _rowN = GetNextRow() as TS_BCTH_02H_DK_TSNN;
			var _thisRow = (sender as GroupFooterBand);
			_thisRow.Visible = false;
			if (_rowN == _rowC)
			{
				_thisRow.Visible = true;
			}
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
		private void tbl_dv1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			if (_row != null)
			{
				var _this = sender as XRTableCell;
				if (_row.DV_TEN_1 != null)
				{
					STT_DV_1 += 1;
					_this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}. {_row.DV_TEN_1}";
					STT_DV_2 = 0;
					STT_TS = 0;
					STT_TS_1 = 0;
					STT_TS_2 = 0;
					STT_TS_3 = 0;
					STT_TS_4 = 0;
					STT_TS_5 = 0;
				}
			}
		}

		private void tbl_dv2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			if (_row != null)
			{
				var _this = sender as XRTableCell;
				if (_row.DV_TEN_2 != null)
				{
					STT_DV_2 += 1;
					_this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}.{STT_DV_2}. {_row.DV_TEN_2}";
					STT_DV_3 = 0;
					STT_TS = 0;
					STT_TS_1 = 0;
					STT_TS_2 = 0;
					STT_TS_3 = 0;
					STT_TS_4 = 0;
					STT_TS_5 = 0;
				}
			}
		}
		private void tbl_dv3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			if (_row != null)
			{
				var _this = sender as XRTableCell;
				if (_row.DV_TEN_3 != null)
				{
					STT_DV_3 += 1;
					_this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}.{STT_DV_2}.{STT_DV_3}. {_row.DV_TEN_3}";
					STT_DV_4 = 0;
					STT_TS = 0;
					STT_TS_1 = 0;
					STT_TS_2 = 0;
					STT_TS_3 = 0;
					STT_TS_4 = 0;
					STT_TS_5 = 0;
				}
			}
		}
		private void tbl_dv4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			if (_row != null)
			{
				var _this = sender as XRTableCell;
				if (_row.DV_TEN_4 != null)
				{
					STT_DV_4 += 1;
					_this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}.{STT_DV_2}.{STT_DV_3}.{STT_DV_4}. {_row.DV_TEN_4}";
					STT_DV_5 = 0;
					STT_TS = 0;
					STT_TS_1 = 0;
					STT_TS_2 = 0;
					STT_TS_3 = 0;
					STT_TS_4 = 0;
					STT_TS_5 = 0;
				}
			}
		}
		private void tbl_dv5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			if (_row != null)
			{
				var _this = sender as XRTableCell;
				if (_row.DV_TEN_5 != null)
				{
					STT_DV_5 += 1;
					_this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}.{STT_DV_2}.{STT_DV_3}.{STT_DV_4}.{STT_DV_5}. {_row.DV_TEN_5}";
					STT_TS = 0;
					STT_TS_1 = 0;
					STT_TS_2 = 0;
					STT_TS_3 = 0;
					STT_TS_4 = 0;
					STT_TS_5 = 0;
				}
			}
		}

        private void SubBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			if (_row != null)
			{
				var _this = sender as SubBand;
				if (_baoCaoTaiSanTongHopSearchModel.BacDonVi == 1 || _baoCaoTaiSanTongHopSearchModel.MA_DON_VI != "018999")
				{
					_this.Visible = false;
				}
				else
				{
					_this.Visible = true;
				}
			}
		}
		int stt = 0;
		int stt1 = 0;
		private void tableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCTH_02H_DK_TSNN _row = GetCurrentRow() as TS_BCTH_02H_DK_TSNN;
			if (_row != null)
			{
				string _rowTen = GetCurrentColumnValue("TEN_LOAI_DON_VI") as string;
				var _this = sender as XRTableCell;
				if (_row.TEN_LOAI_DON_VI != null)
				{
					stt += 1;
				}
				_this.Text = stt.ConvertIntToRomanNumber() + ". " + _rowTen;
			}
			stt1 = 0;
		}

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}
	}
}
