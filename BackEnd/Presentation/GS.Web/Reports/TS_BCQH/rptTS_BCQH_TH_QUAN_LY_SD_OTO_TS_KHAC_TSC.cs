using System;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCQH;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanTongHop;

namespace GS.Web.Reports.TS_BCQH
{
    public partial class rptTS_BCQH_TH_QUAN_LY_SD_OTO_TS_KHAC_TSC
    {
        private readonly BaoCaoTaiSanTongHopSearchModel _baoCaoTaiSanTongHopSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
        public rptTS_BCQH_TH_QUAN_LY_SD_OTO_TS_KHAC_TSC(
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

		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lbDieuKien.Text = "(Thời điểm " + _baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString() + ")";
        }


        int CapDonVi = 0;
        string DonViId = "";
        int sttLoaiDonVi = 1;
        int sttDV = 0;
        private void tableCell110_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCQH_TH_QUAN_LY_SD_OTO_TS_KHAC_TSC _row = GetCurrentRow() as TS_BCQH_TH_QUAN_LY_SD_OTO_TS_KHAC_TSC;
            if (_row != null)
            {
                if (_row.LOAI_CAP_DON_VI_ID != CapDonVi)
                {
                    CapDonVi = (int)_row.LOAI_CAP_DON_VI_ID;
                    sttLoaiDonVi = 1;
                    sttDV = 0;
                }
                if (_row.DV_1 != DonViId)
                {
                    DonViId = _row.DV_1;
                    sttDV += 1;
                    sttLoaiDonVi = 1;
                }
                var _thisrow = sender as XRTableCell;
                _thisrow.Text = sttDV.ToString();
            }
        }
        private void tableCell23_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCQH_TH_QUAN_LY_SD_OTO_TS_KHAC_TSC _row = GetCurrentRow() as TS_BCQH_TH_QUAN_LY_SD_OTO_TS_KHAC_TSC;
            if (_row != null)
            {
                var _thisrow = sender as XRTableCell;
                _thisrow.Text = sttDV.ToString() + "." + sttLoaiDonVi.ToString();
                sttLoaiDonVi += 1;
            }
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            chuthich.Text = "ĐVT cho: Số lượng là: Cái, Khuôn viên; Giá trị là: " + _baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien() + ";";

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

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
	}
}
