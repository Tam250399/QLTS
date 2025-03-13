using GS.Web.Models.TaiSans;
using System.Collections.Generic;

namespace GS.Web.Factories.TaiSans
{
	public interface ICapNhatTaiSanModelFactory
	{
		TaiSanSearchModel PrepareCapNhatTaiSanSearchModel(TaiSanSearchModel searchModel);
		TaiSanSearchModel PrepareCapNhatTaiSanDatNhaSearchModel(TaiSanSearchModel searchModel);
		TaiSanListModel PrepareCapNhatTaiSanListModel(TaiSanSearchModel searchModel);
		List<TaiSanExportOto> PrepareExportTaiSanOto(TaiSanSearchModel searchModel);
	}
}