using System;
using System.Linq;
using System.Text;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCTC;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;

namespace GS.Web.Reports.TS_BCTC
{
    public partial class rptTS_BCTC_04_DK_TSNN
    {
		private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanTongHopSearchModel;
		private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
		private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
		private int Cap1;
		private int Cap2;
		private int Cap3;
		private int Cap4;
		private int Cap5;
		private int stt = 0;
		private int stt1 = 0;
		private int stt2 = 0;
		private int stt3 = 0;
		private int stt4 = 0;

		public rptTS_BCTC_04_DK_TSNN(
			BaoCaoTaiSanChiTietSearchModel baoCaoTaiSanTongHopSearchModel,
			CauHinhBaoCaoModel cauHinhBaoCaoModel,
			CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel
			)
        {
            InitializeComponent();
			this._baoCaoTaiSanTongHopSearchModel = baoCaoTaiSanTongHopSearchModel;
			this._cauHinhBaoCaoModel = cauHinhBaoCaoModel;
			this._cauHinhBaoCaoChungModel = cauHinhBaoCaoChungModel;
        }
		
		private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
			if (_row != null)
			{
				var _thisRow = (sender as DetailBand);
				if (_baoCaoTaiSanTongHopSearchModel.BacTaiSan < 5)
					_thisRow.Visible = false;
				else
					_thisRow.Visible = true;
			}
		}
		private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{

			lblDonVi.Text = "Bộ, tỉnh: " + (_cauHinhBaoCaoModel.DonViKhaiThac != null ? _cauHinhBaoCaoModel.DonViKhaiThac : _baoCaoTaiSanTongHopSearchModel.TEN_BO_NGANH) + "\r\n" + "Tên đơn vị: " + _baoCaoTaiSanTongHopSearchModel.TEN_DON_VI + "\r\n" + "Mã đơn vị: " + _baoCaoTaiSanTongHopSearchModel.MA_DON_VI;
			string text = "Thời điểm báo cáo: " + _baoCaoTaiSanTongHopSearchModel.NgayKetThuc.toDateVNString() + "\r\n" + _baoCaoTaiSanTongHopSearchModel.StringLoaiTaiSan.ToStringLoaiHinhTaiSanFromString() + "\r\n" + "Chi tiết bậc đơn vị: " + _baoCaoTaiSanTongHopSearchModel.BacDonVi + ";" + " Bậc tài sản: " + _baoCaoTaiSanTongHopSearchModel.BacTaiSan;
			if (!string.IsNullOrEmpty(_baoCaoTaiSanTongHopSearchModel.TenLoaiDonVi))
			{
				text += "\r\nLoại đơn vị: " + _baoCaoTaiSanTongHopSearchModel.TenLoaiDonVi;
			}
			if (_baoCaoTaiSanTongHopSearchModel.MA_DON_VI == "018999")
			{
				text += "\r\n" + "Cấp hành chính: " + (_baoCaoTaiSanTongHopSearchModel.StringCapHanhChinh != null ? _baoCaoTaiSanTongHopSearchModel.lstNhanHienThi.Where(c => c.Key == ("List." + new enumTinhHuyenXaTrungUong().GetType().FullName)).Select(c => c.Value).FirstOrDefault() : "Tất cả");
			}
			else
			{
				text += "\r\n" + "Cấp hành chính: " + (_baoCaoTaiSanTongHopSearchModel.StringCapHanhChinh != null ? _baoCaoTaiSanTongHopSearchModel.lstNhanHienThi.Where(c => c.Key == ("List." + new enumTinhHuyenXa().GetType().FullName)).Select(c => c.Value).FirstOrDefault() : "Tất cả");
			}
			//text += "\r\nCấp hành chính: " + _baoCaoTaiSanTongHopSearchModel.TenCapDonViTinhHuyenXa;
			ngayandbactaisan.Text = text;
		}
		
