using GS.Core;
using GS.Core.Domain.TaiSans;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Services.TaiSans
{
	public interface IKhaiThacChiTietServices
	{
		#region KhaiThacChiTiet
		IList<KhaiThacChiTiet> GetAllKhaiThacChiTiets();
		IPagedList<KhaiThacChiTiet> SearchKhaiThacChiTiets(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? khaiThacId = null);
		IPagedList<KhaiThacChiTiet> SearchKhaiThacChiTietsForCapNhatSoTien(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string QUYET_DINH_SO = null, decimal LOAI_HINH_KHAI_THAC_ID = 0, DateTime? QUYET_DINH_NGAY = null, string HOP_DONG_SO = null, DateTime? HOP_DONG_NGAY = null, DateTime? KHAI_THAC_NGAY_TU = null, DateTime? KHAI_THAC_NGAY_DEN = null, decimal donviId = 0);
		IList<KhaiThacChiTiet> GetKhaiThacChiTietByKhaiThacId(decimal? khaiThacId = null);
		KhaiThacChiTiet GetKhaiThacChiTietById(decimal Id);
		IList<KhaiThacChiTiet> GetKhaiThacChiTietByIds(decimal[] newsIds);
		void InsertKhaiThacChiTiet(KhaiThacChiTiet entity);
		void UpdateKhaiThacChiTiet(KhaiThacChiTiet entity);
		void DeleteKhaiThacChiTiet(KhaiThacChiTiet entity);
		void DeleteKhaiThacChiTiets(IEnumerable< KhaiThacChiTiet> entities);
		KhaiThacChiTiet GetMapByKhaiThacIdAndTaiSanId(decimal khaiThacId, decimal taiSanId);
		bool checkTrungKhaiThacTaiSan(decimal KhaiThacId, decimal TaiSanId);

		#endregion
	}
}
