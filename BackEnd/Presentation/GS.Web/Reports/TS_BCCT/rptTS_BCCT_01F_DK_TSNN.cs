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
    public partial class rptTS_BCCT_01F_DK_TSNN
    {
        private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;

        public rptTS_BCCT_01F_DK_TSNN(
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

        private void rptTS_BCCT_01F_DK_TSNN_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblDonVi.Text = "Bộ, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanChiTietSearchModel.TEN_BO_NGANH) + "\r\n" + "Tên đơn vị: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI + "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanChiTietSearchModel.MA_DON_VI;
            lb_DieuKienBaoCao.Text = "Năm báo cáo: " + _baoCaoTaiSanChiTietSearchModel.NamBaoCao+ "\r\n" + _baoCaoTaiSanChiTietSearchModel.StringLoaiTaiSan.ToStringLoaiHinhTaiSanFromString() + "\r\n" + "Chi tiết bậc báo cáo: " + _baoCaoTaiSanChiTietSearchModel.BacTaiSan;
        }
        #region CauHinh
        private void GroupHeader_TieuDeBang_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        #endregion

        #region Cap1
        #endregion

        #region Cap2
        private void GroupHeader_Cap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void tblCell_Ten2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_2 = GetCurrentColumnValue<string>("TEN_2");
            if (TEN_2 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = "   " + TEN_2;
            }
        }
        #endregion

        #region Cap3
        private void GroupHeader_Cap3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var _thisRow = (sender as GroupHeaderBand);
            string Cap_3 = GetCurrentColumnValue<string>("CAP_3");
            if (Cap_3 == null || Cap_3.Length < 4 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 3)
            {
              
                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
        }

        private void tblCell_Ten3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_3 = GetCurrentColumnValue<string>("TEN_3");
            if (TEN_3 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = "      " + TEN_3;
            }
        }
        #endregion

        #region Cap4
        private void GroupHeader_Cap4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var _thisRow = (sender as GroupHeaderBand);
            string Cap_4 = GetCurrentColumnValue<string>("CAP_4");
            if (Cap_4 == null || Cap_4.Length < 4 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 4)
            {
              
                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
        }

        private void tblCell_Ten4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_4 = GetCurrentColumnValue<string>("TEN_4");
            if (TEN_4 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = "         " + TEN_4;
            }
        }

        #endregion

        #region Cap5
        private void GroupHeader_Cap5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var _thisRow = (sender as GroupHeaderBand);
            string Cap_5 = GetCurrentColumnValue<string>("CAP_5");
            if (Cap_5 == null || Cap_5.Length < 4 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 5)
            {
              
                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
        }

        private void tblCell_Ten5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_5 = GetCurrentColumnValue<string>("TEN_5");
            if (TEN_5 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = "            " + TEN_5;
            }
        }
        #endregion

        #region detail, tai san
        private void detailBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCT_01F_DK_TSNN _row = GetCurrentRow() as TS_BCCT_01F_DK_TSNN;
            if (_row != null)
            {
                var _thisRow = (sender as DetailBand);
                if (_row.TREE_LEVEL + 1  > _baoCaoTaiSanChiTietSearchModel.BacTaiSan)
                {
                  
                    _thisRow.Visible = false;
                }
                else
                {
                    _thisRow.Visible = true;
                }
            }
        }
        private void tblCell_TenTaiSan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCT_01F_DK_TSNN _row = GetCurrentRow() as TS_BCCT_01F_DK_TSNN;
            if (_row != null)
            {
                var _thisCell = (sender as XRTableCell);
                StringBuilder @string = new StringBuilder("   ");
                for (int level = 0; level < _row.TREE_LEVEL; level++)
                    @string.Append(" ");
                _thisCell.Text = @string.ToString() + _row.TAI_SAN_TEN;
            }
        }

        #endregion

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //TS_BCCT_01F_DK_TSNN _rowC = GetCurrentRow() as TS_BCCT_01F_DK_TSNN;
            //TS_BCCT_01F_DK_TSNN _rowN = GetNextRow() as TS_BCCT_01F_DK_TSNN;
            //var _thisRow = (sender as GroupFooterBand);
            //_thisRow.Visible = false;
            //if (_rowN == _rowC)
            //{
            //    _thisRow.Visible = true;
            //}
            //ChucDanhNguoiLapBieu.Text = _cauHinhBaoCaoModel.ChucDanhNguoiLapBieu;
            //ChucDanhKeToanTruong.Text = _cauHinhBaoCaoModel.ChucDanhKeToanTruong;
            //ChucDanhThuTruongDonVi.Text = _cauHinhBaoCaoModel.ChucDanhThuTruongDonVi;
            //TenNguoiLapBieu.Text = _cauHinhBaoCaoModel.TenNguoiLapBieu;
            //TenKeToanTruong.Text = _cauHinhBaoCaoModel.TenKeToanTruong;
            //TenThuTruongDonVi.Text = _cauHinhBaoCaoModel.TenThuTruongDonVi;
            //DiaDanhBaoCao.Text = _cauHinhBaoCaoModel.DiaDanhBaoCao;
            //if (String.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhNguoiLapBieu))
            //    lb_GhiChuNLB.Visible = false;
            //if (String.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhKeToanTruong))
            //    lb_GhiChuKeToan.Visible = false;
            //if (String.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhThuTruongDonVi))
            //    lb_GhiChuThuTruong.Visible = false;
        }

        private void tableCell35_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCT_01F_DK_TSNN _row = GetCurrentRow() as TS_BCCT_01F_DK_TSNN;
            if(_row!=null && _row.TONG_NGUYEN_GIA == _row.LUY_KE)
            {
                var _this = sender as XRTableCell;
                _this.Text = "Tài sản đã hao mòn hết";
            }
        }

        private void ReportFooter_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lb_DonVi.Text = "ĐVT cho cột giá trị là: " + _baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien() + ".";
        }

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
	}
}
