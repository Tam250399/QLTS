using GS.Core.Domain.SHTD;
using GS.WebApi.Models.ConsumingApi.TaiSanToanDan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Factories.ConsumingApi
{
    public interface IKho_TaiSanToanDanFactory
    {
		//DuLieuDongBoQuyetDinhTichThu PrepareDuLieuDongBoQuyetDinhTichThu(List<QuyetDinhTichThu> ListQuyetDinhTichThu);
		//DuLieuDongBoPhuongAnXuLy PrepareDuLieuDongBoPhuongAnXuLy(List<XuLy> ListXuLy);
		//DuLieuDongBoKetQuaXuLy PrepareDuLieuDongBoKetQuaXuLy(List<KetQua> ListKetQua);
		//DuLieuDongBoSoSachThuChi PrepareDuLieuDongBoSoSachThuChi(List<ThuChi> ListThuChi);
		List<confiscateDecision> PrepareDuLieuDongBoQuyetDinhTichThu(List<QuyetDinhTichThu> ListQuyetDinhTichThu);
		List<nationalPublicAssets> PrepareDuLieuDongBoTaiSanTd(List<TaiSanTd> ListTaiSanTd);
		List<handlingDecisionPlan> PrepareDuLieuDongBoPhuongAnXuLy(List<XuLy> ListXuLy);
		List<handlingPlanNationalPublicAsset> PrepareDuLieuDongBoTaiSanPhuongAnXuLy(List<TaiSanTdXuLy> ListTaiSanTdXuLy);
		List<handlingResultNationalPublicAsset> PrepareDuLieuDongBoKetQuaXuLy(List<KetQua> ListKetQua);
		List<handlingDecisionAccounting> PrepareDuLieuDongBoSoSachThuChi(List<ThuChi> ListThuChi);

	}
}
