//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.DanhMuc;
using System.Collections.Generic;

namespace GS.Services.DanhMuc
{
    public partial interface ILoaiDonViService
    {
        #region LoaiDonVi
        IList<LoaiDonVi> GetAllLoaiDonVis();
        IList<LoaiDonVi> GetAllLoaiDonVisChuaDb();
        IPagedList<LoaiDonVi> SearchLoaiDonVis(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal ParentID = 0, decimal? TreeLevel = 1);
        LoaiDonVi GetLoaiDonViById(decimal Id);
        IList<LoaiDonVi> GetLoaiDonViByIds(decimal[] newsIds);
        void InsertLoaiDonVi(LoaiDonVi entity);
        void UpdateLoaiDonVi(LoaiDonVi entity);
        void DeleteLoaiDonVi(LoaiDonVi entity);
        int GetCountLoaiDonViSub(decimal ParentID = 0, string KeySearch = null);
        IList<LoaiDonVi> GetListDonViByTreeNode(decimal? id = 0);
        void GenTreeNode(LoaiDonVi entity);
        LoaiDonVi GetLoaiDonViByMa(string Ma = null);
        void UpdateListLoaiDonVi(List<LoaiDonVi> entities);
        void InsertListLoaiDonVi(List<LoaiDonVi> entities);
        LoaiDonVi GetLoaiDonViByIdDb(int ID_DB=0);
        IList<LoaiDonVi> GetLoaiDonViForBaoCao();
        bool CheckMaLoaiDonVi(decimal id, string ma);
		string GetTenLoaiDonViByIds(IList<decimal> ids);
        #endregion
    }
}
