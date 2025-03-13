using System;
using System.Drawing;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_PXNTT;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanTongHop;

namespace GS.Web.Reports.TS_PXNTT
{
    public partial class rptTS_PXNTT_MAU_01
    {
        private readonly BaoCaoTaiSanTongHopSearchModel _baoCaoTaiSanTongHopSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
        public rptTS_PXNTT_MAU_01(
            BaoCaoTaiSanTongHopSearchModel baoCaoTaiSanTongHopSearchModel,
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

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_PXNTT_MAU_01 _row = GetCurrentRow() as TS_PXNTT_MAU_01;
            if (_row == null || (_row.LOAI_HINH_TAI_SAN_ID == 2 && _row.TAI_SAN_DAT_ID != 0) )
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
        private void GroupHeader8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_PXNTT_MAU_01 _row = GetCurrentRow() as TS_PXNTT_MAU_01;
            if (_row == null || _row.LOAI_HINH_TAI_SAN_ID == 1)
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

        private void detailDat_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_PXNTT_MAU_01 _row = GetCurrentRow() as TS_PXNTT_MAU_01;
            var _this = sender as XRTable;
            _this.LocationF = new System.Drawing.PointF(0, 0);
            if (_row == null || _row.LOAI_HINH_TAI_SAN_ID == 2)
            {
                _this.Visible = false;
            }
            else
            {
                _this.Visible = true;
            }
        }

        private void detailNha_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_PXNTT_MAU_01 _row = GetCurrentRow() as TS_PXNTT_MAU_01;
            var _this = sender as XRTable;
            _this.LocationF = new System.Drawing.PointF(0, 0);
            if (_row == null || _row.LOAI_HINH_TAI_SAN_ID == 1)
            {

                _this.Visible = false;               
            }
            else
            {
                _this.Visible = true;

            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_PXNTT_MAU_01 _row = GetCurrentRow() as TS_PXNTT_MAU_01;
            if (_row == null || _row.NAM == _row.CUOI_KY || _row.NAM == _row.DAU_KY)
            {
                var _this = sender as DetailBand;
                _this.Visible = false;
            }
            else
            {
                var _this = sender as DetailBand;
                _this.Visible = true;
            }
        }
        private void FooterNha_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_PXNTT_MAU_01 _row = GetCurrentRow() as TS_PXNTT_MAU_01;
            if (_row == null || _row.LOAI_HINH_TAI_SAN_ID == 1)
            {
                var _this = sender as GroupFooterBand;
                _this.Visible = false;
            }
            else
            {
                var _this = sender as GroupFooterBand;
                _this.Visible = true;
            }
        }

        private void FooterDat_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_PXNTT_MAU_01 _row = GetCurrentRow() as TS_PXNTT_MAU_01;
            if (_row == null || _row.LOAI_HINH_TAI_SAN_ID == 2)
            {
                var _this = sender as GroupFooterBand;
                _this.Visible = false;
            }
            else
            {
                var _this = sender as GroupFooterBand;
                _this.Visible = true;
            }
        }

        private void rptTS_PXNTT_MAU_01_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
            //report.ExportOptions.Xls.FitToPrintedPageWidth = true;
            //report.ExportOptions.Xlsx.FitToPrintedPageWidth = true;
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

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblDonVi.Text = "Bộ, ngành, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanTongHopSearchModel.TEN_BO_NGANH) + "\r\n" + "Tên đơn vị: " + _baoCaoTaiSanTongHopSearchModel.TEN_DON_VI + "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanTongHopSearchModel.MA_DON_VI;
            ngayandbactaisan.Text = "Loại tài sản: Đất, nhà" + "\r\n"+ "Kỳ xác nhận: Từ ngày " + _baoCaoTaiSanTongHopSearchModel.NgayBatDau.toDateVNString()+ " đến ngày " + _baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString();
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ChucDanhNguoiLapBieu.Text = _cauHinhBaoCaoModel.ChucDanhNguoiLapBieu;
            ChucDanhThuTruongDonVi.Text = _cauHinhBaoCaoModel.ChucDanhThuTruongDonVi;
            TenNguoiLapBieu.Text = _cauHinhBaoCaoModel.TenNguoiLapBieu;
            TenThuTruongDonVi.Text = _cauHinhBaoCaoModel.TenThuTruongDonVi;
            DiaDanhBaoCao.Text = _cauHinhBaoCaoModel.DiaDanhBaoCao;
        }
    }
}
