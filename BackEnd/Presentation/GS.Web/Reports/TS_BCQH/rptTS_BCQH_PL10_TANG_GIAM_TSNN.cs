using System;
using System.Drawing;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCQH;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanTongHop;

namespace GS.Web.Reports.TS_BCQH
{
    public partial class rptTS_BCQH_PL10_TANG_GIAM_TSNN
    {
        private readonly BaoCaoTaiSanTongHopSearchModel _baoCaoTaiSanTongHopSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
        public rptTS_BCQH_PL10_TANG_GIAM_TSNN(
            BaoCaoTaiSanTongHopSearchModel baoCaoTaiSanTongHopSearchModel,
            CauHinhBaoCaoModel cauHinhBaoCaoModel,
            CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel
            )
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

		private void rptTS_BCQH_PL10_TANG_GIAM_TSNN_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
        }

		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            label2.Text = "Đơn vị báo cáo: " + _baoCaoTaiSanTongHopSearchModel.TEN_DON_VI;
            lblTieuDe.Text = "TỔNG HỢP TÌNH HÌNH TĂNG, GIẢM TÀI SẢN CÔNG " /*+ (_baoCaoTaiSanTongHopSearchModel.NgayBatDau.Value.Year).ToString()*/;
            lblDieuKien.Text = "(Từ ngày " + _baoCaoTaiSanTongHopSearchModel.NgayBatDau.toDateVNString() + " đến ngày " + _baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString() + ")";
        }

		private void tableCell58_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            TS_BCQH_PL10_TANG_GIAM_TSNN _row = GetCurrentRow() as TS_BCQH_PL10_TANG_GIAM_TSNN;
            if (_row != null)
            {
                var _thisRow = (sender as XRTableCell);
                switch (_row.LOAI_CAP_DON_VI_ID)
                {
                    case 1:
                        _thisRow.Text = "Trung ương";
                        break;
                    case 2:
                        _thisRow.Text = "Địa phương";
                        break;
                    default:
                        _thisRow.Text = "";
                        break;

                }
            }
        }
        private void tableCell84_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCQH_PL10_TANG_GIAM_TSNN _row = GetCurrentRow() as TS_BCQH_PL10_TANG_GIAM_TSNN;
            if (_row != null)
            {
                var _thisRow = (sender as XRTableCell);
                switch (_row.CHUONG)
                {
                    case 1:
                        _thisRow.Text = "I. Tài sản cố định tại các cơ quan, tổ chức, đơn vị, Ban Quản lý dự án";
                        break;
                    case 2:
                        _thisRow.Text = "III. Tài sản được xác lập quyền sở hữu toàn dân";
                        break;
                    default:
                        _thisRow.Text = "";
                        break;

                }
            }
        }
        private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Diện tích là: " + _baoCaoTaiSanTongHopSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Giá trị là: " + _baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien() + ".";
        }

        private void GroupHeader2_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Diện tích là: " + _baoCaoTaiSanTongHopSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Giá trị là: " + _baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien() + ".";
        }

        private void tableCell71_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            TS_BCQH_PL10_TANG_GIAM_TSNN _row = GetCurrentRow() as TS_BCQH_PL10_TANG_GIAM_TSNN;
            if (_row != null)
            {
                var _thisRow = (sender as XRTableCell);

                switch (_row.CAP_1)
                {
                    case 1:
                        _thisRow.Text = "1- Đất";
                        break;
                    case 2:
                        _thisRow.Text = "2- Nhà";
                        break;
                    case 4:
                        _thisRow.Text = "3- Ô tô";
                        break;
                    case 499:
                        _thisRow.Text = "4- Tài sản cố định khác";
                        break;
                }           
                
            }
        }

        private void tableCell97_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCQH_PL10_TANG_GIAM_TSNN _row = GetCurrentRow() as TS_BCQH_PL10_TANG_GIAM_TSNN;
            if (_row != null)
            {
                var _thisRow = (sender as XRTableCell);
                switch (_row.CHUONG)
                {
                    case 1:
                        _thisRow.Text = "Tổng (I)";
                        break;
                    case 2:
                        _thisRow.Text = "Tổng (III)";
                        break;
                    default:
                        _thisRow.Text = "";
                        break;

                }
            }
        }

		private void lblDieuKien_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
	}
}
