using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCCK;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;

namespace GS.Web.Reports.TS_BCCK
{
    public partial class rptTS_BCCK_9b_CK_TSC_QL_SD_TRU_SO
    {
		private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
		private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
		private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;

		public rptTS_BCCK_9b_CK_TSC_QL_SD_TRU_SO(
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
		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDonVi.Text = "Bộ, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanChiTietSearchModel.TEN_BO_NGANH) + "\r\n" + "Cơ quan quản lý cấp trên: "+_baoCaoTaiSanChiTietSearchModel.TEN_DON_VI_CAP_TREN + "\r\n" + "Cơ quan, tổ chức, đơn vị sử dụng tài sản: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI + "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanChiTietSearchModel.MA_DON_VI + "\r\n" + "Loại hình đơn vị: " + _baoCaoTaiSanChiTietSearchModel.TenLoaiHinhDonVi;
			lblDieuKienBaoCao.Text = "NĂM " + _baoCaoTaiSanChiTietSearchModel.NamBaoCao;
		}
      

        private void rptTS_BCCK_9b_CK_TSC_QL_SD_TRU_SO_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
        }

        private void GroupFooter1_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_09B_CK_TSC _rowC = GetCurrentRow() as TS_BCCK_09B_CK_TSC;
            TS_BCCK_09B_CK_TSC _rowN = GetNextRow() as TS_BCCK_09B_CK_TSC;
            var _thisRow = (sender as GroupFooterBand);
            _thisRow.Visible = false;
            if (_rowN == _rowC)
            {
                _thisRow.Visible = true;
            }
            ChucDanhNguoiLapBieu.Text = _cauHinhBaoCaoModel.ChucDanhNguoiLapBieu;
            ChucDanhThuTruongDonVi.Text = _cauHinhBaoCaoModel.ChucDanhThuTruongDonVi;
            TenNguoiLapBieu.Text = _cauHinhBaoCaoModel.TenNguoiLapBieu;
            TenThuTruongDonVi.Text = _cauHinhBaoCaoModel.TenThuTruongDonVi;
            DiaDanhBaoCao.Text = _cauHinhBaoCaoModel.DiaDanhBaoCao;

			if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhNguoiLapBieu) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenNguoiLapBieu))
				lb_GhiChuNLB.Visible = false;
			if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhThuTruongDonVi) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenThuTruongDonVi))
				lb_GhiChuThuTruong.Visible = false;
		}

		private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Diện tích đất là: " + _baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Diện tích nhà là: " + _baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Giá trị là: " + _baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien() + ".";
        }

		private void tableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			//TS_BCCK_09B_CK_TSC _rowC = GetCurrentRow() as TS_BCCK_09B_CK_TSC;
			var _currentCell = (sender as XRTableCell);
			if (_currentCell!= null)
			{
				var TAI_SAN_DAT_ID = GetCurrentColumnValue<decimal?>("TAI_SAN_DAT_ID");
				if (TAI_SAN_DAT_ID > 0)
				{
					_currentCell.Tag = TAI_SAN_DAT_ID;
					//_currentCell.Text = GetCurrentColumnValue<string>("DIA_CHI");
				}
				else
				{
					var nha_diaChi = GetCurrentColumnValue<string>("NHA_DIA_CHI");
					_currentCell.Tag = nha_diaChi;
					//_currentCell.Text = nha_diaChi;
				}
			}
			
		}

		private void cellDiaChiDat_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			var _currentCell = (sender as XRTableCell);
			if (_currentCell != null)
			{
				var TAI_SAN_DAT_ID = GetCurrentColumnValue<decimal?>("TAI_SAN_DAT_ID");
				if (TAI_SAN_DAT_ID > 0)
				{
					_currentCell.Tag = TAI_SAN_DAT_ID;
					_currentCell.Text = GetCurrentColumnValue<string>("DIA_CHI");
				}
				else
				{
					var nha_diaChi = GetCurrentColumnValue<string>("NHA_DIA_CHI");
					_currentCell.Tag = nha_diaChi;
					_currentCell.Text = nha_diaChi;
				}
			}
		}

		private void sumDTDat_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			//if (DataSource != null)
			//{
			//	var list = DataSource as List<TS_BCCK_09B_CK_TSC>;
			//	if (list != null && list.Count > 0)
			//	{
			//		Dictionary<decimal?, decimal?> keyValuePairs = new Dictionary<decimal?, decimal?>().
			//	}
			//}

		}

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}
	}
}
