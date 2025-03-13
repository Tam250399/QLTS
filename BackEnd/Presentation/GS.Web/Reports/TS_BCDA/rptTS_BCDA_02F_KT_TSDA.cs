using System;
using System.Drawing;
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCDA;
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanTongHop;

namespace GS.Web.Reports.TS_BCDA
{
    public partial class rptTS_BCDA_02F_KT_TSDA
    {
        private readonly BaoCaoTaiSanTongHopSearchModel _baoCaoTaiSanTongHopSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
        public rptTS_BCDA_02F_KT_TSDA(BaoCaoTaiSanTongHopSearchModel baoCaoTaiSanTongHopSearchModel,
            CauHinhBaoCaoModel cauHinhBaoCaoModel,
            CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel)
        {
            InitializeComponent();
            this._baoCaoTaiSanTongHopSearchModel = baoCaoTaiSanTongHopSearchModel;
            this._cauHinhBaoCaoModel = cauHinhBaoCaoModel;
            this._cauHinhBaoCaoChungModel = cauHinhBaoCaoChungModel;
            this.ExportOptions.Xls.FitToPrintedPageWidth = true;
            this.ExportOptions.Xlsx.FitToPrintedPageWidth = true;
            this.ExportOptions.Xls.ExportMode = _baoCaoTaiSanTongHopSearchModel.enumDinhDanhXlsExcel;
            this.ExportOptions.Xlsx.ExportMode = _baoCaoTaiSanTongHopSearchModel.enumDinhDanhXlsxExcel;
        }

        private void loaitsduan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var _row = (sender as GroupHeaderBand);
            if (_baoCaoTaiSanTongHopSearchModel.MauSo == 3)
            {
                _row.Visible = true;
            }
            else
            {
                _row.Visible = false;
            }
        }

