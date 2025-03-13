using System;
using System.Drawing;
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCCK;
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanTongHop;

namespace GS.Web.Reports.TS_BCCK
{
    public partial class rptTS_BCCK_TINH_HINH_MUA_SAM
    {
        private readonly BaoCaoTaiSanTongHopSearchModel _baoCaoTaiSanTongHopSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
        private int STT_DV;
        private int STT_DV_1;
        private int STT_DV_2;
        private int STT_DV_3;
        private int STT_DV_4;
        private int STT_DV_5;
        private int STT_TS;
        private int STT_TS_1;
        private int STT_TS_2;
        private int STT_TS_3;
        private int STT_TS_4;
        private int STT_TS_5;
        public rptTS_BCCK_TINH_HINH_MUA_SAM(BaoCaoTaiSanTongHopSearchModel baoCaoTaiSanTongHopSearchModel,
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
            STT_DV_1 = 0;
            STT_DV = 0;
            STT_DV_2 = 0;
            STT_DV_3 = 0;
            STT_DV_4 = 0;
            STT_DV_5 = 0;
            STT_TS = 0;
            STT_TS_1 = 0;
            STT_TS_2 = 0;
            STT_TS_3 = 0;
            STT_TS_4 = 0;
            STT_TS_5 = 0;
        }

        private void rptTS_BCCK_TINH_HINH_MUA_SAM_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
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

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void cap1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_THMS _thisrow = GetCurrentRow() as TS_BCCK_THMS;
            var _row = (sender as GroupHeaderBand);
            if (_thisrow != null)
            {
                _row.Visible = true;
            }
            else
            {
                _row.Visible = false;
            }
        }

