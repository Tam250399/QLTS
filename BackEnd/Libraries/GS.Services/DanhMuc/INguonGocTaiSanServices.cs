//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.DanhMuc;
using System;
using System.Collections.Generic;

namespace GS.Services.DanhMuc
{
	public partial interface INguonGocTaiSanService
	{
		#region NguonGocTaiSan

		IList<NguonGocTaiSan> GetAllNguonGocTaiSans();

		IList<NguonGocTaiSan> GetAllNguonGocTaiSansChuaDb();

		IPagedList<NguonGocTaiSan> SearchNguonGocTaiSans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? ParentId = null);

		NguonGocTaiSan GetNguonGocTaiSanById(decimal Id);

		IList<NguonGocTaiSan> GetNguonGocTaiSanByIds(decimal[] newsIds);

		void InsertNguonGocTaiSan(NguonGocTaiSan entity);

		void UpdateNguonGocTaiSan(NguonGocTaiSan entity);

		void DeleteNguonGocTaiSan(NguonGocTaiSan entity);

		IList<NguonGocTaiSan> GetNguonGocTaiSans(string Ma = null, string TreeNode = null);

		IList<NguonGocTaiSan> GetNguonGocTaiSansForDDL(bool isTichThu = false);

		NguonGocTaiSan GetNguonGocTaiSanByMa(string Ma = null);

		void UpdateListNguonGocTaiSan(List<NguonGocTaiSan> entities);

		void InsertListNguonGocTaiSan(List<NguonGocTaiSan> entities);

		void GenTreeNode(NguonGocTaiSan entity);

		NguonGocTaiSan GetNguonGocTaiSanByDbID(int DB_PARENT_ID = 0);

		int GetCount(decimal id);

		Boolean CheckParent(decimal id);

		bool CheckMaNguonGocTaiSan(decimal? id = 0, string ma = null);

		int GetCountSub(decimal? ParentId = 0);
		bool GetUsed(decimal? Id = 0);
		bool checkIsTaiSanTichThu(decimal? nguonGocId);
		#endregion NguonGocTaiSan
	}
}