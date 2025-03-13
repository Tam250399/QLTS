using System;
using System.Drawing;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCKK;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;

namespace GS.Web.Reports.TS_BCKK
{
	public partial class rptTS_BCKK_05a_DKTSDA_KEKHAITRUSO
	{
		private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
		private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
		private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
		public rptTS_BCKK_05a_DKTSDA_KEKHAITRUSO(
			BaoCaoTaiSanChiTietSearchModel baoCaoTaiSanChiTietSearchModel,
			CauHinhBaoCaoModel cauHinhBaoCaoModel,
			CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel
			)
		{
			InitializeComponent();
			this._baoCaoTaiSanChiTietSearchModel = baoCaoTaiSanChiTietSearchModel;
			this._cauHinhBaoCaoModel = cauHinhBaoCaoModel;
			this._cauHinhBaoCaoChungModel = cauHinhBaoCaoChungModel;

			GroupFooter1.PageBreak = PageBreak.AfterBandExceptLastEntry;
		}

		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDonVi.Text = $"Cơ quan chủ quản: {_baoCaoTaiSanChiTietSearchModel.TEN_DON_VI_CAP_TREN} \r\nTên Ban QLDA: {_baoCaoTaiSanChiTietSearchModel.TEN_DON_VI}";

			string dieuKienBaoCao = string.Empty;
			if (_baoCaoTaiSanChiTietSearchModel.DU_AN_ID>0)
			{
				dieuKienBaoCao = $"Dự án: {_baoCaoTaiSanChiTietSearchModel.tenDuAn}\r\n";
			}
			lblDieuKienBaoCao.Text = dieuKienBaoCao+"Thời điểm báo cáo: " + _baoCaoTaiSanChiTietSearchModel.NgayBaoCao.toDateVNString();
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
			if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhNguoiLapBieu) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenNguoiLapBieu))
				lb_GhiChuNLB.Visible = false;
			if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhKeToanTruong) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenKeToanTruong))
				lb_GhiChuKeToan.Visible = false;
			if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhThuTruongDonVi) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenThuTruongDonVi))
				lb_GhiChuThuTruong.Visible = false;
		}

		private void rptTS_BCKK_05a_DKTSDA_KEKHAITRUSO_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
			//if (_cauHinhBaoCaoModel.MarginLeft > 0)
			//{
			//    Margins.Left = Convert.ToInt32(_cauHinhBaoCaoModel.MarginLeft * 0.3937 * 100);
			//}
			//if (_cauHinhBaoCaoModel.MarginRight > 0)
			//{
			//    Margins.Right = Convert.ToInt32(_cauHinhBaoCaoModel.MarginRight * 0.3937 * 100);
			//}
		}

		private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			chuthich.Text = $"ĐVT cho: Diện tích nhà là: {_baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich()}; Số lượng là: Cái, khuôn viên; Giá trị là: " + _baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien() + ".";
		}

		private void tableCell19_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{


		}

		private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCKK_05A_DK_TSDA _row = GetCurrentRow() as TS_BCKK_05A_DK_TSDA;
			if (_row != null)
			{
				// có đất
				if (_row.TAI_SAN_DAT_ID > 0)
				{
					tbcDiaChi.Text = _row.DAT_DIA_CHI;
					tbcDienTich.Text = _row.DAT_DIEN_TICH.ToVNStringNumber() + " m²";
					//tbcHienTrang.Text = $"Làm trụ sở làm việc: {_row.DAT_TRU_SO_LAM_VIEC.ToVNStringNumber()} m², Làm cơ sở hoạt động sự nghiệp: {_row.DAT_HDSN.ToVNStringNumber()} m², Kinh doanh: {_row.DAT_HDSN_KD.ToVNStringNumber()} m², Cho thuê: {_row.DAT_HDSN_CHO_THUE.ToVNStringNumber()} m², Liên doanh, liên kết: {_row.DAT_HDSN_LDLK.ToVNStringNumber()} m², Sử dụng khác: {_row.DAT_KHAC.ToVNStringNumber()} m²";
					tbcHienTrang.Text = $"Làm trụ sở làm việc: {_row.DAT_TRU_SO_LAM_VIEC.ToVNStringNumber()} m², Sử dụng khác: {(_row.DAT_HDSN + _row.DAT_HDSN_KD + _row.DAT_HDSN_CHO_THUE + _row.DAT_HDSN_LDLK + _row.DAT_KHAC).ToVNStringNumber()} m²";
					tbcGiaTri.Text = $"Nguồn NSNN: {_row.DAT_NGUYEN_GIA_NS.ToVNStringNumber()} đồng, Nguồn khác: {_row.DAT_NGUYEN_GIA_KHAC.ToVNStringNumber()} đồng, Tổng cộng: {_row.DAT_NGUYEN_GIA.ToVNStringNumber()} đồng";
				}
				else
				{
					tbcDiaChi.Text = _row.DIA_CHI_NHA;
				}
			}
		}

		private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCKK_05A_DK_TSDA _row = GetCurrentRow() as TS_BCKK_05A_DK_TSDA;
			if (_row != null)
			{
				// có đất
				if (_row.TAI_SAN_DAT_ID > 0)
				{
					if (_row.HS_CNQSD_NGAY.HasValue)
					{
						rowGiayCNSDDat.Visible = true;
						lblHSCNQSDDatValue.Text = $"Số {_row.HS_CNQSD_SO}, ngày {_row.HS_CNQSD_NGAY.toDateVNString()}";
					}
					else
					{
						rowGiayCNSDDat.Visible = false;
					}
					if (_row.HS_QUYET_DINH_GIAO_NGAY.HasValue)
					{
						rowQDGiaoDat.Visible = true;
						lblQDGiaoDatValue.Text = $"Số {_row.HS_QUYET_DINH_GIAO_SO}, ngày {_row.HS_QUYET_DINH_GIAO_NGAY.toDateVNString()}";
					}
					else
					{
						rowQDGiaoDat.Visible = false;
					}

					if (_row.HS_QUYET_DINH_CHO_THUE_NGAY.HasValue)
					{
						rowQDChoThue.Visible = true;
						lblQDChoThueDatValue.Text = $"Số {_row.HS_QUYET_DINH_CHO_THUE_SO}, ngày {_row.HS_QUYET_DINH_CHO_THUE_NGAY.toDateVNString()}";
					}
					else
					{
						rowQDChoThue.Visible = false;
					}

					if (_row.HS_HOP_DONG_CHO_THUE_NGAY.HasValue)
					{
						rowHDChoThue.Visible = true;
						lblHDChoThueDatValue.Text = $"Số {_row.HS_HOP_DONG_CHO_THUE_SO}, ngày {_row.HS_HOP_DONG_CHO_THUE_NGAY.toDateVNString()}";
					}
					else
					{
						rowHDChoThue.Visible = false;
					}

					if (_row.HS_KHAC != null)
					{
						rowKhac.Visible = true;
						lblKhacValue.Text = $"{_row.HS_KHAC}";
					}
					else
					{
						rowKhac.Visible = false;
					}
				}
				else
				{
					rowGiayCNSDDat.Visible = false;
					rowQDGiaoDat.Visible = false;
					rowQDChoThue.Visible = false;
					rowHDChoThue.Visible = false;
					rowKhac.Visible = false;

				}
			}
		}
	}
}
