using System;
using System.Drawing;
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCCT;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;

namespace GS.Web.Reports.TS_BCCT
{
    public partial class rptTS_BCCT_1B
    {
        private readonly BaoCaoTaiSanChiTietSearchModel  _baoCaoTaiSanChiTietSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;

        public rptTS_BCCT_1B(
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

        private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int Cap_1 = GetCurrentColumnValue<int>("CAP_1");
            if (Cap_1 == 0)
            {
                var _thisRow = (sender as GroupHeaderBand);            
                _thisRow.Visible = false;
            }
        }
        private void GroupHeader3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int Cap_2 = GetCurrentColumnValue<int>("CAP_2");
            var _thisRow = (sender as GroupHeaderBand);
            if (Cap_2 == 0 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 2)
            {
                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
        }
        private void GroupHeader4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int Cap_3 = GetCurrentColumnValue<int>("CAP_3");
            var _thisRow = (sender as GroupHeaderBand);
            if (Cap_3 == 0 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan <  3)
            {
                _thisRow.Visible = false;
            }
            else
            {              
                _thisRow.Visible = true;
            }
        }
        private void GroupHeader5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int Cap_4 = GetCurrentColumnValue<int>("CAP_4");
            var _thisRow = (sender as GroupHeaderBand);
            if (Cap_4 == 0 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 4)
            {
                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
        }
        private void GroupHeader6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int Cap_5 = GetCurrentColumnValue<int>("CAP_5");
            var _thisRow = (sender as GroupHeaderBand);
            if (Cap_5 == 0 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 5)
            {
                
                
                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
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

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblDonVi.Text = "Bộ, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanChiTietSearchModel.TEN_BO_NGANH) + "\r\n" + "Tên đơn vị: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI + "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanChiTietSearchModel.MA_DON_VI;
            label3.Text = "Thời điểm báo cáo: " + _baoCaoTaiSanChiTietSearchModel.NgayKetThuc.toDateVNString() + "\r\n" + _baoCaoTaiSanChiTietSearchModel.StringLoaiTaiSan.ToStringLoaiHinhTaiSanFromString() + "\r\n" + "Chi tiết bậc báo cáo: " + _baoCaoTaiSanChiTietSearchModel.BacTaiSan;
        }
        private void rptTS_BCCT_1B_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        private void tableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCT_1B _row = GetCurrentRow() as TS_BCCT_1B;
            if (_row != null)
            {
                var _thisCell = (sender as XRTableCell);
                StringBuilder @string = new StringBuilder("   ");
                for (int level = 0; level < _row.TREE_LEVEL; level++)
                    @string.Append(" ");
                _thisCell.Text = @string.ToString() + _row.TAI_SAN_TEN;
            }

        }

        private void tableCell50_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_2 = GetCurrentColumnValue<string>("TEN_2");
            var _thisCell = (sender as XRTableCell);
            _thisCell.Text = "  " + TEN_2;
        }

        private void tableCell62_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_3 = GetCurrentColumnValue<string>("TEN_3");
            var _thisCell = (sender as XRTableCell);
            _thisCell.Text = "    " + TEN_3;
        }

        private void tableCell74_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_4 = GetCurrentColumnValue<string>("TEN_4");
            var _thisCell = (sender as XRTableCell);
            _thisCell.Text = "      " + TEN_4;
        }

        private void tableCell86_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_5 = GetCurrentColumnValue<string>("TEN_5");
            var _thisCell = (sender as XRTableCell);
            _thisCell.Text = "        " + TEN_5;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCT_1B _row = GetCurrentRow() as TS_BCCT_1B;
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
            //TS_BCCT_1B _rowC = GetCurrentRow() as TS_BCCT_1B;
            //TS_BCCT_1B _rowN = GetNextRow() as TS_BCCT_1B;
            //var _thisRow = (sender as GroupFooterBand);
            //_thisRow.Visible = false;
            //if (_rowN == _rowC)
            //{
            //    _thisRow.Visible = true;
            //}
           
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            label1.Text = "ĐVT cho: Diện tích đất là: " + _baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Diện tích nhà là: " + _baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich() + ";";
        }

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
	}
}   
