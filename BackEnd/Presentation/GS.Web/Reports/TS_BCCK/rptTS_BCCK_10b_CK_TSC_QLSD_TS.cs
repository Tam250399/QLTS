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
    public partial class rptTS_BCCK_10b_CK_TSC_QLSD_TS
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
		private int LTS2;
		private int LTS3;
		private int LTS4;
		private int LTS5;
		int STT = 0;
		int STT_1 = 0;
		int STT_2 = 0;
		int STT_3 = 0;
		int STT_4 = 0;
		int STT_5 = 0;
		public rptTS_BCCK_10b_CK_TSC_QLSD_TS(
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
			this.LTS2 = 0;
			this.LTS3 = 0;
			this.LTS4 = 0;
			this.LTS5 = 0;


		}
		private void GroupHeader_TieuDe_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Diện tích đất là: " + _baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Diện tích nhà là: " + _baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Giá trị là: " + _baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien() + ".";
		}
		#region phân cấp loại tài sản
		private void GroupHeader_LTaiSanCap1__BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
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
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
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
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
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
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
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
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
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
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
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

		}
		//private void TenLoaiHinh_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;

		//	if (_row != null && _row.CAP_1 != null)
		//	{
		//		if (_row.TREE_LEVEL >= 1)
		//		{
		//			LTS2 = 0;
		//			LTS3 = 0;
		//			LTS4 = 0;
		//			LTS5 = 0;
		//		}
		//		sttLoaiHinhTaiSan += 1;
		//		var _thisCell = (sender as XRTableCell);
		//		_thisCell.Text = sttLoaiHinhTaiSan.ToString() + ". " + _row.TEN_1;
		//	}
		//}
		//private void ten2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
		//	if (_row != null && _row.CAP_2 != null)
		//	{
		//		if (_row.TREE_LEVEL >= 2)
		//		{
		//			LTS3 = 0;
		//			LTS4 = 0;
		//			LTS5 = 0;
		//		}
		//		LTS2 += 1;
		//		var _thisCell = (sender as XRTableCell);
		//		_thisCell.Text = sttLoaiHinhTaiSan.ToString() + "." + LTS2.ToString() + ". " + _row.TEN_2;
		//	}
		//}
		//private void ten3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
		//	if (_row != null && _row.CAP_3 != null)
		//	{
		//		if (_row.TREE_LEVEL >= 3)
		//		{
		//			LTS4 = 0;
		//			LTS5 = 0;
		//		}
		//		LTS3 += 1;
		//		var _thisCell = (sender as XRTableCell);
		//		_thisCell.Text = sttLoaiHinhTaiSan.ToString() + "." + LTS2.ToString()+ "." + LTS3.ToString() + ". " + _row.TEN_3;
		//	}
		//}
		//private void ten4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
		//	if (_row != null && _row.CAP_4 != null)
		//	{
		//		if (_row.TREE_LEVEL >= 4)
		//		{
		//			LTS5 = 0;
		//		}
		//		LTS4 += 1;
		//		var _thisCell = (sender as XRTableCell);
		//		_thisCell.Text = sttLoaiHinhTaiSan.ToString() + "." + LTS2.ToString() + "." + LTS3.ToString() + "." + LTS4.ToString() + ". " + _row.TEN_4;
		//	}
		//}
		//private void ten5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
		//	if (_row != null && _row.CAP_5 != null)
		//	{
		//		LTS5 += 1;
		//		var _thisCell = (sender as XRTableCell);
		//		_thisCell.Text = sttLoaiHinhTaiSan.ToString() + "." + LTS2.ToString() + "." + LTS3.ToString() + "." + LTS4.ToString() + "." + LTS5.ToString() + ". " + _row.TEN_5;
		//	}
		//}
		private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
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
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
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
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
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
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
			if (_row == null || _row.DV_3 == null ||  _baoCaoTaiSanChiTietSearchModel.BacDonVi < 3)
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
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
			if (_row == null || _row.DV_4 == null ||  _baoCaoTaiSanChiTietSearchModel.BacDonVi < 4)
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
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
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
		//	TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
		//	string _rowTen = GetCurrentColumnValue("DV_TEN_1") as string;
		//	var _this = sender as XRTableCell;
		//	if (_row != null && _row.TREE_LEVEL_DV >= 1)
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
		//	TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
		//	string _rowTen = GetCurrentColumnValue("DV_TEN_2") as string;
		//	var _this = sender as XRTableCell;
		//	if (_row.TREE_LEVEL_DV >= 2)
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
		//	TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
		//	string _rowTen = GetCurrentColumnValue("DV_TEN_3") as string;
		//	var _this = sender as XRTableCell;
		//	if (_row.TREE_LEVEL_DV >= 3)
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
		//	TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
		//	string _rowTen = GetCurrentColumnValue("DV_TEN_4") as string;
		//	var _this = sender as XRTableCell;
		//	if (_row.TREE_LEVEL_DV >= 4)
		//	{
		//		Cap5 = 0;
		//	}
		//	if (_row.DV_4 != null)
		//		Cap4 += 1;
		//	_this.Text = Cap1.ToString() + "." + Cap2.ToString() + "." + Cap3.ToString() + "." + Cap4.ToString() + ". " + _rowTen;
		//}
		//private void tbl_dv5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
		//	string _rowTen = GetCurrentColumnValue("DV_TEN_5") as string;
		//	var _this = sender as XRTableCell;
		//	if (_row.DV_5 != null)
		//		Cap5 += 1;
		//	_this.Text = Cap1.ToString() + "." + Cap2.ToString() + "." + Cap3.ToString() + "." + Cap4.ToString() + "." + Cap5.ToString() + ". " + _rowTen;
		//}
		#endregion phân cấp đơn vị
		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblDonVi.Text = "Bộ, ngành, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanChiTietSearchModel.TEN_BO_NGANH) +  "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanChiTietSearchModel.MA_DON_VI ;
            lblDieuKienBaoCao.Text = $"NĂM {_baoCaoTaiSanChiTietSearchModel.NamBaoCao} \r\n {_baoCaoTaiSanChiTietSearchModel.StringLoaiTaiSan.ToStringLoaiHinhTaiSanFromString()}"; 
        }
        private void rptTS_BCCK_10b_CK_TSC_QLSD_TS_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
			if (_row == null) return;
			var _this = sender as XRTableCell;
			if (_row.TREE_LEVEL_DV >= 1)
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
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
			if (_row == null) return;
			var _this = sender as XRTableCell;
			if (_row.TREE_LEVEL_DV >= 2)
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
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
			if (_row == null) return;
			var _this = sender as XRTableCell;
			if (_row.TREE_LEVEL_DV >= 3)
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
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
			if (_row == null) return;
			var _this = sender as XRTableCell;
			if (_row.TREE_LEVEL_DV >= 4)
			{
				Cap5 = 0;
			}
			if (_row.DV_4 != null)
				Cap4 += 1;
			_this.Text = Cap1.ConvertIntToRomanNumber() + "." + Cap2.ToString() + "." + Cap3.ToString() + "." + Cap4.ToString();
		}
		private void sttdv5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
			if (_row == null) return;
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

        private void LoaiHinhTaiSan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
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

        private void TenLoaiHinhTaiSan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
			var _this = sender as XRTableCell;
			if (_row != null)
			{

				_this.Text = _baoCaoTaiSanChiTietSearchModel.lstNhanHienThi.Where(c => c.Key == (new enumNHAN_HIEN_THI_LOAI_HINH_TS().GetType().FullName) + "." + ((enumNHAN_HIEN_THI_LOAI_HINH_TS)_row.LOAI_HINH_TAI_SAN_ID).ToString()).Select(c => c.Value).FirstOrDefault();
			}
		}
		
		

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
			if(_baoCaoTaiSanChiTietSearchModel.CURRENT_CAP_DON_VI == 0 || _baoCaoTaiSanChiTietSearchModel.CURRENT_CAP_DON_VI == 1)
            {
				tableRow6.Visible = true;
				tableRow20.Visible = true;
				tableRow21.Visible = true;
				tableRow22.Visible = true;
			}
            else
            {
				tableRow6.Visible = false;
				tableRow20.Visible = false;
				tableRow21.Visible = false;
				tableRow22.Visible = false;
			}
		}
		private void SttLoaiHinhTaiSan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
			var _this = sender as XRTableCell;
			if (_row != null)
			{
				STT_1 += 1;
				_this.Text = STT_1.ToString()/* CommonExtention.ConvertIntToRomanNumber(STT)*/;
				STT_2 = 0;
			}
		}
		private void tableCell89_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
			var _this = sender as XRTableCell;
			if (_row != null)
			{
				STT_2 += 1;
				_this.Text = $"{STT_1}.{STT_2}";
				STT_3 = 0;
			}
		}

        private void tableCell34_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
			var _this = sender as XRTableCell;
			if (_row != null)
			{
				STT_3 += 1;
				_this.Text = $"{STT_1}.{STT_2}.{STT_3}";
				STT_4 = 0;
			}
		}

        private void tableCell49_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
			var _this = sender as XRTableCell;
			if (_row != null)
			{
				STT_4 += 1;
				_this.Text = $"{STT_1}.{STT_2}.{STT_3}.{STT_4}";
				STT_5 = 0;
			}
		}

        private void tableCell23_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
			var _this = sender as XRTableCell;
			if (_row != null)
			{
				STT_5 += 1;
				_this.Text = $"{STT_1}.{STT_2}.{STT_3}.{STT_4}.{STT_5}";
			}
		}

        private void tableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCCK_10B_CK_TSC _row = GetCurrentRow() as TS_BCCK_10B_CK_TSC;
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
