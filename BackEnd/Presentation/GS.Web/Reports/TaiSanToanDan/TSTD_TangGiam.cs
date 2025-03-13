using System;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TSTD;
using GS.Core.Domain.SHTD;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;

namespace GS.Web.Reports.TaiSanToanDan
{
    public partial class TSTD_TangGiam
    {
        private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
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
		public TSTD_TangGiam(BaoCaoTaiSanChiTietSearchModel baoCaoTaiSanChiTietSearchModel,
            CauHinhBaoCaoModel cauHinhBaoCaoModel,
            CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel)
        {
            InitializeComponent();
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
		#region Cấu hình
		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDonVi.Text = "CƠ QUAN QUẢN LÝ CẤP TRÊN: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI_CAP_TREN.ToUpper() + "\r\n" + "CƠ QUAN CHỦ TRÌ QUẢN LÝ TÀI SẢN: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI.ToUpper();

			label3.Text = "Kỳ báo cáo: Từ ngày " + _baoCaoTaiSanChiTietSearchModel.NgayBatDau.toDateVNString() + " đến ngày " + _baoCaoTaiSanChiTietSearchModel.NgayKetThuc.toDateVNString() + " \r\n Nguồn gốc tài sản: "+_baoCaoTaiSanChiTietSearchModel.TenTaiSan + "; Chi tiết bậc báo cáo: " + _baoCaoTaiSanChiTietSearchModel.BacNguonGocTSTD;
            if (_baoCaoTaiSanChiTietSearchModel.MauSo == 1)
            {
				if(_baoCaoTaiSanChiTietSearchModel.MA_DON_VI == "018999")
                {
					label3.Text = label3.Text + "\r\n" + "Cấp hành chính: " + (_baoCaoTaiSanChiTietSearchModel.StringCapHanhChinh != null ? _baoCaoTaiSanChiTietSearchModel.lstNhanHienThi.Where(c => c.Key == ("List." + new enumTinhHuyenXaTrungUong().GetType().FullName)).Select(c => c.Value).FirstOrDefault() : "Tất cả");
				}
                else
                {
					label3.Text = label3.Text + "\r\n" + "Cấp hành chính: " + (_baoCaoTaiSanChiTietSearchModel.StringCapHanhChinh != null ? _baoCaoTaiSanChiTietSearchModel.lstNhanHienThi.Where(c => c.Key == ("List." + new enumTinhHuyenXa().GetType().FullName)).Select(c => c.Value).FirstOrDefault() : "Tất cả");
				}
            }
            else
            {
				label3.Text = label3.Text + "\r\n" + "Bậc đơn vị: " + _baoCaoTaiSanChiTietSearchModel.BacDonVi;
			}			
		}
		private void lbl_PHAN_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			var _this = sender as XRLabel;
			switch (_baoCaoTaiSanChiTietSearchModel.MauSo)
			{
				case 1:
					_this.Text = "Phần 1: Tổng hợp chung";
					
					break;
				case 2:
					_this.Text = "Phần 2: Chi tiết theo từng đơn vị";
				
					break;
				default :					
					break;
			}
		}
		#endregion
		private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
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
		private void taisanTen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			if (_row != null)
			{
				var _thisCell = (sender as XRTableCell);
				StringBuilder @string = new StringBuilder("   ");
				for (int level = 0; level < _row.TREE_LEVEL; level++)
					@string.Append(" ");
				_thisCell.Text = @string.ToString() + _row.TAI_SAN_TEN;
			}

		}

		private void NguonCap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			if (_row == null || _row.CAP_2 == null || _baoCaoTaiSanChiTietSearchModel.BacNguonGocTSTD < 2)
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
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			if (_row == null || _row.CAP_3 == null || _baoCaoTaiSanChiTietSearchModel.BacNguonGocTSTD < 3)
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
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			if (_row == null || _row.CAP_4 == null || _baoCaoTaiSanChiTietSearchModel.BacNguonGocTSTD < 4)
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
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			if (_row == null || _row.CAP_5 == null || _baoCaoTaiSanChiTietSearchModel.BacNguonGocTSTD < 5)
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
		private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			chuthich.Text = "ĐVT giá trị là: " + _baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien() + ".";
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

		private void stt_ng_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
		
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			STT_TS_2 = 0;
			var _this = sender as XRTableCell;
			if (_row != null && _row.TEN_1 != null)
			{
				STT_TS_1 += 1;

				_this.Text = $"{STT_TS_1}";
			}
		}

		private void stt2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			STT_TS_3 = 0;
			var _this = sender as XRTableCell;
			if (_row != null && _row.TEN_2 != null)
			{
				STT_TS_2 += 1;

				_this.Text = $"{STT_TS_1}.{STT_TS_2}";
			}
		}

		private void stt3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			STT_TS_4 = 0;
			var _this = sender as XRTableCell;
			if (_row != null && _row.TEN_3 != null)
			{
				STT_TS_3 += 1;

				_this.Text = $"{STT_TS_1}.{STT_TS_2}.{STT_TS_3}";
			}
		}

		private void stt4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			STT_TS_5 = 0;
			var _this = sender as XRTableCell;
			if (_row != null && _row.TEN_3 != null)
			{
				STT_TS_4 += 1;

				_this.Text = $"{STT_TS_1}.{STT_TS_2}.{STT_TS_3}.{STT_TS_4}";
			}
		}

		private void stt5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			var _this = sender as XRTableCell;
			if (_row != null && _row.TEN_3 != null)
			{
				STT_TS_5 += 1;

				_this.Text = $"{STT_TS_1}.{STT_TS_2}.{STT_TS_3}.{STT_TS_4}.{STT_TS_5}";
			}
		}

        private void CapDonVi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			var _this = sender as GroupHeaderBand;
			if (_row == null || _baoCaoTaiSanChiTietSearchModel.MauSo != 1)
			{
				
				_this.Visible = false;
			}
			else
			{
				_this.Visible = true;
			}
		}

        private void STT_CapDonVi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			STT_TS_1 = 0;
			var _this = sender as XRTableCell;
			if (_row != null)
			{
				STT_TS += 1;

				_this.Text = $"{STT_TS.ConvertIntToRomanNumber()}";
			}
		}
		private void tbl_dv1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			if (_row != null)
			{
				var _this = sender as XRTableCell;

					STT_DV_1 += 1;
					_this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}";
					STT_DV_2 = 0;
					STT_TS = 0;
					STT_TS_1 = 0;
					STT_TS_2 = 0;
					STT_TS_3 = 0;
					STT_TS_4 = 0;
					STT_TS_5 = 0;
				
			}
		}

		private void tbl_dv2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			if (_row != null)
			{
				var _this = sender as XRTableCell;
				
				
					STT_DV_2 += 1;
					_this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}.{STT_DV_2}";
					STT_DV_3 = 0;
					STT_TS = 0;
					STT_TS_1 = 0;
					STT_TS_2 = 0;
					STT_TS_3 = 0;
					STT_TS_4 = 0;
					STT_TS_5 = 0;
				
			}
		}
		private void tbl_dv3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			if (_row != null)
			{
				var _this = sender as XRTableCell;
				if (_row.DV_TEN_3 != null)
				{
					STT_DV_3 += 1;
					_this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}.{STT_DV_2}.{STT_DV_3}";
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
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			if (_row != null)
			{
				var _this = sender as XRTableCell;
				if (_row.DV_TEN_4 != null)
				{
					STT_DV_4 += 1;
					_this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}.{STT_DV_2}.{STT_DV_3}.{STT_DV_4}";
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
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			if (_row != null)
			{
				var _this = sender as XRTableCell;
				if (_row.DV_TEN_5 != null)
				{
					STT_DV_5 += 1;
					_this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}.{STT_DV_2}.{STT_DV_3}.{STT_DV_4}.{STT_DV_5}";
					STT_TS = 0;
					STT_TS_1 = 0;
					STT_TS_2 = 0;
					STT_TS_3 = 0;
					STT_TS_4 = 0;
					STT_TS_5 = 0;
				}
			}
		}

        private void GroupHeader6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void DV1_GroupBand_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			if (_row == null || _row.DV_1 == null || _baoCaoTaiSanChiTietSearchModel.MauSo != 2 )
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
		private void DV2_GroupBand_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			if (_row == null || _row.DV_2 == null || _baoCaoTaiSanChiTietSearchModel.MauSo != 2 || _baoCaoTaiSanChiTietSearchModel.BacDonVi < 2)
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
		private void DV3_GroupBand_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			if (_row == null || _row.DV_3 == null || _baoCaoTaiSanChiTietSearchModel.MauSo != 2 || _baoCaoTaiSanChiTietSearchModel.BacDonVi < 3)
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
		private void DV4_GroupBand_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			if (_row == null || _row.DV_4 == null || _baoCaoTaiSanChiTietSearchModel.MauSo != 2 || _baoCaoTaiSanChiTietSearchModel.BacDonVi < 4)
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
		private void DV5_GroupBand_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			if (_row == null || _row.DV_5 == null || _baoCaoTaiSanChiTietSearchModel.MauSo != 2 || _baoCaoTaiSanChiTietSearchModel.BacDonVi < 5)
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

        private void TenCapDonVi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			var _this = sender as XRTableCell;
			if (_row != null)
			{
                if (_row.CAP_DON_VI == 0)
                {
					_this.Text = "Trung ương";
                }
                else
                {
					_this.Text = "Địa phương";
				}
			}
			
		}

        private void tableCell156_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
			if (_row != null)
			{
				var _this = sender as XRTableCell;
				var TenLoaiHinh = _baoCaoTaiSanChiTietSearchModel.lstNhanHienThi.Where(c => c.Key == (new enumNHAN_HIEN_THI_LOAI_HINH_TSTD().GetType().FullName) + "." + ((enumNHAN_HIEN_THI_LOAI_HINH_TSTD)_row.LOAI_HINH_TAI_SAN_ID).ToString()).Select(c => c.Value).FirstOrDefault();
				_this.Text = $"{TenLoaiHinh}";
			}
		}

        private void GroupHeader7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TSTD_MAU_02_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_02_BC_XLSHTD;
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

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}
	}
}
