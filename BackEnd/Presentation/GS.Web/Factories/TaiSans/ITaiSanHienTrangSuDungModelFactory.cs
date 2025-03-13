using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;

namespace GS.Web.Factories.TaiSans
{
	public partial interface ITaiSanHienTrangSuDungModelFactory
	{
		TaiSanHienTrangSuDungSearchModel PrepareTaiSanHienTrangSuDungSearchModel(TaiSanHienTrangSuDungSearchModel searchModel);

		TaiSanHienTrangSuDungListModel PrepareTaiSanHienTrangSuDungListModel(TaiSanHienTrangSuDungSearchModel searchModel);

		TaiSanHienTrangSuDungModel PrepareTaiSanHienTrangSuDungModel(TaiSanHienTrangSuDungModel model, TaiSanHienTrangSuDung item, bool excludeProperties = false);
		void InsertHienTrangSuDungForBienDong(decimal bienDongId, decimal taiSanId, string jsonHienTrang);
		void UpdateTaSanHienTrangForBienDong(decimal bienDongId, decimal taiSanId, string jsonHienTrang);
		void UpdateHienTrangSuDungObjOfBienDongChiTiet(decimal bienDongId, string jsonHienTrang);

	}
}