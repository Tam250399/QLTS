using System;
using System.Text;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCCT;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;

namespace GS.Web.Reports.TS_BCCT
{
    public partial class rptTS_BCCT_01B_TSC
    {
        private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
        public rptTS_BCCT_01B_TSC(
            BaoCaoTaiSanChiTietSearchModel baoCaoTaiSanChiTietSearchModel,
            CauHinhBaoCaoModel cauHinhBaoCaoModel,
            CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel)
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
            tcBoNganhTinh.Text = "Bộ, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanChiTietSearchModel.TEN_BO_NGANH) + "\r\n" + "Tên đơn vị: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI + "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanChiTietSearchModel.MA_DON_VI;
            tcDieuKien.Text = "Thời điểm báo cáo: " + _baoCaoTaiSanChiTietSearchModel.NgayKetThuc.toDateVNString() + "\r\n" + _baoCaoTaiSanChiTietSearchModel.StringLoaiTaiSan.ToStringLoaiHinhTaiSanFromString() + "\r\n" + "Chi tiết bậc báo cáo: " + _baoCaoTaiSanChiTietSearchModel.BacTaiSan;
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tcChuThichDonVi.Text = "ĐVT cho: Diện tích đất là: " + _baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Diện tích nhà là: " + _baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich() + ";";
        }

        private void Cap1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int Cap_1 = GetCurrentColumnValue<int>("CAP_1");
            if (Cap_1 == 0)
            {
                var _thisRow = (sender as GroupHeaderBand);
                _thisRow.Visible = false;
            }
        }

        private void Cap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int Cap_2 = GetCurrentColumnValue<int>("CAP_2");
            var _thisRow = (sender as GroupHeaderBand);
            if (Cap_2 == 0 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 2)
            {
                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
        }

        private void Cap3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int Cap_3 = GetCurrentColumnValue<int>("CAP_3");
            var _thisRow = (sender as GroupHeaderBand);
            if (Cap_3 == 0 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 3)
            {
                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
        }

        private void Cap4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int Cap_4 = GetCurrentColumnValue<int>("CAP_4");
            var _thisRow = (sender as GroupHeaderBand);
            if (Cap_4 == 0 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 4)
            {
                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
        }

        private void Cap5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int Cap_5 = GetCurrentColumnValue<int>("CAP_5");
            var _thisRow = (sender as GroupHeaderBand);
            if (Cap_5 == 0 || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 5)
            {


                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCT_1B _row = GetCurrentRow() as TS_BCCT_1B;
            if (_row != null)
            {
                if (_row.TREE_LEVEL + 1 > _baoCaoTaiSanChiTietSearchModel.BacTaiSan)
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

        private void tcTenTaiSan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCT_1B _row = GetCurrentRow() as TS_BCCT_1B;
            if (_row != null)
            {
                var _thisCell = (sender as XRTableCell);
                StringBuilder @string = new StringBuilder("   ");
                for (int level = 0; level < _row.TREE_LEVEL; level++)
                    @string.Append(" ");
                _thisCell.Text = @string.ToString() + _row.TAI_SAN_TEN;
            }
        }

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
	}
}