        private void dv_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var _row = (sender as GroupHeaderBand);
            if (_baoCaoTaiSanTongHopSearchModel.MauSo == 2)
            {
                _row.Visible = true;
            }
            else
            {
                _row.Visible = false;
            }
        }

        private void duan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var _row = (sender as GroupHeaderBand);
            if (_baoCaoTaiSanTongHopSearchModel.MauSo == 2)
            {
                _row.Visible = true;
            }
            else
            {
                _row.Visible = false;
            }
        }

        private void phanloai_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void cap1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCDA_02F_KT_TSDA _thisrow = GetCurrentRow() as TS_BCDA_02F_KT_TSDA;
            var _row = (sender as GroupHeaderBand);
            if (_thisrow != null && (_thisrow.phan_loai_tai_san == (int)enumLOAI_HINH_TAI_SAN_BAO_CAO.TREN_500 || _thisrow.phan_loai_tai_san == (int)enumLOAI_HINH_TAI_SAN_BAO_CAO.DUOI_500) && _thisrow.CAP_1 > 0 && _baoCaoTaiSanTongHopSearchModel.BacTaiSan > 1)
            {
                _row.Visible = true;
            }
            else
            {
                _row.Visible = false;
            }
        }

        private void cap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCDA_02F_KT_TSDA _thisrow = GetCurrentRow() as TS_BCDA_02F_KT_TSDA;
            var _row = (sender as GroupHeaderBand);
            if (_thisrow != null && _thisrow.CAP_2.Length > 4 && _baoCaoTaiSanTongHopSearchModel.BacTaiSan >= 2)
            {
                _row.Visible = true;
            }
            else
            {
                _row.Visible = false;
            }
        }

        private void cap3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCDA_02F_KT_TSDA _thisrow = GetCurrentRow() as TS_BCDA_02F_KT_TSDA;
            var _row = (sender as GroupHeaderBand);
            if (_thisrow != null && _thisrow.CAP_3.Length > 4 && _baoCaoTaiSanTongHopSearchModel.BacTaiSan >= 3)
            {
                _row.Visible = true;
            }
            else
            {
                _row.Visible = false;
            }
        }

        private void cap4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            TS_BCDA_02F_KT_TSDA _thisrow = GetCurrentRow() as TS_BCDA_02F_KT_TSDA;
            var _row = (sender as GroupHeaderBand);
            if (_thisrow != null && _thisrow.CAP_4.Length > 4 && _baoCaoTaiSanTongHopSearchModel.BacTaiSan >= 4)
            {
                _row.Visible = true;
            }
            else
            {
                _row.Visible = false;
            }
        }

        private void cap5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCDA_02F_KT_TSDA _thisrow = GetCurrentRow() as TS_BCDA_02F_KT_TSDA;
            var _row = (sender as GroupHeaderBand);
            if (_thisrow != null && _thisrow.CAP_5.Length > 4 && _baoCaoTaiSanTongHopSearchModel.BacTaiSan >= 5)
            {
                _row.Visible = true;
            }
            else
            {
                _row.Visible = false;
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            TS_BCDA_02F_KT_TSDA _thisrow = GetCurrentRow() as TS_BCDA_02F_KT_TSDA;
            var _row = (sender as DetailBand);
            if (_thisrow != null && _thisrow.tree_level + 1 > _baoCaoTaiSanTongHopSearchModel.BacTaiSan)
            {
                _row.Visible = false;
            }
            else
            {
                _row.Visible = true;
            }
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ChucDanhNguoiLapBieu.Text = _cauHinhBaoCaoModel.ChucDanhNguoiLapBieu;
            ChucDanhThuTruongDonVi.Text = _cauHinhBaoCaoModel.ChucDanhThuTruongDonVi;
            TenNguoiLapBieu.Text = _cauHinhBaoCaoModel.TenNguoiLapBieu;
            TenThuTruongDonVi.Text = _cauHinhBaoCaoModel.TenThuTruongDonVi;
            DiaDanhBaoCao.Text = _cauHinhBaoCaoModel.DiaDanhBaoCao;
            if (String.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhNguoiLapBieu))
                lb_GhiChuNLB.Visible = false;
            if (String.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhThuTruongDonVi))
                lb_GhiChuThuTruong.Visible = false;
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Giá trị là: " + _baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien() + ".";
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblDonVi.Text = "Bộ, ngành, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanTongHopSearchModel.TEN_BO_NGANH) + "\r\n" + "Tên đơn vị: " + _baoCaoTaiSanTongHopSearchModel.TEN_DON_VI + "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanTongHopSearchModel.MA_DON_VI;
            ngayandbactaisan.Text = "Ngày báo cáo: " + _baoCaoTaiSanTongHopSearchModel.NgayBatDau.toDateVNString() + " đến ngày " + _baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString() + "\r\n" + " Chi tiết bậc báo cáo: " + _baoCaoTaiSanTongHopSearchModel.BacTaiSan;
            if (_baoCaoTaiSanTongHopSearchModel.MauSo == 1)
            {
                maubaocao.Text = "(Báo cáo theo tài sản)";
                mauso.Text = "Mẫu số: 02F-1-KT/TSDA";
            }
            else if(_baoCaoTaiSanTongHopSearchModel.MauSo == 2)
            {
                maubaocao.Text = "(Báo cáo theo đơn vị)";
                mauso.Text = "Mẫu số: 02F-2-KT/TSDA";
            }
            else
            {
                maubaocao.Text = "(Báo cáo theo loại tài sản dự án)";
                mauso.Text = "Mẫu số: 02F-3-KT/TSDA";
            }
        }

        private void rptTS_BCDA_02F_KT_TSDA_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
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

        private void tendv_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void tenduan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void tenphanloai_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCDA_02F_KT_TSDA _thisrow = GetCurrentRow() as TS_BCDA_02F_KT_TSDA;
            if (_thisrow != null)
            {
                tenphanloai.Text = ((int)_thisrow.phan_loai_tai_san).ToLoaiTaiSanBaoCao();
            }
        }

        private void ten1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_1 = GetCurrentColumnValue<string>("TEN_1");
            if (TEN_1 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = " " + TEN_1;
            }
        }

        private void ten2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_2 = GetCurrentColumnValue<string>("TEN_2");
            if (TEN_2 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = "  " + TEN_2;
            }
        }

        private void ten3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_3 = GetCurrentColumnValue<string>("TEN_3");
            if (TEN_3 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = "    " + TEN_3;
            }
        }

        private void ten4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_4 = GetCurrentColumnValue<string>("TEN_4");
            if (TEN_4 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = "      " + TEN_4;
            }
        }

        private void ten5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_5 = GetCurrentColumnValue<string>("TEN_5");
            if (TEN_5 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = "        " + TEN_5;
            }
        }

        private void tentaisan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCDA_02F_KT_TSDA _row = GetCurrentRow() as TS_BCDA_02F_KT_TSDA;
            if (_row != null)
            {
                var _thisCell = (sender as XRTableCell);
                StringBuilder @string = new StringBuilder("   ");
                for (int level = 0; level < _row.tree_level; level++)
                    @string.Append(" ");
                _thisCell.Text = @string.ToString() + _row.tai_san_ten;
            }
        }
		private int indexDonVi = 0;
		private void tableCell37_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			tableCell37.Text = (++indexDonVi).ConvertIntToRomanNumber().ToString();
		}

		
	}
}
