using System;
using DevExpress.XtraReports.UI;

namespace GS.Web.Reports.TS_BCCT
{
    public partial class CCThongTin_HuuHinh_B03_CCTT
    {
        public CCThongTin_HuuHinh_B03_CCTT()
        {
            InitializeComponent();
        }

		private void lblDatetimeCreate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            lblDatetimeCreate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
	}
}
