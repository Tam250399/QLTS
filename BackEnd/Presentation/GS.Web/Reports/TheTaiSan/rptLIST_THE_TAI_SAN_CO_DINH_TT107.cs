using System;
using System.Drawing;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using GS.Core.Domain.BaoCaos.TheTaiSan;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;
using GS.Web.Models.BaoCaos.TS_CDKT;

namespace GS.Web.Reports.TheTaiSan
{
	public partial class rptLIST_THE_TAI_SAN_CO_DINH_TT107
	{
		private readonly BaoCaoTaiSanChiTietSearchModel _baoCaoTaiSanChiTietSearchModel;
		private readonly CauHinhBaoCaoModel _cauHinhBaoCaoModel;
		private readonly CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel;
		public rptLIST_THE_TAI_SAN_CO_DINH_TT107(
			BaoCaoTaiSanChiTietSearchModel baoCaoTaiSanChiTietSearchModel,
			CauHinhBaoCaoModel cauHinhBaoCaoModel,
			CauHinhBaoCaoChungModel cauHinhBaoCaoChungModel
			)
		{
			InitializeComponent();
			this._baoCaoTaiSanChiTietSearchModel = baoCaoTaiSanChiTietSearchModel;
			this._cauHinhBaoCaoModel = cauHinhBaoCaoModel;
			this._cauHinhBaoCaoChungModel = cauHinhBaoCaoChungModel;
		}

        private void TheTaiSan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_LIST_THE_TAI_SAN_CO_DINH row = GetCurrentRow() as TS_LIST_THE_TAI_SAN_CO_DINH;
			var subReport = sender as XRSubreport;
			subReport.ReportSource = row.THE_TAI_SAN_CO_DINH_TT107;
		}

       
    }
}

