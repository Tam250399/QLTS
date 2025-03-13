using System;
using System.Drawing;
using System.Linq;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TSTD;
using GS.Core.Domain.SHTD;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;
using GS.Web.Models.SHTD;

namespace GS.Web.Reports.TaiSanToanDan
{
    public partial class TSTD_KeKhaiThongTin
    {
        private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
		private int Cap1;
		private int Cap2;
		public TSTD_KeKhaiThongTin(
            BaoCaoTaiSanChiTietSearchModel baoCaoTaiSanChiTietSearchModel,
            CauHinhBaoCaoModel cauHinhBaoCaoModel,
            CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel)
        {
            InitializeComponent();
			this.Cap1 = 0;
			this.Cap2 = 0;
			this._baoCaoTaiSanChiTietSearchModel = baoCaoTaiSanChiTietSearchModel;
            this._cauHinhBaoCaoModel = cauHinhBaoCaoModel;
            this._cauHinhBaoCaoChungModel = cauHinhBaoCaoChungModel;
            this.ExportOptions.Xls.FitToPrintedPageWidth = true;
            this.ExportOptions.Xlsx.FitToPrintedPageWidth = true;
            this.ExportOptions.Xls.ExportMode = _baoCaoTaiSanChiTietSearchModel.enumDinhDanhXlsExcel;
            this.ExportOptions.Xlsx.ExportMode = _baoCaoTaiSanChiTietSearchModel.enumDinhDanhXlsxExcel;
        }

		#region Cấu hình
		private void TSTD_KeKhaiThongTin_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
		#endregion
		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{					
			lblDonVi.Text = "CƠ QUAN QUẢN LÝ CẤP TRÊN: "+_baoCaoTaiSanChiTietSearchModel.TEN_DON_VI_CAP_TREN.ToUpper() + "\r\n"+"CƠ QUAN CHỦ TRÌ QUẢN LÝ TÀI SẢN: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI.ToUpper();
			lblDieuKien.Text = "Nguồn gốc tài sản: " + _baoCaoTaiSanChiTietSearchModel.TenTaiSan;
			lblChiTiet.Text = 
				$"I. Thời hạn báo cáo: 7 ngày làm việc, kể từ ngày ban hành Quyết định tịch thu/Quyết định xác lập quyền sở hữu toàn dân về tài sản." +
				$"\r\nII. Nội dung báo cáo:" +
				$"\r\n1. Quyết định xác lập sở hữu toàn dân số {_baoCaoTaiSanChiTietSearchModel.SoQuyetDinhTichThu??"...."} ngày {_baoCaoTaiSanChiTietSearchModel.NgayQuyetDinhTichThuString ?? "...."} của {_baoCaoTaiSanChiTietSearchModel.NguoiQuyetDinh ?? "...."} " +
				$"\r\n2.Thông tin về tài sản: ";
		}
		private void ReportFooter_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
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

		private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			chuthich.Text = "ĐVT giá trị là: " + _baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien() + ".";
		}

