using System;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCCK;
using GS.Core.Domain.TaiSans;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;
using GS.Web.Models.BaoCaos.TaiSanTongHop;

namespace GS.Web.Reports.TS_BCCK
{
    public partial class rptTS_BCCK_10d_CK_TSC_KTNL_TS
    {
        private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
        private int Cap1;
        private int Cap2;
        private int Cap3;
        private int Cap4;
        private int Cap5;
		private int sttLoaiHinhTaiSan;
		int STT_1 = 0;
		int STT_2 = 0;
		int STT_3 = 0;
		int STT_4 = 0;
		int STT_5 = 0;
		public rptTS_BCCK_10d_CK_TSC_KTNL_TS(
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
            this.Cap1 = 0;
            this.Cap2 = 0;
            this.Cap3 = 0;
            this.Cap4 = 0;
            this.Cap5 = 0;
			this.sttLoaiHinhTaiSan = 0;

		}
		#region phân cấp loại tài sản
		private void LoaiHinhTaiSan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
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
		private void GroupHeader_LTaiSanCap1__BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			var _thisRow = (sender as GroupHeaderBand);
			if (_row != null && (_row.CAP_1 == 0 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 2))
			{
				_thisRow.Visible = false;
			}
			else
			{
				_thisRow.Visible = true;
			}
		}
		private void GroupHeader_LTaiSanCap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			if (_row != null && (_row.CAP_2 == "0" || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 3))
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
		private void GroupHeader_LTaiSanCap3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			if (_row != null && (_row.CAP_3 == "0" || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 4))
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
		private void GroupHeader_LTaiSanCap4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			if (_row != null && (_row.CAP_4 == "0" || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 5))
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
		private void GroupHeader_LTaiSanCap5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			if (_row != null && (_row.CAP_5 == "0" || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 5))
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
		//------------------------------------------
		private void taisanTen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			if (_row != null)
			{
				var _thisCell = (sender as XRTableCell);
				StringBuilder @string = new StringBuilder("   ");
				for (int level = 0; level < _row.TREE_LEVEL; level++)
					@string.Append(" ");
				_thisCell.Text = @string.ToString() + _row.TAI_SAN_TEN;
			}

		}
		//private void ten1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	string TEN_1 = GetCurrentColumnValue<string>("TEN_1");
		//	if (TEN_1 != null)
		//	{
		//		var _thisCell = (sender as XRTableCell);
		//		_thisCell.Text = " " + TEN_1;
		//	}
		//}
		//private void ten2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	string TEN_2 = GetCurrentColumnValue<string>("TEN_2");
		//	if (TEN_2 != null)
		//	{
		//		var _thisCell = (sender as XRTableCell);
		//		_thisCell.Text = "  " + TEN_2;
		//	}
		//}
		//private void ten3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	string TEN_3 = GetCurrentColumnValue<string>("TEN_3");
		//	if (TEN_3 != null)
		//	{
		//		var _thisCell = (sender as XRTableCell);
		//		_thisCell.Text = "    " + TEN_3;
		//	}
		//}
		//private void ten4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	string TEN_4 = GetCurrentColumnValue<string>("TEN_4");
		//	if (TEN_4 != null)
		//	{
		//		var _thisCell = (sender as XRTableCell);
		//		_thisCell.Text = "      " + TEN_4;
		//	}
		//}
		//private void ten5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	string TEN_5 = GetCurrentColumnValue<string>("TEN_5");
		//	if (TEN_5 != null)
		//	{
		//		var _thisCell = (sender as XRTableCell);
		//		_thisCell.Text = "        " + TEN_5;
		//	}
		//}
		private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			if (_row != null)
			{
				if (_baoCaoTaiSanChiTietSearchModel.BacTaiSan < 5)
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
		#endregion phân cấp tài sản
		#region phân cấp đơn vị
		private void GroupHeader_dv1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			if (_row == null)
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = false;
			}
			else
			{
				var _thisRow = (sender as GroupHeaderBand);
				_thisRow.Visible = true;
			}
			STT_1 = 0;
		}
		private void GroupHeader_dv2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			if (_row == null || _row.DV_2 == null || _baoCaoTaiSanChiTietSearchModel.BacDonVi < 2)
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = false;
			}
			else
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = true;
			}
			STT_1 = 0;
		}
		private void GroupHeader_dv3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			if (_row == null || _row.DV_3 == null || _baoCaoTaiSanChiTietSearchModel.BacDonVi < 3)
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = false;
			}
			else
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = true;
			}
			STT_1 = 0;
		}
		private void GroupHeader_dv4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			if (_row == null || _row.DV_4 == null || _baoCaoTaiSanChiTietSearchModel.BacDonVi < 4)
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = false;
			}
			else
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = true;
			}
			STT_1 = 0;
		}
		private void GroupHeader_dv5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			if (_row == null || _row.DV_5 == null || _baoCaoTaiSanChiTietSearchModel.BacDonVi < 5)
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = false;
				sttLoaiHinhTaiSan = 0;
			}
			else
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = true;
			}
			STT_1 = 0;
		}
		//private void tbl_dv1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
		//	string _rowTen = GetCurrentColumnValue("DV_TEN_1") as string;
		//	var _this = sender as XRTableCell;
		//	if (_row.tree_level_dv >= 1)
		//	{
		//		Cap2 = 0;
		//		Cap3 = 0;
		//		Cap4 = 0;
		//		Cap5 = 0;
		//	}
		//	Cap1 += 1;
		//	_this.Text = Cap1.ToString() + ". " + _rowTen;
		//}

		//private void tbl_dv2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
		//	string _rowTen = GetCurrentColumnValue("DV_TEN_2") as string;
		//	var _this = sender as XRTableCell;
		//	if (_row.tree_level_dv >= 2)
		//	{
		//		Cap3 = 0;
		//		Cap4 = 0;
		//		Cap5 = 0;
		//	}
		//	if (_row.DV_2 != null)
		//		Cap2 += 1;
		//	_this.Text = Cap1.ToString() + "." + Cap2.ToString() + ". " + _rowTen;
		//}
		//private void tbl_dv3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
		//	string _rowTen = GetCurrentColumnValue("DV_TEN_3") as string;
		//	var _this = sender as XRTableCell;
		//	if (_row.tree_level_dv >= 3)
		//	{
		//		Cap4 = 0;
		//		Cap5 = 0;
		//	}
		//	if (_row.DV_3 != null)
		//		Cap3 += 1;
		//	_this.Text = Cap1.ToString() + "." + Cap2.ToString() + "." + Cap3.ToString() + ". " + _rowTen;
		//}
		//private void tbl_dv4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
		//	string _rowTen = GetCurrentColumnValue("DV_TEN_4") as string;
		//	var _this = sender as XRTableCell;
		//	if (_row.tree_level_dv >= 4)
		//	{
		//		Cap5 = 0;
		//	}
		//	if (_row.DV_4 != null)
		//		Cap4 += 1;
		//	_this.Text = Cap1.ToString() + "." + Cap2.ToString() + "." + Cap3.ToString() + "." + Cap4.ToString() + ". " + _rowTen;
		//}
		//private void tbl_dv5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
		//	string _rowTen = GetCurrentColumnValue("DV_TEN_5") as string;
		//	var _this = sender as XRTableCell;
		//	if (_row.DV_5 != null)
		//		Cap5 += 1;
		//	_this.Text = Cap1.ToString() + "." + Cap2.ToString() + "." + Cap3.ToString() + "." + Cap4.ToString() + "." + Cap5.ToString() + ". " + _rowTen;
		//}
		#endregion phân cấp đơn vị
		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDonVi.Text = "Bộ, ngành, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanChiTietSearchModel.TEN_BO_NGANH) + "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanChiTietSearchModel.MA_DON_VI /*+ "\r\n" + "Loại hình đơn vị: " + _baoCaoTaiSanChiTietSearchModel.TenLoaiHinhDonVi*/;
			lblDieuKienBaoCao.Text = $"NĂM {_baoCaoTaiSanChiTietSearchModel.NamBaoCao} \r\n {_baoCaoTaiSanChiTietSearchModel.StringLoaiTaiSan.ToStringLoaiHinhTaiSanFromString()}";
		}
		private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Diện tích đất là: " + _baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Diện tích nhà là: " + _baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Giá trị là: " + _baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien() + ".";
		}
		private void rptTS_BCCK_10d_CK_TSC_KTNL_TS_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
			//    Margins.Left = Convert.ToInt32(_cauHinhBaoCaoModel.MarginLeft * 0.3937 * 100);
			//}
			//if (_cauHinhBaoCaoModel.MarginRight > 0)
			//{
			//    Margins.Right = Convert.ToInt32(_cauHinhBaoCaoModel.MarginRight * 0.3937 * 100);
			//}
		}

		private void sttdv1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			var _this = sender as XRTableCell;
			if (_row.tree_level_dv >= 1)
			{
				Cap2 = 0;
				Cap3 = 0;
				Cap4 = 0;
				Cap5 = 0;
			}
			Cap1 += 1;
			_this.Text = Cap1.ConvertIntToRomanNumber();
		}
		private void sttdv2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			var _this = sender as XRTableCell;
			if (_row.tree_level_dv >= 2)
			{
				Cap3 = 0;
				Cap4 = 0;
				Cap5 = 0;
			}
			if (_row.DV_2 != null)
				Cap2 += 1;
			_this.Text = Cap1.ConvertIntToRomanNumber() + "." + Cap2.ToString();
		}
		private void sttdv3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			var _this = sender as XRTableCell;
			if (_row.tree_level_dv >= 3)
			{
				Cap4 = 0;
				Cap5 = 0;
			}
			if (_row.DV_3 != null)
				Cap3 += 1;
			_this.Text = Cap1.ConvertIntToRomanNumber() + "." + Cap2.ToString() + "." + Cap3.ToString();
		}
		private void sttdv4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			var _this = sender as XRTableCell;
			if (_row.tree_level_dv >= 4)
			{
				Cap5 = 0;
			}
			if (_row.DV_4 != null)
				Cap4 += 1;
			_this.Text = Cap1.ConvertIntToRomanNumber() + "." + Cap2.ToString() + "." + Cap3.ToString() + "." + Cap4.ToString();
		}
		private void sttdv5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			var _this = sender as XRTableCell;
			if (_row.DV_5 != null)
				Cap5 += 1;
			_this.Text = Cap1.ConvertIntToRomanNumber() + "." + Cap2.ToString() + "." + Cap3.ToString() + "." + Cap4.ToString() + "." + Cap5.ToString();
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

        private void tableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			var _this = sender as XRTableCell;
			if (_row != null)
			{

				_this.Text = _baoCaoTaiSanChiTietSearchModel.lstNhanHienThi.Where(c => c.Key == (new enumNHAN_HIEN_THI_LOAI_HINH_TS().GetType().FullName) + "." + ((enumNHAN_HIEN_THI_LOAI_HINH_TS)_row.LOAI_HINH_TAI_SAN_ID).ToString()).Select(c => c.Value).FirstOrDefault();
			}
		}

        private void tableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			var _this = sender as XRTableCell;
			if (_row != null)
			{
				STT_1 += 1;
				_this.Text = STT_1.ToString()/* CommonExtention.ConvertIntToRomanNumber(STT)*/;
				STT_2 = 0;
			}
		}

		private void sttTen1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			var _this = sender as XRTableCell;
			if (_row != null)
			{
				STT_2 += 1;
				_this.Text = $"{STT_1}.{STT_2}";
				STT_3 = 0;
			}
		}

		private void tableCell76_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			var _this = sender as XRTableCell;
			if (_row != null)
			{
				STT_3 += 1;
				_this.Text = $"{STT_1}.{STT_2}.{STT_3}";
				STT_4 = 0;
			}
		}
		private void tableCell62_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			var _this = sender as XRTableCell;
			if (_row != null)
			{
				STT_4 += 1;
				_this.Text = $"{STT_1}.{STT_2}.{STT_3}.{STT_4}";
				STT_5 = 0;
			}
		}

        private void tableCell34_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			var _this = sender as XRTableCell;
			if (_row != null)
			{
				STT_5 += 1;
				_this.Text = $"{STT_1}.{STT_2}.{STT_3}.{STT_4}.{STT_5}";
			}
		}

        private void tableCell48_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCCK_10D_CK_TSC _row = GetCurrentRow() as TS_BCCK_10D_CK_TSC;
			var _this = sender as XRTableCell;
			if (_row != null)
			{
				STT_5 += 1;
				_this.Text = $"{STT_1}.{STT_2}.{STT_3}.{STT_4}.{STT_5}";
			}
		}

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}
	}
}
