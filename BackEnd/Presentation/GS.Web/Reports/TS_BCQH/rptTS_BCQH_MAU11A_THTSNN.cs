using System;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanTongHop;

namespace GS.Web.Reports.TS_BCQH
{
    public partial class rptTS_BCQH_MAU11A_THTSNN
    {
		private readonly BaoCaoTaiSanTongHopSearchModel _baoCaoTaiSanTongHopSearchModel;
		private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
		private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;

		public rptTS_BCQH_MAU11A_THTSNN(
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
		private void tableCell44_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			//var loaiHinh = GetCurrentColumnValue<decimal>("LOAI_HINH_TAI_SAN_ID");
			//var _thisCell = (sender as XRTableCell);
			//if (_thisCell != null)
			//{
			//	switch (loaiHinh)
			//	{
			//		case (int)enumLOAI_HINH_TAI_SAN_BAO_CAO.DAT:
			//			_thisCell.Text = "1";
			//			break;

			//		case (int)enumLOAI_HINH_TAI_SAN_BAO_CAO.NHA:
			//			_thisCell.Text = "2";
			//			break;

			//		case (int)enumLOAI_HINH_TAI_SAN_BAO_CAO.OTO:
			//			_thisCell.Text = "3";
			//			break;

			//		case (int)enumLOAI_HINH_TAI_SAN_BAO_CAO.TREN_500:
			//			_thisCell.Text = "4";
			//			break;

			//		case (int)enumLOAI_HINH_TAI_SAN_BAO_CAO.DUOI_500:
			//			_thisCell.Text = "5";
			//			break;

			//		default:
			//			break;
			//	}
			//}
		}
		//private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//{
		//	ChucDanhNguoiLapBieu.Text = _cauHinhBaoCaoModel.ChucDanhNguoiLapBieu;
		//	ChucDanhThuTruongDonVi.Text = _cauHinhBaoCaoModel.ChucDanhThuTruongDonVi;
		//	TenNguoiLapBieu.Text = _cauHinhBaoCaoModel.TenNguoiLapBieu;
		//	TenThuTruongDonVi.Text = _cauHinhBaoCaoModel.TenThuTruongDonVi;
		//	DiaDanhBaoCao.Text = _cauHinhBaoCaoModel.DiaDanhBaoCao + ",";
		//	if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhNguoiLapBieu) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenNguoiLapBieu))
		//		lb_GhiChuNLB.Visible = false;
		//	if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhThuTruongDonVi) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenThuTruongDonVi))
		//		lb_GhiChuThuTruong.Visible = false;
		//}

		private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Diện tích  là: " + _baoCaoTaiSanTongHopSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Giá trị là: " + _baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien() + ".";
		}

		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            label1.Text = "Đơn vị báo cáo: " + _baoCaoTaiSanTongHopSearchModel.TEN_DON_VI;
			tieude.Text = "Phụ lục XIa"+"\r\n" + "BẢNG TỔNG HỢP TÀI SẢN CÔNG ĐẾN NGÀY " + _baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString();
		}
		int count = 0;
		private void tableCell34_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			(sender as XRTableCell).Text = (++count).ToString();
		}

		private void rptTS_BCQH_MAU11A_THTSNN_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{

		}

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}
	}
}