		private void stt1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_01_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_01_BC_XLSHTD;
			var _this = sender as XRTableCell;
			if (_row != null && _row.CAP_1 >= 0)
			{
				Cap2 = 0;
			}
			if (_row != null)
				Cap1 += 1;
			_this.Text = Cap1.ToString();
		}

		private void tencap1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			//TSTD_MAU_01_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_01_BC_XLSHTD;
			//var _this = sender as XRTableCell;
			//if (_row != null)
			//{
			//	switch (_row.LOAI_HINH_TAI_SAN_ID)
			//	{
			//		case (int)enumNHAN_HIEN_THI_LOAI_HINH_TSTD.DAT:
			//			_this.Text = _baoCaoTaiSanChiTietSearchModel.lstNhanHienThi.Where(c => c.Key.Contains(enumNHAN_HIEN_THI_LOAI_HINH_TSTD.DAT.GetType().FullName)).Select(c => c.Value).FirstOrDefault();
			//			break;
			//		case (int)enumNHAN_HIEN_THI_LOAI_HINH_TSTD.NHA:
			//			_this.Text = _baoCaoTaiSanChiTietSearchModel.lstNhanHienThi.Where(c => c.Key.Contains(enumNHAN_HIEN_THI_LOAI_HINH_TSTD.NHA.GetType().FullName)).Select(c => c.Value).FirstOrDefault();
			//			break;
			//		case (int)enumNHAN_HIEN_THI_LOAI_HINH_TSTD.OTO:
			//			_this.Text = _baoCaoTaiSanChiTietSearchModel.lstNhanHienThi.Where(c => c.Key.Contains(enumNHAN_HIEN_THI_LOAI_HINH_TSTD.OTO.GetType().FullName)).Select(c => c.Value).FirstOrDefault();
			//			break;
			//		case (int)enumNHAN_HIEN_THI_LOAI_HINH_TSTD.TAI_SAN_KHAC:
			//			_this.Text = _baoCaoTaiSanChiTietSearchModel.lstNhanHienThi.Where(c => c.Key.Contains(enumNHAN_HIEN_THI_LOAI_HINH_TSTD.TAI_SAN_KHAC.GetType().FullName)).Select(c => c.Value).FirstOrDefault();
			//			break;
			//	}
			//}
				
		}
		private void cap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_01_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_01_BC_XLSHTD;
			var _this = sender as GroupHeaderBand;
			if (_row != null && _row.CAP_1 == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT)
			{
				_this.Visible = true;
			}
			else
			{
				_this.Visible = false;
			}
		}
		private void stt2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_01_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_01_BC_XLSHTD;
			var _this = sender as XRTableCell;
			if (_row != null && _row.CAP_1 == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT)
				Cap2 += 1;
			if (_row != null && _row.CAP_1 == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT)
				_this.Text = Cap1.ToString() + "." + Cap2.ToString();
		}

		private void tencap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_01_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_01_BC_XLSHTD;
			var _this = sender as XRTableCell;
			if (_row != null)
			_this.Text = "Đất và tài sản gắn liền với đất " + Cap2.ToString();
			
		}
		private void sttDetail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{

        }

        private void ThongTin_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TSTD_MAU_01_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_01_BC_XLSHTD;
			var _this = sender as XRTableCell;
			if (_row != null)
			{
				switch (_row.LOAI_HINH_TAI_SAN_ID)
				{
					case (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT:
							_this.Text = "- Địa chỉ: " + _row.TAI_SAN_TEN + ";\r\n" + "- Loại đất: " + _row.LOAI_TAI_SAN_TEN + ";\r\n" + "- Diện tích: " + _row.GIA_TRI_TINH.ToVNStringNumber(true) + " m2";
						break;
					case (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA:
							_this.Text = "- Nhà, công trình: " + _row.TAI_SAN_TEN + (_row.TAI_SAN_DAT_ID != null ? " thuộc khuôn viên đất;" : " không có đất;") + "\r\n" + "- Địa chỉ: " + _row.DIA_CHI + ";\r\n" + "- Diện tích: " + _row.GIA_TRI_TINH.ToVNStringNumber(true) + " m2" + ";\r\n" + "- Năm đưa vào sử dụng: " + _row.NAM_SU_DUNG + ";\r\n" + "- Cấp/hạng nhà, công trình: " + _row.LOAI_TAI_SAN_TEN;
						break;
					case (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.OTO:
							_this.Text = "- Biển số: " + _row.BIEN_KIEM_SOAT + ";\r\n" + "- Nhãn hiệu: " + _row.NHAN_XE_TEN + ";\r\n" + "- Số loại: " + _row.TAI_SAN_TEN + ";\r\n" + "- Số chỗ ngồi/tải trọng/Chủng loại: " + _row.SO_CHO_NGOI;
						break;
					case (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.TAI_SAN_KHAC:
							_this.Text = "- Tên tài sản (mặt hàng): " + _row.TAI_SAN_TEN;
						break;
				}
			}
				
		}

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}

		//private void dv1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TSTD_MAU_01_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_01_BC_XLSHTD;
		//	var _thisRow = (sender as GroupHeaderBand);
		//	if (_row == null || _row.DV_1 == null || _row.LA_DON_VI_NHAP_LIEU ==1)
		//	{
		//		_thisRow.Visible = false;
		//	}
		//	else
		//	{
		//		_thisRow.Visible = true;
		//	}
		//}

		//private void dv2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TSTD_MAU_01_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_01_BC_XLSHTD;
		//	var _thisRow = (sender as GroupHeaderBand);
		//	if (_row == null || _row.DV_2 == null || _row.LA_DON_VI_NHAP_LIEU == 1)
		//	{
		//		_thisRow.Visible = false;
		//	}
		//	else
		//	{
		//		_thisRow.Visible = true;
		//	}
		//}

		//private void dv3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TSTD_MAU_01_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_01_BC_XLSHTD;
		//	var _thisRow = (sender as GroupHeaderBand);
		//	if (_row == null || _row.DV_3 == null || _row.LA_DON_VI_NHAP_LIEU == 1)
		//	{
		//		_thisRow.Visible = false;
		//	}
		//	else
		//	{
		//		_thisRow.Visible = true;
		//	}
		//}

		//private void dv4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TSTD_MAU_01_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_01_BC_XLSHTD;
		//	var _thisRow = (sender as GroupHeaderBand);
		//	if (_row == null || _row.DV_4 == null || _row.LA_DON_VI_NHAP_LIEU == 1)
		//	{
		//		_thisRow.Visible = false;
		//	}
		//	else
		//	{
		//		_thisRow.Visible = true;
		//	}
		//}

		//private void dv5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TSTD_MAU_01_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_01_BC_XLSHTD;
		//	var _thisRow = (sender as GroupHeaderBand);
		//	if (_row == null || _row.DV_5 == null || _row.LA_DON_VI_NHAP_LIEU == 1)
		//	{
		//		_thisRow.Visible = false;
		//	}
		//	else
		//	{
		//		_thisRow.Visible = true;
		//	}
		//}

		//private void tendv1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TSTD_MAU_01_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_01_BC_XLSHTD;
		//	string _rowTen = GetCurrentColumnValue("DV_TEN_1") as string;
		//	var _this = sender as XRTableCell;
		//	if (_row.TREE_LEVEL_DV >= 1)
		//	{
		//		Cap2 = 0;
		//		Cap3 = 0;
		//		Cap4 = 0;
		//		Cap5 = 0;
		//	}			
		//		Cap1 += 1;
		//	_this.Text = Cap1.ToString() + ". " + _rowTen;
		//}

		//private void tendv2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TSTD_MAU_01_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_01_BC_XLSHTD;
		//	string _rowTen = GetCurrentColumnValue("DV_TEN_2") as string;
		//	var _this = sender as XRTableCell;
		//	if (_row.TREE_LEVEL_DV >= 2)
		//	{
		//		Cap3 = 0;
		//		Cap4 = 0;
		//		Cap5 = 0;
		//	}
		//	if (_row.DV_2 != null)
		//		Cap2 += 1;
		//	_this.Text = Cap1.ToString() + "." + Cap2.ToString() + ". " + _rowTen;
		//}

		//private void tendv3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TSTD_MAU_01_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_01_BC_XLSHTD;
		//	string _rowTen = GetCurrentColumnValue("DV_TEN_3") as string;
		//	var _this = sender as XRTableCell;
		//	if (_row.TREE_LEVEL_DV >= 3)
		//	{
		//		Cap4 = 0;
		//		Cap5 = 0;
		//	}
		//	if (_row.DV_3 != null)
		//		Cap3 += 1;
		//	_this.Text = Cap1.ToString() + "." + Cap2.ToString() + "." + Cap3.ToString() + ". " + _rowTen;
		//}

		//private void tendv4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TSTD_MAU_01_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_01_BC_XLSHTD;
		//	string _rowTen = GetCurrentColumnValue("DV_TEN_4") as string;
		//	var _this = sender as XRTableCell;
		//	if (_row.TREE_LEVEL_DV >= 4)
		//	{
		//		Cap5 = 0;
		//	}
		//	if (_row.DV_4 != null)
		//		Cap4 += 1;
		//	_this.Text = Cap1.ToString() + "." + Cap2.ToString() + "." + Cap3.ToString() + "." + Cap4.ToString() + ". " + _rowTen;
		//}

		//private void tendv5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	TSTD_MAU_01_BC_XLSHTD _row = GetCurrentRow() as TSTD_MAU_01_BC_XLSHTD;
		//	string _rowTen = GetCurrentColumnValue("DV_TEN_5") as string;
		//	var _this = sender as XRTableCell;
		//	if (_row.DV_5 != null)
		//		Cap5 += 1;
		//	_this.Text = Cap1.ToString() + "." + Cap2.ToString() + "." + Cap3.ToString() + "." + Cap4.ToString() + "." + Cap5.ToString() + ". " + _rowTen;
		//}
	}
}