		private void TenCot_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			chuthich.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Diện tích đất là: " + _baoCaoTaiSanTongHopSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Diện tích nhà là: " + _baoCaoTaiSanTongHopSearchModel.DonViDienTich.ToStringDonViDienTich() + "; Giá trị là: " + _baoCaoTaiSanTongHopSearchModel.DonViTien.ToStringDonViTien() + ".";
		}
		private void DonViTen1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
			if (_row == null )
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

		private void DonViTen2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
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
		private void DonViTen3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
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
		private void DonViTen4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
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
		private void DonViTen5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
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
		private void ReporFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			
			ChucDanhNguoiLapBieu.Text = _cauHinhBaoCaoModel.ChucDanhNguoiLapBieu;
			ChucDanhKeToanTruong.Text = _cauHinhBaoCaoModel.ChucDanhKeToanTruong;
			ChucDanhThuTruongDonVi.Text = _cauHinhBaoCaoModel.ChucDanhThuTruongDonVi;
			TenNguoiLapBieu.Text = _cauHinhBaoCaoModel.TenNguoiLapBieu;
			TenKeToanTruong.Text = _cauHinhBaoCaoModel.TenKeToanTruong;
			TenThuTruongDonVi.Text = _cauHinhBaoCaoModel.TenThuTruongDonVi;
			DiaDanhBaoCao.Text = _cauHinhBaoCaoModel.DiaDanhBaoCao;
		}

		private void taisanTen_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
			if (_row!= null)
			{
				var _thisCell = (sender as XRTableCell);
				StringBuilder @string = new StringBuilder("   ");
				for (int level = 0; level < _row.TREE_LEVEL; level++)
					@string.Append(" ");
				_thisCell.Text = @string.ToString() + _row.TAI_SAN_TEN;
			}
		}
		
		private void ten1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
			if (_row != null)
			{
				string _rowTen = GetCurrentColumnValue("TEN_1") as string;
				var _this = sender as XRTableCell;
				if (_row.TEN_1 != null)
				{
					stt += 1;
				}
				_this.Text = stt.ToString() + ". " + _rowTen;
				stt1 = 0;
			}
		}
		private void ten2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
			if (_row != null)
			{
				string TEN_2 = GetCurrentColumnValue("TEN_2") as string;
				var _this = sender as XRTableCell;
				if (_row.TEN_2 != null)
				{
					stt1 += 1;
				}
				stt2 = 0;
				_this.Text = stt.ToString()+"." + stt1.ToString() + ". "+ TEN_2;
			}
		}
		private void ten3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
			if (_row != null)
			{
				string TEN_3 = GetCurrentColumnValue("TEN_3") as string;
				var _this = sender as XRTableCell;
				if (_row.TEN_3 != null)
				{
					stt2 += 1;
				}
				stt3 = 0;
				_this.Text = stt.ToString() + "." + stt1.ToString() + "." + stt2.ToString() + ". " + TEN_3;
			}
		}
		private void ten4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
			if (_row != null)
			{
				string TEN_4 = GetCurrentColumnValue("TEN_4") as string;
				var _this = sender as XRTableCell;
				if (_row.TEN_4 != null)
				{
					stt3 += 1;
				}
				stt4 = 0;
				_this.Text = stt.ToString() + "." + stt1.ToString() + "." + stt2.ToString() + "." + stt3.ToString() + ". " + TEN_4;
			}
		}
		private void ten5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
			if (_row != null)
			{
				string TEN_5 = GetCurrentColumnValue("TEN_5") as string;
				var _this = sender as XRTableCell;
				if (_row.TEN_5 != null)
				{
					stt4 += 1;
				}
				_this.Text = stt.ToString() + "." + stt1.ToString() + "." + stt2.ToString() + "." + stt3.ToString() + "." + stt4.ToString() + ". " + TEN_5;
			}
		}
       

        private void GroupHeader7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
			string Cap_2 = GetCurrentColumnValue<string>("CAP_2");
			var _thisRow = (sender as GroupHeaderBand);
			if (Cap_2 == null || Cap_2.Length < 4 || _baoCaoTaiSanTongHopSearchModel.BacTaiSan < 2 )
			{

				_thisRow.Visible = false;
			}
			else
			{
				_thisRow.Visible = true;
			}
		}

    
        private void GroupHeader8_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
			string Cap_3 = GetCurrentColumnValue<string>("CAP_3");
			var _thisRow = (sender as GroupHeaderBand);
			if (Cap_3 == null || Cap_3.Length < 4 || _baoCaoTaiSanTongHopSearchModel.BacTaiSan < 3 )
			{

				_thisRow.Visible = false;
			}
			else
			{
				_thisRow.Visible = true;
			}
		}

        private void GroupHeader9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
			string Cap_4= GetCurrentColumnValue<string>("CAP_4");
			var _thisRow = (sender as GroupHeaderBand);
			if (Cap_4 == null || Cap_4.Length < 4 || _baoCaoTaiSanTongHopSearchModel.BacTaiSan < 4 )
			{

				_thisRow.Visible = false;
			}
			else
			{
				_thisRow.Visible = true;
			}
		}

        private void GroupHeader10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
			string Cap_5 = GetCurrentColumnValue<string>("CAP_5");
			var _thisRow = (sender as GroupHeaderBand);
			if (Cap_5 == null || Cap_5.Length < 4 || _baoCaoTaiSanTongHopSearchModel.BacTaiSan < 5 )
			{

				_thisRow.Visible = false;
			}
			else
			{
				_thisRow.Visible = true;
			}
		}

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
			var _thisRow = (sender as GroupHeaderBand);
			if (_row == null /*||  _baoCaoTaiSanTongHopSearchModel.BacDonVi < _row.tree_level_dv*/)
			{
				_thisRow.Visible = false;
			}
			else
			{
				_thisRow.Visible = true;
			}
		}

     

        private void tbl_dv1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
			string _rowTen = GetCurrentColumnValue("DV_TEN_1") as string;
			var _this = sender as XRTableCell;
			if (_row.tree_level_dv >= 1)
			{
				Cap2 = 0;
				Cap3 = 0;
				Cap4 = 0;
				Cap5 = 0;
			}
			Cap1 += 1;
			stt = 0;
			_this.Text = Cap1.ConvertIntToRomanNumber() + ". " + _rowTen;
		}

        private void tbl_dv2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
			string _rowTen = GetCurrentColumnValue("DV_TEN_2") as string;
			var _this = sender as XRTableCell;
			if (_row.tree_level_dv >= 2)
			{
				Cap3 = 0;
				Cap4 = 0;
				Cap5 = 0;
			}
			if (_row.DV_2 != null)
				Cap2 += 1;
			stt = 0;
			_this.Text = Cap1.ConvertIntToRomanNumber() + "." + Cap2.ToString() + ". " + _rowTen;
		}

		private void tbl_dv3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
			string _rowTen = GetCurrentColumnValue("DV_TEN_3") as string;
			var _this = sender as XRTableCell;
			if (_row.tree_level_dv >= 3)
			{
				Cap4 = 0;
				Cap5 = 0;
			}
			if (_row.DV_3 != null)
				Cap3 += 1;
			stt = 0;
			_this.Text = Cap1.ConvertIntToRomanNumber() + "." + Cap2.ToString() + "." + Cap3.ToString() + ". " + _rowTen;
		}
		private void tbl_dv4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
			string _rowTen = GetCurrentColumnValue("DV_TEN_4") as string;
			var _this = sender as XRTableCell;
			if (_row.tree_level_dv >= 4)
			{
				Cap5 = 0;
			}
			if (_row.DV_4 != null)
				Cap4 += 1;
			stt = 0;
			_this.Text = Cap1.ConvertIntToRomanNumber() + "." + Cap2.ToString() + "." + Cap3.ToString() + "." + Cap4.ToString() + ". " + _rowTen;
		}
		private void tbl_dv5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			TS_BCTC_04_DK_TSNN _row = GetCurrentRow() as TS_BCTC_04_DK_TSNN;
			string _rowTen = GetCurrentColumnValue("DV_TEN_5") as string;
			var _this = sender as XRTableCell;
			if (_row.DV_5 != null)
				Cap5 += 1;
			stt = 0;
			_this.Text = Cap1.ConvertIntToRomanNumber() + "." + Cap2.ToString() + "." + Cap3.ToString() + "." + Cap4.ToString() + "." + Cap5.ToString() + ". " + _rowTen;
		}

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		}
	}
}
