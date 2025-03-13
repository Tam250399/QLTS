using GS.Core.Domain.BienDongs;
using GS.Web.Models.BienDongs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Factories.BienDongs
{
	public interface ITrungGianBDYCModelFactory
	{
		TrungGianBDYCSearchModel PrepareTrungGianBDYCSearchModel(TrungGianBDYCSearchModel searchModel);

		TrungGianBDYCListModel PrepareTrungGianBDYCListModel(TrungGianBDYCSearchModel searchModel);

		TrungGianBDYCModel PrepareTrungGianBDYCModel(TrungGianBDYCModel model, TrungGianBDYC item, bool isInfoBienDong = false);
		TrungGianBDYCModel PrepareThongTinChiTietTaiSan(TrungGianBDYCModel model);
		TrungGianBDYCModel PrepareTTNguonVon(TrungGianBDYCModel model);
		TrungGianBDYCModel PrepareTTGiamToanBo(TrungGianBDYCModel model);
		ThongTinChungTaiSanModel PrepareTTChung(TrungGianBDYCModel model);
		TrungGianBDYCListModel PrepareListGiamBanThanhLy(TrungGianBDYCSearchModel searchModel);
		TrungGianBDYCModel PrepareBienDongModelGiamTaiSan(TrungGianBDYCModel model, TrungGianBDYC item);

	}
}
