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
    public partial class rptTS_BCQH_MAU08_OTO_VSD
    {
        private readonly BaoCaoTaiSanTongHopSearchModel _baoCaoTaiSanTongHopSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
        private Int32 Stt;
        public rptTS_BCQH_MAU08_OTO_VSD(BaoCaoTaiSanTongHopSearchModel baoCaoTaiSanTongHopSearchModel,
            CauHinhBaoCaoModel cauHinhBaoCaoModel,
            CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel)
        {
            InitializeComponent();
            this._baoCaoTaiSanTongHopSearchModel = baoCaoTaiSanTongHopSearchModel;
            _baoCaoTaiSanTongHopSearchModel.NgayKetThuc = baoCaoTaiSanTongHopSearchModel.NgayKetThuc ?? baoCaoTaiSanTongHopSearchModel.NgayBaoCao;
            this._cauHinhBaoCaoModel = cauHinhBaoCaoModel;
            this._cauHinhBaoCaoChungModel = cauHinhBaoCaoChungModel;
            this.ExportOptions.Xls.FitToPrintedPageWidth = true;
            this.ExportOptions.Xlsx.FitToPrintedPageWidth = true;
            this.ExportOptions.Xls.ExportMode = _baoCaoTaiSanTongHopSearchModel.enumDinhDanhXlsExcel;
            this.ExportOptions.Xlsx.ExportMode = _baoCaoTaiSanTongHopSearchModel.enumDinhDanhXlsxExcel;
            this.Stt = 0;
        }

        private void rptTS_BCQH_MAU08_OTO_VSD_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
            label4.Text = "Đơn vị báo cáo: " + _baoCaoTaiSanTongHopSearchModel.TEN_DON_VI;
            tieude.Text = "Phụ lục VIII" + "\r\n" + "XE Ô TÔ SỬ DỤNG VƯỢT THỜI HẠN QUY ĐỊNH " + "\r\n" + "(Thời điểm báo cáo: "+_baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString()+")";
        }

        private void tableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
            tableCell2.Text = "TỔNG SỐ Ô TÔ"+"\r\n"+"HIỆN CÓ"+"\r\n"+"Đến "+_baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString();
        }

        private void tableCell17_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCQH_MAU08_OTO_VSD _thisrow = GetCurrentRow() as TS_BCQH_MAU08_OTO_VSD;
            if(_thisrow!= null)
            {
                Stt += 1;
                tableCell17.Text = Stt.ToString() + ". " + _thisrow.TEN_1;
            }
        }

        private void tableCell4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell4.Text = "XE Ô TÔ CÓ THỜI GIAN" + "\r\n" + "SỬ DỤNG TRÊN 15 NĂM" + "\r\n" + "(từ trước năm " + ((_baoCaoTaiSanTongHopSearchModel.NgayKetThuc.Value).Year - 15).ToString() + ")";
        }

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
	}
}
