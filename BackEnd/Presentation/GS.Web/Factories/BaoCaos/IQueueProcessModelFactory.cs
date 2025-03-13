using DevExpress.XtraReports.UI;
using GS.Core.Domain.BaoCaos;
using GS.Core.Domain.HeThong;
using GS.Web.Framework.Models;
using GS.Web.Models.BaoCaos;

namespace GS.Web.Factories.BaoCaos
{
	public partial interface IQueueProcessModelFactory
	{
		void SaveFileBaoCao(XtraReport report, string MaBaoCao, decimal idQueue, string data = null);
		byte[] ExportFileBaoCao(XtraReport report, string MaBaoCao, string data = null, enumEXPORT_FILE_TYPE FileType = enumEXPORT_FILE_TYPE.EXCEL_XLSX);
		QueueProcessSearchModel PrepareQueueProcessSearchModel(QueueProcessSearchModel searchModel);

		QueueProcessListModel PrepareQueueProcessListModel(QueueProcessSearchModel searchModel);

		QueueProcessModel PrepareQueueProcessModel(QueueProcessModel model, QueueProcess item, bool excludeProperties = false);
		/// <summary>
		/// Tạo queue process cho một báo cáo chỉ định
		/// </summary>
		/// <param name="maBaoCao">Mã của báo cáo được xuất</param>
		/// <param name="searchModel">search Model đã được prepare</param>
		/// <param name="FileType">Kiểu file muốn xuất</param>
		/// <param name="ClassReportFullName">Full Name của class Report</param>
		/// <param name="ModelReturnFullName">Full Name của entity trả về (được viết trong tầng GS.CORE)</param>
		/// <returns></returns>
		QueueProcess InsertQueueForSpecificReport(string maBaoCao, BaseSearchModel searchModel, int FileType, string ClassReportFullName, string ModelReturnFullName);
	}
}