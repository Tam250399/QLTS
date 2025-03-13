using System;
using System.Linq;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCTH;
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanTongHop;

namespace GS.Web.Reports.TS_BCTH
{
    public partial class rptTS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH
    {
        private readonly BaoCaoTaiSanTongHopSearchModel _baoCaoTaiSanTongHopSearchModel;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
		private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
		private int STT_DV;
		private int STT_DV_1;
		private int STT_DV_2;

		public rptTS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH(
			BaoCaoTaiSanTongHopSearchModel baoCaoTaiSanTongHopSearchModel,
			CauHinhBaoCaoModel cauHinhBaoCaoModel,
			CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel
			)
		{
			InitializeComponent();
			this._baoCaoTaiSanTongHopSearchModel = baoCaoTaiSanTongHopSearchModel;
			this._cauHinhBaoCaoModel = cauHinhBaoCaoModel;
			this._cauHinhBaoCaoChungModel = cauHinhBaoCaoChungModel;
			this.ExportOptions.Xls.ExportMode = _baoCaoTaiSanTongHopSearchModel.enumDinhDanhXlsExcel;
			this.ExportOptions.Xlsx.ExportMode = _baoCaoTaiSanTongHopSearchModel.enumDinhDanhXlsxExcel;
			this.ExportOptions.Xls.FitToPrintedPageWidth = true;
			this.ExportOptions.Xlsx.FitToPrintedPageWidth = true;
			STT_DV_1 = 0;
			STT_DV = 0;
			STT_DV_2 = 0;
		}
		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDonVi.Text = "Bộ, ngành, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanTongHopSearchModel.TEN_BO_NGANH) + "\r\n" + "Tên đơn vị: " + _baoCaoTaiSanTongHopSearchModel.TEN_DON_VI + "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanTongHopSearchModel.MA_DON_VI;
			chuthich.Text = "ĐVT: " + _baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien() + ".";
			label2.Text = $"BÁO CÁO CUNG CẤP THÔNG TIN TÀI CHÍNH \r\n  NĂM {_baoCaoTaiSanTongHopSearchModel.NamBaoCao.ToString()}";
		}
		private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			ChucDanhNguoiLapBieu.Text = _cauHinhBaoCaoModel.ChucDanhNguoiLapBieu;
			ChucDanhKeToanTruong.Text = _cauHinhBaoCaoModel.ChucDanhKeToanTruong;
			ChucDanhThuTruongDonVi.Text = _cauHinhBaoCaoModel.ChucDanhThuTruongDonVi;
			TenNguoiLapBieu.Text = _cauHinhBaoCaoModel.TenNguoiLapBieu;
			TenKeToanTruong.Text = _cauHinhBaoCaoModel.TenKeToanTruong;
			TenThuTruongDonVi.Text = _cauHinhBaoCaoModel.TenThuTruongDonVi;
			DiaDanhBaoCao.Text = _cauHinhBaoCaoModel.DiaDanhBaoCao?.Trim();
			if (String.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhNguoiLapBieu))
				lb_GhiChuNLB.Visible = false;
			if (String.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhKeToanTruong))
				lb_GhiChuKeToan.Visible = false;
			if (String.IsNullOrEmpty(_cauHinhBaoCaoModel.ChucDanhThuTruongDonVi))
				lb_GhiChuThuTruong.Visible = false;
		}
		#region CauHinh
		private void GroupHeader_TieuDeBang_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			var _thisBand = (sender as GroupBand);
			
            if (_baoCaoTaiSanTongHopSearchModel.DonVi != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
            {
				_thisBand.Visible = false;
            }
            else
            {
				_thisBand.Visible = true;
			}
		}
		#endregion

		private void cellSoDauNam1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			var _thisCell = (sender as XRTableCell);
			if (_baoCaoTaiSanTongHopSearchModel.NamBaoCao > 0)
			{
				var firstDay = new DateTime((int)_baoCaoTaiSanTongHopSearchModel.NamBaoCao, 1, 1);
				_thisCell.Text = $"Số đầu năm (tại ngày {firstDay.toDateVNString()})";
			}
			else
			{
				_thisCell.Text = "Số đầu năm ";
			}
		}

		private void cellSoCuoiNam1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			var _thisCell = (sender as XRTableCell);
			if (_baoCaoTaiSanTongHopSearchModel.NamBaoCao > 0)
			{
				var lastDay = new DateTime((int)_baoCaoTaiSanTongHopSearchModel.NamBaoCao, 12, 31);
				_thisCell.Text = $"Số cuối năm (tại ngày {lastDay.toDateVNString()})";
			}
			else
			{
				_thisCell.Text = "Số cuối năm ";
			}
		}

		private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH _row = GetCurrentRow() as TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH;
			if (_row != null)
			{
				
					var _thisRow = (sender as DetailBand);
					_thisRow.Visible = false;
				
			}
		}
        private void TenDonViCap1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH _row = GetCurrentRow() as TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH;
			if (_row != null)
			{
				var _this = sender as XRTableCell;
				if (_row.DV_TEN_1 != null)
				{
					_this.Text = $"{_row.DV_TEN_1}";
				}
			}
		}
		private void STTDonViCap1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH _row = GetCurrentRow() as TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH;
			if (_row != null)
			{
				var _this = sender as XRTableCell;
				if (_row.DV_TEN_1 != null)
				{
					 STT_DV_1 += 1;
					_this.Text = $"{STT_DV_1}";
					STT_DV_2 = 0;
				}
			}
		}
		private void TenDonViCap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH _row = GetCurrentRow() as TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH;
			if (_row != null)
			{
				var _this = sender as XRTableCell;
				if (_row.DV_TEN_2 != null)
				{

					_this.Text = $"{_row.DV_TEN_2}";
				}
			}
		}
		private void STTDonViCap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH _row = GetCurrentRow() as TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH;
			if (_row != null)
			{
				var _this = sender as XRTableCell;
				if (_row.DV_TEN_2 != null)
				{
					STT_DV_2 += 1;
					_this.Text = $"{STT_DV_1}.{STT_DV_2}";
					STT_DV = 0;
				}
			}
		}
		private void NguyenGiaDonViCap1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH _row = GetCurrentRow() as TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH;
			if (_row != null)
			{
				var _this = sender as XRTableCell;

					_this.Text = $"{STT_DV_1}.1";
			}
		}

        private void HMKHDonViCap1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH _row = GetCurrentRow() as TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH;
			if (_row != null)
			{
				var _this = sender as XRTableCell;

				_this.Text = $"{STT_DV_1}.2";
			}
		}
		private void GTCLDonViCap1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH _row = GetCurrentRow() as TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH;
			if (_row != null)
			{
				var _this = sender as XRTableCell;

				_this.Text = $"{STT_DV_1}.3";
			}
		}

		private void NguyenGiaDonViCap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH _row = GetCurrentRow() as TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH;
			if (_row != null)
			{
				var _this = sender as XRTableCell;

				_this.Text = $"{STT_DV_1}.{STT_DV_2}.1";
			}
		}

		private void HMKHDonViCap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH _row = GetCurrentRow() as TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH;
			if (_row != null)
			{
				var _this = sender as XRTableCell;

				_this.Text = $"{STT_DV_1}.{STT_DV_2}.2";
			}
		}
		private void GTCLDonViCap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH _row = GetCurrentRow() as TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH;
			if (_row != null)
			{
				var _this = sender as XRTableCell;

				_this.Text = $"{STT_DV_1}.{STT_DV_2}.3";
			}
		}

        private void Cap2Band_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH _row = GetCurrentRow() as TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH;
			if (_row == null || _row.DV_2 == null|| _baoCaoTaiSanTongHopSearchModel.BacDonVi < 2 )
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

        private void MaDonViCap1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH _row = GetCurrentRow() as TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH;
			var _this = sender as XRTableCell;
			if (_row != null)
			{

				_this.Text = _row.DV_1;
			}
		}

        private void MaDonViCap2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH _row = GetCurrentRow() as TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH;
			var _this = sender as XRTableCell;
			if (_row != null)
			{

				_this.Text = _row.DV_2;
			}
		}

        private void GroupHeader3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void DVT_CAP1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			var _this = (sender as XRLabel);
			var dvt = "ĐVT: " + _baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien() + ".";
			if (_baoCaoTaiSanTongHopSearchModel.DonVi != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
			{
				_this.Text = dvt;
			}
			else
			{
				_this.Text = "";
			}
		}

        private void lblDonViCap1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			var _this = (sender as XRLabel);

			if (_baoCaoTaiSanTongHopSearchModel.DonVi != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
			{
				_this.Visible = true;
			}
			else
			{
				_this.Visible = false;
			}
		}

        private void lblDieuKien_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			if (_baoCaoTaiSanTongHopSearchModel.MA_DON_VI == "018999")
			{
				lblDieuKien.Text = $" Cấp hành chính: { (_baoCaoTaiSanTongHopSearchModel.StringCapHanhChinh != null ? _baoCaoTaiSanTongHopSearchModel.lstNhanHienThi.Where(c => c.Key == ("List." + new enumTinhHuyenXaTrungUong().GetType().FullName)).Select(c => c.Value).FirstOrDefault() : "Tất cả")} \r\n Bậc đơn vị: {_baoCaoTaiSanTongHopSearchModel.BacDonVi}";
			}

			else
			{
				lblDieuKien.Text = $" Bậc đơn vị: {_baoCaoTaiSanTongHopSearchModel.BacDonVi}";

			}
		}

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}
	}
}
