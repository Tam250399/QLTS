using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Web.Models.BienDongs;
using GS.Web.Models.NghiepVu;

namespace GS.Web.Factories.BienDongs
{
	public partial interface IThaoTacBienDongModelFactory
	{
		ResultAction DuyetYeuCauFunc(decimal YeuCauId, YeuCau yeuCau = null);
		ResultAction CapNhatTaiSanThayDoiDiaBanHienTrang(decimal YeuCauId, YeuCau yeuCau = null);
		ResultAction CapNhatSoChoNgoiOto(decimal YeuCauId, YeuCau yeuCau = null);

		ResultAction HuyDuyetBienDongFunc(decimal BienDongId, string Note);

		ResultAction KhongDuyetYeuCauFunc(decimal YeuCauId, string Note);
		ResultAction CapNhatQuyenXuLyTaiSan(TaiSan taiSan, decimal idToUser);
		ResultAction CapNhatBienDongSaiHienTrang(YeuCauChiTietModel model);
		ResultAction DuyetYeuCauFunc1(decimal bienDongId);
	}
}