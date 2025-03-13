using System;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;
using GS.Core.Domain.BaoCaos.TS_BCCK;
using System.Drawing;
using GS.Core;

namespace GS.Web.Reports.TS_BCCK
{
    public partial class rptTS_BCCK_9dd_CK_TSC_KHAITHAC_TC
    {
        private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
        public rptTS_BCCK_9dd_CK_TSC_KHAITHAC_TC(
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
            lblDonVi.Text = "Bộ, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanChiTietSearchModel.TEN_BO_NGANH) + "\r\n" + "Cơ quan quản lý cấp trên: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI_CAP_TREN + "\r\n" + "Cơ quan, tổ chức, đơn vị sử dụng tài sản: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI + "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanChiTietSearchModel.MA_DON_VI + "\r\n" + "Loại hình đơn vị: " + _baoCaoTaiSanChiTietSearchModel.TenLoaiHinhDonVi;
            lblDieuKienBaoCao.Text = "NĂM " + _baoCaoTaiSanChiTietSearchModel.NamBaoCao;
        }
        int STT = 0;
        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_09DD_CK_TSC _rowC = GetCurrentRow() as TS_BCCK_09DD_CK_TSC;
            TS_BCCK_09DD_CK_TSC _rowN = GetNextRow() as TS_BCCK_09DD_CK_TSC;
            //var _thisRow = (sender as GroupFooterBand);
            //_thisRow.Visible = false;
            //if (_rowN == _rowC)
            //{
            //    _thisRow.Visible = true;
            //}
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

        private void rptTS_BCCK_9dd_CK_TSC_KHAITHAC_TC_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void tableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_09DD_CK_TSC _row = GetCurrentRow() as TS_BCCK_09DD_CK_TSC;
            var _this = sender as XRTableCell;
            if (_row != null)
            {
                STT += 1;
                _this.Text = CommonExtention.ConvertIntToRomanNumber(STT);
            }
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Diện tích đất là: " + _baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Diện tích nhà là: " + _baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Giá trị là: " + _baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien() + ".";
        }

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
	}
}
