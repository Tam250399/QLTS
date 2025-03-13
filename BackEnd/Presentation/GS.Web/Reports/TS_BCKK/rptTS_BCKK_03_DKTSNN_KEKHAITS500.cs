using System;
using System.Drawing;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core.Domain.BaoCaos.TS_BCKK;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;

namespace GS.Web.Reports.TS_BCKK
{
    public partial class rptTS_BCKK_03_DKTSNN_KEKHAITS500
    {
        private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
        private int Cap1;
        private int Cap2;
        private int Cap3;
        private int Cap4;
        private int Cap5;
        public rptTS_BCKK_03_DKTSNN_KEKHAITS500(
            BaoCaoTaiSanChiTietSearchModel baoCaoTaiSanChiTietSearchModel,
            CauHinhBaoCaoModel cauHinhBaoCaoModel,
            CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel
            )
        {
            InitializeComponent();
            this._baoCaoTaiSanChiTietSearchModel = baoCaoTaiSanChiTietSearchModel;
            this._cauHinhBaoCaoModel = cauHinhBaoCaoModel;
            this._cauHinhBaoCaoChungModel = cauHinhBaoCaoChungModel;
            this.Cap1 = 0;
            this.Cap2 = 0;
            this.Cap3 = 0;
            this.Cap4 = 0;
            this.Cap5 = 0;
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblDonVi.Text = "Bộ, ngành, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanChiTietSearchModel.TEN_BO_NGANH) + "\r\n" + "Tên đơn vị: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI + "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanChiTietSearchModel.MA_DON_VI + "\r\n" + "Loại hình đơn vị: ";
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
			if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhNguoiLapBieu) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenNguoiLapBieu))
				lb_GhiChuNLB.Visible = false;
			if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhKeToanTruong) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenKeToanTruong))
				lb_GhiChuKeToan.Visible = false;
			if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhThuTruongDonVi) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenThuTruongDonVi))
				lb_GhiChuThuTruong.Visible = false;
		}

        private void rptTS_BCKK_03_DKTSNN_KEKHAITS500_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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


        #region Cap1
        private void Cap1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCKK_03_DK_TSNN _row = GetCurrentRow() as TS_BCKK_03_DK_TSNN;
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
        #endregion Cap1 

        #region Cap2
        private void GroupHeader_Cap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCKK_03_DK_TSNN _row = GetCurrentRow() as TS_BCKK_03_DK_TSNN;
            string Cap_2 = GetCurrentColumnValue<string>("CAP_2");
            if (Cap_2 == null || Cap_2.Length < 4 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 2)
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

        private void tblCell_Ten2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_2 = GetCurrentColumnValue<string>("TEN_2");
            if (TEN_2 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = "   " + TEN_2;
            }
        }
        #endregion Cap2

        #region Cap3
        private void GroupHeader_Cap3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCKK_03_DK_TSNN _row = GetCurrentRow() as TS_BCKK_03_DK_TSNN;
            string Cap_3 = GetCurrentColumnValue<string>("CAP_3");
            if (Cap_3 == null || Cap_3.Length < 4 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 3)
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

        private void tblCell_Ten3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_3 = GetCurrentColumnValue<string>("TEN_3");
            if (TEN_3 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = "      " + TEN_3;
            }
        }
        #endregion Cap3

        #region Cap4
        private void GroupHeader_Cap4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCKK_03_DK_TSNN _row = GetCurrentRow() as TS_BCKK_03_DK_TSNN;
            string Cap_4 = GetCurrentColumnValue<string>("CAP_4");
            if (Cap_4 == null || Cap_4.Length < 4 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 4)
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

        private void tblCell_Ten4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_4 = GetCurrentColumnValue<string>("TEN_4");
            if (TEN_4 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = "         " + TEN_4;
            }
        }

        #endregion Cap4

        #region Cap5
        private void GroupHeader_Cap5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCKK_03_DK_TSNN _row = GetCurrentRow() as TS_BCKK_03_DK_TSNN;
            string Cap_5 = GetCurrentColumnValue<string>("CAP_5");
            if (Cap_5 == null || Cap_5.Length < 4 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 5)
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

        private void tblCell_Ten5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_5 = GetCurrentColumnValue<string>("TEN_5");
            if (TEN_5 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = "            " + TEN_5;
            }
        }

        #endregion Cap5

    }
}
