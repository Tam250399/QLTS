using System;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCCK;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.TaiSans;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;

namespace GS.Web.Reports.TS_BCCK
{
    public partial class rptTS_BCCK_11d_CK_TSC_KTNL_TS
    {
        private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
        private int _cap1;
        private int _cap2;
        private int _cap3;
        private int _cap4;
        private int _cap5;
        private int STT_DV;
        private int STT_DV_1;
        private int STT_DV_2;
        private int STT_DV_3;
        private int STT_DV_4;
        private int STT_DV_5;
        int STT_TS = 0;
        int STT_1 = 0;
        int STT_2 = 0;
        int STT_3 = 0;
        int STT_4 = 0;
        int STT_5 = 0;
        public rptTS_BCCK_11d_CK_TSC_KTNL_TS(
            BaoCaoTaiSanChiTietSearchModel baoCaoTaiSanChiTietSearchModel,
            CauHinhBaoCaoModel cauHinhBaoCaoModel,
            CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel
            )
        {
            InitializeComponent();
            this._baoCaoTaiSanChiTietSearchModel = baoCaoTaiSanChiTietSearchModel;
            this._cauHinhBaoCaoModel = cauHinhBaoCaoModel;
            this._cauHinhBaoCaoChungModel = cauHinhBaoCaoChungModel;
            this._cap1 = 0;
            this._cap2 = 0;
            this._cap3 = 0;
            this._cap4 = 0;
            this._cap5 = 0;
            this.ExportOptions.Xls.FitToPrintedPageWidth = true;
            this.ExportOptions.Xlsx.FitToPrintedPageWidth = true;
            this.ExportOptions.Xls.ExportMode = _baoCaoTaiSanChiTietSearchModel.enumDinhDanhXlsExcel;
            this.ExportOptions.Xlsx.ExportMode = _baoCaoTaiSanChiTietSearchModel.enumDinhDanhXlsxExcel;
        }

