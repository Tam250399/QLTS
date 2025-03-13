//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.DanhMuc;
using System;
using System.Collections.Generic;

namespace GS.Services.DanhMuc
{
    public partial interface ILoaiTaiSanService
    {
        #region LoaiTaiSan
        IList<LoaiTaiSan> GetAllLoaiTaiSans(decimal CheDoHaoMon = 0);
        IList<LoaiTaiSan> GetAllLoaiTaiSansChuaDb();
        IList<LoaiTaiSan> GetAllLoaiTaiSanToanDansChuaDb();
        IPagedList<LoaiTaiSan> SearchLoaiTaiSans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? ParentId = 0, decimal? ChedoId = 0, decimal? loaiHinhTaiSanId = 0, decimal? id = 0, bool isHasNoChildrend= false);
        IPagedList<LoaiTaiSan> SearchLoaiTaiSanToanDan(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? ParentId = 0, decimal? ChedoId = 0, decimal? loaiHinhTaiSanId = 0, decimal? id = 0, bool isHasNoChildrend= false);
		/// <summary>
		/// search tất cả tài sản. 
		/// *Trong trường hợp không có keySeach thì lấy tất cả loại tài sản*
		/// </summary>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <param name="Keysearch"></param>
		/// <param name="ParentId"></param>
		/// <param name="ChedoId"></param>
		/// <param name="loaiHinhTaiSanId"></param>
		/// <param name="id"></param>
		/// <param name="isHasNoChildrend">chỉ lấy ra những loại tài sản không có con</param>
		/// <returns></returns>
		IPagedList<LoaiTaiSan> SearchAllLoaiTaiSan(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? ParentId = 0, decimal? ChedoId = 0, decimal? loaiHinhTaiSanId = 0, decimal? id = 0, bool isHasNoChildrend = false);

		LoaiTaiSan GetLoaiTaiSanById(decimal Id);
        IList<LoaiTaiSan> GetLoaiTaiSanByIds(decimal[] newsIds);
        void InsertLoaiTaiSan(LoaiTaiSan entity);
        void UpdateLoaiTaiSan(LoaiTaiSan entity);
        void InsertLoaiTaiSan(List<LoaiTaiSan> entities);
        void UpdateLoaiTaiSan(List<LoaiTaiSan> entities);
        void DeleteLoaiTaiSan(LoaiTaiSan entity);
        int GetCountSub(decimal? ParentId = 0);
        IList<LoaiTaiSan> GetForInputSearch(string KeySearch = null, decimal? loaiHinhTSId = 0, decimal? cheDoId = 0);
        LoaiTaiSan GetLoaiTaiSanByMa(string ma, String cheDoHaoMonId = enumCheDoHaoMon_ThongTu.CDHM_TT45);
        IList<LoaiTaiSan> GetListLoaiTaiSanTreeNodeByRoot(decimal? cheDoHaoMonId = 0, decimal? LoaiHinhTaiSanId = 0, List<decimal> ListLoaiHinhTaiSanId = null, decimal? ParentLoaiTaiSanId = null, List<decimal> ListIgnoreLTS = null);
        IList<LoaiTaiSan> GetListLoaiTaiSanDatNhaTreeNodeByRoot(decimal? cheDoHaoMonId = 0, decimal? LoaiHinhTaiSanId = 0, int[] ListLoaiHinhTaiSanId = null, decimal? ParentLoaiTaiSanId = null, List<decimal> ListIgnoreLTS = null);
        bool CheckMaLoaiTaiSan(decimal? id = 0, string ma = null, decimal? CheDoHaoMonId = 0);
        LoaiTaiSan GetLoaiTaiSanByTen(string Ten);
        LoaiTaiSan GetLoaiTaiSanByIdDb(decimal IdDb = 0);
        List<string> GetTenLoaiTaiSan(List<decimal> ids);
        #endregion
        #region Cache
        IList<LoaiTaiSan> GetTable();
        LoaiTaiSan GetCacheLoaiTaiSanByMa(string ma);
        LoaiTaiSan GetCacheLoaiTaiSanByTen(string Ten);
        IList<LoaiTaiSan> GetLoaiTaiSanByTreeLevel(decimal TreeLevel = 0);
        IList<LoaiTaiSan> GetLoaiTaiSans(decimal LoaiHinhTaiSanId = 0, string TreeNode = null, decimal cheDoId = 0, List<decimal> ListLoaiHinhTaiSanId = null);
        #endregion
    }
}
