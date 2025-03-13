using System;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCDA;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanTongHop;

namespace GS.Web.Reports.TS_BCDA
{
    public partial class rptTS_BCDA_04A_TRUSODENGHIXULY
    {
		private readonly BaoCaoTaiSanTongHopSearchModel _baoCaoTaiSanTongHopSearchModel;
		private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
		private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
		public rptTS_BCDA_04A_TRUSODENGHIXULY(
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
			GroupFooter1.PageBreak = PageBreak.AfterBandExceptLastEntry;
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

		private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			chuthich.Text = $"ĐVT cho: Diện tích là: {_baoCaoTaiSanTongHopSearchModel.DonViDienTich.ToStringDonViDienTich()}; Giá trị là: {_baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien()}.";
		}

		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			leftHeader.Text = $"Cơ quan chủ quản: {_baoCaoTaiSanTongHopSearchModel.TEN_DON_VI_CHA} \r\nTên Ban QLDA: {_baoCaoTaiSanTongHopSearchModel.TEN_DON_VI}";
			string dieuKienBaoCao = string.Empty;
			if (_baoCaoTaiSanTongHopSearchModel.DuAnId > 0)
			{
				dieuKienBaoCao = $"Dự án: {_baoCaoTaiSanTongHopSearchModel.tenDuAn}\r\n";
			}
			lblDieuKien.Text = $"{dieuKienBaoCao}Thời điểm báo cáo: {_baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString()}";
		}

		private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCDA_04A_TRUSODENGHIXULY _row = GetCurrentRow() as TS_BCDA_04A_TRUSODENGHIXULY;
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

		private void groupDat_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCDA_04A_TRUSODENGHIXULY _row = GetCurrentRow() as TS_BCDA_04A_TRUSODENGHIXULY;
			if (_row != null)
			{
				// có đất
				if (_row.TAI_SAN_DAT_ID > 0)
				{
					tbcDiaChi.Text = _row.DAT_DIA_CHI;
					tbcDienTich.Text = _row.DAT_DIEN_TICH.ToVNStringNumber() + " m²";
					tbcHienTrang.Text = $"Làm trụ sở làm việc: {_row.DAT_TRU_SO_LAM_VIEC.ToVNStringNumber()} m², Làm cơ sở hoạt động sự nghiệp: {_row.DAT_HDSN.ToVNStringNumber()} m², Kinh doanh: {_row.DAT_HDSN_KD.ToVNStringNumber()} m², Cho thuê: {_row.DAT_HDSN_CHO_THUE.ToVNStringNumber()} m², Liên doanh liên kết: {_row.DAT_HDSN_LDLK.ToVNStringNumber()} m², Sử dụng khác: {_row.DAT_KHAC.ToVNStringNumber()} m²";
					//tbcGiaTri.Text = $"Nguồn NS: {_row.NHA_NGUYEN_GIA_NS.ToVNStringNumber()} {_baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien().ToLower()}, Nguồn khác: {_row.NHA_NGUYEN_GIA_KHAC.ToVNStringNumber()} {_baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien().ToLower()}, Tổng cộng: {_row.NHA_NGUYEN_GIA.ToVNStringNumber()} {_baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien().ToLower()}";
				}
				else
				{
					tbcDiaChi.Text = _row.DIA_CHI_NHA;
				}
			}
		}
	}
}
