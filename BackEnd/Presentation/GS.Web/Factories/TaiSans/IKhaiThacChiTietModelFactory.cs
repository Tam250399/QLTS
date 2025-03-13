using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Factories.TaiSans
{
	public  interface IKhaiThacChiTietModelFactory
	{
		#region KhaiThacChiTiet

		KhaiThacChiTietSearchModel PrepareKhaiThacChiTietSearchModel(KhaiThacChiTietSearchModel searchModel);
		KhaiThacChiTietListModel PrepareCapNhatSoTienKhaiThacChiTietListModel(KhaiThacSearchModel searchModel);

		KhaiThacChiTietListModel PrepareKhaiThacChiTietListModel(KhaiThacChiTietSearchModel searchModel);

		KhaiThacChiTietModel PrepareKhaiThacChiTietModel(KhaiThacChiTietModel model, KhaiThacChiTiet item, bool excludeProperties = false);

		void PrepareKhaiThacChiTiet(KhaiThacChiTietModel model, KhaiThacChiTiet item);
		bool ValidateNgayKhaiThac(KhaiThacChiTietModel model);
		#endregion
	}
}
