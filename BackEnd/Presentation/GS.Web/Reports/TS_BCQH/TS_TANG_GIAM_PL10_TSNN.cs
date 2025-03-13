using System;
using DevExpress.XtraReports.UI;

namespace GS.Web.Reports.TS_BCQH
{
    public partial class TS_TANG_GIAM_PL10_TSNN
    {
        public TS_TANG_GIAM_PL10_TSNN()
        {
            InitializeComponent();
        }

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
	}
}
