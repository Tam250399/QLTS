using System;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCQH;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanTongHop;

namespace GS.Web.Reports.TS_BCQH
{
    public partial class rptTS_BCQH_QH14C_OTO_CHUCDANH_DV
    {
        private readonly BaoCaoTaiSanTongHopSearchModel _baoCaoTaiSanTongHopSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
		private int Cap1;
		private int Cap2;
		private int Cap3;
		private int Cap4;
		private int Cap5;
		public rptTS_BCQH_QH14C_OTO_CHUCDANH_DV(
            BaoCaoTaiSanTongHopSearchModel baoCaoTaiSanTongHopSearchModel,
            CauHinhBaoCaoModel cauHinhBaoCaoModel,
            CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel
            )
        {
            InitializeComponent();
			this.Cap1 = 0;
			this.Cap2 = 0;
			this.Cap3 = 0;
			this.Cap4 = 0;
			this.Cap5 = 0;
			this._baoCaoTaiSanTongHopSearchModel = baoCaoTaiSanTongHopSearchModel;
            this._cauHinhBaoCaoModel = cauHinhBaoCaoModel;
            this._cauHinhBaoCaoChungModel = cauHinhBaoCaoChungModel;
            this.ExportOptions.Xls.FitToPrintedPageWidth = true;
            this.ExportOptions.Xlsx.FitToPrintedPageWidth = true;
            this.ExportOptions.Xls.ExportMode = _baoCaoTaiSanTongHopSearchModel.enumDinhDanhXlsExcel;
            this.ExportOptions.Xlsx.ExportMode = _baoCaoTaiSanTongHopSearchModel.enumDinhDanhXlsxExcel;
        }
        int count = 0;
        private void tableCell13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            (sender as XRTableCell).Text = (++count).ToString();
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            chuthich.Text = "ĐVT cho: Giá trị là: " + _baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien() + ".";
        }
		#region đơn vị id
		private void dv1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCQH_QH14_OTO_CHUCDANH _row = GetCurrentRow() as TS_BCQH_QH14_OTO_CHUCDANH;
			if (_row == null )
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = false;
			}
			else
			{
				var _thisRow = (sender as GroupHeaderBand);
				_thisRow.Visible = true;
			}
		}
		private void dv2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCQH_QH14_OTO_CHUCDANH _row = GetCurrentRow() as TS_BCQH_QH14_OTO_CHUCDANH;
			if (_row == null || _row.DV_2 == null  || _baoCaoTaiSanTongHopSearchModel.BacDonVi < 2)
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = false;
			}
			else
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = true;
			}
		}
		private void dv3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCQH_QH14_OTO_CHUCDANH _row = GetCurrentRow() as TS_BCQH_QH14_OTO_CHUCDANH;
			if (_row == null || _row.DV_3 == null  || _baoCaoTaiSanTongHopSearchModel.BacDonVi < 3)
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = false;
			}
			else
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = true;
			}
		}
		private void dv4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCQH_QH14_OTO_CHUCDANH _row = GetCurrentRow() as TS_BCQH_QH14_OTO_CHUCDANH;
			if (_row == null || _row.DV_4 == null  || _baoCaoTaiSanTongHopSearchModel.BacDonVi < 4)
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = false;
			}
			else
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = true;
			}
		}
		private void dv5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCQH_QH14_OTO_CHUCDANH _row = GetCurrentRow() as TS_BCQH_QH14_OTO_CHUCDANH;
			if (_row == null || _row.DV_5 == null  || _baoCaoTaiSanTongHopSearchModel.BacDonVi < 5)
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = false;
			}
			else
			{
				var _this = sender as GroupHeaderBand;
				_this.Visible = true;
			}
		}
		#endregion
		private void detailBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCQH_QH14_OTO_CHUCDANH _row = GetCurrentRow() as TS_BCQH_QH14_OTO_CHUCDANH;
			if (_row != null)
			{
				if ( _baoCaoTaiSanTongHopSearchModel.BacDonVi < _row.tree_level_dv)
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
		private void tbl_dv1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCQH_QH14_OTO_CHUCDANH _row = GetCurrentRow() as TS_BCQH_QH14_OTO_CHUCDANH;
			var _this = sender as XRTableCell;
			if (_row.tree_level_dv >= 1)
			{
				Cap2 = 0;
				Cap3 = 0;
				Cap4 = 0;
				Cap5 = 0;
			}
			Cap1 += 1;
			_this.Text = Cap1.ToString() + ". " ;
		}

		private void tbl_dv2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCQH_QH14_OTO_CHUCDANH _row = GetCurrentRow() as TS_BCQH_QH14_OTO_CHUCDANH;
			var _this = sender as XRTableCell;
			if (_row.tree_level_dv >= 2)
			{
				Cap3 = 0;
				Cap4 = 0;
				Cap5 = 0;
			}
			if (_row.DV_2 != null)
				Cap2 += 1;
			_this.Text = Cap1.ToString() + "." + Cap2.ToString() + ". " ;
		}
		private void tbl_dv3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCQH_QH14_OTO_CHUCDANH _row = GetCurrentRow() as TS_BCQH_QH14_OTO_CHUCDANH;
			var _this = sender as XRTableCell;
			if (_row.tree_level_dv >= 3)
			{
				Cap4 = 0;
				Cap5 = 0;
			}
			if (_row.DV_3 != null)
				Cap3 += 1;
			_this.Text = Cap1.ToString() + "." + Cap2.ToString() + "." + Cap3.ToString() + ". " ;
		}
		private void tbl_dv4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCQH_QH14_OTO_CHUCDANH _row = GetCurrentRow() as TS_BCQH_QH14_OTO_CHUCDANH;
			var _this = sender as XRTableCell;
			if (_row.tree_level_dv >= 4)
			{
				Cap5 = 0;
			}
			if (_row.DV_4 != null)
				Cap4 += 1;
			_this.Text = Cap1.ToString() + "." + Cap2.ToString() + "." + Cap3.ToString() + "." + Cap4.ToString() + ". " ;
		}
		private void tbl_dv5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCQH_QH14_OTO_CHUCDANH _row = GetCurrentRow() as TS_BCQH_QH14_OTO_CHUCDANH;
			var _this = sender as XRTableCell;
			if (_row.DV_5 != null)
				Cap5 += 1;
			_this.Text = Cap1.ToString() + "." + Cap2.ToString() + "." + Cap3.ToString() + "." + Cap4.ToString() + "." + Cap5.ToString() + ". " ;
		}

		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            label1.Text = "Đơn vị báo cáo: " + _baoCaoTaiSanTongHopSearchModel.TEN_DON_VI;
			tieude.Text = "BÁO CÁO TỔNG HỢP XE Ô TÔ CHỨC DANH (LOẠI XE, BIỂN KIỂM SOÁT, CHỨC DANH)" + "\r\n" + " (Tính đến ngày: " + _baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString() + ")";
		}

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}
	}
}
