
using System;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.CCDC;
using GS.Services.DanhMuc;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;

namespace GS.Web.Reports.CCDC
{
    public partial class CCDC_TongHop
    {
        private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
        private int STT_DV_1;
        private int STT_DV_2;
        private int STT_DV_3;
        private int STT_DV_4;
        private int STT_DV_5;
        private int STT;
        public CCDC_TongHop(BaoCaoTaiSanChiTietSearchModel baoCaoTaiSanChiTietSearchModel,
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
            leftHeader.Text = "Cơ quan quản lý cấp trên: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI_CAP_TREN) + "\r\n" + "Tên đơn vị: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI + "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanChiTietSearchModel.MA_DON_VI;
            if(_baoCaoTaiSanChiTietSearchModel.MauSo == 1)
            {
                CenterHeader.Text = $"TỔNG HỢP CÔNG CỤ, DỤNG CỤ \r\n Phần 1: Chi tiết theo đơn vị";
            }
            else 
            {
                CenterHeader.Text = $"TỔNG HỢP CÔNG CỤ, DỤNG CỤ \r\n Phần 2: Chi tiết theo công cụ, dụng cụ";
            }
            ngayANDbophan.Text = "Ngày báo cáo: " + _baoCaoTaiSanChiTietSearchModel.NgayBaoCao.toDateVNString() + "\r\n" + "Phòng ban, bộ phận sử dụng: " + _baoCaoTaiSanChiTietSearchModel.TenListDonViBoPhan /*+ "\r\n" + _donViBoPhan*/;
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
        //private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    LietKeCCDC _rowC = GetCurrentRow() as LietKeCCDC;
        //    LietKeCCDC _rowN = GetNextRow() as LietKeCCDC;
        //    var _thisRow = (sender as GroupFooterBand);
        //    _thisRow.Visible = false;
        //    if (_rowN == _rowC)
        //    {
        //        _thisRow.Visible = true;
        //    }
        //    ChucDanhNguoiLapBieu.Text = _cauHinhBaoCaoModel.ChucDanhNguoiLapBieu;
        //    ChucDanhKeToanTruong.Text = _cauHinhBaoCaoModel.ChucDanhKeToanTruong;
        //    ChucDanhThuTruongDonVi.Text = _cauHinhBaoCaoModel.ChucDanhThuTruongDonVi;
        //    TenNguoiLapBieu.Text = _cauHinhBaoCaoModel.TenNguoiLapBieu;
        //    TenKeToanTruong.Text = _cauHinhBaoCaoModel.TenKeToanTruong;
        //    TenThuTruongDonVi.Text = _cauHinhBaoCaoModel.TenThuTruongDonVi;
        //    DiaDanhBaoCao.Text = _cauHinhBaoCaoModel.DiaDanhBaoCao;
        //}
        private void CCDC_KiemKes_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Margins.Top = Convert.ToInt32(_cauHinhBaoCaoModel.MarginTop * 0.3937 * 100);
            //Margins.Bottom = Convert.ToInt32(_cauHinhBaoCaoModel.MarginBottom * 0.3937 * 100);
            //Margins.Left = Convert.ToInt32(_cauHinhBaoCaoModel.MarginLeft * 0.3937 * 100);
            //Margins.Right = Convert.ToInt32(_cauHinhBaoCaoModel.MarginRight * 0.3937 * 100);
        }

