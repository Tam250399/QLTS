using GS.Core;
using GS.Core.Domain.DanhMuc;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Services.DanhMuc
{
	public interface IDonViLichSuService
	{
		IList<DonViLichSu> GetDonViLichSus();
		IPagedList<DonViLichSu> SearchDonViLichSus(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
		DonViLichSu GetDonViLichSuById(decimal Id);
		IList<DonViLichSu> GetDonViLichSuByIds(decimal[] newsIds);
		void InsertDonViLichSu(DonViLichSu entity);
		void UpdateDonViLichSu(DonViLichSu entity);
		void InsertDonViLichSu(List<DonViLichSu> entities);
		void UpdateDonViLichSu(List<DonViLichSu> entities);
		void DeleteDonViLichSu(DonViLichSu entity);
	}
}
