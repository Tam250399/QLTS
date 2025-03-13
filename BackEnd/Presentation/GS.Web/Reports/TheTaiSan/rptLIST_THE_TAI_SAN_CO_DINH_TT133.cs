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
	public partial class rptLIST_THE_TAI_SAN_CO_DINH_TT133
	{
		public rptLIST_THE_TAI_SAN_CO_DINH_TT133()
		{
			InitializeComponent();
		}

        private void TheTaiSan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_LIST_THE_TAI_SAN_CO_DINH_TT133 row = GetCurrentRow() as TS_LIST_THE_TAI_SAN_CO_DINH_TT133;
			var subReport = sender as XRSubreport;
			subReport.ReportSource = row.TS_THE_TAI_SAN_CO_DINH_TT133;
		}

       
    }
}

