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
    public partial interface IDongXeService
    {
        #region DongXe
        IList<DongXe> GetAllDongXes();
        IList<DongXe> GetListDongXes(decimal? nhanXeId = 0);
        IPagedList<DongXe> SearchDongXes(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        DongXe GetDongXeById(decimal Id);
        IList<DongXe> GetDongXeByIds(decimal[] newsIds);
        void InsertDongXe(DongXe entity);
        void InsertDongXe(List<DongXe> entities);
        void UpdateDongXe(DongXe entity);
        void UpdateDongXe(List<DongXe> entities);
        void DeleteDongXe(DongXe entity);

        bool CheckMaDongXe(decimal id = 0, string ma = null);
        DongXe GetDongXeByMa(string ma=null,string Ten=null);
        #endregion
    }
}