        private void rptTS_BCCK_11d_CK_TSC_KTNL_TS_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
            if (_baoCaoTaiSanChiTietSearchModel.MA_DON_VI == "018999")
            {
                lblBoNganhTinh.Text = "Bộ, tỉnh: Bộ tài chính (" + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI)+")";
            }
            else
            {
                lblBoNganhTinh.Text = "Bộ, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanChiTietSearchModel.TEN_BO_NGANH);
            }          
            lblMaDonVi.Text = "Mã đơn vị: " + _baoCaoTaiSanChiTietSearchModel.MA_DON_VI;
            tieude.Text = "CÔNG KHAI TÌNH HÌNH KHAI THÁC NGUỒN LỰC TÀI CHÍNH TỪ TÀI SẢN CÔNG\r\nNĂM " + _baoCaoTaiSanChiTietSearchModel.NamBaoCao;
            //ngayandbactaisan.Text = "Thời điểm báo cáo: " + _baoCaoTaiSanChiTietSearchModel.NgayKetThuc.toDateVNString() + "\r\n" + _baoCaoTaiSanTongHopSearchModel.StringLoaiTaiSan.ToStringLoaiHinhTaiSanFromString() + "\r\n" + "Chi tiết bậc báo cáo: " + _baoCaoTaiSanTongHopSearchModel.BacTaiSan;
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Diện tích là: " + _baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Giá trị là: " + _baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien() + ".";
        }

        private void Cap1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            var _thisRow = (sender as GroupHeaderBand);
            if (_row == null || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 2)
            {
                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
        }

        private void Cap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            if (_row != null && (_row.CAP_2 == "0" || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 3))
            {
                var _thisRow = (sender as GroupHeaderBand);
                _thisRow.Visible = false;
            }
            else
            {
                var _thisRow = (sender as GroupHeaderBand);
                _thisRow.Visible = true;
            }
        }

        private void Cap3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            if (_row != null && (_row.CAP_3 == "0" || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 4))
            {
                var _thisRow = (sender as GroupHeaderBand);
                _thisRow.Visible = false;
            }
            else
            {
                var _thisRow = (sender as GroupHeaderBand);
                _thisRow.Visible = true;
            }
        }

        private void Cap4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            if (_row != null && (_row.CAP_4 == "0" || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 5))
            {
                var _thisRow = (sender as GroupHeaderBand);
                _thisRow.Visible = false;
            }
            else
            {
                var _thisRow = (sender as GroupHeaderBand);
                _thisRow.Visible = true;
            }
        }

        private void Cap5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            if (_row != null && (_row.CAP_5 == "0" || _baoCaoTaiSanChiTietSearchModel.BacTaiSan < 5))
            {
                var _thisRow = (sender as GroupHeaderBand);

                _thisRow.Visible = false;
            }
            else
            {
                var _thisRow = (sender as GroupHeaderBand);
                _thisRow.Visible = true;
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            if (_row != null)
            {
                if (_baoCaoTaiSanChiTietSearchModel.BacTaiSan < 5)
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

        private void dv1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            if (_row == null || _row.DV_1 == null /*|| _baoCaoTaiSanChiTietSearchModel.MauSo != 3*/ || _baoCaoTaiSanChiTietSearchModel.BacDonVi < 1)
            {
                var _this = sender as GroupHeaderBand;
                _this.Visible = false;
            }
            else
            {
                var _this = sender as GroupHeaderBand;
                _this.Visible = true;
            }
            STT_1 = 0;
            STT_2 = 0;
            STT_3 = 0;
            STT_4 = 0;
            STT_5 = 0;
        }

        private void dv2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            if (_row == null || _row.DV_2 == null /*|| _baoCaoTaiSanChiTietSearchModel.MauSo != 3*/ || _baoCaoTaiSanChiTietSearchModel.BacDonVi < 2)
            {
                var _this = sender as GroupHeaderBand;
                _this.Visible = false;
            }
            else
            {
                var _this = sender as GroupHeaderBand;
                _this.Visible = true;
            }
            STT_1 = 0;
            STT_2 = 0;
            STT_3 = 0;
            STT_4 = 0;
            STT_5 = 0;
        }

        private void dv3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            if (_row == null || _row.DV_3 == null /*|| _baoCaoTaiSanChiTietSearchModel.MauSo != 3*/ || _baoCaoTaiSanChiTietSearchModel.BacDonVi < 3)
            {
                var _this = sender as GroupHeaderBand;
                _this.Visible = false;
            }
            else
            {
                var _this = sender as GroupHeaderBand;
                _this.Visible = true;
            }
            STT_1 = 0;
            STT_2 = 0;
            STT_3 = 0;
            STT_4 = 0;
            STT_5 = 0;
        }

        private void dv4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            if (_row == null || _row.DV_4 == null /*|| _baoCaoTaiSanChiTietSearchModel.MauSo != 3 */|| _baoCaoTaiSanChiTietSearchModel.BacDonVi < 4)
            {
                var _this = sender as GroupHeaderBand;
                _this.Visible = false;
            }
            else
            {
                var _this = sender as GroupHeaderBand;
                _this.Visible = true;
            }
            STT_1 = 0;
            STT_2 = 0;
            STT_3 = 0;
            STT_4 = 0;
            STT_5 = 0;
        }

        private void dv5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            if (_row == null || _row.DV_5 == null /*|| _baoCaoTaiSanChiTietSearchModel.MauSo != 3*/ || _baoCaoTaiSanChiTietSearchModel.BacDonVi < 5)
            {
                var _this = sender as GroupHeaderBand;
                _this.Visible = false;
            }
            else
            {
                var _this = sender as GroupHeaderBand;
                _this.Visible = true;
            }
            STT_1 = 0;
            STT_2 = 0;
            STT_3 = 0;
            STT_4 = 0;
            STT_5 = 0;
        }

        private void tendv1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            if (_row != null)
            {
                var _this = sender as XRTableCell;
                if (_row.DV_TEN_1 != null)
                {
                    STT_DV_1 += 1;
                    _this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}. {_row.DV_TEN_1}";
                    STT_DV_2 = 0;
                    STT_TS = 0;
                    STT_1 = 0;
                    STT_2 = 0;
                    STT_3 = 0;
                    STT_4 = 0;
                    STT_5 = 0;
                }
            }
        }

        private void tendv2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            if (_row != null)
            {
                var _this = sender as XRTableCell;
                if (_row.DV_TEN_2 != null)
                {
                    STT_DV_2 += 1;
                    _this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}.{STT_DV_2}. {_row.DV_TEN_2}";
                    STT_DV_3 = 0;
                    STT_TS = 0;
                    STT_1 = 0;
                    STT_2 = 0;
                    STT_3 = 0;
                    STT_4 = 0;
                    STT_5 = 0;
                }
            }
        }

        private void tendv3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            if (_row != null)
            {
                var _this = sender as XRTableCell;
                if (_row.DV_TEN_3 != null)
                {
                    STT_DV_3 += 1;
                    _this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}.{STT_DV_2}.{STT_DV_3}. {_row.DV_TEN_3}";
                    STT_DV_4 = 0;
                    STT_TS = 0;
                    STT_1 = 0;
                    STT_2 = 0;
                    STT_3 = 0;
                    STT_4 = 0;
                    STT_5 = 0;
                }
            }
        }

        private void tendv4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            if (_row != null)
            {
                var _this = sender as XRTableCell;
                if (_row.DV_TEN_4 != null)
                {
                    STT_DV_4 += 1;
                    _this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}.{STT_DV_2}.{STT_DV_3}.{STT_DV_4}. {_row.DV_TEN_3}";
                    STT_DV_5 = 0;
                    STT_TS = 0;
                    STT_1 = 0;
                    STT_2 = 0;
                    STT_3 = 0;
                    STT_4 = 0;
                    STT_5 = 0;
                }
            }
        }

        private void tendv5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            if (_row != null)
            {
                var _this = sender as XRTableCell;
                if (_row.DV_TEN_5 != null)
                {
                    STT_DV_5 += 1;
                    _this.Text = $"{STT_DV_1.ConvertIntToRomanNumber()}.{STT_DV_2}.{STT_DV_3}.{STT_DV_4}.{STT_DV_5}. {_row.DV_TEN_5}";
                    STT_TS = 0;
                    STT_1 = 0;
                    STT_2 = 0;
                    STT_3 = 0;
                    STT_4 = 0;
                    STT_5 = 0;
                }
            }
        }

        private void taisanTen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            //if (_row != null)
            //{
            //    StringBuilder @string = new StringBuilder("   ");
            //    for (int level = 0; level < _row.TREE_LEVEL; level++)
            //        @string.Append(" ");
            //    taisanTen.Text = @string.ToString() + _row.TAI_SAN_TEN;

            //}
        }

        private void loaicapdv_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            var _this = sender as XRTableCell;
            if (_row != null && _row.LOAI_CAP_DON_VI_ID >= 1)
            {
                _cap1 = 0;
                _cap2 = 0;
                _cap3 = 0;
                _cap4 = 0;
                _cap5 = 0;
            }
            STT_DV_1 = 0;
        }
        int STT = 0;
        private void tableCell331_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            var _this = sender as XRTableCell;
            if (_row != null)
            {
                STT_1 += 1;
                _this.Text = STT_1.ToString()/* CommonExtention.ConvertIntToRomanNumber(STT)*/;
                STT_2 = 0;
            }
        }

        private void tableCell245_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            var _this = sender as XRTableCell;
            if (_row != null)
            {
                STT_2 += 1;
                _this.Text = $"{STT_1}.{STT_2}";
                STT_3 = 0;
            }
        }

        private void tableCell48_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            var _this = sender as XRTableCell;
            if (_row != null)
            {
                STT_3 += 1;
                _this.Text = $"{STT_1}.{STT_2}.{STT_3}";
                STT_4 = 0;
            }
        }

        private void tableCell62_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            var _this = sender as XRTableCell;
            if (_row != null)
            {
                STT_4 += 1;
                _this.Text = $"{STT_1}.{STT_2}.{STT_3}.{STT_4}";
                STT_5 = 0;
            }
        }

        private void tableCell76_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            var _this = sender as XRTableCell;
            if (_row != null)
            {
                STT_5 += 1;
                _this.Text = $"{STT_1}.{STT_2}.{STT_3}.{STT_4}.{STT_5}";
            }
        }

        private void tableCell90_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            var _this = sender as XRTableCell;
            if (_row != null)
            {
                STT_5 += 1;
                _this.Text = $"{STT_1}.{STT_2}.{STT_3}.{STT_4}.{STT_5}";
            }
        }
       
        private void LoaiHinhTaiSan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            var _thisRow = (sender as GroupHeaderBand);
            if (_row == null /*|| _baoCaoTaiSanChiTietSearchModel.BacDonVi < _row.tree_level_dv*/)
            {
                _thisRow.Visible = false;
            }
            else
            {
                _thisRow.Visible = true;
            }
        }

        private void TenLoaiHinhTaiSan_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCCK_11D_CK_TSC _row = GetCurrentRow() as TS_BCCK_11D_CK_TSC;
            var _this = sender as XRTableCell;
            if (_row != null)
            {

                _this.Text = _baoCaoTaiSanChiTietSearchModel.lstNhanHienThi.Where(c => c.Key == (new enumNHAN_HIEN_THI_LOAI_HINH_TS().GetType().FullName) + "." + ((enumNHAN_HIEN_THI_LOAI_HINH_TS)_row.LOAI_HINH_TAI_SAN_ID).ToString()).Select(c => c.Value).FirstOrDefault();
            }
        }

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
	}
}
