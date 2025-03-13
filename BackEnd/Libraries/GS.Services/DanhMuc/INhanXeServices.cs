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
    public partial interface INhanXeService
    {
        #region NhanXe
        IList<NhanXe> GetAllNhanXes();
        IPagedList<NhanXe> SearchNhanXes(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        NhanXe GetNhanXeById(decimal Id);
        IList<NhanXe> GetNhanXeByIds(decimal[] newsIds);
        void InsertNhanXe(NhanXe entity);
        void UpdateNhanXe(NhanXe entity);
        void DeleteNhanXe(NhanXe entity);

        bool CheckMaNhanXe(decimal id = 0, string ma = null);
        NhanXe GetNhanXeByMaTen(string ma=null, string Ten = null);
        void InsertListNhanXe(List<NhanXe> entities);
        void UpdateListNhanXe(List<NhanXe> entities);
        #endregion
    }
}