        private void cap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_THMS _thisrow = GetCurrentRow() as TS_BCCK_THMS;
            var _row = (sender as GroupHeaderBand);
            if (_thisrow != null && _thisrow.CAP_2.Length > 4 && _baoCaoTaiSanTongHopSearchModel.BacTaiSan >= 2)
            {
                _row.Visible = true;
            }
            else
            {
                _row.Visible = false;
            }
        }

        private void cap3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_THMS _thisrow = GetCurrentRow() as TS_BCCK_THMS;
            var _row = (sender as GroupHeaderBand);
            if (_thisrow != null && _thisrow.CAP_3.Length > 4 && _baoCaoTaiSanTongHopSearchModel.BacTaiSan >= 3)
            {
                _row.Visible = true;
            }
            else
            {
                _row.Visible = false;
            }
        }

        private void cap4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_THMS _thisrow = GetCurrentRow() as TS_BCCK_THMS;
            var _row = (sender as GroupHeaderBand);
            if (_thisrow != null && _thisrow.CAP_4.Length > 4 && _baoCaoTaiSanTongHopSearchModel.BacTaiSan >= 4)
            {
                _row.Visible = true;
            }
            else
            {
                _row.Visible = false;
            }
        }

        private void cap5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_THMS _thisrow = GetCurrentRow() as TS_BCCK_THMS;
            var _row = (sender as GroupHeaderBand);
            if (_thisrow != null && _thisrow.CAP_5.Length > 4 && _baoCaoTaiSanTongHopSearchModel.BacTaiSan >= 5)
            {
                _row.Visible = true;
            }
            else
            {
                _row.Visible = false;
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_THMS _thisrow = GetCurrentRow() as TS_BCCK_THMS;
            var _row = (sender as DetailBand);
            if (_thisrow != null && _baoCaoTaiSanTongHopSearchModel.BacTaiSan == 5)
            {
                _row.Visible = true;
            }
            else
            {
                _row.Visible = false;
            }
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ChucDanhNguoiLapBieu.Text = _cauHinhBaoCaoModel.ChucDanhNguoiLapBieu;
            ChucDanhThuTruongDonVi.Text = _cauHinhBaoCaoModel.ChucDanhThuTruongDonVi;
            TenNguoiLapBieu.Text = _cauHinhBaoCaoModel.TenNguoiLapBieu;
            TenThuTruongDonVi.Text = _cauHinhBaoCaoModel.TenThuTruongDonVi;
            DiaDanhBaoCao.Text = _cauHinhBaoCaoModel.DiaDanhBaoCao;
            if (String.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhNguoiLapBieu))
                lb_GhiChuNLB.Visible = false;
            if (String.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhThuTruongDonVi))
                lb_GhiChuThuTruong.Visible = false;
        }

        private void ten1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_1 = GetCurrentColumnValue<string>("TEN_1");
            if (TEN_1 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = " " + TEN_1;
            }
        }

        private void ten2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_2 = GetCurrentColumnValue<string>("TEN_2");
            if (TEN_2 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = "  " + TEN_2;
            }
        }    

        private void ten3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_3 = GetCurrentColumnValue<string>("TEN_3");
            if (TEN_3 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = "    " + TEN_3;
            }
        }

        private void ten4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_4 = GetCurrentColumnValue<string>("TEN_4");
            if (TEN_4 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = "      " + TEN_4;
            }
        }

        private void ten5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TEN_5 = GetCurrentColumnValue<string>("TEN_5");
            if (TEN_5 != null)
            {
                var _thisCell = (sender as XRTableCell);
                _thisCell.Text = "      " + TEN_5;
            }
        }

        private void tentaisan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
                TS_BCCK_THMS _row = GetCurrentRow() as TS_BCCK_THMS;
                if (_row != null)
                {
                    var _thisCell = (sender as XRTableCell);
                    StringBuilder @string = new StringBuilder("   ");
                    for (int level = 0; level < _row.TREE_LEVEL; level++)
                        @string.Append(" ");
                    _thisCell.Text = @string.ToString() + _row.TEN_TAI_SAN;
                }
            }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            lblDonVi.Text = "Tên đơn vị: " + _baoCaoTaiSanTongHopSearchModel.TEN_DON_VI;
            lblBoNganhTinh.Text = "Bộ, ngành, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanTongHopSearchModel.TEN_BO_NGANH);
            lblMaDonVi.Text = "Mã đơn vị: " + _baoCaoTaiSanTongHopSearchModel.MA_DON_VI;
            tieude.Text = "BÁO CÁO TÌNH HÌNH MUA SẮM TẬP TRUNG NĂM " + _baoCaoTaiSanTongHopSearchModel.NamBaoCao
                + "\r\n" + $"Bậc tài sản: {_baoCaoTaiSanTongHopSearchModel.BacTaiSan}"
                + "\r\n" + $"Loại hình tài sản: {_baoCaoTaiSanTongHopSearchModel.TenLoaiHinhTaiSanPrint}";
            chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên" + "Giá trị là: " + _baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien() + ".";
        }

        private void phanloai_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var _this = sender as GroupBand;
            _this.Visible = false;

        }

        private void tenphanloai_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_THMS _thisrow = GetCurrentRow() as TS_BCCK_THMS;
            if (_thisrow != null)
            {
                tenphanloai.Text = ((int)_thisrow.PHAN_LOAI_ID).ToLoaiTaiSanBaoCao();
            }
        }
        private void Stt_TS_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_THMS _row = GetCurrentRow() as TS_BCCK_THMS;
            STT_TS_2 = 0;
            var _this = sender as XRTableCell;
            if (_row != null)
            {
                STT_TS_1 += 1;

                _this.Text = $"{STT_TS_1.ConvertIntToRomanNumber()}";
            }
        }
        private void Stt_TS_2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_THMS _row = GetCurrentRow() as TS_BCCK_THMS;
            STT_TS_3 = 0;
            var _this = sender as XRTableCell;
            if (_row != null && _row.TEN_2 != null)
            {
                STT_TS_2 += 1;

                _this.Text = $"{STT_TS_1.ConvertIntToRomanNumber()}.{STT_TS_2}";
            }
        }
        private void Stt_TS_3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_THMS _row = GetCurrentRow() as TS_BCCK_THMS;
            STT_TS_4 = 0;
            var _this = sender as XRTableCell;
            if (_row != null && _row.TEN_3 != null)
            {
                STT_TS_3 += 1;
                _this.Text = $"{STT_TS_1.ConvertIntToRomanNumber()}.{STT_TS_2}.{STT_TS_3}";



            }
        }
        private void Stt_TS_4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_THMS _row = GetCurrentRow() as TS_BCCK_THMS;
            STT_TS_5 = 0;
            var _this = sender as XRTableCell;
            if (_row != null && _row.TEN_4 != null)
            {
                STT_TS_4 += 1;
                _this.Text = $"{STT_TS_1.ConvertIntToRomanNumber()}.{STT_TS_2}.{STT_TS_3}.{STT_TS_4}";
            }
        }
        private void Stt_TS_5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_THMS _row = GetCurrentRow() as TS_BCCK_THMS;
            var _this = sender as XRTableCell;
            if (_row != null && _row.TEN_5 != null)
            {
                STT_TS_5 += 1;

                _this.Text = $"{STT_TS_1.ConvertIntToRomanNumber()}.{STT_TS_2}.{STT_TS_3}.{STT_TS_4}.{STT_TS_5}";


            }
        }

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
	}
}
