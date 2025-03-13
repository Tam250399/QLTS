using System;
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCTC;
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;

namespace GS.Web.Reports.TS_BCTC
{
    public partial class rptTS_BCTC_03_DK_TSNN
    {
		private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
		private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
		private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
		private int stt = 0;
		private int stt1 = 0;
		private int stt2 = 0;
		private int stt3 = 0;
		private int stt4 = 0;
		public rptTS_BCTC_03_DK_TSNN(
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
		#region Table Cell
		private void tableCell34_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_03_DK_TSNN _row = GetCurrentRow() as TS_BCTC_03_DK_TSNN;
			if (_row == null && GetCurrentRow() != null)
				_row = GetCurrentRow().toStringJson().toEntity<TS_BCTC_03_DK_TSNN>();
			if (_row != null)
			{
				var _thisCell = (sender as XRTableCell);
				StringBuilder @string = new StringBuilder("   ");
				for (int level = 0; level < _row.TREE_LEVEL; level++)
					@string.Append(" ");
				_thisCell.Text = @string.ToString() + _row.TAI_SAN_TEN;
			}

		}
		/// <summary>
		/// Cấp nhà hoặc số chỗ ngồi, tải trọng hoặc thông số kỹ thuật
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tableCell36_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_03_DK_TSNN _row = GetCurrentRow() as TS_BCTC_03_DK_TSNN;
			if (_row == null && GetCurrentRow() != null)
				_row = GetCurrentRow().toStringJson().toEntity<TS_BCTC_03_DK_TSNN>();


			var _thisCell = (sender as XRTableCell);
			if (_row != null)
			{
				switch (_row.LOAI_HINH_TAI_SAN_ID)
				{
					case (int)enumLOAI_HINH_TAI_SAN.NHA:
						_thisCell.Text = _row.TEN_LOAI_TAI_SAN;
						break;

					case (int)enumLOAI_HINH_TAI_SAN.OTO:
					case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
						if (_row.SO_CHO > 0)
							_thisCell.Text = _row.SO_CHO.ToString() + " chỗ";
						else if (_row.TAI_TRONG > 0)
							_thisCell.Text = _row.TAI_TRONG.ToString();
						break;
					case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
					case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
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
		/// <summary>
		/// Tỷ lệ chất lượng còn lại (%)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tableCell40_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_03_DK_TSNN _row = GetCurrentRow() as TS_BCTC_03_DK_TSNN;
			if (_row == null && GetCurrentRow() != null)
				_row = GetCurrentRow().toStringJson().toEntity<TS_BCTC_03_DK_TSNN>();
			if (_row != null)
			{
				var _thisCell = (sender as XRTableCell);
				if (_row.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT && _row.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
				{
					var gtcl = _row.GIA_TRI_CON_LAI ?? 0;
					var tongNG = _row.NGUYEN_GIA_NGAN_SACH.Value + _row.NGUYEN_GIA_KHAC.Value;
					//var ng = (_row.NGUYEN_GIA == 0 || _row.NGUYEN_GIA == null) ? 1 : _row.NGUYEN_GIA.Value;
					if(tongNG != 0)
					{
						_thisCell.Text = Math.Ceiling((gtcl / tongNG) * 100).ToString();
					}
                    else
                    {
						_thisCell.Text = "0";
					}
				}
				else
				{
					_thisCell.Text = "100";
				}
			}
		}
		/// <summary>
		/// Số tầng
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tableCell37_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_03_DK_TSNN _row = GetCurrentRow() as TS_BCTC_03_DK_TSNN;
			if (_row == null && GetCurrentRow() != null)
				_row = GetCurrentRow().toStringJson().toEntity<TS_BCTC_03_DK_TSNN>();
			if (_row != null)
			{
				var _thisCell = (sender as XRTableCell);
				if (_row.SO_TANG == 0)
					_thisCell.Text = "";
				else
					_thisCell.Text = _row.SO_TANG.ToString();
			}
		}
		#endregion Table Cell
		private void tableCell45_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_03_DK_TSNN _row = GetCurrentRow() as TS_BCTC_03_DK_TSNN;
			if (_row != null)
			{
				string _rowTen = GetCurrentColumnValue("TEN_1") as string;
				var _this = sender as XRTableCell;
				if (_row.TEN_1 != null)
				{
					stt += 1;
				}
				_this.Text = stt.ToString() + ". " + _rowTen;
				stt1 = 0;
			}
		}
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
		private void tableCell56_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_03_DK_TSNN _row = GetCurrentRow() as TS_BCTC_03_DK_TSNN;
			if (_row != null)
			{
				string _rowTen = GetCurrentColumnValue("TEN_2") as string;
				var _this = sender as XRTableCell;
				if (_row.TEN_2 != null)
				{
					stt1 += 1;
				}
				_this.Text = stt.ToString() + "." + stt1.ToString() + ". " + _rowTen;
				stt2 = 0;
			}
		}
		#endregion Cấp 2

