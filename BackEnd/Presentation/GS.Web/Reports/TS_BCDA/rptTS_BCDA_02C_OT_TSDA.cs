using System;
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCDA;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanTongHop;

namespace GS.Web.Reports.TS_BCDA
{
    public partial class rptTS_BCDA_02C_OT_TSDA
    {
        private readonly BaoCaoTaiSanTongHopSearchModel _baoCaoTaiSanTongHopSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
		private int Cap1;
		private int Cap2;
		private int Cap3;
		private int Cap4;
		private int Cap5;
		public rptTS_BCDA_02C_OT_TSDA(BaoCaoTaiSanTongHopSearchModel baoCaoTaiSanTongHopSearchModel,
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
			this.Cap1 = 0;
			this.Cap2 = 0;
			this.Cap3 = 0;
			this.Cap4 = 0;
			this.Cap5 = 0;
		}

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblDonVi.Text = "Bộ, ngành, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanTongHopSearchModel.TEN_BO_NGANH) + "\r\n" + "Tên đơn vị: " + _baoCaoTaiSanTongHopSearchModel.TEN_DON_VI + "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanTongHopSearchModel.MA_DON_VI;
            ngayandbactaisan.Text = "Ngày báo cáo: " + _baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString() + "\r\n" + /*_baoCaoTaiSanTongHopSearchModel.StringLoaiTaiSan.ToStringLoaiHinhTaiSanFromString() + "\r\n" +*/ " Chi tiết bậc báo cáo: " + _baoCaoTaiSanTongHopSearchModel.BacTaiSan;
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Giá trị là: " + _baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien() + ".";
        }

        private void dv1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCDA_02C_OT_TSDA _row = GetCurrentRow() as TS_BCDA_02C_OT_TSDA;
            var _this = sender as GroupHeaderBand;
            if (_row == null || _row.DV_1 == null)
            {

                _this.Visible = false;
            }
            else
            {
                _this.Visible = true;
            }
        }

        private void dv2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCDA_02C_OT_TSDA _row = GetCurrentRow() as TS_BCDA_02C_OT_TSDA;
            var _this = sender as GroupHeaderBand;
            if (_row == null || _row.DV_2 == null)
            {

                _this.Visible = false;
            }
            else
            {
                _this.Visible = true;
            }
        }

        private void dv3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCDA_02C_OT_TSDA _row = GetCurrentRow() as TS_BCDA_02C_OT_TSDA;
            var _this = sender as GroupHeaderBand;
            if (_row == null || _row.DV_3 == null)
            {

                _this.Visible = false;
            }
            else
            {
                _this.Visible = true;
            }
        }

        private void dv4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCDA_02C_OT_TSDA _row = GetCurrentRow() as TS_BCDA_02C_OT_TSDA;
            var _this = sender as GroupHeaderBand;
            if (_row == null || _row.DV_4 == null)
            {

                _this.Visible = false;
            }
            else
            {
                _this.Visible = true;
            }
        }

        private void dv5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCDA_02C_OT_TSDA _row = GetCurrentRow() as TS_BCDA_02C_OT_TSDA;
            var _this = sender as GroupHeaderBand;
            if (_row == null || _row.DV_5 == null)
            {

                _this.Visible = false;
            }
            else
            {
                _this.Visible = true;
            }
        }

        private void duan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void cap1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void cap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCDA_02C_OT_TSDA _thisrow = GetCurrentRow() as TS_BCDA_02C_OT_TSDA;
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
            TS_BCDA_02C_OT_TSDA _thisrow = GetCurrentRow() as TS_BCDA_02C_OT_TSDA;
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
            TS_BCDA_02C_OT_TSDA _thisrow = GetCurrentRow() as TS_BCDA_02C_OT_TSDA;
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
            TS_BCDA_02C_OT_TSDA _thisrow = GetCurrentRow() as TS_BCDA_02C_OT_TSDA;
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
            TS_BCDA_02C_OT_TSDA _thisrow = GetCurrentRow() as TS_BCDA_02C_OT_TSDA;
            var _row = (sender as DetailBand);
            if (_thisrow != null && _thisrow.tree_level + 1 > _baoCaoTaiSanTongHopSearchModel.BacTaiSan)
            {
                _row.Visible = false;
            }
            else
            {
                _row.Visible = true;
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

        private void tentaisan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TS_BCDA_02C_OT_TSDA _row = GetCurrentRow() as TS_BCDA_02C_OT_TSDA;
            if (_row != null)
            {
                var _thisCell = (sender as XRTableCell);
                StringBuilder @string = new StringBuilder("   ");
                for (int level = 0; level < _row.tree_level; level++)
                    @string.Append(" ");
                _thisCell.Text = @string.ToString() + _row.tai_san_ten;
            }
        }

		private void tbl_dv1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCDA_02C_OT_TSDA _row = GetCurrentRow() as TS_BCDA_02C_OT_TSDA;
			var _this = sender as XRTableCell;
			if (_row.tree_level_dv >= 1)
			{
				Cap2 = 0;
				Cap3 = 0;
				Cap4 = 0;
				Cap5 = 0;
			}
			Cap1 += 1;
			_this.Text = Cap1.ToString() + ". ";
		}

		private void tbl_dv2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCDA_02C_OT_TSDA _row = GetCurrentRow() as TS_BCDA_02C_OT_TSDA;
			var _this = sender as XRTableCell;
			if (_row.tree_level_dv >= 2)
			{
				Cap3 = 0;
				Cap4 = 0;
				Cap5 = 0;
			}
			if (_row.DV_2 != null)
				Cap2 += 1;
			_this.Text = Cap1.ToString() + "." + Cap2.ToString() + ". ";
		}
		private void tbl_dv3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCDA_02C_OT_TSDA _row = GetCurrentRow() as TS_BCDA_02C_OT_TSDA;
			var _this = sender as XRTableCell;
			if (_row.tree_level_dv >= 3)
			{
				Cap4 = 0;
				Cap5 = 0;
			}
			if (_row.DV_3 != null)
				Cap3 += 1;
			_this.Text = Cap1.ToString() + "." + Cap2.ToString() + "." + Cap3.ToString() + ". ";
		}
		private void tbl_dv4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCDA_02C_OT_TSDA _row = GetCurrentRow() as TS_BCDA_02C_OT_TSDA;
			var _this = sender as XRTableCell;
			if (_row.tree_level_dv >= 4)
			{
				Cap5 = 0;
			}
			if (_row.DV_4 != null)
				Cap4 += 1;
			_this.Text = Cap1.ToString() + "." + Cap2.ToString() + "." + Cap3.ToString() + "." + Cap4.ToString() + ". ";
		}
		private void tbl_dv5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCDA_02C_OT_TSDA _row = GetCurrentRow() as TS_BCDA_02C_OT_TSDA;
			var _this = sender as XRTableCell;
			if (_row.DV_5 != null)
				Cap5 += 1;
			_this.Text = Cap1.ToString() + "." + Cap2.ToString() + "." + Cap3.ToString() + "." + Cap4.ToString() + "." + Cap5.ToString() + ". ";
		}
		private int indexTenDuAn = 0;
		private void tenduan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			var _thisCell = (sender as XRTableCell);
			var _tenDuAn = (GetCurrentColumnValue<string>("ten_du_an"));
			if (_thisCell != null)
			{
				_thisCell.Text = $"{++indexTenDuAn}. {_tenDuAn}";
			}
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
                _thisCell.Text = "        " + TEN_5;
            }
        }
    }
}
