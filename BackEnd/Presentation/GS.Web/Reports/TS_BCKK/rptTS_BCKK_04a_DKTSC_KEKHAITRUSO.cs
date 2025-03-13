using System;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCKK;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;


namespace GS.Web.Reports.TS_BCKK
{
    public partial class rptTS_BCKK_04a_DKTSC_KEKHAITRUSO
    {
        private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
        public rptTS_BCKK_04a_DKTSC_KEKHAITRUSO(
            BaoCaoTaiSanChiTietSearchModel baoCaoTaiSanChiTietSearchModel,
            CauHinhBaoCaoModel cauHinhBaoCaoModel,
            CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel
            )
        {
            InitializeComponent();
            this._baoCaoTaiSanChiTietSearchModel = baoCaoTaiSanChiTietSearchModel;
            this._cauHinhBaoCaoModel = cauHinhBaoCaoModel;
            this._cauHinhBaoCaoChungModel = cauHinhBaoCaoChungModel;
			//table4.AnchorVertical = VerticalAnchorStyles.Both;
			//table1.AnchorVertical = VerticalAnchorStyles.Both;

			GroupFooter1.PageBreak = PageBreak.AfterBandExceptLastEntry;

		}

		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{

			lblDonVi.Text = "Bộ, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanChiTietSearchModel.TEN_BO_NGANH) + "\r\n" + "Cơ quan quản lý cấp trên: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI_CAP_TREN + "\r\n" + "Cơ quan, tổ chức, đơn vị sử dụng tài sản: " + _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI + "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanChiTietSearchModel.MA_DON_VI + "\r\n" + "Loại hình đơn vị: " + _baoCaoTaiSanChiTietSearchModel.TenLoaiHinhDonVi;
			label1.Text = "Thời điểm báo cáo: " + _baoCaoTaiSanChiTietSearchModel.NgayBaoCao.toDateVNString() ;
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
			DiaDanhBaoCao2.Text = _cauHinhBaoCaoModel.DiaDanhBaoCao;
			if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhNguoiLapBieu) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenNguoiLapBieu))
				lb_GhiChuNLB.Visible = false;
			if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhKeToanTruong) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenKeToanTruong))
				lb_GhiChuKeToan.Visible = false;
			if (string.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhThuTruongDonVi) && string.IsNullOrEmpty(_cauHinhBaoCaoModel.TenThuTruongDonVi))
				lb_GhiChuThuTruong.Visible = false;
		}

		private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCKK_04A_DK_TSC _row = GetCurrentRow() as TS_BCKK_04A_DK_TSC;
			if (_row!= null)
			{
				// có đất
				if (_row.TAI_SAN_DAT_ID>0)
				{
					tbcDiaChi.Text = _row.DAT_DIA_CHI;
					tbcDienTich.Text = _row.DAT_DIEN_TICH.ToVNStringNumber() + " m²";
					tbcHienTrang.Text = $"Làm trụ sở làm việc: {_row.DAT_TRU_SO_LAM_VIEC.ToVNStringNumber()} m², Làm cơ sở hoạt động sự nghiệp: {_row.DAT_HDSN.ToVNStringNumber()} m², Kinh doanh: {_row.DAT_HDSN_KD.ToVNStringNumber()} m², Cho thuê: {_row.DAT_HDSN_CHO_THUE.ToVNStringNumber()} m², Liên doanh liên kết: {_row.DAT_HDSN_LDLK.ToVNStringNumber()} m², Sử dụng khác: {_row.DAT_KHAC.ToVNStringNumber()} m²";
					tbcGiaTri.Text = $"Nguồn NS: {_row.DAT_NGUYEN_GIA_NS.ToVNStringNumber()} {_baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien().ToLower()}, Nguồn khác: {_row.DAT_NGUYEN_GIA_KHAC.ToVNStringNumber()} {_baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien().ToLower()}, Tổng cộng: {_row.DAT_NGUYEN_GIA.ToVNStringNumber()} {_baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien().ToLower()}";
				}
				else
				{
					tbcDiaChi.Text = _row.DIA_CHI_NHA;
				}
			}
		}

		private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCKK_04A_DK_TSC _row = GetCurrentRow() as TS_BCKK_04A_DK_TSC;
			if (_row != null)
			{
				if (_row.countBienDong > 1)
				{
					ckbKeKhaiLanDau.Checked = false;
					ckbKeKhaiBoSung.Checked = true;
				}
				else
				{
					ckbKeKhaiLanDau.Checked = true;
					ckbKeKhaiBoSung.Checked = false;
				}
				
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

					if (_row.HS_KHAC!= null)
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

		private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			chuthich.Text = $"ĐVT cho: Diện tích nhà là: {_baoCaoTaiSanChiTietSearchModel.DonViDienTich.ToStringDonViDienTich()}; Số lượng là: Cái, khuôn viên; Giá trị là: " + _baoCaoTaiSanChiTietSearchModel.DonViTien.ToStringDonViTien() + ".";
		}

		private void GroupFooter1_AfterPrint(object sender, EventArgs e)
		{

		}

		private void tableCell20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCKK_04A_DK_TSC _row = GetCurrentRow() as TS_BCKK_04A_DK_TSC;
			if (_row != null)
			{
				(sender as XRTableCell).Text = tbRecord.Summary.GetResult() + "- " + _row.TAI_SAN_NHA_TEN;
			}
		}
	}
}
