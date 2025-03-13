using System;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanTongHop;

namespace GS.Web.Reports.TS_BCQH
{
    public partial class rptTS_BCQH_PL012_TANG_GIAM_NHOM_DV
    {
        private readonly BaoCaoTaiSanTongHopSearchModel _baoCaoTaiSanTongHopSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly Models.BaoCaos.CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
        private Int32 Stt;
        public rptTS_BCQH_PL012_TANG_GIAM_NHOM_DV(
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
            this.Stt = 0;
        }

		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDieuKien.Text = "(Từ ngày " + "\r\n" + _baoCaoTaiSanTongHopSearchModel.NgayBatDau.toDateVNString() + "\r\n" + "đến ngày" + _baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString() + ")";
        }

		private void tableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            tableCell15.Text = "SỐ ĐẦU KỲ" + "\r\n" + "( " + (_baoCaoTaiSanTongHopSearchModel.NgayBatDau.Value).toDateVNString() + ")";
        }

		private void tableCell24_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            tableCell24.Text = "SỐ CUỐI KỲ" + "\r\n" +"( " + (_baoCaoTaiSanTongHopSearchModel.NgayKetThuc.Value).toDateVNString() + ")";
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Diện tích là: " + _baoCaoTaiSanTongHopSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Giá trị là: " + _baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien() + ".";
        }

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
	}
}