        private void table_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var row = GetCurrentRow() as TongHopCCDC;
            if (row == null)
            {
                var table = (sender as XRTable);
                table.Visible = false;
            }
            else
            {
                var table = (sender as XRTable);
                table.Visible = true;
            }
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            chuthich.Text = "ĐVT cho giá trị: " + _baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TongHopCCDC _row = GetCurrentRow() as TongHopCCDC;
            if (_row != null)
            {
                if (_baoCaoTaiSanChiTietSearchModel.MauSo < 2)
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

        private void GroupHeader5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TongHopCCDC _row = GetCurrentRow() as TongHopCCDC;
            var _thisRow = (sender as GroupHeaderBand);
            if (_row == null )
            {
                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
        }

        private void GroupHeader4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TongHopCCDC _row = GetCurrentRow() as TongHopCCDC;
            var _thisRow = (sender as GroupHeaderBand);
            if (_row != null && _row.DV_2 != null && _baoCaoTaiSanChiTietSearchModel.MauSo == 2)
            {
                _thisRow.Visible = true;
            }
            else if (_row == null || _row.DV_2 == null || _baoCaoTaiSanChiTietSearchModel.BacDonVi < 2)
            {
                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
        }

        private void GroupHeader3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TongHopCCDC _row = GetCurrentRow() as TongHopCCDC;
            var _thisRow = (sender as GroupHeaderBand);
            if (_row != null && _row.DV_3 != null && _baoCaoTaiSanChiTietSearchModel.MauSo == 2)
            {
                _thisRow.Visible = true;
            }
            else if (_row == null || _row.DV_3 == null || _baoCaoTaiSanChiTietSearchModel.BacDonVi < 3)
            {
                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
        }

        private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TongHopCCDC _row = GetCurrentRow() as TongHopCCDC;
            var _thisRow = (sender as GroupHeaderBand);
            if (_row != null && _row.DV_4 != null && _baoCaoTaiSanChiTietSearchModel.MauSo == 2)
            {
                _thisRow.Visible = true;
            }
            else if (_row == null || _row.DV_4 == null || _baoCaoTaiSanChiTietSearchModel.BacDonVi < 4)
            {
                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TongHopCCDC _row = GetCurrentRow() as TongHopCCDC;
            var _thisRow = (sender as GroupHeaderBand);
            if (_row != null && _row.DV_5 != null && _baoCaoTaiSanChiTietSearchModel.MauSo == 2)
            {
                _thisRow.Visible = true;
            }
            else if (_row == null || _row.DV_5 == null || _baoCaoTaiSanChiTietSearchModel.BacDonVi < 5)
            {
                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
        }

        private void tableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var _this = sender as XRTableCell;
            if(_baoCaoTaiSanChiTietSearchModel.MauSo == 1)
            {
                _this.Text = "Tên đơn vị";
            }
            else
            {
                _this.Text = "Tên công cụ";
            }
        }

        private void tableCell20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TongHopCCDC _row = GetCurrentRow() as TongHopCCDC;
            if (_row != null)
            {
                var _this = sender as XRTableCell;
                if (_row.DV_TEN_1 != null)
                {
                    STT_DV_1 += 1;
                    STT_DV_2 = 0;
                    STT_DV_3 = 0;
                    STT_DV_4 = 0;
                    STT_DV_5 = 0;
                    STT = 0;
                }
                _this.Text = STT_DV_1.ConvertIntToRomanNumber();
            }
        }

        private void tableCell28_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TongHopCCDC _row = GetCurrentRow() as TongHopCCDC;
            if (_row != null)
            {
                var _this = sender as XRTableCell;
                if (_row.DV_TEN_2 != null)
                {
                    STT_DV_2 += 1;
                    STT_DV_3 = 0;
                    STT_DV_4 = 0;
                    STT_DV_5 = 0;
                    STT = 0;
                }
                _this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}. {STT_DV_2}";
            }
        }

        private void tableCell36_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TongHopCCDC _row = GetCurrentRow() as TongHopCCDC;
            if (_row != null)
            {
                var _this = sender as XRTableCell;
                if (_row.DV_TEN_3 != null)
                {
                    STT_DV_3 += 1;
                    STT_DV_4 = 0;
                    STT_DV_5 = 0;
                    STT = 0;
                }
                _this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}. {STT_DV_2}. {STT_DV_3}";
            }
        }

        private void tableCell44_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TongHopCCDC _row = GetCurrentRow() as TongHopCCDC;
            if (_row != null)
            {
                var _this = sender as XRTableCell;
                if (_row.DV_TEN_4 != null)
                {
                    STT_DV_4 += 1;
                    STT_DV_5 = 0;
                    STT = 0;
                }
                _this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}. {STT_DV_2}. {STT_DV_3}. {STT_DV_4}";
            }
        }

        private void tableCell52_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TongHopCCDC _row = GetCurrentRow() as TongHopCCDC;
            if (_row != null)
            {
                var _this = sender as XRTableCell;
                if (_row.DV_TEN_5 != null)
                {
                    STT_DV_5 += 1;
                    STT = 0;
                }
                _this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}. {STT_DV_2}. {STT_DV_3}. {STT_DV_4}. {STT_DV_5}";
            }
        }

        private void tableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            TongHopCCDC _row = GetCurrentRow() as TongHopCCDC;
            if (_row != null)
            {
                var _this = sender as XRTableCell;
                STT += 1;
                _this.Text = $"{STT}";
            }
        }

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
	}
}