		#region Cấp 3
		private void GroupHeader4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			string Cap_3 = GetCurrentColumnValue<string>("CAP_3");
			var _thisRow = (sender as GroupHeaderBand);
			if (Cap_3 == null || Cap_3.Length < 4 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 3)
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
			TS_BCTC_03_DK_TSNN _row = GetCurrentRow() as TS_BCTC_03_DK_TSNN;
			if (_row != null)
			{
				string _rowTen = GetCurrentColumnValue("TEN_3") as string;
				var _this = sender as XRTableCell;
				if (_row.TEN_3 != null)
				{
					stt2 += 1;
				}
				_this.Text = stt.ToString() + "." + stt1.ToString() + "." + stt2.ToString() + ". " + _rowTen;
				stt3 = 0;
			}
		}
		#endregion Cấp 3

		#region Cấp 4

		private void GroupHeader5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			string Cap_4 = GetCurrentColumnValue<string>("CAP_4");
			var _thisRow = (sender as GroupHeaderBand);
			if (Cap_4 == null || Cap_4.Length < 4 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 4)
			{
				_thisRow.Visible = false;
			}
			else
			{
				_thisRow.Visible = true;
			}
		}
		private void tableCell78_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_03_DK_TSNN _row = GetCurrentRow() as TS_BCTC_03_DK_TSNN;
			if (_row != null)
			{
				string _rowTen = GetCurrentColumnValue("TEN_4") as string;
				var _this = sender as XRTableCell;
				if (_row.TEN_4 != null)
				{
					stt3 += 1;
				}
				_this.Text = stt.ToString() + "." + stt1.ToString() + "." + stt2.ToString() + "." + stt3.ToString() + ". " + _rowTen;
				stt4 = 0;
			}
		}
		#endregion Cấp 4

		#region Cấp 5
		private void GroupHeader6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			string Cap_5 = GetCurrentColumnValue<string>("CAP_5");
			var _thisRow = (sender as GroupHeaderBand);
			if (Cap_5 == null || Cap_5.Length < 4 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 5)
			{
				_thisRow.Visible = false;
			}
			else
			{
				_thisRow.Visible = true;
			}
		}
		private void tableCell89_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_03_DK_TSNN _row = GetCurrentRow() as TS_BCTC_03_DK_TSNN;
			if (_row != null)
			{
				string _rowTen = GetCurrentColumnValue("TEN_5") as string;
				var _this = sender as XRTableCell;
				if (_row.TEN_5 != null)
				{
					stt4 += 1;
				}
				_this.Text = stt.ToString() + "." + stt1.ToString() + "." + stt2.ToString() + "." + stt3.ToString() + "." + stt4.ToString() + ". " + _rowTen;				
			}
		}
		#endregion Cấp 5

		#region Cấu hình
		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			//var stringLoaiTaiSan = "";
			//var stringListLoaiTaiSan = new List<string>();
			//if (_baoCaoTaiSanChiTietSearchModel.StringLoaiTaiSan.Length > 0)
			//{
			//	foreach(var ts in _baoCaoTaiSanChiTietSearchModel.StringLoaiTaiSan.Split(',').Select(c=>c.ToNumberInt32()).ToList())
			//	{
			//		stringListLoaiTaiSan.Add(ts.ToStringLoaiHinhTaiSan());
			//	}
			//	stringLoaiTaiSan = "Loại tài sản: " + String.Join(", ", stringListLoaiTaiSan) + "\r\n";
			//}
			lblDonVi.Text = "Bộ, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanChiTietSearchModel.TEN_BO_NGANH) + "\r\n" + "Tên đơn vị: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI + "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanChiTietSearchModel.MA_DON_VI;

			//lblBoNganhTinh.Text = "Bộ, ngành, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanChiTietSearchModel.TEN_BO_NGANH);
			//lblDonVi.Text = "Tên đơn vị: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI;
			//lblMaDonVi.Text = "Mã đơn vị: " + _baoCaoTaiSanChiTietSearchModel.MA_DON_VI;
			label3.Text = "Thời điểm báo cáo: " + _baoCaoTaiSanChiTietSearchModel.NgayKetThuc.toDateVNString() + "\r\n" + "Loại tài sản: "+_baoCaoTaiSanChiTietSearchModel.StringDisplayListLoaiTaiSan + "\r\n" + "Chi tiết bậc báo cáo: " + _baoCaoTaiSanChiTietSearchModel.BacTaiSan
				+ "\r\n" + $"Số cầu xe : {_baoCaoTaiSanChiTietSearchModel.TenCauXePrint}"
				+ "\r\n" + $"Chức danh : {_baoCaoTaiSanChiTietSearchModel.TenChucDanhPrint}";
		}
		#endregion

		private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			ChucDanhNguoiLapBieu.Text = _cauHinhBaoCaoModel.ChucDanhNguoiLapBieu;
			ChucDanhKeToanTruong.Text = _cauHinhBaoCaoModel.ChucDanhKeToanTruong;
			ChucDanhThuTruongDonVi.Text = _cauHinhBaoCaoModel.ChucDanhThuTruongDonVi;
			TenNguoiLapBieu.Text = _cauHinhBaoCaoModel.TenNguoiLapBieu;
			TenKeToanTruong.Text = _cauHinhBaoCaoModel.TenKeToanTruong;
			TenThuTruongDonVi.Text = _cauHinhBaoCaoModel.TenThuTruongDonVi;
			DiaDanhBaoCao.Text = _cauHinhBaoCaoModel.DiaDanhBaoCao;
		}

		private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Diện tích đất là: " + _baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Diện tích nhà là: " + _baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Giá trị là: " + _baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien() + ".";

		}

		private void detailBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_03_DK_TSNN _row = GetCurrentRow() as TS_BCTC_03_DK_TSNN;
			if (_row == null && GetCurrentRow() != null)
				_row = GetCurrentRow().toStringJson().toEntity<TS_BCTC_03_DK_TSNN>();
			if (_row != null)
			{
				if (_row.TREE_LEVEL + 1 > _baoCaoTaiSanChiTietSearchModel.BacTaiSan)
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
			TS_BCTC_03_DK_TSNN _rowC = GetCurrentRow() as TS_BCTC_03_DK_TSNN;
			if (_rowC == null && GetCurrentRow() != null)
				_rowC = GetCurrentRow().toStringJson().toEntity<TS_BCTC_03_DK_TSNN>();
			TS_BCTC_03_DK_TSNN _rowN = GetNextRow() as TS_BCTC_03_DK_TSNN;
			if (_rowN == null && GetCurrentRow() != null)
				_rowN = GetCurrentRow().toStringJson().toEntity<TS_BCTC_03_DK_TSNN>();
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
			DiaDanhBaoCao.Text = _cauHinhBaoCaoModel.DiaDanhBaoCao + ",";
			if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhNguoiLapBieu) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenNguoiLapBieu))
				lb_GhiChuNLB.Visible = false;
			if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhKeToanTruong) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenKeToanTruong))
				lb_GhiChuKeToan.Visible = false;
			if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhThuTruongDonVi) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenThuTruongDonVi))
				lb_GhiChuThuTruong.Visible = false;
		}

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}
	}
}
