using System;
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
    public partial class rptTS_BCQH_MAU04_TS_LTS
    {
		private readonly BaoCaoTaiSanTongHopSearchModel _baoCaoTaiSanTongHopSearchModel;
		private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
		private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;

		public rptTS_BCQH_MAU04_TS_LTS(
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

		private void rptTS_BCQH_MAU04_TS_LTS_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            label4.Text = "Đơn vị báo cáo: " + _baoCaoTaiSanTongHopSearchModel.TEN_DON_VI;
			tieude.Text = "Phụ lục IV"+ "\r\n" + "TỔNG HỢP TÀI SẢN CÔNG"+"\r\n"+"CHIA THEO LOẠI TÀI SẢN"+"\r\n"+"(Thời điểm báo cáo: "+_baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString()+")";
		}

		private void tableCell47_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCQH_MAU04_TS_LTS _row = GetCurrentRow() as TS_BCQH_MAU04_TS_LTS;
			if (_row != null)
			{
				var _thisRow = (sender as XRTableCell);
				if (_row.KIEU_TAI_SAN == (int)enumNhanKieuTaiSanBaoCao.TAI_SAN_CONG)
					switch (_row.LOAI_HINH_TAI_SAN_ID)
					{
						case 1:
							_thisRow.Text = "1. Đất";
							break;
						case 2:
							_thisRow.Text = "2. Nhà";
							break;
						case 4:
							_thisRow.Text = "3. Ô tô";
							break;
						case 499:
							_thisRow.Text = "4. Tài sản cố định khác";
							break;
							//case 500:
							//	_thisRow.Text = "4- Tài sản có nguyên giá từ 500 triệu đồng trở lên";
							//	break;
							//case 501:
							//	_thisRow.Text = "5- Tài sản có nguyên giá dưới 500 triệu đồng";
							//	break;
					}
				else if (_row.KIEU_TAI_SAN == (int)enumNhanKieuTaiSanBaoCao.TAI_SAN_TOAN_DAN)
				{
					var nhan = (new enumNHAN_HIEN_THI_LOAI_HINH_TSTD().GetType().FullName) + "." + new enumNHAN_HIEN_THI_LOAI_HINH_TSTD().GetType().GetEnumName((Int32)_row.LOAI_HINH_TAI_SAN_ID);
					//_thisRow.Text = _baoCaoTaiSanTongHopSearchModel.lstNhanHienThi.Where(c => c.Key == nhan).Select(c => c.Value).FirstOrDefault();
					if(_row.LOAI_HINH_TAI_SAN_ID == 2)
                    {
						_thisRow.Text = "1. Nhà";
					}				
					else
                    {
						_thisRow.Text = "2. Ô tô";
					}						
					
				}
			}
		}

		private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Diện tích là: " + _baoCaoTaiSanTongHopSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Giá trị là: " + _baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien() + ".";
		}

        private void tenKieuTaiSan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCQH_MAU04_TS_LTS _row = GetCurrentRow() as TS_BCQH_MAU04_TS_LTS;
			if (_row != null)
			{
				var _thisRow = (sender as XRTableCell);
				var nhan = (new enumNhanKieuTaiSanBaoCao().GetType().FullName) + "." + new enumNhanKieuTaiSanBaoCao().GetType().GetEnumName((Int32)_row.KIEU_TAI_SAN);
				_thisRow.Text = _baoCaoTaiSanTongHopSearchModel.lstNhanHienThi.Where(c => c.Key == nhan).Select(c => c.Value).FirstOrDefault();
			}
		}

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}
	}
}
