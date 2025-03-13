using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCQH;
using GS.Core.Domain.SHTD;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanTongHop;

namespace GS.Web.Reports.TS_BCQH
{
    public partial class rptTS_BCQH_MAU01_THTSNN
    {
		private readonly BaoCaoTaiSanTongHopSearchModel _baoCaoTaiSanTongHopSearchModel;
		private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
		private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;

		public rptTS_BCQH_MAU01_THTSNN(
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

		private void rptTS_BCQH_MAU01_THTSNN_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
				//item.BorderColor = ColorTranslator.FromHtml(_cauHinhBaoCaoChungModel.ColorBorder);
				//item.BorderWidth = _cauHinhBaoCaoChungModel.WidthBorder;
				//item.BorderDashStyle = (BorderDashStyle)_cauHinhBaoCaoChungModel.StyleBorder;
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
			{
				Margins.Top = Convert.ToInt32(_cauHinhBaoCaoModel.MarginTop * 0.3937 * 100);
			}
			if (_cauHinhBaoCaoModel.MarginBottom > 0)
			{
				Margins.Bottom = Convert.ToInt32(_cauHinhBaoCaoModel.MarginBottom * 0.3937 * 100);
			}
		}
        private void GroupHeader_4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            label4.Text = "Đơn vị báo cáo: " + _baoCaoTaiSanTongHopSearchModel.TEN_DON_VI;
			tieude.Text = "Phụ lục I"+"\r\n" + "TỔNG HỢP TÀI SẢN CÔNG" + "\r\n" + "(Thời điểm báo cáo: "+_baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString()+")";
		}
		private void tenloaihinh_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCQH_MAU01_THTSNN _row = GetCurrentRow() as TS_BCQH_MAU01_THTSNN;
			if (_row != null)
			{
				var _thisRow = (sender as XRTableCell);
				if(_row.KIEU_TAI_SAN == (int)enumNhanKieuTaiSanBaoCao.TAI_SAN_CONG)
				switch (_row.LOAI_HINH_TAI_SAN_ID)
				{
					case 1:
						_thisRow.Text = "1- Đất";
						break;
					case 2:
						_thisRow.Text = "2- Nhà";
						break;
					case 4:
						_thisRow.Text = "3- Ô tô";
						break;
					case 499:
						_thisRow.Text = "4- Tài sản cố định khác";
						break;
					
				}
				else if (_row.KIEU_TAI_SAN == (int)enumNhanKieuTaiSanBaoCao.TAI_SAN_TOAN_DAN)
                {
					var nhan = (new enumNHAN_HIEN_THI_LOAI_HINH_TSTD().GetType().FullName) + "." + new enumNHAN_HIEN_THI_LOAI_HINH_TSTD().GetType().GetEnumName((Int32)_row.LOAI_HINH_TAI_SAN_ID);
                    //_thisRow.Text = _baoCaoTaiSanTongHopSearchModel.lstNhanHienThi.Where(c => c.Key == nhan).Select(c => c.Value).FirstOrDefault();
					if(_row.LOAI_HINH_TAI_SAN_ID == 2)
                    {
						_thisRow.Text = "1- Nhà";
					}
                    else
                    {
						_thisRow.Text = "2- Ô tô";
					}						
                }
			}
		}

		private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			donviDienTich.Text = _baoCaoTaiSanTongHopSearchModel.DonViDienTich.ToStringDonViDienTich();
			donviTien.Text = _baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien();
		}

        private void tenKieuTaiSan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCQH_MAU01_THTSNN _row = GetCurrentRow() as TS_BCQH_MAU01_THTSNN;
			if (_row != null)
			{
				var _thisRow = (sender as XRTableCell);
				var nhan = (new enumNhanKieuTaiSanBaoCao().GetType().FullName) + "." + new enumNhanKieuTaiSanBaoCao().GetType().GetEnumName((Int32)_row.KIEU_TAI_SAN);
				_thisRow.Text = _baoCaoTaiSanTongHopSearchModel.lstNhanHienThi.Where(c => c.Key == nhan).Select(c => c.Value).FirstOrDefault();
			}
		}


        

        //private void GroupHeader4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    if (decimal.Parse(tableCell60.Value.ToVNStringNumber()) <= 0)
        //        tableCell60.Row.Visible = false;
        //    else if (decimal.Parse(tableCell5.Value.ToVNStringNumber()) <= 0)
        //        tableCell5.Row.Visible = false;
        //}
        //private void GroupHeader5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    if (decimal.Parse(tableCell71.Value.ToVNStringNumber()) <= 0)
        //        tableCell71.Row.Visible = false;
        //    else if (decimal.Parse(tableCell65.Value.ToVNStringNumber()) <= 0)
        //        tableCell65.Row.Visible = false;
        //}

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCQH_MAU01_THTSNN _row = GetCurrentRow() as TS_BCQH_MAU01_THTSNN;
			if (_row != null)
			{
				var _thisRow = (sender as SubBand);
				if ((_row.SO_LUONG_TRUNG_UONG ?? 0) <= 0)
				{
					this.tableRow8.Visible = false;

				}
				else
				{
					this.tableRow8.Visible = true;
				}
				if ((_row.SO_LUONG_DIA_PHUONG ?? 0) <= 0)
				{
					this.tableRow7.Visible = false;

				}
				else
				{
					this.tableRow7.Visible = true;
				}
			}
		}


        private void tableCell46_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCQH_MAU01_THTSNN _row = GetCurrentRow() as TS_BCQH_MAU01_THTSNN;
			if (_row != null)
			{
				var _thisCell = (sender as XRTableCell);
				List<TS_BCQH_MAU01_THTSNN> data = DataSource as List<TS_BCQH_MAU01_THTSNN>;
                if (data != null)
                {
					var NguyenGiaTrungUong = data.Where(c => c.LOAI_CAP_DON_VI_ID == 1 && c.LOAI_HINH_TAI_SAN_ID == _row.LOAI_HINH_TAI_SAN_ID).Sum(c => c.NGUYEN_GIA_TRUNG_UONG) ?? 0;
					var NguyenGiaDiaPhuong = data.Where(c => c.LOAI_CAP_DON_VI_ID == 2 && c.LOAI_HINH_TAI_SAN_ID == _row.LOAI_HINH_TAI_SAN_ID).Sum(c => c.NGUYEN_GIA_DIA_PHUONG) ?? 0;
					decimal TyTrong = 0;
					if ((NguyenGiaTrungUong + NguyenGiaDiaPhuong) != 0)
					{
						TyTrong = NguyenGiaTrungUong * 100 / (NguyenGiaTrungUong + NguyenGiaDiaPhuong);
					}
					var result = "";
                    if (Math.Round(TyTrong, 2) == TyTrong)
                    {
						result = TyTrong.ToVNStringNumber();
                    }
                    else
                    {
						result = Math.Round(TyTrong, 2).ToString();

					}
					_thisCell.Text = TyTrong.ConvertDecimalToString() == "0" ? "" : TyTrong.ConvertDecimalToString();
				}
				

			}
		}

        private void tableCell40_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCQH_MAU01_THTSNN _row = GetCurrentRow() as TS_BCQH_MAU01_THTSNN;
			if (_row != null)
			{
				var _thisCell = (sender as XRTableCell);
				List<TS_BCQH_MAU01_THTSNN> data = DataSource as List<TS_BCQH_MAU01_THTSNN>;
				if (data != null)
				{
					var NguyenGiaTrungUong = data.Where(c => c.LOAI_CAP_DON_VI_ID == 1 && c.LOAI_HINH_TAI_SAN_ID == _row.LOAI_HINH_TAI_SAN_ID).Sum(c => c.NGUYEN_GIA_TRUNG_UONG) ?? 0;
					var NguyenGiaDiaPhuong = data.Where(c => c.LOAI_CAP_DON_VI_ID == 2 && c.LOAI_HINH_TAI_SAN_ID == _row.LOAI_HINH_TAI_SAN_ID).Sum(c => c.NGUYEN_GIA_DIA_PHUONG) ?? 0;
					decimal TyTrong = 0;
					if ((NguyenGiaTrungUong + NguyenGiaDiaPhuong) != 0)
					{
						TyTrong = NguyenGiaDiaPhuong * 100 / (NguyenGiaTrungUong + NguyenGiaDiaPhuong);
					}
					
					_thisCell.Text = TyTrong.ConvertDecimalToString() == "0"? "": TyTrong.ConvertDecimalToString();
				}


			}
		}

        private void tableCell44_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCQH_MAU01_THTSNN _row = GetCurrentRow() as TS_BCQH_MAU01_THTSNN;
			if (_row != null)
			{
				var _thisCell = (sender as XRTableCell);
				List<TS_BCQH_MAU01_THTSNN> data = DataSource as List<TS_BCQH_MAU01_THTSNN>;
				if (data != null)
				{
					var NguyenGiaTrungUong = data.Where(c => c.LOAI_CAP_DON_VI_ID == 1 && c.LOAI_HINH_TAI_SAN_ID == _row.LOAI_HINH_TAI_SAN_ID).Sum(c => c.DIEN_TICH_TRUNG_UONG) ?? 0;
					var NguyenGiaDiaPhuong = data.Where(c => c.LOAI_CAP_DON_VI_ID == 2 && c.LOAI_HINH_TAI_SAN_ID == _row.LOAI_HINH_TAI_SAN_ID).Sum(c => c.DIEN_TICH_DIA_PHUONG) ?? 0;
					decimal TyTrong = 0;
					if ((NguyenGiaTrungUong + NguyenGiaDiaPhuong) != 0)
					{
						TyTrong = NguyenGiaTrungUong * 100 / (NguyenGiaTrungUong + NguyenGiaDiaPhuong);
					}
					var result = "";
					if (Math.Round(TyTrong, 2) == TyTrong)
					{
						result = TyTrong.ToVNStringNumber();
					}
					else
					{
						result = Math.Round(TyTrong, 2).ToString();

					}
					_thisCell.Text = TyTrong.ConvertDecimalToString() == "0" ? "" : TyTrong.ConvertDecimalToString();
				}


			}
		}

        private void tableCell38_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCQH_MAU01_THTSNN _row = GetCurrentRow() as TS_BCQH_MAU01_THTSNN;
			if (_row != null)
			{
				var _thisCell = (sender as XRTableCell);
				List<TS_BCQH_MAU01_THTSNN> data = DataSource as List<TS_BCQH_MAU01_THTSNN>;
				if (data != null)
				{
					var NguyenGiaTrungUong = data.Where(c => c.LOAI_CAP_DON_VI_ID == 1 && c.LOAI_HINH_TAI_SAN_ID == _row.LOAI_HINH_TAI_SAN_ID).Sum(c => c.DIEN_TICH_TRUNG_UONG) ?? 0;
					var NguyenGiaDiaPhuong = data.Where(c => c.LOAI_CAP_DON_VI_ID == 2 && c.LOAI_HINH_TAI_SAN_ID == _row.LOAI_HINH_TAI_SAN_ID).Sum(c => c.DIEN_TICH_DIA_PHUONG) ?? 0;
					decimal TyTrong = 0;
					if ((NguyenGiaTrungUong + NguyenGiaDiaPhuong) != 0)
					{
						TyTrong = NguyenGiaDiaPhuong * 100 / (NguyenGiaTrungUong + NguyenGiaDiaPhuong);
					}
					var result = "";
					if (Math.Round(TyTrong, 2) == TyTrong)
					{
						result = TyTrong.ToVNStringNumber();
					}
					else
					{
						result = Math.Round(TyTrong, 2).ToString();

					}
					_thisCell.Text = TyTrong.ConvertDecimalToString() == "0" ? "" : TyTrong.ConvertDecimalToString();
				}


			}
		}

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}
	}
}
