using System;
using System.Drawing;
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCQH;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanTongHop;

namespace GS.Web.Reports.TS_BCQH
{
    public partial class rptTS_BCQH_PL07_TANG_GIAM_TSNN
    {
        private readonly BaoCaoTaiSanTongHopSearchModel _baoCaoTaiSanTongHopSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
        public rptTS_BCQH_PL07_TANG_GIAM_TSNN(
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
        #region Cấp 2
        private void GroupHeader_C2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            string Cap_2 = GetCurrentColumnValue<string>("CAP_2");
            var _thisRow = (sender as GroupHeaderBand);
            if (Cap_2 == null || Cap_2.Length < 4 || _baoCaoTaiSanTongHopSearchModel.BacTaiSan < 2)
            {

                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
        }
        private void cap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_2 = GetCurrentColumnValue<string>("TEN_2");
            if (TEN_2 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = "   " + TEN_2;
            }
        }
		#endregion Cấp 2

		#region Cấp 3
		private void GroupHeader_C3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string Cap_3 = GetCurrentColumnValue<string>("CAP_3");
            var _thisRow = (sender as GroupHeaderBand);
            if (Cap_3 == null || Cap_3.Length < 4 || _baoCaoTaiSanTongHopSearchModel.BacTaiSan < 3)
            {

                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
        }
        private void cap3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_3 = GetCurrentColumnValue<string>("TEN_3");
            if (TEN_3 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = "    " + TEN_3;
            }
        }
        #endregion Cấp 3
        private void detailBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCQH_PL07_TANG_GIAM_TSNN _row = GetCurrentRow() as TS_BCQH_PL07_TANG_GIAM_TSNN;
            if (_row != null)
            {
                if (_row.TREE_LEVEL + 1 > _baoCaoTaiSanTongHopSearchModel.BacTaiSan)
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
        private void tblCell_TaiSanTen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCQH_PL07_TANG_GIAM_TSNN _row = GetCurrentRow() as TS_BCQH_PL07_TANG_GIAM_TSNN;
            if (_row != null)
            {
                var _thisCell = (sender as XRTableCell);
                StringBuilder @string = new StringBuilder("   ");
                for (int level = 0; level < _row.TREE_LEVEL; level++)
                    @string.Append(" ");
                _thisCell.Text = @string.ToString() + _row.TAI_SAN_TEN;
            }
        }

        private void rptTS_BCQH_PL07_TANG_GIAM_TSNN_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
            lbTenBaoCao.Text = "TỔNG HỢP TÌNH HÌNH TĂNG, GIẢM TÀI SẢN CÔNG NĂM " + _baoCaoTaiSanTongHopSearchModel.NgayKetThuc.Value.Year;
            lblDieuKien.Text = "(Từ ngày "+ _baoCaoTaiSanTongHopSearchModel.NgayBatDau.toDateVNString() + " đến ngày " + _baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString() + ")";
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Diện tích là: " + _baoCaoTaiSanTongHopSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Giá trị là: " + _baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien() + ".";

        }

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
	}
}
