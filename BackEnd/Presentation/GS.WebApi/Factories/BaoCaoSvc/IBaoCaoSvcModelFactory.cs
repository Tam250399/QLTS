using GS.Core.Domain.Api.BaoCao;
using GS.Core.Domain.BaoCaos;
using GS.WebApi.Models.BaoCaoSvc;

namespace GS.WebApi.Factories.BaoCaoSvc
{
	public interface IBaoCaoSvcModelFactory
	{
		ReportBaseSearchModel PrepareReportBaseSearchModel(ThamSoNhanBaoCaoSvcSearchModel thamSoNhanBaoCao);
        BaoCaoTaiSanDongBoSearch PrepareReportSearchModel(ThamSoNhanBaoCaoSvcSearchModel thamSoNhanBaoCao);
        ReturnBaoCaoSvc PrepareReturnBaoCaoSvcFromQueueProcess(QueueProcess queue);
		ReturnBaoCaoSvc PrepareReturnBaoCaoSvcFromQueueProcessAndQlCauHinh(QueueProcess queue);
		string PrepareStatementQueueProcess(string baseStatement, ThamSoNhanBaoCaoSvcSearchModel thamSoNhanBaoCao);
	}
}