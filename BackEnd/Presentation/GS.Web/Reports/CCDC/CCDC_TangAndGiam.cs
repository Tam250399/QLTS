using System;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.CCDC;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;

namespace GS.Web.Reports.CCDC
{
    public partial class CCDC_TangAndGiam
    {
        private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
        public CCDC_TangAndGiam(BaoCaoTaiSanChiTietSearchModel baoCaoTaiSanChiTietSearchModel,
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

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            LeftHeader.Text = "Cơ quan quản lý cấp trên: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI_CAP_TREN) + "\r\n" + "Tên đơn vị: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI + "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanChiTietSearchModel.MA_DON_VI;
            CenterHeader.Text = "BẢNG KÊ TĂNG, GIẢM CÔNG CỤ, DỤNG CỤ ";
            ngayANDbophan.Text = "Kỳ báo cáo từ ngày: " + _baoCaoTaiSanChiTietSearchModel.NgayBatDau.toDateVNString() + " đến ngày " + _baoCaoTaiSanChiTietSearchModel.NgayKetThuc.toDateVNString() + " \r\n " + "Lý do tăng, giảm: " + _baoCaoTaiSanChiTietSearchModel.StringLyDoTangGiam; /*+ "Phòng ban, bộ phận sử dụng: " + _donvibophan*/;
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
        //private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    TangGiamCongCu _rowC = GetCurrentRow() as TangGiamCongCu;
        //    TangGiamCongCu _rowN = GetNextRow() as TangGiamCongCu;
        //    var _thisRow = (sender as GroupFooterBand);
        //    _thisRow.Visible = false;
        //    if (_rowN == _rowC)
        //    {
        //        _thisRow.Visible = true;
        //    }
        //    ChucDanhNguoiLapBieu.Text = _cauHinhBaoCaoModel.ChucDanhNguoiLapBieu;
        //    ChucDanhKeToanTruong.Text = _cauHinhBaoCaoModel.ChucDanhKeToanTruong;
        //    ChucDanhThuTruongDonVi.Text = _cauHinhBaoCaoModel.ChucDanhThuTruongDonVi;
        //    TenNguoiLapBieu.Text = _cauHinhBaoCaoModel.TenNguoiLapBieu;
        //    TenKeToanTruong.Text = _cauHinhBaoCaoModel.TenKeToanTruong;
        //    TenThuTruongDonVi.Text = _cauHinhBaoCaoModel.TenThuTruongDonVi;
        //    DiaDanhBaoCao.Text = _cauHinhBaoCaoModel.DiaDanhBaoCao;
        //}
        private void CCDC_KiemKes_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Margins.Top = Convert.ToInt32(_cauHinhBaoCaoModel.MarginTop * 0.3937 * 100);
            //Margins.Bottom = Convert.ToInt32(_cauHinhBaoCaoModel.MarginBottom * 0.3937 * 100);
            //Margins.Left = Convert.ToInt32(_cauHinhBaoCaoModel.MarginLeft * 0.3937 * 100);
            //Margins.Right = Convert.ToInt32(_cauHinhBaoCaoModel.MarginRight * 0.3937 * 100);
        }

        private void tableCell24_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (_baoCaoTaiSanChiTietSearchModel.LyDoID == 2)
            {
                tableCell24.Text = "Ngày giảm";
            }
            else if (_baoCaoTaiSanChiTietSearchModel.LyDoID == 1)
            {
                tableCell24.Text = "Ngày tăng";
            }
            else
            {
                tableCell24.Text = "Ngày tăng/giảm";
            }
        }
        private void table_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TangGiamCongCu row = GetCurrentRow() as TangGiamCongCu;
            if(row == null)
            {
                var table = (sender as XRTable);
                table.Visible = false;
            }
            else
            {
                var table = (sender as XRTable);
                table.Visible = true;
            }
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            chuthich.Text = "ĐVT cho giá trị: " + _baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien();
        }

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
	}
}
