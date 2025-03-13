using System;
using System.Drawing;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core.Domain.BaoCaos.TheTaiSan;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;

namespace GS.Web.Reports.TheTaiSan
{
	public partial class rptTHE_KIEM_KE_TSCD
	{
		private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
		private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
		private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
		public rptTHE_KIEM_KE_TSCD(
			BaoCaoTaiSanChiTietSearchModel baoCaoTaiSanChiTietSearchModel,
			CauHinhBaoCaoModel cauHinhBaoCaoModel,
			CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel)
		{
			InitializeComponent();
			this._baoCaoTaiSanChiTietSearchModel = baoCaoTaiSanChiTietSearchModel;
			this._cauHinhBaoCaoModel = cauHinhBaoCaoModel;
			this._cauHinhBaoCaoChungModel = cauHinhBaoCaoChungModel;
		}
		public rptTHE_KIEM_KE_TSCD()
		{
			InitializeComponent();
		}
		private void table1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			cellBoNganh.Text = _baoCaoTaiSanChiTietSearchModel.TEN_BO_NGANH;
			cellTenDonVi.Text = _baoCaoTaiSanChiTietSearchModel.TEN_DON_VI;
		}

		private void rptTHE_KIEM_KE_TSCD_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{

		}
	}
}
