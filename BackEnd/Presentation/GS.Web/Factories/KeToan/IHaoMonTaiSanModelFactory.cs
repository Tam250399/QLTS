using GS.Core.Domain.BienDongs;
using GS.Core.Domain.Common;
using GS.Core.Domain.KT;
using GS.Core.Domain.NghiepVu;
using GS.Web.Models.DongBoTaiSan;
using GS.Web.Models.KeToan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Factories.KeToan
{
	public partial interface IHaoMonTaiSanModelFactory
	{
		HaoMonTaiSanModel PrepareHaoMonTaiSanModel(YeuCau yeuCau, YeuCauChiTiet yeuCauChiTiet, HaoMonTaiSanModel haoMonTaiSanModel);

		void InsertUpdateHaoMonTaiSanModelFromTangMoiTS(YeuCau yeuCau, YeuCauChiTiet yeuCauChiTiet, HaoMonTaiSanModel haoMonTaiSanModel);
		HaoMonTaiSanModel PrepareHaoMonTaiSanImport(BienDong bienDong, BienDongChiTiet bienDongChiTiet, HaoMonTaiSanModel haoMonTaiSanModel);
		void PrepareHaoMonTaiSan(HaoMonTaiSanModel model, HaoMonTaiSan item);
		HaoMonTaiSanSearchModel PrepareHaoMonTaiSanSearchModel(HaoMonTaiSanSearchModel searchModel);
		HaoMonTaiSanListModel PrepareHaoMonTaiSanListModel(HaoMonTaiSanSearchModel searchModel);
		HaoMonTaiSanModel PrepareHaoMonForEditModel(HaoMonTaiSanModel model, HaoMonTaiSan item);
		MessageReturn InSertHaoMonDongBo(HaoMonDBModel model, int nguonId);
		void PrepareHaoMonTaiSanFromHaoMonDB(HaoMonDBModel model, HaoMonTaiSan item);



	}
}
