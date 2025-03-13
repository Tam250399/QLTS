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
	public partial class rptLIST_THE_KIEM_KE_TSCD
	{
		public rptLIST_THE_KIEM_KE_TSCD()
		{
			InitializeComponent();
		}

        private void TheTaiSan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			TS_LIST_THE_KIEM_KE_TSCD row = GetCurrentRow() as TS_LIST_THE_KIEM_KE_TSCD;
			var subReport = sender as XRSubreport;
			subReport.ReportSource = row.THE_KIEM_KE_TSCD;
		}

       
    }
}

