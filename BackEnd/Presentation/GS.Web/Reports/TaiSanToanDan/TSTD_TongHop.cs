using System;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Services.DanhMuc;
using GS.Web.Models.BaoCaos;

namespace GS.Web.Reports.TaiSanToanDan
{
    public partial class TSTD_TongHop
    {
        private readonly IWorkContext _workContext;
        private readonly DateTime _ngayBaoCaoFrom;
        private readonly DateTime _ngayBaoCaoTo;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly Decimal _donViBoPhanId;
        private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
        private readonly Int32 _donViTien;
        private readonly Int32 _donViDienTich;
        public TSTD_TongHop(IWorkContext workContext,
            DateTime ngayBaoCaoFrom,
            DateTime ngayBaoCaoTo,
            IDonViBoPhanService donViBoPhanService,
            Decimal donViBoPhanId,
            CauHinhBaoCaoModel cauHinhBaoCaoModel,
            Int32 donViTien,
            Int32 donViDienTich
            )
        {
            InitializeComponent();
            this._workContext = workContext;
            this._ngayBaoCaoFrom = ngayBaoCaoFrom;
            this._ngayBaoCaoTo = ngayBaoCaoTo;
            this._donViBoPhanService = donViBoPhanService;
            this._donViBoPhanId = donViBoPhanId;
            this._cauHinhBaoCaoModel = cauHinhBaoCaoModel;
            this._donViDienTich = donViDienTich;
            this._donViTien = donViTien;
        }
        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            leftHeader.Text = _cauHinhBaoCaoModel.DonViKhaiThac + "\r\n" + _workContext.CurrentDonVi.TEN_DON_VI;
            centerHeader.Text = "BÁO CÁO TỔNG HỢP TÌNH HÌNH TĂNG, GIẢM TÀI SẢN ĐƯỢC XÁC LẬP QUYỀN SỞ HỮU TOÀN DÂN \r\n  Kỳ báo cáo:Từ ngày " + _ngayBaoCaoFrom.toDateVNString()+" đến ngày " + _ngayBaoCaoTo.toDateVNString();
            if (this._donViBoPhanId > 0)
                centerHeader.Text = centerHeader.Text + "\r\n" + _donViBoPhanService.GetDonViBoPhanById(_donViBoPhanId).TEN;
            noteHeader.Text = "ĐVT cho: Số lượng là: Cái, khuôn viên; Diện tích là: " + _donViDienTich.ToStringDonViDienTichTSTD() + "; Giá trị là: " + _donViTien.ToStringDonViTienTSTD() + ".";
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
        }

        private void TSTD_TongHop_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Margins.Top = Convert.ToInt32(_cauHinhBaoCaoModel.MarginTop * 0.3937 * 100);
            Margins.Bottom = Convert.ToInt32(_cauHinhBaoCaoModel.MarginBottom * 0.3937 * 100);
            Margins.Left = Convert.ToInt32(_cauHinhBaoCaoModel.MarginLeft * 0.3937 * 100);
            Margins.Right = Convert.ToInt32(_cauHinhBaoCaoModel.MarginRight * 0.3937 * 100);
        }

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
	}
}
