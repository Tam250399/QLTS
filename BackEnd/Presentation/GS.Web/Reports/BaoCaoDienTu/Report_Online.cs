using System;
using DevExpress.XtraReports.UI;
using GS.Web.Models.BaoCaoDienTu;
using GS.Web.Models.BaoCaos;

namespace GS.Web.Reports.Online
{
    public partial class Report_Online
    {
        private readonly BaoCaoDienTuSearchModel _baoCaoDienTuSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
        public Report_Online(
            BaoCaoDienTuSearchModel baoCaoDienTuSearchModel,
            CauHinhBaoCaoModel cauHinhBaoCaoModel,
            CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel
            )
        {
            InitializeComponent();
            this._baoCaoDienTuSearchModel = baoCaoDienTuSearchModel;
            this._cauHinhBaoCaoModel = cauHinhBaoCaoModel;
            this._cauHinhBaoCaoChungModel = cauHinhBaoCaoChungModel;
            this.ExportOptions.Xls.FitToPrintedPageWidth = true;
            this.ExportOptions.Xlsx.FitToPrintedPageWidth = true;
            this.ExportOptions.Xls.ExportMode = _baoCaoDienTuSearchModel.enumDinhDanhXlsExcel;
            this.ExportOptions.Xlsx.ExportMode = _baoCaoDienTuSearchModel.enumDinhDanhXlsxExcel;
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblDonVi.Text = "\r\n" + _baoCaoDienTuSearchModel.TEN_DON_VI;
        }

        private void label6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            label6.Text = "Căn cứ Nghị định số 52/2009/NĐ-CP ngày 03 tháng 6 năm 2009 của Chính phủ (gọi tắt Nghị định 52/2009/NĐ.CP) quy định chi tiết và hướng dẫn thi hành môt số điều luật của Luật Quản lý, sử dụng tài sản nhà nước, " + _baoCaoDienTuSearchModel.TEN_DON_VI + " tổng hợp và báo cáo tình hình quản lý, sử dụng tài sản nhà nước năm 2022 như sau:";
        }

        private void label3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            label3.Text = "\r\n" + "......, ngày " +_baoCaoDienTuSearchModel.NGAY_BAO_CAO.Value.Day + " tháng " + _baoCaoDienTuSearchModel.NGAY_BAO_CAO.Value.Month + " năm " + _baoCaoDienTuSearchModel.NGAY_BAO_CAO.Value.Year;
        }

        private void label19_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            label19.Text = _baoCaoDienTuSearchModel.KIEN_NGHI;
        }

        private void label13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            label13.Text = _baoCaoDienTuSearchModel.CONG_TAC_CHI_DAO;
        }

        private void label15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            label15.Text = _baoCaoDienTuSearchModel.TINH_HINH_THUC_HIEN;
        }

        private void label17_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            label17.Text = _baoCaoDienTuSearchModel.KET_QUA_KHAC;
        }

        private void label9_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            label9.Text = _baoCaoDienTuSearchModel.TINH_HINH_BAN_HANH_VAN_BAN;
        }

        private void label23_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            label23.Text = "a. Phân tích thực trạng" + "\r\n" + _baoCaoDienTuSearchModel.THUC_TRANG;
        }

        private void label24_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            label24.Text = "b. Tình hình tăng/giảm tài sản nhà nước theo từng loại tài sản \r\n " + _baoCaoDienTuSearchModel.TINH_HINH_TANG_GIAM;
        }

        private void label26_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            label26.Text = _baoCaoDienTuSearchModel.DANH_GIA_TICH_CUC;
        }

        private void label28_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            label28.Text = _baoCaoDienTuSearchModel.DANH_GIA_TINH_HINH;
        }

        private void label29_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if(_baoCaoDienTuSearchModel.LY_DO_TU_CHOI != null)
            {
                label29.Text = "Lý do từ chối:" + _baoCaoDienTuSearchModel.LY_DO_TU_CHOI;
            }
            else
            {
                label29.Text = "";
            }
        }

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
	}
}
