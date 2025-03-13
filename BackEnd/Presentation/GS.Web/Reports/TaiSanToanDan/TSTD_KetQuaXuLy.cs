using System;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TSTD;
using GS.Core.Infrastructure;
using GS.Services;
using GS.Services.DanhMuc;
using GS.Web.Factories.SHTD;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;

namespace GS.Web.Reports.TaiSanToanDan
{
    public partial class TSTD_KetQuaXuLy
    {
        private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
        public TSTD_KetQuaXuLy(BaoCaoTaiSanChiTietSearchModel baoCaoTaiSanChiTietSearchModel,
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
        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblDonVi.Text = "CƠ QUAN QUẢN LÝ CẤP TRÊN: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI_CAP_TREN.ToUpper() + "\r\n" + "CƠ QUAN CHỦ TRÌ QUẢN LÝ TÀI SẢN: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            if (_cauHinhBaoCaoModel.ChucDanhNguoiLapBieu != null)
            {
                ChucDanhNguoiLapBieu.Text = _cauHinhBaoCaoModel.ChucDanhNguoiLapBieu;
            }
            if (_cauHinhBaoCaoModel.ChucDanhThuTruongDonVi != null)
            {
                ChucDanhThuTruongDonVi.Text = _cauHinhBaoCaoModel.ChucDanhThuTruongDonVi;
            }
            if (_cauHinhBaoCaoModel.TenNguoiLapBieu != null)
            {
                TenNguoiLapBieu.Text = _cauHinhBaoCaoModel.TenNguoiLapBieu;
            }
            if (_cauHinhBaoCaoModel.TenThuTruongDonVi != null)
            {
                TenThuTruongDonVi.Text = _cauHinhBaoCaoModel.TenThuTruongDonVi;
            }
            if (_cauHinhBaoCaoModel.DiaDanhBaoCao != null)
            {
                DiaDanhBaoCao.Text = _cauHinhBaoCaoModel.DiaDanhBaoCao;
            }
            if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhNguoiLapBieu) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenNguoiLapBieu))
                lb_GhiChuNLB.Visible = false;
            if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhThuTruongDonVi) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenThuTruongDonVi))
                lb_GhiChuThuTruong.Visible = false;
        }

        private void TSTD_KetQuaXuLy_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            chuthich.Text = "ĐVT giá trị là: " + _baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien() + ".";
        }

        private void tencap1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BaoCaoKetQuaXuLyTSTD _row = GetCurrentRow() as BaoCaoKetQuaXuLyTSTD;
            if (_row != null)
            {
                var _thisRow = (sender as XRTableCell);
                var _itemFactory = EngineContext.Current.Resolve<IThuChiModelFactory>();
                _thisRow.Text = _itemFactory.PrepareDanhSachQuyetDinhXuLy(_row.LIST_XU_LY_ID);
            }
        }

        private void label3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            label3.Text = $"Kỳ báo cáo: Từ ngày {_baoCaoTaiSanChiTietSearchModel.NgayBatDau.toDateVNString()} đến ngày {_baoCaoTaiSanChiTietSearchModel.NgayKetThuc.toDateVNString()}";
        }

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
	}
}
